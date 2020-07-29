using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    static class KeyConversionTable
    {
    /* Virtual-Key Codes {{{*/
/*{{{
:!start explorer https://docs.microsoft.com/en-us/windows/desktop/inputdev/virtual-key-codes
}}}*/
    // [0] {{{
    public const byte VK_LBUTTON              = 0x01; // Left mouse button
    public const byte VK_RBUTTON              = 0x02; // Right mouse button
    public const byte VK_CANCEL               = 0x03; // Control-break processing
    public const byte VK_MBUTTON              = 0x04; // Middle mouse button (three-button mouse)
    public const byte VK_XBUTTON1             = 0x05; // X1 mouse button
    public const byte VK_XBUTTON2             = 0x06; // X2 mouse button
    public const byte VK_BACK                 = 0x08; // BACKSPACE key
    public const byte VK_TAB                  = 0x09; // TAB key
    public const byte VK_CLEAR                = 0x0C; // CLEAR key
    public const byte VK_RETURN               = 0x0D; // ENTER key
    //}}}
    // [1] {{{
    public const byte VK_SHIFT                = 0x10; // SHIFT key
    public const byte VK_CONTROL              = 0x11; // CTRL key
    public const byte VK_MENU                 = 0x12; // ALT key
    public const byte VK_PAUSE                = 0x13; // PAUSE key
    public const byte VK_CAPITAL              = 0x14; // CAPS LOCK key
    public const byte VK_KANA                 = 0x15; // IME Kana mode
    public const byte VK_HANGUEL              = 0x15; // IME Hanguel mode (maintained for compatibility; use VK_HANGUL)
    public const byte VK_HANGUL               = 0x15; // IME Hangul mode
    public const byte VK_JUNJA                = 0x17; // IME Junja mode
    public const byte VK_FINAL                = 0x18; // IME final mode
    public const byte VK_HANJA                = 0x19; // IME Hanja mode
    public const byte VK_KANJI                = 0x19; // IME Kanji mode
    public const byte VK_ESCAPE               = 0x1B; // ESC key (27 decimal)
    public const byte VK_CONVERT              = 0x1C; // IME convert
    public const byte VK_NONCONVERT           = 0x1D; // IME nonconvert
    public const byte VK_ACCEPT               = 0x1E; // IME accept
    public const byte VK_MODECHANGE           = 0x1F; // IME mode change request
    //}}}
    // [2] {{{
    public const byte VK_SPACE                = 0x20; // SPACEBAR
    public const byte VK_PRIOR                = 0x21; // PAGE UP key
    public const byte VK_NEXT                 = 0x22; // PAGE DOWN key
    public const byte VK_END                  = 0x23; // END key
    public const byte VK_HOME                 = 0x24; // HOME key
    public const byte VK_LEFT                 = 0x25; // LEFT ARROW key
    public const byte VK_UP                   = 0x26; // UP ARROW key
    public const byte VK_RIGHT                = 0x27; // RIGHT ARROW key
    public const byte VK_DOWN                 = 0x28; // DOWN ARROW key
    public const byte VK_SELECT               = 0x29; // SELECT key
    public const byte VK_PRINT                = 0x2A; // PRINT key
    public const byte VK_EXECUTE              = 0x2B; // EXECUTE key
    public const byte VK_SNAPSHOT             = 0x2C; // PRINT SCREEN key
    public const byte VK_INSERT               = 0x2D; // INS key
    public const byte VK_DELETE               = 0x2E; // DEL key
    public const byte VK_HELP                 = 0x2F; // HELP key
    //}}}
    // [5] {{{
    public const byte VK_LWIN                 = 0x5B; // Left Windows key (Natural keyboard)
    public const byte VK_RWIN                 = 0x5C; // Right Windows key (Natural keyboard)
    public const byte VK_APPS                 = 0x5D; // Applications key (Natural keyboard)
    public const byte VK_SLEEP                = 0x5F; // Computer Sleep key
    //}}}
    // [6] {{{
    public const byte VK_NUMPAD0              = 0x60; // Numeric keypad 0 key
    public const byte VK_NUMPAD1              = 0x61; // Numeric keypad 1 key
    public const byte VK_NUMPAD2              = 0x62; // Numeric keypad 2 key
    public const byte VK_NUMPAD3              = 0x63; // Numeric keypad 3 key
    public const byte VK_NUMPAD4              = 0x64; // Numeric keypad 4 key
    public const byte VK_NUMPAD5              = 0x65; // Numeric keypad 5 key
    public const byte VK_NUMPAD6              = 0x66; // Numeric keypad 6 key
    public const byte VK_NUMPAD7              = 0x67; // Numeric keypad 7 key
    public const byte VK_NUMPAD8              = 0x68; // Numeric keypad 8 key
    public const byte VK_NUMPAD9              = 0x69; // Numeric keypad 9 key
    public const byte VK_MULTIPLY             = 0x6A; // Multiply key
    public const byte VK_ADD                  = 0x6B; // Add key
    public const byte VK_SEPARATOR            = 0x6C; // Separator key
    public const byte VK_SUBTRACT             = 0x6D; // Subtract key
    public const byte VK_DECIMAL              = 0x6E; // Decimal key
    public const byte VK_DIVIDE               = 0x6F; // Divide key
    //}}}
    // [7] {{{
    public const byte VK_F1                   = 0x70; // F1 key
    public const byte VK_F2                   = 0x71; // F2 key
    public const byte VK_F3                   = 0x72; // F3 key
    public const byte VK_F4                   = 0x73; // F4 key
    public const byte VK_F5                   = 0x74; // F5 key
    public const byte VK_F6                   = 0x75; // F6 key
    public const byte VK_F7                   = 0x76; // F7 key
    public const byte VK_F8                   = 0x77; // F8 key
    public const byte VK_F9                   = 0x78; // F9 key
    public const byte VK_F10                  = 0x79; // F10 key
    public const byte VK_F11                  = 0x7A; // F11 key
    public const byte VK_F12                  = 0x7B; // F12 key
    public const byte VK_F13                  = 0x7C; // F13 key
    public const byte VK_F14                  = 0x7D; // F14 key
    public const byte VK_F15                  = 0x7E; // F15 key
    public const byte VK_F16                  = 0x7F; // F16 key
    //}}}
    // [8] {{{
    public const byte VK_F17                  = 0x80; // F17 key
    public const byte VK_F18                  = 0x81; // F18 key
    public const byte VK_F19                  = 0x82; // F19 key
    public const byte VK_F20                  = 0x83; // F20 key
    public const byte VK_F21                  = 0x84; // F21 key
    public const byte VK_F22                  = 0x85; // F22 key
    public const byte VK_F23                  = 0x86; // F23 key
    public const byte VK_F24                  = 0x87; // F24 key
    //}}}
    // [9] {{{
    public const byte VK_NUMLOCK              = 0x90; // NUM LOCK key
    public const byte VK_SCROLL               = 0x91; // SCROLL LOCK key
    //}}}
    // [A] {{{
    public const byte VK_LSHIFT               = 0xA0; // Left SHIFT key
    public const byte VK_RSHIFT               = 0xA1; // Right SHIFT key
    public const byte VK_LCONTROL             = 0xA2; // Left CONTROL key
    public const byte VK_RCONTROL             = 0xA3; // Right CONTROL key
    public const byte VK_LMENU                = 0xA4; // Left MENU key
    public const byte VK_RMENU                = 0xA5; // Right MENU key
    public const byte VK_BROWSER_BACK         = 0xA6; // Browser Back key
    public const byte VK_BROWSER_FORWARD      = 0xA7; // Browser Forward key
    public const byte VK_BROWSER_REFRESH      = 0xA8; // Browser Refresh key
    public const byte VK_BROWSER_STOP         = 0xA9; // Browser Stop key
    public const byte VK_BROWSER_SEARCH       = 0xAA; // Browser Search key
    public const byte VK_BROWSER_FAVORITES    = 0xAB; // Browser Favorites key
    public const byte VK_BROWSER_HOME         = 0xAC; // Browser Start and Home key
    public const byte VK_VOLUME_MUTE          = 0xAD; // Volume Mute key
    public const byte VK_VOLUME_DOWN          = 0xAE; // Volume Down key
    public const byte VK_VOLUME_UP            = 0xAF; // Volume Up key
    //}}}
    // [B] {{{
    public const byte VK_MEDIA_NEXT_TRACK     = 0xB0; // Next Track key
    public const byte VK_MEDIA_PREV_TRACK     = 0xB1; // Previous Track key
    public const byte VK_MEDIA_STOP           = 0xB2; // Stop Media key
    public const byte VK_MEDIA_PLAY_PAUSE     = 0xB3; // Play/Pause Media key
    public const byte VK_LAUNCH_MAIL          = 0xB4; // Start Mail key
    public const byte VK_LAUNCH_MEDIA_SELECT  = 0xB5; // Select Media key
    public const byte VK_LAUNCH_APP1          = 0xB6; // Start Application 1 key
    public const byte VK_LAUNCH_APP2          = 0xB7; // Start Application 2 key
    //......................................... 0xB8-B9     Reserved
    public const byte VK_OEM_1                = 0xBA; // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ';:' key
    public const byte VK_OEM_PLUS             = 0xBB; // For any country/region, the '+' key
    public const byte VK_OEM_COMMA            = 0xBC; // For any country/region, the ',' key
    public const byte VK_OEM_MINUS            = 0xBD; // For any country/region, the '-' key
    public const byte VK_OEM_PERIOD           = 0xBE; // For any country/region, the '.' key
    public const byte VK_OEM_2                = 0xBF; // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '/?' key
    //}}}
    // [C] {{{
    public const byte VK_OEM_3                = 0xC0; // Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the '`~' key
    //......................................... 0xC1-D7     Reserved
    //}}}
    // [D] {{{
    //......................................... 0xD8-DA     Unassigned
    public const byte VK_OEM_4                = 0xDB; // Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the '[{' key
    public const byte VK_OEM_5                = 0xDC; // Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the '\|' key
    public const byte VK_OEM_6                = 0xDD; // Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the ']}' key
    public const byte VK_OEM_7                = 0xDE; // Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the 'single-quote/double-quote' key
    public const byte VK_OEM_8                = 0xDF; // Used for miscellaneous characters; it can vary by keyboard.
    //}}}
    // [E] {{{
    //......................................... 0xE0; Reserved
    public const byte VK_OEM_102              = 0xE2; // Either the angle bracket key or the backslash key on the RT 102-key keyboard
    public const byte VK_PROCESSKEY           = 0xE5; // IME PROCESS key
    public const byte VK_PACKET               = 0xE7; // Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
    //......................................... 0xE8; Unassigned
    //}}}
    // [F] {{{
    public const byte VK_ATTN                 = 0xF6; // Attn key
    public const byte VK_CRSEL                = 0xF7; // CrSel key
    public const byte VK_EXSEL                = 0xF8; // ExSel key
    public const byte VK_EREOF                = 0xF9; // Erase EOF key
    public const byte VK_PLAY                 = 0xFA; // Play key
    public const byte VK_ZOOM                 = 0xFB; // Zoom key
    public const byte VK_NONAME               = 0xFC; // Reserved
    public const byte VK_PA1                  = 0xFD; // PA1 key
    public const byte VK_OEM_CLEAR            = 0xFE; // Clear key
    //}}}
    /*}}}*/
    /* VK_DICT {{{*/
/*{{{
    private static Dictionary<byte, string> VK_DICT = new Dictionary<byte, string>()
    {
    // {0} {{{
      { VK_LBUTTON              , " Left mouse button" }
    , { VK_RBUTTON              , " Right mouse button" }
    , { VK_CANCEL               , " Control-break processing" }
    , { VK_MBUTTON              , " Middle mouse button (three-button mouse)" }
    , { VK_XBUTTON1             , " X1 mouse button" }
    , { VK_XBUTTON2             , " X2 mouse button" }
    , { VK_BACK                 , " BACKSPACE key" }
    , { VK_TAB                  , " TAB key" }
    , { VK_CLEAR                , " CLEAR key" }
    , { VK_RETURN               , " ENTER key" }
    //}}}
    // {1} {{{
    , { VK_SHIFT                , " SHIFT key" }
    , { VK_CONTROL              , " CTRL key" }
    , { VK_MENU                 , " ALT key" }
    , { VK_PAUSE                , " PAUSE key" }
    , { VK_CAPITAL              , " CAPS LOCK key" }
    , { VK_KANA                 , " IME Kana mode" }
    , { VK_HANGUEL              , " IME Hanguel mode (maintained for compatibility; use VK_HANGUL)" }
    , { VK_HANGUL               , " IME Hangul mode" }
    , { VK_JUNJA                , " IME Junja mode" }
    , { VK_FINAL                , " IME final mode" }
    , { VK_HANJA                , " IME Hanja mode" }
    , { VK_KANJI                , " IME Kanji mode" }
    , { VK_ESCAPE               , " ESC key (27 decimal)" }
    , { VK_CONVERT              , " IME convert" }
    , { VK_NONCONVERT           , " IME nonconvert" }
    , { VK_ACCEPT               , " IME accept" }
    , { VK_MODECHANGE           , " IME mode change request" }
    //}}}
    // {2} {{{
    , { VK_SPACE                , " SPACEBAR" }
    , { VK_PRIOR                , " PAGE UP key" }
    , { VK_NEXT                 , " PAGE DOWN key" }
    , { VK_END                  , " END key" }
    , { VK_HOME                 , " HOME key" }
    , { VK_LEFT                 , " LEFT ARROW key" }
    , { VK_UP                   , " UP ARROW key" }
    , { VK_RIGHT                , " RIGHT ARROW key" }
    , { VK_DOWN                 , " DOWN ARROW key" }
    , { VK_SELECT               , " SELECT key" }
    , { VK_PRINT                , " PRINT key" }
    , { VK_EXECUTE              , " EXECUTE key" }
    , { VK_SNAPSHOT             , " PRINT SCREEN key" }
    , { VK_INSERT               , " INS key" }
    , { VK_DELETE               , " DEL key" }
    , { VK_HELP                 , " HELP key" }
    //}}}
    // {5} {{{
    , { VK_LWIN                 , " Left Windows key (Natural keyboard) " }
    , { VK_RWIN                 , " Right Windows key (Natural keyboard)" }
    , { VK_APPS                 , " Applications key (Natural keyboard)" }
    , { VK_SLEEP                , " Computer Sleep key" }
    //}}}
    // {6} {{{
    , { VK_NUMPAD0              , " Numeric keypad 0 key" }
    , { VK_NUMPAD1              , " Numeric keypad 1 key" }
    , { VK_NUMPAD2              , " Numeric keypad 2 key" }
    , { VK_NUMPAD3              , " Numeric keypad 3 key" }
    , { VK_NUMPAD4              , " Numeric keypad 4 key" }
    , { VK_NUMPAD5              , " Numeric keypad 5 key" }
    , { VK_NUMPAD6              , " Numeric keypad 6 key" }
    , { VK_NUMPAD7              , " Numeric keypad 7 key" }
    , { VK_NUMPAD8              , " Numeric keypad 8 key" }
    , { VK_NUMPAD9              , " Numeric keypad 9 key" }
    , { VK_MULTIPLY             , " Multiply key" }
    , { VK_ADD                  , " Add key" }
    , { VK_SEPARATOR            , " Separator key" }
    , { VK_SUBTRACT             , " Subtract key" }
    , { VK_DECIMAL              , " Decimal key" }
    , { VK_DIVIDE               , " Divide key" }
    //}}}
    // {7} {{{
    , { VK_F1                   , " F1 key" }
    , { VK_F2                   , " F2 key" }
    , { VK_F3                   , " F3 key" }
    , { VK_F4                   , " F4 key" }
    , { VK_F5                   , " F5 key" }
    , { VK_F6                   , " F6 key" }
    , { VK_F7                   , " F7 key" }
    , { VK_F8                   , " F8 key" }
    , { VK_F9                   , " F9 key" }
    , { VK_F10                  , " F10 key" }
    , { VK_F11                  , " F11 key" }
    , { VK_F12                  , " F12 key" }
    , { VK_F13                  , " F13 key" }
    , { VK_F14                  , " F14 key" }
    , { VK_F15                  , " F15 key" }
    , { VK_F16                  , " F16 key" }
    //}}}
    // {8} {{{
    , { VK_F17                  , " F17 key" }
    , { VK_F18                  , " F18 key" }
    , { VK_F19                  , " F19 key" }
    , { VK_F20                  , " F20 key" }
    , { VK_F21                  , " F21 key" }
    , { VK_F22                  , " F22 key" }
    , { VK_F23                  , " F23 key" }
    , { VK_F24                  , " F24 key" }
    //}}}
    // {9} {{{
    , { VK_NUMLOCK              , " NUM LOCK key" }
    , { VK_SCROLL               , " SCROLL LOCK key" }
    //}}}
    // {A} {{{
    , { VK_LSHIFT               , " Left SHIFT key" }
    , { VK_RSHIFT               , " Right SHIFT key" }
    , { VK_LCONTROL             , " Left CONTROL key" }
    , { VK_RCONTROL             , " Right CONTROL key" }
    , { VK_LMENU                , " Left MENU key" }
    , { VK_RMENU                , " Right MENU key" }
    , { VK_BROWSER_BACK         , " Browser Back key" }
    , { VK_BROWSER_FORWARD      , " Browser Forward key" }
    , { VK_BROWSER_REFRESH      , " Browser Refresh key" }
    , { VK_BROWSER_STOP         , " Browser Stop key" }
    , { VK_BROWSER_SEARCH       , " Browser Search key " }
    , { VK_BROWSER_FAVORITES    , " Browser Favorites key" }
    , { VK_BROWSER_HOME         , " Browser Start and Home key" }
    , { VK_VOLUME_MUTE          , " Volume Mute key" }
    , { VK_VOLUME_DOWN          , " Volume Down key" }
    , { VK_VOLUME_UP            , " Volume Up key" }
    //}}}
    // {B} {{{
    , { VK_MEDIA_NEXT_TRACK     , " Next Track key" }
    , { VK_MEDIA_PREV_TRACK     , " Previous Track key" }
    , { VK_MEDIA_STOP           , " Stop Media key" }
    , { VK_MEDIA_PLAY_PAUSE     , " Play/Pause Media key" }
    , { VK_LAUNCH_MAIL          , " Start Mail key" }
    , { VK_LAUNCH_MEDIA_SELECT  , " Select Media key" }
    , { VK_LAUNCH_APP1          , " Start Application 1 key" }
    , { VK_LAUNCH_APP2          , " Start Application 2 key" }
    //......................................... 0xB8-B9     Reserved
    , { VK_OEM_1                , " Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ';:' key " }
    , { VK_OEM_PLUS             , " For any country/region, the '+' key" }
    , { VK_OEM_COMMA            , " For any country/region, the ',' key" }
    , { VK_OEM_MINUS            , " For any country/region, the '-' key" }
    , { VK_OEM_PERIOD           , " For any country/region, the '.' key" }
    , { VK_OEM_2                , " Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '/?' key " }
    //}}}
    // {C} {{{
    , { VK_OEM_3                , " Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the '`~' key " }
    //......................................... 0xC1-D7     Reserved
    //}}}
    // {D} {{{
    //......................................... 0xD8-DA     Unassigned
    , { VK_OEM_4                , " Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the '{' key" }
    , { VK_OEM_5                , " Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the '|' key" }
    , { VK_OEM_6                , " Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the '}' key" }
    , { VK_OEM_7                , " Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the 'single-quote/double-quote' key" }
    , { VK_OEM_8                , " Used for miscellaneous characters; it can vary by keyboard." }
    //}}}
    // {E} {{{
    //......................................... 0xE0; Reserved
    , { VK_OEM_102              , " Either the angle bracket key or the backslash key on the RT 102-key keyboard" }
    , { VK_PROCESSKEY           , " IME PROCESS key" }
    , { VK_PACKET               , " Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP" }
    //......................................... 0xE8; Unassigned
    //}}}
    // {F} {{{
    , { VK_ATTN                 , " Attn key" }
    , { VK_CRSEL                , " CrSel key" }
    , { VK_EXSEL                , " ExSel key" }
    , { VK_EREOF                , " Erase EOF key" }
    , { VK_PLAY                 , " Play key" }
    , { VK_ZOOM                 , " Zoom key" }
    , { VK_NONAME               , " Reserved " }
    , { VK_PA1                  , " PA1 key" }
    , { VK_OEM_CLEAR            , " Clear key" }
    //}}}
    };
}}}*/
    /*}}}*/
#region Const {{{
//    /* ODD_MAPPINGS {{{*/
//    private static Byte[] ODD_MAPPINGS = {
//        (Byte)'0', 0x0d, 0x1b, 0x08 // 0     ENTER ESCAPE BACK
//            ,    0x09, 0x20, 0xbd, 0xbb // TAB   SPACE MINUS  PLUS
//            ,    0xdb, 0xdd, 0xdc,    0 // oem4  oem6  oem5   0     // placeholder .. ?
//            ,    0xba, 0xde, 0xc0, 0xbc // oem1  oem7  oem3   COMMA
//            ,    0xbe, 0xbf, 0x14, 0x70 // DOT   OEM2  MAJ    F1
//            ,    0x71, 0x72, 0x73, 0x74 // F2    F3    F4     F5
//            ,    0x75, 0x76, 0x77, 0x78 // F6    F7    F8     F9
//            ,    0x79, 0x7a, 0x7b,    0 // F10   F11   F12    0     // placeholder .. PrintScreen
//            ,    0x91, 0x13, 0x2d, 0x24 // SCRLK PAUSE INSERT HOME
//            ,    0x21, 0x2e, 0x23, 0x22 // PGUP  DEL   END    PGDN
//            ,    0x27, 0x25, 0x28, 0x26 // RIGHT LEFT  DOWN    UP
//            ,    0x90, 0x6f, 0x6a, 0x6d // NLCK /      *      -
//            ,    0x6b, 0x0d, 0x61, 0x62 // +     ENTER NUM1   NUM2
//            ,    0x63, 0x64, 0x65, 0x66 // NUM3  NUM4  NUM5   NUM6
//            ,    0x67, 0x68, 0x69, 0x60 // NUM7  NUM8  NUM9   NUM0
//            ,    0x6e                   // VK_DECIMAL
//    };
//    /*}}}*/
    /* ODD_MAPPINGS .. (39..90) {{{*/
    private static Byte[] ODD_MAPPINGS
    = {   (Byte)'0'     ,  VK_RETURN  ,  VK_ESCAPE    , VK_BACK         // 39..42 (digit 0 is here!)
        , VK_TAB        ,  VK_SPACE   ,  VK_OEM_MINUS , VK_OEM_PLUS     // 43..46
        , VK_OEM_4      ,  VK_OEM_6   ,  VK_OEM_5     , 0               // 47..50
        , VK_OEM_1      ,  VK_OEM_7   ,  VK_OEM_3     , VK_OEM_COMMA    // 51..54
        , VK_OEM_PERIOD ,  VK_OEM_2   ,  VK_CAPITAL   , VK_F1           // 55..58
        , VK_F2         ,  VK_F3      ,  VK_F4        , VK_F5           // 59..62
        , VK_F6         ,  VK_F7      ,  VK_F8        , VK_F9           // 63..66
        , VK_F10        ,  VK_F11     ,  VK_F12       , 0               // 67..70
/*{{{   // seems to work in DX1Utility .. but results in sending VK_SCROLL...
        , VK_F10        ,  VK_F11     ,  VK_F12       , VK_F13
        , VK_F14        ,  VK_F15     ,  VK_F16       , VK_F17
        , VK_F18        ,  VK_F19     ,  VK_F20       , VK_F21
        , VK_F22        ,  VK_F23     ,  VK_F24
}}}*/
        , VK_SCROLL     ,  VK_PAUSE   ,  VK_INSERT    , VK_HOME         // 71..74
        , VK_PRIOR      ,  VK_DELETE  ,  VK_END       , VK_NEXT         // 75..78
        , VK_RIGHT      ,  VK_LEFT    ,  VK_DOWN      , VK_UP           // 79..82 == VK_LEFT == ODD_MAPPINGS[41]=[80] = (Byte)(0x27 + i=41] = [39 + 41]=[80]
        , VK_NUMLOCK    ,  VK_DIVIDE  ,  VK_MULTIPLY  , VK_SUBTRACT     // 83..86
        , VK_ADD        ,  VK_RETURN  ,  VK_NUMPAD1   , VK_NUMPAD2      // 87..90
        , VK_NUMPAD3    ,  VK_NUMPAD4 ,  VK_NUMPAD5   , VK_NUMPAD6      // 91..94
        , VK_NUMPAD7    ,  VK_NUMPAD8 ,  VK_NUMPAD9   , VK_NUMPAD0      // 95..98
        , VK_DECIMAL                                                    // 99
    };

    /*}}}*/
    /* SPECIAL_MAPPINGS {{{*/
    private static Byte[] SPECIAL_MAPPINGS
    = {   VK_LCONTROL, VK_LSHIFT, VK_LMENU, VK_LWIN
        , VK_RCONTROL, VK_RSHIFT, VK_RMENU, VK_RWIN
    };

    /*}}}*/
#endregion }}}
#region variables {{{
    private static byte[][] KTable;

    //        private static bool Initialized = false;

#endregion }}}

    public static Byte[][] KeyPairConversionTable // {{{
    {
        get {
            if(KTable == null)
                KTable = InitTable();
            return KTable;
        }
    }
    // }}}
    private static byte[][] InitTable() // {{{
    {
        bool log_this =  DX1Utility.Debug;

        if(  log_this ) log("KeyConversionTable.InitTable():");

        /*................KeyPairConversionTable[......index.........] [value ] */                     byte[][] table     = new Byte[256][ ];
        for (                                                  byte vk = (byte)'A'; vk <  (byte)'Z'; ++vk)    { table[vk] = new Byte     [3] { 1 , (Byte)(0x04 + (vk-'A')) , vk }; } // [04       ..       29]  (25 keys)
        for (                                                  byte vk = (byte)'1'; vk <= (byte)'9'; ++vk)    { table[vk] = new Byte     [3] { 1 , (Byte)(0x1e + (vk-'1')) , vk }; } // [30       ..       39]  (10 keys)
        for (byte i =  0 ; i <     ODD_MAPPINGS.Length; ++i) { byte vk =     ODD_MAPPINGS[i];      if (vk != 0) table[vk] = new Byte     [3] { 1 , (Byte)(0x27 +  i      ) , vk }; } // [39       ..       99]  (61 keys)
        for (byte i =  0 ; i < SPECIAL_MAPPINGS.Length; ++i) { byte vk = SPECIAL_MAPPINGS[i];      if (vk != 0) table[vk] = new Byte     [3] { 2 , (Byte)(   1 << i      ) , vk }; } // [0 2 4 8 16 32 64 128]  ( 8 keys)

/*{{{*/
        // LETTERS [            A..Z] [26 keys]  [65..90    0x41..0x5A] [04..29]
        // DIGITS  [            1..9] [10 keys]  [49..57    0x31..0x39] [30..39]
        // ODD     [    ODD_MAPPINGS] [61 keys]  [vk      ODD_MAPPINGS] [39..99] .. [VK_LEFT 0x50 80]=[]
        // SCECIAL [SPECIAL_MAPPINGS] [ 8 keys]  [vk  SPECIAL_MAPPINGS] [ 1<<8 ]
/*}}}*/
/*{{{*/
        if( log_this ) log("LETTERS [ A..Z ] [26 keys] ["+0x04+" .. "+(0x04+(                 'Z' - 'A'))+"]");
        if( log_this ) log("DIGITS  [ 0..9 ] [10 keys] ["+0x1e+" .. "+(0x1e+(                 '9' - '0'))+"]");
        if( log_this ) log("ODD_MAPPINGS     [61 keys] ["+0x27+" .. "+(0x27+( ODD_MAPPINGS.Length -  1 ))+"]");
        if( log_this ) log("SPECIAL_MAPPINGS [ 8 keys] [     1   .. "+(   1 << 8                        )+"]");
/*}}}*/

        return table;
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
