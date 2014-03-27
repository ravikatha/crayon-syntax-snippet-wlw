namespace DC.Crayon.Wlw.Forms
{
	partial class OptionsForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.resetPluginButton = new System.Windows.Forms.Button();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl = new System.Windows.Forms.TabControl();
			this.settingsTabPage = new System.Windows.Forms.TabPage();
			this.panel = new System.Windows.Forms.Panel();
			this.settingsTable = new System.Windows.Forms.TableLayoutPanel();
			this.pluginTabPage = new System.Windows.Forms.TabPage();
			this.updateManagementGroupBox = new System.Windows.Forms.GroupBox();
			this.checkNowButton = new System.Windows.Forms.Button();
			this.lastCheckedAtLabel = new System.Windows.Forms.Label();
			this.lastCheckedAtFieldLabel = new System.Windows.Forms.Label();
			this.includePreReleaseVersionsCheckbox = new System.Windows.Forms.CheckBox();
			this.checkForUpdatesCheckbox = new System.Windows.Forms.CheckBox();
			this.aboutTabPage = new System.Windows.Forms.TabPage();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.productNameLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.tabControl.SuspendLayout();
			this.settingsTabPage.SuspendLayout();
			this.panel.SuspendLayout();
			this.pluginTabPage.SuspendLayout();
			this.updateManagementGroupBox.SuspendLayout();
			this.aboutTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(589, 362);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 9;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OnSave);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(670, 362);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 8;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// resetPluginButton
			// 
			this.resetPluginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.resetPluginButton.Location = new System.Drawing.Point(12, 362);
			this.resetPluginButton.Name = "resetPluginButton";
			this.resetPluginButton.Size = new System.Drawing.Size(125, 23);
			this.resetPluginButton.TabIndex = 7;
			this.resetPluginButton.Text = "&Reset";
			this.resetPluginButton.UseVisualStyleBackColor = true;
			this.resetPluginButton.Click += new System.EventHandler(this.OnReset);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// toolTip
			// 
			this.toolTip.IsBalloon = true;
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.settingsTabPage);
			this.tabControl.Controls.Add(this.pluginTabPage);
			this.tabControl.Controls.Add(this.aboutTabPage);
			this.tabControl.Location = new System.Drawing.Point(12, 12);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(734, 344);
			this.tabControl.TabIndex = 10;
			// 
			// settingsTabPage
			// 
			this.settingsTabPage.Controls.Add(this.panel);
			this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
			this.settingsTabPage.Name = "settingsTabPage";
			this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.settingsTabPage.Size = new System.Drawing.Size(726, 318);
			this.settingsTabPage.TabIndex = 0;
			this.settingsTabPage.Text = "Crayon";
			this.settingsTabPage.UseVisualStyleBackColor = true;
			// 
			// panel
			// 
			this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel.AutoScroll = true;
			this.panel.Controls.Add(this.settingsTable);
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(726, 318);
			this.panel.TabIndex = 7;
			// 
			// settingsTable
			// 
			this.settingsTable.AutoSize = true;
			this.settingsTable.ColumnCount = 2;
			this.settingsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.settingsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.settingsTable.Dock = System.Windows.Forms.DockStyle.Top;
			this.settingsTable.Location = new System.Drawing.Point(0, 0);
			this.settingsTable.Name = "settingsTable";
			this.settingsTable.RowCount = 1;
			this.settingsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.settingsTable.Size = new System.Drawing.Size(726, 0);
			this.settingsTable.TabIndex = 1;
			// 
			// pluginTabPage
			// 
			this.pluginTabPage.Controls.Add(this.updateManagementGroupBox);
			this.pluginTabPage.Location = new System.Drawing.Point(4, 22);
			this.pluginTabPage.Name = "pluginTabPage";
			this.pluginTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.pluginTabPage.Size = new System.Drawing.Size(726, 318);
			this.pluginTabPage.TabIndex = 1;
			this.pluginTabPage.Text = "Plug-in";
			this.pluginTabPage.UseVisualStyleBackColor = true;
			// 
			// updateManagementGroupBox
			// 
			this.updateManagementGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.updateManagementGroupBox.Controls.Add(this.checkNowButton);
			this.updateManagementGroupBox.Controls.Add(this.lastCheckedAtLabel);
			this.updateManagementGroupBox.Controls.Add(this.lastCheckedAtFieldLabel);
			this.updateManagementGroupBox.Controls.Add(this.includePreReleaseVersionsCheckbox);
			this.updateManagementGroupBox.Controls.Add(this.checkForUpdatesCheckbox);
			this.updateManagementGroupBox.Location = new System.Drawing.Point(7, 7);
			this.updateManagementGroupBox.Name = "updateManagementGroupBox";
			this.updateManagementGroupBox.Size = new System.Drawing.Size(713, 76);
			this.updateManagementGroupBox.TabIndex = 0;
			this.updateManagementGroupBox.TabStop = false;
			this.updateManagementGroupBox.Text = "Update Management";
			// 
			// checkNowButton
			// 
			this.checkNowButton.Location = new System.Drawing.Point(601, 22);
			this.checkNowButton.Name = "checkNowButton";
			this.checkNowButton.Size = new System.Drawing.Size(100, 23);
			this.checkNowButton.TabIndex = 4;
			this.checkNowButton.Text = "Check now...";
			this.checkNowButton.UseVisualStyleBackColor = true;
			this.checkNowButton.Click += new System.EventHandler(this.OnCheckUpdates);
			// 
			// lastCheckedAtLabel
			// 
			this.lastCheckedAtLabel.AutoSize = true;
			this.lastCheckedAtLabel.Location = new System.Drawing.Point(233, 45);
			this.lastCheckedAtLabel.Name = "lastCheckedAtLabel";
			this.lastCheckedAtLabel.Size = new System.Drawing.Size(0, 13);
			this.lastCheckedAtLabel.TabIndex = 3;
			// 
			// lastCheckedAtFieldLabel
			// 
			this.lastCheckedAtFieldLabel.AutoSize = true;
			this.lastCheckedAtFieldLabel.Location = new System.Drawing.Point(233, 22);
			this.lastCheckedAtFieldLabel.Name = "lastCheckedAtFieldLabel";
			this.lastCheckedAtFieldLabel.Size = new System.Drawing.Size(84, 13);
			this.lastCheckedAtFieldLabel.TabIndex = 2;
			this.lastCheckedAtFieldLabel.Text = "Last checked at";
			// 
			// includePreReleaseVersionsCheckbox
			// 
			this.includePreReleaseVersionsCheckbox.AutoSize = true;
			this.includePreReleaseVersionsCheckbox.Location = new System.Drawing.Point(16, 45);
			this.includePreReleaseVersionsCheckbox.Name = "includePreReleaseVersionsCheckbox";
			this.includePreReleaseVersionsCheckbox.Size = new System.Drawing.Size(158, 17);
			this.includePreReleaseVersionsCheckbox.TabIndex = 1;
			this.includePreReleaseVersionsCheckbox.Text = "Include pre-release versions";
			this.includePreReleaseVersionsCheckbox.UseVisualStyleBackColor = true;
			// 
			// checkForUpdatesCheckbox
			// 
			this.checkForUpdatesCheckbox.AutoSize = true;
			this.checkForUpdatesCheckbox.Location = new System.Drawing.Point(16, 22);
			this.checkForUpdatesCheckbox.Name = "checkForUpdatesCheckbox";
			this.checkForUpdatesCheckbox.Size = new System.Drawing.Size(160, 17);
			this.checkForUpdatesCheckbox.TabIndex = 0;
			this.checkForUpdatesCheckbox.Text = "Check for updates at startup";
			this.checkForUpdatesCheckbox.UseVisualStyleBackColor = true;
			// 
			// aboutTabPage
			// 
			this.aboutTabPage.Controls.Add(this.linkLabel3);
			this.aboutTabPage.Controls.Add(this.linkLabel2);
			this.aboutTabPage.Controls.Add(this.label4);
			this.aboutTabPage.Controls.Add(this.textBox1);
			this.aboutTabPage.Controls.Add(this.label3);
			this.aboutTabPage.Controls.Add(this.label2);
			this.aboutTabPage.Controls.Add(this.productNameLabel);
			this.aboutTabPage.Location = new System.Drawing.Point(4, 22);
			this.aboutTabPage.Name = "aboutTabPage";
			this.aboutTabPage.Size = new System.Drawing.Size(726, 318);
			this.aboutTabPage.TabIndex = 2;
			this.aboutTabPage.Text = "About";
			this.aboutTabPage.UseVisualStyleBackColor = true;
			// 
			// linkLabel3
			// 
			this.linkLabel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.linkLabel3.Location = new System.Drawing.Point(0, 172);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(726, 15);
			this.linkLabel3.TabIndex = 8;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "blog: http://www.dotcastle.com/blog";
			this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabel3.VisitedLinkColor = System.Drawing.Color.Blue;
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnBlogClick);
			// 
			// linkLabel2
			// 
			this.linkLabel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.linkLabel2.Location = new System.Drawing.Point(0, 157);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(726, 15);
			this.linkLabel2.TabIndex = 7;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "www: http://www.dotcastle.com";
			this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabel2.VisitedLinkColor = System.Drawing.Color.Blue;
			this.linkLabel2.Click += new System.EventHandler(this.OnWebClick);
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(0, 140);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(726, 17);
			this.label4.TabIndex = 5;
			this.label4.Text = "Phone: +91 (40) 40 168 169";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Window;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(0, 92);
			this.textBox1.Margin = new System.Windows.Forms.Padding(0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(726, 48);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "L1, 1-90/7/53/P, Blossom Heights,\r\nPlot 53, Patrika Nagar, Madhapur,\r\nHyderabad, " +
    "Andhra Pradesh 500081 INDIA";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new System.Drawing.Point(0, 82);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(726, 10);
			this.label3.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Location = new System.Drawing.Point(0, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(726, 31);
			this.label2.TabIndex = 1;
			this.label2.Text = "DotCastle TechnoSolutions Private Limited";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// productNameLabel
			// 
			this.productNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.productNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.productNameLabel.Location = new System.Drawing.Point(0, 0);
			this.productNameLabel.Name = "productNameLabel";
			this.productNameLabel.Size = new System.Drawing.Size(726, 51);
			this.productNameLabel.TabIndex = 0;
			this.productNameLabel.Text = "Crayon Code Snippet Plug-in for Windows Live Writer";
			this.productNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(758, 397);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.resetPluginButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OptionsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Crayon Code Snippet Plug-in Options";
			this.Load += new System.EventHandler(this.OnLoad);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.settingsTabPage.ResumeLayout(false);
			this.panel.ResumeLayout(false);
			this.panel.PerformLayout();
			this.pluginTabPage.ResumeLayout(false);
			this.updateManagementGroupBox.ResumeLayout(false);
			this.updateManagementGroupBox.PerformLayout();
			this.aboutTabPage.ResumeLayout(false);
			this.aboutTabPage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button resetPluginButton;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage settingsTabPage;
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.TableLayoutPanel settingsTable;
		private System.Windows.Forms.TabPage pluginTabPage;
		private System.Windows.Forms.TabPage aboutTabPage;
		private System.Windows.Forms.GroupBox updateManagementGroupBox;
		private System.Windows.Forms.CheckBox checkForUpdatesCheckbox;
		private System.Windows.Forms.CheckBox includePreReleaseVersionsCheckbox;
		private System.Windows.Forms.Label lastCheckedAtLabel;
		private System.Windows.Forms.Label lastCheckedAtFieldLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label productNameLabel;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button checkNowButton;
	}
}