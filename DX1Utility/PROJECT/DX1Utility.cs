#region using {{{
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
#endregion //}}}
namespace DX1Utility {// DX1Utility_TAG = (211003:14h:44) */
    public partial class DX1Utility : Form, LoggerInterface {
#region MEMBERS {{{
#region WizardForm variables {{{


    static DX1Utility       DX1UtilityInstance;

    // Tab Order when clicking Next
    static int[] SingleKey  = new int[]{ 0, 1, 5, 6};
    static int[] MultiKey   = new int[]{ 0, 2, 5, 6};
    static int[] MacroKey   = new int[]{ 0, 3, 6   };
    static int[] SpecialKey = new int[]{ 0, 4, 6   };

#endregion //}}}

#region static private variables {{{

    static public bool          Debug               =  false;

    // UI
    public bool                 DX1UtilityHasFocus  =  false;

    // Device
    private IntPtr              last_app_window     = IntPtr.Zero;
    private IntPtr              SelfHandle          = IntPtr.Zero;
    public  DX1Device           dx1Device           = null;

    // Timer for Macro Playback
    private MacroPlayer         macroPlayer         = new MacroPlayer();
    private System.Timers.Timer macroTimer          = new System.Timers.Timer();
    private System.DateTime     initialTime         =     System.DateTime.Now;

#endregion //}}}
#region instance private variables {{{

    public  KeyMap                    CurrentKeyMap            = new KeyMap();
    private string[]                  sKeyBindings             = new string[] { "", "Single-Key", "Multi-Key Macro", "Timed-Macro", "Special-Key" };
    private Dictionary<String, Macro> macroDict                = new Dictionary<String, Macro>();
    private Macro[]                   mKeyMacroSequenceMapping = new Macro[Globals.KEYS_MAX];
    private SpecialKeyPlayer          specialKeyPlayer         = null;

    private Profile                   profile                  = null;

    public  bool                      ShutDown_Requested       = false;
    public  bool                      KeyMap_SEND_REQUESTED    =  true;

    private System.Windows.Forms.NotifyIcon notifyIcon;

    private int                       cmdLine_apply_delay_sec  = 0;

#endregion //}}}

#endregion //}}}
#region MAIN {{{
        // static void Main {{{
        [STAThread]
        static void Main(string[] args)
        {
            using(Mutex mutex = new Mutex(false, "Global\\ewDx1Utility"))
            {
                if(!mutex.WaitOne(0, false)) {
                    if( Environment.UserInteractive )
                        MessageBox.Show("Dx1Utility is already running","Error");
                    return;
                }
                // Instanciate
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                new DX1Utility(ref args);
                Application.Run( DX1UtilityInstance );
            }
        } //}}}
        /*     DX1Utility {{{*/
        public DX1Utility(ref string[] args)
        {
            //{{{
            string caller = "DX1Utility";

            log_DeleteFile();
            Logger.LogStart(this);
            //}}}
            /* Command-line arguments {{{*/
            for(int i=0; i < args.Length; ++i)
            {
                /* USAGE {{{*/
                if(    (args[i].StartsWith("-")) // -h --help
                    || (args[i].StartsWith("/")) // /?
                  ) {
                    System.Console.WriteLine(
                        "USAGE:\n"
                        +"DX1Utility [cmdLine_apply_delay_sec]\n"
                        );
                    Environment.Exit(1);
                }
                /*}}}*/
                /* [cmdLine_apply_delay_sec] {{{*/
                else if(cmdLine_apply_delay_sec == 0)
                {
                    cmdLine_apply_delay_sec = int.Parse( args[i] );

                    KeyMap_SEND_REQUESTED   = true;
                }
                /*}}}*/
            }
            /*}}}*/
            // Check for DX1Profiles folder if it doesn't exist, create it {{{
            check_UserProfileFolder();

            //}}}
            // InitializeComponent in DX1Utility.Designer.cs {{{

            if( DX1Utility.Debug ) Log(caller+".InitializeComponent:");
            InitializeComponent();

            DX1UtilityInstance = this;

            DX1UtilityHasFocus = DX1UtilityInstance.ContainsFocus;
            //}}}
            // Debug Logger {{{
            C_Debug.Checked     = Debug;

            T_LOG.MouseDoubleClick += new MouseEventHandler( T_LOG_DoubleClick );

            //}}}
            // Check Environment.UserInteractive {{{
            if( Environment.UserInteractive ) Log(  "INTERACTIVE User Mode .. "+DateTime.Now);
            else                              Log("UNATTENDED Service Mode .. "+DateTime.Now);

            //}}}
            // [profile] .. [SyncUI] {{{
            profile = new Profile( this );

            SyncUI.Set_Form_Profile(this, profile);
            //}}}
            // specialKeyPlayer.InitPlayer {{{
if( DX1Utility.Debug ) Log("specialKeyPlayer.InitPlayer:");

            specialKeyPlayer = new SpecialKeyPlayer();
            specialKeyPlayer.InitPlayer();
            //}}}
            // macroTimer TODO ? {{{
            if( DX1Utility.Debug ) Log("macroTimer:");
            macroTimer.AutoReset = false;
            macroTimer.Elapsed += new System.Timers.ElapsedEventHandler( macroTimer_Elapsed_handler );

            RebuildMacroList();

            //}}}
            // Left system button displayed in the titlebar ? {{{
            ControlBox = true;

            //}}}
            //Load profiles list {{{
            profile.Load_ProfileList();

            //}}}
            // Profile menu {{{
            MenuItem tray_profilesMenu = init_ProfileControls();

            //}}}
            // AutoSelect Global profile (if none, create it) {{{
            if( !D_Profiles.Items.Contains( Globals.MENU_GLOBAL_PROFILE ) )
            {
                Log("Creating "+Globals.MENU_GLOBAL_PROFILE+" default profile:");
                profile.CurrentProfileName = Globals.MENU_GLOBAL_PROFILE;
            }

            //}}}
            // [IWE] (140313) - Moved Create New profile to the end of the list {{{
            //Add the (Create new profile) option to the profile Combobox
            D_Profiles.Items.Add(Globals.MENU_NEW_PROFILE);
            D_Profiles.Items.Add(Globals.OPEN_PROFILE_FOLDER);
            D_Profiles.Items.Add(Globals.OPEN_EXECUTABLE_FOLDER);

            profile.SelectProfile( Globals.MENU_GLOBAL_PROFILE );

            //}}}
            /* [IWE] [211003) - Default to send key mappings to device {{{*/
            if( KeyMap_SEND_REQUESTED )
                profile.keyProgrammer.notify_keyMap_SEND_REQUESTED();

            //}}}
            // KeyMap List  and G_DX1_KEYS {{{
            init_G_DX1_KEYS();

            //}}}
            // Tray menu {{{
            NotifyIcon notifyIcon = init_notifyIcon( tray_profilesMenu );

            //}}}
            // CHECKED STATES .. SET DEFAULT .. SYNC UI {{{
            set_C1_Profile_commit_Checked(false, Globals.CHECKED_PASSIVE);
            set_C2_KeyMap_commit_Checked (false, Globals.CHECKED_PASSIVE);
            set_C3_Macro_on_Close_Checked(false, Globals.CHECKED_PASSIVE);
            set_C4_Exit_on_Close_Checked ( true, Globals.CHECKED_PASSIVE); // TODO !profile.has_some_macro(); .. EXIT ON CLOSE OR SOME MACROS TO HANDLE FOR THE FIRST SELECTED PROFILE

            //}}}
            // [dx1Device_Handler] {{{
            if(cmdLine_apply_delay_sec > 0)
            {
                new Thread(new ThreadStart( dx1Device_Handler )).Start();
            }
            else {
                dx1Device_Handler();
                ApplyKeySet( caller );
                this.TopLeft_to_ScreenTopCenter();
            }
            //}}}
        }
        /*}}}*/
        /* check_UserProfileFolder {{{*/
        private void check_UserProfileFolder()
        {
            if (System.IO.Directory.Exists(Globals.UserProfileFolder)) {
                if( DX1Utility.Debug )   Log("check_UserProfileFolder: CHECKED USER PROFILE FOLDER:\n - ["+ Globals.UserProfileFolder +"]");
            }
            else {
                System.IO.Directory.CreateDirectory( Globals.UserProfileFolder );
            }
              /*if( DX1Utility.Debug )*/ Log("check_UserProfileFolder: CREATED  USER PROFILE FOLDER:\n - ["+ Globals.UserProfileFolder +"]");

        }
        /*}}}*/
        /*_ dx1Device_Handler {{{*/
        private void dx1Device_Handler()
        {
            string caller ="dx1Device_Handler";
            if( DX1Utility.Debug ) Log("=====================");
            Log("ACCESSING DX1 DEVICE:");
            /* APPLY COMMAND-LINE-DELAY {{{*/
            if(cmdLine_apply_delay_sec > 0)
            {
                /* FOCUS UNSET */
                DX1UtilityHasFocus = false;

                Log("...starting a "+cmdLine_apply_delay_sec+"s countdown");

                while(cmdLine_apply_delay_sec > FOCUS_LOST_APPLY_DELAY_SEC)
                {
                    if(    ( cmdLine_apply_delay_sec       < 10)
                        || ((cmdLine_apply_delay_sec %  5) == 0)
                      )
                        Log(".. "+cmdLine_apply_delay_sec+"s");

                    Thread.Sleep( 1000 );
                    cmdLine_apply_delay_sec -= 1;

                    if(DX1UtilityHasFocus) break;

                    this.Invoke( (MethodInvoker)delegate() { this.TopLeft_to_ScreenTopCenter(); } );
                }

                /* FOCUS TAKEN BY USER */
                if( DX1UtilityHasFocus ) Log("...countdown interrupted");
            }
            /*}}}*/
            if( DX1Utility.Debug ) Log("=====================");

            dx1Device = new DX1Device(Handle);

            if( this.InvokeRequired )
                this.Invoke( (MethodInvoker)delegate() { ApplyKeySet(caller); } );
            else                                         ApplyKeySet(caller);
        }
        /*}}}*/

        /*_ T_LOG_DoubleClick {{{*/
        private void T_LOG_DoubleClick(object sender, EventArgs e)
        {
            log_Clear();
        }
        /*}}}*/
        // ToString {{{
        public override string ToString()
        {
            return "DX1UtilityInstance";

        }
        //}}}
#endregion //}}}
#region KEYS {{{
        /*_ startWizard {{{*/
        private void startWizard()
        {
            // [ENTERING KEYMAP MODE] {{{
            Log("ENTERING KEYMAP MODE .. [startWizard]");

            dx1Device.enter_KEYMAP_MODE();
            //}}}
            // GRID
            int keyNum = 1 + G_DX1_KEYS.CurrentRow.Index;
            if( DX1Utility.Debug ) Log("startWizard(keyNum "+ keyNum +")");
            G_DX1_KEYS.Focus();

            // KEYMAP START
            profile.keyProgrammer.KeyNum = keyNum;

            SyncUI.sync("startWizard");
        }
        /*}}}*/
        /*_ stopWizard {{{*/
        private void stopWizard(string caller)
        {
            if( DX1Utility.Debug ) Log("stopWizard "+Globals.SYMBOL_ARROW_L+" "+caller);//FIXME
            // KEYMAP STOP
            profile.keyProgrammer.KeyNum = 0;

            SyncUI.sync("stopWizard "+Globals.SYMBOL_ARROW_L+" "+caller);
        }
        /*}}}*/

        // GRID SELECT
        /*_ select_keyNum {{{*/
        private void select_keyNum(int keyNum)
        {
            bool log_this = DX1Utility.Debug;
            if(  log_this ) Log("select_keyNum("+ keyNum +") DX1UtilityHasFocus=["+ DX1UtilityHasFocus +"]");

            if(keyNum < 1                      ) { G_DX1_KEYS.CurrentCell = null; return; }

            if(keyNum > G_DX1_KEYS.Rows.Count) {                                  return; }

            // KEY
            profile.keyProgrammer.KeyNum = keyNum;

            // GRID
            grid_show_keyNum( keyNum );

            // MACRO
            MacroList.ClearSelected();

            // UI
            SyncUI.sync("select_keyNum");
        }
        /*}}}*/
        /*_ grid_show_keyNum {{{*/
        private void grid_show_keyNum(int keyNum)
        {
            if( DX1Utility.Debug ) Log("grid_show_keyNum("+ keyNum +") DX1UtilityHasFocus=["+ DX1UtilityHasFocus +"]");

            G_DX1_KEYS.CurrentCell = G_DX1_KEYS.Rows[keyNum-1].Cells[0];

        }
        /*}}}*/

#endregion //}}}
#region INIT {{{
        /*_ init_ProfileControls {{{*/
        private MenuItem init_ProfileControls()
        {
            // Add profiles to D_Profiles Combo Box and Context Menu

            // Profiles menu
            MenuItem tray_profilesMenu        = new MenuItem();
            tray_profilesMenu.Popup          += new EventHandler( profile.profilesMenu_PopupCB );
            tray_profilesMenu.Text            = "Profile";
            tray_profilesMenu.RadioCheck      = true;

            foreach (string profile_name in profile.ProfileList)
            {
                MenuItem subMenu        = new MenuItem();
                subMenu.Text            = profile_name;
                subMenu.RadioCheck      = true;
                subMenu.Click          += new EventHandler( profile.profilesMenuCB );

                tray_profilesMenu.MenuItems.Add( subMenu );

                D_Profiles.Items.Add( profile_name );
            }

            // [IWE] (141313) - Preserve menu entries build order
            D_Profiles.Sorted       = false; // true;

            return tray_profilesMenu;

        }
        /*}}}*/
        /*_ init_notifyIcon {{{*/
        private NotifyIcon init_notifyIcon(MenuItem tray_profilesMenu)
        {
            // TRAY ICON
            if( DX1Utility.Debug ) Log("Tray icon:");

            notifyIcon                   = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Icon              = this.Icon;
            notifyIcon.Text              = "DX1 Utility";
            notifyIcon.MouseDoubleClick += new MouseEventHandler( F_Minimize_Toggle );
//          notifyIcon.MouseClick       += new MouseEventHandler( F_Minimize_Toggle );
            notifyIcon.Visible           = true;

            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add( tray_profilesMenu );
            menu.MenuItems.Add("-");
            menu.MenuItems.Add("O&pen",    new      EventHandler( F_Minimize_Toggle ));
            menu.MenuItems.Add("E&xit",    new      EventHandler( exitAppCB         ));

            notifyIcon.ContextMenu       = menu;

            notifyIcon.BalloonTipIcon    = ToolTipIcon.Info;
            notifyIcon.BalloonTipTitle   = Globals.NOTIFY_TITLE;
            update_notifyIcon(profile.CurrentProfileName+" profile");

            return notifyIcon;

        }
        /*}}}*/
        /*_ update_notifyIcon {{{*/
        private void update_notifyIcon(string text)
        {
            if(cmdLine_apply_delay_sec > 0)
                text += "\n( "+cmdLine_apply_delay_sec+"s apply delay )";

            notifyIcon.BalloonTipText    = text;

            notifyIcon.ShowBalloonTip(2000);
        }
        /*}}}*/
        /*_ init_G_DX1_KEYS {{{*/
        private void init_G_DX1_KEYS()
        {
            // KeyMap List  and G_DX1_KEYS {{{
            G_DX1_KEYS.AllowUserToResizeColumns   = true;
            G_DX1_KEYS.AllowUserToResizeRows      = false;
            G_DX1_KEYS.AutoGenerateColumns        = false;
            G_DX1_KEYS.DataSource                 = profile.keyMapList;

          //G_DX1_KEYS.ColumnHeadersHeightSizeMode= DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
          //G_DX1_KEYS.RowHeadersWidthSizeMode    = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            DataGridViewTextBoxColumn col;

            // Header [KeyNum]
            col                             = new DataGridViewTextBoxColumn();
            col.Width                       = 20;
            col.DataPropertyName            = "KeyNum";
            col.HeaderText                  = "#";
            col.ReadOnly                    = true;
            col.DefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleCenter;
            G_DX1_KEYS.Columns.Add( col );

            // Header [KeyName]
            col                             = new DataGridViewTextBoxColumn();
            col.Width                       = 60;
            col.DataPropertyName            = "KeyName";
            col.HeaderText                  = "Key";
            col.ReadOnly                    = true;
            col.DefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleCenter;
            G_DX1_KEYS.Columns.Add( col );

            // Header [KeyDesc]
            col                             = new DataGridViewTextBoxColumn();
            col.Width                       = 80;
            col.DataPropertyName            = "KeyDesc";
            col.HeaderText                  = "KeyDesc";
            col.ReadOnly                    = true;
            G_DX1_KEYS.Columns.Add( col );

            //}}}
            G_DX1_KEYS.AutoResizeColumns();

        }
        /*}}}*/
        /*_ select_CurrentKeyMap {{{*/
        private void select_CurrentKeyMap(KeyMap keyMap)
        {
            if( DX1Utility.Debug ) Log("DX1Utility.select_CurrentKeyMap("+ keyMap.KeyName +")");

            // Select CurrentKeyMap
            CurrentKeyMap           = keyMap;

            T_Conf_Type.Text        = sKeyBindings[ CurrentKeyMap.KeyType ];
            T_Conf_Code.Text        = CurrentKeyMap.KeyCode.ToString();

            T_Conf_Type.Enabled     = false;
            T_Conf_Code.Enabled     = false;

            BuildMacroList();

            SyncUI.sync("select_CurrentKeyMap");
        }
        /*}}}*/

    /*_ BuildMacroList {{{*/
    private void BuildMacroList()
    {
        //Build the Macro list
        MacroList.Items.Clear();
        //MacroList.Items.Add("NEW MACRO");

        // Add the files from the directory
        if (!System.IO.Directory.Exists(Globals.ProfileMacroPath))
            System.IO.Directory.CreateDirectory(Globals.ProfileMacroPath);

        String[] files = System.IO.Directory.GetFiles(Globals.ProfileMacroPath, "*.mac");
        foreach (String name in files)
        {
            System.IO.FileStream stream = new System.IO.FileStream(name, System.IO.FileMode.Open);
            Macro                 macro = Macro.Read(stream);
            stream.Close();

            String[] pathComponents = name.Split('\\');
            pathComponents  = pathComponents.Last().Split('.');
            macro.name      = pathComponents.First();

            MacroList.Items.Add(macro.name);
        }
    }
    /*}}}*/
    /*_ ProcessExtraData {{{*/
/*{{{
    private void ProcessExtraData()
    {
        // Custom process
        //  to Take the extra data
        //  for Special-Keys
        //  and convert it to KeyData
        //  for the CurrentKey
        switch(specialKeyPlayer.SpecialKeys[G_Special.CurrentRow.Index].ExtraDataType)
        {
            // For a Boolean Extra data
            // pass in the value of Checked for True
            case SpecialKeysExtraData.Boolean:
                    CurrentKeyMap.KeyData
                        = specialKeyPlayer.GetCustomData(G_Special.CurrentRow.Index, R_True.Checked.ToString());
                    break;

            default:
                    break;
        }
    }
}}}*/
    /*}}}*/
#endregion //}}}
#region PROFILE {{{
        // DIALOG {{{
        public string get_D_Profiles_Text()                   { return D_Profiles.Text;                        }
        public void   set_D_Profiles_Text(string profileName) {        D_Profiles.Text = profileName; B_Delete     .Enabled = true; B_EditProfile.Enabled = true; }
        public void   add_D_Profiles_Item(string profileName) {        D_Profiles.Items.Add( profileName );    }
        public void   del_D_Profiles_item(string profileName) {        D_Profiles.Items.Remove( profileName ); }
        //}}}
        // DELETE {{{
        //}}}
        public void   set_B_Delete_Enabled        (bool state) { B_Delete.Enabled     = state; }
#endregion //}}}
#region EXIT {{{
//{{{
//
//}}}
        /*_ exitAppCB {{{*/
        private void exitAppCB(object sender, EventArgs e)
        {
            ShutDown_Requested = true;

            ApplyKeySet("exitAppCB");

            new Thread(new ThreadStart( exitApp_Handler )).Start();
        }
        /*}}}*/
        /*_ exitApp_Handler {{{*/
        const int      SHUTDOWN_DELAY = 500;
        private   void exitApp_Handler()
        {
            if( DX1Utility.Debug ) Log("exitApp_Handler: .. calling Environment.exit(0) in "+ SHUTDOWN_DELAY +"ms");
            Thread.Sleep( SHUTDOWN_DELAY );

            Log("Exiting "+ notifyIcon.Text);
            notifyIcon.Visible = false;

            log_FileClose();

            Environment.Exit(0);
        }
        /*}}}*/

#endregion //}}}
#region CALLBACKS {{{

    // FORM state
    /*_ set_hasMoved {{{*/
    private bool hasMoved = false;
    private void set_hasMoved(bool state, string _caller)
    {
        if(state == hasMoved) return;
Log("set_hasMoved("+state+") .. "+_caller);//FIXME

        hasMoved = state;
    }
    /*}}}*/
    /*_ is_FormWindowState_Minimized {{{*/
    public  bool is_FormWindowState_Minimized()
    {
        return (WindowState == FormWindowState.Minimized);
    }
    /*}}}*/

    // FORM controls
        /*_ F_Load_CB {{{*/
        private   void F_Load_CB(object sender, EventArgs e)
        {
            SyncUI.set_tooltips();
        }
        /*}}}*/
        /*_ F_KeyUp_CB {{{*/
        private   void F_KeyUp_CB(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }
        /*}}}*/
        /*_ F_MouseClick {{{*/
        private   void F_MouseClick(object sender, MouseEventArgs e)
        {
/*{{{
log("F_MouseClick("+((Control)sender).Name+")");
}}}*/
            if     ( L1_Profile_commit.Bounds.Contains( e.Location ) ) L1_Profile_commit_CB(sender, (EventArgs)e);
            else if( L2_KeyMap_commit .Bounds.Contains( e.Location ) ) L2_KeyMap_commit_CB (sender, (EventArgs)e);
            else if( L3_Macro_onClose .Bounds.Contains( e.Location ) ) L3_Macro_onClose_CB (sender, (EventArgs)e);
            else if( L4_Exit_onClose  .Bounds.Contains( e.Location ) ) L4_Exit_onClose_CB  (sender, (EventArgs)e);

            // (Disabled CheckBox call to Label callback)
            if     (!C1_Profile_commit.Enabled && C1_Profile_commit.Bounds.Contains( e.Location ) ) L1_Profile_commit_CB(sender, (EventArgs)e);
            else if(!C2_KeyMap_commit .Enabled && C2_KeyMap_commit .Bounds.Contains( e.Location ) ) L2_KeyMap_commit_CB (sender, (EventArgs)e);
            else if(!C3_Macro_onClose .Enabled && C3_Macro_onClose .Bounds.Contains( e.Location ) ) L3_Macro_onClose_CB (sender, (EventArgs)e);
            else if(!C4_Exit_onClose  .Enabled && C4_Exit_onClose  .Bounds.Contains( e.Location ) ) L4_Exit_onClose_CB  (sender, (EventArgs)e);
        }
        /*}}}*/
        /*_ F_OnMouseDown_disabledControl {{{*/
        private string F_OnMouseDown_disabledControl(MouseEventArgs e, string _caller)
        {
            string result = "";

            if     (!L1_Profile_commit.Enabled && (L1_Profile_commit.Bounds.Contains( e.Location ))) result = "L1_Profile_commit";
            else if(!L2_KeyMap_commit .Enabled && (L2_KeyMap_commit .Bounds.Contains( e.Location ))) result = "L2_KeyMap_commit" ;
            else if(!L3_Macro_onClose .Enabled && (L3_Macro_onClose .Bounds.Contains( e.Location ))) result = "L3_Macro_onClose" ;
            else if(!L4_Exit_onClose  .Enabled && (L4_Exit_onClose  .Bounds.Contains( e.Location ))) result = "L4_Exit_onClose"  ;

            if     (!C1_Profile_commit.Enabled && (C1_Profile_commit.Bounds.Contains( e.Location ))) result = "C1_Profile_commit";
            else if(!C2_KeyMap_commit .Enabled && (C2_KeyMap_commit .Bounds.Contains( e.Location ))) result = "C2_KeyMap_commit" ;
            else if(!C3_Macro_onClose .Enabled && (C3_Macro_onClose .Bounds.Contains( e.Location ))) result = "C3_Macro_onClose" ;
            else if(!C4_Exit_onClose  .Enabled && (C4_Exit_onClose  .Bounds.Contains( e.Location ))) result = "C4_Exit_onClose"  ;

if(result != "") Log(_caller+" FROM "+result);//FIXME
            return result;
        }
        /*}}}*/

    // FORM minimize
        // [Hide_On_Next_Minimize_Call] {{{
        private bool Hide_On_Next_Minimize_Call = false;

        //}}}
        /*_ F_Minimize_Toggle {{{*/
        private void F_Minimize_Toggle(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
                Activate();
            }
            else {
                Hide_On_Next_Minimize_Call = true;
                this.WindowState           = FormWindowState.Minimized;
            }
        }
        /*}}}*/
        /*_ F_Minimize_Sync {{{*/
        private void F_Minimize_Sync()
        {
            if( DX1Utility.Debug ) Log("F_Minimize_Sync");

            if(WindowState == FormWindowState.Minimized && Hide_On_Next_Minimize_Call)
            {
                if( DX1Utility.Debug ) Log("========");
                if( DX1Utility.Debug ) Log("= Hide =");
                if( DX1Utility.Debug ) Log("========");
                Hide();
            }
            else if(WindowState == FormWindowState.Normal)
            {
                if( DX1Utility.Debug ) Log("=================================");
                if( DX1Utility.Debug ) Log("Hide_On_Next_Minimize_Call = false");
                if( DX1Utility.Debug ) Log("=================================");
                Hide_On_Next_Minimize_Call      = false;
            }
        }
        /*}}}*/

    // STATE checkbox..label
        //{{{
        // CLICKED CHECKBOX .. (apply result state)
        private void C1_Profile_commit_CB(object sender, EventArgs e) { if(hasMoved) return; set_C1_Profile_commit_Checked( false ); }
        private void C2_KeyMap_commit_CB (object sender, EventArgs e) { if(hasMoved) return; set_C2_KeyMap_commit_Checked ( false ); }
        private void C3_Macro_onClose_CB (object sender, EventArgs e) { if(hasMoved) return; set_C3_Macro_on_Close_Checked( false ); }
        private void C4_Exit_onClose_CB  (object sender, EventArgs e) { if(hasMoved) return; set_C4_Exit_on_Close_Checked ( false ); }

        // CLICKED    LABEL .. (toggle current state)
        private void L1_Profile_commit_CB(object sender, EventArgs e) { if(hasMoved) return; set_C1_Profile_commit_Checked( !C1_Profile_commit.Checked ); }
        private void L2_KeyMap_commit_CB (object sender, EventArgs e) { if(hasMoved) return; set_C2_KeyMap_commit_Checked ( !C2_KeyMap_commit .Checked ); }
        private void L3_Macro_onClose_CB (object sender, EventArgs e) { if(hasMoved) return; set_C3_Macro_on_Close_Checked( !C3_Macro_onClose .Checked ); }
        private void L4_Exit_onClose_CB  (object sender, EventArgs e) { if(hasMoved) return; set_C4_Exit_on_Close_Checked ( !C4_Exit_onClose  .Checked ); }

        /*_ set_C1_Profile_commit_Checked {{{*/
        public  void set_C1_Profile_commit_Checked(bool state, bool checked_passive=false)
        {
            // APPLY PROFILE STATE
            C1_Profile_commit.Checked =  state;
            C1_Profile_commit.Enabled =  state;
            L1_Profile_commit.Enabled =  state;

            if(checked_passive) return;
            if(!DX1UtilityHasFocus) TakeFocus("set_C1_Profile_commit_Checked");

            // CLEAR PROGRESS UI ON REQUEST
            if( C1_Profile_commit.Checked) L_STANDBY.Text = "";

            if(!C1_Profile_commit.Checked) profile.keyProgrammer.notify_profile_commit_done();
            else                           profile.keyProgrammer.notify_profile_SAVE_REQUESTED();

            ApplyKeySet("set_C1_Profile_commit_Checked("+state+")");
        }
        /*}}}*/
        /*_ set_C2_KeyMap_commit_Checked  {{{*/
        public  void set_C2_KeyMap_commit_Checked (bool state, bool checked_passive=false)
        {
            // APPLY KEYMAP STATE
            C2_KeyMap_commit.Checked  =  state;
            C2_KeyMap_commit.Enabled  =  state;
            L2_KeyMap_commit.Enabled  =  state;

            if(checked_passive) return;
            if(!DX1UtilityHasFocus) TakeFocus("set_C2_KeyMap_commit_Checked");

            // CLEAR PROGRESS UI ON REQUEST
            if( C2_KeyMap_commit.Checked) L_STANDBY.Text = "";

            if(!C2_KeyMap_commit.Checked) profile.keyProgrammer.notify_keyMap_commit_done();
            else                          profile.keyProgrammer.notify_keyMap_SEND_REQUESTED();

            ApplyKeySet("set_C2_KeyMap_commit_Checked("+state+")");

        }
        /*}}}*/
        /*_ set_C3_Macro_on_Close_Checked {{{*/
        public  void set_C3_Macro_on_Close_Checked(bool state, bool checked_passive=false)
        {
            // APPLY MACRO STATE
            C3_Macro_onClose.Checked  =  state;
            C3_Macro_onClose.Enabled  =  state;
            L3_Macro_onClose.Enabled  =  state;

            // MIRROR EXIT STATE (RADIO-BEHAVIOR)
            C4_Exit_onClose .Checked  = !state;
            C4_Exit_onClose .Enabled  = !state;
            L4_Exit_onClose .Enabled  = !state;

            if(checked_passive) return;
            if(!DX1UtilityHasFocus) TakeFocus("set_C3_Macro_on_Close_Checked");

            ApplyKeySet("set_C3_Macro_on_Close_Checked");
        }
        /*}}}*/
        /*_ set_C4_Exit_on_Close_Checked {{{*/
        public  void set_C4_Exit_on_Close_Checked(bool state, bool checked_passive=false)
        {
            // APPLY EXIT STATE
            C4_Exit_onClose .Checked  =  state;
            C4_Exit_onClose .Enabled  =  state;
            L4_Exit_onClose .Enabled  =  state;

            // MIRROR MACRO STATE (RADIO-BEHAVIOR)
            C3_Macro_onClose.Checked  = !state;
            C3_Macro_onClose.Enabled  = !state;
            L3_Macro_onClose.Enabled  = !state;

            if(checked_passive) return;
            if(!DX1UtilityHasFocus) TakeFocus("set_C4_Exit_on_Close_Checked");

            ApplyKeySet("set_C4_Exit_on_Close_Checked");
        }
        /*}}}*/
        //}}}

    // CLOSE
        /*_ F_Close_CB {{{*/
        private void F_Close_CB(object sender, FormClosingEventArgs e)
        {
            // .. exiting app is the shutdown-button privilege
            // .. overriding the Close control box .. minimize the form instead
            if(!ShutDown_Requested ) {
                e.Cancel                    = true;
                Hide_On_Next_Minimize_Call   = true;
                this.WindowState            = FormWindowState.Minimized;
            }
            else {
                notifyIcon.Visible  = false;
            }

        }
        /*}}}*/
        /*_ B_ShutDown_CB {{{*/
        private void B_ShutDown_CB(object sender, EventArgs e)
        {
            if(C4_Exit_onClose.Checked)
                exitAppCB(sender, e);
            else
                this.WindowState            = FormWindowState.Minimized;
        }
        /*}}}*/

    // TOOLTIP
    /*_ toolTip_MouseEnter {{{*/
    private void toolTip_MouseEnter(object sender, EventArgs e)
    {
        SyncUI.toolTip_MouseEnter(sender, e);
    }
    /*}}}*/


    // PROFILE
        //{{{

        //}}}
        /*_ B_Delete_CB {{{*/
        private void B_Delete_CB(object sender, EventArgs e)
        {
            // Delete profile

            if(D_Profiles.Text == Globals.MENU_GLOBAL_PROFILE)
            {
                if( Environment.UserInteractive )
                    MessageBox.Show("Cannot Delete the Global profile", "", MessageBoxButtons.OK);
                return;
            }

            bool confirmed = true;
            if( Environment.UserInteractive )
            {
                DialogResult answer  = MessageBox.Show("Are you sure you want to delete profile " + D_Profiles.Text + " ?", "", MessageBoxButtons.YesNo);
                confirmed = (answer == DialogResult.Yes);
            }
            if( confirmed )
            {
                ProfileManager.DeleteProfile( D_Profiles.Text );

                profile.ProfileList.RemoveAll((profile) => profile == D_Profiles.Text);

                profile.RemoveProfile(          D_Profiles.Text);

                profile.SelectProfile( Globals.MENU_GLOBAL_PROFILE );
                update_notifyIcon(profile.CurrentProfileName+" profile");

                // Repaint G_DX1_KEYS
                G_DX1_KEYS.Invalidate();
                G_DX1_KEYS.AutoResizeColumns();

                ApplyKeySet("B_Delete_CB");
            }

        }
        /*}}}*/
        /*_ B_EditProfile_CB {{{*/
        private void B_EditProfile_CB(object sender, EventArgs e)
        {
            profile.EditProfile(D_Profiles.Text);
        }
        /*}}}*/
        /*_ D_ProfilesCB {{{*/
        private void D_ProfilesCB(object sender, EventArgs e)
        {
            if( DX1Utility.Debug ) Log("D_ProfilesCB("+ sender +")");

            stopWizard("D_ProfilesCB");

            string  item_string  = D_Profiles.SelectedItem.ToString();

/*{{{
:new $LOCAL/STORE/DEV/PROJECTS/RTabs/Util/src/Settings.cs
}}}*/
            if(    (item_string == Globals.OPEN_PROFILE_FOLDER   )
                || (item_string == Globals.OPEN_EXECUTABLE_FOLDER)
              ) {
                string cmd_file = "explorer";

                string cmd_args = (item_string == Globals.OPEN_PROFILE_FOLDER)
                    ?              Globals.UserProfileFolder
                    :              Environment.CurrentDirectory
                    ;

                Log("..  "+cmd_file +" "+cmd_args);

                ProcessStartInfo psinfo         = new ProcessStartInfo();
                psinfo.FileName                 = cmd_file;
                psinfo.Arguments                = cmd_args;
                psinfo.CreateNoWindow           = true;
                psinfo.UseShellExecute          = true;

                Process proc    = new Process();
                proc.StartInfo  = psinfo;
                proc.Start();

                // RESYNC COMBOBOX ENTRY TO CURRENT PROFILE
                D_Profiles.Text =  profile.CurrentProfileName;
            }
            else if(item_string == Globals.MENU_NEW_PROFILE)
            {
                profile.CurrentProfileName = "";

                profile.EditProfile( item_string );
            }
            else {
                profile.SelectProfile( item_string );
                update_notifyIcon(profile.CurrentProfileName+" profile");

                // Repaint G_DX1_KEYS
                G_DX1_KEYS.Invalidate();
                G_DX1_KEYS.AutoResizeColumns();

                ApplyKeySet("D_ProfilesCB");
            }
        }
        /*}}}*/

    // KEYS
        /*_ G_CellMouseDown_CB {{{*/
        private void G_CellMouseDown_CB(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                select_keyNum(e.RowIndex + 1);

        }
        /*}}}*/
        /*_ G_CellFormatting_CB {{{*/
        private void G_CellFormatting_CB(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // The CellFormatting event occurs every time each cell is painted
            if(e.Value == null) // unnassigned
                e.CellStyle.ForeColor = Color.Gray;

        }
        /*}}}*/
        /*_ G_RBMenu_Clear_CB {{{*/
        private void G_RBMenu_Clear_CB(object sender, EventArgs e)
        {
            if( DX1Utility.Debug ) Log(".. G_RBMenu_Clear_CB()");

            byte keyIndex = (byte)(G_DX1_KEYS.CurrentRow.Index);
            profile.initKeyMap( keyIndex );

            if(!C1_Profile_commit.Enabled) set_C1_Profile_commit_Checked( true );
            if(!C2_KeyMap_commit .Enabled) set_C2_KeyMap_commit_Checked ( true );
            if(!C4_Exit_onClose  .Enabled) set_C4_Exit_on_Close_Checked ( true );

            stopWizard("G_RBMenu_Clear_CB");

        }
        /*}}}*/
        /*_ G_RBMenu_Properties_CB {{{*/
        private void G_RBMenu_Properties_CB(object sender, EventArgs e)
        {
            if( DX1Utility.Debug ) Log("G_RBMenu_Properties_CB()");

            stopWizard("G_RBMenu_Properties_CB");

            byte keyIndex = (byte)(G_DX1_KEYS.CurrentRow.Index);
            select_CurrentKeyMap( profile.keyMapList[keyIndex] );

        }
        /*}}}*/
        /*_ G_RBMenu_Wizard_CB {{{*/
        private void G_RBMenu_Wizard_CB(object sender, EventArgs e)
        {
            if( DX1Utility.Debug ) Log("G_RBMenu_Wizard_CB()");

            stopWizard("G_RBMenu_Wizard_CB");

            byte keyIndex   = (byte)(G_DX1_KEYS.CurrentRow.Index);
            select_CurrentKeyMap( profile.keyMapList[keyIndex] );

        }
        /*}}}*/
        /*_ B_KeyProgrammer_CB {{{*/
        private void B_KeyProgrammer_CB(object sender, EventArgs e)
        {
            if(B_KeyProgrammer.Text == Globals.B_START_MAPPING_TEXT)
                startWizard();
            else
                stopWizard("B_KeyProgrammer_CB");

        }
        /*}}}*/

    // MACROS
    /*_ MacroList_CB {{{*/
    private void MacroList_CB(object sender, EventArgs e)
    {
        int index = MacroList.SelectedIndex;
        if(index <= 0) return;

        // Repaint G_DX1_KEYS
        G_DX1_KEYS.Invalidate();
        G_DX1_KEYS.AutoResizeColumns();

        // profile.keyProgrammer
        if(profile.keyProgrammer.KeyNum > 0)
            profile.keyProgrammer.assignMacro(MacroList.SelectedItem.ToString(), ref profile.keyMapList);

        // CurrentKeyMap
        CurrentKeyMap.KeyCode   = 0;
        CurrentKeyMap.KeyDesc   = MacroList.SelectedItem.ToString();
        CurrentKeyMap.KeyType   = 0x3;
        CurrentKeyMap.MacName   = MacroList.SelectedItem.ToString();

        // Tab panel
        T_Description.Text      = CurrentKeyMap.KeyDesc;

    }
    /*}}}*/
    /*_ G_Special_CellMouseDown {{{*/
    private void G_Special_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        // Force selection of the row that was clicked on no matter which mouse button
        if(e.RowIndex >= 0)
        {
            G_Special.CurrentCell = G_Special.Rows[e.RowIndex].Cells[e.ColumnIndex];

            // additional information
            if(specialKeyPlayer.SpecialKeys[G_Special.CurrentRow.Index].ReqData)
            {
                switch (specialKeyPlayer.SpecialKeys[G_Special.CurrentRow.Index].ExtraDataType)
                {
                    case SpecialKeysExtraData.Boolean:
                        R_True .Visible = true;
                        R_False.Visible = true;

                        R_True .Text = specialKeyPlayer.SpecialKeys[G_Special.CurrentRow.Index].ExtraDataParams["True" ];
                        R_False.Text = specialKeyPlayer.SpecialKeys[G_Special.CurrentRow.Index].ExtraDataParams["False"];
                        break;
                    default:
                        break;
                }
            }

            // Assign Default KeyDesc of DX1 Key to current Special Key name
            CurrentKeyMap.KeyType   = 4;
            CurrentKeyMap.KeyCode   = (byte)G_Special.CurrentRow.Index;
            CurrentKeyMap.KeyDesc   = G_Special.Rows[G_Special.CurrentRow.Index].Cells[0].Value.ToString();

            T_Description.Text      = CurrentKeyMap.KeyDesc;

        }

    }
    /*}}}*/

    // DIALOG
    /*_ ProcessDialogKey {{{*/
    protected override bool ProcessDialogKey(Keys keyData)
    {
        if(profile.keyProgrammer.KeyNum > 0)
            return false;
        return base.ProcessDialogKey( keyData );
    }
    /*}}}*/

    // DEBUG
        /*_ C_Debug_CB {{{*/
        private void C_Debug_CB(object sender, EventArgs e)
        {
            if (C_Debug.Checked) {
                DX1Utility.Debug = true;

                Log("LOG ALSO SAVED IN:\n"+LogFilePath);
            }
            else {
                DX1Utility.Debug = false;
            }
            Log("Debug"+ DX1Utility.Debug);

        }
        /*}}}*/
    /*_ B_Test_CB {{{*/
    private void B_Test_CB(object sender, EventArgs e)
    {
/*{{{
        dump_keyMapList();
}}}*/
/*{{{
        Log("===");
        Log("calling sendProgramPacket:");
        Log("===");
        dx1Device.sendProgramPacket( profile.keyProgrammer.get_keyMapList_num_type_code_byteArray( profile.keyMapList ) );
}}}*/

        Log("=================================================");
        Log("is_in_KEYMAP_MODE: "+dx1Device.is_in_KEYMAP_MODE());
        Log("");
        if( dx1Device.is_in_KEYMAP_MODE() ) {

            Log("calling leave_KEYMAP_MODE:");
            dx1Device.leave_KEYMAP_MODE();

/*{{{
            Log("calling sendProgramPacket(null):");
            dx1Device.sendProgramPacket(null);
}}}*/

/*{{{
            Log("calling sendProgramPacket( profile.keyMapList ):");
            dx1Device.sendProgramPacket( profile.keyProgrammer.get_keyMapList_num_type_code_byteArray( profile.keyMapList ) );
}}}*/
        }
        else {
            Log("calling enter_KEYMAP_MODE:");
            dx1Device.enter_KEYMAP_MODE();
        }
        Log("");
        Log("is_in_KEYMAP_MODE: "+dx1Device.is_in_KEYMAP_MODE());
        Log("=================================================");

    }
    /*}}}*/
    /*_ dump_keyMapList {{{*/
    //............................    A     B     C     D     E  PGUP RIGHT, NUM7
    //............................   65,   66,   67,   68,   69    33    39   103
    private byte[] KeyCodeList = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x21, 0x27, 0x67 };

    private void dump_keyMapList()
    {

        input.type = SpecialKeyPlayer.INPUT_KEYBOARD;

        ushort keyCode;

        Log("=== keyMapList =================================:");
        int count = 0;
        foreach(KeyMap keyMap in profile.keyMapList) {
            keyCode = (ushort)keyMap.KeyCode;
            Log("keyCode=["+ keyCode +"] .. "+ keyMap.ToString());
            key_press( keyCode );
            if(++count == 5) break;
        }

        Log("=== KeyCodeList =================================:");
        foreach(byte index in KeyCodeList)
        {
            keyCode = KeyConversionTable.KeyPairConversionTable[ index ][1];
            Log("index=["+ index +" 0x"+index.ToString("X2")+"] .. keyCode=["+ keyCode +" 0x"+keyCode.ToString("X2")+"]");
            key_press( keyCode );
        }

    }
    /*}}}*/
    // key_press {{{
    //{{{
    private const int KEY_DOWN_DURATION         = 30;
    private const int KEY_UP_DURATION           = 50;
    uint                             F_KEYUP    = SpecialKeyPlayer.KEYEVENTF_KEYUP;
    uint                             F_KEYDN    = 0;
    uint                             F_SCANCODE = SpecialKeyPlayer.KEYEVENTF_SCANCODE;

    List<SpecialKeyPlayer.KMH_INPUT> inputList  = new List<SpecialKeyPlayer.KMH_INPUT>();
    SpecialKeyPlayer.KMH_INPUT       input      = new      SpecialKeyPlayer.KMH_INPUT();

    //}}}
    private void key_press(ushort keyCode)
    {
        bool use_scancode = true;
/*{{{
        bool use_scancode = false;
}}}*/

        // KEY DOWN .. (KEY_DOWN_DURATION)
        //.....SCANCODE.............SC-VK ..KEYCODE...........DWFLAGS...F_KEYDN.....SCANCODE
        if(use_scancode) { input.ki.wScan = keyCode; input.ki.dwFlags = F_KEYDN | F_SCANCODE; }
        else             { input.ki.wVk   = keyCode; input.ki.dwFlags = F_KEYDN             ; }
        inputList.Add( input );

        send_inputList( inputList );
        Thread.Sleep( KEY_DOWN_DURATION );
        inputList.Clear();

        // KEY UP .. (KEY_UP_DURATION)
        //.....SCANCODE.............SC-VK ..KEYCODE...........DWFLAGS...F_KEYDN.....SCANCODE
        if(use_scancode) { input.ki.wScan = keyCode; input.ki.dwFlags = F_KEYUP | F_SCANCODE; }
        else             { input.ki.wVk   = keyCode; input.ki.dwFlags = F_KEYUP             ; }
        inputList.Add(input);

        send_inputList( inputList );
        Thread.Sleep(KEY_UP_DURATION);
        inputList.Clear();
    }
    //}}}
    /*_ send_inputList {{{*/
    private void send_inputList(List<SpecialKeyPlayer.KMH_INPUT> inputList)
    {
        SpecialKeyPlayer.KMH_INPUT[] inputArray = inputList.ToArray();

        if(inputArray.Length > 0)
        {
            int size = Marshal.SizeOf( inputArray[0] );

            NativeMethods.SendInput((uint)inputArray.Length, inputArray, size);

        }
    }
    /*}}}*/

    /* NativeMethods {{{*/
    internal static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
            public static extern uint SendInput(uint nInputs, SpecialKeyPlayer.KMH_INPUT[] pInputs, int cbSize);

    }
    /*}}}*/

#endregion //}}}
#region LOG {{{
        //{{{
        private System.IO.StreamWriter   log_sw;

        //}}}
        /*_ log {{{*/
        public  void log(string msg)
        {
            // STANDOUT {{{
            if( msg.IndexOf("***") >= 0) msg = "\n"+ msg;

            //}}}
            // LOG TO FILE {{{
            log_ToFile( msg );

            //}}}
            // LOG TO SCREEN {{{
            if(T_LOG != null) {
                if( this.InvokeRequired )
                    this.Invoke( (MethodInvoker)delegate() { log_T_LOG(Globals.SYMBOL_SUN+" "+msg); } );
                else                                         log_T_LOG(                       msg);
            }
            //}}}
        }
        /*}}}*/
        /*_ LogFilePath {{{*/
        private string LogFilePath
        {
            get {
                if(  Environment.UserInteractive )
                    return Globals.UserProfileFolder
                        +   APP_WINDOW_TITLE
                        +   ".log"
                        ;
                else
                    return Globals.UserProfileFolder
                        +   APP_WINDOW_TITLE +"_UNATTENDED"
                        +   ".log"
                        ;
            }
        }
        /*}}}*/
        /*_ Log {{{*/
        private void Log(string msg)
        {
            Logger.Log(msg); // UI thread-safe

        }
        /*}}}*/
        /*_ log_T_LOG {{{*/
        private void log_T_LOG(string msg)
        {
            if(!msg.EndsWith("\n") )     msg += "\n";
            T_LOG.AppendText( msg );

            T_LOG.SelectionStart = T_LOG.Text.Length;
            T_LOG.ScrollToCaret();
        }
        /*}}}*/
        /*_ log_ToFile {{{*/
        private void log_ToFile(string msg)
        {
            if(log_sw == null) log_FileOpen();

            log_sw.WriteLine("{0}", msg);
            log_sw.Flush();
        }
        /*}}}*/
        /*_ log_FileOpen {{{*/
        private void log_FileOpen()
        {
            log_sw = new System.IO.StreamWriter(LogFilePath, true);

        }
        /*}}}*/
        /*_ log_FileClose {{{*/
        private void log_FileClose()
        {
            if(log_sw != null)
                log_sw.Close();

        }
        /*}}}*/
        /*_ log_Clear {{{*/
        private void log_Clear()
        {
            if(T_LOG != null)
                T_LOG.Clear();

        }
        /*}}}*/
        /*_ log_DeleteFile {{{*/
        private void log_DeleteFile()
        {
            string filePath = LogFilePath;
            if( System.IO.File.Exists( filePath ) )
                System.IO.File.Delete( filePath );

        }
        /*}}}*/

#endregion //}}}
    }
#region NativeMethods {{{
    /* NativeMethods {{{*/
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
            public static extern int GetKeyboardState(byte[] lpKeyState);

    }
    /*}}}*/
#endregion //}}}
}
/* DOC {{{
:new             $LOCAL/STORE/DEV/PROJECTS/BAK/DX1Utility-1_52/Readme.txt
:!start explorer "C:\LOCAL\STORE\DEV\PROJECTS\BAK\DX1Utility-1_52"

# BANNER
:!start explorer "http://patorjk.com/software/taag/\#p=display&f=Electronic&t=LOG"

:!start explorer "https://code.google.com/p/ew-ergodex-dx1-driver"
:!start explorer "https://github.com/EarthwormO/ew-ergodex-dx1-driver/wiki"
:!start explorer "https://polygonalhell.blogspot.com/2009/01/new-32-and-64-bit-ergodex-dx1-drivers.html"

:!start explorer "https://groups.google.com/forum/#!search/Ergodex/techlunch/qWT2UNliBZQ/a4T7_FasJNoJ"
:!start explorer "https://github.com/EarthwormO/ew-ergodex-dx1-driver/blob/master/HardwareInterface.cs"
:!start explorer ""
}}}*/

