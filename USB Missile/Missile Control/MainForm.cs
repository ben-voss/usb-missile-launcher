using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LittleNet.UsbMissile {
	public partial class MainForm : Form {

		#region Fields

		/// <summary>
		/// The missile device
		/// </summary>
		private MissileDevice _device;

		#endregion

		#region Constructors

		/// <summary>
		/// Initialises a new instance of the form
		/// </summary>
		public MainForm() {
			InitializeComponent();
		}

		#endregion

		private void MainForm_Load(object sender, EventArgs e) {
			_device = new MissileDevice();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
			if (_device != null) {
				_device.Dispose();
				_device = null;
			}
		}

		private void LeftButtonMouseDown(object sender, MouseEventArgs e) {
			_device.Command(DeviceCommand.Left);
		}

		private void ButtonMouseUp(object sender, MouseEventArgs e) {
			_device.Command(DeviceCommand.Stop);
		}

		private void UpButtonMouseDown(object sender, MouseEventArgs e) {
			_device.Command(DeviceCommand.Up);
		}

		private void RightButtonMouseDown(object sender, MouseEventArgs e) {
			_device.Command(DeviceCommand.Right);
		}

		private void DownButtonMouseDown(object sender, MouseEventArgs e) {
			_device.Command(DeviceCommand.Down);
		}

		private void _fireButton_Click(object sender, EventArgs e) {
			_device.Command(DeviceCommand.Fire);
		}
	}
}