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
    public partial class MacroEditor : Form
    {
        public MacroEditor() // {{{
        {
            log("MacroEditor.InitializeComponent:");
            InitializeComponent();

            MacroCommands.Columns.Add( "Time"    );
            MacroCommands.Columns.Add( "KeyType" );
            MacroCommands.Columns.Add( "KeyCode" );

            ListViewItem item = new ListViewItem(""+0);

            item.SubItems.Add(         "KeyType" );
            item.SubItems.Add(         ""+0x21   );

            MacroCommands.Items.Add(item);
            MacroCommands.MouseDoubleClick += new MouseEventHandler(MacroCommands_MouseDoubleClick);


        }
        // }}}
        void ValidateMacroTiming() // {{{
        {
            uint time = 0;
            foreach(ListViewItem item in MacroCommands.Items)
            {
                if (UInt32.Parse(item.SubItems[0].Text) < time)
                    item.SubItems[0].Text = ("" + time);
                time = UInt32.Parse(item.SubItems[0].Text);
            }

        }
        // }}}
        protected override void OnKeyDown(KeyEventArgs e) // {{{
        {
            log("MacroEditor.OnKeyDown");

            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {                
                if (MacroCommands.SelectedIndices.Count > 0)
                {
                    int index = MacroCommands.SelectedIndices[0];
                    MacroCommands.Items.RemoveAt(index);
                }

            }
            if (e.KeyCode == System.Windows.Forms.Keys.Insert)
            {
                if (MacroCommands.SelectedIndices.Count > 0)
                {
                    int index = MacroCommands.SelectedIndices[0];
                    ListViewItem item = new ListViewItem("" + 0);
                    item.SubItems.Add("KeyDown");
                    item.SubItems.Add("" + 0x21);
                    MacroCommands.Items.Insert(index, item);
                    ValidateMacroTiming();
                }
                else if (MacroCommands.Focused)
                {
                    ListViewItem item = new ListViewItem("" + 0);
                    item.SubItems.Add("KeyDown");
                    item.SubItems.Add("" + 0x21);
                    MacroCommands.Items.Add(item);
                    ValidateMacroTiming();
                }

            }

            
            base.OnKeyDown(e);
        }
        // }}}
        public void RebuildFromMacroDefinition(Macro macro) // {{{
        {
            MacroCommands.Items.Clear();
            if (macro.macroEvents != null)
            {
                for (int i = 0; i < macro.macroEvents.Length; i++)
                {
                    MacroEvent macroEvent = macro.macroEvents[i];
                    ListViewItem                 item = new ListViewItem(""+ macroEvent.time);
                    if( macroEvent.keyUp ) {
                        item.SubItems.Add("KeyUp");
                    }
                    else {
                        switch( macroEvent.keyType ) {
                            case  1: item.SubItems.Add("KeyUp"  ); break;
                            case  2: item.SubItems.Add("Mouse"  ); break;
                            default: item.SubItems.Add("KeyDown"); break;
                        }
                    }
                    item.SubItems.Add("" + macroEvent.keyCode);

                    MacroCommands.Items.Add(item);
                }
            }
            MacroName.Text              = macro.name;

            ME_C_UseScanCodes .Checked  = ((macro.macroType & Macro.MACRO_TYPE.USE_SCANCODE) != 0);

            // either multi-key or timed macro
            ME_R_TimedMacro   .Checked  = ((macro.macroType & Macro.MACRO_TYPE.MULTI_KEY   ) == 0);
            ME_R_MultikeyMacro.Checked  = !ME_R_TimedMacro.Checked;


        }
        // }}}
         public Macro GetMacroDefinition() // {{{
         {
             List<MacroEvent> keyList = new List<MacroEvent>();
             foreach(ListViewItem item in MacroCommands.Items)
             {
                 uint time    = UInt32.Parse(item.SubItems[0].Text);

                 Byte keyCode = Byte.Parse(item.SubItems[2].Text);

                 bool keyUp   = (item.SubItems[1].Text == "KeyUp");

                 int  keyType;
                 switch(item.SubItems[1].Text) {
                     case "KeyUp": keyType = 1; break;
                     case "Mouse": keyType = 2; break;
                     default     : keyType = 0; break;
                 }

                 keyList.Add(new MacroEvent(time, keyUp, keyCode, keyType));
             }
             String name = MacroName.Text;

             Macro.MACRO_TYPE type = 0;
             type |= ME_R_MultikeyMacro.Checked ? Macro.MACRO_TYPE.MULTI_KEY    : 0;
             type |= ME_C_UseScanCodes .Checked ? Macro.MACRO_TYPE.USE_SCANCODE : 0;

             return new Macro(name, keyList.ToArray(), type);
         }
         // }}}
        private void MacroCommands_MouseDoubleClick(object sender, EventArgs e) // {{{
        {
            MacroActionEditor edit = new MacroActionEditor();
            ListViewItem item = MacroCommands.SelectedItems[0];
            edit.Init(item);
            edit.Location = System.Windows.Forms.Control.MousePosition;
            if (edit.ShowDialog() == DialogResult.OK)
            {
                item = edit.GetListViewItem();
                MacroCommands.Items[MacroCommands.SelectedIndices[0]] = item;
                ValidateMacroTiming();
            }
            MacroCommands.Invalidate();
        }
        // }}}
        private void OKButton_CB(object sender, EventArgs e) // {{{
        {

        }
        // }}}
        /* log {{{*/
        private static void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }
}
