using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DX1Utility
{
    public class MacroPlayer {
#region [StructLayout] {{{

    [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint   dwFlags;
            public uint   time;
            public IntPtr dwExtraInfo;
        }

    [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int    dx;
            public int    dy;
            public int    mouseData;
            public uint   dwFlags;
            public uint   time;
            public IntPtr dwExtraInfo;
        }

    [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            uint   uMsg;
            ushort wParamL;
            ushort wParamH;
        }

#endregion }}}
#region Const {{{

    const int INPUT_MOUSE               = 0;
    const int INPUT_KEYBOARD            = 1;
    const int INPUT_HARDWARE            = 2;

    const uint KEYEVENTF_EXTENDEDKEY    = 0x0001;
    const uint KEYEVENTF_KEYUP          = 0x0002;
    const uint KEYEVENTF_UNICODE        = 0x0004;
    const uint KEYEVENTF_SCANCODE       = 0x0008;

    const uint XBUTTON1                 = 0x0001;
    const uint XBUTTON2                 = 0x0002;

    const uint MOUSEEVENTF_MOVE         = 0x0001;

    const uint MOUSEEVENTF_LEFTDOWN     = 0x0002;
    const uint MOUSEEVENTF_LEFTUP       = 0x0004;

    const uint MOUSEEVENTF_RIGHTDOWN    = 0x0008;
    const uint MOUSEEVENTF_RIGHTUP      = 0x0010;
    const uint MOUSEEVENTF_MIDDLEDOWN   = 0x0020;
    const uint MOUSEEVENTF_MIDDLEUP     = 0x0040;

    const uint MOUSEEVENTF_XDOWN        = 0x0080;
    const uint MOUSEEVENTF_XUP          = 0x0100;

    const uint MOUSEEVENTF_WHEEL        = 0x0800;

    const uint MOUSEEVENTF_VIRTUALDESK  = 0x4000;

    const uint MOUSEEVENTF_ABSOLUTE     = 0x8000;

    public const UInt64 MACRO_TIME_DONE = 0xffffffffffffffff;

#endregion }}}
#region variables {{{

    private List<MacroProc> macroProcList = new List<MacroProc>();

#endregion }}}

    public void Add(MacroProc macroProc) // {{{
    {
        macroProcList.Add( macroProc );
    }
    // }}}
    public UInt64 Tick(UInt64 curTime) // {{{
    {
        UInt64 minTime = MACRO_TIME_DONE;
        foreach(MacroProc macroProc in macroProcList)
        {
            UInt64 next = macroProc._proceed( curTime );
            if(next < minTime)
                minTime = next;
        }
        return minTime;
    }
    // }}}

    public void macroPressed(Macro macro) // {{{
    {
        _macroSendInput(macro, false);
    }
    // }}}
    public void macroReleased(Macro macro) // {{{
    {
        _macroSendInput(macro, true);
    }
    // }}}
    private void _macroSendInput(Macro macro, bool keyUp) // {{{
    {
        SpecialKeyPlayer.KMH_INPUT[] inputArray = macro.getInputList(keyUp).ToArray();
        if(inputArray.Length > 0) {
            int size = Marshal.SizeOf(inputArray[0]);
            NativeMethods.SendInput((uint)inputArray.Length, inputArray, size);
        }
    }
    // }}}

    public class MacroProc // {{{
    {
        // variables {{{
        private Macro   macro;
        private UInt64  launchTime;
        private int     tickNum = 0;

        //}}}
        public MacroProc(Macro macro) // {{{
        {
            this.macro = macro;
        }
        // }}}
        public UInt64 start(UInt64 launchTime) // {{{
        {
            // Returns time of next Event or MACRO_TIME_DONE

            if((macro.macroType & Macro.MACRO_TYPE.MULTI_KEY) != 0)
                return MACRO_TIME_DONE;

            this.launchTime = launchTime;

            return _proceed( launchTime );
        }
        // }}}
        // private
        public UInt64 _proceed(UInt64 time) // {{{
        {
            List<SpecialKeyPlayer.KMH_INPUT> inputList = new List<SpecialKeyPlayer.KMH_INPUT>();

            bool use_scancode = ((macro.macroType & Macro.MACRO_TYPE.USE_SCANCODE) != 0);

            for(; tickNum < macro.macroEvents.Length; tickNum++) {
                // DUE LATER, STOP THERE {{{
                MacroEvent macroEvent = macro.macroEvents[ tickNum ];
                if(launchTime + macroEvent.time > time)
                    break;

                //}}}
                // KEYBOARD {{{
                SpecialKeyPlayer.KMH_INPUT input = new SpecialKeyPlayer.KMH_INPUT();
                if(macroEvent.keyType != 2) {

                    //....type.........KEYBOARD
                    input.type = INPUT_KEYBOARD;

                    //.....scancode.............SC-VK .....................keyCode............dwFlags.............. keyUp...............keyDown..............SCANCODE
                    if(use_scancode) { input.ki.wScan = (ushort)macroEvent.keyCode;  input.ki.dwFlags = (macroEvent.keyUp ? KEYEVENTF_KEYUP : 0) | KEYEVENTF_SCANCODE; }
                    else             { input.ki.wVk   =         macroEvent.keyCode;  input.ki.dwFlags = (macroEvent.keyUp ? KEYEVENTF_KEYUP : 0)                     ; }

                }
                //}}}
                // MOUSE {{{
                else {
                    input.type              = INPUT_MOUSE;
                    input.mi.dx             = 0;
                    input.mi.dy             = 0;
                    input.mi.mouseData      = 0;
                    input.mi.dwFlags        = (uint)macroEvent.keyCode;
                }
                //}}}
                inputList.Add(input);
            }
            // SEND INPUT {{{
            SpecialKeyPlayer.KMH_INPUT[] inputArray = inputList.ToArray();
            if(inputArray.Length > 0) {
                int size = Marshal.SizeOf(inputArray[0]);
                NativeMethods.SendInput((uint)inputArray.Length, inputArray, size);

            } //}}}

        return tickNum < macro.macroEvents.Length
            ? launchTime + macro.macroEvents[tickNum].time
            : MACRO_TIME_DONE
            ;
        }
        // }}}
    }
    // }}}

#region NativeMethods {{{
    internal static class NativeMethods
    {
#region SendInput {{{

    [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, SpecialKeyPlayer.KMH_INPUT[] pInputs, int cbSize);

#endregion }}}
    }
#endregion //}}}

        /* log {{{*/
        private void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }
}
