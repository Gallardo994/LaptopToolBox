﻿using CustomControls;
using GHelper.AnimeMatrix;
using GHelper.Gpu;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Timers;

namespace GHelper
{

    public partial class SettingsForm : RForm
    {

        private ContextMenuStrip contextMenuStrip = new CustomContextMenu();
        private ToolStripMenuItem menuSilent, menuBalanced, menuTurbo, menuEco, menuStandard, menuUltimate, menuOptimized;

        public ToastForm toast = new ToastForm();

        public static System.Timers.Timer aTimer = default!;
        public static Point trayPoint;

        public string versionUrl = "http://github.com/seerge/g-helper/releases";

        public string modeName = "Balanced";

        public AniMatrix matrix;
        public Fans fans;
        public Extra keyb;
        public Updates updates;

        static long lastRefresh;

        private bool customFans = false;
        private int customPower = 0;

        bool isGpuSection = true;

        public SettingsForm()
        {

            InitializeComponent();
            InitTheme(true);

            buttonSilent.Text = Properties.Strings.Silent;
            buttonBalanced.Text = Properties.Strings.Balanced;
            buttonTurbo.Text = Properties.Strings.Turbo;
            buttonFans.Text = Properties.Strings.FansPower;

            buttonEco.Text = Properties.Strings.EcoMode;
            buttonUltimate.Text = Properties.Strings.UltimateMode;
            buttonStandard.Text = Properties.Strings.StandardMode;
            buttonOptimized.Text = Properties.Strings.Optimized;


            buttonScreenAuto.Text = Properties.Strings.AutoMode;
            buttonMiniled.Text = Properties.Strings.Multizone;

            buttonKeyboardColor.Text = Properties.Strings.Color;
            buttonKeyboard.Text = Properties.Strings.Extra;

            labelPerf.Text = Properties.Strings.PerformanceMode;
            labelGPU.Text = Properties.Strings.GPUMode;
            labelSreen.Text = Properties.Strings.LaptopScreen;
            labelKeyboard.Text = Properties.Strings.LaptopKeyboard;
            labelMatrix.Text = Properties.Strings.AnimeMatrix;
            labelBatteryTitle.Text = Properties.Strings.BatteryChargeLimit;

            checkMatrix.Text = Properties.Strings.TurnOffOnBattery;
            checkStartup.Text = Properties.Strings.RunOnStartup;

            buttonMatrix.Text = Properties.Strings.PictureGif;
            buttonQuit.Text = Properties.Strings.Quit;
            buttonUpdates.Text = Properties.Strings.Updates;

            FormClosing += SettingsForm_FormClosing;

            buttonSilent.BorderColor = colorEco;
            buttonBalanced.BorderColor = colorStandard;
            buttonTurbo.BorderColor = colorTurbo;

            buttonEco.BorderColor = colorEco;
            buttonStandard.BorderColor = colorStandard;
            buttonUltimate.BorderColor = colorTurbo;
            buttonOptimized.BorderColor = colorEco;
            buttonXGM.BorderColor = colorTurbo;

            button60Hz.BorderColor = SystemColors.ActiveBorder;
            button120Hz.BorderColor = SystemColors.ActiveBorder;
            buttonScreenAuto.BorderColor = SystemColors.ActiveBorder;
            buttonMiniled.BorderColor = colorTurbo;

            buttonSilent.Click += ButtonSilent_Click;
            buttonBalanced.Click += ButtonBalanced_Click;
            buttonTurbo.Click += ButtonTurbo_Click;

            buttonEco.Click += ButtonEco_Click;
            buttonStandard.Click += ButtonStandard_Click;
            buttonUltimate.Click += ButtonUltimate_Click;
            buttonOptimized.Click += ButtonOptimized_Click;

            VisibleChanged += SettingsForm_VisibleChanged;

            button60Hz.Click += Button60Hz_Click;
            button120Hz.Click += Button120Hz_Click;
            buttonScreenAuto.Click += ButtonScreenAuto_Click;
            buttonMiniled.Click += ButtonMiniled_Click;

            buttonQuit.Click += ButtonQuit_Click;

            buttonKeyboardColor.Click += ButtonKeyboardColor_Click;

            buttonFans.Click += ButtonFans_Click;
            buttonKeyboard.Click += ButtonKeyboard_Click;

            pictureColor.Click += PictureColor_Click;
            pictureColor2.Click += PictureColor2_Click;

            labelCPUFan.Click += LabelCPUFan_Click;
            labelGPUFan.Click += LabelCPUFan_Click;

            comboMatrix.DropDownStyle = ComboBoxStyle.DropDownList;
            comboMatrixRunning.DropDownStyle = ComboBoxStyle.DropDownList;

            comboMatrix.DropDownClosed += ComboMatrix_SelectedValueChanged;
            comboMatrixRunning.DropDownClosed += ComboMatrixRunning_SelectedValueChanged;

            buttonMatrix.Click += ButtonMatrix_Click;

            checkStartup.CheckedChanged += CheckStartup_CheckedChanged;

            labelVersion.Click += LabelVersion_Click;
            labelVersion.ForeColor = Color.FromArgb(128, Color.Gray);

            buttonOptimized.MouseMove += ButtonOptimized_MouseHover;
            buttonOptimized.MouseLeave += ButtonGPU_MouseLeave;

            buttonEco.MouseMove += ButtonEco_MouseHover;
            buttonEco.MouseLeave += ButtonGPU_MouseLeave;

            buttonStandard.MouseMove += ButtonStandard_MouseHover;
            buttonStandard.MouseLeave += ButtonGPU_MouseLeave;

            buttonUltimate.MouseMove += ButtonUltimate_MouseHover;
            buttonUltimate.MouseLeave += ButtonGPU_MouseLeave;

            tableGPU.MouseMove += ButtonXGM_MouseMove;
            tableGPU.MouseLeave += ButtonGPU_MouseLeave;

            buttonXGM.Click += ButtonXGM_Click;

            buttonScreenAuto.MouseMove += ButtonScreenAuto_MouseHover;
            buttonScreenAuto.MouseLeave += ButtonScreen_MouseLeave;

            button60Hz.MouseMove += Button60Hz_MouseHover;
            button60Hz.MouseLeave += ButtonScreen_MouseLeave;

            button120Hz.MouseMove += Button120Hz_MouseHover;
            button120Hz.MouseLeave += ButtonScreen_MouseLeave;

            buttonUpdates.Click += ButtonUpdates_Click;

            sliderBattery.ValueChanged += SliderBattery_ValueChanged;
            Program.trayIcon.MouseMove += TrayIcon_MouseMove;

            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;

            SetVersionLabel(Properties.Strings.VersionLabel + ": " + Assembly.GetExecutingAssembly().GetName().Version);

            string model = AppConfig.GetModel();
            int trim = model.LastIndexOf("_");
            if (trim > 0) model = model.Substring(0, trim);

            labelModel.Text = model + (ProcessHelper.IsUserAdministrator() ? "." : "");

            TopMost = AppConfig.Is("topmost");

            SetContextMenu();

            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                CheckForUpdatesAsync();
            });

        }

        private void ButtonUpdates_Click(object? sender, EventArgs e)
        {
            if (updates == null || updates.Text == "")
            {
                updates = new Updates();
                updates.Show();
            }
            else
            {
                updates.Close();
            }
        }

        protected override void WndProc(ref Message m)
        {

            switch (m.Msg)
            {
                case NativeMethods.WM_POWERBROADCAST:
                    if (m.WParam == (IntPtr)NativeMethods.PBT_POWERSETTINGCHANGE)
                    {
                        var settings = (NativeMethods.POWERBROADCAST_SETTING)m.GetLParam(typeof(NativeMethods.POWERBROADCAST_SETTING));
                        switch (settings.Data)
                        {
                            case 0:
                                Logger.WriteLine("Monitor Power Off");
                                AsusUSB.ApplyBrightness(0);
                                break;
                            case 1:
                                Logger.WriteLine("Monitor Power On");
                                Program.SetAutoModes();
                                break;
                            case 2:
                                Logger.WriteLine("Monitor Dimmed");
                                break;
                        }
                    }
                    m.Result = (IntPtr)1;
                    break;
            }

            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void RunToast(string text, ToastIcon? icon = null)
        {
            toast.RunToast(text, icon);
        }

        public void SetContextMenu()
        {

            var mode = Modes.GetCurrent();

            contextMenuStrip.Items.Clear();
            Padding padding = new Padding(15, 5, 5, 5);

            var title = new ToolStripMenuItem(Properties.Strings.PerformanceMode);
            title.Margin = padding;
            title.Enabled = false;
            contextMenuStrip.Items.Add(title);

            menuSilent = new ToolStripMenuItem(Properties.Strings.Silent);
            menuSilent.Click += ButtonSilent_Click;
            menuSilent.Margin = padding;
            menuSilent.Checked = (mode == AsusACPI.PerformanceSilent);
            contextMenuStrip.Items.Add(menuSilent);

            menuBalanced = new ToolStripMenuItem(Properties.Strings.Balanced);
            menuBalanced.Click += ButtonBalanced_Click;
            menuBalanced.Margin = padding;
            menuBalanced.Checked = (mode == AsusACPI.PerformanceBalanced);
            contextMenuStrip.Items.Add(menuBalanced);

            menuTurbo = new ToolStripMenuItem(Properties.Strings.Turbo);
            menuTurbo.Click += ButtonTurbo_Click;
            menuTurbo.Margin = padding;
            menuTurbo.Checked = (mode == AsusACPI.PerformanceTurbo);
            contextMenuStrip.Items.Add(menuTurbo);

            contextMenuStrip.Items.Add("-");

            if (isGpuSection)
            {
                var titleGPU = new ToolStripMenuItem(Properties.Strings.GPUMode);
                titleGPU.Margin = padding;
                titleGPU.Enabled = false;
                contextMenuStrip.Items.Add(titleGPU);

                menuEco = new ToolStripMenuItem(Properties.Strings.EcoMode);
                menuEco.Click += ButtonEco_Click;
                menuEco.Margin = padding;
                contextMenuStrip.Items.Add(menuEco);

                menuStandard = new ToolStripMenuItem(Properties.Strings.StandardMode);
                menuStandard.Click += ButtonStandard_Click;
                menuStandard.Margin = padding;
                contextMenuStrip.Items.Add(menuStandard);

                menuUltimate = new ToolStripMenuItem(Properties.Strings.UltimateMode);
                menuUltimate.Click += ButtonUltimate_Click;
                menuUltimate.Margin = padding;
                contextMenuStrip.Items.Add(menuUltimate);

                menuOptimized = new ToolStripMenuItem(Properties.Strings.Optimized);
                menuOptimized.Click += ButtonOptimized_Click;
                menuOptimized.Margin = padding;
                contextMenuStrip.Items.Add(menuOptimized);

                contextMenuStrip.Items.Add("-");
            }


            var quit = new ToolStripMenuItem(Properties.Strings.Quit);
            quit.Click += ButtonQuit_Click;
            quit.Margin = padding;
            contextMenuStrip.Items.Add(quit);

            //contextMenuStrip.ShowCheckMargin = true;
            contextMenuStrip.RenderMode = ToolStripRenderMode.System;

            if (CheckSystemDarkModeStatus())
            {
                contextMenuStrip.BackColor = this.BackColor;
                contextMenuStrip.ForeColor = this.ForeColor;
            }

            Program.trayIcon.ContextMenuStrip = contextMenuStrip;


        }

        private void ButtonXGM_Click(object? sender, EventArgs e)
        {

            Task.Run(async () =>
            {
                BeginInvoke(delegate
                {
                    ButtonEnabled(buttonOptimized, false);
                    ButtonEnabled(buttonEco, false);
                    ButtonEnabled(buttonStandard, false);
                    ButtonEnabled(buttonUltimate, false);
                    ButtonEnabled(buttonXGM, false);
                });

                if (Program.acpi.DeviceGet(AsusACPI.GPUXG) == 1)
                {
                    HardwareControl.KillGPUApps();
                    DialogResult dialogResult = MessageBox.Show("Did you close all applications running on XG Mobile?", "Disabling XG Mobile", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Program.acpi.DeviceSet(AsusACPI.GPUXG, 0, "GPU XGM");
                        await Task.Delay(TimeSpan.FromSeconds(15));
                    }
                }
                else
                {
                    Program.acpi.DeviceSet(AsusACPI.GPUXG, 1, "GPU XGM");
                    AsusUSB.ApplyXGMLight(AppConfig.Is("xmg_light"));

                    await Task.Delay(TimeSpan.FromSeconds(15));

                    if (AppConfig.IsMode("auto_apply"))
                        AsusUSB.SetXGMFan(AppConfig.GetFanConfig(AsusFan.XGM));
                }

                BeginInvoke(delegate
                {
                    InitGPUMode();
                });


            });

        }

        private void SliderBattery_ValueChanged(object? sender, EventArgs e)
        {
            SetBatteryChargeLimit(sliderBattery.Value);
        }


        public async void CheckForUpdatesAsync()
        {

            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "C# App");
                    var json = await httpClient.GetStringAsync("https://api.github.com/repos/seerge/g-helper/releases/latest");
                    var config = JsonSerializer.Deserialize<JsonElement>(json);
                    var tag = config.GetProperty("tag_name").ToString().Replace("v", "");
                    var assets = config.GetProperty("assets");

                    string url = null;

                    for (int i = 0; i < assets.GetArrayLength(); i++)
                    {
                        if (assets[i].GetProperty("browser_download_url").ToString().Contains(".zip"))
                            url = assets[i].GetProperty("browser_download_url").ToString();
                    }

                    if (url is null)
                        url = assets[0].GetProperty("browser_download_url").ToString();

                    var gitVersion = new Version(tag);
                    var appVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                    //appVersion = new Version("0.50.0.0"); 

                    if (gitVersion.CompareTo(appVersion) > 0)
                    {
                        SetVersionLabel(Properties.Strings.DownloadUpdate + ": " + tag, url);
                        if (AppConfig.GetString("skip_version") != tag)
                        {
                            DialogResult dialogResult = MessageBox.Show(Properties.Strings.DownloadUpdate + ": G-Helper " + tag + "?", "Update", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                                AutoUpdate(url);
                            else
                                AppConfig.Set("skip_version", tag);
                        }

                    }
                    else
                    {
                        Debug.WriteLine("Latest version");
                    }

                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLine("Failed to check for updates:" + ex.Message);

            }

        }

        void SetVersionLabel(string label, string url = null)
        {
            BeginInvoke(delegate
            {
                labelVersion.Text = label;
                if (url is not null)
                {
                    this.versionUrl = url;
                    labelVersion.ForeColor = Color.Red;
                }
            });

        }


        public async void AutoUpdate(string requestUri)
        {

            Uri uri = new Uri(requestUri);
            string zipName = Path.GetFileName(uri.LocalPath);

            string exeLocation = Application.ExecutablePath;
            string exeDir = Path.GetDirectoryName(exeLocation);
            string zipLocation = exeDir + "\\" + zipName;

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(uri, zipLocation);

                Logger.WriteLine(requestUri);
                Logger.WriteLine(zipLocation);
                Logger.WriteLine(exeLocation);

                var cmd = new Process();
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.FileName = "powershell";
                cmd.StartInfo.Arguments = $"Start-Sleep -Seconds 1; Expand-Archive {zipLocation} -DestinationPath {exeDir} -Force; Remove-Item {zipLocation} -Force; {exeLocation}";
                cmd.Start();

                Application.Exit();
            }

        }


        private void LabelVersion_Click(object? sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo(versionUrl) { UseShellExecute = true });
        }

        private static void TrayIcon_MouseMove(object? sender, MouseEventArgs e)
        {
            Program.settingsForm.RefreshSensors();
        }


        private static void OnTimedEvent(Object? source, ElapsedEventArgs? e)
        {
            Program.settingsForm.RefreshSensors();
        }

        private void Button120Hz_MouseHover(object? sender, EventArgs e)
        {
            labelTipScreen.Text = Properties.Strings.MaxRefreshTooltip;
        }

        private void Button60Hz_MouseHover(object? sender, EventArgs e)
        {
            labelTipScreen.Text = Properties.Strings.MinRefreshTooltip;
        }

        private void ButtonScreen_MouseLeave(object? sender, EventArgs e)
        {
            labelTipScreen.Text = "";
        }

        private void ButtonScreenAuto_MouseHover(object? sender, EventArgs e)
        {
            labelTipScreen.Text = Properties.Strings.AutoRefreshTooltip;
        }

        private void ButtonUltimate_MouseHover(object? sender, EventArgs e)
        {
            labelTipGPU.Text = Properties.Strings.UltimateGPUTooltip;
        }

        private void ButtonStandard_MouseHover(object? sender, EventArgs e)
        {
            labelTipGPU.Text = Properties.Strings.StandardGPUTooltip;
        }

        private void ButtonEco_MouseHover(object? sender, EventArgs e)
        {
            labelTipGPU.Text = Properties.Strings.EcoGPUTooltip;
        }

        private void ButtonOptimized_MouseHover(object? sender, EventArgs e)
        {
            labelTipGPU.Text = Properties.Strings.OptimizedGPUTooltip;
        }

        private void ButtonGPU_MouseLeave(object? sender, EventArgs e)
        {
            labelTipGPU.Text = "";
        }

        private void ButtonXGM_MouseMove(object? sender, MouseEventArgs e)
        {
            if (sender is null) return;
            TableLayoutPanel table = (TableLayoutPanel)sender;

            if (!buttonXGM.Visible) return;

            labelTipGPU.Text = buttonXGM.Bounds.Contains(table.PointToClient(Cursor.Position)) ?
                "XGMobile toggle works only in Standard mode" : "";

        }


        private void ButtonOptimized_Click(object? sender, EventArgs e)
        {
            AppConfig.Set("gpu_auto", (AppConfig.Get("gpu_auto") == 1) ? 0 : 1);
            VisualiseGPUMode();
            AutoGPUMode();
        }

        private void ButtonScreenAuto_Click(object? sender, EventArgs e)
        {
            AppConfig.Set("screen_auto", 1);
            InitScreen();
            AutoScreen();
        }


        private void CheckStartup_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is null) return;
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                Startup.Schedule();
            }
            else
            {
                Startup.UnSchedule();
            }
        }

        private void CheckMatrix_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is null) return;
            CheckBox check = (CheckBox)sender;
            AppConfig.Set("matrix_auto", check.Checked ? 1 : 0);
            matrix?.SetMatrix();
        }



        private void ButtonMatrix_Click(object? sender, EventArgs e)
        {
            string fileName = null;

            Thread t = new Thread(() =>
            {
                OpenFileDialog of = new OpenFileDialog();
                of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png,*.gif)|*.BMP;*.JPG;*.JPEG;*.PNG;*.GIF";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    fileName = of.FileName;
                }
                return;
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            if (fileName is not null)
            {
                AppConfig.Set("matrix_picture", fileName);
                AppConfig.Set("matrix_running", 2);

                matrix?.SetMatrixPicture(fileName);
                BeginInvoke(delegate
                {
                    comboMatrixRunning.SelectedIndex = 2;
                });
            }


        }

        private void ComboMatrixRunning_SelectedValueChanged(object? sender, EventArgs e)
        {
            AppConfig.Set("matrix_running", comboMatrixRunning.SelectedIndex);
            matrix?.SetMatrix();
        }


        private void ComboMatrix_SelectedValueChanged(object? sender, EventArgs e)
        {
            AppConfig.Set("matrix_brightness", comboMatrix.SelectedIndex);
            matrix?.SetMatrix();
        }


        private void LabelCPUFan_Click(object? sender, EventArgs e)
        {
            AppConfig.Set("fan_rpm", (AppConfig.Get("fan_rpm") == 1) ? 0 : 1);
            RefreshSensors(true);
        }

        private void PictureColor2_Click(object? sender, EventArgs e)
        {

            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.Color = pictureColor2.BackColor;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                AppConfig.Set("aura_color2", colorDlg.Color.ToArgb());
                SetAura();
            }
        }

        private void PictureColor_Click(object? sender, EventArgs e)
        {
            buttonKeyboardColor.PerformClick();
        }

        private void ButtonKeyboard_Click(object? sender, EventArgs e)
        {
            if (keyb == null || keyb.Text == "")
            {
                keyb = new Extra();
                keyb.Show();
            }
            else
            {
                keyb.Close();
            }
        }

        public void FansToggle()
        {
            if (fans == null || fans.Text == "")
            {
                fans = new Fans();
            }

            if (fans.Visible)
            {
                fans.Close();
            }
            else
            {
                fans.FormPosition();
                fans.Show();
            }

        }

        private void ButtonFans_Click(object? sender, EventArgs e)
        {
            FansToggle();
        }

        private void ButtonKeyboardColor_Click(object? sender, EventArgs e)
        {

            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.Color = pictureColor.BackColor;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                AppConfig.Set("aura_color", colorDlg.Color.ToArgb());
                SetAura();
            }
        }

        public void InitAura()
        {
            AsusUSB.Mode = AppConfig.Get("aura_mode");
            AsusUSB.Speed = AppConfig.Get("aura_speed");
            AsusUSB.SetColor(AppConfig.Get("aura_color"));
            AsusUSB.SetColor2(AppConfig.Get("aura_color2"));

            comboKeyboard.DropDownStyle = ComboBoxStyle.DropDownList;
            comboKeyboard.DataSource = new BindingSource(AsusUSB.GetModes(), null);
            comboKeyboard.DisplayMember = "Value";
            comboKeyboard.ValueMember = "Key";
            comboKeyboard.SelectedValue = AsusUSB.Mode;
            comboKeyboard.SelectedValueChanged += ComboKeyboard_SelectedValueChanged;

            pictureColor.BackColor = AsusUSB.Color1;
            pictureColor2.BackColor = AsusUSB.Color2;
            pictureColor2.Visible = AsusUSB.HasSecondColor();

            if (AppConfig.ContainsModel("GA401"))
            {
                panelColor.Visible = false;
            }

            if (AppConfig.ContainsModel("GA401I"))
            {
                comboKeyboard.Visible = false;
            }

        }

        public void InitMatrix()
        {

            matrix = new AniMatrix();

            if (!matrix.IsValid)
            {
                panelMatrix.Visible = false;
                return;
            }

            int brightness = AppConfig.Get("matrix_brightness");
            int running = AppConfig.Get("matrix_running");

            comboMatrix.SelectedIndex = (brightness != -1) ? Math.Min(brightness, comboMatrix.Items.Count - 1) : 0;
            comboMatrixRunning.SelectedIndex = (running != -1) ? Math.Min(running, comboMatrixRunning.Items.Count - 1) : 0;

            checkMatrix.Checked = (AppConfig.Get("matrix_auto") == 1);
            checkMatrix.CheckedChanged += CheckMatrix_CheckedChanged;

        }


        public void SetAura()
        {
            AsusUSB.Mode = AppConfig.Get("aura_mode");
            AsusUSB.Speed = AppConfig.Get("aura_speed");
            AsusUSB.SetColor(AppConfig.Get("aura_color"));
            AsusUSB.SetColor2(AppConfig.Get("aura_color2"));

            pictureColor.BackColor = AsusUSB.Color1;
            pictureColor2.BackColor = AsusUSB.Color2;
            pictureColor2.Visible = AsusUSB.HasSecondColor();

            AsusUSB.ApplyAura();

        }

        public void CycleAuraMode()
        {
            if (comboKeyboard.SelectedIndex < comboKeyboard.Items.Count - 1)
                comboKeyboard.SelectedIndex += 1;
            else
                comboKeyboard.SelectedIndex = 0;
        }

        private void ComboKeyboard_SelectedValueChanged(object? sender, EventArgs e)
        {
            AppConfig.Set("aura_mode", (int)comboKeyboard.SelectedValue);
            SetAura();
        }


        private void Button120Hz_Click(object? sender, EventArgs e)
        {
            AppConfig.Set("screen_auto", 0);
            SetScreen(1000, 1);
        }

        private void Button60Hz_Click(object? sender, EventArgs e)
        {
            AppConfig.Set("screen_auto", 0);
            SetScreen(60, 0);
        }

        public void ToogleMiniled()
        {
            int miniled = (AppConfig.Get("miniled") == 1) ? 0 : 1;
            AppConfig.Set("miniled", miniled);
            SetScreen(-1, -1, miniled);
        }

        private void ButtonMiniled_Click(object? sender, EventArgs e)
        {
            ToogleMiniled();
        }

        public void SetScreen(int frequency = -1, int overdrive = -1, int miniled = -1)
        {

            if (NativeMethods.GetRefreshRate() < 0) // Laptop screen not detected or has unknown refresh rate
            {
                InitScreen();
                return;
            }

            if (frequency >= 1000)
            {
                frequency = NativeMethods.GetRefreshRate(true);
            }

            if (frequency > 0)
            {
                NativeMethods.SetRefreshRate(frequency);
            }

            if (overdrive >= 0)
            {
                if (AppConfig.Get("no_overdrive") == 1) overdrive = 0;
                Program.acpi.DeviceSet(AsusACPI.ScreenOverdrive, overdrive, "ScreenOverdrive");

            }

            if (miniled >= 0)
            {
                Program.acpi.DeviceSet(AsusACPI.ScreenMiniled, miniled, "Miniled");
                Debug.WriteLine("Miniled " + miniled);
            }

            InitScreen();
        }

        public void InitScreen()
        {

            int frequency = NativeMethods.GetRefreshRate();
            int maxFrequency = NativeMethods.GetRefreshRate(true);

            bool screenAuto = (AppConfig.Get("screen_auto") == 1);
            bool overdriveSetting = (AppConfig.Get("no_overdrive") != 1);

            int overdrive = Program.acpi.DeviceGet(AsusACPI.ScreenOverdrive);
            int miniled = Program.acpi.DeviceGet(AsusACPI.ScreenMiniled);

            bool screenEnabled = (frequency >= 0);

            // Default to 120Hz if unknown, usually happens if Mux is switched to dGPU-only.
            // It's been observed that dGPU screen is named same as internal screen, but with EXTERNAL at the end. Might as well check for that?
            var displayFrequency = maxFrequency > 0 ? maxFrequency : 120; 
            button120Hz.Text = $"{displayFrequency} Hz + OD";
            
            ButtonEnabled(button60Hz, screenEnabled);
            ButtonEnabled(button120Hz, screenEnabled);
            ButtonEnabled(buttonScreenAuto, screenEnabled);
            ButtonEnabled(buttonMiniled, screenEnabled);

            labelSreen.Text = screenEnabled
                ? Properties.Strings.LaptopScreen + ": " + frequency + "Hz" + ((overdrive == 1) ? " + " + Properties.Strings.Overdrive : "")
                : Properties.Strings.LaptopScreen + ": " + Properties.Strings.TurnedOff;

            button60Hz.Activated = false;
            button120Hz.Activated = false;
            buttonScreenAuto.Activated = false;

            if (screenAuto)
            {
                buttonScreenAuto.Activated = true;
            }
            else if (frequency == 60)
            {
                button60Hz.Activated = true;
            }
            else if (frequency > 60)
            {
                button120Hz.Activated = true;
            }

            if (maxFrequency > 60)
            {
                button120Hz.Text = maxFrequency.ToString() + "Hz" + (overdriveSetting ? " + OD" : "");
                panelScreen.Visible = true;
            }
            else if (maxFrequency > 0)
            {
                panelScreen.Visible = false;
            }

            if (miniled >= 0)
            {
                buttonMiniled.Activated = (miniled == 1);
                AppConfig.Set("miniled", miniled);
            }
            else
            {
                buttonMiniled.Visible = false;
            }

            AppConfig.Set("frequency", frequency);
            AppConfig.Set("overdrive", overdrive);
        }

        private void ButtonQuit_Click(object? sender, EventArgs e)
        {
            matrix.Dispose();
            Close();
            Program.trayIcon.Visible = false;
            Application.Exit();
        }

        public void HideAll()
        {
            this.Hide();
            if (fans != null && fans.Text != "") fans.Close();
            if (keyb != null && keyb.Text != "") keyb.Close();
        }

        public void CloseOthers()
        {
        }

        private void SettingsForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideAll();
            }
        }

        private void ButtonUltimate_Click(object? sender, EventArgs e)
        {
            SetGPUMode(AsusACPI.GPUModeUltimate);
        }

        private void ButtonStandard_Click(object? sender, EventArgs e)
        {
            SetGPUMode(AsusACPI.GPUModeStandard);
        }

        private void ButtonEco_Click(object? sender, EventArgs e)
        {
            SetGPUMode(AsusACPI.GPUModeEco);
        }

        public async void RefreshSensors(bool force = false)
        {

            if (!force && Math.Abs(DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastRefresh) < 2000) return;
            lastRefresh = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            string cpuTemp = "";
            string gpuTemp = "";
            string battery = "";

            HardwareControl.ReadSensors();

            if (HardwareControl.cpuTemp > 0)
                cpuTemp = ": " + Math.Round((decimal)HardwareControl.cpuTemp).ToString() + "°C";

            if (HardwareControl.batteryDischarge > 0)
                battery = Properties.Strings.Discharging + ": " + Math.Round((decimal)HardwareControl.batteryDischarge, 1).ToString() + "W";

            if (HardwareControl.gpuTemp > 0)
            {
                gpuTemp = $": {HardwareControl.gpuTemp}°C";
            }


            Program.settingsForm.BeginInvoke(delegate
            {
                labelCPUFan.Text = "CPU" + cpuTemp + " " + HardwareControl.cpuFan;
                labelGPUFan.Text = "GPU" + gpuTemp + " " + HardwareControl.gpuFan;
                if (HardwareControl.midFan is not null)
                    labelMidFan.Text = "Mid" + HardwareControl.midFan;

                labelBattery.Text = battery;
            });

            string trayTip = "CPU" + cpuTemp + " " + HardwareControl.cpuFan;
            if (gpuTemp.Length > 0) trayTip += "\nGPU" + gpuTemp + " " + HardwareControl.gpuFan;
            if (battery.Length > 0) trayTip += "\n" + battery;

            Program.trayIcon.Text = trayTip;

        }


        private void SettingsForm_VisibleChanged(object? sender, EventArgs e)
        {
            if (this.Visible)
            {
                InitScreen();
                InitXGM();

                this.Left = Screen.FromControl(this).WorkingArea.Width - 10 - this.Width;
                this.Top = Screen.FromControl(this).WorkingArea.Height - 10 - this.Height;
                this.Activate();

                aTimer.Enabled = true;

            }
            else
            {
                aTimer.Enabled = false;
            }
        }


        private void SetPerformanceLabel()
        {
            labelPerf.Text = Properties.Strings.PerformanceMode + ": " + Modes.GetCurrentName() + (customFans ? "+" : "") + ((customPower > 0) ? " " + customPower + "W" : "");
        }

        public void SetPower()
        {

            int limit_total = AppConfig.GetMode("limit_total");
            int limit_cpu = AppConfig.GetMode("limit_cpu");
            int limit_fast = AppConfig.GetMode("limit_fast");

            if (limit_total > AsusACPI.MaxTotal) return;
            if (limit_total < AsusACPI.MinTotal) return;

            if (limit_cpu > AsusACPI.MaxCPU) return;
            if (limit_cpu < AsusACPI.MinCPU) return;

            if (limit_fast > AsusACPI.MaxTotal) return;
            if (limit_fast < AsusACPI.MinTotal) return;

            // SPL + SPPT togeher in one slider
            if (Program.acpi.DeviceGet(AsusACPI.PPT_TotalA0) >= 0)
            {
                Program.acpi.DeviceSet(AsusACPI.PPT_TotalA0, limit_total, "PowerLimit A0");
                Program.acpi.DeviceSet(AsusACPI.PPT_APUA3, limit_total, "PowerLimit A3");
                customPower = limit_total;
            }

            if (Program.acpi.IsAllAmdPPT()) // CPU limit all amd models
            {
                Program.acpi.DeviceSet(AsusACPI.PPT_CPUB0, limit_cpu, "PowerLimit B0");
                customPower = limit_cpu;
            }
            else if (Program.acpi.DeviceGet(AsusACPI.PPT_APUC1) >= 0) // FPPT boost for non all-amd models
            {
                Program.acpi.DeviceSet(AsusACPI.PPT_APUC1, limit_fast, "PowerLimit C1");
                customPower = limit_fast;
            }


            Program.settingsForm.BeginInvoke(SetPerformanceLabel);

        }


        public void SetGPUClocks(bool launchAsAdmin = true)
        {

            int gpu_core = AppConfig.GetMode("gpu_core");
            int gpu_memory = AppConfig.GetMode("gpu_memory");

            if (gpu_core == -1 && gpu_memory == -1) return;

            //if ((gpu_core > -5 && gpu_core < 5) && (gpu_memory > -5 && gpu_memory < 5)) launchAsAdmin = false;

            if (Program.acpi.DeviceGet(AsusACPI.GPUEco) == 1) return;
            if (HardwareControl.GpuControl is null) return;
            if (!HardwareControl.GpuControl!.IsNvidia) return;

            using NvidiaGpuControl nvControl = (NvidiaGpuControl)HardwareControl.GpuControl;
            try
            {
                int getStatus = nvControl.GetClocks(out int current_core, out int current_memory);
                if (getStatus != -1)
                {
                    if (Math.Abs(gpu_core - current_core) < 5 && Math.Abs(gpu_memory - current_memory) < 5) return;
                }

                int setStatus = nvControl.SetClocks(gpu_core, gpu_memory);
                if (launchAsAdmin && setStatus == -1) ProcessHelper.RunAsAdmin("gpu");

            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.ToString());
            }
        }

        public void SetGPUPower()
        {

            int gpu_boost = AppConfig.GetMode("gpu_boost");
            int gpu_temp = AppConfig.GetMode("gpu_temp");


            if (gpu_boost < AsusACPI.MinGPUBoost || gpu_boost > AsusACPI.MaxGPUBoost) return;
            if (gpu_temp < AsusACPI.MinGPUTemp || gpu_temp > AsusACPI.MaxGPUTemp) return;

            if (Program.acpi.DeviceGet(AsusACPI.PPT_GPUC0) >= 0)
            {
                Program.acpi.DeviceSet(AsusACPI.PPT_GPUC0, gpu_boost, "PowerLimit C0");
            }

            if (Program.acpi.DeviceGet(AsusACPI.PPT_GPUC2) >= 0)
            {
                Program.acpi.DeviceSet(AsusACPI.PPT_GPUC2, gpu_temp, "PowerLimit C2");
            }

        }

        protected void LabelFansResult(string text)
        {
            if (fans != null && fans.Text != "")
                fans.LabelFansResult(text);
        }


        public void AutoFans(bool force = false)
        {
            customFans = false;

            if (AppConfig.IsMode("auto_apply") || force)
            {

                bool xgmFan = false;
                if (AppConfig.Is("xgm_fan") && Program.acpi.IsXGConnected())
                {
                    AsusUSB.SetXGMFan(AppConfig.GetFanConfig(AsusFan.XGM));
                    xgmFan = true;
                }

                int cpuResult = Program.acpi.SetFanCurve(AsusFan.CPU, AppConfig.GetFanConfig(AsusFan.CPU));
                int gpuResult = Program.acpi.SetFanCurve(AsusFan.GPU, AppConfig.GetFanConfig(AsusFan.GPU));


                if (AppConfig.Is("mid_fan"))
                    Program.acpi.SetFanCurve(AsusFan.Mid, AppConfig.GetFanConfig(AsusFan.Mid));


                // something went wrong, resetting to default profile
                if (cpuResult != 1 || gpuResult != 1)
                {
                    int mode = Modes.GetCurrentBase();
                    Logger.WriteLine("ASUS BIOS rejected fan curve, resetting mode to " + mode);
                    Program.acpi.DeviceSet(AsusACPI.PerformanceMode, mode, "Reset Mode");
                    LabelFansResult("ASUS BIOS rejected fan curve");
                }
                else
                {
                    LabelFansResult("");
                    customFans = true;
                }

                // force set PPTs for missbehaving bios on FX507/517 series
                if ((AppConfig.ContainsModel("FX507") || AppConfig.ContainsModel("FX517") || xgmFan) && !AppConfig.IsMode("auto_apply_power"))
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        Program.acpi.DeviceSet(AsusACPI.PPT_TotalA0, 80, "PowerLimit Fix A0");
                        Program.acpi.DeviceSet(AsusACPI.PPT_APUA3, 80, "PowerLimit Fix A3");
                    });
                }

            }

            Program.settingsForm.BeginInvoke(SetPerformanceLabel);

        }

        private static bool isManualModeRequired()
        {
            if (!AppConfig.IsMode("auto_apply_power"))
                return false;

            return
                AppConfig.Is("manual_mode") ||
                AppConfig.ContainsModel("GU604") ||
                AppConfig.ContainsModel("FX517") ||
                AppConfig.ContainsModel("G733");
        }

        public void AutoPower(int delay = 0)
        {

            customPower = 0;

            bool applyPower = AppConfig.IsMode("auto_apply_power");
            bool applyFans = AppConfig.IsMode("auto_apply");
            //bool applyGPU = true;

            if (applyPower)
            {
                // force fan curve for misbehaving bios PPTs on G513
                if (AppConfig.ContainsModel("G513") && !applyFans)
                {
                    delay = 500;
                    AutoFans(true);
                }

                // Fix for models that don't support PPT settings in all modes, setting a "manual" mode for them
                if (isManualModeRequired() && !applyFans)
                {
                    AutoFans(true);
                }
            }

            if (delay > 0)
            {
                var timer = new System.Timers.Timer(delay);
                timer.Elapsed += delegate
                {
                    timer.Stop();
                    timer.Dispose();
                    if (applyPower) SetPower();
                    SetGPUPower();
                };
                timer.Start();
            }
            else
            {
                if (applyPower) SetPower();
                SetGPUPower();
            }

        }


        public void SetPerformanceMode(int mode = -1, bool notify = false)
        {

            int oldMode = Modes.GetCurrent();
            if (mode < 0) mode = oldMode;

            buttonSilent.Activated = false;
            buttonBalanced.Activated = false;
            buttonTurbo.Activated = false;

            menuSilent.Checked = false;
            menuBalanced.Checked = false;
            menuTurbo.Checked = false;

            switch (mode)
            {
                case AsusACPI.PerformanceSilent:
                    buttonSilent.Activated = true;
                    menuSilent.Checked = true;
                    break;
                case AsusACPI.PerformanceTurbo:
                    buttonTurbo.Activated = true;
                    menuTurbo.Checked = true;
                    break;
                case AsusACPI.PerformanceBalanced:
                    buttonBalanced.Activated = true;
                    menuBalanced.Checked = true;
                    break;
                default:
                    if (!Modes.Exists(mode))
                    {
                        buttonBalanced.Activated = true;
                        menuBalanced.Checked = true;
                        mode = AsusACPI.PerformanceBalanced;
                    }
                    break;
            }


            Modes.SetCurrent(mode);

            SetPerformanceLabel();

            if (isManualModeRequired())
                Program.acpi.DeviceSet(AsusACPI.PerformanceMode, AsusACPI.PerformanceManual, "Manual Mode");
            else
                Program.acpi.DeviceSet(AsusACPI.PerformanceMode, Modes.GetBase(mode), "Mode");

            if (AppConfig.Is("xgm_fan") && Program.acpi.IsXGConnected()) AsusUSB.ResetXGM();

            if (notify)
            {
                try
                {
                    toast.RunToast(Modes.GetCurrentName(), SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online ? ToastIcon.Charger : ToastIcon.Battery);
                }
                catch
                {
                    Debug.WriteLine("Toast error");
                }
            }

            SetGPUClocks();

            AutoFans();
            AutoPower(1000);

            if (AppConfig.Get("auto_apply_power_plan") != 0)
            {
                if (AppConfig.GetModeString("scheme") is not null)
                    NativeMethods.SetPowerScheme(AppConfig.GetModeString("scheme"));
                else
                    NativeMethods.SetPowerScheme(Modes.GetBase(mode));
            }

            if (AppConfig.GetMode("auto_boost") != -1)
            {
                NativeMethods.SetCPUBoost(AppConfig.GetMode("auto_boost"));
            }

            if (NativeMethods.PowerGetEffectiveOverlayScheme(out Guid activeScheme) == 0)
            {
                Debug.WriteLine("Effective :" + activeScheme);
            }

            if (fans != null && fans.Text != "")
            {
                fans.InitMode();
                fans.InitFans();
                fans.InitPower();
                fans.InitBoost();
                fans.InitGPU();
            }
        }


        public void CyclePerformanceMode()
        {
            SetPerformanceMode(Modes.GetNext(Control.ModifierKeys == Keys.Shift), true);
        }


        public void AutoKeyboard()
        {
            InputDispatcher.SetBacklightAuto(true);

            if (Program.acpi.IsXGConnected()) 
                AsusUSB.ApplyXGMLight(AppConfig.Is("xmg_light"));

            if (AppConfig.ContainsModel("X16") || AppConfig.ContainsModel("X13")) InputDispatcher.TabletMode();

        }

        public void AutoPerformance(bool powerChanged = false)
        {
            var Plugged = SystemInformation.PowerStatus.PowerLineStatus;

            int mode = AppConfig.Get("performance_" + (int)Plugged);
            if (mode != -1)
                SetPerformanceMode(mode, powerChanged);
            else
                SetPerformanceMode(Modes.GetCurrent());
        }


        public void AutoScreen(bool force = false)
        {
            if (force || AppConfig.Is("screen_auto"))
            {
                if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online)
                    SetScreen(1000, 1);
                else
                    SetScreen(60, 0);
            }
            else
            {
                SetScreen(overdrive: AppConfig.Get("overdrive"));
            }



        }

        public static bool IsPlugged()
        {
            bool optimizedUSBC = AppConfig.Get("optimized_usbc") != 1;

            return SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online &&
                   (optimizedUSBC || Program.acpi.DeviceGet(AsusACPI.ChargerMode) < AsusACPI.ChargerUSB);

        }

        public bool AutoGPUMode()
        {

            bool GpuAuto = AppConfig.Is("gpu_auto");
            bool ForceGPU = AppConfig.ContainsModel("503");

            int GpuMode = AppConfig.Get("gpu_mode");

            if (!GpuAuto && !ForceGPU) return false;

            int eco = Program.acpi.DeviceGet(AsusACPI.GPUEco);
            int mux = Program.acpi.DeviceGet(AsusACPI.GPUMux);

            if (mux == 0) // GPU in Ultimate, ignore
                return false;
            else
            {

                if (ReEnableGPU()) return true;

                if (eco == 1)
                    if ((GpuAuto && IsPlugged()) || (ForceGPU && GpuMode == AsusACPI.GPUModeStandard))
                    {
                        SetGPUEco(0);
                        return true;
                    }
                if (eco == 0)
                    if ((GpuAuto && !IsPlugged()) || (ForceGPU && GpuMode == AsusACPI.GPUModeEco))
                    {

                        if (HardwareControl.IsUsedGPU())
                        {
                            DialogResult dialogResult = MessageBox.Show(Properties.Strings.AlertDGPU, Properties.Strings.AlertDGPUTitle, MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.No) return false;
                        }

                        SetGPUEco(1);
                        return true;
                    }
            }

            return false;

        }

        public bool ReEnableGPU()
        {

            if (AppConfig.Get("gpu_reenable") != 1) return false;
            if (Screen.AllScreens.Length <= 1) return false;

            Logger.WriteLine("Re-enabling gpu for 503 model");

            Thread.Sleep(1000);
            SetGPUEco(1);
            Thread.Sleep(1000);
            SetGPUEco(0);
            return true;
        }

        private void UltimateUI(bool ultimate)
        {
            if (!ultimate)
            {
                tableGPU.Controls.Remove(buttonUltimate);
                tablePerf.ColumnCount = 0;
                tableGPU.ColumnCount = 0;
                tableScreen.ColumnCount = 0;
                menuUltimate.Visible = false;

            }
            //tableLayoutMatrix.ColumnCount = 0;
        }

        public void InitXGM()
        {

            buttonXGM.Enabled = buttonXGM.Visible = Program.acpi.IsXGConnected();

            int activated = Program.acpi.DeviceGet(AsusACPI.GPUXG);
            if (activated < 0) return;

            buttonXGM.Activated = (activated == 1);

            if (buttonXGM.Activated)
            {
                ButtonEnabled(buttonOptimized, false);
                ButtonEnabled(buttonEco, false);
                ButtonEnabled(buttonStandard, false);
                ButtonEnabled(buttonUltimate, false);
            }
            else
            {
                ButtonEnabled(buttonOptimized, true);
                ButtonEnabled(buttonEco, true);
                ButtonEnabled(buttonStandard, true);
                ButtonEnabled(buttonUltimate, true);
            }

        }


        public int InitGPUMode()
        {

            int eco = Program.acpi.DeviceGet(AsusACPI.GPUEco);
            int mux = Program.acpi.DeviceGet(AsusACPI.GPUMux);

            Logger.WriteLine("Eco flag : " + eco);
            Logger.WriteLine("Mux flag : " + mux);

            int GpuMode;

            if (mux == 0)
                GpuMode = AsusACPI.GPUModeUltimate;
            else
            {
                if (eco == 1)
                    GpuMode = AsusACPI.GPUModeEco;
                else
                    GpuMode = AsusACPI.GPUModeStandard;

                UltimateUI(mux == 1);

                if (eco < 0 && mux < 0)
                {
                    isGpuSection = tableGPU.Visible = false;
                    SetContextMenu();
                    if (HardwareControl.FormatFan(Program.acpi.DeviceGet(AsusACPI.GPU_Fan)) is null) panelGPU.Visible = false;
                }

            }

            AppConfig.Set("gpu_mode", GpuMode);

            ButtonEnabled(buttonOptimized, true);
            ButtonEnabled(buttonEco, true);
            ButtonEnabled(buttonStandard, true);
            ButtonEnabled(buttonUltimate, true);

            InitXGM();

            VisualiseGPUMode(GpuMode);

            return GpuMode;

        }

        public void RestartGPU(bool confirm = true)
        {
            if (HardwareControl.GpuControl is null) return;
            if (!HardwareControl.GpuControl!.IsNvidia) return;

            if (confirm)
            {
                DialogResult dialogResult = MessageBox.Show(Properties.Strings.RestartGPU, Properties.Strings.EcoMode, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No) return;
            }

            ProcessHelper.RunAsAdmin("gpurestart");

            if (!ProcessHelper.IsUserAdministrator()) return;

            Logger.WriteLine("Trying to restart dGPU");

            Task.Run(async () =>
            {
                Program.settingsForm.BeginInvoke(delegate
                {
                    labelTipGPU.Text = "Restarting GPU ...";
                    ButtonEnabled(buttonOptimized, false);
                    ButtonEnabled(buttonEco, false);
                    ButtonEnabled(buttonStandard, false);
                    ButtonEnabled(buttonUltimate, false);
                });

                var nvControl = (NvidiaGpuControl)HardwareControl.GpuControl;
                bool status = nvControl.RestartGPU();

                Program.settingsForm.BeginInvoke(delegate
                {
                    labelTipGPU.Text = status ? "GPU Restarted, you can try Eco mode again" : "Failed to restart GPU";
                    InitGPUMode();
                });
            });

        }


        public void SetGPUEco(int eco, bool hardWay = false)
        {

            ButtonEnabled(buttonOptimized, false);
            ButtonEnabled(buttonEco, false);
            ButtonEnabled(buttonStandard, false);
            ButtonEnabled(buttonUltimate, false);
            ButtonEnabled(buttonXGM, false);

            labelGPU.Text = Properties.Strings.GPUMode + ": " + Properties.Strings.GPUChanging + " ...";

            Task.Run(async () =>
            {

                int status = 1;

                if (eco == 1) HardwareControl.KillGPUApps();

                Logger.WriteLine($"Running eco command {eco}");

                status = Program.acpi.SetGPUEco(eco);

                if (status == 0 && eco == 1 && hardWay) RestartGPU();

                await Task.Delay(TimeSpan.FromMilliseconds(100));
                Program.settingsForm.BeginInvoke(delegate
                {
                    InitGPUMode();
                    AutoScreen();
                });

                if (eco == 0)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(3000));
                    HardwareControl.RecreateGpuControl();
                    SetGPUClocks(false);
                }

            });


        }

        public void SetGPUMode(int GPUMode)
        {

            int CurrentGPU = AppConfig.Get("gpu_mode");
            AppConfig.Set("gpu_auto", 0);

            if (CurrentGPU == GPUMode)
            {
                VisualiseGPUMode();
                return;
            }

            var restart = false;
            var changed = false;

            if (CurrentGPU == AsusACPI.GPUModeUltimate)
            {
                DialogResult dialogResult = MessageBox.Show(Properties.Strings.AlertUltimateOff, Properties.Strings.AlertUltimateTitle, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Program.acpi.DeviceSet(AsusACPI.GPUMux, 1, "GPUMux");
                    restart = true;
                    changed = true;
                }
            }
            else if (GPUMode == AsusACPI.GPUModeUltimate)
            {
                DialogResult dialogResult = MessageBox.Show(Properties.Strings.AlertUltimateOn, Properties.Strings.AlertUltimateTitle, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Program.acpi.DeviceSet(AsusACPI.GPUMux, 0, "GPUMux");
                    restart = true;
                    changed = true;
                }

            }
            else if (GPUMode == AsusACPI.GPUModeEco)
            {
                VisualiseGPUMode(GPUMode);
                SetGPUEco(1, true);
                changed = true;
            }
            else if (GPUMode == AsusACPI.GPUModeStandard)
            {
                VisualiseGPUMode(GPUMode);
                SetGPUEco(0);
                changed = true;
            }

            if (changed)
            {
                AppConfig.Set("gpu_mode", GPUMode);
            }

            if (restart)
            {
                VisualiseGPUMode();
                Process.Start("shutdown", "/r /t 1");
            }

        }


        public void VisualiseGPUMode(int GPUMode = -1)
        {

            if (GPUMode == -1)
                GPUMode = AppConfig.Get("gpu_mode");

            bool GPUAuto = AppConfig.Is("gpu_auto");

            buttonEco.Activated = false;
            buttonStandard.Activated = false;
            buttonUltimate.Activated = false;
            buttonOptimized.Activated = false;

            switch (GPUMode)
            {
                case AsusACPI.GPUModeEco:
                    buttonOptimized.BorderColor = colorEco;
                    buttonEco.Activated = !GPUAuto;
                    buttonOptimized.Activated = GPUAuto;
                    labelGPU.Text = Properties.Strings.GPUMode + ": " + Properties.Strings.GPUModeEco;
                    Program.trayIcon.Icon = Properties.Resources.eco;
                    ButtonEnabled(buttonXGM, false);
                    break;
                case AsusACPI.GPUModeUltimate:
                    buttonUltimate.Activated = true;
                    labelGPU.Text = Properties.Strings.GPUMode + ": " + Properties.Strings.GPUModeUltimate;
                    Program.trayIcon.Icon = Properties.Resources.ultimate;
                    break;
                default:
                    buttonOptimized.BorderColor = colorStandard;
                    buttonStandard.Activated = !GPUAuto;
                    buttonOptimized.Activated = GPUAuto;
                    labelGPU.Text = Properties.Strings.GPUMode + ": " + Properties.Strings.GPUModeStandard;
                    Program.trayIcon.Icon = Properties.Resources.standard;
                    ButtonEnabled(buttonXGM, true);
                    break;
            }

            if (isGpuSection)
            {
                menuEco.Checked = buttonEco.Activated;
                menuStandard.Checked = buttonStandard.Activated;
                menuUltimate.Checked = buttonUltimate.Activated;
                menuOptimized.Checked = buttonOptimized.Activated;
            }

        }


        private void ButtonSilent_Click(object? sender, EventArgs e)
        {
            SetPerformanceMode(AsusACPI.PerformanceSilent);
        }

        private void ButtonBalanced_Click(object? sender, EventArgs e)
        {
            SetPerformanceMode(AsusACPI.PerformanceBalanced);
        }

        private void ButtonTurbo_Click(object? sender, EventArgs e)
        {
            SetPerformanceMode(AsusACPI.PerformanceTurbo);
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        public void ButtonEnabled(RButton but, bool enabled)
        {
            but.Enabled = enabled;
            but.BackColor = but.Enabled ? Color.FromArgb(255, but.BackColor) : Color.FromArgb(100, but.BackColor);
        }

        public void SetStartupCheck(bool status)
        {
            checkStartup.CheckedChanged -= CheckStartup_CheckedChanged;
            checkStartup.Checked = status;
            checkStartup.CheckedChanged += CheckStartup_CheckedChanged;
        }

        public void SetBatteryChargeLimit(int limit)
        {

            if (limit < 40 || limit > 100) return;

            //Debug.WriteLine(limit);

            labelBatteryTitle.Text = Properties.Strings.BatteryChargeLimit + ": " + limit.ToString() + "%";
            sliderBattery.Value = limit;

            Program.acpi.DeviceSet(AsusACPI.BatteryLimit, limit, "BatteryLimit");
            try
            {
                OptimizationService.SetChargeLimit(limit);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            AppConfig.Set("charge_limit", limit);

        }


    }


}
