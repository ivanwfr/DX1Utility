using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DX1Utility
{
    [Serializable]
    public class Macro
    {
        // CONSTANTS {{{
        static int VERSION  = 1;

        public enum MACRO_TYPE {
        //  TIMED_MACRO     = 0x0,
            MULTI_KEY       = 0x1,
            USE_SCANCODE    = 0x2
        }
        //}}}
        // variables {{{
        public String       name      = "";
        public MACRO_TYPE   macroType = 0 | MACRO_TYPE.USE_SCANCODE;
        public MacroEvent[] macroEvents;

        //}}}
        public Macro(String name, MacroEvent[] macroEvents, MACRO_TYPE type) // {{{
        {
            this.name        = name;
            this.macroType   = type;
            this.macroEvents = macroEvents;
        }
        // }}}
        public Macro(MacroEvent[] macroEvents) // {{{
        {
            this.macroEvents = macroEvents;
        }
        // }}}

        public List<SpecialKeyPlayer.KMH_INPUT> getInputList(bool keyUp) // {{{
        {
            // Invert order for Key up

            List<SpecialKeyPlayer.KMH_INPUT> inputs      = new List<SpecialKeyPlayer.KMH_INPUT>();

            int start, done, step;
            if(keyUp) {
                start = macroEvents.Length-1;
                step  = -1;
                done  = -1;
            } else {
                start = 0;
                step  = 1;
                done  = macroEvents.Length;
            }

            for(int i=start; i != done ; i += step)
            {
                MacroEvent macroEvent               = macroEvents[i];
                SpecialKeyPlayer.KMH_INPUT  input   = new SpecialKeyPlayer.KMH_INPUT();
                input.type                          =     SpecialKeyPlayer.INPUT_KEYBOARD;

                if((macroType & Macro.MACRO_TYPE.USE_SCANCODE) != 0) {
                    input.ki.wScan          = (ushort)macroEvent.keyCode; // Note scan codes send a different value for key up
                    if(keyUp)
                        input.ki.dwFlags    = SpecialKeyPlayer.KEYEVENTF_SCANCODE | SpecialKeyPlayer.KEYEVENTF_KEYUP;
                    else
                        input.ki.dwFlags    = SpecialKeyPlayer.KEYEVENTF_SCANCODE;
                }
                else {
                    input.ki.wVk            = macroEvent.keyCode;
                    if(keyUp)
                        input.ki.dwFlags    = SpecialKeyPlayer.KEYEVENTF_KEYUP;
                    else
                        input.ki.dwFlags    = 0;
                }

                inputs.Add( input );
            }
            return inputs;
        }
        // }}}


        // Serializable
        public static Macro Read(System.IO.Stream stream) // {{{
        {
            IFormatter  formatter   = new BinaryFormatter();
            int         version     = (Int32)formatter.Deserialize(stream);
            Macro       md          = (Macro)formatter.Deserialize(stream);
            return md;
        }
        // }}}
        public static void Write(System.IO.Stream stream, Macro md) // {{{
        {
            IFormatter formatter        = new BinaryFormatter();
            formatter.Serialize(stream, VERSION);
            formatter.Serialize(stream, md);
        }
        // }}}
        /* log {{{*/
        private void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }

    [Serializable]
    public class MacroEvent  // {{{
    {
        // variables {{{
        public UInt32 time;
        public byte   keyCode;
        public int    keyType = 0;
        public bool   keyUp;

        //}}}
        public MacroEvent(UInt32 t, bool up, byte key, int type) // {{{
        {
            keyCode = key;
            keyType = type;
            time    = t;
            keyUp   = up;
        }
        // }}}
        /* log {{{*/
        private void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }
// }}}

}

