//{{{
using System;
using System.Windows.Forms;
//}}}
namespace DX1Utility
{
    public interface LoggerInterface
    {
        Object Invoke(Delegate method);
        void      log(string msg);
    }

    public class Logger {

        private static LoggerInterface AppLogger;

        public  static void            LogStart(LoggerInterface appLogger)
        {
            AppLogger = appLogger;
        }

        public  static void            Log(string msg)
        {
            if(((Control)AppLogger).InvokeRequired )
                AppLogger.Invoke( (MethodInvoker)delegate() { AppLogger.log( msg ); } ); // async  call   to UI Thread
            else                                              AppLogger.log( msg );      // direct call from UI Thread
        }

    }
}
