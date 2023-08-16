namespace Game_Control_Panel
{
    partial class UserInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            this.GameSettingsLabel = new System.Windows.Forms.Label();
            this.NumberOfLivesLabel = new System.Windows.Forms.Label();
            this.NumberOfLivesComboBox = new System.Windows.Forms.ComboBox();
            this.GameLengthLabel = new System.Windows.Forms.Label();
            this.GameLengthComboBox = new System.Windows.Forms.ComboBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.EStopButton = new System.Windows.Forms.Button();
            this.TimeRemainingLabel = new System.Windows.Forms.Label();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.IndividualGameRadioButton = new System.Windows.Forms.RadioButton();
            this.TeamGameRadioButton = new System.Windows.Forms.RadioButton();
            this.NumberOfTeamsLabel = new System.Windows.Forms.Label();
            this.NumberOfTeamsComboBox = new System.Windows.Forms.ComboBox();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.robot1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.robot2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openHelpFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Scorekeeper = new Game_Control_Panel.ScoreControl();
            this.espanolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameSettingsLabel
            // 
            this.GameSettingsLabel.AutoSize = true;
            this.GameSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameSettingsLabel.Location = new System.Drawing.Point(12, 24);
            this.GameSettingsLabel.Name = "GameSettingsLabel";
            this.GameSettingsLabel.Size = new System.Drawing.Size(157, 26);
            this.GameSettingsLabel.TabIndex = 0;
            this.GameSettingsLabel.Text = "Game Settings";
            // 
            // NumberOfLivesLabel
            // 
            this.NumberOfLivesLabel.AutoSize = true;
            this.NumberOfLivesLabel.Location = new System.Drawing.Point(35, 50);
            this.NumberOfLivesLabel.Name = "NumberOfLivesLabel";
            this.NumberOfLivesLabel.Size = new System.Drawing.Size(89, 13);
            this.NumberOfLivesLabel.TabIndex = 1;
            this.NumberOfLivesLabel.Text = "Number of Lives";
            // 
            // NumberOfLivesComboBox
            // 
            this.NumberOfLivesComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.NumberOfLivesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NumberOfLivesComboBox.FormattingEnabled = true;
            this.NumberOfLivesComboBox.Items.AddRange(new object[] {
            "1 Life",
            "2 Lives",
            "3 Lives",
            "4 Lives",
            "5 Lives",
            "6 Lives"});
            this.NumberOfLivesComboBox.Location = new System.Drawing.Point(38, 66);
            this.NumberOfLivesComboBox.Name = "NumberOfLivesComboBox";
            this.NumberOfLivesComboBox.Size = new System.Drawing.Size(129, 21);
            this.NumberOfLivesComboBox.TabIndex = 2;
            this.NumberOfLivesComboBox.SelectedIndexChanged += new System.EventHandler(this.NumberOfLivesComboBox_SelectedIndexChanged);
            // 
            // GameLengthLabel
            // 
            this.GameLengthLabel.AutoSize = true;
            this.GameLengthLabel.Location = new System.Drawing.Point(35, 90);
            this.GameLengthLabel.Name = "GameLengthLabel";
            this.GameLengthLabel.Size = new System.Drawing.Size(75, 13);
            this.GameLengthLabel.TabIndex = 3;
            this.GameLengthLabel.Text = "Game Length";
            // 
            // GameLengthComboBox
            // 
            this.GameLengthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GameLengthComboBox.FormattingEnabled = true;
            this.GameLengthComboBox.Items.AddRange(new object[] {
            "1 Minute",
            "2 Minutes",
            "3 Minutes",
            "4 Minutes",
            "5 Minutes",
            "6 Minutes",
            "7 Minutes",
            "8 Minutes",
            "9 Minutes",
            "10 Minutes",
            "15 Minutes",
            "20 Minutes",
            "25 Minutes",
            "30 Minutes",
            "35 Minutes",
            "40 Minutes",
            "45 Minutes",
            "50 Minutes",
            "55 Minutes",
            "60 Minutes"});
            this.GameLengthComboBox.Location = new System.Drawing.Point(38, 106);
            this.GameLengthComboBox.Name = "GameLengthComboBox";
            this.GameLengthComboBox.Size = new System.Drawing.Size(129, 21);
            this.GameLengthComboBox.TabIndex = 4;
            this.GameLengthComboBox.SelectedIndexChanged += new System.EventHandler(this.GameLengthComboBox_SelectedIndexChanged);
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(17, 138);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(150, 150);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // EStopButton
            // 
            this.EStopButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EStopButton.Location = new System.Drawing.Point(188, 138);
            this.EStopButton.Name = "EStopButton";
            this.EStopButton.Size = new System.Drawing.Size(150, 150);
            this.EStopButton.TabIndex = 11;
            this.EStopButton.Text = "Emergency Stop";
            this.EStopButton.UseVisualStyleBackColor = true;
            this.EStopButton.Click += new System.EventHandler(this.EStopButton_Click);
            // 
            // TimeRemainingLabel
            // 
            this.TimeRemainingLabel.AutoSize = true;
            this.TimeRemainingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeRemainingLabel.Location = new System.Drawing.Point(12, 317);
            this.TimeRemainingLabel.Name = "TimeRemainingLabel";
            this.TimeRemainingLabel.Size = new System.Drawing.Size(171, 26);
            this.TimeRemainingLabel.TabIndex = 12;
            this.TimeRemainingLabel.Text = "Time Remaining";
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimerLabel.Location = new System.Drawing.Point(12, 342);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(347, 91);
            this.TimerLabel.TabIndex = 13;
            this.TimerLabel.Text = "00:00:00";
            // 
            // IndividualGameRadioButton
            // 
            this.IndividualGameRadioButton.AutoSize = true;
            this.IndividualGameRadioButton.Checked = true;
            this.IndividualGameRadioButton.Location = new System.Drawing.Point(188, 48);
            this.IndividualGameRadioButton.Name = "IndividualGameRadioButton";
            this.IndividualGameRadioButton.Size = new System.Drawing.Size(108, 17);
            this.IndividualGameRadioButton.TabIndex = 6;
            this.IndividualGameRadioButton.TabStop = true;
            this.IndividualGameRadioButton.Text = "Individual Game";
            this.IndividualGameRadioButton.UseVisualStyleBackColor = true;
            this.IndividualGameRadioButton.CheckedChanged += new System.EventHandler(this.TeamRadioButton_CheckedChanged);
            // 
            // TeamGameRadioButton
            // 
            this.TeamGameRadioButton.AutoSize = true;
            this.TeamGameRadioButton.Location = new System.Drawing.Point(188, 67);
            this.TeamGameRadioButton.Name = "TeamGameRadioButton";
            this.TeamGameRadioButton.Size = new System.Drawing.Size(83, 17);
            this.TeamGameRadioButton.TabIndex = 7;
            this.TeamGameRadioButton.Text = "Team Game";
            this.TeamGameRadioButton.UseVisualStyleBackColor = true;
            // 
            // NumberOfTeamsLabel
            // 
            this.NumberOfTeamsLabel.AutoSize = true;
            this.NumberOfTeamsLabel.Location = new System.Drawing.Point(183, 90);
            this.NumberOfTeamsLabel.Name = "NumberOfTeamsLabel";
            this.NumberOfTeamsLabel.Size = new System.Drawing.Size(96, 13);
            this.NumberOfTeamsLabel.TabIndex = 8;
            this.NumberOfTeamsLabel.Text = "Number of Teams";
            this.NumberOfTeamsLabel.Visible = false;
            // 
            // NumberOfTeamsComboBox
            // 
            this.NumberOfTeamsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NumberOfTeamsComboBox.FormattingEnabled = true;
            this.NumberOfTeamsComboBox.Items.AddRange(new object[] {
            "2 Teams",
            "3 Teams",
            "4 Teams"});
            this.NumberOfTeamsComboBox.Location = new System.Drawing.Point(186, 106);
            this.NumberOfTeamsComboBox.Name = "NumberOfTeamsComboBox";
            this.NumberOfTeamsComboBox.Size = new System.Drawing.Size(129, 21);
            this.NumberOfTeamsComboBox.TabIndex = 9;
            this.NumberOfTeamsComboBox.Visible = false;
            this.NumberOfTeamsComboBox.SelectedIndexChanged += new System.EventHandler(this.NumberOfTeamsComboBox_SelectedIndexChanged);
            // 
            // MenuBar
            // 
            this.MenuBar.BackColor = System.Drawing.SystemColors.Control;
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.videoFeedToolStripMenuItem,
            this.languagesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(624, 24);
            this.MenuBar.TabIndex = 15;
            this.MenuBar.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // videoFeedToolStripMenuItem
            // 
            this.videoFeedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.robot1ToolStripMenuItem,
            this.robot2ToolStripMenuItem});
            this.videoFeedToolStripMenuItem.Name = "videoFeedToolStripMenuItem";
            this.videoFeedToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.videoFeedToolStripMenuItem.Text = "Video Feed";
            // 
            // robot1ToolStripMenuItem
            // 
            this.robot1ToolStripMenuItem.Name = "robot1ToolStripMenuItem";
            this.robot1ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.robot1ToolStripMenuItem.Text = "Robot 1";
            this.robot1ToolStripMenuItem.Click += new System.EventHandler(this.robot1ToolStripMenuItem_Click);
            // 
            // robot2ToolStripMenuItem
            // 
            this.robot2ToolStripMenuItem.Name = "robot2ToolStripMenuItem";
            this.robot2ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.robot2ToolStripMenuItem.Text = "Robot 2";
            this.robot2ToolStripMenuItem.Click += new System.EventHandler(this.robot2ToolStripMenuItem_Click);
            // 
            // languagesToolStripMenuItem
            // 
            this.languagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.chineseToolStripMenuItem,
            this.espanolToolStripMenuItem});
            this.languagesToolStripMenuItem.Name = "languagesToolStripMenuItem";
            this.languagesToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.languagesToolStripMenuItem.Text = "Languages";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // chineseToolStripMenuItem
            // 
            this.chineseToolStripMenuItem.Name = "chineseToolStripMenuItem";
            this.chineseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.chineseToolStripMenuItem.Text = "中文";
            this.chineseToolStripMenuItem.Click += new System.EventHandler(this.chineseToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openHelpFileToolStripMenuItem,
            this.viewHelpWebsiteToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // openHelpFileToolStripMenuItem
            // 
            this.openHelpFileToolStripMenuItem.Name = "openHelpFileToolStripMenuItem";
            this.openHelpFileToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.openHelpFileToolStripMenuItem.Text = "Open Help File";
            this.openHelpFileToolStripMenuItem.Click += new System.EventHandler(this.openHelpFileToolStripMenuItem_Click);
            // 
            // viewHelpWebsiteToolStripMenuItem
            // 
            this.viewHelpWebsiteToolStripMenuItem.Name = "viewHelpWebsiteToolStripMenuItem";
            this.viewHelpWebsiteToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.viewHelpWebsiteToolStripMenuItem.Text = "View Help Website";
            this.viewHelpWebsiteToolStripMenuItem.Click += new System.EventHandler(this.viewHelpWebsiteToolStripMenuItem_Click);
            // 
            // Scorekeeper
            // 
            this.Scorekeeper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Scorekeeper.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Scorekeeper.Location = new System.Drawing.Point(350, 12);
            this.Scorekeeper.Name = "Scorekeeper";
            this.Scorekeeper.Size = new System.Drawing.Size(275, 438);
            this.Scorekeeper.TabIndex = 14;
            // 
            // espanolToolStripMenuItem
            // 
            this.espanolToolStripMenuItem.Name = "espanolToolStripMenuItem";
            this.espanolToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.espanolToolStripMenuItem.Text = "Español";
            this.espanolToolStripMenuItem.Click += new System.EventHandler(this.espanolToolStripMenuItem_Click);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.Scorekeeper);
            this.Controls.Add(this.NumberOfTeamsComboBox);
            this.Controls.Add(this.NumberOfTeamsLabel);
            this.Controls.Add(this.TeamGameRadioButton);
            this.Controls.Add(this.IndividualGameRadioButton);
            this.Controls.Add(this.TimerLabel);
            this.Controls.Add(this.TimeRemainingLabel);
            this.Controls.Add(this.EStopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.GameLengthComboBox);
            this.Controls.Add(this.GameLengthLabel);
            this.Controls.Add(this.NumberOfLivesComboBox);
            this.Controls.Add(this.NumberOfLivesLabel);
            this.Controls.Add(this.GameSettingsLabel);
            this.Controls.Add(this.MenuBar);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuBar;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "UserInterface";
            this.Text = "Game Control Panel";
            this.TransparencyKey = System.Drawing.Color.LightPink;
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GameSettingsLabel;
        private System.Windows.Forms.Label NumberOfLivesLabel;
        private System.Windows.Forms.Label GameLengthLabel;
        private System.Windows.Forms.ComboBox GameLengthComboBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button EStopButton;
        private System.Windows.Forms.Label TimeRemainingLabel;
        public System.Windows.Forms.Label TimerLabel;
        private System.Windows.Forms.RadioButton IndividualGameRadioButton;
        private System.Windows.Forms.RadioButton TeamGameRadioButton;
        private System.Windows.Forms.Label NumberOfTeamsLabel;
        private System.Windows.Forms.ComboBox NumberOfTeamsComboBox;
        private System.Windows.Forms.ComboBox NumberOfLivesComboBox;
        private ScoreControl Scorekeeper;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem openHelpFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoFeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem robot1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem robot2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espanolToolStripMenuItem;
    }
}

