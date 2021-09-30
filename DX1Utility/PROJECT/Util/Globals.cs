using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    public static class Globals
    {

        // FOLDERS .. FILES
        public static string MyDocumentsFolder          = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
        public static string UserProfileFolder          = MyDocumentsFolder + "\\DX1\\Profiles\\";
        public static string ProfileMacroPath           = UserProfileFolder + "\\Macros\\";

        // NAMES
        public const  string MENU_GLOBAL_PROFILE        = "Global";

        // COUNT
        public  const int    KEYS_MAX                   = 50;

        // UI TEXT
        public const  string SYMBOL_ARROW_L             = "\x25C4";
        public const  string SYMBOL_ARROW_R             = "\x25BA";
        public const  string SYMBOL_CHECK               = "\x2713 ";
        public const  string SYMBOL_SUN                 = "\x2600";
        public const  string SYMBOL_GEAR                = "\x2699";
        public const  string MENU_NEW_PROFILE           = "...new profile";
        public const  string OPEN_PROFILE_FOLDER        = "...open profile folder";
        public const  string OPEN_EXECUTABLE_FOLDER     = "...open executable folder";
        public const  string B_START_MAPPING_TEXT       = "Start mapping the selected DX1 key";

        public const  string NOTIFY_TITLE               = DX1Utility.APP_WINDOW_TITLE;
        public const  string NOTIFY_TEXT                = "NOTIFY_TEXT";

        // UI OPACITY
        public const  float  OPACITY_WRITING            = 1.00F;
        public const  float  OPACITY_PROFILE            = 0.95F;
        public const  float  OPACITY_FOCUS              = 0.90F;
        public const  float  OPACITY_UPDATED            = 0.50F;

        // ARGUMENT FLAGS
        public const  bool   CHECKED_PASSIVE            = true;

    }
}
