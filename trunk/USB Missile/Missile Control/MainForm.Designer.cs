namespace LittleNet.UsbMissile {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this._leftButton = new System.Windows.Forms.Button();
			this._rightButton = new System.Windows.Forms.Button();
			this._upButton = new System.Windows.Forms.Button();
			this._downButton = new System.Windows.Forms.Button();
			this._fireButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _leftButton
			// 
			this._leftButton.Location = new System.Drawing.Point(17, 51);
			this._leftButton.Name = "_leftButton";
			this._leftButton.Size = new System.Drawing.Size(52, 23);
			this._leftButton.TabIndex = 0;
			this._leftButton.Text = "Left";
			this._leftButton.UseVisualStyleBackColor = true;
			this._leftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftButtonMouseDown);
			this._leftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
			// 
			// _rightButton
			// 
			this._rightButton.Location = new System.Drawing.Point(133, 51);
			this._rightButton.Name = "_rightButton";
			this._rightButton.Size = new System.Drawing.Size(52, 23);
			this._rightButton.TabIndex = 1;
			this._rightButton.Text = "Right";
			this._rightButton.UseVisualStyleBackColor = true;
			this._rightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightButtonMouseDown);
			this._rightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
			// 
			// _upButton
			// 
			this._upButton.Location = new System.Drawing.Point(75, 22);
			this._upButton.Name = "_upButton";
			this._upButton.Size = new System.Drawing.Size(52, 23);
			this._upButton.TabIndex = 2;
			this._upButton.Text = "Up";
			this._upButton.UseVisualStyleBackColor = true;
			this._upButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UpButtonMouseDown);
			this._upButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
			// 
			// _downButton
			// 
			this._downButton.Location = new System.Drawing.Point(75, 80);
			this._downButton.Name = "_downButton";
			this._downButton.Size = new System.Drawing.Size(52, 23);
			this._downButton.TabIndex = 3;
			this._downButton.Text = "Down";
			this._downButton.UseVisualStyleBackColor = true;
			this._downButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DownButtonMouseDown);
			this._downButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
			// 
			// _fireButton
			// 
			this._fireButton.Location = new System.Drawing.Point(75, 51);
			this._fireButton.Name = "_fireButton";
			this._fireButton.Size = new System.Drawing.Size(52, 23);
			this._fireButton.TabIndex = 4;
			this._fireButton.Text = "Fire";
			this._fireButton.UseVisualStyleBackColor = true;
			this._fireButton.Click += new System.EventHandler(this._fireButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(202, 125);
			this.Controls.Add(this._leftButton);
			this.Controls.Add(this._rightButton);
			this.Controls.Add(this._fireButton);
			this.Controls.Add(this._downButton);
			this.Controls.Add(this._upButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Missile Launcher";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button _leftButton;
		private System.Windows.Forms.Button _rightButton;
		private System.Windows.Forms.Button _upButton;
		private System.Windows.Forms.Button _downButton;
		private System.Windows.Forms.Button _fireButton;
	}
}

