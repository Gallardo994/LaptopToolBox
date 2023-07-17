using System;

namespace LaptopToolBox.DeviceControls.CPU.Vendors.AMD;

public interface IRyzenAccess : IDisposable
{
    public RyzenAccessStatus SendMp1(uint message, ref uint[] arguments);
    public RyzenAccessStatus SendPsmu(uint message, ref uint[] arguments);
    public RyzenAccessStatus SendMp164(uint message, ref ulong[] arguments);
    public RyzenAccessStatus SendPsmu64(uint message, ref ulong[] arguments);
    public bool SendSmuCommand(uint SMU_ADDR_MSG, uint SMU_ADDR_RSP, uint SMU_ADDR_ARG, uint msg, ref uint[] args);

    public RyzenAccessStatus SendMsg(uint SMU_ADDR_MSG, uint SMU_ADDR_RSP, uint SMU_ADDR_ARG, uint msg, ref uint[] args);
    public RyzenAccessStatus SendMsg64(uint SMU_ADDR_MSG, uint SMU_ADDR_RSP, uint SMU_ADDR_ARG, uint msg, ref ulong[] args);
    public bool SmuWaitDone(uint SMU_ADDR_RSP);
    public string GetCpuName();
}