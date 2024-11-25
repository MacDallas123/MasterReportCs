namespace MasterReport
{
    partial class ImportExportUserControl
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.spinnerPercentage = new System.Windows.Forms.Label();
            this.spinnerLabel = new System.Windows.Forms.Label();
            this.processSpinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.periodComboBox = new MetroFramework.Controls.MetroComboBox();
            this.importBtn = new MaterialSkin.Controls.MaterialButton();
            this.periodLabel = new System.Windows.Forms.Label();
            this.chooseFileBtn = new MaterialSkin.Controls.MaterialButton();
            this.filenameTextBox = new System.Windows.Forms.TextBox();
            this.filenameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.YearsList = new MetroFramework.Controls.MetroComboBox();
            this.MonthsList = new MetroFramework.Controls.MetroComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.exportToXlsxBtn = new System.Windows.Forms.Button();
            this.exportMessageLabel = new System.Windows.Forms.Label();
            this.exportSpinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.label3 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.htmlToolTip1 = new MetroFramework.Drawing.Html.HtmlToolTip();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.HotTrack = true;
            this.metroTabControl1.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(600, 430);
            this.metroTabControl1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTabControl1.TabIndex = 3;
            this.metroTabControl1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.AutoScroll = true;
            this.metroTabPage1.Controls.Add(this.label13);
            this.metroTabPage1.Controls.Add(this.spinnerPercentage);
            this.metroTabPage1.Controls.Add(this.spinnerLabel);
            this.metroTabPage1.Controls.Add(this.processSpinner);
            this.metroTabPage1.Controls.Add(this.periodComboBox);
            this.metroTabPage1.Controls.Add(this.importBtn);
            this.metroTabPage1.Controls.Add(this.periodLabel);
            this.metroTabPage1.Controls.Add(this.chooseFileBtn);
            this.metroTabPage1.Controls.Add(this.filenameTextBox);
            this.metroTabPage1.Controls.Add(this.filenameLabel);
            this.metroTabPage1.Controls.Add(this.label1);
            this.metroTabPage1.HorizontalScrollbar = true;
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(592, 388);
            this.metroTabPage1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Importer";
            this.metroTabPage1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTabPage1.UseStyleColors = true;
            this.metroTabPage1.VerticalScrollbar = true;
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = true;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label13.AutoEllipsis = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Image = global::MasterReport.Properties.Resources.info_green_32px;
            this.label13.Location = new System.Drawing.Point(117, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 30);
            this.label13.TabIndex = 66;
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.htmlToolTip1.SetToolTip(this.label13, "Importer les données provenant d\'un fichier excel ayant un format correct\r\nsinon " +
        "l\'importation echouera.\r\n\r\nUne fois l\'importation terminée, redémarrer le logici" +
        "el\r\npour appliquer les changements\r\n");
            // 
            // spinnerPercentage
            // 
            this.spinnerPercentage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.spinnerPercentage.AutoEllipsis = true;
            this.spinnerPercentage.AutoSize = true;
            this.spinnerPercentage.BackColor = System.Drawing.Color.Transparent;
            this.spinnerPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinnerPercentage.ForeColor = System.Drawing.Color.Black;
            this.spinnerPercentage.Location = new System.Drawing.Point(459, 86);
            this.spinnerPercentage.Name = "spinnerPercentage";
            this.spinnerPercentage.Size = new System.Drawing.Size(32, 20);
            this.spinnerPercentage.TabIndex = 19;
            this.spinnerPercentage.Text = "0%";
            this.spinnerPercentage.Visible = false;
            // 
            // spinnerLabel
            // 
            this.spinnerLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.spinnerLabel.AutoEllipsis = true;
            this.spinnerLabel.AutoSize = true;
            this.spinnerLabel.BackColor = System.Drawing.Color.Transparent;
            this.spinnerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinnerLabel.ForeColor = System.Drawing.Color.Black;
            this.spinnerLabel.Location = new System.Drawing.Point(156, 86);
            this.spinnerLabel.Name = "spinnerLabel";
            this.spinnerLabel.Size = new System.Drawing.Size(297, 20);
            this.spinnerLabel.TabIndex = 18;
            this.spinnerLabel.Text = "Importation en cours, veuillez patienter ...";
            this.spinnerLabel.Visible = false;
            // 
            // processSpinner
            // 
            this.processSpinner.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.processSpinner.BackColor = System.Drawing.Color.White;
            this.processSpinner.CustomBackground = true;
            this.processSpinner.Location = new System.Drawing.Point(101, 72);
            this.processSpinner.Maximum = 100;
            this.processSpinner.Name = "processSpinner";
            this.processSpinner.Size = new System.Drawing.Size(49, 46);
            this.processSpinner.Style = MetroFramework.MetroColorStyle.Green;
            this.processSpinner.TabIndex = 17;
            this.processSpinner.Theme = MetroFramework.MetroThemeStyle.Light;
            this.processSpinner.UseCustomBackColor = true;
            this.processSpinner.UseSelectable = true;
            this.processSpinner.Visible = false;
            // 
            // periodComboBox
            // 
            this.periodComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.periodComboBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.periodComboBox.FormattingEnabled = true;
            this.periodComboBox.ItemHeight = 23;
            this.periodComboBox.Items.AddRange(new object[] {
            "Juin 2024",
            "Juillet 2024",
            "Août 2024",
            "Septembre 2024",
            "Octobre 2024"});
            this.periodComboBox.Location = new System.Drawing.Point(141, 206);
            this.periodComboBox.Name = "periodComboBox";
            this.periodComboBox.Size = new System.Drawing.Size(344, 29);
            this.periodComboBox.Style = MetroFramework.MetroColorStyle.Green;
            this.periodComboBox.TabIndex = 16;
            this.periodComboBox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.periodComboBox.UseSelectable = true;
            // 
            // importBtn
            // 
            this.importBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.importBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.importBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.importBtn.Depth = 0;
            this.importBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importBtn.HighEmphasis = true;
            this.importBtn.Icon = global::MasterReport.Properties.Resources.save_32px;
            this.importBtn.Location = new System.Drawing.Point(434, 267);
            this.importBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.importBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.importBtn.Name = "importBtn";
            this.importBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.importBtn.Size = new System.Drawing.Size(122, 36);
            this.importBtn.TabIndex = 15;
            this.importBtn.Text = "Importer";
            this.importBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.importBtn.UseAccentColor = false;
            this.importBtn.UseVisualStyleBackColor = true;
            this.importBtn.Click += new System.EventHandler(this.importBtn_Click);
            // 
            // periodLabel
            // 
            this.periodLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.periodLabel.AutoEllipsis = true;
            this.periodLabel.AutoSize = true;
            this.periodLabel.BackColor = System.Drawing.Color.Transparent;
            this.periodLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodLabel.ForeColor = System.Drawing.Color.Black;
            this.periodLabel.Location = new System.Drawing.Point(62, 215);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(71, 20);
            this.periodLabel.TabIndex = 9;
            this.periodLabel.Text = "Période :";
            // 
            // chooseFileBtn
            // 
            this.chooseFileBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chooseFileBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chooseFileBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.chooseFileBtn.Depth = 0;
            this.chooseFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooseFileBtn.HighEmphasis = true;
            this.chooseFileBtn.Icon = global::MasterReport.Properties.Resources.opened_folder_24px;
            this.chooseFileBtn.Location = new System.Drawing.Point(492, 149);
            this.chooseFileBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.chooseFileBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.chooseFileBtn.Name = "chooseFileBtn";
            this.chooseFileBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.chooseFileBtn.Size = new System.Drawing.Size(64, 36);
            this.chooseFileBtn.TabIndex = 8;
            this.chooseFileBtn.Text = "+";
            this.chooseFileBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.chooseFileBtn.UseAccentColor = false;
            this.chooseFileBtn.UseVisualStyleBackColor = true;
            this.chooseFileBtn.Click += new System.EventHandler(this.chooseFileBtn_Click);
            // 
            // filenameTextBox
            // 
            this.filenameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.filenameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filenameTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.filenameTextBox.Location = new System.Drawing.Point(141, 154);
            this.filenameTextBox.Name = "filenameTextBox";
            this.filenameTextBox.ReadOnly = true;
            this.filenameTextBox.Size = new System.Drawing.Size(344, 26);
            this.filenameTextBox.TabIndex = 5;
            // 
            // filenameLabel
            // 
            this.filenameLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.filenameLabel.AutoEllipsis = true;
            this.filenameLabel.AutoSize = true;
            this.filenameLabel.BackColor = System.Drawing.Color.Transparent;
            this.filenameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filenameLabel.ForeColor = System.Drawing.Color.Black;
            this.filenameLabel.Location = new System.Drawing.Point(62, 154);
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Size = new System.Drawing.Size(64, 20);
            this.filenameLabel.TabIndex = 4;
            this.filenameLabel.Text = "Fichier :";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(153, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 36);
            this.label1.TabIndex = 3;
            this.label1.Text = "Importer des données";
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.label2);
            this.metroTabPage2.Controls.Add(this.YearsList);
            this.metroTabPage2.Controls.Add(this.MonthsList);
            this.metroTabPage2.Controls.Add(this.label5);
            this.metroTabPage2.Controls.Add(this.exportToXlsxBtn);
            this.metroTabPage2.Controls.Add(this.exportMessageLabel);
            this.metroTabPage2.Controls.Add(this.exportSpinner);
            this.metroTabPage2.Controls.Add(this.label3);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(592, 388);
            this.metroTabPage2.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Exporter";
            this.metroTabPage2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoEllipsis = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Image = global::MasterReport.Properties.Resources.info_green_32px;
            this.label2.Location = new System.Drawing.Point(118, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 30);
            this.label2.TabIndex = 66;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.htmlToolTip1.SetToolTip(this.label2, "Exporter les données vers un fichier excel importable par l\'application");
            // 
            // YearsList
            // 
            this.YearsList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.YearsList.ForeColor = System.Drawing.Color.SteelBlue;
            this.YearsList.FormattingEnabled = true;
            this.YearsList.ItemHeight = 23;
            this.YearsList.Items.AddRange(new object[] {
            "2024"});
            this.YearsList.Location = new System.Drawing.Point(373, 146);
            this.YearsList.Name = "YearsList";
            this.YearsList.Size = new System.Drawing.Size(96, 29);
            this.YearsList.Style = MetroFramework.MetroColorStyle.Green;
            this.YearsList.TabIndex = 38;
            this.YearsList.Theme = MetroFramework.MetroThemeStyle.Light;
            this.YearsList.UseSelectable = true;
            // 
            // MonthsList
            // 
            this.MonthsList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.MonthsList.ForeColor = System.Drawing.Color.SteelBlue;
            this.MonthsList.FormattingEnabled = true;
            this.MonthsList.ItemHeight = 23;
            this.MonthsList.Items.AddRange(new object[] {
            "Janvier",
            "Février",
            "Mars",
            "Avril",
            "Mai",
            "Juin",
            "Juillet",
            "Août",
            "Septembre",
            "Octobre",
            "Novembre",
            "Décembre"});
            this.MonthsList.Location = new System.Drawing.Point(200, 146);
            this.MonthsList.Name = "MonthsList";
            this.MonthsList.Size = new System.Drawing.Size(167, 29);
            this.MonthsList.Style = MetroFramework.MetroColorStyle.Green;
            this.MonthsList.TabIndex = 37;
            this.MonthsList.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MonthsList.UseSelectable = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(123, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "Période :";
            // 
            // exportToXlsxBtn
            // 
            this.exportToXlsxBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.exportToXlsxBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.exportToXlsxBtn.Image = global::MasterReport.Properties.Resources.microsoft_excel_2019_64px;
            this.exportToXlsxBtn.Location = new System.Drawing.Point(234, 195);
            this.exportToXlsxBtn.Name = "exportToXlsxBtn";
            this.exportToXlsxBtn.Size = new System.Drawing.Size(124, 111);
            this.exportToXlsxBtn.TabIndex = 23;
            this.exportToXlsxBtn.Text = "Exporter dans Excel";
            this.exportToXlsxBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exportToXlsxBtn.UseVisualStyleBackColor = true;
            this.exportToXlsxBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // exportMessageLabel
            // 
            this.exportMessageLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.exportMessageLabel.AutoEllipsis = true;
            this.exportMessageLabel.AutoSize = true;
            this.exportMessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.exportMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportMessageLabel.ForeColor = System.Drawing.Color.Black;
            this.exportMessageLabel.Location = new System.Drawing.Point(175, 95);
            this.exportMessageLabel.Name = "exportMessageLabel";
            this.exportMessageLabel.Size = new System.Drawing.Size(297, 20);
            this.exportMessageLabel.TabIndex = 21;
            this.exportMessageLabel.Text = "Exportation en cours, veuillez patienter ...";
            this.exportMessageLabel.Visible = false;
            // 
            // exportSpinner
            // 
            this.exportSpinner.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.exportSpinner.BackColor = System.Drawing.Color.White;
            this.exportSpinner.CustomBackground = true;
            this.exportSpinner.Location = new System.Drawing.Point(120, 80);
            this.exportSpinner.Maximum = 100;
            this.exportSpinner.Name = "exportSpinner";
            this.exportSpinner.Size = new System.Drawing.Size(49, 46);
            this.exportSpinner.Style = MetroFramework.MetroColorStyle.Green;
            this.exportSpinner.TabIndex = 20;
            this.exportSpinner.Theme = MetroFramework.MetroThemeStyle.Light;
            this.exportSpinner.UseCustomBackColor = true;
            this.exportSpinner.UseSelectable = true;
            this.exportSpinner.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(158, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(317, 36);
            this.label3.TabIndex = 4;
            this.label3.Text = "Exporter les données";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // htmlToolTip1
            // 
            this.htmlToolTip1.AutomaticDelay = 1500;
            this.htmlToolTip1.AutoPopDelay = 15000;
            this.htmlToolTip1.InitialDelay = 500;
            this.htmlToolTip1.IsBalloon = true;
            this.htmlToolTip1.OwnerDraw = true;
            this.htmlToolTip1.ReshowDelay = 300;
            this.htmlToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.htmlToolTip1.ToolTipTitle = "Aide";
            // 
            // ImportExportUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroTabControl1);
            this.Name = "ImportExportUserControl";
            this.Size = new System.Drawing.Size(600, 430);
            this.Load += new System.EventHandler(this.ImportExportUserControl_Load);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private System.Windows.Forms.Label periodLabel;
        private MaterialSkin.Controls.MaterialButton chooseFileBtn;
        private System.Windows.Forms.Label filenameLabel;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroComboBox periodComboBox;
        private System.Windows.Forms.Label label3;
        public MetroFramework.Controls.MetroProgressSpinner processSpinner;
        public MaterialSkin.Controls.MaterialButton importBtn;
        public System.Windows.Forms.TextBox filenameTextBox;
        private System.Windows.Forms.Label spinnerLabel;
        public System.Windows.Forms.Label spinnerPercentage;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label exportMessageLabel;
        public MetroFramework.Controls.MetroProgressSpinner exportSpinner;
        public System.Windows.Forms.Button exportToXlsxBtn;
        private System.Windows.Forms.Label label5;
        public MetroFramework.Controls.MetroComboBox YearsList;
        public MetroFramework.Controls.MetroComboBox MonthsList;
        private System.Windows.Forms.Label label13;
        private MetroFramework.Drawing.Html.HtmlToolTip htmlToolTip1;
        private System.Windows.Forms.Label label2;
    }
}
