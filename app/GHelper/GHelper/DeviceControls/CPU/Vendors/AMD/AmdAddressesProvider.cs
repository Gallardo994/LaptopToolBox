using Ninject;

namespace GHelper.DeviceControls.CPU.Vendors.AMD
{
    internal class AmdAddressesProvider : IAmdAddressesProvider
    {
        public uint SMU_PCI_ADDR { get; set; }
        public uint SMU_OFFSET_ADDR { get; set; }
        public uint SMU_OFFSET_DATA { get; set; }

        public uint MP1_ADDR_MSG { get; set; }
        public uint MP1_ADDR_RSP { get; set; }
        public uint MP1_ADDR_ARG { get; set; }

        public uint PSMU_ADDR_MSG { get; set; }
        public uint PSMU_ADDR_RSP { get; set; }
        public uint PSMU_ADDR_ARG { get; set; }

        [Inject]
        public AmdAddressesProvider(ICpuFamilyProvider cpuFamilyProvider)
        {
            SMU_PCI_ADDR = 0x00000000;
            SMU_OFFSET_ADDR = 0xB8;
            SMU_OFFSET_DATA = 0xBC;

            if (cpuFamilyProvider.FamilyId == -1)
            {
                MP1_ADDR_MSG = 0X3B10528;
                MP1_ADDR_RSP = 0X3B10564;
                MP1_ADDR_ARG = 0X3B10598;

                PSMU_ADDR_MSG = 0x3B1051C;
                PSMU_ADDR_RSP = 0X3B10568;
                PSMU_ADDR_ARG = 0X3B10590;
            }

            if (cpuFamilyProvider.FamilyId == 0 || cpuFamilyProvider.FamilyId == 1 || cpuFamilyProvider.FamilyId == 2 || cpuFamilyProvider.FamilyId == 3 || cpuFamilyProvider.FamilyId == 7)
            {
                MP1_ADDR_MSG = 0x3B10528;
                MP1_ADDR_RSP = 0x3B10564;
                MP1_ADDR_ARG = 0x3B10998;

                PSMU_ADDR_MSG = 0x3B10A20;
                PSMU_ADDR_RSP = 0x3B10A80;
                PSMU_ADDR_ARG = 0x3B10A88;
            }
            else if (cpuFamilyProvider.FamilyId == 5 || cpuFamilyProvider.FamilyId == 8 || cpuFamilyProvider.FamilyId == 9 || cpuFamilyProvider.FamilyId == 11)
            {
                MP1_ADDR_MSG = 0x3B10528;
                MP1_ADDR_RSP = 0x3B10578;
                MP1_ADDR_ARG = 0x3B10998;

                PSMU_ADDR_MSG = 0x3B10a20;
                PSMU_ADDR_RSP = 0x3B10a80;
                PSMU_ADDR_ARG = 0x3B10a88;
            }
            else if (cpuFamilyProvider.FamilyId == 4 || cpuFamilyProvider.FamilyId == 6)
            {
                MP1_ADDR_MSG = 0x3B10530;
                MP1_ADDR_RSP = 0x3B1057C;
                MP1_ADDR_ARG = 0x3B109C4;

                PSMU_ADDR_MSG = 0x3B10524;
                PSMU_ADDR_RSP = 0x3B10570;
                PSMU_ADDR_ARG = 0x3B10A40;
            }
            else if (cpuFamilyProvider.FamilyId == 10)
            {
                MP1_ADDR_MSG = 0x3010508;
                MP1_ADDR_RSP = 0x3010988;
                MP1_ADDR_ARG = 0x3010984;

                PSMU_ADDR_MSG = 0x3B10524;
                PSMU_ADDR_RSP = 0x3B10570;
                PSMU_ADDR_ARG = 0x3B10A40;
            }
            else
            {
                MP1_ADDR_MSG = 0;
                MP1_ADDR_RSP = 0;
                MP1_ADDR_ARG = 0;

                PSMU_ADDR_MSG = 0;
                PSMU_ADDR_RSP = 0;
                PSMU_ADDR_ARG = 0;
            }
        }
    }
}