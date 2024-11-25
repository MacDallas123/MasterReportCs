namespace MasterReport
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.activeMarkerPanel = new System.Windows.Forms.Panel();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.importExportBtn = new System.Windows.Forms.Button();
            this.reportsBtn = new System.Windows.Forms.Button();
            this.managementBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.managementUserControl = new MasterReport.ManagementUserControl();
            this.reportsUserControl = new MasterReport.ReportsUserControl();
            this.importExportUserControl = new MasterReport.ImportExportUserControl();
            this.settingsUserControl = new MasterReport.SettingsUserControl();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.splitContainer1);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(938, 547);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.MintCream;
            this.splitContainer1.Panel1.Controls.Add(this.activeMarkerPanel);
            this.splitContainer1.Panel1.Controls.Add(this.settingsBtn);
            this.splitContainer1.Panel1.Controls.Add(this.exitBtn);
            this.splitContainer1.Panel1.Controls.Add(this.importExportBtn);
            this.splitContainer1.Panel1.Controls.Add(this.reportsBtn);
            this.splitContainer1.Panel1.Controls.Add(this.managementBtn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(938, 547);
            this.splitContainer1.SplitterDistance = 128;
            this.splitContainer1.TabIndex = 2;
            // 
            // activeMarkerPanel
            // 
            this.activeMarkerPanel.BackColor = System.Drawing.Color.Green;
            this.activeMarkerPanel.Location = new System.Drawing.Point(3, 3);
            this.activeMarkerPanel.Name = "activeMarkerPanel";
            this.activeMarkerPanel.Size = new System.Drawing.Size(12, 61);
            this.activeMarkerPanel.TabIndex = 0;
            // 
            // settingsBtn
            // 
            this.settingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.settingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.settingsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsBtn.Image = global::MasterReport.Properties.Resources.settings_32px;
            this.settingsBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.settingsBtn.Location = new System.Drawing.Point(3, 204);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(122, 61);
            this.settingsBtn.TabIndex = 3;
            this.settingsBtn.Text = "Options";
            this.settingsBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.metroToolTip1.SetToolTip(this.settingsBtn, "Informations supplémentaires (détails entreprise, préférences, ...)");
            this.settingsBtn.UseVisualStyleBackColor = false;
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exitBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.exitBtn.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.exitBtn.FlatAppearance.BorderSize = 2;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.exitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBtn.Image = global::MasterReport.Properties.Resources.shutdown_32px;
            this.exitBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.exitBtn.Location = new System.Drawing.Point(3, 271);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(122, 61);
            this.exitBtn.TabIndex = 6;
            this.exitBtn.Text = "Quitter";
            this.exitBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // importExportBtn
            // 
            this.importExportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importExportBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.importExportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.importExportBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importExportBtn.Image = global::MasterReport.Properties.Resources.up_down_arrow_32px;
            this.importExportBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.importExportBtn.Location = new System.Drawing.Point(3, 137);
            this.importExportBtn.Name = "importExportBtn";
            this.importExportBtn.Size = new System.Drawing.Size(122, 61);
            this.importExportBtn.TabIndex = 5;
            this.importExportBtn.Text = "Imports/Exports";
            this.importExportBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.metroToolTip1.SetToolTip(this.importExportBtn, "Importations et exportations des données");
            this.importExportBtn.UseVisualStyleBackColor = false;
            this.importExportBtn.Click += new System.EventHandler(this.importExportBtn_Click);
            // 
            // reportsBtn
            // 
            this.reportsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportsBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.reportsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reportsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportsBtn.Image = global::MasterReport.Properties.Resources.report_file_32px;
            this.reportsBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.reportsBtn.Location = new System.Drawing.Point(3, 70);
            this.reportsBtn.Name = "reportsBtn";
            this.reportsBtn.Size = new System.Drawing.Size(122, 61);
            this.reportsBtn.TabIndex = 4;
            this.reportsBtn.Text = "Rapports";
            this.reportsBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.metroToolTip1.SetToolTip(this.reportsBtn, "Génération des suivis et etats des clients");
            this.reportsBtn.UseVisualStyleBackColor = false;
            this.reportsBtn.Click += new System.EventHandler(this.reportsBtn_Click);
            // 
            // managementBtn
            // 
            this.managementBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.managementBtn.BackColor = System.Drawing.Color.MintCream;
            this.managementBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.managementBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.managementBtn.Image = global::MasterReport.Properties.Resources.add_user_male_32px;
            this.managementBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.managementBtn.Location = new System.Drawing.Point(3, 3);
            this.managementBtn.Name = "managementBtn";
            this.managementBtn.Size = new System.Drawing.Size(122, 61);
            this.managementBtn.TabIndex = 2;
            this.managementBtn.Text = "Gestion";
            this.managementBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.metroToolTip1.SetToolTip(this.managementBtn, "Enregistrements, modifications et suppression des clients et de leurs suivis");
            this.managementBtn.UseVisualStyleBackColor = false;
            this.managementBtn.Click += new System.EventHandler(this.managementBtn_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.managementUserControl);
            this.panel2.Controls.Add(this.reportsUserControl);
            this.panel2.Controls.Add(this.importExportUserControl);
            this.panel2.Controls.Add(this.settingsUserControl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(806, 547);
            this.panel2.TabIndex = 0;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // managementUserControl
            // 
            this.managementUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managementUserControl.Location = new System.Drawing.Point(0, 0);
            this.managementUserControl.Name = "managementUserControl";
            this.managementUserControl.Size = new System.Drawing.Size(804, 545);
            this.managementUserControl.TabIndex = 3;
            // 
            // reportsUserControl
            // 
            this.reportsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportsUserControl.Location = new System.Drawing.Point(0, 0);
            this.reportsUserControl.Name = "reportsUserControl";
            this.reportsUserControl.Size = new System.Drawing.Size(804, 545);
            this.reportsUserControl.TabIndex = 2;
            // 
            // importExportUserControl
            // 
            this.importExportUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importExportUserControl.Location = new System.Drawing.Point(0, 0);
            this.importExportUserControl.Name = "importExportUserControl";
            this.importExportUserControl.Size = new System.Drawing.Size(804, 545);
            this.importExportUserControl.TabIndex = 1;
            this.importExportUserControl.Load += new System.EventHandler(this.importExportUserControl_Load);
            // 
            // settingsUserControl
            // 
            this.settingsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsUserControl.Location = new System.Drawing.Point(0, 0);
            this.settingsUserControl.Name = "settingsUserControl";
            this.settingsUserControl.Size = new System.Drawing.Size(804, 545);
            this.settingsUserControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 627);
            this.Controls.Add(this.metroPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "MasterReport v1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.metroPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private System.Windows.Forms.Button managementBtn;
        private System.Windows.Forms.Button settingsBtn;
        private System.Windows.Forms.Button reportsBtn;
        private System.Windows.Forms.Button importExportBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Panel activeMarkerPanel;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private SettingsUserControl settingsUserControl;
        private ImportExportUserControl importExportUserControl;
        private ReportsUserControl reportsUserControl;
        private ManagementUserControl managementUserControl;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
    }
}

