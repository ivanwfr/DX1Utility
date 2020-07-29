namespace DX1Utility
{
    partial class DX1Utility
    {
        public const string APP_WINDOW_TITLE    = "DX1Utility";

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
            if(disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code //{{{

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.D_Profiles = new System.Windows.Forms.ComboBox();
            this.B_EditProfile = new System.Windows.Forms.Button();
            this.B_Delete = new System.Windows.Forms.Button();
            this.B_ShutDown = new System.Windows.Forms.Button();
            this.B_MACRO_EDIT = new System.Windows.Forms.Button();
            this.C_Debug = new System.Windows.Forms.CheckBox();
            this.B_KeyProgrammer = new System.Windows.Forms.Button();
            this.T_LOG = new System.Windows.Forms.RichTextBox();
            this.G_RB_MENU = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.G_RBMenu_Wizard = new System.Windows.Forms.ToolStripMenuItem();
            this.G_RBMenu_Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.G_RBMenu_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.G_DX1_KEYS = new System.Windows.Forms.DataGridView();
            this.MacroList = new System.Windows.Forms.ListBox();
            this.T_DXKey = new System.Windows.Forms.TextBox();
            this.L_PROFILES = new System.Windows.Forms.Label();
            this.R_False = new System.Windows.Forms.RadioButton();
            this.R_True = new System.Windows.Forms.RadioButton();
            this.G_Special = new System.Windows.Forms.DataGridView();
            this.L_MultiKey = new System.Windows.Forms.ListBox();
            this.R_Special = new System.Windows.Forms.RadioButton();
            this.R_Macro = new System.Windows.Forms.RadioButton();
            this.R_Multi = new System.Windows.Forms.RadioButton();
            this.B_Test = new System.Windows.Forms.Button();
            this.C1_Profile_commit = new System.Windows.Forms.CheckBox();
            this.C2_KeyMap_commit = new System.Windows.Forms.CheckBox();
            this.B_Right_Button = new System.Windows.Forms.Label();
            this.T_Conf_Code = new System.Windows.Forms.TextBox();
            this.T_Conf_Type = new System.Windows.Forms.TextBox();
            this.L_CONF_CODE = new System.Windows.Forms.Label();
            this.L_CONF_KEYTYPE = new System.Windows.Forms.Label();
            this.T_SingleKey = new System.Windows.Forms.TextBox();
            this.R_Single = new System.Windows.Forms.RadioButton();
            this.T_Description = new System.Windows.Forms.TextBox();
            this.L_STATUS = new System.Windows.Forms.Label();
            this.P_border_U = new System.Windows.Forms.Panel();
            this.P_border_R = new System.Windows.Forms.Panel();
            this.P_border_D = new System.Windows.Forms.Panel();
            this.P_border_L = new System.Windows.Forms.Panel();
            this.C4_Exit_onClose = new System.Windows.Forms.CheckBox();
            this.L1_Profile_commit_behind = new System.Windows.Forms.Label();
            this.L2_KeyMap_commit_behind = new System.Windows.Forms.Label();
            this.L3_Macro_onClose_behind = new System.Windows.Forms.Label();
            this.L4_Exit_onClose_behind = new System.Windows.Forms.Label();
            this.C3_Macro_onClose = new System.Windows.Forms.CheckBox();
            this.L_Logo = new System.Windows.Forms.Label();
            this.L1_Profile_commit = new System.Windows.Forms.Label();
            this.L2_KeyMap_commit = new System.Windows.Forms.Label();
            this.L3_Macro_onClose = new System.Windows.Forms.Label();
            this.L4_Exit_onClose = new System.Windows.Forms.Label();
            this.L_ON = new System.Windows.Forms.Label();
            this.L_REC = new System.Windows.Forms.Label();
            this.L_WRITING = new System.Windows.Forms.Label();
            this.L_STANDBY = new System.Windows.Forms.Label();
            this.G_RB_MENU.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.G_DX1_KEYS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.G_Special)).BeginInit();
            this.SuspendLayout();
            // 
            // D_Profiles
            // 
            this.D_Profiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.D_Profiles.FormattingEnabled = true;
            this.D_Profiles.Location = new System.Drawing.Point(87, 177);
            this.D_Profiles.Name = "D_Profiles";
            this.D_Profiles.Size = new System.Drawing.Size(170, 21);
            this.D_Profiles.Sorted = true;
            this.D_Profiles.TabIndex = 9;
            this.D_Profiles.SelectionChangeCommitted += new System.EventHandler(this.D_ProfilesCB);
            this.D_Profiles.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // B_EditProfile
            // 
            this.B_EditProfile.BackColor = System.Drawing.SystemColors.Control;
            this.B_EditProfile.Location = new System.Drawing.Point(270, 176);
            this.B_EditProfile.Margin = new System.Windows.Forms.Padding(0);
            this.B_EditProfile.Name = "B_EditProfile";
            this.B_EditProfile.Size = new System.Drawing.Size(99, 23);
            this.B_EditProfile.TabIndex = 11;
            this.B_EditProfile.Text = "Edit";
            this.B_EditProfile.UseVisualStyleBackColor = false;
            this.B_EditProfile.Click += new System.EventHandler(this.B_EditProfile_CB);
            this.B_EditProfile.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // B_Delete
            // 
            this.B_Delete.BackColor = System.Drawing.SystemColors.Control;
            this.B_Delete.Location = new System.Drawing.Point(375, 176);
            this.B_Delete.Margin = new System.Windows.Forms.Padding(0);
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.Size = new System.Drawing.Size(98, 23);
            this.B_Delete.TabIndex = 14;
            this.B_Delete.Text = "Delete";
            this.B_Delete.UseVisualStyleBackColor = false;
            this.B_Delete.Click += new System.EventHandler(this.B_Delete_CB);
            this.B_Delete.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // B_ShutDown
            // 
            this.B_ShutDown.BackColor = System.Drawing.SystemColors.Control;
            this.B_ShutDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B_ShutDown.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.B_ShutDown.FlatAppearance.BorderSize = 3;
            this.B_ShutDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.B_ShutDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B_ShutDown.Location = new System.Drawing.Point(616, 9);
            this.B_ShutDown.Margin = new System.Windows.Forms.Padding(0);
            this.B_ShutDown.Name = "B_ShutDown";
            this.B_ShutDown.Size = new System.Drawing.Size(128, 128);
            this.B_ShutDown.TabIndex = 15;
            this.B_ShutDown.Text = "Shut down\r\nDX1 Utility";
            this.B_ShutDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.B_ShutDown.UseVisualStyleBackColor = false;
            this.B_ShutDown.Click += new System.EventHandler(this.B_ShutDown_CB);
            this.B_ShutDown.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // B_MACRO_EDIT
            // 
            this.B_MACRO_EDIT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.B_MACRO_EDIT.BackColor = System.Drawing.SystemColors.Control;
            this.B_MACRO_EDIT.Location = new System.Drawing.Point(370, 559);
            this.B_MACRO_EDIT.Margin = new System.Windows.Forms.Padding(0);
            this.B_MACRO_EDIT.Name = "B_MACRO_EDIT";
            this.B_MACRO_EDIT.Size = new System.Drawing.Size(112, 23);
            this.B_MACRO_EDIT.TabIndex = 7;
            this.B_MACRO_EDIT.Text = "Edit Macro";
            this.B_MACRO_EDIT.UseVisualStyleBackColor = false;
            this.B_MACRO_EDIT.Click += new System.EventHandler(this.B_EditMacro_CB);
            this.B_MACRO_EDIT.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // C_Debug
            // 
            this.C_Debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.C_Debug.AutoSize = true;
            this.C_Debug.BackColor = System.Drawing.SystemColors.Control;
            this.C_Debug.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.C_Debug.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.C_Debug.Location = new System.Drawing.Point(681, 660);
            this.C_Debug.Name = "C_Debug";
            this.C_Debug.Size = new System.Drawing.Size(63, 19);
            this.C_Debug.TabIndex = 13;
            this.C_Debug.Text = "Debug";
            this.C_Debug.UseVisualStyleBackColor = false;
            this.C_Debug.CheckedChanged += new System.EventHandler(this.C_Debug_CB);
            this.C_Debug.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // B_KeyProgrammer
            // 
            this.B_KeyProgrammer.BackColor = System.Drawing.SystemColors.Control;
            this.B_KeyProgrammer.Location = new System.Drawing.Point(539, 232);
            this.B_KeyProgrammer.Margin = new System.Windows.Forms.Padding(0);
            this.B_KeyProgrammer.Name = "B_KeyProgrammer";
            this.B_KeyProgrammer.Size = new System.Drawing.Size(128, 128);
            this.B_KeyProgrammer.TabIndex = 0;
            this.B_KeyProgrammer.TabStop = false;
            this.B_KeyProgrammer.Text = "Map DX1 key";
            this.B_KeyProgrammer.UseVisualStyleBackColor = false;
            this.B_KeyProgrammer.Click += new System.EventHandler(this.B_KeyProgrammer_CB);
            this.B_KeyProgrammer.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // T_LOG
            // 
            this.T_LOG.AccessibleDescription = "Processing report";
            this.T_LOG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_LOG.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.T_LOG.Font = new System.Drawing.Font("Noto Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T_LOG.ForeColor = System.Drawing.SystemColors.ControlText;
            this.T_LOG.Location = new System.Drawing.Point(267, 682);
            this.T_LOG.Name = "T_LOG";
            this.T_LOG.ReadOnly = true;
            this.T_LOG.ShortcutsEnabled = false;
            this.T_LOG.Size = new System.Drawing.Size(477, 553);
            this.T_LOG.TabIndex = 17;
            this.T_LOG.Text = "";
            this.T_LOG.KeyUp += new System.Windows.Forms.KeyEventHandler(this.F_KeyUp_CB);
            this.T_LOG.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // G_RB_MENU
            // 
            this.G_RB_MENU.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.G_RBMenu_Wizard,
            this.G_RBMenu_Properties,
            this.G_RBMenu_Clear});
            this.G_RB_MENU.Name = "G_RB_MENU";
            this.G_RB_MENU.ShowImageMargin = false;
            this.G_RB_MENU.Size = new System.Drawing.Size(111, 70);
            // 
            // G_RBMenu_Wizard
            // 
            this.G_RBMenu_Wizard.Name = "G_RBMenu_Wizard";
            this.G_RBMenu_Wizard.Size = new System.Drawing.Size(110, 22);
            this.G_RBMenu_Wizard.Text = "Wizard";
            this.G_RBMenu_Wizard.Click += new System.EventHandler(this.G_RBMenu_Wizard_CB);
            // 
            // G_RBMenu_Properties
            // 
            this.G_RBMenu_Properties.Name = "G_RBMenu_Properties";
            this.G_RBMenu_Properties.Size = new System.Drawing.Size(110, 22);
            this.G_RBMenu_Properties.Text = "Properties";
            this.G_RBMenu_Properties.Click += new System.EventHandler(this.G_RBMenu_Properties_CB);
            // 
            // G_RBMenu_Clear
            // 
            this.G_RBMenu_Clear.Name = "G_RBMenu_Clear";
            this.G_RBMenu_Clear.Size = new System.Drawing.Size(110, 22);
            this.G_RBMenu_Clear.Text = "Unassigned";
            this.G_RBMenu_Clear.Click += new System.EventHandler(this.G_RBMenu_Clear_CB);
            // 
            // G_DX1_KEYS
            // 
            this.G_DX1_KEYS.AccessibleDescription = "List of 50 DX1 keys";
            this.G_DX1_KEYS.AllowUserToAddRows = false;
            this.G_DX1_KEYS.AllowUserToDeleteRows = false;
            this.G_DX1_KEYS.AllowUserToResizeColumns = false;
            this.G_DX1_KEYS.AllowUserToResizeRows = false;
            this.G_DX1_KEYS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.G_DX1_KEYS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.G_DX1_KEYS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.G_DX1_KEYS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.G_DX1_KEYS.ContextMenuStrip = this.G_RB_MENU;
            this.G_DX1_KEYS.Location = new System.Drawing.Point(7, 208);
            this.G_DX1_KEYS.Margin = new System.Windows.Forms.Padding(0);
            this.G_DX1_KEYS.MultiSelect = false;
            this.G_DX1_KEYS.Name = "G_DX1_KEYS";
            this.G_DX1_KEYS.RowHeadersVisible = false;
            this.G_DX1_KEYS.RowHeadersWidth = 30;
            this.G_DX1_KEYS.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.G_DX1_KEYS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.G_DX1_KEYS.Size = new System.Drawing.Size(253, 1027);
            this.G_DX1_KEYS.TabIndex = 16;
            this.G_DX1_KEYS.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.G_CellFormatting_CB);
            this.G_DX1_KEYS.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.G_CellMouseDown_CB);
            this.G_DX1_KEYS.KeyUp += new System.Windows.Forms.KeyEventHandler(this.F_KeyUp_CB);
            this.G_DX1_KEYS.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // MacroList
            // 
            this.MacroList.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.MacroList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MacroList.FormattingEnabled = true;
            this.MacroList.Location = new System.Drawing.Point(270, 588);
            this.MacroList.Name = "MacroList";
            this.MacroList.Size = new System.Drawing.Size(215, 69);
            this.MacroList.TabIndex = 8;
            this.MacroList.TabStop = false;
            this.MacroList.SelectedIndexChanged += new System.EventHandler(this.MacroList_CB);
            this.MacroList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.F_KeyUp_CB);
            this.MacroList.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // T_DXKey
            // 
            this.T_DXKey.Location = new System.Drawing.Point(0, 0);
            this.T_DXKey.Name = "T_DXKey";
            this.T_DXKey.Size = new System.Drawing.Size(100, 20);
            this.T_DXKey.TabIndex = 0;
            // 
            // L_PROFILES
            // 
            this.L_PROFILES.AutoSize = true;
            this.L_PROFILES.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_PROFILES.Location = new System.Drawing.Point(3, 174);
            this.L_PROFILES.Name = "L_PROFILES";
            this.L_PROFILES.Size = new System.Drawing.Size(65, 20);
            this.L_PROFILES.TabIndex = 10;
            this.L_PROFILES.Text = "Profile:";
            this.L_PROFILES.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // R_False
            // 
            this.R_False.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.R_False.Location = new System.Drawing.Point(431, 522);
            this.R_False.Name = "R_False";
            this.R_False.Size = new System.Drawing.Size(50, 17);
            this.R_False.TabIndex = 45;
            this.R_False.Text = "False";
            this.R_False.UseVisualStyleBackColor = true;
            this.R_False.Visible = false;
            this.R_False.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // R_True
            // 
            this.R_True.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.R_True.Checked = true;
            this.R_True.Location = new System.Drawing.Point(383, 522);
            this.R_True.Name = "R_True";
            this.R_True.Size = new System.Drawing.Size(47, 17);
            this.R_True.TabIndex = 44;
            this.R_True.TabStop = true;
            this.R_True.Text = "True";
            this.R_True.UseVisualStyleBackColor = true;
            this.R_True.Visible = false;
            this.R_True.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // G_Special
            // 
            this.G_Special.AllowUserToAddRows = false;
            this.G_Special.AllowUserToDeleteRows = false;
            this.G_Special.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.G_Special.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.G_Special.Location = new System.Drawing.Point(270, 433);
            this.G_Special.MultiSelect = false;
            this.G_Special.Name = "G_Special";
            this.G_Special.ReadOnly = true;
            this.G_Special.RowHeadersVisible = false;
            this.G_Special.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.G_Special.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.G_Special.Size = new System.Drawing.Size(216, 84);
            this.G_Special.TabIndex = 43;
            this.G_Special.TabStop = false;
            this.G_Special.KeyUp += new System.Windows.Forms.KeyEventHandler(this.F_KeyUp_CB);
            this.G_Special.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L_MultiKey
            // 
            this.L_MultiKey.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.L_MultiKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.L_MultiKey.FormattingEnabled = true;
            this.L_MultiKey.Location = new System.Drawing.Point(550, 435);
            this.L_MultiKey.Name = "L_MultiKey";
            this.L_MultiKey.Size = new System.Drawing.Size(194, 82);
            this.L_MultiKey.TabIndex = 41;
            this.L_MultiKey.TabStop = false;
            this.L_MultiKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.F_KeyUp_CB);
            this.L_MultiKey.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // R_Special
            // 
            this.R_Special.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.R_Special.BackColor = System.Drawing.SystemColors.Control;
            this.R_Special.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.R_Special.Location = new System.Drawing.Point(270, 407);
            this.R_Special.Name = "R_Special";
            this.R_Special.Size = new System.Drawing.Size(93, 20);
            this.R_Special.TabIndex = 37;
            this.R_Special.Text = "Special Key";
            this.R_Special.UseVisualStyleBackColor = false;
            this.R_Special.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // R_Macro
            // 
            this.R_Macro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.R_Macro.BackColor = System.Drawing.SystemColors.Control;
            this.R_Macro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.R_Macro.Location = new System.Drawing.Point(270, 559);
            this.R_Macro.Name = "R_Macro";
            this.R_Macro.Size = new System.Drawing.Size(93, 23);
            this.R_Macro.TabIndex = 36;
            this.R_Macro.Text = "Macro";
            this.R_Macro.UseVisualStyleBackColor = false;
            this.R_Macro.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // R_Multi
            // 
            this.R_Multi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.R_Multi.AutoSize = true;
            this.R_Multi.BackColor = System.Drawing.SystemColors.Control;
            this.R_Multi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.R_Multi.Location = new System.Drawing.Point(553, 404);
            this.R_Multi.Name = "R_Multi";
            this.R_Multi.Size = new System.Drawing.Size(100, 24);
            this.R_Multi.TabIndex = 35;
            this.R_Multi.Text = "Multi-Key";
            this.R_Multi.UseVisualStyleBackColor = false;
            this.R_Multi.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // B_Test
            // 
            this.B_Test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.B_Test.Location = new System.Drawing.Point(550, 586);
            this.B_Test.Margin = new System.Windows.Forms.Padding(0);
            this.B_Test.Name = "B_Test";
            this.B_Test.Size = new System.Drawing.Size(194, 71);
            this.B_Test.TabIndex = 55;
            this.B_Test.Text = "Send keyMapList Sample";
            this.B_Test.UseVisualStyleBackColor = true;
            this.B_Test.Click += new System.EventHandler(this.B_Test_CB);
            this.B_Test.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // C1_Profile_commit
            // 
            this.C1_Profile_commit.AccessibleDescription = "Update file|(on Exit or Focus Lost)";
            this.C1_Profile_commit.Appearance = System.Windows.Forms.Appearance.Button;
            this.C1_Profile_commit.BackColor = System.Drawing.Color.Transparent;
            this.C1_Profile_commit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.C1_Profile_commit.Enabled = false;
            this.C1_Profile_commit.FlatAppearance.BorderSize = 0;
            this.C1_Profile_commit.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.C1_Profile_commit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.C1_Profile_commit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.C1_Profile_commit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.C1_Profile_commit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1_Profile_commit.ForeColor = System.Drawing.Color.Black;
            this.C1_Profile_commit.Location = new System.Drawing.Point(61, 140);
            this.C1_Profile_commit.Margin = new System.Windows.Forms.Padding(0);
            this.C1_Profile_commit.Name = "C1_Profile_commit";
            this.C1_Profile_commit.Size = new System.Drawing.Size(128, 28);
            this.C1_Profile_commit.TabIndex = 59;
            this.C1_Profile_commit.Text = "Save Profile";
            this.C1_Profile_commit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C1_Profile_commit.UseVisualStyleBackColor = false;
            this.C1_Profile_commit.Click += new System.EventHandler(this.C1_Profile_commit_CB);
            this.C1_Profile_commit.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // C2_KeyMap_commit
            // 
            this.C2_KeyMap_commit.AccessibleDescription = "Update|(on Exit or Focus Lost)";
            this.C2_KeyMap_commit.Appearance = System.Windows.Forms.Appearance.Button;
            this.C2_KeyMap_commit.BackColor = System.Drawing.Color.Transparent;
            this.C2_KeyMap_commit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.C2_KeyMap_commit.Enabled = false;
            this.C2_KeyMap_commit.FlatAppearance.BorderSize = 0;
            this.C2_KeyMap_commit.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.C2_KeyMap_commit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.C2_KeyMap_commit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.C2_KeyMap_commit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.C2_KeyMap_commit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C2_KeyMap_commit.ForeColor = System.Drawing.Color.Black;
            this.C2_KeyMap_commit.Location = new System.Drawing.Point(200, 140);
            this.C2_KeyMap_commit.Margin = new System.Windows.Forms.Padding(0);
            this.C2_KeyMap_commit.Name = "C2_KeyMap_commit";
            this.C2_KeyMap_commit.Size = new System.Drawing.Size(128, 28);
            this.C2_KeyMap_commit.TabIndex = 60;
            this.C2_KeyMap_commit.Text = "Key Mappings";
            this.C2_KeyMap_commit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C2_KeyMap_commit.UseVisualStyleBackColor = false;
            this.C2_KeyMap_commit.Click += new System.EventHandler(this.C2_KeyMap_commit_CB);
            this.C2_KeyMap_commit.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // B_Right_Button
            // 
            this.B_Right_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.B_Right_Button.BackColor = System.Drawing.SystemColors.Control;
            this.B_Right_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B_Right_Button.ForeColor = System.Drawing.Color.Black;
            this.B_Right_Button.Location = new System.Drawing.Point(270, 520);
            this.B_Right_Button.Name = "B_Right_Button";
            this.B_Right_Button.Size = new System.Drawing.Size(111, 20);
            this.B_Right_Button.TabIndex = 61;
            this.B_Right_Button.Text = "Right Button";
            this.B_Right_Button.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // T_Conf_Code
            // 
            this.T_Conf_Code.BackColor = System.Drawing.SystemColors.Control;
            this.T_Conf_Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.T_Conf_Code.Enabled = false;
            this.T_Conf_Code.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T_Conf_Code.Location = new System.Drawing.Point(307, 368);
            this.T_Conf_Code.Name = "T_Conf_Code";
            this.T_Conf_Code.ReadOnly = true;
            this.T_Conf_Code.Size = new System.Drawing.Size(164, 20);
            this.T_Conf_Code.TabIndex = 53;
            this.T_Conf_Code.TabStop = false;
            this.T_Conf_Code.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // T_Conf_Type
            // 
            this.T_Conf_Type.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.T_Conf_Type.BackColor = System.Drawing.SystemColors.Control;
            this.T_Conf_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.T_Conf_Type.Enabled = false;
            this.T_Conf_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T_Conf_Type.Location = new System.Drawing.Point(307, 340);
            this.T_Conf_Type.Name = "T_Conf_Type";
            this.T_Conf_Type.ReadOnly = true;
            this.T_Conf_Type.Size = new System.Drawing.Size(164, 20);
            this.T_Conf_Type.TabIndex = 49;
            this.T_Conf_Type.TabStop = false;
            this.T_Conf_Type.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L_CONF_CODE
            // 
            this.L_CONF_CODE.BackColor = System.Drawing.SystemColors.Control;
            this.L_CONF_CODE.Location = new System.Drawing.Point(270, 368);
            this.L_CONF_CODE.Name = "L_CONF_CODE";
            this.L_CONF_CODE.Size = new System.Drawing.Size(32, 13);
            this.L_CONF_CODE.TabIndex = 52;
            this.L_CONF_CODE.Text = "Code";
            this.L_CONF_CODE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.L_CONF_CODE.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L_CONF_KEYTYPE
            // 
            this.L_CONF_KEYTYPE.BackColor = System.Drawing.SystemColors.Control;
            this.L_CONF_KEYTYPE.Location = new System.Drawing.Point(270, 342);
            this.L_CONF_KEYTYPE.Name = "L_CONF_KEYTYPE";
            this.L_CONF_KEYTYPE.Size = new System.Drawing.Size(31, 13);
            this.L_CONF_KEYTYPE.TabIndex = 48;
            this.L_CONF_KEYTYPE.Text = "Type";
            this.L_CONF_KEYTYPE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.L_CONF_KEYTYPE.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // T_SingleKey
            // 
            this.T_SingleKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.T_SingleKey.Enabled = false;
            this.T_SingleKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T_SingleKey.Location = new System.Drawing.Point(270, 240);
            this.T_SingleKey.Name = "T_SingleKey";
            this.T_SingleKey.ReadOnly = true;
            this.T_SingleKey.Size = new System.Drawing.Size(203, 44);
            this.T_SingleKey.TabIndex = 39;
            this.T_SingleKey.TabStop = false;
            this.T_SingleKey.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // R_Single
            // 
            this.R_Single.BackColor = System.Drawing.SystemColors.Control;
            this.R_Single.Checked = true;
            this.R_Single.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.R_Single.Location = new System.Drawing.Point(270, 208);
            this.R_Single.Name = "R_Single";
            this.R_Single.Size = new System.Drawing.Size(115, 26);
            this.R_Single.TabIndex = 34;
            this.R_Single.TabStop = true;
            this.R_Single.Text = "Single Key";
            this.R_Single.UseVisualStyleBackColor = false;
            this.R_Single.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // T_Description
            // 
            this.T_Description.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.T_Description.Enabled = false;
            this.T_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T_Description.Location = new System.Drawing.Point(270, 290);
            this.T_Description.Name = "T_Description";
            this.T_Description.ReadOnly = true;
            this.T_Description.Size = new System.Drawing.Size(203, 44);
            this.T_Description.TabIndex = 47;
            this.T_Description.TabStop = false;
            this.T_Description.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L_STATUS
            // 
            this.L_STATUS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.L_STATUS.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_STATUS.ForeColor = System.Drawing.Color.Black;
            this.L_STATUS.Location = new System.Drawing.Point(7, 1244);
            this.L_STATUS.Name = "L_STATUS";
            this.L_STATUS.Size = new System.Drawing.Size(737, 21);
            this.L_STATUS.TabIndex = 65;
            this.L_STATUS.Text = "status";
            this.L_STATUS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // P_border_U
            // 
            this.P_border_U.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.P_border_U.Location = new System.Drawing.Point(0, 0);
            this.P_border_U.Margin = new System.Windows.Forms.Padding(0);
            this.P_border_U.Name = "P_border_U";
            this.P_border_U.Size = new System.Drawing.Size(756, 3);
            this.P_border_U.TabIndex = 68;
            // 
            // P_border_R
            // 
            this.P_border_R.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.P_border_R.BackColor = System.Drawing.SystemColors.Control;
            this.P_border_R.Location = new System.Drawing.Point(753, 0);
            this.P_border_R.Margin = new System.Windows.Forms.Padding(0);
            this.P_border_R.Name = "P_border_R";
            this.P_border_R.Size = new System.Drawing.Size(3, 1268);
            this.P_border_R.TabIndex = 67;
            // 
            // P_border_D
            // 
            this.P_border_D.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.P_border_D.Location = new System.Drawing.Point(0, 1265);
            this.P_border_D.Margin = new System.Windows.Forms.Padding(0);
            this.P_border_D.Name = "P_border_D";
            this.P_border_D.Size = new System.Drawing.Size(756, 3);
            this.P_border_D.TabIndex = 69;
            // 
            // P_border_L
            // 
            this.P_border_L.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.P_border_L.Location = new System.Drawing.Point(0, 0);
            this.P_border_L.Margin = new System.Windows.Forms.Padding(0);
            this.P_border_L.Name = "P_border_L";
            this.P_border_L.Size = new System.Drawing.Size(3, 1268);
            this.P_border_L.TabIndex = 66;
            // 
            // C4_Exit_onClose
            // 
            this.C4_Exit_onClose.AccessibleDescription = "Exit or Service Macros|(hiding in System Tray)";
            this.C4_Exit_onClose.Appearance = System.Windows.Forms.Appearance.Button;
            this.C4_Exit_onClose.BackColor = System.Drawing.Color.Transparent;
            this.C4_Exit_onClose.Checked = true;
            this.C4_Exit_onClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.C4_Exit_onClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.C4_Exit_onClose.Enabled = false;
            this.C4_Exit_onClose.FlatAppearance.BorderSize = 0;
            this.C4_Exit_onClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.C4_Exit_onClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.C4_Exit_onClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.C4_Exit_onClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.C4_Exit_onClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C4_Exit_onClose.ForeColor = System.Drawing.Color.Black;
            this.C4_Exit_onClose.Location = new System.Drawing.Point(478, 140);
            this.C4_Exit_onClose.Margin = new System.Windows.Forms.Padding(0);
            this.C4_Exit_onClose.Name = "C4_Exit_onClose";
            this.C4_Exit_onClose.Size = new System.Drawing.Size(128, 28);
            this.C4_Exit_onClose.TabIndex = 70;
            this.C4_Exit_onClose.Text = "Apply ► Exit";
            this.C4_Exit_onClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C4_Exit_onClose.UseVisualStyleBackColor = false;
            this.C4_Exit_onClose.Click += new System.EventHandler(this.C4_Exit_onClose_CB);
            this.C4_Exit_onClose.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L1_Profile_commit_behind
            // 
            this.L1_Profile_commit_behind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.L1_Profile_commit_behind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L1_Profile_commit_behind.Location = new System.Drawing.Point(60, 8);
            this.L1_Profile_commit_behind.Name = "L1_Profile_commit_behind";
            this.L1_Profile_commit_behind.Size = new System.Drawing.Size(130, 130);
            this.L1_Profile_commit_behind.TabIndex = 73;
            this.L1_Profile_commit_behind.Click += new System.EventHandler(this.L1_Profile_commit_CB);
            this.L1_Profile_commit_behind.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L2_KeyMap_commit_behind
            // 
            this.L2_KeyMap_commit_behind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.L2_KeyMap_commit_behind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L2_KeyMap_commit_behind.Location = new System.Drawing.Point(199, 8);
            this.L2_KeyMap_commit_behind.Name = "L2_KeyMap_commit_behind";
            this.L2_KeyMap_commit_behind.Size = new System.Drawing.Size(130, 130);
            this.L2_KeyMap_commit_behind.TabIndex = 74;
            this.L2_KeyMap_commit_behind.Click += new System.EventHandler(this.L2_KeyMap_commit_CB);
            this.L2_KeyMap_commit_behind.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L3_Macro_onClose_behind
            // 
            this.L3_Macro_onClose_behind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.L3_Macro_onClose_behind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L3_Macro_onClose_behind.Location = new System.Drawing.Point(338, 8);
            this.L3_Macro_onClose_behind.Name = "L3_Macro_onClose_behind";
            this.L3_Macro_onClose_behind.Size = new System.Drawing.Size(130, 130);
            this.L3_Macro_onClose_behind.TabIndex = 75;
            this.L3_Macro_onClose_behind.Click += new System.EventHandler(this.L3_Macro_onClose_CB);
            this.L3_Macro_onClose_behind.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L4_Exit_onClose_behind
            // 
            this.L4_Exit_onClose_behind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.L4_Exit_onClose_behind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L4_Exit_onClose_behind.Location = new System.Drawing.Point(477, 8);
            this.L4_Exit_onClose_behind.Name = "L4_Exit_onClose_behind";
            this.L4_Exit_onClose_behind.Size = new System.Drawing.Size(130, 130);
            this.L4_Exit_onClose_behind.TabIndex = 76;
            this.L4_Exit_onClose_behind.Click += new System.EventHandler(this.L4_Exit_onClose_CB);
            this.L4_Exit_onClose_behind.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // C3_Macro_onClose
            // 
            this.C3_Macro_onClose.AccessibleDescription = "Exit or Service Macros|(hiding in System Tray)";
            this.C3_Macro_onClose.Appearance = System.Windows.Forms.Appearance.Button;
            this.C3_Macro_onClose.BackColor = System.Drawing.Color.Transparent;
            this.C3_Macro_onClose.Checked = true;
            this.C3_Macro_onClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.C3_Macro_onClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.C3_Macro_onClose.Enabled = false;
            this.C3_Macro_onClose.FlatAppearance.BorderSize = 0;
            this.C3_Macro_onClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.C3_Macro_onClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.C3_Macro_onClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.C3_Macro_onClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.C3_Macro_onClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C3_Macro_onClose.ForeColor = System.Drawing.Color.Black;
            this.C3_Macro_onClose.Location = new System.Drawing.Point(339, 140);
            this.C3_Macro_onClose.Margin = new System.Windows.Forms.Padding(0);
            this.C3_Macro_onClose.Name = "C3_Macro_onClose";
            this.C3_Macro_onClose.Size = new System.Drawing.Size(128, 28);
            this.C3_Macro_onClose.TabIndex = 77;
            this.C3_Macro_onClose.Text = "Apply ☀ Macros";
            this.C3_Macro_onClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C3_Macro_onClose.UseVisualStyleBackColor = false;
            this.C3_Macro_onClose.Click += new System.EventHandler(this.C3_Macro_onClose_CB);
            // 
            // L_Logo
            // 
            this.L_Logo.AutoSize = true;
            this.L_Logo.BackColor = System.Drawing.Color.Transparent;
            this.L_Logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L_Logo.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_Logo.ForeColor = System.Drawing.Color.Lime;
            this.L_Logo.Image = global::DX1Utility.Properties.Resources.transparent_48;
            this.L_Logo.Location = new System.Drawing.Point(3, 3);
            this.L_Logo.Margin = new System.Windows.Forms.Padding(0);
            this.L_Logo.Name = "L_Logo";
            this.L_Logo.Size = new System.Drawing.Size(38, 42);
            this.L_Logo.TabIndex = 58;
            this.L_Logo.Text = "  ";
            this.L_Logo.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L1_Profile_commit
            // 
            this.L1_Profile_commit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L1_Profile_commit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L1_Profile_commit.ForeColor = System.Drawing.Color.Black;
            this.L1_Profile_commit.Image = global::DX1Utility.Properties.Resources.image1_profile_128;
            this.L1_Profile_commit.Location = new System.Drawing.Point(61, 9);
            this.L1_Profile_commit.Name = "L1_Profile_commit";
            this.L1_Profile_commit.Size = new System.Drawing.Size(128, 128);
            this.L1_Profile_commit.TabIndex = 72;
            this.L1_Profile_commit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.L1_Profile_commit.Click += new System.EventHandler(this.L1_Profile_commit_CB);
            this.L1_Profile_commit.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L2_KeyMap_commit
            // 
            this.L2_KeyMap_commit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L2_KeyMap_commit.Enabled = false;
            this.L2_KeyMap_commit.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L2_KeyMap_commit.ForeColor = System.Drawing.Color.Black;
            this.L2_KeyMap_commit.Image = global::DX1Utility.Properties.Resources.image2_keymap_128;
            this.L2_KeyMap_commit.Location = new System.Drawing.Point(200, 9);
            this.L2_KeyMap_commit.Name = "L2_KeyMap_commit";
            this.L2_KeyMap_commit.Size = new System.Drawing.Size(128, 128);
            this.L2_KeyMap_commit.TabIndex = 73;
            this.L2_KeyMap_commit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.L2_KeyMap_commit.Click += new System.EventHandler(this.L2_KeyMap_commit_CB);
            this.L2_KeyMap_commit.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L3_Macro_onClose
            // 
            this.L3_Macro_onClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L3_Macro_onClose.Enabled = false;
            this.L3_Macro_onClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L3_Macro_onClose.Image = global::DX1Utility.Properties.Resources.image3_macro_128;
            this.L3_Macro_onClose.Location = new System.Drawing.Point(339, 9);
            this.L3_Macro_onClose.Name = "L3_Macro_onClose";
            this.L3_Macro_onClose.Size = new System.Drawing.Size(128, 128);
            this.L3_Macro_onClose.TabIndex = 74;
            this.L3_Macro_onClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.L3_Macro_onClose.Click += new System.EventHandler(this.L3_Macro_onClose_CB);
            this.L3_Macro_onClose.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L4_Exit_onClose
            // 
            this.L4_Exit_onClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L4_Exit_onClose.Enabled = false;
            this.L4_Exit_onClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L4_Exit_onClose.ForeColor = System.Drawing.Color.Black;
            this.L4_Exit_onClose.Image = global::DX1Utility.Properties.Resources.image4_exit_128;
            this.L4_Exit_onClose.Location = new System.Drawing.Point(478, 9);
            this.L4_Exit_onClose.Name = "L4_Exit_onClose";
            this.L4_Exit_onClose.Size = new System.Drawing.Size(128, 128);
            this.L4_Exit_onClose.TabIndex = 75;
            this.L4_Exit_onClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.L4_Exit_onClose.Click += new System.EventHandler(this.L4_Exit_onClose_CB);
            this.L4_Exit_onClose.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L_ON
            // 
            this.L_ON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.L_ON.Image = global::DX1Utility.Properties.Resources.ON_48;
            this.L_ON.Location = new System.Drawing.Point(670, 215);
            this.L_ON.Name = "L_ON";
            this.L_ON.Size = new System.Drawing.Size(74, 74);
            this.L_ON.TabIndex = 64;
            this.L_ON.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L_ON.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L_REC
            // 
            this.L_REC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.L_REC.Image = global::DX1Utility.Properties.Resources.rec_48;
            this.L_REC.Location = new System.Drawing.Point(670, 290);
            this.L_REC.Name = "L_REC";
            this.L_REC.Size = new System.Drawing.Size(74, 74);
            this.L_REC.TabIndex = 62;
            this.L_REC.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L_REC.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // L_WRITING
            // 
            this.L_WRITING.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.L_WRITING.Font = new System.Drawing.Font("Noto Emoji", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_WRITING.ForeColor = System.Drawing.Color.Red;
            this.L_WRITING.Image = global::DX1Utility.Properties.Resources.pending_48;
            this.L_WRITING.Location = new System.Drawing.Point(670, 290);
            this.L_WRITING.Margin = new System.Windows.Forms.Padding(0);
            this.L_WRITING.Name = "L_WRITING";
            this.L_WRITING.Size = new System.Drawing.Size(74, 74);
            this.L_WRITING.TabIndex = 71;
            this.L_WRITING.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // L_STANDBY
            // 
            this.L_STANDBY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.L_STANDBY.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_STANDBY.ForeColor = System.Drawing.Color.Black;
            this.L_STANDBY.Image = global::DX1Utility.Properties.Resources.standby_48;
            this.L_STANDBY.Location = new System.Drawing.Point(670, 290);
            this.L_STANDBY.Name = "L_STANDBY";
            this.L_STANDBY.Size = new System.Drawing.Size(74, 74);
            this.L_STANDBY.TabIndex = 63;
            this.L_STANDBY.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L_STANDBY.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            // 
            // DX1Utility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(756, 1268);
            this.Controls.Add(this.C3_Macro_onClose);
            this.Controls.Add(this.C4_Exit_onClose);
            this.Controls.Add(this.P_border_U);
            this.Controls.Add(this.P_border_R);
            this.Controls.Add(this.P_border_D);
            this.Controls.Add(this.P_border_L);
            this.Controls.Add(this.L_STATUS);
            this.Controls.Add(this.L_PROFILES);
            this.Controls.Add(this.G_DX1_KEYS);
            this.Controls.Add(this.L_Logo);
            this.Controls.Add(this.C1_Profile_commit);
            this.Controls.Add(this.B_ShutDown);
            this.Controls.Add(this.C2_KeyMap_commit);
            this.Controls.Add(this.L_CONF_KEYTYPE);
            this.Controls.Add(this.T_Description);
            this.Controls.Add(this.L_CONF_CODE);
            this.Controls.Add(this.R_Single);
            this.Controls.Add(this.T_Conf_Type);
            this.Controls.Add(this.T_Conf_Code);
            this.Controls.Add(this.T_SingleKey);
            this.Controls.Add(this.B_Right_Button);
            this.Controls.Add(this.B_Test);
            this.Controls.Add(this.R_False);
            this.Controls.Add(this.R_True);
            this.Controls.Add(this.G_Special);
            this.Controls.Add(this.L_MultiKey);
            this.Controls.Add(this.R_Special);
            this.Controls.Add(this.R_Macro);
            this.Controls.Add(this.R_Multi);
            this.Controls.Add(this.MacroList);
            this.Controls.Add(this.C_Debug);
            this.Controls.Add(this.B_Delete);
            this.Controls.Add(this.D_Profiles);
            this.Controls.Add(this.B_EditProfile);
            this.Controls.Add(this.B_MACRO_EDIT);
            this.Controls.Add(this.L_ON);
            this.Controls.Add(this.L_REC);
            this.Controls.Add(this.L_WRITING);
            this.Controls.Add(this.T_LOG);
            this.Controls.Add(this.L_STANDBY);
            this.Controls.Add(this.B_KeyProgrammer);
            this.Controls.Add(this.L1_Profile_commit);
            this.Controls.Add(this.L2_KeyMap_commit);
            this.Controls.Add(this.L3_Macro_onClose);
            this.Controls.Add(this.L4_Exit_onClose);
            this.Controls.Add(this.L1_Profile_commit_behind);
            this.Controls.Add(this.L2_KeyMap_commit_behind);
            this.Controls.Add(this.L3_Macro_onClose_behind);
            this.Controls.Add(this.L4_Exit_onClose_behind);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(645, 526);
            this.Name = "DX1Utility";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DX1Utility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_Close_CB);
            this.Load += new System.EventHandler(this.F_Load_CB);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.F_MouseClick);
            this.MouseEnter += new System.EventHandler(this.toolTip_MouseEnter);
            this.G_RB_MENU.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.G_DX1_KEYS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.G_Special)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion //}}}

        public System.Windows.Forms.Label             L1_Profile_commit;
        public System.Windows.Forms.Label             L1_Profile_commit_behind;
        public System.Windows.Forms.Label             L2_KeyMap_commit;
        public System.Windows.Forms.Label             L2_KeyMap_commit_behind;
        public System.Windows.Forms.Label             L3_Macro_onClose;
        public System.Windows.Forms.Label             L3_Macro_onClose_behind;
        public System.Windows.Forms.Label             L4_Exit_onClose;
        public System.Windows.Forms.Label             L4_Exit_onClose_behind;

        public System.Windows.Forms.Button            B_Delete;
        public System.Windows.Forms.Button            B_EditProfile;
        public System.Windows.Forms.Button            B_KeyProgrammer;
        public System.Windows.Forms.Button            B_MACRO_EDIT;
        public System.Windows.Forms.Button            B_ShutDown;
        public System.Windows.Forms.Button            B_Test;
        public System.Windows.Forms.CheckBox          C_Debug;
        public System.Windows.Forms.CheckBox          C4_Exit_onClose;
        public System.Windows.Forms.CheckBox          C2_KeyMap_commit;
        public System.Windows.Forms.CheckBox          C1_Profile_commit;
        public System.Windows.Forms.ComboBox          D_Profiles;
        public System.Windows.Forms.ContextMenuStrip  G_RB_MENU;
        public System.Windows.Forms.DataGridView      G_DX1_KEYS;
        public System.Windows.Forms.DataGridView      G_Special;
        public System.Windows.Forms.Label             B_Right_Button;
        public System.Windows.Forms.Label             L_CONF_CODE;
        public System.Windows.Forms.Label             L_CONF_KEYTYPE;
        public System.Windows.Forms.Label             L_Logo;
        public System.Windows.Forms.Label             L_ON;
        public System.Windows.Forms.Label             L_WRITING;
        public System.Windows.Forms.Label             L_PROFILES;
        public System.Windows.Forms.Label             L_REC;
        public System.Windows.Forms.Label             L_STANDBY;
        public System.Windows.Forms.Label             L_STATUS;

        public System.Windows.Forms.ListBox           L_MultiKey;
        public System.Windows.Forms.ListBox           MacroList;
        public System.Windows.Forms.Panel             P_border_D;
        public System.Windows.Forms.Panel             P_border_L;
        public System.Windows.Forms.Panel             P_border_R;
        public System.Windows.Forms.Panel             P_border_U;
        public System.Windows.Forms.RadioButton       R_False;
        public System.Windows.Forms.RadioButton       R_Macro;
        public System.Windows.Forms.RadioButton       R_Multi;
        public System.Windows.Forms.RadioButton       R_Single;
        public System.Windows.Forms.RadioButton       R_Special;
        public System.Windows.Forms.RadioButton       R_True;
        public System.Windows.Forms.RichTextBox       T_LOG;
        public System.Windows.Forms.TextBox           T_Conf_Code;
        public System.Windows.Forms.TextBox           T_Conf_Type;
        public System.Windows.Forms.TextBox           T_DXKey;
        public System.Windows.Forms.TextBox           T_Description;
        public System.Windows.Forms.TextBox           T_SingleKey;
        public System.Windows.Forms.ToolStripMenuItem G_RBMenu_Clear;
        public System.Windows.Forms.ToolStripMenuItem G_RBMenu_Properties;
        public System.Windows.Forms.ToolStripMenuItem G_RBMenu_Wizard;
        public System.Windows.Forms.CheckBox C3_Macro_onClose;
    }
}
