namespace MasterReport
{
    partial class SettingsUserControl
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
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.structureDescription = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.saveBtn = new MaterialSkin.Controls.MaterialButton();
            this.structurePhone = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.structureEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.structureLocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chooseImageBtn = new MaterialSkin.Controls.MaterialButton();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.structureName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.htmlToolTip1 = new MetroFramework.Drawing.Html.HtmlToolTip();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.HotTrack = true;
            this.metroTabControl1.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(581, 626);
            this.metroTabControl1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTabControl1.TabIndex = 2;
            this.metroTabControl1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.AutoScroll = true;
            this.metroTabPage1.Controls.Add(this.structureDescription);
            this.metroTabPage1.Controls.Add(this.label8);
            this.metroTabPage1.Controls.Add(this.label7);
            this.metroTabPage1.Controls.Add(this.saveBtn);
            this.metroTabPage1.Controls.Add(this.structurePhone);
            this.metroTabPage1.Controls.Add(this.label6);
            this.metroTabPage1.Controls.Add(this.structureEmail);
            this.metroTabPage1.Controls.Add(this.label5);
            this.metroTabPage1.Controls.Add(this.structureLocation);
            this.metroTabPage1.Controls.Add(this.label4);
            this.metroTabPage1.Controls.Add(this.chooseImageBtn);
            this.metroTabPage1.Controls.Add(this.logoPictureBox);
            this.metroTabPage1.Controls.Add(this.label3);
            this.metroTabPage1.Controls.Add(this.structureName);
            this.metroTabPage1.Controls.Add(this.label2);
            this.metroTabPage1.Controls.Add(this.label1);
            this.metroTabPage1.HorizontalScrollbar = true;
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(573, 584);
            this.metroTabPage1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Structure";
            this.metroTabPage1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTabPage1.UseStyleColors = true;
            this.metroTabPage1.VerticalScrollbar = true;
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // structureDescription
            // 
            this.structureDescription.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.structureDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.structureDescription.ForeColor = System.Drawing.Color.SteelBlue;
            this.structureDescription.Location = new System.Drawing.Point(166, 471);
            this.structureDescription.Multiline = true;
            this.structureDescription.Name = "structureDescription";
            this.structureDescription.Size = new System.Drawing.Size(351, 49);
            this.structureDescription.TabIndex = 66;
            this.structureDescription.Text = "Mini Structure proposant des solutions digitales aux organisations";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(43, 474);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 20);
            this.label8.TabIndex = 65;
            this.label8.Text = "Téléphone :";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoEllipsis = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Image = global::MasterReport.Properties.Resources.info_green_32px;
            this.label7.Location = new System.Drawing.Point(44, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 30);
            this.label7.TabIndex = 64;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.htmlToolTip1.SetToolTip(this.label7, "Les informations indiquées ici seront utilisées dans les documents générés par le" +
        " logiciel");
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.saveBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.saveBtn.Depth = 0;
            this.saveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.HighEmphasis = true;
            this.saveBtn.Icon = global::MasterReport.Properties.Resources.save_32px;
            this.saveBtn.Location = new System.Drawing.Point(365, 530);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.saveBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.saveBtn.Size = new System.Drawing.Size(152, 36);
            this.saveBtn.TabIndex = 15;
            this.saveBtn.Text = "Sauvegarder";
            this.saveBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.saveBtn.UseAccentColor = false;
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // structurePhone
            // 
            this.structurePhone.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.structurePhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.structurePhone.ForeColor = System.Drawing.Color.SteelBlue;
            this.structurePhone.Location = new System.Drawing.Point(166, 429);
            this.structurePhone.Name = "structurePhone";
            this.structurePhone.Size = new System.Drawing.Size(351, 26);
            this.structurePhone.TabIndex = 14;
            this.structurePhone.Text = "680 70 05 87";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(43, 432);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Téléphone :";
            // 
            // structureEmail
            // 
            this.structureEmail.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.structureEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.structureEmail.ForeColor = System.Drawing.Color.SteelBlue;
            this.structureEmail.Location = new System.Drawing.Point(166, 387);
            this.structureEmail.Name = "structureEmail";
            this.structureEmail.Size = new System.Drawing.Size(351, 26);
            this.structureEmail.TabIndex = 12;
            this.structureEmail.Text = "digitalmakingsoft@gmail.com";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(43, 390);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Email :";
            // 
            // structureLocation
            // 
            this.structureLocation.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.structureLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.structureLocation.ForeColor = System.Drawing.Color.SteelBlue;
            this.structureLocation.Location = new System.Drawing.Point(166, 345);
            this.structureLocation.Name = "structureLocation";
            this.structureLocation.Size = new System.Drawing.Size(351, 26);
            this.structureLocation.TabIndex = 10;
            this.structureLocation.Text = "Nsimeyong - Yaoundé";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(43, 348);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Localisation :";
            // 
            // chooseImageBtn
            // 
            this.chooseImageBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chooseImageBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chooseImageBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.chooseImageBtn.Depth = 0;
            this.chooseImageBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooseImageBtn.HighEmphasis = true;
            this.chooseImageBtn.Icon = global::MasterReport.Properties.Resources.screenshot_32px;
            this.chooseImageBtn.Location = new System.Drawing.Point(389, 220);
            this.chooseImageBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.chooseImageBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.chooseImageBtn.Name = "chooseImageBtn";
            this.chooseImageBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.chooseImageBtn.Size = new System.Drawing.Size(64, 36);
            this.chooseImageBtn.TabIndex = 8;
            this.chooseImageBtn.Text = "+";
            this.chooseImageBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.chooseImageBtn.UseAccentColor = false;
            this.chooseImageBtn.UseVisualStyleBackColor = true;
            this.chooseImageBtn.Click += new System.EventHandler(this.chooseImageBtn_Click);
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.logoPictureBox.Image = global::MasterReport.Properties.Resources.image_120px;
            this.logoPictureBox.Location = new System.Drawing.Point(203, 118);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(179, 138);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 7;
            this.logoPictureBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(264, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Logo";
            // 
            // structureName
            // 
            this.structureName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.structureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.structureName.ForeColor = System.Drawing.Color.SteelBlue;
            this.structureName.Location = new System.Drawing.Point(166, 303);
            this.structureName.Name = "structureName";
            this.structureName.Size = new System.Drawing.Size(351, 26);
            this.structureName.TabIndex = 5;
            this.structureName.Text = "Digital Making Soft";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(43, 306);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nom structure :";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(80, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 36);
            this.label1.TabIndex = 3;
            this.label1.Text = "Informations sur la structure";
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
            // SettingsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroTabControl1);
            this.Name = "SettingsUserControl";
            this.Size = new System.Drawing.Size(581, 626);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox structureName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private MaterialSkin.Controls.MaterialButton chooseImageBtn;
        private System.Windows.Forms.TextBox structureLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox structurePhone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox structureEmail;
        private System.Windows.Forms.Label label5;
        private MaterialSkin.Controls.MaterialButton saveBtn;
        private System.Windows.Forms.Label label7;
        private MetroFramework.Drawing.Html.HtmlToolTip htmlToolTip1;
        private System.Windows.Forms.TextBox structureDescription;
        private System.Windows.Forms.Label label8;
    }
}
