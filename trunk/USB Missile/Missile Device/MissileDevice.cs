using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace LittleNet.UsbMissile {

	public class MissileDevice : IDisposable {

		#region Fields

		private static readonly byte[] SetupMessage1 = new byte[] { 0, (byte)'U', (byte)'S', (byte)'B', (byte)'C', 0, 0, 4, 0 };
		private static readonly byte[] SetupMessage2 = new byte[] { 0, (byte)'U', (byte)'S', (byte)'B', (byte)'C', 0, 0x40, 2, 0 };

		private static int VendorId = 0x1130;
		private static int ProductId = 0x0202;

		private SafeFileHandle _setupHandle;
		private SafeFileHandle _controlHandle;

		#endregion

		public MissileDevice() {
			Guid hidGuid = Guid.Empty;

			NativeMethods.HidD_GetHidGuid(ref hidGuid);

			List<String> devicePathNames = GetHIDDevices(hidGuid);

			OpenDevices(devicePathNames, VendorId, ProductId);
		}

		private void OpenDevices(List<String> devicePathNames, int vendorId, int productId) {
			NativeMethods.HIDD_ATTRIBUTES deviceAttributes = new NativeMethods.HIDD_ATTRIBUTES();
			deviceAttributes.Size = Marshal.SizeOf(typeof(NativeMethods.HIDD_ATTRIBUTES));

			NativeMethods.SECURITY_ATTRIBUTES security = new NativeMethods.SECURITY_ATTRIBUTES();
			security.SecurityDescriptor = 0;
			security.InheritHandle = 1;
			security.Length = Marshal.SizeOf(typeof(NativeMethods.SECURITY_ATTRIBUTES));

			for (int i = 0; i < devicePathNames.Count; i++) {
				SafeFileHandle handle = NativeMethods.CreateFile(devicePathNames[i], 0, NativeMethods.FILE_SHARE_READ | NativeMethods.FILE_SHARE_WRITE, ref security, NativeMethods.OPEN_EXISTING, 0, 0);

				if (!handle.IsInvalid) {
					bool success = NativeMethods.HidD_GetAttributes(handle, ref deviceAttributes);
					if (success) {
						if ((deviceAttributes.VendorID == vendorId) && (deviceAttributes.ProductID == productId)) {
							NativeMethods.HIDP_CAPS deviceCapabilities = GetDeviceCapabilities(handle);

							// Finally open the device
							if (deviceCapabilities.OutputReportByteLength == 65)
								_controlHandle = NativeMethods.CreateFile(devicePathNames[i], NativeMethods.GENERIC_WRITE, NativeMethods.FILE_SHARE_READ | NativeMethods.FILE_SHARE_WRITE, ref security, NativeMethods.OPEN_EXISTING, 0, 0);
							else
								_setupHandle = NativeMethods.CreateFile(devicePathNames[i], NativeMethods.GENERIC_WRITE, NativeMethods.FILE_SHARE_READ | NativeMethods.FILE_SHARE_WRITE, ref security, NativeMethods.OPEN_EXISTING, 0, 0);

						}
					}

					handle.Close();
				}
			}
		}

		private NativeMethods.HIDP_CAPS GetDeviceCapabilities(SafeFileHandle hidHandle) {
			NativeMethods.HIDP_CAPS capabilities = new NativeMethods.HIDP_CAPS();

			IntPtr preparsedDataPointer = new IntPtr();
			bool success = NativeMethods.HidD_GetPreparsedData(hidHandle, ref preparsedDataPointer);
			try {
				int result = NativeMethods.HidP_GetCaps(preparsedDataPointer, ref capabilities);
			} finally {
				NativeMethods.HidD_FreePreparsedData(ref preparsedDataPointer);
			}

			return capabilities;
		}

		private List<String> GetHIDDevices(Guid hidGuid) {
			IntPtr deviceInfoSet = NativeMethods.SetupDiGetClassDevs(ref hidGuid, null, IntPtr.Zero, NativeMethods.DIGCF_PRESENT | NativeMethods.DIGCF_DEVICEINTERFACE);
			try {
				int memberIndex = 0;

				int bufferSize = 0;
				List<String> devicePathNames = new List<string>();
				bool lastDevice = false;
				do {
					NativeMethods.SP_DEVICE_INTERFACE_DATA deviceInterfaceData = new NativeMethods.SP_DEVICE_INTERFACE_DATA();
					deviceInterfaceData.Size = Marshal.SizeOf(typeof(NativeMethods.SP_DEVICE_INTERFACE_DATA));

					bool success = NativeMethods.SetupDiEnumDeviceInterfaces(deviceInfoSet, 0, ref hidGuid, memberIndex, ref deviceInterfaceData);

					if (!success) {
						lastDevice = true;
					} else {
						success = NativeMethods.SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref deviceInterfaceData, IntPtr.Zero, 0, ref bufferSize, IntPtr.Zero);

						NativeMethods.SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData = new NativeMethods.SP_DEVICE_INTERFACE_DETAIL_DATA();
						deviceInterfaceDetailData.Size = Marshal.SizeOf(typeof(NativeMethods.SP_DEVICE_INTERFACE_DETAIL_DATA));

						IntPtr detailDataBuffer = Marshal.AllocHGlobal(bufferSize);
						try {
							Marshal.WriteInt32(detailDataBuffer, 4 + Marshal.SystemDefaultCharSize);

							success = NativeMethods.SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref deviceInterfaceData, detailDataBuffer, bufferSize, ref bufferSize, IntPtr.Zero);

							IntPtr pdevicePathName = new IntPtr(detailDataBuffer.ToInt32() + 4);

							devicePathNames.Add(Marshal.PtrToStringAuto(pdevicePathName));
						} finally {
							Marshal.FreeHGlobal(detailDataBuffer);
						}
					}

					memberIndex++;
				} while (!lastDevice);

				return devicePathNames;
			} finally {
				NativeMethods.SetupDiDestroyDeviceInfoList(deviceInfoSet);
			}
		}

		public void Command(DeviceCommand command) {
			if ((_setupHandle == null) || (_controlHandle == null))
				throw new ApplicationException("Unable to find a USB Missile Launcher device.");

			WriteBytes(_setupHandle, SetupMessage1);
			WriteBytes(_setupHandle, SetupMessage2);

			byte[] bytes = new byte[65];

			switch (command) {
				case DeviceCommand.Left:
				bytes[2] = 1;
				break;

				case DeviceCommand.Right:
				bytes[3] = 1;
				break;

				case DeviceCommand.Up:
				bytes[4] = 1;
				break;

				case DeviceCommand.Down:
				bytes[5] = 1;
				break;

				case DeviceCommand.Fire:
				bytes[6] = 1;
				break;
			}

			bytes[7] = 8;
			bytes[8] = 8;

			WriteBytes(_controlHandle, bytes);
		}

		private void WriteBytes(SafeFileHandle handle, byte[] bytes) {
			int numWritten = 0;
			if ((!NativeMethods.WriteFile(handle, bytes, bytes.Length, ref numWritten, 0)) || (numWritten != bytes.Length))
				throw new ApplicationException("Could not write data to device.");
		}


		#region IDisposable Members

		public void Dispose() {
			if (_controlHandle != null) {
				_controlHandle.Close();
				_controlHandle = null;
			}

			if (_setupHandle != null) {
				_setupHandle.Close();
				_setupHandle = null;
			}
		}

		#endregion
	}
}