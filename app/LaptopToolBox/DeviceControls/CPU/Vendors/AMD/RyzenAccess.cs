using System;
using System.Collections.Generic;
using System.Threading;
using Ninject;

[assembly: CLSCompliant(false)]


namespace LaptopToolBox.DeviceControls.CPU.Vendors.AMD
{
    public class RyzenAccess : IRyzenAccess
    {
        private readonly Dictionary<RyzenAccessStatus, string> _status = new()
        {
            { RyzenAccessStatus.BAD, "BAD" },
            { RyzenAccessStatus.OK, "OK" },
            { RyzenAccessStatus.FAILED, "Failed" },
            { RyzenAccessStatus.UNKNOWN_CMD, "Unknown Command" },
            { RyzenAccessStatus.CMD_REJECTED_PREREQ, "CMD Rejected Prereq" },
            { RyzenAccessStatus.CMD_REJECTED_BUSY, "CMD Rejected Busy" }
        };
        
        private readonly Mutex _amdSmuMutex;
        private const ushort SmuTimeout = 8192;

        private readonly IAmdAddressesProvider _addressesProvider;
        private readonly IOls _ols;


        [Inject]
        public RyzenAccess(IAmdAddressesProvider addressesProvider, IOls ols)
        {
            _addressesProvider = addressesProvider;
            
            _ols = ols;
            _ols.InitializeOls();
            
            _amdSmuMutex = new Mutex();

            switch (_ols.GetDllStatus())
            {
                case (uint)Ols.OlsDllStatus.OLS_DLL_NO_ERROR:
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
                    throw new ApplicationException("WinRing OLS_DRIVER_NOT_LOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
                    throw new ApplicationException("WinRing OLS_UNSUPPORTED_PLATFORM");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_NOT_FOUND");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_UNLOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
                    throw new ApplicationException("WinRing DRIVER_NOT_LOADED_ON_NETWORK");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
                    throw new ApplicationException("WinRing OLS_DLL_UNKNOWN_ERROR");
            }
            
            switch (_ols.GetStatus())
            {
                case (uint)Ols.Status.NO_ERROR:
                    break;
                case (uint)Ols.Status.DLL_NOT_FOUND:
                    throw new ApplicationException("WinRing DLL_NOT_FOUND");
                case (uint)Ols.Status.DLL_INCORRECT_VERSION:
                    throw new ApplicationException("WinRing DLL_INCORRECT_VERSION");
                case (uint)Ols.Status.DLL_INITIALIZE_ERROR:
                    throw new ApplicationException("WinRing DLL_INITIALIZE_ERROR");
            }
        }

        public uint GetCpuId()
        {
            uint eax = 0, ebx = 0, ecx = 0, edx = 0;
            if (_ols.Cpuid(0x00000001, ref eax, ref ebx, ref ecx, ref edx) == 1)
            {
                return eax;
            }
            return 0;
        }

        public void Dispose()
        {
            _ols.DeinitializeOls();
        }

        public RyzenAccessStatus SendMp1(uint message, ref uint[] arguments)
        {
            return SendMsg(_addressesProvider.MP1_ADDR_MSG, _addressesProvider.MP1_ADDR_RSP, _addressesProvider.MP1_ADDR_ARG, message, ref arguments);
        }

        public RyzenAccessStatus SendPsmu(uint message, ref uint[] arguments)
        {
            return SendMsg(_addressesProvider.PSMU_ADDR_MSG, _addressesProvider.PSMU_ADDR_RSP, _addressesProvider.PSMU_ADDR_ARG, message, ref arguments);
        }

        public RyzenAccessStatus SendMp164(uint message, ref ulong[] arguments)
        {
            return SendMsg64(_addressesProvider.MP1_ADDR_MSG, _addressesProvider.MP1_ADDR_RSP, _addressesProvider.MP1_ADDR_ARG, message, ref arguments);
        }

        public RyzenAccessStatus SendPsmu64(uint message, ref ulong[] arguments)
        {
            return SendMsg64(_addressesProvider.PSMU_ADDR_MSG, _addressesProvider.PSMU_ADDR_RSP, _addressesProvider.PSMU_ADDR_ARG, message, ref arguments);
        }

        public bool SendSmuCommand(uint smuAddrMsg, uint smuAddrRsp, uint smuAddrArg, uint msg, ref uint[] args)
        {
            return (SendMsg(smuAddrMsg, smuAddrRsp, smuAddrArg, msg, ref args) == RyzenAccessStatus.OK);
        }

        public RyzenAccessStatus SendMsg(uint smuAddrMsg, uint smuAddrRsp, uint smuAddrArg, uint msg, ref uint[] args)
        {
            var timeout = SmuTimeout;
            var cmdArgs = new uint[6];
            var argsLength = args.Length;
            uint status = 0;

            if (argsLength > cmdArgs.Length)
                argsLength = cmdArgs.Length;

            for (var i = 0; i < argsLength; ++i)
                cmdArgs[i] = args[i];

            if (_amdSmuMutex.WaitOne(5000))
            {
                // Clear response register
                bool temp;
                do
                    temp = SmuWriteReg(smuAddrRsp, 0);
                while ((!temp) && --timeout > 0);

                if (timeout == 0)
                {
                    _amdSmuMutex.ReleaseMutex();
                    SmuReadReg(smuAddrRsp, ref status);
                    return (RyzenAccessStatus)status;
                }

                // Write data
                for (var i = 0; i < cmdArgs.Length; ++i)
                    SmuWriteReg(smuAddrArg + (uint)(i * 4), cmdArgs[i]);

                // Send message
                SmuWriteReg(smuAddrMsg, msg);

                // Wait done
                if (!SmuWaitDone(smuAddrRsp))
                {
                    _amdSmuMutex.ReleaseMutex();
                    SmuReadReg(smuAddrRsp, ref status);
                    return (RyzenAccessStatus)status;
                }

                // Read back args
                for (var i = 0; i < args.Length; ++i)
                    SmuReadReg(smuAddrArg + (uint)(i * 4), ref args[i]);
            }

            _amdSmuMutex.ReleaseMutex();
            SmuReadReg(smuAddrRsp, ref status);

            return (RyzenAccessStatus)status;
        }

        public RyzenAccessStatus SendMsg64(uint smuAddrMsg, uint smuAddrRsp, uint smuAddrArg, uint msg, ref ulong[] args)
        {
            var timeout = SmuTimeout;
            var cmdArgs = new ulong[6];
            var argsLength = args.Length;
            uint status = 0;

            if (argsLength > cmdArgs.Length)
                argsLength = cmdArgs.Length;

            for (var i = 0; i < argsLength; ++i)
                cmdArgs[i] = args[i];

            if (_amdSmuMutex.WaitOne(5000))
            {
                // Clear response register
                bool temp;
                do
                    temp = SmuWriteReg(smuAddrRsp, 0);
                while ((!temp) && --timeout > 0);

                if (timeout == 0)
                {
                    _amdSmuMutex.ReleaseMutex();
                    SmuReadReg(smuAddrRsp, ref status);
                    return (RyzenAccessStatus)status;
                }

                // Write data
                for (var i = 0; i < cmdArgs.Length; ++i)
                    SmuWriteReg64(smuAddrArg + (uint)(i * 4), cmdArgs[i]);

                // Send message
                SmuWriteReg64(smuAddrMsg, msg);

                // Wait done
                if (!SmuWaitDone(smuAddrRsp))
                {
                    _amdSmuMutex.ReleaseMutex();
                    SmuReadReg(smuAddrRsp, ref status);
                    return (RyzenAccessStatus)status;
                }

                // Read back args
                for (var i = 0; i < args.Length; ++i)
                    SmuReadReg64(smuAddrArg + (uint)(i * 4), ref args[i]);
            }

            _amdSmuMutex.ReleaseMutex();
            SmuReadReg(smuAddrRsp, ref status);

            return (RyzenAccessStatus)status;
        }

        public bool SmuWaitDone(uint smuAddrRsp)
        {
            bool res;
            var timeout = SmuTimeout;
            uint data = 0;

            do
                res = SmuReadReg(smuAddrRsp, ref data);
            while ((!res || data != 1) && --timeout > 0);

            if (timeout == 0 || data != 1) res = false;

            return res;
        }


        private bool SmuWriteReg(uint addr, uint data)
        {
            if (_ols.WritePciConfigDwordEx(_addressesProvider.SMU_PCI_ADDR, _addressesProvider.SMU_OFFSET_ADDR, addr) == 1)
            {
                return _ols.WritePciConfigDwordEx(_addressesProvider.SMU_PCI_ADDR, _addressesProvider.SMU_OFFSET_DATA, data) == 1;
            }
            return false;
        }

        private bool SmuReadReg(uint addr, ref uint data)
        {
            if (_ols.WritePciConfigDwordEx(_addressesProvider.SMU_PCI_ADDR, _addressesProvider.SMU_OFFSET_ADDR, addr) == 1)
            {
                return _ols.ReadPciConfigDwordEx(_addressesProvider.SMU_PCI_ADDR, _addressesProvider.SMU_OFFSET_DATA, ref data) == 1;
            }
            return false;
        }

        private bool SmuWriteReg64(uint addr, ulong data)
        {
            if (_ols.WritePciConfigDwordEx(_addressesProvider.SMU_PCI_ADDR, _addressesProvider.SMU_OFFSET_ADDR, addr) == 1)
            {
                return _ols.WritePciConfigDwordEx64(_addressesProvider.SMU_PCI_ADDR, _addressesProvider.SMU_OFFSET_DATA, data) == 1;
            }
            return false;
        }

        private bool SmuReadReg64(uint addr, ref ulong data)
        {
            if (_ols.WritePciConfigDwordEx(_addressesProvider.SMU_PCI_ADDR, _addressesProvider.SMU_OFFSET_ADDR, addr) == 1)
            {
                return _ols.ReadPciConfigDwordEx64(_addressesProvider.SMU_PCI_ADDR, _addressesProvider.SMU_OFFSET_DATA, ref data) == 1;
            }
            return false;
        }
        private uint ReadDword(uint value)
        {
            _ols.WritePciConfigDword(_addressesProvider.SMU_PCI_ADDR, (byte)_addressesProvider.SMU_OFFSET_ADDR, value);
            return _ols.ReadPciConfigDword(_addressesProvider.SMU_PCI_ADDR, (byte)_addressesProvider.SMU_OFFSET_DATA);
        }


        private bool Wait4Rsp(uint smuAddrRsp)
        {
            var res = false;
            ushort timeout = 1000;
            uint data = 0;
            
            while ((!res || data == 0) && --timeout > 0)
            {
                res = SmuReadReg(smuAddrRsp, ref data);
                Thread.Sleep(1);
            }

            if (timeout == 0 || data != 1)
            {
                res = false;
            }

            return res;
        }

        private string GetStringPart(uint val)
        {
            return val != 0 ? Convert.ToChar(val).ToString() : "";
        }

        private string IntToStr(uint val)
        {
            var part1 = val & 0xff;
            var part2 = val >> 8 & 0xff;
            var part3 = val >> 16 & 0xff;
            var part4 = val >> 24 & 0xff;

            return $"{GetStringPart(part1)}{GetStringPart(part2)}{GetStringPart(part3)}{GetStringPart(part4)}";
        }

        public string GetCpuName()
        {
            var model = "";
            uint eax = 0, ebx = 0, ecx = 0, edx = 0;

            if (_ols.Cpuid(0x80000002, ref eax, ref ebx, ref ecx, ref edx) == 1)
            {
                model = model + IntToStr(eax) + IntToStr(ebx) + IntToStr(ecx) + IntToStr(edx);
            }

            if (_ols.Cpuid(0x80000003, ref eax, ref ebx, ref ecx, ref edx) == 1)
            {
                model = model + IntToStr(eax) + IntToStr(ebx) + IntToStr(ecx) + IntToStr(edx);
            }

            if (_ols.Cpuid(0x80000004, ref eax, ref ebx, ref ecx, ref edx) == 1)
            {
                model = model + IntToStr(eax) + IntToStr(ebx) + IntToStr(ecx) + IntToStr(edx);
            }

            return model.Trim();
        }
    }
}