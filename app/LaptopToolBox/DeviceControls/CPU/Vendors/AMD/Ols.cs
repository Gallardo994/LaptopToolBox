//-----------------------------------------------------------------------------
//     Author : hiyohiyo
//       Mail : hiyohiyo@crystalmark.info
//        Web : http://openlibsys.org/
//    License : The modified BSD license
//
//                     Copyright 2007-2009 OpenLibSys.org. All rights reserved.
//-----------------------------------------------------------------------------
// This is support library for WinRing0 1.3.x.

using System;
using System.Runtime.InteropServices;
using Vanara.PInvoke;

namespace LaptopToolBox.DeviceControls.CPU.Vendors.AMD
{
    public class Ols : IOls
    {
        private const string DllNameX64 = "WinRing0x64.dll";

        // for this support library
        public enum Status
        {
            NO_ERROR = 0,
            DLL_NOT_FOUND = 1,
            DLL_INCORRECT_VERSION = 2,
            DLL_INITIALIZE_ERROR = 3,
        }

        // for WinRing0
        public enum OlsDllStatus
        {
            OLS_DLL_NO_ERROR = 0,
            OLS_DLL_UNSUPPORTED_PLATFORM = 1,
            OLS_DLL_DRIVER_NOT_LOADED = 2,
            OLS_DLL_DRIVER_NOT_FOUND = 3,
            OLS_DLL_DRIVER_UNLOADED = 4,
            OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK = 5,
            OLS_DLL_UNKNOWN_ERROR = 9
        }

        // for WinRing0
        public enum OlsDriverType
        {
            OLS_DRIVER_TYPE_UNKNOWN = 0,
            OLS_DRIVER_TYPE_WIN_9X = 1,
            OLS_DRIVER_TYPE_WIN_NT = 2,
            OLS_DRIVER_TYPE_WIN_NT4 = 3,    // Obsolete
            OLS_DRIVER_TYPE_WIN_NT_X64 = 4,
            OLS_DRIVER_TYPE_WIN_NT_IA64 = 5
        }

        // for WinRing0
        public enum OlsErrorPci : uint
        {
            OLS_ERROR_PCI_BUS_NOT_EXIST = 0xE0000001,
            OLS_ERROR_PCI_NO_DEVICE = 0xE0000002,
            OLS_ERROR_PCI_WRITE_CONFIG = 0xE0000003,
            OLS_ERROR_PCI_READ_CONFIG = 0xE0000004
        }

        // Bus Number, Device Number and Function Number to PCI Device Address
        public uint PciBusDevFunc(uint bus, uint dev, uint func)
        {
            return ((bus & 0xFF) << 8) | ((dev & 0x1F) << 3) | (func & 7);
        }

        // PCI Device Address to Bus Number
        public uint PciGetBus(uint address)
        {
            return ((address >> 8) & 0xFF);
        }

        // PCI Device Address to Device Number
        public uint PciGetDev(uint address)
        {
            return ((address >> 3) & 0x1F);
        }

        // PCI Device Address to Function Number
        public uint PciGetFunc(uint address)
        {
            return (address & 7);
        }

        private Kernel32.SafeHINSTANCE module;
        private uint status = (uint)Status.NO_ERROR;

        public Ols()
        {
            module = Kernel32.LoadLibrary(DllNameX64);
            if (module == IntPtr.Zero)
            {
                status = (uint)Status.DLL_NOT_FOUND;
            }
            else
            {

                GetDllStatusInternal = (_GetDllStatus)GetDelegate("GetDllStatus", typeof(_GetDllStatus));
                GetDllVersionInternal = (_GetDllVersion)GetDelegate("GetDllVersion", typeof(_GetDllVersion));
                GetDriverVersionInternal = (_GetDriverVersion)GetDelegate("GetDriverVersion", typeof(_GetDriverVersion));
                GetDriverTypeInternal = (_GetDriverType)GetDelegate("GetDriverType", typeof(_GetDriverType));

                InitializeOlsInternal = (_InitializeOls)GetDelegate("InitializeOls", typeof(_InitializeOls));
                DeinitializeOlsInternal = (_DeinitializeOls)GetDelegate("DeinitializeOls", typeof(_DeinitializeOls));

                IsCpuidInternal = (_IsCpuid)GetDelegate("IsCpuid", typeof(_IsCpuid));
                IsMsrInternal = (_IsMsr)GetDelegate("IsMsr", typeof(_IsMsr));
                IsTscInternal = (_IsTsc)GetDelegate("IsTsc", typeof(_IsTsc));
                HltInternal = (_Hlt)GetDelegate("Hlt", typeof(_Hlt));
                HltTxInternal = (_HltTx)GetDelegate("HltTx", typeof(_HltTx));
                HltPxInternal = (_HltPx)GetDelegate("HltPx", typeof(_HltPx));
                RdmsrInternal = (_Rdmsr)GetDelegate("Rdmsr", typeof(_Rdmsr));
                RdmsrTxInternal = (_RdmsrTx)GetDelegate("RdmsrTx", typeof(_RdmsrTx));
                RdmsrPxInternal = (_RdmsrPx)GetDelegate("RdmsrPx", typeof(_RdmsrPx));
                WrmsrInternal = (_Wrmsr)GetDelegate("Wrmsr", typeof(_Wrmsr));
                WrmsrTxInternal = (_WrmsrTx)GetDelegate("WrmsrTx", typeof(_WrmsrTx));
                WrmsrPxInternal = (_WrmsrPx)GetDelegate("WrmsrPx", typeof(_WrmsrPx));
                RdpmcInternal = (_Rdpmc)GetDelegate("Rdpmc", typeof(_Rdpmc));
                RdpmcTxInternal = (_RdpmcTx)GetDelegate("RdpmcTx", typeof(_RdpmcTx));
                RdpmcPxInternal = (_RdpmcPx)GetDelegate("RdpmcPx", typeof(_RdpmcPx));
                CpuidInternal = (_Cpuid)GetDelegate("Cpuid", typeof(_Cpuid));
                CpuidTxInternal = (_CpuidTx)GetDelegate("CpuidTx", typeof(_CpuidTx));
                CpuidPxInternal = (_CpuidPx)GetDelegate("CpuidPx", typeof(_CpuidPx));
                RdtscInternal = (_Rdtsc)GetDelegate("Rdtsc", typeof(_Rdtsc));
                RdtscTxInternal = (_RdtscTx)GetDelegate("RdtscTx", typeof(_RdtscTx));
                RdtscPxInternal = (_RdtscPx)GetDelegate("RdtscPx", typeof(_RdtscPx));

                ReadIoPortByteInternal = (_ReadIoPortByte)GetDelegate("ReadIoPortByte", typeof(_ReadIoPortByte));
                ReadIoPortWordInternal = (_ReadIoPortWord)GetDelegate("ReadIoPortWord", typeof(_ReadIoPortWord));
                ReadIoPortDwordInternal = (_ReadIoPortDword)GetDelegate("ReadIoPortDword", typeof(_ReadIoPortDword));
                ReadIoPortByteExInternal = (_ReadIoPortByteEx)GetDelegate("ReadIoPortByteEx", typeof(_ReadIoPortByteEx));
                ReadIoPortWordExInternal = (_ReadIoPortWordEx)GetDelegate("ReadIoPortWordEx", typeof(_ReadIoPortWordEx));
                ReadIoPortDwordExInternal = (_ReadIoPortDwordEx)GetDelegate("ReadIoPortDwordEx", typeof(_ReadIoPortDwordEx));

                WriteIoPortByteInternal = (_WriteIoPortByte)GetDelegate("WriteIoPortByte", typeof(_WriteIoPortByte));
                WriteIoPortWordInternal = (_WriteIoPortWord)GetDelegate("WriteIoPortWord", typeof(_WriteIoPortWord));
                WriteIoPortDwordInternal = (_WriteIoPortDword)GetDelegate("WriteIoPortDword", typeof(_WriteIoPortDword));
                WriteIoPortByteExInternal = (_WriteIoPortByteEx)GetDelegate("WriteIoPortByteEx", typeof(_WriteIoPortByteEx));
                WriteIoPortWordExInternal = (_WriteIoPortWordEx)GetDelegate("WriteIoPortWordEx", typeof(_WriteIoPortWordEx));
                WriteIoPortDwordExInternal = (_WriteIoPortDwordEx)GetDelegate("WriteIoPortDwordEx", typeof(_WriteIoPortDwordEx));

                SetPciMaxBusIndexInternal = (_SetPciMaxBusIndex)GetDelegate("SetPciMaxBusIndex", typeof(_SetPciMaxBusIndex));
                ReadPciConfigByteInternal = (_ReadPciConfigByte)GetDelegate("ReadPciConfigByte", typeof(_ReadPciConfigByte));
                ReadPciConfigWordInternal = (_ReadPciConfigWord)GetDelegate("ReadPciConfigWord", typeof(_ReadPciConfigWord));
                ReadPciConfigDwordInternal = (_ReadPciConfigDword)GetDelegate("ReadPciConfigDword", typeof(_ReadPciConfigDword));
                ReadPciConfigByteExInternal = (_ReadPciConfigByteEx)GetDelegate("ReadPciConfigByteEx", typeof(_ReadPciConfigByteEx));
                ReadPciConfigWordExInternal = (_ReadPciConfigWordEx)GetDelegate("ReadPciConfigWordEx", typeof(_ReadPciConfigWordEx));
                ReadPciConfigDwordExInternal = (_ReadPciConfigDwordEx)GetDelegate("ReadPciConfigDwordEx", typeof(_ReadPciConfigDwordEx));
                ReadPciConfigDwordEx64Internal = (_ReadPciConfigDwordEx64)GetDelegate("ReadPciConfigDwordEx", typeof(_ReadPciConfigDwordEx64));
                WritePciConfigByteInternal = (_WritePciConfigByte)GetDelegate("WritePciConfigByte", typeof(_WritePciConfigByte));
                WritePciConfigWordInternal = (_WritePciConfigWord)GetDelegate("WritePciConfigWord", typeof(_WritePciConfigWord));
                WritePciConfigDwordInternal = (_WritePciConfigDword)GetDelegate("WritePciConfigDword", typeof(_WritePciConfigDword));
                WritePciConfigByteExInternal = (_WritePciConfigByteEx)GetDelegate("WritePciConfigByteEx", typeof(_WritePciConfigByteEx));
                WritePciConfigWordExInternal = (_WritePciConfigWordEx)GetDelegate("WritePciConfigWordEx", typeof(_WritePciConfigWordEx));
                WritePciConfigDwordExInternal = (_WritePciConfigDwordEx)GetDelegate("WritePciConfigDwordEx", typeof(_WritePciConfigDwordEx));
                WritePciConfigDwordEx64Internal = (_WritePciConfigDwordEx64)GetDelegate("WritePciConfigDwordEx", typeof(_WritePciConfigDwordEx64));
                FindPciDeviceByIdInternal = (_FindPciDeviceById)GetDelegate("FindPciDeviceById", typeof(_FindPciDeviceById));
                FindPciDeviceByClassInternal = (_FindPciDeviceByClass)GetDelegate("FindPciDeviceByClass", typeof(_FindPciDeviceByClass));

                /*
                ReadDmiMemory = (_ReadDmiMemory)GetDelegate("ReadDmiMemory", typeof(_ReadDmiMemory));
                ReadPhysicalMemory = (_ReadPhysicalMemory)GetDelegate("ReadPhysicalMemory", typeof(_ReadPhysicalMemory));
                WritePhysicalMemory = (_WritePhysicalMemory)GetDelegate("WritePhysicalMemory", typeof(_WritePhysicalMemory));
				*/

                if (!(
                   GetDllStatusInternal != null
                && GetDllVersionInternal != null
                && GetDriverVersionInternal != null
                && GetDriverTypeInternal != null
                && InitializeOlsInternal != null
                && DeinitializeOlsInternal != null
                && IsCpuidInternal != null
                && IsMsrInternal != null
                && IsTscInternal != null
                && HltInternal != null
                && HltTxInternal != null
                && HltPxInternal != null
                && RdmsrInternal != null
                && RdmsrTxInternal != null
                && RdmsrPxInternal != null
                && WrmsrInternal != null
                && WrmsrTxInternal != null
                && WrmsrPxInternal != null
                && RdpmcInternal != null
                && RdpmcTxInternal != null
                && RdpmcPxInternal != null
                && CpuidInternal != null
                && CpuidTxInternal != null
                && CpuidPxInternal != null
                && RdtscInternal != null
                && RdtscTxInternal != null
                && RdtscPxInternal != null
                && ReadIoPortByteInternal != null
                && ReadIoPortWordInternal != null
                && ReadIoPortDwordInternal != null
                && ReadIoPortByteExInternal != null
                && ReadIoPortWordExInternal != null
                && ReadIoPortDwordExInternal != null
                && WriteIoPortByteInternal != null
                && WriteIoPortWordInternal != null
                && WriteIoPortDwordInternal != null
                && WriteIoPortByteExInternal != null
                && WriteIoPortWordExInternal != null
                && WriteIoPortDwordExInternal != null
                && SetPciMaxBusIndexInternal != null
                && ReadPciConfigByteInternal != null
                && ReadPciConfigWordInternal != null
                && ReadPciConfigDwordInternal != null
                && ReadPciConfigByteExInternal != null
                && ReadPciConfigWordExInternal != null
                && ReadPciConfigDwordExInternal != null
                && ReadPciConfigDwordEx64Internal != null
                && WritePciConfigByteInternal != null
                && WritePciConfigWordInternal != null
                && WritePciConfigDwordInternal != null
                && WritePciConfigByteExInternal != null
                && WritePciConfigWordExInternal != null
                && WritePciConfigDwordExInternal != null
                && WritePciConfigDwordEx64Internal != null
                && FindPciDeviceByIdInternal != null
                && FindPciDeviceByClassInternal != null
                /*&& ReadDmiMemory != null
                && ReadPhysicalMemory != null
                && WritePhysicalMemory != null
				*/
                ))
                {
                    status = (uint)Status.DLL_INCORRECT_VERSION;
                }

                if (InitializeOlsInternal() == 0)
                {
                    status = (uint)Status.DLL_INITIALIZE_ERROR;
                }
            }
        }

        public void Dispose()
        {
            if (module.IsInvalid)
            {
                return;
            }

            DeinitializeOlsInternal();
            Kernel32.FreeLibrary(module);
            module = null;
        }

        private Delegate GetDelegate(string procName, Type delegateType)
        {
            var ptr = Kernel32.GetProcAddress(module, procName);
            if (ptr != IntPtr.Zero)
            {
                var d = Marshal.GetDelegateForFunctionPointer(ptr, delegateType);
                return d;
            }

            var result = Marshal.GetHRForLastWin32Error();
            throw Marshal.GetExceptionForHR(result);
        }

        //-----------------------------------------------------------------------------
        // DLL Information
        //-----------------------------------------------------------------------------
        private delegate uint _GetDllStatus();

        private delegate uint _GetDllVersion(ref byte major, ref byte minor, ref byte revision, ref byte release);

        private delegate uint _GetDriverVersion(ref byte major, ref byte minor, ref byte revision, ref byte release);

        private delegate uint _GetDriverType();

        private delegate int _InitializeOls();

        private delegate void _DeinitializeOls();

        private _GetDllStatus GetDllStatusInternal;
        private _GetDriverType GetDriverTypeInternal;
        private _GetDllVersion GetDllVersionInternal;
        private _GetDriverVersion GetDriverVersionInternal;
        private _InitializeOls InitializeOlsInternal;
        private _DeinitializeOls DeinitializeOlsInternal;

        //-----------------------------------------------------------------------------
        // CPU
        //-----------------------------------------------------------------------------
        private delegate int _IsCpuid();
        private delegate int _IsMsr();
        private delegate int _IsTsc();
        private delegate int _Hlt();
        private delegate int _HltTx(UIntPtr threadAffinityMask);
        private delegate int _HltPx(UIntPtr processAffinityMask);
        private delegate int _Rdmsr(uint index, ref uint eax, ref uint edx);
        private delegate int _RdmsrTx(uint index, ref uint eax, ref uint edx, UIntPtr threadAffinityMask);
        private delegate int _RdmsrPx(uint index, ref uint eax, ref uint edx, UIntPtr processAffinityMask);
        private delegate int _Wrmsr(uint index, uint eax, uint edx);
        private delegate int _WrmsrTx(uint index, uint eax, uint edx, UIntPtr threadAffinityMask);
        private delegate int _WrmsrPx(uint index, uint eax, uint edx, UIntPtr processAffinityMask);
        private delegate int _Rdpmc(uint index, ref uint eax, ref uint edx);
        private delegate int _RdpmcTx(uint index, ref uint eax, ref uint edx, UIntPtr threadAffinityMask);
        private delegate int _RdpmcPx(uint index, ref uint eax, ref uint edx, UIntPtr processAffinityMask);
        private delegate int _Cpuid(uint index, ref uint eax, ref uint ebx, ref uint ecx, ref uint edx);

        private delegate int _CpuidTx(uint index, ref uint eax, ref uint ebx, ref uint ecx, ref uint edx, UIntPtr threadAffinityMask);
        private delegate int _CpuidPx(uint index, ref uint eax, ref uint ebx, ref uint ecx, ref uint edx, UIntPtr processAffinityMask);
        private delegate int _Rdtsc(ref uint eax, ref uint edx);
        private delegate int _RdtscTx(ref uint eax, ref uint edx, UIntPtr threadAffinityMask);
        private delegate int _RdtscPx(ref uint eax, ref uint edx, UIntPtr processAffinityMask);

        private _IsCpuid IsCpuidInternal = null;
        private _IsMsr IsMsrInternal = null;
        private _IsTsc IsTscInternal = null;
        private _Hlt HltInternal = null;
        private _HltTx HltTxInternal = null;
        private _HltPx HltPxInternal = null;
        private _Rdmsr RdmsrInternal = null;
        private _RdmsrTx RdmsrTxInternal = null;
        private _RdmsrPx RdmsrPxInternal = null;
        private _Wrmsr WrmsrInternal = null;
        private _WrmsrTx WrmsrTxInternal = null;
        private _WrmsrPx WrmsrPxInternal = null;
        private _Rdpmc RdpmcInternal = null;
        private _RdpmcTx RdpmcTxInternal = null;
        private _RdpmcPx RdpmcPxInternal = null;
        private _Cpuid CpuidInternal = null;
        private _CpuidTx CpuidTxInternal = null;
        private _CpuidPx CpuidPxInternal = null;
        private _Rdtsc RdtscInternal = null;
        private _RdtscTx RdtscTxInternal = null;
        private _RdtscPx RdtscPxInternal = null;

        //-----------------------------------------------------------------------------
        // I/O
        //-----------------------------------------------------------------------------
        private delegate byte _ReadIoPortByte(ushort port);
        private delegate ushort _ReadIoPortWord(ushort port);
        private delegate uint _ReadIoPortDword(ushort port);
        private _ReadIoPortByte ReadIoPortByteInternal;
        private _ReadIoPortWord ReadIoPortWordInternal;
        private _ReadIoPortDword ReadIoPortDwordInternal;

        private delegate int _ReadIoPortByteEx(ushort port, ref byte value);
        private delegate int _ReadIoPortWordEx(ushort port, ref ushort value);
        private delegate int _ReadIoPortDwordEx(ushort port, ref uint value);
        private _ReadIoPortByteEx ReadIoPortByteExInternal;
        private _ReadIoPortWordEx ReadIoPortWordExInternal;
        private _ReadIoPortDwordEx ReadIoPortDwordExInternal;

        private delegate void _WriteIoPortByte(ushort port, byte value);
        private delegate void _WriteIoPortWord(ushort port, ushort value);
        private delegate void _WriteIoPortDword(ushort port, uint value);
        private _WriteIoPortByte WriteIoPortByteInternal;
        private _WriteIoPortWord WriteIoPortWordInternal;
        private _WriteIoPortDword WriteIoPortDwordInternal;

        private delegate int _WriteIoPortByteEx(ushort port, byte value);
        private delegate int _WriteIoPortWordEx(ushort port, ushort value);
        private delegate int _WriteIoPortDwordEx(ushort port, uint value);
        private _WriteIoPortByteEx WriteIoPortByteExInternal;
        private _WriteIoPortWordEx WriteIoPortWordExInternal;
        private _WriteIoPortDwordEx WriteIoPortDwordExInternal;

        //-----------------------------------------------------------------------------
        // PCI
        //-----------------------------------------------------------------------------
        private delegate void _SetPciMaxBusIndex(byte max);
        private _SetPciMaxBusIndex SetPciMaxBusIndexInternal;

        private delegate byte _ReadPciConfigByte(uint pciAddress, byte regAddress);
        private delegate ushort _ReadPciConfigWord(uint pciAddress, byte regAddress);
        private delegate uint _ReadPciConfigDword(uint pciAddress, byte regAddress);
        private _ReadPciConfigByte ReadPciConfigByteInternal;
        private _ReadPciConfigWord ReadPciConfigWordInternal;
        private _ReadPciConfigDword ReadPciConfigDwordInternal;

        private delegate int _ReadPciConfigByteEx(uint pciAddress, uint regAddress, ref byte value);
        private delegate int _ReadPciConfigWordEx(uint pciAddress, uint regAddress, ref ushort value);
        public delegate int _ReadPciConfigDwordEx(uint pciAddress, uint regAddress, ref uint value);
        private _ReadPciConfigByteEx ReadPciConfigByteExInternal;
        private _ReadPciConfigWordEx ReadPciConfigWordExInternal;
        private _ReadPciConfigDwordEx ReadPciConfigDwordExInternal;
        
        private delegate int _ReadPciConfigDwordEx64(uint pciAddress, uint regAddress, ref ulong value);
        private _ReadPciConfigDwordEx64 ReadPciConfigDwordEx64Internal;
        
        private delegate void _WritePciConfigByte(uint pciAddress, byte regAddress, byte value);
        private delegate void _WritePciConfigWord(uint pciAddress, byte regAddress, ushort value);
        private delegate void _WritePciConfigDword(uint pciAddress, byte regAddress, uint value);
        private _WritePciConfigByte WritePciConfigByteInternal;
        private _WritePciConfigWord WritePciConfigWordInternal;
        private _WritePciConfigDword WritePciConfigDwordInternal;
        
        private delegate int _WritePciConfigByteEx(uint pciAddress, uint regAddress, byte value);
        private delegate int _WritePciConfigWordEx(uint pciAddress, uint regAddress, ushort value);
        private delegate int _WritePciConfigDwordEx(uint pciAddress, uint regAddress, uint value);
        private _WritePciConfigByteEx WritePciConfigByteExInternal;
        private _WritePciConfigWordEx WritePciConfigWordExInternal;
        private _WritePciConfigDwordEx WritePciConfigDwordExInternal;
        private delegate int _WritePciConfigDwordEx64(uint pciAddress, uint regAddress, ulong value);
        private _WritePciConfigDwordEx64 WritePciConfigDwordEx64Internal;
        
        private delegate uint _FindPciDeviceById(ushort vendorId, ushort deviceId, byte index);
        private delegate uint _FindPciDeviceByClass(byte baseClass, byte subClass, byte programIf, byte index);
        private _FindPciDeviceById FindPciDeviceByIdInternal;
        private _FindPciDeviceByClass FindPciDeviceByClassInternal;

        //-----------------------------------------------------------------------------
        // Physical Memory (unsafe)
        //-----------------------------------------------------------------------------

        /*public unsafe delegate uint _ReadDmiMemory(byte* buffer, uint count, uint unitSize);
        public _ReadDmiMemory ReadDmiMemory;

        public unsafe delegate uint _ReadPhysicalMemory(UIntPtr address, byte* buffer, uint count, uint unitSize);
        public unsafe delegate uint _WritePhysicalMemory(UIntPtr address, byte* buffer, uint count, uint unitSize);

        public _ReadPhysicalMemory ReadPhysicalMemory;
        public _WritePhysicalMemory WritePhysicalMemory;
		*/
        
        // Public methods
        public int InitializeOls()
        { 
            return InitializeOlsInternal();
        }

        public void DeinitializeOls()
        {
            DeinitializeOlsInternal();
        }
        
        public int Cpuid(uint index, ref uint eax, ref uint ebx, ref uint ecx, ref uint edx)
        {
            return CpuidInternal(index, ref eax, ref ebx, ref ecx, ref edx);
        }
        
        public uint GetDllStatus()
        {
            return GetDllStatusInternal();
        }
        
        public uint GetDriverType()
        {
            return GetDriverTypeInternal();
        }
        
        public uint GetDriverVersion(ref byte major, ref byte minor, ref byte revision, ref byte release)
        {
            return GetDriverVersionInternal(ref major, ref minor, ref revision, ref release);
        }
        
        public uint GetDllVersion(ref byte major, ref byte minor, ref byte revision, ref byte release)
        {
            return GetDllVersionInternal(ref major, ref minor, ref revision, ref release);
        }
        
        public uint ReadPciConfigDword(uint pciAddress, byte regAddress)
        {
            return ReadPciConfigDwordInternal(pciAddress, regAddress);
        }
        public int ReadPciConfigDwordEx(uint pciAddress, uint regAddress, ref uint value)
        {
            return ReadPciConfigDwordExInternal(pciAddress, regAddress, ref value);
        }
        
        public int ReadPciConfigDwordEx64(uint pciAddress, uint regAddress, ref ulong value)
        {
            return ReadPciConfigDwordEx64Internal(pciAddress, regAddress, ref value);
        }
        public void WritePciConfigDword(uint pciAddress, byte regAddress, uint value)
        {
            WritePciConfigDwordInternal(pciAddress, regAddress, value);
        }
        public int WritePciConfigDwordEx(uint pciAddress, uint regAddress, byte value)
        {
            return WritePciConfigDwordExInternal(pciAddress, regAddress, value);
        }
        
        public int WritePciConfigDwordEx(uint pciAddress, uint regAddress, uint value)
        {
            return WritePciConfigDwordExInternal(pciAddress, regAddress, value);
        }
        
        public int WritePciConfigDwordEx64(uint pciAddress, uint regAddress, uint value)
        {
            return WritePciConfigDwordEx64Internal(pciAddress, regAddress, value);
        }
        
        public int WritePciConfigDwordEx64(uint pciAddress, uint regAddress, ulong value)
        {
            return WritePciConfigDwordEx64Internal(pciAddress, regAddress, value);
        }
        
        public uint GetStatus()
        {
            return status;
        }
    }
}
