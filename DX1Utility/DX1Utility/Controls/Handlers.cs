#region using {{{
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Threading;

using System.Text;
using System.Windows.Forms;

#endregion //}}}

namespace DX1Utility
{
    public partial class DX1Utility : Form {
#region Handlers_override {{{

        private void ReleaseFocus(string _caller)
        {
            log("ReleaseFocus called by "+_caller);
            OnDeactivate(null);
            DX1UtilityHasFocus = false;
        }
        private void TakeFocus(string _caller)
        {
            log("TakeFocus called by "+_caller);
            OnActivated(null);
        }
        protected override void OnActivated(System.EventArgs e) // {{{ focus
        {
            string caller = "OnActivated";

            //FIXME if( DX1Utility.Debug ) log_Clear();
            if( DX1Utility.Debug ) log("===============");
            if( DX1Utility.Debug ) log("= "+caller+"  =");
            if( DX1Utility.Debug ) log("===============");

            if( ShutDown_Requested ) return;

            // UI .. (update version age)
            this.Text = DX1Utility.APP_WINDOW_TITLE + Util.RetrieveLinkerTimestamp();

            if( DX1UtilityInstance.InvokeRequired )
                DX1UtilityInstance.Invoke( (MethodInvoker)delegate() { SyncUI.L_Status_set_text( this.Text ); } );
            else                                                       SyncUI.L_Status_set_text( this.Text );

            // UI .. (focus changed)
            DX1UtilityHasFocus = DX1UtilityInstance.ContainsFocus;
            if( DX1Utility.Debug ) log("OnActivated:    DX1UtilityInstance.ContainsFocus "+ DX1UtilityHasFocus);

            ApplyKeySet( caller );
        }
        //}}}
//        protected override void OnMouseEnter(System.EventArgs e) // {{{ focus
//        {
//            if( DX1Utility.Debug ) log("================");
//            if( DX1Utility.Debug ) log("= OnMouseEnter =");
//            if( DX1Utility.Debug ) log("================");
//
//            // CHECK UI focus state change
//            if( DX1Utility.Debug ) log("OnMouseEnter: DX1UtilityInstance.ContainsFocus "+ DX1UtilityInstance.ContainsFocus);
//            new Thread(new ThreadStart( contains_focus_Handler )).Start();
//        }//}}}
        //_ contains_focus_Handler {{{
        const int      CONTAINS_FOCUS_DELAY = 500;
        private void contains_focus_Handler()
        {
            string caller = "contains_focus_Handler";
            if( DX1Utility.Debug ) log(caller+": .. CHECKING [ContainsFocus] IN "+ CONTAINS_FOCUS_DELAY +"ms");
            Thread.Sleep( CONTAINS_FOCUS_DELAY );

            DX1UtilityHasFocus  = DX1UtilityInstance.ContainsFocus;
            if( DX1Utility.Debug ) log(caller+": DX1UtilityHasFocus "+ DX1UtilityHasFocus);

        } //}}}
        protected override void OnDeactivate(System.EventArgs e) // {{{ focus lost
        {
            string caller = "OnDeactivate";

            //FIXME if( DX1Utility.Debug ) log_Clear();
            if( DX1Utility.Debug ) log("================");
            if( DX1Utility.Debug ) log("= OnDeactivate =");
            if( DX1Utility.Debug ) log("================");

            // UI .. (focus changed)
            DX1UtilityHasFocus  = false;

            ApplyKeySet( caller );
        } //}}}
        protected override void OnResize(System.EventArgs e) // {{{
        {
            if( DX1Utility.Debug ) log("============");
            if( DX1Utility.Debug ) log("= OnResize =");
            if( DX1Utility.Debug ) log("============");
            F_Minimize_Sync();
        } //}}}
        protected override void OnKeyUp(KeyEventArgs e) // {{{
        {
            string caller = "OnKeyUp";
            bool log_this = DX1Utility.Debug;
            if(  log_this ) log_Clear();
            if(  log_this ) log(caller+"("+ e.KeyCode +"): DX1UtilityHasFocus=["+ DX1UtilityHasFocus +"]");

            // propagate grid event first {{{
            base.OnKeyUp(e);

            //}}}
            // [wizard_is_running] .. AssignSingleKey {{{
            bool wizard_is_running
                // (B_KeyProgrammer.Text != B_START_MAPPING_TEXT)
                =  dx1Device.is_in_KEYMAP_MODE()
                && (profile.keyProgrammer.KeyNum > 0)
                ;

            if( wizard_is_running )
            {
                e.SuppressKeyPress = true; // whether the key event should be passed on to the underlying control

                int keyCode=(int)e.KeyCode;

                if(log_this) log(".. mapping ["+e.KeyCode+"] to KEY #"+ profile.keyProgrammer.KeyNum);

                if( profile.keyMapList[profile.keyProgrammer.KeyNum-1].AssignSingleKey(keyCode, e.KeyCode.ToString()) )
                {
                    profile.keyProgrammer.notify_keyMap_KEY_CHANGED();
                    profile.keyProgrammer.notify_profile_KEYMAP_CHANGED();
                    if(!C2_KeyMap_commit .Checked) set_C2_KeyMap_commit_Checked ( true );
                    if(!C1_Profile_commit.Checked) set_C1_Profile_commit_Checked( true );
                }

                select_keyNum(profile.keyProgrammer.KeyNum + 1);
            }
            //}}}
            // [Delete] a macro file {{{
            else if( (e.KeyCode   == System.Windows.Forms.Keys.Delete)
                &&   (MacroList.Focused && MacroList.SelectedIndex != 0)
                ) {
                System.IO.File.Delete( Globals.ProfileMacroPath + MacroList.SelectedItem.ToString() + ".mac" );
                RebuildMacroList();

                SyncUI.sync("OnKeyUp");
            }
            //}}}
            // select current Grid cell {{{
            else {
                // vk {{{
                int       vk = (int)e.KeyCode;
                int       keyType = 1;
                // SHIFT, CTRL, ALT {{{
                if(vk >= 0x10 && vk <= 0x12)
                {
                    vk = 0xa0 + 2*(vk - 0x10);
                    keyType = 2;
if(log_this) log("MODIFIER..=["+vk+" "+vk.ToString("X2")+"]");
                }
                else {
if(log_this) log(".. .....vk=["+vk+" "+vk.ToString("X2")+"]");
                }
                //}}}

                //}}}
                // keyCode {{{
                int  keyCode = 0;
                byte[] keyTuple = KeyConversionTable.KeyPairConversionTable[ vk ];
                if(keyTuple != null) {
                    keyCode = keyTuple[1];
if(log_this) log(".. keyCode=["+keyCode+"]");
                }

                //}}}
                // keyNum {{{
                byte  keyNum = 0;
                if(keyCode > 0) {
                    foreach(KeyMap keyMap in profile.keyMapList)
                    {
                        if((keyMap.KeyType == keyType) && (keyMap.KeyCode == keyCode))
                        {
                            keyNum = (byte)keyMap.KeyNum;
if(log_this) log(".. .keyNum=["+keyNum+"] .. keyMap=["+keyMap.ToString()+"]");
                            break;
                        }
                    }
                }
                //}}}
                select_keyNum( keyNum );
            }
            //}}}
        }
        //}}}

#endregion //}}}
#region Move_and_Resize {{{
    // SET INITIAL LOCATION {{{
    private void TopLeft_to_ScreenTopCenter()
    {
        int s_w = Screen.PrimaryScreen.Bounds.Width;

        this.Top  =     0;
        this.Left = s_w/2;
    } //}}}
        // Form [MOVE .. RESIZE] {{{
        // variables {{{
        private const int   MAGNET_SIZE = 30;
        public  bool        locked      = false;
        private bool        RESIZABLE   = false;

        private const int   GRIP_SIZE   = 30;

        private bool        dragging    = false;
        private bool        moving      = false;

        private bool        resize_B    = false;
        private bool        resize_L    = false;
        private bool        resize_R    = false;
        private bool        resize_T    = false;

        private Point       dragPoint   = new Point();
        private Rectangle   origin      = new Rectangle();
        //}}}
        protected override void OnMouseUp(MouseEventArgs e)// {{{
        {
log("OnMouseUp");//FIXME
            dragging    = false;
            set_hasMoved(false, "OnMouseUp");
        }
        //}}}
        protected override void OnMouseDown(MouseEventArgs e)// {{{
        {
            string caller = "OnMouseDown";
log(caller);//FIXME
            set_hasMoved(false, caller);

            if( F_OnMouseDown_disabledControl(e, "dragging") != "") return;
            if( locked                                            ) return;
            if( dragging                                          ) return; // already on it

            dragging        = true;
            dragPoint       = PointToScreen(e.Location);
            origin.X        = Location.X;
            origin.Y        = Location.Y;
            origin.Width    = Size.Width;
            origin.Height   = Size.Height;

            if( RESIZABLE ) {
                if(e.X > (Size.Width -GRIP_SIZE)) resize_R = true;
                if(e.X <              GRIP_SIZE ) resize_L = true;
                if(e.Y <              GRIP_SIZE ) resize_T = true;
                if(e.Y > (Size.Height-GRIP_SIZE)) resize_B = true;
            }

            moving = (!resize_T && !resize_B && !resize_L && !resize_R);

            if         (resize_T) {
                if     (resize_L)           Cursor.Current = Cursors.SizeNWSE;
                else if(resize_R)           Cursor.Current = Cursors.SizeNESW;
                else                        Cursor.Current = Cursors.SizeNS;
            }
            else if(    resize_B) {
                if     (resize_L)           Cursor.Current = Cursors.SizeNESW;
                else if(resize_R)           Cursor.Current = Cursors.SizeNWSE;
                else                        Cursor.Current = Cursors.SizeNS;
            }
            else if(resize_L || resize_R)   Cursor.Current = Cursors.SizeWE;
            else                            Cursor.Current = Cursors.SizeAll;

        }
        //}}}
        protected override void OnMouseMove(MouseEventArgs e)// {{{
        {
            if(locked ) return;
            // update {{{
            if(dragging) {
                set_hasMoved(true, "OnMouseMove");

                Point p = PointToScreen(e.Location);

                int                         dx = p.X - dragPoint.X;
                int                         W  =   Size.Width;
                if(     resize_R)           W  = origin.Width  + dx;
                else if(resize_L)           W  = origin.Width  - dx;
                if(  W  <  MinimumSize.Width)
                {
                    dx -= (MinimumSize.Width - W);
                    W   =  MinimumSize.Width;
                }

                int                         dy = p.Y - dragPoint.Y;
                int                         H  =   Size.Height;
                if(     resize_B)           H  = origin.Height + dy;
                else if(resize_T)           H  = origin.Height - dy;
                if(  H  < MinimumSize.Height)
                {
                    dy -= (MinimumSize.Height - H);
                    H   =  MinimumSize.Height;
                }

                int L = Left;
                int T = Top;

                if(moving || resize_T) T  = origin.Y+dy;
                if(moving || resize_L) L = origin.X+dx;

                int s_w = Screen.PrimaryScreen.Bounds.Width;
                int s_h = Screen.PrimaryScreen.Bounds.Height;

                if(!resize_T && !resize_L && !resize_B && !resize_R)
                {
                    if     (Math.Abs((s_w  ) - (L+ Width  )) < MAGNET_SIZE) L = s_w   -  Width; // [FRAME_RIGHT]  to [SCREEN_RIGHT ]
                    else if(Math.Abs((s_w/2) - (L+ Width  )) < MAGNET_SIZE) L = s_w/2 -  Width; // [FRAME_RIGHT]  to [SCREEN_CENTER]
                    else if(Math.Abs((s_w/2) -  L          ) < MAGNET_SIZE) L = s_w/2         ; // [FRAME_LEFT ]  to [SCREEN_CENTER]
                    else if(                    L            < MAGNET_SIZE) L = 0             ; // [FRAME_LEFT ]  to [SCREEN_LEFT  ]

                    if     (Math.Abs((s_h  ) - (T + Height)) < MAGNET_SIZE) T = s_h   - Height; // [FRAME_BOTTOM] to [SCREEN_BOTTOM]
                    else if(Math.Abs((s_h/2) - (T + Height)) < MAGNET_SIZE) T = s_h/2 - Height; // [FRAME_RIGHT]  to [SCREEN_CENTER]
                    else if(Math.Abs((s_h/2) -  T          ) < MAGNET_SIZE) T = s_h/2         ; // [FRAME_TOP   ] to [SCREEN_MIDDLE]
                    else if(                    T            < MAGNET_SIZE) T = 0             ; // [FRAME_TOP   ] to [SCREEN_TOP   ]
                }
                else if(resize_B || resize_R)
                {
                     int sw875 = (int)(s_w * 0.875);
                     int sw750 = (int)(s_w * 0.750);
                     int sw625 = (int)(s_w * 0.625);
                     int sw500 = (int)(s_w * 0.500);
                     int sw375 = (int)(s_w * 0.375);
                     int sw250 = (int)(s_w * 0.250);
                     int sw125 = (int)(s_w * 0.125);

                     int sh875 = (int)(s_h * 0.875);
                     int sh750 = (int)(s_h * 0.750);
                     int sh625 = (int)(s_h * 0.625);
                     int sh500 = (int)(s_h * 0.500);
                     int sh375 = (int)(s_h * 0.375);
                     int sh250 = (int)(s_h * 0.250);
                     int sh125 = (int)(s_h * 0.125);

                     if( RESIZABLE ) {
                         if     (Math.Abs(s_w   -  W) < MAGNET_SIZE) W = s_w;
                         else if(Math.Abs(sw875 -  W) < MAGNET_SIZE) W = sw875;
                         else if(Math.Abs(sw750 -  W) < MAGNET_SIZE) W = sw750;
                         else if(Math.Abs(sw625 -  W) < MAGNET_SIZE) W = sw625;
                         else if(Math.Abs(sw500 -  W) < MAGNET_SIZE) W = sw500;
                         else if(Math.Abs(sw375 -  W) < MAGNET_SIZE) W = sw375;
                         else if(Math.Abs(sw250 -  W) < MAGNET_SIZE) W = sw250;
                         else if(Math.Abs(sw125 -  W) < MAGNET_SIZE) W = sw125;

                         if     (Math.Abs(s_h   -  H) < MAGNET_SIZE) H = s_h;
                         else if(Math.Abs(sh875 -  H) < MAGNET_SIZE) H = sh875;
                         else if(Math.Abs(sh750 -  H) < MAGNET_SIZE) H = sh750;
                         else if(Math.Abs(sh625 -  H) < MAGNET_SIZE) H = sh625;
                         else if(Math.Abs(sh500 -  H) < MAGNET_SIZE) H = sh500;
                         else if(Math.Abs(sh375 -  H) < MAGNET_SIZE) H = sh375;
                         else if(Math.Abs(sh250 -  H) < MAGNET_SIZE) H = sh250;
                         else if(Math.Abs(sh125 -  H) < MAGNET_SIZE) H = sh125;
                     }

                }

                Width  = W;
                Height = H;
                Top    = T;
                Left   = L;

            }
            //}}}
            // cursor {{{
            else {
                // handle {{{
                resize_B = false;
                resize_L = false;
                resize_R = false;
                resize_T = false;
                if( RESIZABLE ) {
                    if(e.X > (Size.Width -GRIP_SIZE)) resize_R = true;
                    if(e.X <              GRIP_SIZE ) resize_L = true;
                    if(e.Y <              GRIP_SIZE ) resize_T = true;
                    if(e.Y > (Size.Height-GRIP_SIZE)) resize_B = true;
                }
                //}}}
                if         (resize_T) {
                    if     (resize_L)           Cursor.Current = Cursors.SizeNWSE;
                    else if(resize_R)           Cursor.Current = Cursors.SizeNESW;
                    else                        Cursor.Current = Cursors.SizeNS;
                }
                else if(    resize_B) {
                    if     (resize_L)           Cursor.Current = Cursors.SizeNESW;
                    else if(resize_R)           Cursor.Current = Cursors.SizeNWSE;
                    else                        Cursor.Current = Cursors.SizeNS;
                }
                else if(resize_L || resize_R)   Cursor.Current = Cursors.SizeWE;
                else                            Cursor.Current = Cursors.SizeAll;
            }
            //}}}
        }

        //}}}
        //}}}
#endregion //}}}
    }
}
