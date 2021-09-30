using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    public class KeyProgrammer
    {
#region const {{{
        private const string    KEYMAP_KEND_REQUESTED   = "KEYMAP SEND REQUESTED";
        private const string    KEYMAP_KEY_CHANGED      = "KEYMAP KEY CHANGED";
        private const string    KEYMAP_KEY_CLEARED      = "KEYMAP KEY CLEARED";
        private const string    KEYMAP_KROFILE_LOADED   = "KEYMAP PROFILE LOADED";

        private const string    PROFILE_KEYMAP_CHANGED  = "PROFILE KEYMAP CHANGED";
        private const string    PROFILE_SAVE_REQUESTED  = "PROFILE SAVE REQUESTED";

        private const int       INACTIVE                = -1;

#endregion }}}
#region variables {{{
        private string      profile_changeToCommit = "";
        private string      keyMap_changeToCommit  = "";
        private int         keyNum                 = 0;

#endregion }}}

        // Accessors {{{
        public int    KeyNum                 { get { return                 keyNum; } set { keyNum = value; } }
        public string KeyMap_changeToCommit  { get { return  keyMap_changeToCommit; }      /* readonly */     }
        public string Profile_changeToCommit { get { return profile_changeToCommit; }      /* readonly */     }

        // }}}

        // PROFILE COMMIT STAGING {{{
        public void notify_profile_SAVE_REQUESTED() //{{{
        {
            if( DX1Utility.Debug ) log("notify_profile_SAVE_REQUESTED");
            profile_changeToCommit = PROFILE_SAVE_REQUESTED;
        }
        //}}}
        public void notify_profile_KEYMAP_CHANGED() //{{{
        {
            if( DX1Utility.Debug ) log("notify_profile_KEYMAP_CHANGED");
            profile_changeToCommit = PROFILE_KEYMAP_CHANGED;
        }
        //}}}
        //}}}
        // KEYMAP  COMMIT STAGING {{{
        public void notify_keyMap_KEY_CLEARED() //{{{
        {
            if( DX1Utility.Debug ) log("notify_keyMap_KEY_CLEARED");
            keyMap_changeToCommit = KEYMAP_KEY_CLEARED;
        }
        //}}}
        public void notify_keyMap_KEY_CHANGED() //{{{
        {
            if( DX1Utility.Debug ) log("notify_keyMap_KEY_CHANGED");
            keyMap_changeToCommit = KEYMAP_KEY_CHANGED;
        }
        //}}}
        public void notify_keyMap_SEND_REQUESTED() //{{{
        {
            if( DX1Utility.Debug ) log("notify_keyMap_SEND_REQUESTED");
            keyMap_changeToCommit = KEYMAP_KEND_REQUESTED;
        }
        //}}}
        public void notify_keyMap_PROFILE_LOADED() //{{{
        {
            if( DX1Utility.Debug ) log("notify_keyMap_PROFILE_LOADED");
            keyMap_changeToCommit = KEYMAP_KROFILE_LOADED;
        }
        //}}}
        //}}}
        // STAGED  COMMIT .. KEYMAP .. PROFILE {{{
        /* REQUIREMENTS {{{
         * o---------------------------------------------------o
         * |                                                   |
         * |  keyMap_changeToCommit = KEYMAP_KEND_REQUESTED;   |
         * |  keyMap_changeToCommit = KEYMAP_KEY_CHANGED;      |
         * |  keyMap_changeToCommit = KEYMAP_KEY_CLEARED;      |
         * |  keyMap_changeToCommit = KEYMAP_KROFILE_LOADED;   |
         * |                                                   |
         * |---------------------------------------------------|
         * |                                                   |
         * |  profile_changeToCommit = PROFILE_KEYMAP_CHANGED; |
         * |  profile_changeToCommit = PROFILE_SAVE_REQUESTED; |
         * |                                                   |
         * o---------------------------------------------------o
         *///}}}
        //}}}
        // PUSHED  COMMIT .. KEYMAP .. PROFILE {{{
        public void notify_keyMap_commit_done() //{{{
        {
            if( DX1Utility.Debug ) log("notify_keyMap_commit_done");

            keyMap_changeToCommit = "";
        }
        //}}}
        public void notify_profile_commit_done() //{{{
        {
            if( DX1Utility.Debug ) log("notify_profile_commit_done");

            profile_changeToCommit = "";
        }
        //}}}
        public void notify_all_commit_done() //{{{
        {
            if( DX1Utility.Debug ) log("notify_all_commit_done");

            notify_keyMap_commit_done();
            notify_profile_commit_done();
        }
        //}}}
        //}}}

        public bool assignMacro(String name, ref List<KeyMap> keyMapList) // {{{
        {
            if(keyNum == 0) return false;

            keyMapList[keyNum-1].MacName = name;
            keyMapList[keyNum-1].KeyCode = 0;
            keyMapList[keyNum-1].KeyType = 0x3;
            keyMapList[keyNum-1].KeyDesc = name;
            /* done */ keyNum=0;

            return true;
        }
        // }}}

        public byte[] get_keyMapList_num_type_code_byteArray(List<KeyMap> keyMapList) // {{{
        {
            byte[] byte_array = new Byte[3 * 50];
            int Offset = 0;

            foreach(KeyMap keyMap in keyMapList)
            {
                keyMap.to_num_type_code_byteArray().CopyTo(byte_array, Offset);
                Offset = Offset + 3;
            }
            return byte_array;
        }
        // }}}

        /* log {{{*/
        private void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }
}
