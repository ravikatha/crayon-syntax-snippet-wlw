namespace DC.Crayon.Wlw.Forms
{
	partial class SideBarControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.panel = new System.Windows.Forms.Panel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.titleLabel = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.overridesButton = new System.Windows.Forms.Button();
			this.resetButton = new System.Windows.Forms.Button();
			this.updateButton = new System.Windows.Forms.Button();
			this.urlTextBox = new CustomTextBox();
			this.urlFieldLabel = new System.Windows.Forms.Label();
			this.codeTextBox = new CustomTextBox();
			this.codeFieldLabel = new System.Windows.Forms.Label();
			this.markedLinesTextBox = new CustomTextBox();
			this.markedLinesFieldLabel = new System.Windows.Forms.Label();
			this.lineRangeTextBox = new CustomTextBox();
			this.lineRangeFieldLabel = new System.Windows.Forms.Label();
			this.languageComboBox = new System.Windows.Forms.ComboBox();
			this.languageFieldLabel = new System.Windows.Forms.Label();
			this.dontHighlightCheckBox = new System.Windows.Forms.CheckBox();
			this.inlineCheckBox = new System.Windows.Forms.CheckBox();
			this.titleTextBox = new CustomTextBox();
			this.titleFieldLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// panel
			// 
			this.panel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panel.Controls.Add(this.pictureBox);
			this.panel.Controls.Add(this.titleLabel);
			this.panel.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(200, 26);
			this.panel.TabIndex = 19;
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox.Image = global::DC.Crayon.Wlw.Properties.Resources.Icon;
			this.pictureBox.Location = new System.Drawing.Point(5, 5);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(16, 16);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox.TabIndex = 5;
			this.pictureBox.TabStop = false;
			// 
			// titleLabel
			// 
			this.titleLabel.BackColor = System.Drawing.Color.Transparent;
			this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.titleLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.titleLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.titleLabel.Location = new System.Drawing.Point(27, 2);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(170, 20);
			this.titleLabel.TabIndex = 4;
			this.titleLabel.Text = "Crayon Code from DotCastle";
			this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.overridesButton);
			this.panel1.Controls.Add(this.resetButton);
			this.panel1.Controls.Add(this.updateButton);
			this.panel1.Controls.Add(this.urlTextBox);
			this.panel1.Controls.Add(this.urlFieldLabel);
			this.panel1.Controls.Add(this.codeTextBox);
			this.panel1.Controls.Add(this.codeFieldLabel);
			this.panel1.Controls.Add(this.markedLinesTextBox);
			this.panel1.Controls.Add(this.markedLinesFieldLabel);
			this.panel1.Controls.Add(this.lineRangeTextBox);
			this.panel1.Controls.Add(this.lineRangeFieldLabel);
			this.panel1.Controls.Add(this.languageComboBox);
			this.panel1.Controls.Add(this.languageFieldLabel);
			this.panel1.Controls.Add(this.dontHighlightCheckBox);
			this.panel1.Controls.Add(this.inlineCheckBox);
			this.panel1.Controls.Add(this.titleTextBox);
			this.panel1.Controls.Add(this.titleFieldLabel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 26);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 474);
			this.panel1.TabIndex = 20;
			// 
			// overridesButton
			// 
			this.overridesButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.overridesButton.Location = new System.Drawing.Point(13, 442);
			this.overridesButton.Name = "overridesButton";
			this.overridesButton.Size = new System.Drawing.Size(175, 23);
			this.overridesButton.TabIndex = 35;
			this.overridesButton.Text = "Settings...";
			this.overridesButton.UseVisualStyleBackColor = true;
			this.overridesButton.Click += new System.EventHandler(this.OnOverrides);
			// 
			// resetButton
			// 
			this.resetButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.resetButton.Location = new System.Drawing.Point(113, 413);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(75, 23);
			this.resetButton.TabIndex = 34;
			this.resetButton.Text = "Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.OnReset);
			// 
			// updateButton
			// 
			this.updateButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.updateButton.Location = new System.Drawing.Point(13, 413);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(75, 23);
			this.updateButton.TabIndex = 33;
			this.updateButton.Text = "Update";
			this.updateButton.UseVisualStyleBackColor = true;
			this.updateButton.Click += new System.EventHandler(this.OnUpdate);
			// 
			// urlTextBox
			// 
			this.urlTextBox.Location = new System.Drawing.Point(13, 384);
			this.urlTextBox.Name = "urlTextBox";
			this.urlTextBox.Size = new System.Drawing.Size(175, 23);
			this.urlTextBox.TabIndex = 32;
			// 
			// urlFieldLabel
			// 
			this.urlFieldLabel.AutoSize = true;
			this.urlFieldLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.urlFieldLabel.Location = new System.Drawing.Point(10, 365);
			this.urlFieldLabel.Name = "urlFieldLabel";
			this.urlFieldLabel.Size = new System.Drawing.Size(22, 15);
			this.urlFieldLabel.TabIndex = 31;
			this.urlFieldLabel.Text = "Url";
			// 
			// codeTextBox
			// 
			this.codeTextBox.Location = new System.Drawing.Point(13, 213);
			this.codeTextBox.Multiline = true;
			this.codeTextBox.Name = "codeTextBox";
			this.codeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.codeTextBox.Size = new System.Drawing.Size(175, 143);
			this.codeTextBox.TabIndex = 30;
			this.codeTextBox.WordWrap = false;
			// 
			// codeFieldLabel
			// 
			this.codeFieldLabel.AutoSize = true;
			this.codeFieldLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.codeFieldLabel.Location = new System.Drawing.Point(10, 194);
			this.codeFieldLabel.Name = "codeFieldLabel";
			this.codeFieldLabel.Size = new System.Drawing.Size(35, 15);
			this.codeFieldLabel.TabIndex = 29;
			this.codeFieldLabel.Text = "Code";
			// 
			// markedLinesTextBox
			// 
			this.markedLinesTextBox.Location = new System.Drawing.Point(104, 159);
			this.markedLinesTextBox.Name = "markedLinesTextBox";
			this.markedLinesTextBox.Size = new System.Drawing.Size(84, 23);
			this.markedLinesTextBox.TabIndex = 28;
			// 
			// markedLinesFieldLabel
			// 
			this.markedLinesFieldLabel.AutoSize = true;
			this.markedLinesFieldLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.markedLinesFieldLabel.Location = new System.Drawing.Point(101, 140);
			this.markedLinesFieldLabel.Name = "markedLinesFieldLabel";
			this.markedLinesFieldLabel.Size = new System.Drawing.Size(74, 15);
			this.markedLinesFieldLabel.TabIndex = 27;
			this.markedLinesFieldLabel.Text = "Marked lines";
			// 
			// lineRangeTextBox
			// 
			this.lineRangeTextBox.Location = new System.Drawing.Point(13, 159);
			this.lineRangeTextBox.Name = "lineRangeTextBox";
			this.lineRangeTextBox.Size = new System.Drawing.Size(84, 23);
			this.lineRangeTextBox.TabIndex = 26;
			// 
			// lineRangeFieldLabel
			// 
			this.lineRangeFieldLabel.AutoSize = true;
			this.lineRangeFieldLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lineRangeFieldLabel.Location = new System.Drawing.Point(10, 140);
			this.lineRangeFieldLabel.Name = "lineRangeFieldLabel";
			this.lineRangeFieldLabel.Size = new System.Drawing.Size(62, 15);
			this.lineRangeFieldLabel.TabIndex = 25;
			this.lineRangeFieldLabel.Text = "Line range";
			// 
			// languageComboBox
			// 
			this.languageComboBox.DisplayMember = "Text";
			this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.languageComboBox.FormattingEnabled = true;
			this.languageComboBox.Location = new System.Drawing.Point(13, 106);
			this.languageComboBox.Name = "languageComboBox";
			this.languageComboBox.Size = new System.Drawing.Size(175, 23);
			this.languageComboBox.TabIndex = 24;
			this.languageComboBox.ValueMember = "Value";
			// 
			// languageFieldLabel
			// 
			this.languageFieldLabel.AutoSize = true;
			this.languageFieldLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.languageFieldLabel.Location = new System.Drawing.Point(10, 87);
			this.languageFieldLabel.Name = "languageFieldLabel";
			this.languageFieldLabel.Size = new System.Drawing.Size(59, 15);
			this.languageFieldLabel.TabIndex = 23;
			this.languageFieldLabel.Text = "Language";
			// 
			// dontHighlightCheckBox
			// 
			this.dontHighlightCheckBox.AutoSize = true;
			this.dontHighlightCheckBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dontHighlightCheckBox.Location = new System.Drawing.Point(89, 60);
			this.dontHighlightCheckBox.Name = "dontHighlightCheckBox";
			this.dontHighlightCheckBox.Size = new System.Drawing.Size(106, 19);
			this.dontHighlightCheckBox.TabIndex = 22;
			this.dontHighlightCheckBox.Text = "Don\'t highlight";
			this.dontHighlightCheckBox.UseVisualStyleBackColor = true;
			// 
			// inlineCheckBox
			// 
			this.inlineCheckBox.AutoSize = true;
			this.inlineCheckBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.inlineCheckBox.Location = new System.Drawing.Point(13, 60);
			this.inlineCheckBox.Name = "inlineCheckBox";
			this.inlineCheckBox.Size = new System.Drawing.Size(55, 19);
			this.inlineCheckBox.TabIndex = 21;
			this.inlineCheckBox.Text = "Inline";
			this.inlineCheckBox.UseVisualStyleBackColor = true;
			// 
			// titleTextBox
			// 
			this.titleTextBox.Location = new System.Drawing.Point(13, 30);
			this.titleTextBox.Name = "titleTextBox";
			this.titleTextBox.Size = new System.Drawing.Size(175, 23);
			this.titleTextBox.TabIndex = 20;
			// 
			// titleFieldLabel
			// 
			this.titleFieldLabel.AutoSize = true;
			this.titleFieldLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.titleFieldLabel.Location = new System.Drawing.Point(10, 11);
			this.titleFieldLabel.Name = "titleFieldLabel";
			this.titleFieldLabel.Size = new System.Drawing.Size(30, 15);
			this.titleFieldLabel.TabIndex = 19;
			this.titleFieldLabel.Text = "Title";
			// 
			// SideBarControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel);
			this.Name = "SideBarControl";
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.panel.ResumeLayout(false);
			this.panel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Label titleLabel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button overridesButton;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.Button updateButton;
		private CustomTextBox urlTextBox;
		private System.Windows.Forms.Label urlFieldLabel;
		private CustomTextBox codeTextBox;
		private System.Windows.Forms.Label codeFieldLabel;
		private CustomTextBox markedLinesTextBox;
		private System.Windows.Forms.Label markedLinesFieldLabel;
		private CustomTextBox lineRangeTextBox;
		private System.Windows.Forms.Label lineRangeFieldLabel;
		private System.Windows.Forms.ComboBox languageComboBox;
		private System.Windows.Forms.Label languageFieldLabel;
		private System.Windows.Forms.CheckBox dontHighlightCheckBox;
		private System.Windows.Forms.CheckBox inlineCheckBox;
		private CustomTextBox titleTextBox;
		private System.Windows.Forms.Label titleFieldLabel;


	}
}
