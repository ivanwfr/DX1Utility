#region using {{{
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System;

#endregion }}}

namespace DX1Utility
{
// --------------------------------------------------------------------
// PROFILE SAVER --------------- ProfileManager_TAG (200729:18h:20) ---
// --------------------------------------------------------------------

    [Serializable]
    public class ProfileManager
    {
        // CONSTANTS {{{
        private const string    DX1_EXTENSION   = ".dx1_profile";
        private const string    XML_EXTENSION   = ".xml";

        private const string XMLDECLARATION     = "<?xml version='1.0'?>";
        private const string XMLTAG_DX1PROFILE  = "DX1Profile";
        private const string  XMLTAG_KEYMAP     = "keyMap";
        private const string   XMLTAG_KEYINDX   = "keyNum";
        private const string   XMLTAG_KEYCODE   = "keyCode";
        private const string   XMLTAG_KEYTYPE   = "keyType";
        private const string   XMLTAG_KEYNAME   = "keyName";
        private const string   XMLTAG_KEYDESC   = "keyDesc";
        private const string   XMLTAG_MACNAME   = "macName";
        private const string   XMLTAG_MAPDATA   = "keyData";

        //}}}
        // public instance variables {{{
        public string		profileName;
        public List<KeyMap>	keyMaps;
        public String[]		macroMap;

        //}}}

        private ProfileManager(List<KeyMap> keyMaps, string profileName) // {{{
        {
            this.keyMaps     = keyMaps;
            this.profileName = profileName;
        } //}}}

        public static List<KeyMap> LoadProfile(string profileName) //{{{
        {
            if( DX1Utility.Debug ) log("ProfileManager.LoadProfile("+profileName+"):");

            List<KeyMap> keyMaps = new List<KeyMap>();

            // access file {{{
            string fileName = Globals.UserProfileFolder + profileName + XML_EXTENSION;
            if( !System.IO.File.Exists( fileName ) )
            {
                log("*** FILE NOT FOUND:\n"+ fileName);

                profileName = "Global";
                fileName    = Globals.UserProfileFolder+"\\Global.xml";

                File.Copy("Profiles\\Global.xml", fileName);
            }//}}}

            // load file {{{
            XmlTextReader reader  = null;
            try {
                KeyMap keyMap = null;
                bool readingKeyMap = false;
                reader = new XmlTextReader( fileName );
                while (reader.Read()) 
                {
                    // key node line .. <keyMap>..</keyMap> {{{
                    if(reader.Name == XMLTAG_KEYMAP) {
                        // <keyMap> ..(new key)
                        if(reader.NodeType == XmlNodeType.Element   ) {
                            keyMap = new KeyMap();

                            readingKeyMap = true;
                        }
                        // </keyMap> .. (key loaded)
                        else if(reader.NodeType == XmlNodeType.EndElement) {
                            if(keyMap.KeyNum != 0)
                            {
                                keyMaps.Add( keyMap );
/*{{{
                                log( "ooo\n"
                                +"keyMap=["+ keyMap.ToString()    +"]\n"
                                +"keyMap=["+ keyMap.PrettyPrint() +"]\n"
                                +KeyMap_ToXML( keyMap )
                                );
}}}*/
                            }

                            readingKeyMap = false;
                        }
                    }
                    //}}}
                    // key elements columns .. (num..code..type..name..desc..mac..data) {{{
                    else if( readingKeyMap ) {
                        if     (reader.Name == XMLTAG_KEYINDX ) keyMap.KeyNum  = (byte  )reader.ReadElementContentAs(typeof(byte  ), null);
                        else if(reader.Name == XMLTAG_KEYCODE ) keyMap.KeyCode = (byte  )reader.ReadElementContentAs(typeof(byte  ), null);
                        else if(reader.Name == XMLTAG_KEYTYPE ) keyMap.KeyType = (byte  )reader.ReadElementContentAs(typeof(byte  ), null);
                        else if(reader.Name == XMLTAG_KEYNAME ) keyMap.KeyName =((string)reader.ReadElementContentAs(typeof(string), null)).Trim();
                        else if(reader.Name == XMLTAG_KEYDESC ) keyMap.KeyDesc =((string)reader.ReadElementContentAs(typeof(string), null)).Trim();
                        else if(reader.Name == XMLTAG_MACNAME ) keyMap.MacName =((string)reader.ReadElementContentAs(typeof(string), null)).Trim();
                        else if(reader.Name == XMLTAG_MAPDATA ) keyMap.KeyData =((string)reader.ReadElementContentAs(typeof(string), null)).Trim();
                    }
                    //}}}
                }
            }
            catch(Exception ex) {
                log("***ProfileManager.LoadProfileXML("+profileName+") Exception:\n"+ ex);
            }
            finally {
                if(reader != null)
                    reader.Close();
            }//}}}

            if     ( keyMaps.Count < 1) log("***Loading "+fileName+" returns an empty KeyMap list"  );
            else if( DX1Utility.Debug ) log(".. return a KeyMap list of "+ keyMaps.Count +" entries");

            return keyMaps;

        } //}}}
        public static void SaveProfile(string profileName, List<KeyMap> keyMaps) // {{{
        {
            log("SAVING PROFILE ["+profileName+"]");

            // Create file
            string fileName = Globals.UserProfileFolder + profileName + XML_EXTENSION;
            StreamWriter sw = new StreamWriter( fileName );

            // Write file
            sw.Write(      XMLDECLARATION    + "\n");
            sw.Write("<" + XMLTAG_DX1PROFILE +">\n");

            foreach(KeyMap keyMap in keyMaps)
                sw.Write(KeyMap_ToXML( keyMap ) +"\n");

            sw.Write("</"+ XMLTAG_DX1PROFILE +">\n");

            sw.Close();

        } //}}}
        public static void DeleteProfile(string profileName) // {{{
        {
            log("DELETING PROFILE ["+profileName+"]");

            string fileName = Globals.UserProfileFolder + profileName + DX1_EXTENSION;

            if( System.IO.File.Exists( fileName ) )
            {
                if( DX1Utility.Debug ) log(".. deleting "+fileName+":");
                System.IO.File.Delete( fileName );
            }

        } //}}}

        private static string KeyMap_ToXML(KeyMap keyMap) //{{{
        {
            return " <"+  XMLTAG_KEYMAP  +">"
                +   " <"+  XMLTAG_KEYINDX +">"+ string.Format("{0,2}" , keyMap.KeyNum ) +"</"+ XMLTAG_KEYINDX +">"
                +   " <"+  XMLTAG_KEYCODE +">"+ string.Format("{0,3}" , keyMap.KeyCode) +"</"+ XMLTAG_KEYCODE +">"
                +   " <"+  XMLTAG_KEYTYPE +">"+ string.Format("{0,2}" , keyMap.KeyType) +"</"+ XMLTAG_KEYTYPE +">"
                +   " <"+  XMLTAG_KEYNAME +">"+ string.Format("{0,10}", keyMap.KeyName) +"</"+ XMLTAG_KEYNAME +">"
                +   " <"+  XMLTAG_KEYDESC +">"+ string.Format("{0,10}", keyMap.KeyDesc) +"</"+ XMLTAG_KEYDESC +">"
                +   " <"+  XMLTAG_MACNAME +">"+ string.Format("{0,10}", keyMap.MacName) +"</"+ XMLTAG_MACNAME +">"
                +   " <"+  XMLTAG_MAPDATA +">"+ string.Format("{0,2}" , keyMap.KeyData) +"</"+ XMLTAG_MAPDATA +">"
                +  " </"+ XMLTAG_KEYMAP  +">"
                ;

        }
        //}}}
        /* log {{{*/
        private static void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }
} //}}}

