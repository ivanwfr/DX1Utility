namespace DX1Utility
{
    partial class ProfileProperties
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
            this.T_Profile_Name = new System.Windows.Forms.TextBox();
            this.L_PROFILE_NAME = new System.Windows.Forms.Label();
            this.L_EXE_PATH = new System.Windows.Forms.Label();
            this.T_Profile_ExePath = new System.Windows.Forms.TextBox();
            this.B_Profile_OK = new System.Windows.Forms.Button();
            this.B_Profile_Cancel = new System.Windows.Forms.Button();
            this.B_ExePath = new System.Windows.Forms.Button();
            this.Panel_Profile = new System.Windows.Forms.Panel();
            this.L_PROFILE_OPTIONS = new System.Windows.Forms.Label();
            this.C_Profile_Blank = new System.Windows.Forms.CheckBox();
            this.Panel_Profile.SuspendLayout();
            this.SuspendLayout();
            // 
            // T_Profile_Name
            // 
            this.T_Profile_Name.Location = new System.Drawing.Point(108, 15);
            this.T_Profile_Name.Name = "T_Profile_Name";
            this.T_Profile_Name.Size = new System.Drawing.Size(254, 20);
            this.T_Profile_Name.TabIndex = 0;
            // 
            // L_PROFILE_NAME
            // 
            this.L_PROFILE_NAME.AutoSize = true;
            this.L_PROFILE_NAME.Location = new System.Drawing.Point(15, 18);
            this.L_PROFILE_NAME.Name = "L_PROFILE_NAME";
            this.L_PROFILE_NAME.Size = new System.Drawing.Size(70, 13);
            this.L_PROFILE_NAME.TabIndex = 1;
            this.L_PROFILE_NAME.Text = "Profile Name:";
            // 
            // L_EXE_PATH
            // 
            this.L_EXE_PATH.AutoSize = true;
            this.L_EXE_PATH.Location = new System.Drawing.Point(15, 49);
            this.L_EXE_PATH.Name = "L_EXE_PATH";
            this.L_EXE_PATH.Size = new System.Drawing.Size(87, 13);
            this.L_EXE_PATH.TabIndex = 3;
            this.L_EXE_PATH.Text = "Exe Path:";
            // 
            // T_Profile_ExePath
            // 
            this.T_Profile_ExePath.Location = new System.Drawing.Point(108, 46);
            this.T_Profile_ExePath.Name = "T_Profile_ExePath";
            this.T_Profile_ExePath.Size = new System.Drawing.Size(254, 20);
            this.T_Profile_ExePath.TabIndex = 1;
            // 
            // B_Profile_OK
            // 
            this.B_Profile_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_Profile_OK.Location = new System.Drawing.Point(210, 163);
            this.B_Profile_OK.Name = "B_Profile_OK";
            this.B_Profile_OK.Size = new System.Drawing.Size(75, 23);
            this.B_Profile_OK.TabIndex = 10;
            this.B_Profile_OK.Text = "OK";
            this.B_Profile_OK.UseVisualStyleBackColor = true;
            this.B_Profile_OK.Click += new System.EventHandler(this.B_Profile_OK_CB);
            // 
            // B_Profile_Cancel
            // 
            this.B_Profile_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Profile_Cancel.Location = new System.Drawing.Point(129, 163);
            this.B_Profile_Cancel.Name = "B_Profile_Cancel";
            this.B_Profile_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Profile_Cancel.TabIndex = 11;
            this.B_Profile_Cancel.Text = "Cancel";
            this.B_Profile_Cancel.UseVisualStyleBackColor = true;
            // 
            // B_ExePath
            // 
            this.B_ExePath.Location = new System.Drawing.Point(371, 44);
            this.B_ExePath.Name = "B_ExePath";
            this.B_ExePath.Size = new System.Drawing.Size(28, 23);
            this.B_ExePath.TabIndex = 2;
            this.B_ExePath.Text = "...";
            this.B_ExePath.UseVisualStyleBackColor = true;
            this.B_ExePath.Click += new System.EventHandler(this.B_ExePath_CB);
            // 
            // Panel_Profile
            // 
            this.Panel_Profile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Profile.Controls.Add(this.C_Profile_Blank);
            this.Panel_Profile.Controls.Add(this.L_PROFILE_OPTIONS);
            this.Panel_Profile.Location = new System.Drawing.Point(12, 83);
            this.Panel_Profile.Name = "Panel_Profile";
            this.Panel_Profile.Size = new System.Drawing.Size(387, 74);
            this.Panel_Profile.TabIndex = 11;
            // 
            // L_PROFILE_OPTIONS
            // 
            this.L_PROFILE_OPTIONS.AutoSize = true;
            this.L_PROFILE_OPTIONS.Location = new System.Drawing.Point(2, 4);
            this.L_PROFILE_OPTIONS.Name = "L_PROFILE_OPTIONS";
            this.L_PROFILE_OPTIONS.Size = new System.Drawing.Size(75, 13);
            this.L_PROFILE_OPTIONS.TabIndex = 9;
            this.L_PROFILE_OPTIONS.Text = "Profile Options";
            // 
            // C_Profile_Blank
            // 
            this.C_Profile_Blank.AutoSize = true;
            this.C_Profile_Blank.Location = new System.Drawing.Point(118, 49);
            this.C_Profile_Blank.Name = "C_Profile_Blank";
            this.C_Profile_Blank.Size = new System.Drawing.Size(119, 17);
            this.C_Profile_Blank.TabIndex = 5;
            this.C_Profile_Blank.Text = "Blank profile";
            this.C_Profile_Blank.UseVisualStyleBackColor = true;
            // 
            // ProfileProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 193);
            this.Controls.Add(this.Panel_Profile);
            this.Controls.Add(this.B_ExePath);
            this.Controls.Add(this.B_Profile_Cancel);
            this.Controls.Add(this.B_Profile_OK);
            this.Controls.Add(this.L_EXE_PATH);
            this.Controls.Add(this.T_Profile_ExePath);
            this.Controls.Add(this.L_PROFILE_NAME);
            this.Controls.Add(this.T_Profile_Name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProfileProperties";
            this.Text = "ProfileProperties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProfileProperties_FormClosing);
            this.Panel_Profile.ResumeLayout(false);
            this.Panel_Profile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox T_Profile_Name;
        private System.Windows.Forms.Label L_PROFILE_NAME;
        private System.Windows.Forms.Label L_EXE_PATH;
        private System.Windows.Forms.TextBox T_Profile_ExePath;
        private System.Windows.Forms.Button B_Profile_OK;
        private System.Windows.Forms.Button B_Profile_Cancel;
        private System.Windows.Forms.Button B_ExePath;
        private System.Windows.Forms.Panel Panel_Profile;
        private System.Windows.Forms.CheckBox C_Profile_Blank;
        private System.Windows.Forms.Label L_PROFILE_OPTIONS;
    }
}
