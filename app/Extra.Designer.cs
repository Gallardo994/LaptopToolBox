﻿using System.Drawing;
using System.Windows.Forms;
using CustomControls;
using GHelper.Properties;

namespace GHelper
{
    partial class Extra
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBindings = new GroupBox();
            tableKeys = new TableLayoutPanel();
            labelFNC = new Label();
            textM2 = new TextBox();
            textM1 = new TextBox();
            comboM1 = new RComboBox();
            labelM1 = new Label();
            comboM4 = new RComboBox();
            comboM3 = new RComboBox();
            textM4 = new TextBox();
            textM3 = new TextBox();
            labelM4 = new Label();
            labelM3 = new Label();
            labelM2 = new Label();
            comboM2 = new RComboBox();
            labelFNF4 = new Label();
            comboFNF4 = new RComboBox();
            textFNF4 = new TextBox();
            comboFNC = new RComboBox();
            textFNC = new TextBox();
            pictureHelp = new PictureBox();
            groupLight = new GroupBox();
            panelBacklightExtra = new Panel();
            numericBacklightPluggedTime = new NumericUpDown();
            labelBacklightTimeoutPlugged = new Label();
            numericBacklightTime = new NumericUpDown();
            labelBacklightTimeout = new Label();
            labelBrightness = new Label();
            trackBrightness = new TrackBar();
            labelSpeed = new Label();
            comboKeyboardSpeed = new RComboBox();
            panelXMG = new Panel();
            checkXMG = new CheckBox();
            tableBacklight = new TableLayoutPanel();
            labelBacklight = new Label();
            checkAwake = new CheckBox();
            checkBoot = new CheckBox();
            checkSleep = new CheckBox();
            checkShutdown = new CheckBox();
            labelBacklightLogo = new Label();
            checkAwakeLogo = new CheckBox();
            checkBootLogo = new CheckBox();
            checkSleepLogo = new CheckBox();
            checkShutdownLogo = new CheckBox();
            labelBacklightBar = new Label();
            checkAwakeBar = new CheckBox();
            checkBootBar = new CheckBox();
            checkSleepBar = new CheckBox();
            checkShutdownBar = new CheckBox();
            labelBacklightLid = new Label();
            checkAwakeLid = new CheckBox();
            checkBootLid = new CheckBox();
            checkSleepLid = new CheckBox();
            checkShutdownLid = new CheckBox();
            groupOther = new GroupBox();
            checkAutoApplyWindowsPowerMode = new CheckBox();
            checkTopmost = new CheckBox();
            checkNoOverdrive = new CheckBox();
            checkUSBC = new CheckBox();
            checkVariBright = new CheckBox();
            checkGpuApps = new CheckBox();
            checkFnLock = new CheckBox();
            panelServices = new Panel();
            labelServices = new Label();
            buttonServices = new RButton();
            groupBindings.SuspendLayout();
            tableKeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureHelp).BeginInit();
            groupLight.SuspendLayout();
            panelBacklightExtra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericBacklightPluggedTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericBacklightTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBrightness).BeginInit();
            panelXMG.SuspendLayout();
            tableBacklight.SuspendLayout();
            groupOther.SuspendLayout();
            panelServices.SuspendLayout();
            SuspendLayout();
            // 
            // groupBindings
            // 
            groupBindings.AutoSize = true;
            groupBindings.Controls.Add(tableKeys);
            groupBindings.Controls.Add(pictureHelp);
            groupBindings.Dock = DockStyle.Top;
            groupBindings.Location = new Point(9, 11);
            groupBindings.Margin = new Padding(4, 2, 4, 2);
            groupBindings.Name = "groupBindings";
            groupBindings.Padding = new Padding(4, 2, 50, 10);
            groupBindings.Size = new Size(966, 351);
            groupBindings.TabIndex = 0;
            groupBindings.TabStop = false;
            groupBindings.Text = "Key Bindings";
            // 
            // tableKeys
            // 
            tableKeys.ColumnCount = 3;
            tableKeys.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableKeys.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableKeys.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableKeys.Controls.Add(labelFNC, 0, 5);
            tableKeys.Controls.Add(textM2, 2, 1);
            tableKeys.Controls.Add(textM1, 2, 0);
            tableKeys.Controls.Add(comboM1, 1, 0);
            tableKeys.Controls.Add(labelM1, 0, 0);
            tableKeys.Controls.Add(comboM4, 1, 3);
            tableKeys.Controls.Add(comboM3, 1, 2);
            tableKeys.Controls.Add(textM4, 2, 3);
            tableKeys.Controls.Add(textM3, 2, 2);
            tableKeys.Controls.Add(labelM4, 0, 3);
            tableKeys.Controls.Add(labelM3, 0, 2);
            tableKeys.Controls.Add(labelM2, 0, 1);
            tableKeys.Controls.Add(comboM2, 1, 1);
            tableKeys.Controls.Add(labelFNF4, 0, 4);
            tableKeys.Controls.Add(comboFNF4, 1, 4);
            tableKeys.Controls.Add(textFNF4, 2, 4);
            tableKeys.Controls.Add(comboFNC, 1, 5);
            tableKeys.Controls.Add(textFNC, 2, 5);
            tableKeys.Dock = DockStyle.Top;
            tableKeys.Location = new Point(4, 34);
            tableKeys.Margin = new Padding(4, 2, 4, 2);
            tableKeys.Name = "tableKeys";
            tableKeys.Padding = new Padding(9, 11, 0, 11);
            tableKeys.RowCount = 6;
            tableKeys.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            tableKeys.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            tableKeys.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            tableKeys.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            tableKeys.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableKeys.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            tableKeys.Size = new Size(912, 307);
            tableKeys.TabIndex = 10;
            // 
            // labelFNC
            // 
            labelFNC.AutoSize = true;
            labelFNC.Location = new Point(13, 254);
            labelFNC.Margin = new Padding(4, 0, 4, 0);
            labelFNC.Name = "labelFNC";
            labelFNC.Size = new Size(80, 32);
            labelFNC.TabIndex = 15;
            labelFNC.Text = "FN+C:";
            // 
            // textM2
            // 
            textM2.Dock = DockStyle.Top;
            textM2.Location = new Point(554, 62);
            textM2.Margin = new Padding(4, 2, 4, 2);
            textM2.Name = "textM2";
            textM2.PlaceholderText = "action";
            textM2.Size = new Size(354, 39);
            textM2.TabIndex = 14;
            // 
            // textM1
            // 
            textM1.Dock = DockStyle.Top;
            textM1.Location = new Point(554, 13);
            textM1.Margin = new Padding(4, 2, 4, 2);
            textM1.Name = "textM1";
            textM1.PlaceholderText = "action";
            textM1.Size = new Size(354, 39);
            textM1.TabIndex = 13;
            // 
            // comboM1
            // 
            comboM1.BorderColor = Color.White;
            comboM1.ButtonColor = Color.FromArgb(255, 255, 255);
            comboM1.Dock = DockStyle.Top;
            comboM1.FormattingEnabled = true;
            comboM1.Items.AddRange(new object[] { Strings.Default, Strings.VolumeMute, Strings.PlayPause, Strings.PrintScreen, Strings.ToggleAura, Strings.Custom });
            comboM1.Location = new Point(193, 13);
            comboM1.Margin = new Padding(4, 2, 4, 2);
            comboM1.Name = "comboM1";
            comboM1.Size = new Size(353, 40);
            comboM1.TabIndex = 11;
            // 
            // labelM1
            // 
            labelM1.AutoSize = true;
            labelM1.Location = new Point(13, 11);
            labelM1.Margin = new Padding(4, 0, 4, 0);
            labelM1.Name = "labelM1";
            labelM1.Size = new Size(54, 32);
            labelM1.TabIndex = 9;
            labelM1.Text = "M1:";
            // 
            // comboM4
            // 
            comboM4.BorderColor = Color.White;
            comboM4.ButtonColor = Color.FromArgb(255, 255, 255);
            comboM4.Dock = DockStyle.Top;
            comboM4.FormattingEnabled = true;
            comboM4.Items.AddRange(new object[] { Strings.PerformanceMode, Strings.OpenGHelper, Strings.Custom });
            comboM4.Location = new Point(193, 160);
            comboM4.Margin = new Padding(4, 2, 4, 2);
            comboM4.Name = "comboM4";
            comboM4.Size = new Size(353, 40);
            comboM4.TabIndex = 3;
            // 
            // comboM3
            // 
            comboM3.BorderColor = Color.White;
            comboM3.ButtonColor = Color.FromArgb(255, 255, 255);
            comboM3.Dock = DockStyle.Top;
            comboM3.FormattingEnabled = true;
            comboM3.Items.AddRange(new object[] { Strings.Default, Strings.VolumeMute, Strings.PlayPause, Strings.PrintScreen, Strings.ToggleAura, Strings.Custom });
            comboM3.Location = new Point(193, 111);
            comboM3.Margin = new Padding(4, 2, 4, 2);
            comboM3.Name = "comboM3";
            comboM3.Size = new Size(353, 40);
            comboM3.TabIndex = 1;
            // 
            // textM4
            // 
            textM4.Dock = DockStyle.Top;
            textM4.Location = new Point(554, 160);
            textM4.Margin = new Padding(4, 2, 4, 2);
            textM4.Name = "textM4";
            textM4.PlaceholderText = "action";
            textM4.Size = new Size(354, 39);
            textM4.TabIndex = 5;
            // 
            // textM3
            // 
            textM3.Dock = DockStyle.Top;
            textM3.Location = new Point(554, 111);
            textM3.Margin = new Padding(4, 2, 4, 2);
            textM3.Name = "textM3";
            textM3.PlaceholderText = "action";
            textM3.Size = new Size(354, 39);
            textM3.TabIndex = 4;
            // 
            // labelM4
            // 
            labelM4.AutoSize = true;
            labelM4.Location = new Point(13, 158);
            labelM4.Margin = new Padding(4, 0, 4, 0);
            labelM4.Name = "labelM4";
            labelM4.Size = new Size(54, 32);
            labelM4.TabIndex = 2;
            labelM4.Text = "M4:";
            // 
            // labelM3
            // 
            labelM3.AutoSize = true;
            labelM3.Location = new Point(13, 109);
            labelM3.Margin = new Padding(4, 0, 4, 0);
            labelM3.Name = "labelM3";
            labelM3.Size = new Size(54, 32);
            labelM3.TabIndex = 0;
            labelM3.Text = "M3:";
            // 
            // labelM2
            // 
            labelM2.AutoSize = true;
            labelM2.Location = new Point(13, 60);
            labelM2.Margin = new Padding(4, 0, 4, 0);
            labelM2.Name = "labelM2";
            labelM2.Size = new Size(54, 32);
            labelM2.TabIndex = 10;
            labelM2.Text = "M2:";
            // 
            // comboM2
            // 
            comboM2.BorderColor = Color.White;
            comboM2.ButtonColor = Color.FromArgb(255, 255, 255);
            comboM2.Dock = DockStyle.Top;
            comboM2.FormattingEnabled = true;
            comboM2.Items.AddRange(new object[] { Strings.Default, Strings.VolumeMute, Strings.PlayPause, Strings.PrintScreen, Strings.ToggleAura, Strings.Custom });
            comboM2.Location = new Point(193, 62);
            comboM2.Margin = new Padding(4, 2, 4, 2);
            comboM2.Name = "comboM2";
            comboM2.Size = new Size(353, 40);
            comboM2.TabIndex = 12;
            // 
            // labelFNF4
            // 
            labelFNF4.AutoSize = true;
            labelFNF4.Location = new Point(13, 207);
            labelFNF4.Margin = new Padding(4, 0, 4, 0);
            labelFNF4.Name = "labelFNF4";
            labelFNF4.Size = new Size(90, 32);
            labelFNF4.TabIndex = 6;
            labelFNF4.Text = "FN+F4:";
            // 
            // comboFNF4
            // 
            comboFNF4.BorderColor = Color.White;
            comboFNF4.ButtonColor = Color.FromArgb(255, 255, 255);
            comboFNF4.Dock = DockStyle.Top;
            comboFNF4.FormattingEnabled = true;
            comboFNF4.Location = new Point(193, 209);
            comboFNF4.Margin = new Padding(4, 2, 4, 2);
            comboFNF4.Name = "comboFNF4";
            comboFNF4.Size = new Size(353, 40);
            comboFNF4.TabIndex = 7;
            // 
            // textFNF4
            // 
            textFNF4.Dock = DockStyle.Top;
            textFNF4.Location = new Point(554, 209);
            textFNF4.Margin = new Padding(4, 2, 4, 2);
            textFNF4.Name = "textFNF4";
            textFNF4.PlaceholderText = "action";
            textFNF4.Size = new Size(354, 39);
            textFNF4.TabIndex = 8;
            // 
            // comboFNC
            // 
            comboFNC.BorderColor = Color.White;
            comboFNC.ButtonColor = Color.FromArgb(255, 255, 255);
            comboFNC.Dock = DockStyle.Top;
            comboFNC.FormattingEnabled = true;
            comboFNC.Location = new Point(193, 256);
            comboFNC.Margin = new Padding(4, 2, 4, 2);
            comboFNC.Name = "comboFNC";
            comboFNC.Size = new Size(353, 40);
            comboFNC.TabIndex = 16;
            // 
            // textFNC
            // 
            textFNC.Dock = DockStyle.Top;
            textFNC.Location = new Point(554, 256);
            textFNC.Margin = new Padding(4, 2, 4, 2);
            textFNC.Name = "textFNC";
            textFNC.PlaceholderText = "action";
            textFNC.Size = new Size(354, 39);
            textFNC.TabIndex = 17;
            // 
            // pictureHelp
            // 
            pictureHelp.BackgroundImage = Resources.icons8_help_64;
            pictureHelp.BackgroundImageLayout = ImageLayout.Zoom;
            pictureHelp.Cursor = Cursors.Hand;
            pictureHelp.Location = new Point(921, 51);
            pictureHelp.Margin = new Padding(4, 2, 4, 2);
            pictureHelp.Name = "pictureHelp";
            pictureHelp.Size = new Size(32, 32);
            pictureHelp.TabIndex = 9;
            pictureHelp.TabStop = false;
            // 
            // groupLight
            // 
            groupLight.AutoSize = true;
            groupLight.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupLight.Controls.Add(panelBacklightExtra);
            groupLight.Controls.Add(panelXMG);
            groupLight.Controls.Add(tableBacklight);
            groupLight.Dock = DockStyle.Top;
            groupLight.Location = new Point(9, 362);
            groupLight.Margin = new Padding(4, 2, 4, 2);
            groupLight.Name = "groupLight";
            groupLight.Padding = new Padding(4, 2, 4, 11);
            groupLight.Size = new Size(966, 526);
            groupLight.TabIndex = 1;
            groupLight.TabStop = false;
            groupLight.Text = "Keyboard Backlight";
            // 
            // panelBacklightExtra
            // 
            panelBacklightExtra.AutoSize = true;
            panelBacklightExtra.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelBacklightExtra.Controls.Add(numericBacklightPluggedTime);
            panelBacklightExtra.Controls.Add(labelBacklightTimeoutPlugged);
            panelBacklightExtra.Controls.Add(numericBacklightTime);
            panelBacklightExtra.Controls.Add(labelBacklightTimeout);
            panelBacklightExtra.Controls.Add(labelBrightness);
            panelBacklightExtra.Controls.Add(trackBrightness);
            panelBacklightExtra.Controls.Add(labelSpeed);
            panelBacklightExtra.Controls.Add(comboKeyboardSpeed);
            panelBacklightExtra.Dock = DockStyle.Top;
            panelBacklightExtra.Location = new Point(4, 299);
            panelBacklightExtra.Margin = new Padding(4, 2, 4, 2);
            panelBacklightExtra.Name = "panelBacklightExtra";
            panelBacklightExtra.Size = new Size(958, 216);
            panelBacklightExtra.TabIndex = 43;
            // 
            // numericBacklightPluggedTime
            // 
            numericBacklightPluggedTime.Location = new Point(702, 169);
            numericBacklightPluggedTime.Margin = new Padding(4, 2, 4, 2);
            numericBacklightPluggedTime.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numericBacklightPluggedTime.Name = "numericBacklightPluggedTime";
            numericBacklightPluggedTime.Size = new Size(193, 39);
            numericBacklightPluggedTime.TabIndex = 49;
            // 
            // labelBacklightTimeoutPlugged
            // 
            labelBacklightTimeoutPlugged.Location = new Point(13, 171);
            labelBacklightTimeoutPlugged.Margin = new Padding(4, 0, 4, 0);
            labelBacklightTimeoutPlugged.Name = "labelBacklightTimeoutPlugged";
            labelBacklightTimeoutPlugged.Size = new Size(683, 45);
            labelBacklightTimeoutPlugged.TabIndex = 48;
            labelBacklightTimeoutPlugged.Text = "Seconds to turn off backlight when plugged";
            // 
            // numericBacklightTime
            // 
            numericBacklightTime.Location = new Point(702, 120);
            numericBacklightTime.Margin = new Padding(4, 2, 4, 2);
            numericBacklightTime.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numericBacklightTime.Name = "numericBacklightTime";
            numericBacklightTime.Size = new Size(193, 39);
            numericBacklightTime.TabIndex = 47;
            // 
            // labelBacklightTimeout
            // 
            labelBacklightTimeout.Location = new Point(13, 122);
            labelBacklightTimeout.Margin = new Padding(4, 0, 4, 0);
            labelBacklightTimeout.Name = "labelBacklightTimeout";
            labelBacklightTimeout.Size = new Size(683, 45);
            labelBacklightTimeout.TabIndex = 46;
            labelBacklightTimeout.Text = "Seconds to turn off backlight on battery";
            // 
            // labelBrightness
            // 
            labelBrightness.Location = new Point(13, 75);
            labelBrightness.Margin = new Padding(4, 0, 4, 0);
            labelBrightness.Name = "labelBrightness";
            labelBrightness.Size = new Size(336, 43);
            labelBrightness.TabIndex = 41;
            labelBrightness.Text = "Brightness";
            // 
            // trackBrightness
            // 
            trackBrightness.LargeChange = 1;
            trackBrightness.Location = new Point(355, 59);
            trackBrightness.Margin = new Padding(4, 2, 4, 2);
            trackBrightness.Maximum = 3;
            trackBrightness.Name = "trackBrightness";
            trackBrightness.Size = new Size(557, 90);
            trackBrightness.TabIndex = 42;
            trackBrightness.TickStyle = TickStyle.TopLeft;
            // 
            // labelSpeed
            // 
            labelSpeed.Location = new Point(13, 18);
            labelSpeed.Margin = new Padding(4, 0, 4, 0);
            labelSpeed.Name = "labelSpeed";
            labelSpeed.Size = new Size(539, 41);
            labelSpeed.TabIndex = 44;
            labelSpeed.Text = "Animation Speed";
            // 
            // comboKeyboardSpeed
            // 
            comboKeyboardSpeed.BorderColor = Color.White;
            comboKeyboardSpeed.ButtonColor = SystemColors.ControlLight;
            comboKeyboardSpeed.FlatStyle = FlatStyle.Flat;
            comboKeyboardSpeed.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            comboKeyboardSpeed.FormattingEnabled = true;
            comboKeyboardSpeed.ItemHeight = 32;
            comboKeyboardSpeed.Items.AddRange(new object[] { "Slow", "Normal", "Fast" });
            comboKeyboardSpeed.Location = new Point(607, 16);
            comboKeyboardSpeed.Margin = new Padding(4, 11, 4, 9);
            comboKeyboardSpeed.Name = "comboKeyboardSpeed";
            comboKeyboardSpeed.Size = new Size(292, 40);
            comboKeyboardSpeed.TabIndex = 43;
            comboKeyboardSpeed.TabStop = false;
            // 
            // panelXMG
            // 
            panelXMG.Controls.Add(checkXMG);
            panelXMG.Dock = DockStyle.Top;
            panelXMG.Location = new Point(4, 241);
            panelXMG.Margin = new Padding(4, 2, 4, 2);
            panelXMG.Name = "panelXMG";
            panelXMG.Size = new Size(958, 58);
            panelXMG.TabIndex = 42;
            // 
            // checkXMG
            // 
            checkXMG.AutoSize = true;
            checkXMG.Location = new Point(4, 11);
            checkXMG.Margin = new Padding(4, 2, 4, 2);
            checkXMG.Name = "checkXMG";
            checkXMG.Padding = new Padding(15, 2, 6, 2);
            checkXMG.Size = new Size(179, 40);
            checkXMG.TabIndex = 2;
            checkXMG.Text = "XG Mobile";
            checkXMG.UseVisualStyleBackColor = true;
            // 
            // tableBacklight
            // 
            tableBacklight.AutoSize = true;
            tableBacklight.ColumnCount = 4;
            tableBacklight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableBacklight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableBacklight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableBacklight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableBacklight.Controls.Add(labelBacklight, 0, 0);
            tableBacklight.Controls.Add(checkAwake, 0, 1);
            tableBacklight.Controls.Add(checkBoot, 0, 2);
            tableBacklight.Controls.Add(checkSleep, 0, 3);
            tableBacklight.Controls.Add(checkShutdown, 0, 4);
            tableBacklight.Controls.Add(labelBacklightLogo, 1, 0);
            tableBacklight.Controls.Add(checkAwakeLogo, 1, 1);
            tableBacklight.Controls.Add(checkBootLogo, 1, 2);
            tableBacklight.Controls.Add(checkSleepLogo, 1, 3);
            tableBacklight.Controls.Add(checkShutdownLogo, 1, 4);
            tableBacklight.Controls.Add(labelBacklightBar, 2, 0);
            tableBacklight.Controls.Add(checkAwakeBar, 2, 1);
            tableBacklight.Controls.Add(checkBootBar, 2, 2);
            tableBacklight.Controls.Add(checkSleepBar, 2, 3);
            tableBacklight.Controls.Add(checkShutdownBar, 2, 4);
            tableBacklight.Controls.Add(labelBacklightLid, 3, 0);
            tableBacklight.Controls.Add(checkAwakeLid, 3, 1);
            tableBacklight.Controls.Add(checkBootLid, 3, 2);
            tableBacklight.Controls.Add(checkSleepLid, 3, 3);
            tableBacklight.Controls.Add(checkShutdownLid, 3, 4);
            tableBacklight.Dock = DockStyle.Top;
            tableBacklight.Location = new Point(4, 34);
            tableBacklight.Margin = new Padding(0);
            tableBacklight.Name = "tableBacklight";
            tableBacklight.RowCount = 5;
            tableBacklight.RowStyles.Add(new RowStyle());
            tableBacklight.RowStyles.Add(new RowStyle());
            tableBacklight.RowStyles.Add(new RowStyle());
            tableBacklight.RowStyles.Add(new RowStyle());
            tableBacklight.RowStyles.Add(new RowStyle());
            tableBacklight.Size = new Size(958, 207);
            tableBacklight.TabIndex = 41;
            // 
            // labelBacklight
            // 
            labelBacklight.Dock = DockStyle.Fill;
            labelBacklight.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelBacklight.Location = new Point(4, 0);
            labelBacklight.Margin = new Padding(4, 0, 4, 0);
            labelBacklight.Name = "labelBacklight";
            labelBacklight.Padding = new Padding(9, 4, 6, 4);
            labelBacklight.Size = new Size(231, 43);
            labelBacklight.TabIndex = 6;
            labelBacklight.Text = "Keyboard";
            // 
            // checkAwake
            // 
            checkAwake.Dock = DockStyle.Fill;
            checkAwake.Location = new Point(4, 43);
            checkAwake.Margin = new Padding(4, 0, 4, 0);
            checkAwake.Name = "checkAwake";
            checkAwake.Padding = new Padding(15, 2, 6, 2);
            checkAwake.Size = new Size(231, 41);
            checkAwake.TabIndex = 1;
            checkAwake.Text = Strings.Awake;
            checkAwake.UseVisualStyleBackColor = true;
            // 
            // checkBoot
            // 
            checkBoot.Dock = DockStyle.Fill;
            checkBoot.Location = new Point(4, 84);
            checkBoot.Margin = new Padding(4, 0, 4, 0);
            checkBoot.Name = "checkBoot";
            checkBoot.Padding = new Padding(15, 2, 6, 2);
            checkBoot.Size = new Size(231, 41);
            checkBoot.TabIndex = 2;
            checkBoot.Text = Strings.Boot;
            checkBoot.UseVisualStyleBackColor = true;
            // 
            // checkSleep
            // 
            checkSleep.Dock = DockStyle.Fill;
            checkSleep.Location = new Point(4, 125);
            checkSleep.Margin = new Padding(4, 0, 4, 0);
            checkSleep.Name = "checkSleep";
            checkSleep.Padding = new Padding(15, 2, 6, 2);
            checkSleep.Size = new Size(231, 41);
            checkSleep.TabIndex = 3;
            checkSleep.Text = "Sleep";
            checkSleep.UseVisualStyleBackColor = true;
            // 
            // checkShutdown
            // 
            checkShutdown.Dock = DockStyle.Fill;
            checkShutdown.Location = new Point(4, 166);
            checkShutdown.Margin = new Padding(4, 0, 4, 0);
            checkShutdown.Name = "checkShutdown";
            checkShutdown.Padding = new Padding(15, 2, 6, 2);
            checkShutdown.Size = new Size(231, 41);
            checkShutdown.TabIndex = 4;
            checkShutdown.Text = Strings.Shutdown;
            checkShutdown.UseVisualStyleBackColor = true;
            // 
            // labelBacklightLogo
            // 
            labelBacklightLogo.Dock = DockStyle.Fill;
            labelBacklightLogo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelBacklightLogo.Location = new Point(243, 0);
            labelBacklightLogo.Margin = new Padding(4, 0, 4, 0);
            labelBacklightLogo.Name = "labelBacklightLogo";
            labelBacklightLogo.Padding = new Padding(9, 4, 6, 4);
            labelBacklightLogo.Size = new Size(231, 43);
            labelBacklightLogo.TabIndex = 21;
            labelBacklightLogo.Text = "Logo";
            // 
            // checkAwakeLogo
            // 
            checkAwakeLogo.Dock = DockStyle.Fill;
            checkAwakeLogo.Location = new Point(243, 43);
            checkAwakeLogo.Margin = new Padding(4, 0, 4, 0);
            checkAwakeLogo.Name = "checkAwakeLogo";
            checkAwakeLogo.Padding = new Padding(15, 2, 6, 2);
            checkAwakeLogo.Size = new Size(231, 41);
            checkAwakeLogo.TabIndex = 17;
            checkAwakeLogo.Text = Strings.Awake;
            checkAwakeLogo.UseVisualStyleBackColor = true;
            // 
            // checkBootLogo
            // 
            checkBootLogo.Dock = DockStyle.Fill;
            checkBootLogo.Location = new Point(243, 84);
            checkBootLogo.Margin = new Padding(4, 0, 4, 0);
            checkBootLogo.Name = "checkBootLogo";
            checkBootLogo.Padding = new Padding(15, 2, 6, 2);
            checkBootLogo.Size = new Size(231, 41);
            checkBootLogo.TabIndex = 18;
            checkBootLogo.Text = Strings.Boot;
            checkBootLogo.UseVisualStyleBackColor = true;
            // 
            // checkSleepLogo
            // 
            checkSleepLogo.Dock = DockStyle.Fill;
            checkSleepLogo.Location = new Point(243, 125);
            checkSleepLogo.Margin = new Padding(4, 0, 4, 0);
            checkSleepLogo.Name = "checkSleepLogo";
            checkSleepLogo.Padding = new Padding(15, 2, 6, 2);
            checkSleepLogo.Size = new Size(231, 41);
            checkSleepLogo.TabIndex = 19;
            checkSleepLogo.Text = Strings.Sleep;
            checkSleepLogo.UseVisualStyleBackColor = true;
            // 
            // checkShutdownLogo
            // 
            checkShutdownLogo.Dock = DockStyle.Fill;
            checkShutdownLogo.Location = new Point(243, 166);
            checkShutdownLogo.Margin = new Padding(4, 0, 4, 0);
            checkShutdownLogo.Name = "checkShutdownLogo";
            checkShutdownLogo.Padding = new Padding(15, 2, 6, 2);
            checkShutdownLogo.Size = new Size(231, 41);
            checkShutdownLogo.TabIndex = 20;
            checkShutdownLogo.Text = Strings.Shutdown;
            checkShutdownLogo.UseVisualStyleBackColor = true;
            // 
            // labelBacklightBar
            // 
            labelBacklightBar.Dock = DockStyle.Fill;
            labelBacklightBar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelBacklightBar.Location = new Point(482, 0);
            labelBacklightBar.Margin = new Padding(4, 0, 4, 0);
            labelBacklightBar.Name = "labelBacklightBar";
            labelBacklightBar.Padding = new Padding(9, 4, 6, 4);
            labelBacklightBar.Size = new Size(231, 43);
            labelBacklightBar.TabIndex = 11;
            labelBacklightBar.Text = "Lightbar";
            // 
            // checkAwakeBar
            // 
            checkAwakeBar.Dock = DockStyle.Fill;
            checkAwakeBar.Location = new Point(482, 43);
            checkAwakeBar.Margin = new Padding(4, 0, 4, 0);
            checkAwakeBar.Name = "checkAwakeBar";
            checkAwakeBar.Padding = new Padding(15, 2, 6, 2);
            checkAwakeBar.Size = new Size(231, 41);
            checkAwakeBar.TabIndex = 7;
            checkAwakeBar.Text = Strings.Awake;
            checkAwakeBar.UseVisualStyleBackColor = true;
            // 
            // checkBootBar
            // 
            checkBootBar.Dock = DockStyle.Fill;
            checkBootBar.Location = new Point(482, 84);
            checkBootBar.Margin = new Padding(4, 0, 4, 0);
            checkBootBar.Name = "checkBootBar";
            checkBootBar.Padding = new Padding(15, 2, 6, 2);
            checkBootBar.Size = new Size(231, 41);
            checkBootBar.TabIndex = 8;
            checkBootBar.Text = Strings.Boot;
            checkBootBar.UseVisualStyleBackColor = true;
            // 
            // checkSleepBar
            // 
            checkSleepBar.Dock = DockStyle.Fill;
            checkSleepBar.Location = new Point(482, 125);
            checkSleepBar.Margin = new Padding(4, 0, 4, 0);
            checkSleepBar.Name = "checkSleepBar";
            checkSleepBar.Padding = new Padding(15, 2, 6, 2);
            checkSleepBar.Size = new Size(231, 41);
            checkSleepBar.TabIndex = 9;
            checkSleepBar.Text = Strings.Sleep;
            checkSleepBar.UseVisualStyleBackColor = true;
            // 
            // checkShutdownBar
            // 
            checkShutdownBar.Dock = DockStyle.Fill;
            checkShutdownBar.Location = new Point(482, 166);
            checkShutdownBar.Margin = new Padding(4, 0, 4, 0);
            checkShutdownBar.Name = "checkShutdownBar";
            checkShutdownBar.Padding = new Padding(15, 2, 6, 2);
            checkShutdownBar.Size = new Size(231, 41);
            checkShutdownBar.TabIndex = 10;
            checkShutdownBar.Text = Strings.Shutdown;
            checkShutdownBar.UseVisualStyleBackColor = true;
            // 
            // labelBacklightLid
            // 
            labelBacklightLid.Dock = DockStyle.Fill;
            labelBacklightLid.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelBacklightLid.Location = new Point(721, 0);
            labelBacklightLid.Margin = new Padding(4, 0, 4, 0);
            labelBacklightLid.Name = "labelBacklightLid";
            labelBacklightLid.Padding = new Padding(9, 4, 6, 4);
            labelBacklightLid.Size = new Size(233, 43);
            labelBacklightLid.TabIndex = 16;
            labelBacklightLid.Text = "Lid";
            // 
            // checkAwakeLid
            // 
            checkAwakeLid.Dock = DockStyle.Fill;
            checkAwakeLid.Location = new Point(721, 43);
            checkAwakeLid.Margin = new Padding(4, 0, 4, 0);
            checkAwakeLid.Name = "checkAwakeLid";
            checkAwakeLid.Padding = new Padding(15, 2, 6, 2);
            checkAwakeLid.Size = new Size(233, 41);
            checkAwakeLid.TabIndex = 12;
            checkAwakeLid.Text = Strings.Awake;
            checkAwakeLid.UseVisualStyleBackColor = true;
            // 
            // checkBootLid
            // 
            checkBootLid.Dock = DockStyle.Fill;
            checkBootLid.Location = new Point(721, 84);
            checkBootLid.Margin = new Padding(4, 0, 4, 0);
            checkBootLid.Name = "checkBootLid";
            checkBootLid.Padding = new Padding(15, 2, 6, 2);
            checkBootLid.Size = new Size(233, 41);
            checkBootLid.TabIndex = 13;
            checkBootLid.Text = Strings.Boot;
            checkBootLid.UseVisualStyleBackColor = true;
            // 
            // checkSleepLid
            // 
            checkSleepLid.Dock = DockStyle.Fill;
            checkSleepLid.Location = new Point(721, 125);
            checkSleepLid.Margin = new Padding(4, 0, 4, 0);
            checkSleepLid.Name = "checkSleepLid";
            checkSleepLid.Padding = new Padding(15, 2, 6, 2);
            checkSleepLid.Size = new Size(233, 41);
            checkSleepLid.TabIndex = 14;
            checkSleepLid.Text = Strings.Sleep;
            checkSleepLid.UseVisualStyleBackColor = true;
            // 
            // checkShutdownLid
            // 
            checkShutdownLid.Dock = DockStyle.Fill;
            checkShutdownLid.Location = new Point(721, 166);
            checkShutdownLid.Margin = new Padding(4, 0, 4, 0);
            checkShutdownLid.Name = "checkShutdownLid";
            checkShutdownLid.Padding = new Padding(15, 2, 6, 2);
            checkShutdownLid.Size = new Size(233, 41);
            checkShutdownLid.TabIndex = 15;
            checkShutdownLid.Text = Strings.Shutdown;
            checkShutdownLid.UseVisualStyleBackColor = true;
            // 
            // groupOther
            // 
            groupOther.AutoSize = true;
            groupOther.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupOther.Controls.Add(checkAutoApplyWindowsPowerMode);
            groupOther.Controls.Add(checkTopmost);
            groupOther.Controls.Add(checkNoOverdrive);
            groupOther.Controls.Add(checkUSBC);
            groupOther.Controls.Add(checkVariBright);
            groupOther.Controls.Add(checkGpuApps);
            groupOther.Controls.Add(checkFnLock);
            groupOther.Dock = DockStyle.Top;
            groupOther.Location = new Point(9, 888);
            groupOther.Margin = new Padding(4, 2, 4, 2);
            groupOther.Name = "groupOther";
            groupOther.Padding = new Padding(20, 2, 4, 10);
            groupOther.Size = new Size(966, 296);
            groupOther.TabIndex = 2;
            groupOther.TabStop = false;
            groupOther.Text = "Other";
            // 
            // checkAutoApplyWindowsPowerMode
            // 
            checkAutoApplyWindowsPowerMode.AutoSize = true;
            checkAutoApplyWindowsPowerMode.Dock = DockStyle.Top;
            checkAutoApplyWindowsPowerMode.Location = new Point(20, 250);
            checkAutoApplyWindowsPowerMode.Margin = new Padding(4, 2, 4, 2);
            checkAutoApplyWindowsPowerMode.Name = "checkAutoApplyWindowsPowerMode";
            checkAutoApplyWindowsPowerMode.Size = new Size(942, 36);
            checkAutoApplyWindowsPowerMode.TabIndex = 47;
            checkAutoApplyWindowsPowerMode.Text = "Auto Adjust Windows Power Mode";
            checkAutoApplyWindowsPowerMode.UseVisualStyleBackColor = true;
            // 
            // checkTopmost
            // 
            checkTopmost.AutoSize = true;
            checkTopmost.Dock = DockStyle.Top;
            checkTopmost.Location = new Point(20, 214);
            checkTopmost.Margin = new Padding(4, 2, 4, 2);
            checkTopmost.Name = "checkTopmost";
            checkTopmost.Size = new Size(942, 36);
            checkTopmost.TabIndex = 1;
            checkTopmost.Text = Strings.WindowTop;
            checkTopmost.UseVisualStyleBackColor = true;
            // 
            // checkNoOverdrive
            // 
            checkNoOverdrive.AutoSize = true;
            checkNoOverdrive.Dock = DockStyle.Top;
            checkNoOverdrive.Location = new Point(20, 178);
            checkNoOverdrive.Margin = new Padding(4, 2, 4, 2);
            checkNoOverdrive.Name = "checkNoOverdrive";
            checkNoOverdrive.Size = new Size(942, 36);
            checkNoOverdrive.TabIndex = 3;
            checkNoOverdrive.Text = Strings.DisableOverdrive;
            checkNoOverdrive.UseVisualStyleBackColor = true;
            // 
            // checkUSBC
            // 
            checkUSBC.AutoSize = true;
            checkUSBC.Dock = DockStyle.Top;
            checkUSBC.Location = new Point(20, 142);
            checkUSBC.Margin = new Padding(4, 2, 4, 2);
            checkUSBC.Name = "checkUSBC";
            checkUSBC.Size = new Size(942, 36);
            checkUSBC.TabIndex = 4;
            checkUSBC.Text = "Keep GPU disabled on USB-C charger in Optimized mode";
            checkUSBC.UseVisualStyleBackColor = true;
            // 
            // checkVariBright
            // 
            checkVariBright.AutoSize = true;
            checkVariBright.Dock = DockStyle.Top;
            checkVariBright.Location = new Point(20, 106);
            checkVariBright.Margin = new Padding(4, 2, 4, 2);
            checkVariBright.Name = "checkVariBright";
            checkVariBright.Size = new Size(942, 36);
            checkVariBright.TabIndex = 50;
            checkVariBright.Text = "AMD Display VariBright";
            checkVariBright.UseVisualStyleBackColor = true;
            // 
            // checkGpuApps
            // 
            checkGpuApps.AutoSize = true;
            checkGpuApps.Dock = DockStyle.Top;
            checkGpuApps.Location = new Point(20, 70);
            checkGpuApps.Margin = new Padding(4, 2, 4, 2);
            checkGpuApps.Name = "checkGpuApps";
            checkGpuApps.Size = new Size(942, 36);
            checkGpuApps.TabIndex = 48;
            checkGpuApps.Text = "Stop all apps using GPU when switching to Eco";
            checkGpuApps.UseVisualStyleBackColor = true;
            // 
            // checkFnLock
            // 
            checkFnLock.AutoSize = true;
            checkFnLock.Dock = DockStyle.Top;
            checkFnLock.Location = new Point(20, 34);
            checkFnLock.Margin = new Padding(4, 2, 4, 2);
            checkFnLock.MaximumSize = new Size(780, 0);
            checkFnLock.Name = "checkFnLock";
            checkFnLock.Size = new Size(780, 36);
            checkFnLock.TabIndex = 49;
            checkFnLock.Text = "Process Fn+F hotkeys without Fn";
            checkFnLock.UseVisualStyleBackColor = true;
            // 
            // panelServices
            // 
            panelServices.Controls.Add(labelServices);
            panelServices.Controls.Add(buttonServices);
            panelServices.Dock = DockStyle.Top;
            panelServices.Location = new Point(9, 1184);
            panelServices.Name = "panelServices";
            panelServices.Size = new Size(966, 72);
            panelServices.TabIndex = 3;
            // 
            // labelServices
            // 
            labelServices.AutoSize = true;
            labelServices.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelServices.Location = new Point(23, 19);
            labelServices.Name = "labelServices";
            labelServices.Size = new Size(273, 32);
            labelServices.TabIndex = 20;
            labelServices.Text = "Asus Services Running";
            // 
            // buttonServices
            // 
            buttonServices.Activated = false;
            buttonServices.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonServices.BackColor = SystemColors.ControlLight;
            buttonServices.BorderColor = Color.Transparent;
            buttonServices.BorderRadius = 2;
            buttonServices.FlatStyle = FlatStyle.Flat;
            buttonServices.Location = new Point(695, 14);
            buttonServices.Margin = new Padding(4, 2, 4, 2);
            buttonServices.Name = "buttonServices";
            buttonServices.Secondary = true;
            buttonServices.Size = new Size(250, 44);
            buttonServices.TabIndex = 19;
            buttonServices.Text = "Start Services";
            buttonServices.UseVisualStyleBackColor = false;
            // 
            // Extra
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(984, 1262);
            Controls.Add(panelServices);
            Controls.Add(groupOther);
            Controls.Add(groupLight);
            Controls.Add(groupBindings);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 2, 4, 2);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            MinimumSize = new Size(1010, 71);
            Name = "Extra";
            Padding = new Padding(9, 11, 9, 11);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Extra Settings";
            groupBindings.ResumeLayout(false);
            tableKeys.ResumeLayout(false);
            tableKeys.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureHelp).EndInit();
            groupLight.ResumeLayout(false);
            groupLight.PerformLayout();
            panelBacklightExtra.ResumeLayout(false);
            panelBacklightExtra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericBacklightPluggedTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericBacklightTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBrightness).EndInit();
            panelXMG.ResumeLayout(false);
            panelXMG.PerformLayout();
            tableBacklight.ResumeLayout(false);
            groupOther.ResumeLayout(false);
            groupOther.PerformLayout();
            panelServices.ResumeLayout(false);
            panelServices.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBindings;
        private Label labelM3;
        private RComboBox comboM3;
        private RComboBox comboM4;
        private Label labelM4;
        private TextBox textM4;
        private TextBox textM3;
        private TextBox textFNF4;
        private RComboBox comboFNF4;
        private Label labelFNF4;
        private GroupBox groupLight;
        private GroupBox groupOther;
        private CheckBox checkTopmost;
        private CheckBox checkNoOverdrive;
        private PictureBox pictureHelp;
        private CheckBox checkUSBC;
        private TableLayoutPanel tableBacklight;
        private CheckBox checkShutdown;
        private CheckBox checkAwake;
        private CheckBox checkBoot;
        private CheckBox checkSleep;
        private CheckBox checkBootLid;
        private Label labelBacklight;
        private CheckBox checkSleepBar;
        private CheckBox checkShutdownBar;
        private Label labelBacklightBar;
        private CheckBox checkAwakeBar;
        private CheckBox checkBootBar;
        private CheckBox checkSleepLid;
        private CheckBox checkShutdownLid;
        private Label labelBacklightLid;
        private CheckBox checkAwakeLid;
        private Label labelBacklightLogo;
        private CheckBox checkAwakeLogo;
        private CheckBox checkBootLogo;
        private CheckBox checkSleepLogo;
        private CheckBox checkShutdownLogo;
        private Panel panelBacklightExtra;
        private Label labelBrightness;
        private TrackBar trackBrightness;
        private Label labelSpeed;
        private RComboBox comboKeyboardSpeed;
        private Panel panelXMG;
        private CheckBox checkXMG;
        private Label labelBacklightTimeout;
        private NumericUpDown numericBacklightTime;
        private CheckBox checkAutoApplyWindowsPowerMode;
        private TableLayoutPanel tableKeys;
        private Label labelM1;
        private Label labelM2;
        private RComboBox comboM1;
        private RComboBox comboM2;
        private TextBox textM2;
        private TextBox textM1;
        private NumericUpDown numericBacklightPluggedTime;
        private Label labelBacklightTimeoutPlugged;
        private CheckBox checkGpuApps;
        private CheckBox checkFnLock;
        private Label labelFNC;
        private RComboBox comboFNC;
        private TextBox textFNC;
        private CheckBox checkVariBright;
        private Panel panelServices;
        private RButton buttonServices;
        private Label labelServices;
    }
}