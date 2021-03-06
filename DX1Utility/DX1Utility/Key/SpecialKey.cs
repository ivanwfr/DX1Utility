﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    public enum SpecialKeysExtraData { None=0, Boolean=1, TextBox=2, Slider=4, Byte=5 }

    public class SpecialKey
    {
        private int                         _SpecialID;
        private string                      _SpecialName;
        private ushort                      _SpecialValue;

        private SpecialKeysExtraData        _ExtraDataType      = 0;                                // Extra Data type (1=Boolean, 2=Textbox, 4=Slider)
        private Dictionary<string, string>  _ExtraDataParams    = new Dictionary<string,string>();  // Extra data display
        private bool                        _ReqData            = false;                            // Extra settings required
        
        public int                                SpecialID { get { return _SpecialID;       } set { _SpecialID       = value; } }
        public string                           SpecialName { get { return _SpecialName;     } set { _SpecialName     = value; } }
        public ushort                          SpecialValue { get { return _SpecialValue;    } set { _SpecialValue    = value; } }

        public Dictionary<string, string>   ExtraDataParams { get { return _ExtraDataParams; } set { _ExtraDataParams = value; } }
        public SpecialKeysExtraData           ExtraDataType { get { return _ExtraDataType;   } set { _ExtraDataType   = value; } }
        public bool                                 ReqData { get { return _ReqData;         } set { _ReqData         = value; } }

    }
}
