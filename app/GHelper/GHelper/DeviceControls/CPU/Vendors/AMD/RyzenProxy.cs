using Ninject;

namespace GHelper.DeviceControls.CPU.Vendors.AMD
{
    internal class RyzenProxy : IRyzenProxy
    {
        private readonly IRyzenAccess _ryzenAccess;
        private readonly ICpuFamilyProvider _cpuFamilyProvider;

        [Inject]
        public RyzenProxy(IRyzenAccess ryzenAccess, ICpuFamilyProvider cpuFamilyProvider)
        {
            _ryzenAccess = ryzenAccess;
            _cpuFamilyProvider = cpuFamilyProvider;
        }

        //STAMP Limit
        public void set_stapm_limit(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x1a, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x14, ref args);
                    _ryzenAccess.SendPsmu(0x31, ref args);
                    break;
                default:
                    break;
            }
        }

        //STAMP2 Limit
        public void set_stapm2_limit(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendPsmu(0x31, ref args);
                    break;
                default:
                    break;
            }
        }

        //Fast Limit
        public void set_fast_limit(uint value)
        {
            var args = new uint[6];
            args[0] = value;
            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x1b, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x15, ref args);
                    break;
                default:
                    break;
            }
        }

        //Slow Limit
        public void set_slow_limit(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x1c, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x16, ref args);
                    break;
                default:
                    break;
            }
        }

        //Slow time
        public void set_slow_time(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x1d, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x17, ref args);
                    break;
                default:
                    break;
            }
        }

        //STAMP Time
        public void set_stapm_time(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x1e, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x18, ref args);
                    break;
                default:
                    break;
            }
        }

        //TCTL Temp Limit
        public void set_tctl_temp(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case -1:
                    _ryzenAccess.SendPsmu(0x68, ref args);
                    break;
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x1f, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x19, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendMp1(0x23, ref args);
                    _ryzenAccess.SendPsmu(0x56, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x59, ref args);
                    break;
                default:
                    break;
            }
        }

        //cHTC Temp Limit
        public void set_cHTC_temp(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendPsmu(0x56, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendPsmu(0x37, ref args);
                    break;
                default:
                    break;
            }
        }

        //Skin Temp limit
        public void set_apu_skin_temp_limit(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 5:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x33, ref args);
                    break;
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x38, ref args);
                    break;
                default:
                    break;
            }
        }

        //VRM Current
        public void set_vrm_current(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x20, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x1a, ref args);
                    break;
                default:
                    break;
            }
        }

        //VRM SoC Current
        public void set_vrmsoc_current(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x21, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x1b, ref args);
                    break;
                default:
                    break;
            }
        }

        //VRM GFX Current
        public void set_vrmgfx_current(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 5:
                    _ryzenAccess.SendMp1(0x1c, ref args);
                    break;
                default:
                    break;
            }
        }

        //VRM CVIP Current
        public void set_vrmcvip_current(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 5:
                    _ryzenAccess.SendMp1(0x1d, ref args);
                    break;
                default:
                    break;
            }
        }

        //VRM Max Current
        public void set_vrmmax_current(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x22, ref args);
                    break;
                case 5:
                    _ryzenAccess.SendMp1(0x1e, ref args);
                    break;
                case 3:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x1c, ref args);
                    break;
                default:
                    break;
            }
        }

        //VRM GFX Max Current
        public void set_vrmgfxmax_current(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 5:
                    _ryzenAccess.SendMp1(0x1f, ref args);
                    break;
                default:
                    break;
            }
        }

        //VRM SoC Max Current
        public void set_vrmsocmax_current(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x23, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x1d, ref args);
                    break;
                default:
                    break;
            }
        }

        //GFX Clock Max
        public void set_max_gfxclk_freq(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x46, ref args);
                    break;
                default:
                    break;
            }
        }

        //GFX Clock Min
        public void set_min_gfxclk_freq(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x47, ref args);
                    break;
                default:
                    break;
            }
        }

        //SoC Clock Max
        public void set_max_socclk_freq(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x48, ref args);
                    break;
                default:
                    break;
            }
        }

        //SoC Clock Min
        public void set_min_socclk_freq(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x49, ref args);
                    break;
                default:
                    break;
            }
        }

        //FCLK Clock Max
        public void set_max_fclk_freq(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x4a, ref args);
                    break;
                default:
                    break;
            }
        }

        //FCLK Clock Min
        public void set_min_fclk_freq(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x4b, ref args);
                    break;
                default:
                    break;
            }
        }

        //VCN Clock Max
        public void set_max_vcn_freq(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x4c, ref args);
                    break;
                default:
                    break;
            }
        }

        //VCN Clock Min
        public void set_min_vcn_freq(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x4d, ref args);
                    break;
                default:
                    break;
            }
        }

        //LCLK Clock Max
        public void set_max_lclk(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x4e, ref args);
                    break;
                default:
                    break;
            }
        }

        //LCLK Clock Min
        public void set_min_lclk(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x4f, ref args);
                    break;
            }
        }

        //Prochot Ramp
        public void set_prochot_deassertion_ramp(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x26, ref args);
                    break;
                case 5:
                    _ryzenAccess.SendMp1(0x22, ref args);
                    break;
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x20, ref args);
                    break;
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x1f, ref args);
                    break;
                default:
                    break;
            }
        }

        //GFX Clock
        public void set_gfx_clk(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 3:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendPsmu(0x89, ref args);
                    break;
                default:
                    break;
            }
        }

        //dGPU Skin Temp
        public void set_dGPU_skin(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x37, ref args);
                    break;
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x32, ref args);
                    break;
                default:
                    break;
            }
        }

        //Power Saving
        public void set_power_saving(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x19, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x12, ref args);
                    break;
                default:
                    break;
            }
        }

        //Max Performance
        public void set_max_performance(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendMp1(0x18, ref args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x11, ref args);
                    break;
                default:
                    break;
            }
        }

        //Set All Core OC
        public void set_oc_clk(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case -1:
                    _ryzenAccess.SendPsmu(0x6c, ref args);
                    break;
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendPsmu(0x7d, ref args);
                    break;
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x31, ref args);
                    _ryzenAccess.SendPsmu(0x19, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendMp1(0x26, ref args);
                    _ryzenAccess.SendPsmu(0x5c, ref args);
                    break;
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendPsmu(0x19, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x5F, ref args);
                    break;
                default:
                    break;
            }
        }

        //Set Per Core OC
        public void set_per_core_oc_clk(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case -1:
                    _ryzenAccess.SendPsmu(0x6d, ref args);
                    break;
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendPsmu(0x7E, ref args);
                    break;
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x32, ref args);
                    _ryzenAccess.SendPsmu(0x1a, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendMp1(0x27, ref args);
                    _ryzenAccess.SendPsmu(0x5d, ref args);
                    break;
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendPsmu(0x1a, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x60, ref args);
                    break;
                default:
                    break;
            }
        }

        //Set VID
        public void set_oc_volt(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case -1:
                    _ryzenAccess.SendPsmu(0x6e, ref args);
                    break;
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendPsmu(0x7f, ref args);
                    break;
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x33, ref args);
                    _ryzenAccess.SendPsmu(0x1b, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendMp1(0x28, ref args);
                    _ryzenAccess.SendPsmu(0x61, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x61, ref args);
                    break;
                default:
                    break;
            }
        }

        //Set All Core Curve Optimiser
        public void set_coall(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x55, ref args);
                    _ryzenAccess.SendPsmu(0xB1, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendMp1(0x36, ref args);
                    _ryzenAccess.SendPsmu(0xB, ref args);
                    break;
                case 5:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x4c, ref args);
                    _ryzenAccess.SendPsmu(0x5D, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x7, ref args);
                    break;
                default:
                    break;
            }
        }

        //Set Per Core Curve Optimiser
        public void set_coper(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x54, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendMp1(0x35, ref args);
                    break;
                case 5:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendMp1(0x4b, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x6, ref args);
                    break;
                default:
                    break;
            }
        }

        //Set iGPU Curve Optimiser
        public void set_cogfx(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x64, ref args);
                    _ryzenAccess.SendPsmu(0x57, ref args);
                    break;
                case 5:
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendPsmu(0xb7, ref args);
                    break;
                default:
                    break;
            }
        }

        //Disable OC
        public void set_disable_oc()
        {
            uint value = 0x0;

            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case -1:
                    _ryzenAccess.SendMp1(0x24, ref args);
                    break;
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendPsmu(0x6A, ref args);
                    break;
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x30, ref args);
                    _ryzenAccess.SendPsmu(0x1d, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendMp1(0x25, ref args);
                    _ryzenAccess.SendPsmu(0x5b, ref args);
                    break;
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendPsmu(0x18, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x5E, ref args);
                    break;
                default:
                    break;
            }
        }

        //Enable OC
        public void set_enable_oc()
        {
            uint value = 0x0;

            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case -1:
                    _ryzenAccess.SendMp1(0x23, ref args);
                    break;
                case 0:
                case 1:
                case 2:
                    _ryzenAccess.SendPsmu(0x69, ref args);
                    break;
                case 3:
                case 7:
                    _ryzenAccess.SendMp1(0x2f, ref args);
                    _ryzenAccess.SendPsmu(0x1d, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendMp1(0x24, ref args);
                    _ryzenAccess.SendPsmu(0x5a, ref args);
                    break;
                case 8:
                case 9:
                case 11:
                    _ryzenAccess.SendPsmu(0x17, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x5D, ref args);
                    break;
                default:
                    break;
            }
        }

        //Set PBO Scaler
        public void set_scaler(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case -1:
                    _ryzenAccess.SendPsmu(0x6a, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendPsmu(0x58, ref args);
                    _ryzenAccess.SendMp1(0x2F, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x5b, ref args);
                    break;
                default:
                    break;
            }
        }


        //Set PPT
        public void set_ppt(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case -1:
                    _ryzenAccess.SendPsmu(0x64, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendPsmu(0x53, ref args);
                    _ryzenAccess.SendMp1(0x3D, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x56, ref args);
                    break;
                default:
                    break;
            }
        }


        //Set TDC
        public void set_tdc(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case -1:
                    _ryzenAccess.SendPsmu(0x65, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendPsmu(0x54, ref args);
                    _ryzenAccess.SendMp1(0x3B, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x57, ref args);
                    break;
                default:
                    break;
            }
        }

        //Set EDC
        public void set_edc(uint value)
        {
            var args = new uint[6];
            args[0] = value;

            switch (_cpuFamilyProvider.FamilyId)
            {
                case -1:
                    _ryzenAccess.SendPsmu(0x66, ref args);
                    break;
                case 4:
                case 6:
                    _ryzenAccess.SendPsmu(0x55, ref args);
                    _ryzenAccess.SendMp1(0x3c, ref args);
                    break;
                case 10:
                    _ryzenAccess.SendPsmu(0x58, ref args);
                    break;
                default:
                    break;
            }
        }
    }
}