using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DX1Utility
{
    public partial class ProfileProperties : Form
    {
        public ProfileProperties() // {{{
        {
            if( DX1Utility.Debug ) log("ProfileProperties.InitializeComponent:");
            InitializeComponent();
        }
        // }}}

        public void EditProfile(string profile_Name) // {{{
        {
            T_Profile_Name.Text         = profile_Name;
            if(profile_Name != "New")
                C_Profile_Blank.Text    = "Clear all programmed keys";
        }
        // }}}
        public void GetEditedProfile(ref string name, ref bool clearProfile) // {{{
        {
            name         = T_Profile_Name.Text;
            clearProfile = C_Profile_Blank.Checked;
        }
        // }}}
        public string GetProfileNameOnly() // {{{
        {
            return T_Profile_Name.Text;
        }
        // }}}

        private void B_ExePath_CB(object sender, EventArgs e) // {{{
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Executable Files(*.exe)|*.exe";
            dialog.RestoreDirectory = true;

            if(dialog.ShowDialog() == DialogResult.OK)
                T_Profile_ExePath.Text = dialog.FileName;
        }
        // }}}
        private void B_Profile_OK_CB(object sender, EventArgs e) // {{{
        {
            if( DX1Utility.Debug ) log("B_Profile_OK_CB():");
                            
        }
        // }}}
        private void ProfileProperties_FormClosing(object sender, FormClosingEventArgs e) // {{{
        {
            if( DX1Utility.Debug ) log("ProfileProperties_FormClosing():");
            if( DX1Utility.Debug ) log("...ExePath["+ T_Profile_ExePath.Text             +"]");
            if( DX1Utility.Debug ) log("......Name["+ T_Profile_Name.Text                +"]");
            if( DX1Utility.Debug ) log(".....Blank["+ C_Profile_Blank.Checked.ToString() +"]");

            ProfileSearcher ps = new ProfileSearcher();

            //Check to ensure name is not "New"
            if(T_Profile_Name.Text == "New" && this.DialogResult != DialogResult.Cancel)
            {
                if( Environment.UserInteractive )
                    MessageBox.Show("Profile Name cannot be 'New'","",MessageBoxButtons.OK);
                e.Cancel = true;
            }
        }
        // }}}

        /* log {{{*/
        private void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }
}
