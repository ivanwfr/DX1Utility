using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DX1Utility
{
    public class SpecialKeyPlayer
    {
        // STRUCT {{{
        [StructLayout(LayoutKind.Sequential)]
            public struct    MOUSEINPUT {
                public int    dx;
                public int    dy;
                public int    mouseData;
                public uint   dwFlags;
                public uint   time;
                public IntPtr dwExtraInfo;
            }

        [StructLayout(LayoutKind.Sequential)]
            public struct    KEYBDINPUT {
                public ushort wVk;
                public ushort wScan;
                public uint   dwFlags;
                public uint   time;
                public IntPtr dwExtraInfo;
            }

        [StructLayout(LayoutKind.Sequential)]
            public struct    HARDWAREINPUT {
                uint          uMsg;
                ushort        wParamL;
                ushort        wParamH;
            }

        [StructLayout(LayoutKind.Explicit)]
            public struct KMH_INPUT {                   // FIXME IWE (4 should be aligned to 8)
                [FieldOffset(0)] public int             type;
                [FieldOffset(4)] public KEYBDINPUT      ki;
                [FieldOffset(4)] public MOUSEINPUT      mi;
                [FieldOffset(4)]        HARDWAREINPUT   hi;
            }

        //}}}
        // CONSTANTS {{{

        public const int    INPUT_MOUSE             = 0;
        public const int    INPUT_KEYBOARD          = 1;
        public const int    INPUT_HARDWARE          = 2;

        public const uint   MOUSEEVENTF_MOVE        = 0x0001;
        public const uint   MOUSEEVENTF_LEFTDOWN    = 0x0002;
        public const uint   MOUSEEVENTF_LEFTUP      = 0x0004;
        public const uint   MOUSEEVENTF_RIGHTDOWN   = 0x0008;
        public const uint   MOUSEEVENTF_RIGHTUP     = 0x0010;
        public const uint   MOUSEEVENTF_MIDDLEDOWN  = 0x0020;
        public const uint   MOUSEEVENTF_MIDDLEUP    = 0x0040;
        public const uint   MOUSEEVENTF_XDOWN       = 0x0080;
        public const uint   MOUSEEVENTF_XUP         = 0x0100;
        public const uint   MOUSEEVENTF_VWHEEL      = 0x0800;
        public const uint   MOUSEEVENTF_HWHEEL      = 0x1000;

        public const ushort VOLUME_MUTE             = 0xAD;
        public const ushort VOLUME_DOWN             = 0xAE;
        public const ushort VOLUME_UP               = 0xAF;
        public const ushort MEDIA_NEXT              = 0xB0;
        public const ushort MEDIA_PREV              = 0xB1;
        public const ushort MEDIA_STOP              = 0xB2;
        public const ushort MEDIA_PLAY_PAUSE        = 0xB3;

        public const uint   KEYEVENTF_KEYUP         = 0x0002;
        public const uint   KEYEVENTF_SCANCODE      = 0x0008;

        //}}}
        // variables {{{
        private List<SpecialKey> _SpecialKeys = new List<SpecialKey>();
        public List<SpecialKey>   SpecialKeys { get { return _SpecialKeys; } set { _SpecialKeys = value; } }

        //}}}

        public void InitPlayer() // {{{
        {
            //Initializes the Special-Keys and Ability to play the keys
            //Since Special-Keys are a hard-Coded List, add Each Special-Key
            SpecialKey TempSpecial = new SpecialKey();

            //SpecialKey[0] = Left Mouse Button
            TempSpecial.SpecialID = 0;
            TempSpecial.SpecialName = "Left Mouse Button";
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[1] = Right Mouse Button
            TempSpecial.SpecialID = 1;
            TempSpecial.SpecialName = "Right Mouse Button";
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[2] = Middle Mouse Button
            TempSpecial.SpecialID = 2;
            TempSpecial.SpecialName = "Middle Mouse Button";
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[3] = Scroll Wheel
            TempSpecial.SpecialID = 3;
            TempSpecial.SpecialName = "Mouse Vertical Scroll";
            TempSpecial.ReqData = true;
            TempSpecial.ExtraDataType = SpecialKeysExtraData.Boolean;
            TempSpecial.ExtraDataParams.Add("True", "Up");
            TempSpecial.ExtraDataParams.Add("False", "Down");
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[4] = Scroll Wheel
            TempSpecial.SpecialID = 4;
            TempSpecial.SpecialName = "Mouse Horizontal Scroll";
            TempSpecial.ReqData = true;
            TempSpecial.ExtraDataType = SpecialKeysExtraData.Boolean;
            TempSpecial.ExtraDataParams.Add("True", "Right");
            TempSpecial.ExtraDataParams.Add("False", "Left");
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[5] = Media Play/Pause
            TempSpecial.SpecialID = 5;
            TempSpecial.SpecialName = "Media Play/Pause";
            TempSpecial.SpecialValue = (ushort)MEDIA_PLAY_PAUSE;
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[6] = Media Stop
            TempSpecial.SpecialID = 6;
            TempSpecial.SpecialName = "Media Stop";
            TempSpecial.SpecialValue = (ushort)MEDIA_STOP;
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[7] = Media Next
            TempSpecial.SpecialID = 7;
            TempSpecial.SpecialName = "Media Next Track";
            TempSpecial.SpecialValue = (ushort)MEDIA_NEXT;
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[8] = Media Previous
            TempSpecial.SpecialID = 8;
            TempSpecial.SpecialName = "Media Prev Track";
            TempSpecial.SpecialValue = (ushort)MEDIA_PREV;
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[9] = Volume Mute
            TempSpecial.SpecialID = 9;
            TempSpecial.SpecialName = "Volume Mute";
            TempSpecial.SpecialValue = (ushort)VOLUME_MUTE;
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[10] = Volume Down
            TempSpecial.SpecialID = 10;
            TempSpecial.SpecialName = "Volume Down";
            TempSpecial.SpecialValue = (ushort)VOLUME_DOWN;
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[11] = Volumen Up
            TempSpecial.SpecialID = 11;
            TempSpecial.SpecialName = "Volume Up";
            TempSpecial.SpecialValue = (ushort)VOLUME_UP;
            _SpecialKeys.Add(TempSpecial);


        }
        // }}}
        public string GetCustomData(int SpecialID, string InputData) // {{{
        {
            // Turn the input Data into a KeyData String that the Player understands later for this specific Special-Key
            string TempString = "";

            switch (_SpecialKeys[SpecialID].ExtraDataType)
            {
                case SpecialKeysExtraData.Boolean:
                        if(InputData  == "True")
                            TempString = "1";
                        else
                            TempString = "0";
                        break;
                default:
                        break;
            }
            return TempString;
        }
        // }}}

        public void KeyDown(KeyMap CurrentKey, string ExtraData = "") // SPECIAL KEY PRESSED {{{
        {
            KMH_INPUT input                 = new KMH_INPUT();
            switch( CurrentKey.KeyCode )
            {
                case 0: // Left Mouse Button //{{{

                    //Old Code - Mostly Worked
                    //mouse_event(0x02, 0, 0, 0, 0);

                    input.type              = INPUT_MOUSE;
                    input.mi.dx             = 0;
                    input.mi.dy             = 0;
                    input.mi.mouseData      = 0;
                    input.mi.dwFlags        = (int)MOUSEEVENTF_LEFTDOWN;

                    break;
                    //}}}
                case 1: // Right Mouse Button //{{{
                    input.type              = INPUT_MOUSE;
                    input.mi.dx             = 0;
                    input.mi.dy             = 0;
                    input.mi.mouseData      = 0;
                    input.mi.dwFlags        = (int)MOUSEEVENTF_RIGHTDOWN;

                    break;
                    //}}}
                case 2: // Middle Mouse Button //{{{
                    input.type              = INPUT_MOUSE;
                    input.mi.dx             = 0;
                    input.mi.dy             = 0;
                    input.mi.mouseData      = 0;
                    input.mi.dwFlags        = (int)MOUSEEVENTF_MIDDLEDOWN;

                    break;
                    //}}}
                case 3: // Mouse Vertical Scroll //{{{
                    input.type              = INPUT_MOUSE;
                    input.mi.dx             = 0;
                    input.mi.dy             = 0;

                    //Check to see if scrolling Up or down
                    if (CurrentKey.KeyData == "1")
                        input.mi.mouseData  =  100;
                    else
                        input.mi.mouseData  = -100;

                    input.mi.dwFlags        = (int)MOUSEEVENTF_VWHEEL;

                    break;
                    //}}}
                case 4: // Mouse Horizontal Scroll //{{{
                    input.type              = INPUT_MOUSE;
                    input.mi.dx             = 0;
                    input.mi.dy             = 0;

                    //Check to see if scrolling Up or down
                    if (CurrentKey.KeyData == "1")
                        input.mi.mouseData  =  100;
                    else
                        input.mi.mouseData  = -100;

                    input.mi.dwFlags        = (int)MOUSEEVENTF_HWHEEL;

                    break;
                    //}}}
                    /* case 5: // Media Play/Pause //{{{
                       input.ki.wVk         = (ushort)MEDIA_PLAY_PAUSE;
                       input.ki.wScan       = 0;
                       input.ki.dwExtraInfo = IntPtr.Zero;
                       input.ki.dwFlags     = 0;

                       break;
                    //}}} */
                default:// Special-Key that can be sent just with the SpecialValue to the Keyboard Input //{{{
                    log("SpecialKeyPlayer.KeyDown("+CurrentKey.ToString()+")");
                    log("...");

                    input.type              = INPUT_KEYBOARD;
                    input.ki.wVk            = _SpecialKeys[CurrentKey.KeyCode].SpecialValue;
                    input.ki.wScan          = 0;
                    input.ki.dwExtraInfo    = IntPtr.Zero;
                    input.ki.dwFlags        = 0;

                    break;
                    //}}}
            }
            KMH_INPUT[] inputs              = { input };
            NativeMethods.SendInput(1, inputs, Marshal.SizeOf(input));
        }
        // }}}
        public void KeyUp  (KeyMap CurrentKey, string ExtraData = "") // SPECIAL KEY {{{
        {
            KMH_INPUT input_up              = new KMH_INPUT();
            switch (CurrentKey.KeyCode)
            {
                case 0: // Left Mouse Button {{{
                    //mouse_event(0x04, 0, 0, 0, 0);
                    input_up.type           = INPUT_MOUSE;
                    input_up.mi.dx          = 0;
                    input_up.mi.dy          = 0;
                    input_up.mi.mouseData   = 0;
                    input_up.mi.dwFlags     = (int)MOUSEEVENTF_LEFTUP;

                    break;
                    //}}}
                case 1: // Right Mouse Button {{{
                    input_up.type           = INPUT_MOUSE;
                    input_up.mi.dx          = 0;
                    input_up.mi.dy          = 0;
                    input_up.mi.mouseData   = 0;
                    input_up.mi.dwFlags     = (int)MOUSEEVENTF_RIGHTUP;

                    break;
                    //}}}
                case 2: // Middle Mouse Button {{{
                    input_up.type           = INPUT_MOUSE;
                    input_up.mi.dx          = 0;
                    input_up.mi.dy          = 0;
                    input_up.mi.mouseData   = 0;
                    input_up.mi.dwFlags     = (int)MOUSEEVENTF_MIDDLEUP;

                    break;
                    //}}}
                default:// Special-Key that can be sent just with the SpecialValue to the Keyboard Input {{{
                    input_up.type           = INPUT_KEYBOARD;
                    input_up.ki.wVk         = _SpecialKeys[CurrentKey.KeyCode].SpecialValue;
                    input_up.ki.wScan       = 0;
                    input_up.ki.dwExtraInfo = IntPtr.Zero;
                    input_up.ki.dwFlags     = KEYEVENTF_KEYUP;

                    break;
                    //}}}
            }
            KMH_INPUT[] inputs              = { input_up };
            NativeMethods.SendInput(1, inputs, Marshal.SizeOf(input_up));
        }
        // }}}

        /* log {{{*/
        private void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/

#region NativeMethods {{{
        internal static class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
                public static extern uint SendInput(uint nInputs, KMH_INPUT[] pInputs, int cbSize);

            //[System.Runtime.InteropServices.DllImport("user32.dll")]
            //    public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        }
#endregion //}}}
    }
}
