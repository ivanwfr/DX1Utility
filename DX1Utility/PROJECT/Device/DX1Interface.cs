using System;
using System.Runtime.InteropServices;

namespace DX1Utility
{
#region [StructLayout] {{{
    [StructLayout(LayoutKind.Sequential)]
        public struct DEV_BROADCAST_DEVICEHANDLE // {{{
        {
            public uint dbch_size;
            public uint dbch_devicetype;
            public uint dbch_reserved;

            public IntPtr dcbh_handle;
            public IntPtr dcbh_hdevnotify;

            public Guid dcbh_eventguid;
            public UInt32 dcbh_nameoffset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]        // Currently all out packets are 6 bytes Max
                public Byte[] dbch_data;
        }
    // }}}
    [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA // {{{
        {
            public uint                 cbSize;
            public Guid                 ClassGuid;
            public uint                 DevInst;
            public IntPtr               Reserved;
        }
    // }}}
    [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVICE_INTERFACE_DATA // {{{
        {
            public int                  cbSize;
            public System.Guid          InterfaceClassGuid;
            public int                  Flags;
            public IntPtr Reserved;
        }
    // }}}
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SP_DEVICE_INTERFACE_DETAIL_DATA // {{{
        {
            public UInt32                       cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
                public string                       DevicePath;
        }
    // }}}
    [StructLayout(LayoutKind.Sequential)]
        public struct DEV_BROADCAST_DEVICEINTERFACE { // {{{
            public uint         dbcc_size;
            public uint         dbcc_devicetype;
            public uint         dbcc_reserved;
            public Guid         dbcc_classguid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=1)]
                public string       dbcc_name;
        }
        // }}}
#endregion }}}
#region [Flags] {{{
    [Flags]
        public enum FileAccess : uint // {{{
        {
            GenericAll     = 0x10000000,
            GenericExecute = 0x20000000,
            GenericRead    = 0x80000000,
            GenericWrite   = 0x40000000
        }
    // }}}
    [Flags]
        public enum FileShare : uint // {{{
        {
            /// <summary>
            /// Enables subsequent open operations on an object to request (delete,read,write) access.
            /// If this flag is not specified, but the object has been opened for (delete,read,write) access, the function fails.
            /// Otherwise, other processes cannot open the object if they request (delete,read,write) access.
            /// </summary>

            None   = 0x00000000,
            Read   = 0x00000001,
            Write  = 0x00000002,
            Delete = 0x00000004
        }
    // }}}
    [Flags]
        public enum CreationDisposition : uint // {{{
        {
            /// <summary>
            /// Creates a new file. The function fails if a specified file exists.
            /// </summary>
            New = 1,

            /// <summary>
            /// Creates a new file, always.
            /// If a file exists, the function overwrites the file, clears the existing attributes, combines the specified file attributes,
            /// and flags with FILE_ATTRIBUTE_ARCHIVE, but does not set the security descriptor that the SECURITY_ATTRIBUTES structure specifies.
            /// </summary>
            CreateAlways = 2,

            /// <summary>
            /// Opens a file. The function fails if the file does not exist.
            /// </summary>
            OpenExisting = 3,

            /// <summary>
            /// Opens a file, always.
            /// If a file does not exist, the function creates a file as if dwCreationDisposition is CREATE_NEW.
            /// </summary>
            OpenAlways = 4,

            /// <summary>
            /// Opens a file and truncates it so that its size is 0 (zero) bytes. The function fails if the file does not exist.
            /// The calling process must open the file with the GENERIC_WRITE access right.
            /// </summary>
            TruncateExisting = 5

        }
    // }}}
    [Flags]
        public enum FileAttributes : uint // {{{
        {
            Readonly               = 0x00000001,
            Hidden                 = 0x00000002,
            System                 = 0x00000004,
            Directory              = 0x00000010,
            Archive                = 0x00000020,
            Device                 = 0x00000040,
            Normal                 = 0x00000080,
            Temporary              = 0x00000100,
            SparseFile             = 0x00000200,
            ReparsePoint           = 0x00000400,
            Compressed             = 0x00000800,
            Offline                = 0x00001000,
            NotContentIndexed      = 0x00002000,
            Encrypted              = 0x00004000,
            SecurityImpersonation  = 0x00020000,
            Write_Through          = 0x80000000,
            Overlapped             = 0x40000000,
            NoBuffering            = 0x20000000,
            RandomAccess           = 0x10000000,
            SequentialScan         = 0x08000000,
            DeleteOnClose          = 0x04000000,
            BackupSemantics        = 0x02000000,
            PosixSemantics         = 0x01000000,
            OpenReparsePoint       = 0x00200000,
            OpenNoRecall           = 0x00100000,
            FirstPipeInstance      = 0x00080000

        }
    // }}}
#endregion }}}

    public class DX1Device
    {
#region const {{{
    private const string    DX1_GUID                    = "573E8C73-0CB4-4471-A1BF-FAB26C31D384";
    private const string    DX1_VID                     = "1603";       // USB  VENDOR ID
    private const string    DX1_PID                     = "0002";       // USB PRODUCT ID

    private const int       DIGCF_DEFAULT               = 0x00000001;   // only valid with DIGCF_DEVICEINTERFACE
    private const int       DIGCF_PRESENT               = 0x00000002;
    private const int       DIGCF_ALLCLASSES            = 0x00000004;
    private const int       DIGCF_PROFILE               = 0x00000008;
    private const int       DIGCF_DEVICEINTERFACE       = 0x00000010;

//  private const int       kNumOutstandingReadRequests = 16;
//  private const int       kSizeOfReadRequest          = 8;

#endregion }}}
#region instance private variables {{{
    private IntPtr  dx1Handle       = IntPtr.Zero;
    private IntPtr  formHandle      = IntPtr.Zero;
    private bool    in_KEYMAP_mode  = false;

#endregion }}}
#region public methods

    // ACCESS DEVICE
    public DX1Device(IntPtr formHandle) //{{{
    {
        openDevice(true); // synchronous

        this.formHandle = formHandle;
        this.registerWindowForEvents();
    }
    //}}}

    // KEYMAP MODE ENTER
    public void enter_KEYMAP_MODE() // {{{
    {
        // (RED LED) .. (Device Firmware Update)
        // .. called by OnActivated
        // .. called by ApplyKeySet
        if( DX1Utility.Debug ) log("Device.enter_KEYMAP_MODE()");

        // device, Write, command, buffered
        UInt32 CONTROLCODE_KEYMAP_ENTER
            = 0x65500 << 16
            |       2 << 14
            |   0x810 <<  2     // KEYMAP RED LED
            | 0
            ;

        uint bytesReturned = 0;

        bool result
            = NativeMethods.DeviceIoControl(
                dx1Handle           , CONTROLCODE_KEYMAP_ENTER
                , IntPtr.Zero       , 0                 // Bufffer-OUT
                , IntPtr.Zero       , 0                 // Bufffer-IN
                , ref bytesReturned , IntPtr.Zero       // RESULTS
                );
        if( DX1Utility.Debug ) log("Device.enter_KEYMAP_MODE() .. result "+ result);

        in_KEYMAP_mode = true;
        if( DX1Utility.Debug ) log(".. in_KEYMAP_mode set to "+in_KEYMAP_mode);
    }
    // }}}

    // KEYMAP MODE UPDATE
    public void sendProgramPacket(Byte[] mapping) // {{{
    {
        // Called by ApplyKeySet
        if( DX1Utility.Debug ) log("Device.sendProgramPacket(): ..  (mapping == null): "+(mapping == null));

        // device, Write, command, buffered
        UInt32 CONTROLCODE_MAPPING
            = 0x65500 << 16
            |       2 << 14
            |   0x801 <<  2     // key mappings .. sending an array of [num type code]
            | 0
            ;

        // COMMANDPACKET
        if(mapping == null) {
            Byte[] COMMANDPACKET
                = { 0x02, 0x01, 0x06, 0x01 , 0x03, 0x00, 0x03, 0x03
                  , 0x00, 0x00, 0x00, 0x00 , 0x00, 0x00, 0x00, 0x04
                  , 0x01, 0x07
                };

            mapping = COMMANDPACKET;
        }

        uint bytesReturned = 0;

        bool result
            = NativeMethods.DeviceIoControl(
                dx1Handle           , CONTROLCODE_MAPPING
                , mapping           , mapping.Length    // Bufffer-OUT
                , IntPtr.Zero       , 0                 // Bufffer-IN
                , ref bytesReturned , IntPtr.Zero       // RESULTS
                );
        if( DX1Utility.Debug ) log("Device.sendProgramPacket() .. result "+ result);

        in_KEYMAP_mode = false;
        if( DX1Utility.Debug ) log(".. in_KEYMAP_mode set to "+in_KEYMAP_mode);
    }
    // }}}

    // KEYMAP MODE LEAVE
    public void leave_KEYMAP_MODE() // {{{
    {
        if( DX1Utility.Debug ) log("Device.leave_KEYMAP_MODE()");
        log("FIXME .. NO-OP .. CONTROLCODE_KEYMAP_LEAVE not documented yet");
        return;
//FIXME LOCKS DEVICE IN KEYMAP MODE .. UNTIL USB UNPLUGGED
/*{{{
 
        // device, Write, command, buffered
        UInt32 CONTROLCODE_KEYMAP_LEAVE
            = 0x65500 << 16
            |       2 << 14
            |   0x801 <<  2     // key mappings .. sending an array of [num type code]
            | 0
            ;

        uint bytesReturned = 0;

        bool result
            = NativeMethods.DeviceIoControl(
                dx1Handle           , CONTROLCODE_KEYMAP_LEAVE
                , IntPtr.Zero       , 0                 // Bufffer-OUT
                , IntPtr.Zero       , 0                 // Bufffer-IN
                , ref bytesReturned , IntPtr.Zero       // RESULTS
                );
        if( DX1Utility.Debug ) log("Device.leave_KEYMAP_MODE() .. result "+ result);
}}}*/
    }
    // }}}

    // ON-OFF
    public bool is_ON() //{{{
    {
        if( DX1Utility.Debug ) log("Device.is_ON() .. return "+ (dx1Handle != (IntPtr)0));

        return (dx1Handle != (IntPtr)0);
    }
    // }}}

    // KEYMAP MODE
    public bool is_in_KEYMAP_MODE() //{{{
    {
        if( DX1Utility.Debug ) log("Device.is_in_KEYMAP_MODE() .. return "+ in_KEYMAP_mode);

        return in_KEYMAP_mode;
    }
    // }}}

    // DEVICE INPUT EVENTS
    public bool registerWindowForEvents() // {{{
    {
        // Accessing DX1 device
        // Called by DX1Utility.Constructor
        if( DX1Utility.Debug ) log("Device.registerWindowForEvents("+ this.formHandle +")");

        // And register for the actual event
        DEV_BROADCAST_DEVICEHANDLE devHandle    = new DEV_BROADCAST_DEVICEHANDLE();
        devHandle.dbch_size                     = (uint)Marshal.SizeOf(devHandle);
        devHandle.dbch_devicetype               = 6;
        devHandle.dcbh_handle                   = dx1Handle;
        devHandle.dcbh_hdevnotify               = IntPtr.Zero;
        devHandle.dcbh_eventguid                = new Guid( DX1_GUID );

        IntPtr  not2  = NativeMethods.RegisterDeviceNotification(this.formHandle, ref devHandle, 0);
        return (not2 != IntPtr.Zero);
    }
    // }}}

#endregion
#region private methods {{{
    private void openDevice(bool synchronous) // {{{
    {
        if( DX1Utility.Debug ) log("Device.openDevice()");

        string path  = getDevicePath();
        if(    path == "") return;

        dx1Handle
            = NativeMethods.CreateFile( path
                , FileAccess.GenericRead | FileAccess.GenericWrite
                , FileShare.Write        | FileShare.Read
                , IntPtr.Zero
                , CreationDisposition.OpenExisting
                , ((synchronous
                        ? FileAttributes.Normal
                        : FileAttributes.Overlapped
                   ) |    FileAttributes.SecurityImpersonation
                  )
                , IntPtr.Zero
                );

    }
    // }}}
    private string getDevicePath() // {{{
    {
        if( DX1Utility.Debug ) log("Device.getDevicePath()");

        Guid deviceGuid   =
            new Guid( DX1_GUID );

        IntPtr DeviceInfo =
            NativeMethods.SetupDiGetClassDevs(ref deviceGuid, IntPtr.Zero, IntPtr.Zero, (DIGCF_PRESENT | DIGCF_DEVICEINTERFACE));

        if(DeviceInfo == (IntPtr)(-1)) {
            Console.WriteLine("SetupDiGetClassDevs Failed");
            return "";
        }

        SP_DEVICE_INTERFACE_DATA deviceInterfaceData = new SP_DEVICE_INTERFACE_DATA();
        deviceInterfaceData.cbSize = Marshal.SizeOf(deviceInterfaceData);

        bool result =
            NativeMethods.SetupDiEnumDeviceInterfaces(DeviceInfo, IntPtr.Zero, ref deviceGuid, 0, ref deviceInterfaceData);

        if( !result ) {
            Console.WriteLine("SetupDiEnumDeviceInterfaces Failed");
            NativeMethods.SetupDiDestroyDeviceInfoList( DeviceInfo );
            return "";
        }

        uint requiredSize = 0;
        NativeMethods.SetupDiGetDeviceInterfaceDetail(DeviceInfo
            , ref deviceInterfaceData
            , IntPtr.Zero
            , 0
            , out requiredSize
            , IntPtr.Zero);

        SP_DEVICE_INTERFACE_DETAIL_DATA didd = new SP_DEVICE_INTERFACE_DETAIL_DATA();

        if(requiredSize < Marshal.SizeOf(didd)) {
            didd.cbSize = 6;
            uint length = requiredSize;
            result              = NativeMethods.SetupDiGetDeviceInterfaceDetail(DeviceInfo, ref deviceInterfaceData, ref didd, length, out requiredSize, IntPtr.Zero);
        }

        Console.WriteLine("NameLength = "+ didd.DevicePath.Length);
        Console.WriteLine(      "Name = "+ didd.DevicePath);

        NativeMethods.SetupDiDestroyDeviceInfoList(DeviceInfo);
        if( !result )
            return "\\\\?\\usb#vid_"+DX1_VID+"&pid_"+DX1_PID+"&mi_01#6&b8aa88a&0&0001#{"+DX1_GUID+"}";
        //                                     "\\\\?\\usb#vid_1603&pid_0002&mi_01#6&b8aa88a&0&0001#{573e8c73-0cb4-4471-a1bf-fab26c31d384}"

        return didd.DevicePath;
    }
    // }}}
#endregion }}}
#region NativeMethods {{{
        internal static class NativeMethods
        {
#region [CreateFile] [DeviceIoControl] {{{

        [DllImport("Kernel32.dll", SetLastError = false, CharSet = CharSet.Auto)]
            public static extern bool DeviceIoControl(  IntPtr hDevice
                ,                                       UInt32 IoControlCode
                , [MarshalAs(UnmanagedType.AsAny)] [In] object InBuffer
                ,                                          int nInBufferSize
                ,                                  [In] IntPtr OutBuffer
                ,                                         uint nOutBufferSize
                ,                                     ref uint pBytesReturned
                ,                                       IntPtr Overlapped
                //, [In] ref System.Threading.NativeOverlapped Overlapped
                );

        [DllImport("Kernel32.dll", SetLastError = false, CharSet = CharSet.Auto)]
            public static extern bool DeviceIoControl(  IntPtr hDevice
                ,                                       UInt32 IoControlCode
                , [MarshalAs(UnmanagedType.AsAny)] [In] object InBuffer
                ,                                         uint nInBufferSize
                ,                                  [In] IntPtr OutBuffer
                ,                                         uint nOutBufferSize
                ,                                     ref uint pBytesReturned
                //,                                     IntPtr Overlapped
                ,        ref System.Threading.NativeOverlapped Overlapped
                );

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr CreateFile(              string fileName
                , [MarshalAs(UnmanagedType.U4)]              FileAccess fileAccess
                , [MarshalAs(UnmanagedType.U4)]               FileShare fileShare
                ,                                                IntPtr securityAttributes
                , [MarshalAs(UnmanagedType.U4)]     CreationDisposition creationDisposition
                ,                                        FileAttributes flags
                ,                                                IntPtr template
                );

#endregion }}}
    #region RegisterDeviceNotification DEV_BROADCAST[_DEVICEINTERFACE] [_DEVICEHANDLE] {{{

        [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr RegisterDeviceNotification( IntPtr hRecipient
                ,                        ref DEV_BROADCAST_DEVICEINTERFACE NotificationFilter
                ,                                                     uint Flags);

        [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr RegisterDeviceNotification( IntPtr hRecipient
            ,                               ref DEV_BROADCAST_DEVICEHANDLE NotificationFilter
            ,                                                         uint Flags);

    #endregion }}}
    #region SetupDi(-DestroyDeviceInfoList] [-GetClassDevs] [-EnumDeviceInterfaces] [-GetDeviceInterfaceDetail] {{{

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetupDiDestroyDeviceInfoList( IntPtr hDevInfo);


    [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        // 1st form using a ClassGUID
        public static extern IntPtr SetupDiGetClassDevs( ref Guid ClassGuid
            ,                                              IntPtr Enumerator
            ,                                              IntPtr hwndParent
            ,                                                 int Flags
            );

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiEnumDeviceInterfaces( IntPtr hDevInfo
            ,                                                     IntPtr devInfo
            ,                                                   ref Guid interfaceClassGuid
            ,                                                     UInt32 memberIndex
            ,                               ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData
            );

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail( IntPtr hDevInfo
            ,                                   ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData
            ,                                                         IntPtr deviceInterfaceDetailData
            ,                                                         UInt32 deviceInterfaceDetailDataSize
            ,                                                     out UInt32 requiredSize
            ,                                                         IntPtr deviceInfoData
            );

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail( IntPtr hDevInfo
            ,                                   ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData
            ,                            ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData
            ,                                                         UInt32 deviceInterfaceDetailDataSize
            ,                                                     out UInt32 requiredSize
            ,                                                         IntPtr deviceInfoData
            );

    #endregion }}}
        }
#endregion //}}}
        /* log {{{*/
        private void log(string message)
        {
            Logger.Log( message );
        }
        /*}}}*/
    }

}
/* DOC
:!start explorer "https://github.com/EarthwormO/ew-ergodex-dx1-driver/blob/master/HardwareInterface.cs"

*/
