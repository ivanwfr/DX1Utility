#region using {{{
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#endregion }}}
namespace DX1Utility {
    public class Profile {
        // {{{
        public   List<string> ProfileList;

        public  string          CurrentProfileName  = "";
        public  KeyProgrammer   keyProgrammer       = null;
        public  List<KeyMap>    keyMapList          = null;
        private DX1Utility      ui                  = null;
        //}}}
        public Profile(DX1Utility formHandle) //{{{
        {
            this.ui         = formHandle;
            keyMapList      = new List<KeyMap>();
            keyProgrammer   = new KeyProgrammer();
        } //}}}

    // LOAD
    public  void Load_ProfileList() // {{{
    {
        //{{{
        // TODO: JSON instead of XML
        // TODO: list existing files instead of storing profile names
        string  caller = "Load_ProfileList";
        if( DX1Utility.Debug ) log(    caller);

        //}}}
        // [user_Dx1Profiles_dat_pathName] {{{
        ProfileList = new List<string>();

        string user_Dx1Profiles_dat_pathName = Globals.UserProfileFolder + "Dx1Profiles.dat";
        if( DX1Utility.Debug ) log(".. loading ["+user_Dx1Profiles_dat_pathName+"]");

        //}}}
        // Return [a profile List from USER FOLDER FILE] {{{
        System.IO.FileStream fs = null;
        try {
            if( System.IO.File.Exists( user_Dx1Profiles_dat_pathName ) ) {

                fs = new System.IO.FileStream(user_Dx1Profiles_dat_pathName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read);
                System.IO.BinaryReader     br = new System.IO.BinaryReader(fs);
                long                byteCount = new System.IO.FileInfo( user_Dx1Profiles_dat_pathName ).Length;
                byte[]              byteArray = br.ReadBytes( (Int32)byteCount );

                System.IO.MemoryStream stream = new System.IO.MemoryStream( byteArray );
                IFormatter          formatter = new BinaryFormatter();
                stream.Position               = 0;

                List<string>           list = (List<string>)formatter.Deserialize( stream );
                stream.Close();

                ProfileList = list;
            }//}}}
    // Or .. [ MENU_GLOBAL_PROFILE ] {{{
        else {

            // create a new profile List
            List<string> list = new List<string>();

            // add Default Global profile
            init_keyMapList();

            CurrentProfileName = Globals.MENU_GLOBAL_PROFILE;
            list.Add( CurrentProfileName );

            //ProfileManager.SaveProfile(CurrentProfileName, keyMapList);

            ProfileList = list;
        }//}}}
    // Exception .. [EMPTY LIST] {{{
    }
    catch(SerializationException ex) {
        log("*** "+caller+"("+user_Dx1Profiles_dat_pathName+") WRONG VERSION: "+ex.Message);
    }
    catch(Exception ex) {
        log("*** "+caller+"("+user_Dx1Profiles_dat_pathName+") Exception:\n"+ ex);
    }
    finally {
        if(fs != null) fs.Close();
    }
    //}}}
    } //}}}
    private bool _LoadProfile(string newProfileName) // {{{
    {
        string caller = "_LoadProfile";

        // LOAD PROFILE .. OUT [keyMapList] [keyProgrammer]
        // .. OUT   .. [keyMapList] [keyProgrammer]
        if( DX1Utility.Debug ) log("_LoadProfile("+newProfileName+")");
        // LOAD SELECTED PROFILE {{{
        keyMapList = ProfileManager.LoadProfile( newProfileName );
        if(keyMapList.Count > 0)
        {
            ui.MapMacroKeys(caller);

            log("");
            if(newProfileName == CurrentProfileName)
            {
                // CLEAR PENDING KEYMAP AND PROFILE COMMIT REQUIREMENTS
                log("["+ CurrentProfileName +"] RELOADING CURRENT PROFILE");

                keyProgrammer.notify_keyMap_commit_done();
                keyProgrammer.notify_profile_commit_done();

                if(ui.C2_KeyMap_commit .Checked ) ui.set_C2_KeyMap_commit_Checked ( false );
                if(ui.C1_Profile_commit.Checked ) ui.set_C1_Profile_commit_Checked( false );
            }
            else {
                // SET PENDING PROFILE COMMIT REQUIREMENTS
                log("NEW PROFILE LOADED ["+ newProfileName +"]");

                keyProgrammer.notify_keyMap_PROFILE_LOADED();
                if(ui.C2_KeyMap_commit .Checked ) ui.set_C2_KeyMap_commit_Checked( true );
            }
            return true;
        }
        //}}}
        // LOAD THE DEFAULT MAPPING INTO NEW PROFILE {{{
        if( ui.is_FormWindowState_Minimized() )
            log("loading Default mappings");
        else
            if( Environment.UserInteractive )
                MessageBox.Show("Error loading profile "+ newProfileName +"\n.. loading Defaults.", "", MessageBoxButtons.OK);

        if(newProfileName != Globals.MENU_GLOBAL_PROFILE)
            ProfileManager.SaveProfile(newProfileName, keyMapList);

        return false;
        //}}}
    } //}}}

    // SELECT .. EDIT .. SAVE
    public bool SelectProfile(string profileName) // {{{
    {
        // ..     OUT .. [CurrentProfileName] [D_Profiles.Text]
        // ..   CALLS .. ui.ApplyKeySet() (KEYMAP mode)
        // .. ENABLES .. BUTTONS
        string caller = "SelectProfile("+profileName+")";
        if( DX1Utility.Debug ) log(caller);

        // COMMIT CURRENT PROFILE CHANGES {{{
        if((CurrentProfileName != "") && (profileName != CurrentProfileName))
        {
            if( DX1Utility.Debug ) log("LEAVING ["+CurrentProfileName+"] PROFILE");

            ui.ApplyKeySet(caller);
        }
        //}}}
        // SEARCH AND (RE)LOAD [profileName]
        ProfileSearcher                    Searcher = new ProfileSearcher();
        string            newProfileName = Searcher.ProfileSearchByName(ProfileList, profileName);
        if( _LoadProfile( newProfileName ) )
        {
            CurrentProfileName = newProfileName;

            // BUTTONS .. f(loaded profile)
            ui.set_D_Profiles_Text( CurrentProfileName );
            return true;
        }
        // OR CLEAR PENDING COMMIT REQUIREMENTS
        else {
            keyProgrammer.notify_keyMap_commit_done();
            if(ui.C1_Profile_commit.Checked ) ui.set_C1_Profile_commit_Checked( false );

            return false;
        }
    } //}}}
    public void EditProfile(string profileName) // {{{
    {
        // Cannot Edit the Global profile {{{
/*{{{
        if(profileName == Globals.MENU_GLOBAL_PROFILE)
        {
            if( Environment.UserInteractive )
                MessageBox.Show("Cannot Edit the Global profile's properties", "", MessageBoxButtons.OK);

            return;
        }
}}}*/
        //}}}
        // CHECK PROFILE NAME .. (NEW OR EXISTING) {{{
        ProfileSearcher     Searcher = new ProfileSearcher();
        ProfileProperties   PProp    = new ProfileProperties();

        //}}}
        // [PProp DIALOG BOX] .. f(New menu entry) .. f(profile name .. from ui.D_Profiles.Text) {{{
        bool bNewProfile
            = (profileName == Globals.MENU_NEW_PROFILE)
            ;

        CurrentProfileName
            = bNewProfile
            ?  "New"
            :  Searcher.ProfileSearchByName(ProfileList, ui.get_D_Profiles_Text());

        PProp.EditProfile( CurrentProfileName );
        //}}}

        bool         clearProfile = false;
        DialogResult PPropAnswer  = PProp.ShowDialog();
        // DIALOG [CANCEL] {{{
        if(PPropAnswer != DialogResult.OK)
        {
            // if we were creating a new profile switch to the Global profile
            if( bNewProfile )
                SelectProfile( Globals.MENU_GLOBAL_PROFILE );
        }
        //}}}

        // DIALOG [OK] {{{
        else {
            // [bNewProfile] {{{
            if( bNewProfile )
            {
                // REJECT ALREADY EXISTING PROFILE NAME {{{
                if(Searcher.ProfileSearchByName(ProfileList, PProp.GetProfileNameOnly()) != null)
                {
                    if( Environment.UserInteractive )
                        MessageBox.Show("This profile already exists, you should load it first. Changes cancelled.", "", MessageBoxButtons.OK);

                    ui.set_D_Profiles_Text( Globals.MENU_NEW_PROFILE );
                }
                //}}}
                else {
                    // CREATE NEW PROFILE {{{
                    ui.set_D_Profiles_Text( CurrentProfileName );

                    PProp.GetEditedProfile(ref CurrentProfileName, ref clearProfile);
                    ui.add_D_Profiles_Item( CurrentProfileName );

                    ProfileList.Add( CurrentProfileName );
                    _Save_ProfileList_to_Dx1Profiles_dat();
                    //}}}
                    // [UNASSIGN ALL KEYS] {{{
                    if( clearProfile )
                    {
                        init_keyMapList();
                        ProfileManager.SaveProfile(CurrentProfileName, keyMapList);
                        SyncUI.sync("EditProfile");
                    }
                    else {
                        ProfileManager.SaveProfile(CurrentProfileName, keyMapList);
                    }
                    //}}}
                }
            }
            //}}}
            // UPDATE SELECTED PROFILE {{{
            else
            {
                PProp.GetEditedProfile(ref CurrentProfileName, ref clearProfile);
                if( clearProfile ) {
                    //Clear all the currently programmed keys on this profile
                    init_keyMapList();
                    ProfileManager.SaveProfile(CurrentProfileName, keyMapList);
                }
            }
            //}}}
            ui.set_B_Delete_Enabled( true );
        } //}}}

    } //}}}
    public void RemoveProfile(string profileName) // {{{
    {
        ui.del_D_Profiles_item( profileName );

        _Save_ProfileList_to_Dx1Profiles_dat();
    } //}}}
    private void _Save_ProfileList_to_Dx1Profiles_dat() //{{{
    {
        // [ProfileList] Serialize {{{
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        IFormatter      formatter = new BinaryFormatter();
        formatter.Serialize(ms, ProfileList);

        //}}}
        // UPDATE FILE [Dx1Profiles.dat] {{{
        System.IO.FileStream fs
            = new System.IO.FileStream(
                Globals.UserProfileFolder + "Dx1Profiles.dat"
                , System.IO.FileMode.OpenOrCreate
                , System.IO.FileAccess.Write
                );

        byte[] ba = ms.ToArray() ;
        fs.Write(ba.ToArray(), 0, ba.Length);

        fs.Close();
        ms.Close();
        ba = null;
        //}}}
    } //}}}

    // MENU
    public void profilesMenu_PopupCB(object sender, EventArgs e) // {{{
    {
        // profile names {{{
        List<string> profileNames = new List<string>();

        foreach (string profile in ProfileList)
            profileNames.Add( profile );

        profileNames.Sort();

        //}}}
        // Profile menu {{{
        MenuItem menu = (MenuItem)sender;
        menu.MenuItems.Clear();
        foreach (string profileName in profileNames)
        {
            MenuItem subMenu     = new MenuItem();
            subMenu.Text         = profileName;
            subMenu.RadioCheck   = true;

            if(CurrentProfileName == profileName)
                subMenu.Checked  = true;

            subMenu.Click       += new EventHandler( profilesMenuCB );

            menu.MenuItems.Add( subMenu );
        }
        //}}}
    } //}}}
    public void profilesMenuCB(object sender, EventArgs e) // {{{
    {
        //Runs when a new profile is selected in the Context menu
        MenuItem menu = ((MenuItem)sender);

        // Load selected profile keymap
        string profileName = menu.Text;

        SelectProfile( profileName );

        //Set the Menu Item Checked.
        menu.Checked = true;

        ui.ApplyKeySet("profilesMenuCB");

    } //}}}

    // KEYS
#region keyMapList {{{
        private void init_keyMapList() //{{{
        {
            keyMapList = new List<KeyMap>();

            for(int keyNum = 1; keyNum <= Globals.KEYS_MAX; ++keyNum )
            {
                KeyMap keyMap       = new KeyMap();

                keyMap.KeyCode      = 0;
                keyMap.KeyDesc      = "Unassigned";
                keyMap.KeyNum       = (byte)(keyNum);

                keyMapList.Add( keyMap );
            }

        } //}}}
        public void initKeyMap(byte keyIndex) //{{{
        {
            string caller = "initKeyMap";

            KeyMap keyMap = keyMapList[keyIndex];

            keyProgrammer.notify_keyMap_KEY_CLEARED();

            keyMap.KeyCode = 0;
            keyMap.KeyData = "";
            keyMap.KeyDesc = "Unassigned";
            keyMap.KeyName = "--";
            keyMap.KeyType = 0;
            keyMap.MacName = "";

            if( DX1Utility.Debug ) log(caller+": "+keyProgrammer.KeyMap_changeToCommit);
        } //}}}
#endregion //}}}

    // LOG
        /* log {{{*/
        private void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }
}
