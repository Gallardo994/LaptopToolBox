using CustomControls;

namespace GHelper.Updates
{
    partial class UpdatesWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdatesWindow));
            buttonRefresh = new RButton();
            tableUpdates = new DataGridView();
            SuspendLayout();
            //
            // buttonRefresh
            //
            buttonRefresh.Location = new Point(10, 10);
            buttonRefresh.Click += new EventHandler(Refresh_Pressed);
            buttonRefresh.FlatStyle = FlatStyle.Flat;
            buttonRefresh.FlatAppearance.BorderSize = 0;
            buttonRefresh.Text = Properties.Strings.ButtonRefresh;
            // 
            // tableBios
            // 
            tableUpdates.AutoSize = true;
            tableUpdates.Dock = DockStyle.Top;
            tableUpdates.Location = new Point(10, 10);
            tableUpdates.Margin = new Padding(2);
            tableUpdates.MinimumSize = new Size(550, 0);
            tableUpdates.Name = "tableUpdates";
            tableUpdates.Size = new Size(608, 0);
            tableUpdates.TabIndex = 0;
            // 
            // Updates
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            ClientSize = new Size(628, 345);
            
            Controls.Add(buttonRefresh);
            Controls.Add(tableUpdates);
            
            Margin = new Padding(2);
            MinimizeBox = false;
            Name = "UpdatesWindow";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "BIOS and Driver Updates";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RButton buttonRefresh;
        private DataGridView tableUpdates;
    }
}