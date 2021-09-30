#region using {{{
using System;
using System.Threading;
using System.Windows.Forms;
#endregion //}}}
namespace DX1Utility
{
    public partial class DX1Utility : Form, LoggerInterface {
#region KEYMAP {{{
        //{{{
        const         int    FOCUS_LOST_APPLY_DELAY_SEC = 5;

        private       Thread ApplyKeySet_thread         = null;
        //}}}
        public  void ApplyKeySet(string caller) //{{{
        {
            if( DX1Utility.Debug ) Log("ApplyKeySet"+Globals.SYMBOL_ARROW_L+" "+caller);
            if( DX1Utility.Debug ) Log("DX1UtilityHasFocus "+DX1UtilityHasFocus);

            // 1/2 - PUSH PENDING COMMIT .. (on shutdown or loosing focus) {{{
            if(!DX1UtilityHasFocus || ShutDown_Requested)
            {
                ApplyKeySet_post();

            }
            //}}}
            // 2/2 - DISPLAY PENDING COMMIT {{{
            else {
                // CANCEL PENDING UPDATES {{{
                if(ApplyKeySet_thread != null)
                {
                    ApplyKeySet_thread = null;

                    Log(Globals.SYMBOL_ARROW_L+"UPDATES CANCELED .. NOW UNDER USER CONTROL");
                }
                //}}}
                // LOG PENDING UPDATES TO COMMIT {{{
                string msg_line1 = "";
                if(profile.keyProgrammer.Profile_changeToCommit != "")
                    msg_line1 = "PENDING UPDATE: PROFILE ["+profile.CurrentProfileName+"] .. ("+profile.keyProgrammer.Profile_changeToCommit+")";
                else
                    if( DX1Utility.Debug ) Log(".. no pending ["+profile.CurrentProfileName+"] PROFILE UPDATE");

                string msg_line2 = "";
                if(profile.keyProgrammer.KeyMap_changeToCommit != "")
                    msg_line2 = "PENDING UPDATE: KEYMAP ["+profile.CurrentProfileName+"] ..  ("+profile.keyProgrammer.KeyMap_changeToCommit+")";
                else
                    if( DX1Utility.Debug ) Log(".. no pending ["+profile.CurrentProfileName+"] KEYMAP UPDATE");

                if(msg_line1 != "") Log(msg_line1);
                if(msg_line2 != "") Log(msg_line2);

                //}}}
            }
            //}}}
            // UI COLORS AND FOCUS {{{
            stopWizard("ApplyKeySet"+Globals.SYMBOL_ARROW_L+" "+caller);

            if( ShutDown_Requested )
                SyncUI.sync_colors();

            G_DX1_KEYS.Focus();
            //}}}
        } //}}}
        private void ApplyKeySet_post() //{{{
        {
            if(ApplyKeySet_thread == null)
            {
                // [something_to_commit] {{{
                bool something_to_commit
                    =  (profile.keyProgrammer.Profile_changeToCommit != "")
                    || (profile.keyProgrammer.KeyMap_changeToCommit  != "")
                    ;

                if(!something_to_commit)
                {
                    if( DX1Utility.Debug ) Log("NOTHING TO UPDATE");

                    return;
                }
                //}}}
                ApplyKeySet_thread = new Thread(new ThreadStart( ApplyKeySet_handler ));

                ApplyKeySet_thread.Start();
            }
        } //}}}
        public  bool ApplyKeySet_posted() //{{{
        {
            return (ApplyKeySet_thread != null) ;
        }
        //}}}
        private void ApplyKeySet_handler() //{{{
        {
            // CANCELED BEFORE COUNTDOWN {{{
            if(ApplyKeySet_thread == null)
            {
                Log("UPDATES .. CANCELED BEFORE COUNTDOWN");

                return;
            }
            //}}}
            // [something_to_commit] {{{
            bool something_to_commit
                =  (profile.keyProgrammer.Profile_changeToCommit != "")
                || (profile.keyProgrammer.KeyMap_changeToCommit  != "")
                ;

            if(!something_to_commit)
            {
                if( DX1Utility.Debug ) Log("UPDATES .. NONE");

                return;
            }
            else {
                L_STANDBY.Text = ""; // CLEAR PROGRESS BAR
            }
            //}}}
            // SHUTDOWN .. OR FOCUS LOST DELAY {{{
            if(!ShutDown_Requested )
            {
                int delay_sec
                    = (cmdLine_apply_delay_sec > 0)
                    ?  cmdLine_apply_delay_sec
                    :  FOCUS_LOST_APPLY_DELAY_SEC;

                cmdLine_apply_delay_sec = -1; /* consume single usage command-line argument */

                if( Environment.UserInteractive )
                    Log("UPDATES .. TO BE APPLIED IN "+delay_sec+"s");

                ApplyKeySet_countDown( delay_sec );
            }
            //}}}
            // CANCELED DURING COUNTDOWN {{{
            if(ApplyKeySet_thread == null)
            {
                Log(Globals.SYMBOL_ARROW_L+Globals.SYMBOL_ARROW_L+" UPDATES .. CANCELED BY USER DURING COUNTDOWN");

                return;
            }
            else {
                // USE PENDING COLOR FOR STANDBY CHECKMARK
                L_STANDBY.ForeColor = L_WRITING.ForeColor;

                Log("UPDATES .. APPLYING");
            }
            //}}}
            //{{{ [apply_cause]
            string apply_cause
                =     (!DX1UtilityHasFocus ? "FOCUS LOST" : "")
                +" "+ ( ShutDown_Requested ? "SHUTDOWN"   : "")
                ;

            apply_cause = apply_cause.Trim();
            //}}}
            // COMMIT PROFILE UPDATE {{{
            if(profile.keyProgrammer.Profile_changeToCommit != "")
            {
                if( DX1Utility.Debug ) Log("===");

                Log("UPDATING PROFILE: "+profile.CurrentProfileName+"");

                if( DX1Utility.Debug ) Log(".. commited by ["+profile.keyProgrammer.Profile_changeToCommit+" ON "+apply_cause+"]");

                ProfileManager.SaveProfile(profile.CurrentProfileName, profile.keyMapList);

                Log(Globals.SYMBOL_ARROW_L+Globals.SYMBOL_ARROW_L+" PROFILE UPDATE .. DONE");

                if( DX1Utility.Debug ) Log("===");
            }
            else {
                if( DX1Utility.Debug ) Log("===");
                if( DX1Utility.Debug ) Log("PROFILE UNCHANGED: ["+profile.CurrentProfileName+"]");
                if( DX1Utility.Debug ) Log("===");
            }
            //}}}
            // COMMIT KEY MAPPINGS {{{
            if(profile.keyProgrammer.KeyMap_changeToCommit != "")
            {
                if( DX1Utility.Debug ) Log("===");

                Log("SENDING KEYMAP: ["+profile.CurrentProfileName+"]");

                if( DX1Utility.Debug ) Log(".. commited by ["+profile.keyProgrammer.KeyMap_changeToCommit+" ON "+apply_cause+"]");

                update_notifyIcon( profile.CurrentProfileName+" profile\n.. SENDING KEYMAP");

                dx1Device.sendProgramPacket( profile.keyProgrammer.get_keyMapList_num_type_code_byteArray( profile.keyMapList ) );

                Log(Globals.SYMBOL_ARROW_L+Globals.SYMBOL_ARROW_L+" KEYSET  UPDATE .. DONE");

                if( DX1Utility.Debug ) Log("===");
            }
            else {
                if( DX1Utility.Debug ) Log("===");
                if( DX1Utility.Debug ) Log("KEYMAP UNCHANGED: ["+profile.CurrentProfileName+"]");
                if( DX1Utility.Debug ) Log("===");
            }
            //}}}
            /* 1/2 - EXIT FROM UNATTENDED SERVICE MODE {{{*/
            ApplyKeySet_thread = null;
            if(!Environment.UserInteractive )
            {
                ShutDown_Requested = true;
                exitApp_Handler();
            }
            /*}}}*/
            // 2/2 - OR CLEAR UI COMMIT TRIGGERS {{{
            if( something_to_commit )
                L_STANDBY.Text = Globals.SYMBOL_CHECK; // CHECK PROGRESS BAR

          //                                         profile.keyProgrammer.notify_all_commit_done();
            this.Invoke( (MethodInvoker)delegate() { profile.keyProgrammer.notify_all_commit_done(); } );

            if(C2_KeyMap_commit .Checked ) set_C2_KeyMap_commit_Checked ( false );
            if(C1_Profile_commit.Checked ) set_C1_Profile_commit_Checked( false );

            SyncUI.sync("ApplyKeySet_handler");
            //}}}
        } //}}}
        private void ApplyKeySet_countDown(int delay_sec) //{{{
        {
            int countDown = delay_sec;
            string text;
            do {
                text = "";

                if(countDown > 5) {
                    text = countDown+"s";
                }
                else {
                    for(int i = 0; i < countDown; ++i)
                        text += "o";
                }

                ApplyKeySet_progress( text );
                Thread.Sleep( 1000 );
            }
            while( (--countDown > 0)
                && (ApplyKeySet_thread != null));

            ApplyKeySet_progress("");

        } //}}}
        private void ApplyKeySet_progress(string text) //{{{
        {
            Log( text );

            if( this.InvokeRequired )
                this.Invoke( (MethodInvoker)delegate() { L_WRITING.Text = text; } );
            else                                         L_WRITING.Text = text;
        }//}}}
#endregion //}}}
    }
}
