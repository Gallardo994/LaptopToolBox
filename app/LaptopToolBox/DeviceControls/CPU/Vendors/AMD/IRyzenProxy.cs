namespace LaptopToolBox.DeviceControls.CPU.Vendors.AMD;

public interface IRyzenProxy
{
    public void set_stapm_limit(uint value);
    public void set_stapm2_limit(uint value);
    public void set_fast_limit(uint value);
    public void set_slow_limit(uint value);
    public void set_slow_time(uint value);
    public void set_stapm_time(uint value);
    public void set_tctl_temp(uint value);
    public void set_cHTC_temp(uint value);
    public void set_apu_skin_temp_limit(uint value);
    public void set_vrm_current(uint value);
    public void set_vrmsoc_current(uint value);
    public void set_vrmgfx_current(uint value);
    public void set_vrmcvip_current(uint value);
    public void set_vrmmax_current(uint value);
    public void set_vrmgfxmax_current(uint value);
    public void set_vrmsocmax_current(uint value);
    public void set_max_gfxclk_freq(uint value);
    public void set_min_gfxclk_freq(uint value);
    public void set_max_socclk_freq(uint value);
    public void set_min_socclk_freq(uint value);
    public void set_max_fclk_freq(uint value);
    public void set_min_fclk_freq(uint value);
    public void set_max_vcn_freq(uint value);
    public void set_min_vcn_freq(uint value);
    public void set_max_lclk(uint value);
    public void set_min_lclk(uint value);
    public void set_prochot_deassertion_ramp(uint value);
    public void set_gfx_clk(uint value);
    public void set_dGPU_skin(uint value);
    public void set_power_saving(uint value);
    public void set_max_performance(uint value);
    public void set_oc_clk(uint value);
    public void set_per_core_oc_clk(uint value);
    public void set_oc_volt(uint value);
    public void set_coall(uint value);
    public void set_coper(uint value);
    public void set_cogfx(uint value);
    public void set_disable_oc();
    public void set_enable_oc();
    public void set_scaler(uint value);
    public void set_ppt(uint value);
    public void set_tdc(uint value);
    public void set_edc(uint value);
}