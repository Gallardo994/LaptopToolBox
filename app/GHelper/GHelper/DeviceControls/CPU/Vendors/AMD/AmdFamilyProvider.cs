using System;
using System.Management;

namespace GHelper.DeviceControls.CPU.Vendors.AMD
{
    internal class AmdFamilyProvider : IAmdFamilyProvider
    {
        private readonly string[] _familyNames = { "Raven", "Picasso", "Dali", "Renoir/Lucienne", "Matisse", "Vangogh", "Vermeer", "Cezanne/Barcello", "Rembrandt", "Phoenix", "Raphael/Dragon Range" };
        public int FamilyId { get; init; }
        public string FamilyName { get; init; }
        private string CpuModel { get; init; }

        public AmdFamilyProvider()
        {
            FamilyName = "Unknown";
            
            var myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                CpuModel = obj["Caption"].ToString();
            }
            
            FamilyId = -1;

            if (CpuModel.Contains("Model " + Convert.ToString(1)) || CpuModel.Contains("Model " + Convert.ToString(8)))
            {
                FamilyId = -1; //Zen1/+ DT
            }

            if (CpuModel.Contains("Model " + Convert.ToString(17)))
            {
                FamilyId = 0; //RAVEN
            }

            if (CpuModel.Contains("Model " + Convert.ToString(24)))
            {
                FamilyId = 1; //PICASSO
            }

            if (CpuModel.Contains("Model " + Convert.ToString(32)))
            {
                FamilyId = 2; //DALI
            }

            if (CpuModel.Contains("Model " + Convert.ToString(33)))
            {
                FamilyId = 6; //VERMEER
            }

            if (CpuModel.Contains("Model " + Convert.ToString(96)) || CpuModel.Contains("Model " + Convert.ToString(104)))
            {
                FamilyId = 3; //RENOIR/LUCIENNE
            }

            if (CpuModel.Contains("Model " + Convert.ToString(144)))
            {
                FamilyId = 5; //VANGOGH
            }

            if (CpuModel.Contains("Model " + Convert.ToString(80)))
            {
                FamilyId = 7; //CEZANNE/BARCELO
            }

            if (CpuModel.Contains("Model " + Convert.ToString(64)) || CpuModel.Contains("Model " + Convert.ToString(68)))
            {
                FamilyId = 8; //REMBRANDT
            }

            if (CpuModel.Contains("Model " + Convert.ToString(116)))
            {
                FamilyId = 9; //PHEONIX 
            }

            if (CpuModel.Contains("Model " + Convert.ToString(97)))
            {
                FamilyId = 10; //RAPHAEL/DRAGON RANGE
            }

            if (CpuModel.Contains("Model " + Convert.ToString(160)))
            {
                FamilyId = 11; //MENDOCINO
            }
            
            if (FamilyId > 0 && FamilyId < _familyNames.Length)
            {
                FamilyName = _familyNames[FamilyId];
            }
        }
    }
}