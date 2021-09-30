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

        const int DBT_CUSTOMEVENT = 0x8006;
        const int WM_DEVICECHANGE = 0x219;

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
        protected override void WndProc(ref Message m) // {{{
        {
            switch( m.Msg ) {
                case WM_DEVICECHANGE:
                    switch(m.WParam.ToInt32())
                    {
                        case DBT_CUSTOMEVENT:
                            DEV_BROADCAST_DEVICEHANDLE vol;
                            vol = (DEV_BROADCAST_DEVICEHANDLE) Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_DEVICEHANDLE));

//                            if(((int)vol.dbch_data[0]) > 0)
//                                log("Device.WndProc("+ vol.dbch_data[0] +"): DX1UtilityHasFocus=["+ DX1UtilityHasFocus +"]");

                            if( DX1UtilityHasFocus ) {
                                // on release
                                int keyNum = (int)vol.dbch_data[0];
                                if(keyNum > 0)
                                    select_keyNum( keyNum );
                            }
                            else {
                                int keyNum = (int)vol.dbch_data[0];
                                if(keyNum > 0)
                                    grid_show_keyNum( keyNum );
                                deviceKeyDispatch( vol.dbch_data );
                            }
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        } //}}}

    }
}
