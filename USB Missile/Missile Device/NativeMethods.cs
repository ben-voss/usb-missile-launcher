using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace LittleNet.UsbMissile {
	internal class NativeMethods {
		public const int DIGCF_PRESENT = 0x2;
		public const int DIGCF_DEVICEINTERFACE = 0x10;
		public const int FILE_SHARE_READ = 0x1;
		public const int FILE_SHARE_WRITE = 0x2;
		public const int OPEN_EXISTING = 0x3;
		public const int GENERIC_WRITE = 0x40000000;
		public const short HidP_Input = 0;

		[DllImport("hid.dll")]
		public static extern void HidD_GetHidGuid(ref Guid hidGuid);

		[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetupDiGetClassDevs(ref Guid classGuid, String enumerator, IntPtr hwndParent, int flags);

		[DllImport("setupapi.dll")]
		public static extern bool SetupDiEnumDeviceInterfaces(IntPtr deviceInfoSet, int deviceInfoData, ref Guid interfaceClassGuid, int memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

		[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
		public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr deviceInfoSet, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, int DeviceInterfaceDetailDataSize, ref int RequiredSize, IntPtr DeviceInfoData);

		[DllImport("setupapi.dll")]
		public static extern int SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

		[StructLayout(LayoutKind.Sequential)]
		public struct SP_DEVICE_INTERFACE_DATA {
			public int Size;
			public Guid InterfaceClassGuid;
			public int Flags;
			public int Reserved;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SP_DEVICE_INTERFACE_DETAIL_DATA {
			public int Size;
			public String DevicePath;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct HIDD_ATTRIBUTES {
			public int Size;
			public short VendorID;
			public short ProductID;
			public short VersionNumber;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SECURITY_ATTRIBUTES {
			public int Length;
			public int SecurityDescriptor;
			public int InheritHandle;
		}

		[DllImport("hid.dll")]
		public static extern bool HidD_SetOutputReport(SafeFileHandle HidDeviceObject, byte[] lpReportBuffer, int ReportBufferLength);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern SafeFileHandle CreateFile(String lpFileName, int DesiredAccess, int dwShareMode, ref SECURITY_ATTRIBUTES SecurityAttributes, int CreationDisposition, int FlagsAndAttributes, int TemplateFile);

		[DllImport("hid.dll")]
		public static extern bool HidD_GetAttributes(SafeFileHandle hidDeviceObject, ref HIDD_ATTRIBUTES Attributes);

		[DllImport("kernel32.dll")]
		public static extern bool WriteFile(SafeFileHandle hFile, byte[] lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, int lpOverlapped);

		[StructLayout(LayoutKind.Sequential)]
		public struct HIDP_CAPS {
			public short Usage;
			public short UsagePage;
			public short InputReportByteLength;
			public short OutputReportByteLength;
			public short FeatureReportByteLength;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
			public short[] Reserved;
			public short NumberLinkCollectionNodes;
			public short NumberInputButtonCaps;
			public short NumberInputValueCaps;
			public short NumberInputDataIndices;
			public short NumberOutputButtonCaps;
			public short NumberOutputValueCaps;
			public short NumberOutputDataIndices;
			public short NumberFeatureButtonCaps;
			public short NumberFeatureValueCaps;
			public short NumberFeatureDataIndices;
		}

		[DllImport("hid.dll")]
		public static extern bool HidD_FreePreparsedData(ref IntPtr PreparsedData);

		[DllImport("hid.dll")]
		public static extern bool HidD_GetPreparsedData(SafeFileHandle HidDeviceObject, ref IntPtr PreparsedData);

		[DllImport("hid.dll")]
		public static extern int HidP_GetCaps(IntPtr PreparsedData, ref HIDP_CAPS Capabilities);

		//[DllImport("hid.dll")]
		//public static extern int HidP_GetValueCaps(short ReportType, ref byte ValueCaps, ref short ValueCapsLength, IntPtr PreparsedData);
	}
}
