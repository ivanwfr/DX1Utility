#region using {{{
using System.Threading;
using System.Windows.Forms;
using System;
using System.Drawing;
#endregion //}}}
namespace DX1Utility {
    static class SyncUI {
        // const {{{
        private const  int     FG_UPDATE_DELAY      = 200;
        private const  int     BG_UPDATE_DELAY      = 500;

        private static Color   UI_BLACK             = Color.FromArgb(  0,   0,   0);
        private static Color   UI_WHITE             = Color.FromArgb(255, 255, 255);
        private static Color   UI_TRANSPARENT       = Color.Transparent;

        private static Color   UI_RED               = Color.FromArgb(224, 192, 192);
        private static Color   UI_BLUE              = Color.FromArgb(168, 192, 224);
        private static Color   UI_MAGENTA           = Color.FromArgb(224, 192, 224);
        private static Color   UI_GREEN             = Color.FromArgb(128, 255, 128);
        private static Color   UI_GRAY              = Color.FromArgb(224, 224, 224);

        private static ToolTip toolTip;
        private static Thread  SyncUI_colors_thread = null;
        private static string  Last_led_indicators  = "";

        //}}}
        // var {{{
        private static DX1Utility ui = null;
        private static Profile    profile    = null;

        //}}}
#region SYNC {{{
        public  static void Set_Form_Profile(DX1Utility formHandle, Profile _profile) //{{{
        {
            ui      = formHandle;
            profile = _profile;
        }
        //}}}
        public  static void sync(string caller) //{{{
        {
            if( DX1Utility.Debug ) log("sync"+Globals.SYMBOL_ARROW_L+" "+caller);

            // SYNC LED INDICATORS {{{
            ui.L_ON     .Enabled = (ui.dx1Device != null) &&  ui.dx1Device.is_ON();
            ui.L_REC    .Enabled = (ui.dx1Device != null) &&  ui.dx1Device.is_in_KEYMAP_MODE() && !ui.L_WRITING.Enabled;
            ui.L_STANDBY.Enabled = (ui.dx1Device != null) && !ui.L_WRITING.Enabled             && !ui.L_REC    .Enabled;
            ui.L_WRITING.Enabled = (ui.dx1Device != null) &&  ui.ApplyKeySet_posted();

            // LOG INDICATORS WHEN CHANGED
            string this_led_indicators
                = (ui.L_ON     .Enabled ? "ON"              : "OFF")
                + (ui.L_REC    .Enabled ? "RECORDING"       :    "")
                + (ui.L_STANDBY.Enabled ? "PLAYING"         :    "")
                + (ui.L_WRITING.Enabled ? "PENDING UPDATES" :    "")
                ;

            if(this_led_indicators != Last_led_indicators)
            {
                if( DX1Utility.Debug ) {
                    log("sync "+ Globals.SYMBOL_ARROW_L+" "+caller);//FIXME
                    log((ui.L_ON     .Enabled ? Globals.SYMBOL_CHECK : "..") + " ui.L_ON"     );//FIXME
                    log((ui.L_STANDBY.Enabled ? Globals.SYMBOL_CHECK : "..") + " ui.L_STANDBY");//FIXME
                    log((ui.L_REC    .Enabled ? Globals.SYMBOL_CHECK : "..") + " ui.L_REC"    );//FIXME
                    log((ui.L_WRITING.Enabled ? Globals.SYMBOL_CHECK : "..") + " ui.L_WRITING");//FIXME
                    log("");
                }

                Last_led_indicators     = this_led_indicators;
            }

            // SHOW ENABLED INDICATORS
            ui.L_WRITING.Visible = ui.L_WRITING.Enabled;
            ui.L_REC    .Visible = ui.L_REC    .Enabled;
          //ui.L_STANDBY.Visible = ui.L_STANDBY.Enabled;

            //}}}
            // DataGridView {{{
            if( ui.InvokeRequired )
                ui.Invoke( (MethodInvoker)delegate() { sync_G_DX1_KEYS(); } );
            else                                       sync_G_DX1_KEYS();
            //}}}
            // [ui.B_KeyProgrammer] STATE .. (sync UI UPDATE){{{
            if( ui.L_REC.Enabled )
            {
                if(profile.keyProgrammer.KeyNum > 0)
                {
                    ui.B_KeyProgrammer.Text      = ".. mapping DX1 key #"+ profile.keyProgrammer.KeyNum;
                    ui.B_KeyProgrammer.BackColor = Color.Red;
                    ui.B_KeyProgrammer.ForeColor = UI_WHITE;
                }
                else {
                    ui.B_KeyProgrammer.Text      = ".. mapping DX1 key\n"+"STOPPED in KEYMAP_MODE";
                }

                if( DX1Utility.Debug ) log( ui.B_KeyProgrammer.Text );
            }
            else if(ui.B_KeyProgrammer.Text     != Globals.B_START_MAPPING_TEXT)
            {
                ui.B_KeyProgrammer.Text          = Globals.B_START_MAPPING_TEXT;
                ui.B_KeyProgrammer.BackColor     = UI_TRANSPARENT;
                ui.B_KeyProgrammer.ForeColor     = UI_BLACK;

            }
            //}}}
            // UI COLOR POST .. (async UI UPDATE) {{{
            sync_colors_post();

            //}}}
        } //}}}
        private static void sync_G_DX1_KEYS() //{{{
        {
            ui.G_DX1_KEYS.AutoGenerateColumns= false;
            ui.G_DX1_KEYS.DataSource         = null;
            ui.G_DX1_KEYS.DataSource         = profile.keyMapList;
            ui.G_DX1_KEYS.AutoResizeColumns();
        } //}}}
        private static void sync_colors_post() //{{{
        {
            if(SyncUI_colors_thread == null)
            {
                SyncUI_colors_thread = new Thread(new ThreadStart( sync_colors_handler ));
                SyncUI_colors_thread.Start();
            }
        } //}}}
        private static void sync_colors_handler() //{{{
        {
            string caller = "sync_colors_handler";
            int delay
                = ui.DX1UtilityHasFocus
                ?  FG_UPDATE_DELAY
                :  BG_UPDATE_DELAY;

            if( DX1Utility.Debug ) log(caller+": Sleep("+ delay +"):");
            Thread.Sleep( delay );

            if( DX1Utility.Debug ) log(caller+": .. calling sync_colors:");

            if( ui.InvokeRequired )
                ui.Invoke( (MethodInvoker)delegate() { sync_colors(); } );
            else                                       sync_colors();

            SyncUI_colors_thread = null;

        } //}}}
        public  static void sync_colors()//{{{
        {
            // CURRENT KEY  {{{
            KeyMap keyMap
                = (            profile.keyProgrammer.KeyNum > 0 )
                ?  profile.keyMapList[ profile.keyProgrammer.KeyNum - 1 ]
                :       ui.CurrentKeyMap;

            if( ui.ShutDown_Requested ) {
                    ui.T_Description.Text = "SHUTDOWN";
                    ui.T_SingleKey  .Text = profile.CurrentProfileName;
                    ui.T_Conf_Code  .Text = "";
                    ui.T_Conf_Type  .Text = "";
            }
            else {
                if(keyMap.TypeToString() != "") {
                    ui.T_Description.Text = keyMap.KeyDesc;
                    ui.T_SingleKey  .Text = keyMap.KeyName;
                    ui.T_Conf_Code  .Text = keyMap.KeyCode.ToString();
                    ui.T_Conf_Type  .Text = keyMap.TypeToString();
                }
                else {
                    ui.T_Description.Text = "";
                    ui.T_SingleKey  .Text = "";
                    ui.T_Conf_Code  .Text = "";
                    ui.T_Conf_Type  .Text = "";
                }
            }
            //}}}
            //  SHUTDOWN {{{
            if( ui.ShutDown_Requested )
            {
                ui                .BackColor = UI_GRAY;
                ui                .ForeColor = UI_WHITE;
                /*{{{
                  ui.B_ShutDown     .ForeColor = UI_WHITE;
                  ui.B_KeyProgrammer.ForeColor = UI_WHITE;
                  ui.G_DX1_KEYS     .ForeColor = UI_WHITE;
                  }}}*/
                ui.G_DX1_KEYS.CurrentCell = null;
            }
            //}}}
            // PENDING COMMITS {{{
            else {
                // COLORS: {{{
                bool keyMap_to_commit             = (profile.keyProgrammer.KeyMap_changeToCommit  != "");
                bool profile_to_commit            = (profile.keyProgrammer.Profile_changeToCommit != "");
                bool keyMap_and_profile_to_commit =  keyMap_to_commit && profile_to_commit;

                Color border_color
                    = keyMap_and_profile_to_commit ? Color.Magenta
                    : keyMap_to_commit             ? Color.Red
                    : profile_to_commit            ? Color.Blue
                    : ui.C4_Exit_onClose.Checked   ? Color.White
                    :                                Color.Green
                    ;

                Color back_color
                    = keyMap_and_profile_to_commit ? UI_MAGENTA
                    : keyMap_to_commit             ? UI_RED
                    : profile_to_commit            ? UI_BLUE
                    : ui.C4_Exit_onClose.Checked   ? Color.Black
                    :                                UI_GREEN
                    ;

                //}}}
                // KEYMAP CHECKBOX STATE {{{

                ui.set_C2_KeyMap_commit_Checked( keyMap_to_commit, Globals.CHECKED_PASSIVE);

                //}}}
                // COLORS: BORDERS .. LOGO .. MIMINIZE {{{
                ui.P_border_U.BackColor   = border_color;
                ui.P_border_R.BackColor   = border_color;
                ui.P_border_D.BackColor   = border_color;
                ui.P_border_L.BackColor   = border_color;

                //}}}
                // COLORS: SHUTDOWN BUTTON {{{
                ui.B_ShutDown.FlatAppearance.BorderColor = border_color;
                ui.B_ShutDown   .BackColor               = back_color;
                ui.B_ShutDown   .ForeColor               = border_color;

                ui.L_WRITING.ForeColor                   = border_color;
                //}}}
                // COLORS: CHECKBOXES .. KEYS .. PROFILE .. EXIT {{{
                ui.C2_KeyMap_commit   .ForeColor = Color.Red;
                ui.C1_Profile_commit  .ForeColor = Color.Blue;
                ui.C4_Exit_onClose    .ForeColor = Color.Black;

                ui.L_STATUS.BackColor = back_color;
                ui.L_STATUS.ForeColor = border_color;

                //}}}
                // TEXT: KEYS .. PROFILE .. EXIT {{{
                string     label_keys = (profile.keyProgrammer.KeyMap_changeToCommit  != "") ? " "+Globals.SYMBOL_ARROW_R+" Send\nKeys"     : Globals.SYMBOL_CHECK+" do not\nSend Keys";
                string  label_profile = (profile.keyProgrammer.Profile_changeToCommit != "") ? " "+Globals.SYMBOL_ARROW_R+" Save\nProfile"  : Globals.SYMBOL_CHECK+" do not\nSave Profile";
                string     label_exit =  ui.C4_Exit_onClose.Checked                          ? " "+Globals.SYMBOL_ARROW_R+" Exit\non Close" : Globals.SYMBOL_CHECK+" SysTray\non close";

                string text = label_keys   +"\n\n"
                    +         label_profile+"\n\n"
                    +         label_exit;

                string   tt = text.Replace("\n","\r\n");

                if( ui.InvokeRequired )
                    ui.Invoke( (MethodInvoker)delegate() { ui.B_ShutDown.Text = text; toolTip.SetToolTip(ui.B_ShutDown, tt); } );
                else                                       ui.B_ShutDown.Text = text; toolTip.SetToolTip(ui.B_ShutDown, tt);

                //}}}
                // OPACITY: COMMIT .. FOCUS {{{
                bool pending
                    =  (profile.keyProgrammer.KeyMap_changeToCommit  != "")
                    || (profile.keyProgrammer.Profile_changeToCommit != "")
                    ;

                ui.Opacity
                    = ui.L_WRITING.Enabled  ?            Globals.OPACITY_WRITING
                    : ui.DX1UtilityHasFocus ? (pending ? Globals.OPACITY_PROFILE : Globals.OPACITY_FOCUS)
                    :                                    Globals.OPACITY_UPDATED
                    ;

                //}}}
            } //}}}
        } //}}}
        public  static void set_tooltips() //{{{
        {
            toolTip              = new ToolTip();

            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay  =  500;
            toolTip.ShowAlways   = true; // .. on disable controls too

            if( ui.InvokeRequired )
                ui.Invoke( (MethodInvoker)delegate() { toolTip.SetToolTip(ui, DX1Utility.APP_WINDOW_TITLE); } );
            else                                       toolTip.SetToolTip(ui, DX1Utility.APP_WINDOW_TITLE);

        } //}}}
#endregion //}}}
#region EVENT {{{
        public  static void toolTip_MouseEnter(object sender, EventArgs e) //{{{
        {
            //{{{
            Control control = sender as Control;
            //}}}
            // FORM & LOGO .. (handled elsewhere) {{{
            if((sender == ui) || (sender == ui.L_Logo) )
            {
                L_Status_show_text("");

                return;
            }
            //}}}
            // ALREADY SET {{{
            string tt_text = toolTip.GetToolTip( control );
            if(tt_text != "")
            {
//log("GetToolTip=["+tt_text+"] on "+control.ToString());//FIXME

                L_Status_show_text(tt_text.Replace("\r\n", " .. "));
                return;
            }
            //}}}
            // TOOLTIP TEXT {{{
            tt_text = control.AccessibleDescription;

            if((tt_text == null) || (tt_text == "") && (control.GetType() == typeof(Label))) tt_text = control.Name;
            else tt_text = tt_text.Replace("|", "\n");

            if((tt_text == null) || (tt_text == ""))
                    tt_text = control.ToString();

//log("tt_text=["+tt_text+"] on "+control.ToString());//FIXME
            if((tt_text == null) || (tt_text == ""))
                return;
            //}}}
            // APPLY .. Controls names {{{
            // [B]utton .. [C]heckBox .. [G]rid .. [L]abel .. [R]adio .. [T]ext
            tt_text
                = tt_text.Replace("System.Windows.Forms.",     "")
                .         Replace(                   "B_",     "")
                .         Replace(                   "C_",     "")
                .         Replace(                   "G_",     "")
                .         Replace(                   "L_",     "")
                .         Replace(                   "R_",     "")
                .         Replace(                   "T_",     "")
                .         Replace(               "Text: ",     "")
                .         Replace(                    "_",    " ")
                .         Trim()
                ;

            if( ui.InvokeRequired )
                ui.Invoke( (MethodInvoker)delegate() { toolTip.SetToolTip(control, tt_text.Replace("\n", "\r\n")); } );
            else                                       toolTip.SetToolTip(control, tt_text.Replace("\n", "\r\n"));
            //}}}
        }
        //}}}
#endregion }}}
#region STATUS {{{
        //{{{
        private const  int       STATUS_LINE_DURATION = 2000;

        private static Thread    L_Status_hide_thread = null;
        private static string    L_Status_persistant_text;
        //}}}
        public  static void L_Status_set_text(string text) //{{{
        {
            ui.L_STATUS.Text             = text;
            L_Status_persistant_text     = ui.L_STATUS.Text;
            toolTip.SetToolTip(ui.L_Logo,  L_Status_persistant_text);

        } //}}}
        public  static void L_Status_show_text(string tt_text) //{{{
        {
            if(tt_text != "") ui.L_STATUS.Text = tt_text;
            else              ui.L_STATUS.Text = L_Status_persistant_text;

        } //}}}
        public  static void L_Status_hide_post() //{{{
        {
            if( L_Status_hide_thread != null) {

                L_Status_hide_thread  = null;
            }

            L_Status_hide_thread = new Thread(new ThreadStart( L_Status_hide_Handler ));
            L_Status_hide_thread.Start();

        } //}}}
        public  static void L_Status_hide_Handler() //{{{
        {
            Thread.Sleep( STATUS_LINE_DURATION );
            if(L_Status_hide_thread  != null)
            {
                ui.L_STATUS.Text         = L_Status_persistant_text;
                L_Status_hide_thread  = null;
            }
        } //}}}
#endregion //}}}
        /* log {{{*/
        private static void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }
}
