using System;

namespace DX1Utility
{
    public class Util
    {
        public static string RetrieveLinkerTimestamp() // {{{
        {
            byte[] b = new byte[2048];
            System.IO.Stream s = null;
            try {
                string filePath =     System.Reflection.Assembly.GetCallingAssembly().Location;
                s               = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally { if(s!=null) s.Close(); }


            const int C_PEHEADEROFFSET          = 60;
            int i                               = System.BitConverter.ToInt32(b,     C_PEHEADEROFFSET);

            const int C_LINKERTIMESTAMPOFFSET   = 8;
            int secondsSince1970                = System.BitConverter.ToInt32(b, i + C_LINKERTIMESTAMPOFFSET);

            System.DateTime dt                  = new System.DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds( secondsSince1970 );
            dt = dt.AddHours  ( System.TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours );


            // COMPILE TIME & LAUNCH TIME
            string          time_stamp          = " - V"+dt.ToString("yyMMdd");
            TimeSpan        span                = DateTime.Now.Subtract( dt );

            // AGE
            time_stamp         += " (";

            if     (span.Days    > 730) time_stamp += (span.Days / 365) +" years ";
            else if(span.Days    > 365) time_stamp += (span.Days / 365) +" year ";

            else if(span.Days    >  60) time_stamp += (span.Days /  30) +" months ";
            else if(span.Days    >  30) time_stamp += (span.Days /  30) +" month  ";

            else if(span.Days    >   2) time_stamp +=  span.Days        +" days ";
            else if(span.Days    >   0) time_stamp +=  span.Days        +" day ";

            else if(span.Hours   >   1) time_stamp +=  span.Hours       +" hours ";
            else if(span.Hours   >   0) time_stamp +=  span.Hours       +" hour ";

            else if(span.Minutes >   0) time_stamp +=  span.Minutes     +" min ";

            else                        time_stamp +=  span.Seconds      +" sec ";

            time_stamp         += "old)";

            if     (span.Seconds <  10) time_stamp  = time_stamp.ToUpper();

            return time_stamp;
        } //}}}
    }

}


