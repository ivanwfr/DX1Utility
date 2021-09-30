#region using {{{
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#endregion }}}

namespace DX1Utility
{
    public partial class DX1Utility : Form {
        // constants {{{
        private const uint      DATALEN         = 6;

        //}}}
        // variables {{{
        private       byte[]    dx1_prev_data   = new byte[ DATALEN ];

        //}}}

        public void MapMacroKeys(string _caller) // {{{
        {
            if( DX1Utility.Debug ) log("MapMacroKeys("+_caller+")");

            foreach(KeyMap km in profile.keyMapList)
            {
                if(km.KeyType == 0x3)
                {
                    if( macroDict.ContainsKey(km.MacName) )
                    {
                        mKeyMacroSequenceMapping[km.KeyNum - 1]
                            = macroDict[km.MacName];
                    }
                    else {
                        log("Error Macro not found: "+ km.MacName);
                    }
                }
            }

        } //}}}
        void RebuildMacroList() // {{{
        {
            if( DX1Utility.Debug ) log("RebuildMacroList():");

            // Init
            macroDict.Clear();

            MacroList.Items.Clear();
            MacroList.Items.Add("NEW MACRO");

            // Macro folder
            if( !System.IO.Directory.Exists(Globals.ProfileMacroPath) )
                System.IO.Directory.CreateDirectory( Globals.ProfileMacroPath );

            // Add macro files
            String[] files = System.IO.Directory.GetFiles(Globals.ProfileMacroPath, "*.mac");
            foreach(String name in files)
            {
                System.IO.FileStream stream = new System.IO.FileStream(name, System.IO.FileMode.Open);
                Macro     macro = Macro.Read( stream );
                stream.Close();

                String[]   macroPath = name.Split('\\');
                macroPath            = macroPath.Last().Split('.');
                string macroFileName = macroPath.First();

                macro.name           = macroFileName;

                MacroList.Items.Add( macro.name );

                if(macro != null)
                    macroDict.Add(macro.name, macro);
            }

        }
        // }}}
        void macroTimer_Elapsed_handler(Object sender, System.Timers.ElapsedEventArgs e) // {{{
        {
            macroTimer.Stop();
            System.DateTime time        = System.DateTime.Now;
            TimeSpan        elapsedTime = time - initialTime;
            UInt64          curTimeMS   = (UInt64)elapsedTime.TotalMilliseconds;

            // Create the new Macro instance
            // process the rest of the running macros to this point.
            UInt64 nextMacroTickMS = macroPlayer.Tick( curTimeMS );

            // And if any are still waiting start a timer
            if(nextMacroTickMS != MacroPlayer.MACRO_TIME_DONE)
            {
                macroTimer.AutoReset = false;
                macroTimer.Interval  = (UInt32)(nextMacroTickMS - curTimeMS);
                macroTimer.Start();
            }
        }
        // }}}

        private void B_EditMacro_CB(object sender, EventArgs e) // {{{
        {
        log("B_EditMacro_CB():");

            // selected macro
            if(MacroList.SelectedIndex == -1)
                return;

            Macro macro;

            // existing macro
            if(MacroList.SelectedIndex > 0) {
                string name = MacroList.SelectedItem.ToString();
                macro       = macroDict[name];
            }
            // new macro
            else {
                macro         = new Macro(null);
                macro.name    = GetUniqueMacroName();
            }

            // edit macro
            MacroEditor macroEditor = new MacroEditor();
            macroEditor.RebuildFromMacroDefinition( macro );

            // user  OK
            if(macroEditor.ShowDialog() == DialogResult.OK)
            {
                macro = macroEditor.GetMacroDefinition();

                System.IO.FileStream stream
                    = new System.IO.FileStream(
                        Globals.ProfileMacroPath + macro.name + ".mac"
                        , System.IO.FileMode.OpenOrCreate
                        , System.IO.FileAccess.Write);

                Macro.Write(stream, macro);

                stream.Close();
            }
            RebuildMacroList();
        } //}}}

        private void deviceKeyDispatch(byte[] dx1_curr_data) // {{{
        {
            log("deviceKeyDispatch(dx1_curr_data)");

            // SORT
            Array.Sort   (dx1_curr_data);
            Array.Reverse(dx1_curr_data);

            // PRESS-RELEASE (in any of the possible SIMULTANEOUSLY-PRESSED-KEYS)
            int prev_index = 0;
            int curr_index = 0;
            while((curr_index < DATALEN) || (prev_index < DATALEN))
            {
                // NO MORE DATA {{{
                byte prev = (prev_index < DATALEN) ? dx1_prev_data[ prev_index ] : (byte)0;
                byte curr = (curr_index < DATALEN) ? dx1_curr_data[ curr_index ] : (byte)0;

                if((prev == 0) && (curr == 0))
                    break;

                //}}}
                // NO CHANGE {{{
                if(prev == curr) {
                    ++prev_index;
                    ++curr_index;
                } //}}}
                // RELEASED {{{
                else if(prev > curr) {
                    ++prev_index;

                    // MACRO
                    if(profile.keyMapList[prev - 1].KeyType > 3) {
                        specialKeyPlayer.KeyUp(profile.keyMapList[prev - 1]);
                    }
                    // SPECIAL
                    else {
                        // ...use original Macro Player by Rob
                        Macro macro = mKeyMacroSequenceMapping[prev - 1];
                        if(macro != null && (macro.macroType & Macro.MACRO_TYPE.MULTI_KEY) != 0)
                            macroPlayer.macroReleased( macro );
                    }
                } //}}}
                // PRESSED {{{
                else {
                    // SPECIAL
                    if(profile.keyMapList[curr-1].KeyType > 3) {
                        specialKeyPlayer.KeyDown( profile.keyMapList[curr-1] );
                    }
                    // MACRO
                    else {
                        // ...use original Macro Player by Rob
                        Macro macro = mKeyMacroSequenceMapping[curr - 1];
                        if(macro != null)
                        {
                            if((macro.macroType & Macro.MACRO_TYPE.MULTI_KEY) == 0)
                            {
                                macroTimer.Stop();

                                // current timing
                                System.DateTime time        = System.DateTime.Now;
                                TimeSpan        elapsedTime = time - initialTime;
                                UInt64          curTimeMS   = (UInt64)elapsedTime.TotalMilliseconds;

                                // new Macro instance to process events
                                MacroPlayer.MacroProc macroProc = new MacroPlayer.MacroProc( macro  );
                                UInt64          nextMacroTickMS = macroProc.start( curTimeMS );

                                // time-spanned sequence -- not instantaneous -> add to todo-list
                                if(nextMacroTickMS != MacroPlayer.MACRO_TIME_DONE)
                                    macroPlayer.Add( macroProc );

                                // process remaining events -- up to this point in time
                                nextMacroTickMS = macroPlayer.Tick( curTimeMS );

                                // start a timer for the next macro event
                                if(nextMacroTickMS != MacroPlayer.MACRO_TIME_DONE)
                                {
                                    macroTimer.AutoReset    = false;
                                    macroTimer.Interval     = (UInt32)(nextMacroTickMS - curTimeMS);
                                    macroTimer.Start();
                                }
                            }
                            else {
                                macroPlayer.macroPressed( macro );
                            }
                        }
                    }
                    ++curr_index;
                } //}}}
            }
            Array.Copy(dx1_curr_data, dx1_prev_data, DATALEN);
        }
        // }}}
        private string GetUniqueMacroName() // {{{
        {
            int index   = 1;
            string name = "Macro" + index;
            while( macroDict.ContainsKey(name) )
                name = "Macro" + (++index);

            return name;
        } //}}}
}
}
