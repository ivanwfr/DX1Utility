using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    [Serializable]
    public class KeyMap
    {
#region variables {{{
        private int     keyVK  ; // Virtual Keycode
        private byte    keyNum ; // Ergodex-device key number
        private byte    keyCode; // scancode sent
        private byte    keyType; // 1=Single-Key, 2=Multi-Key-Macro, 3=Timed-Macro, 4=Special-Key

        private string  keyName; // symbol-name
        private string  keyDesc; // display-name
        private string  keyData; // extra data
        private string  macName; // associated macro
#endregion }}}

    // Key Accessors .. Num Code Name Type Desc Data Macro
    //{{{
    public int    KeyVK     { get { return keyVK  ; } set { keyVK   = value; } }
    public byte   KeyNum    { get { return keyNum;  } set { keyNum  = value; } }
    public byte   KeyCode   { get { return keyCode; } set { keyCode = value; } }
    public string KeyName   { get { return keyName; } set { keyName = value; } }
    public byte   KeyType   { get { return keyType; } set { keyType = value; } }
    public string KeyDesc   { get { return keyDesc; } set { keyDesc = value; } }
    public string KeyData   { get { return keyData; } set { keyData = value; } }
    public string MacName   { get { return macName; } set { macName = value; } }

    //}}}

    // Key Assign .. Code Desc .. KeyPairConversionTable[0][1]
    public bool AssignSingleKey(int vk, string keyDesc_arg) // {{{
    {
        string caller = "KeyMap.AssignSingleKey";

        if( DX1Utility.Debug ) log(caller+"(vk 0x"+ vk.ToString("X2") +")");

        byte[] keyTuple = null;
        try {

            // DIFFERENTIATE LEFT AND RIGHT SHIFT, CTRL, ALT {{{
            if(vk >= 0x10 && vk <= 0x12)
            {
                Byte[] state    = new Byte[256];

                NativeMethods.GetKeyboardState( state );

                vk         = 0xa0 + 2*(vk - 0x10);

                // Separate left and right shift/ctrl/alt
                if((state[vk + 1] & 0x80) != 0)
                    ++vk;  // RHS version
            }

            //}}}

            keyTuple = KeyConversionTable.KeyPairConversionTable[ vk ];
            if(keyTuple != null)
            {
                keyType = keyTuple[0];
                keyCode = keyTuple[1];
                keyVK   = keyTuple[2];
                keyName = keyDesc_arg;
                keyDesc = keyDesc_arg;

                if( DX1Utility.Debug ) log(".. "+ this.ToString()    );
                return true;
            }
            else {
                keyName = "";
                keyDesc = "Error VK 0x"+vk.ToString("X2");

                if( DX1Utility.Debug ) log( keyDesc );
                return false;
            }
        }
        catch(Exception ex) {
            keyName = "";    keyDesc  = "Excep 0x"+vk.ToString("X2");
            log(caller+": "+ keyDesc +":\n"+ex);
            return false;
        }

    }
    // }}}

    // called by:
    // .. KeyProgrammer  . get_keyMapList_num_type_code_byteArray
    // .. for DX1Utility . ApplyKeySet
    public byte[] to_num_type_code_byteArray() // {{{
    {
        // DX1 programming byte sequence

        byte[] num_type_code_byteArray = new byte[3];

        num_type_code_byteArray[0]     =           keyNum ;
        num_type_code_byteArray[1]     = Math.Min( keyType   , (byte)3);
        num_type_code_byteArray[2]     =           keyCode ;

        return num_type_code_byteArray;
    }
    // }}}

    public override string ToString() //{{{
    {
        return string.Format("{0} #{1} [{2}] [{3} 0x{4:X2})] {5}{6} [{7}]"
            ,                  TypeToString()
            ,                      keyNum
            ,                           keyVK
            ,                                 keyCode
            ,                                       keyCode
            ,                                                macName
            ,                                                   keyName
            ,                                                        keyDesc
            );

    }
    public string TypeToString()
    {
        switch( keyType ) {
            case  1: return "KEY";
            case  2: return "MOD";
            case  3: return "MAC";
            case  4: return "SPE";
            default: return    "";
        }
    }
    //}}}

        /* log {{{*/
        private void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }

}
