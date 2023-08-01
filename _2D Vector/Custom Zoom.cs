#region Header
// https://github.com/ArdeshirV/2D-Vector
// Copyright 2002-2004 ArdeshirV, Licensed Under MIT


using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Applications.Vector
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmCustomZoom :
		System.Windows.Forms.Form
	{
		#region Variables

		private bool m_blnIsOK = false;
		private int m_intZoomValueTemp;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblValue;
		private System.Windows.Forms.TrackBar trkZoom;
		private System.Windows.Forms.Button btnCancel;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion
		//-------------------------------------------------------------------------------
		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public frmCustomZoom(int ZoomSize)
		{
			InitializeComponent();

			lblValue.Text = ZoomSize.ToString();
			btnOK.Click += new EventHandler(OnOk_Click);
			m_intZoomValueTemp = trkZoom.Value = ZoomSize;
			StartPosition = FormStartPosition.CenterParent;

#if !DEBUG
			//OpacityChangerInterval = 40;
#endif
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Properties
		
		/// <summary>
		/// Get or set user entered value.
		/// </summary>
		public int Value
		{
			get
			{
				return trkZoom.Value;
			}
		}

		/// <summary>
		/// Gets dialog mode.
		/// </summary>
		public bool IsChangedZoom
		{
			get
			{
				if(m_intZoomValueTemp != trkZoom.Value)
					return m_blnIsOK;
				else
					return false;
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Event Handlers

		/// <summary>
		/// Update Label text.
		/// </summary>
		/// <param name="sender">Scroll bar</param>
		/// <param name="e">Event Argument</param>
		private void trkZoom_Scroll(object sender, System.EventArgs e)
		{
			lblValue.Text = "%" + trkZoom.Value.ToString();
		}

		/// <summary>
		/// Occured whenever OK button has been clicked.
		/// </summary>
		/// <param name="sender">OK button</param>
		/// <param name="e">Event argument</param>
		private void OnOk_Click(object sender, EventArgs e)
		{
			m_blnIsOK = true;
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Overrided Functions

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
				if(components != null)
					components.Dispose();

			base.Dispose(disposing);
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmCustomZoom));
			this.lblValue = new System.Windows.Forms.Label();
			this.trkZoom = new System.Windows.Forms.TrackBar();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.trkZoom)).BeginInit();
			this.SuspendLayout();
			// 
			// lblValue
			// 
			this.lblValue.AccessibleDescription = ((string)(resources.GetObject("lblValue.AccessibleDescription")));
			this.lblValue.AccessibleName = ((string)(resources.GetObject("lblValue.AccessibleName")));
			this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblValue.Anchor")));
			this.lblValue.AutoSize = ((bool)(resources.GetObject("lblValue.AutoSize")));
			this.lblValue.BackColor = System.Drawing.Color.Transparent;
			this.lblValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblValue.Dock")));
			this.lblValue.Enabled = ((bool)(resources.GetObject("lblValue.Enabled")));
			this.lblValue.Font = ((System.Drawing.Font)(resources.GetObject("lblValue.Font")));
			this.lblValue.Image = ((System.Drawing.Image)(resources.GetObject("lblValue.Image")));
			this.lblValue.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblValue.ImageAlign")));
			this.lblValue.ImageIndex = ((int)(resources.GetObject("lblValue.ImageIndex")));
			this.lblValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblValue.ImeMode")));
			this.lblValue.Location = ((System.Drawing.Point)(resources.GetObject("lblValue.Location")));
			this.lblValue.Name = "lblValue";
			this.lblValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblValue.RightToLeft")));
			this.lblValue.Size = ((System.Drawing.Size)(resources.GetObject("lblValue.Size")));
			this.lblValue.TabIndex = ((int)(resources.GetObject("lblValue.TabIndex")));
			this.lblValue.Text = resources.GetString("lblValue.Text");
			this.lblValue.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblValue.TextAlign")));
			this.lblValue.Visible = ((bool)(resources.GetObject("lblValue.Visible")));
			// 
			// trkZoom
			// 
			this.trkZoom.AccessibleDescription = ((string)(resources.GetObject("trkZoom.AccessibleDescription")));
			this.trkZoom.AccessibleName = ((string)(resources.GetObject("trkZoom.AccessibleName")));
			this.trkZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("trkZoom.Anchor")));
			this.trkZoom.BackColor = System.Drawing.Color.Silver;
			this.trkZoom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("trkZoom.BackgroundImage")));
			this.trkZoom.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("trkZoom.Dock")));
			this.trkZoom.Enabled = ((bool)(resources.GetObject("trkZoom.Enabled")));
			this.trkZoom.Font = ((System.Drawing.Font)(resources.GetObject("trkZoom.Font")));
			this.trkZoom.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("trkZoom.ImeMode")));
			this.trkZoom.Location = ((System.Drawing.Point)(resources.GetObject("trkZoom.Location")));
			this.trkZoom.Maximum = 200;
			this.trkZoom.Minimum = 10;
			this.trkZoom.Name = "trkZoom";
			this.trkZoom.Orientation = ((System.Windows.Forms.Orientation)(resources.GetObject("trkZoom.Orientation")));
			this.trkZoom.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("trkZoom.RightToLeft")));
			this.trkZoom.Size = ((System.Drawing.Size)(resources.GetObject("trkZoom.Size")));
			this.trkZoom.TabIndex = ((int)(resources.GetObject("trkZoom.TabIndex")));
			this.trkZoom.Text = resources.GetString("trkZoom.Text");
			this.trkZoom.Value = 10;
			this.trkZoom.Visible = ((bool)(resources.GetObject("trkZoom.Visible")));
			this.trkZoom.Scroll += new System.EventHandler(this.trkZoom_Scroll);
			// 
			// btnOK
			// 
			this.btnOK.AccessibleDescription = ((string)(resources.GetObject("btnOK.AccessibleDescription")));
			this.btnOK.AccessibleName = ((string)(resources.GetObject("btnOK.AccessibleName")));
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOK.Anchor")));
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOK.Dock")));
			this.btnOK.Enabled = ((bool)(resources.GetObject("btnOK.Enabled")));
			this.btnOK.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnOK.FlatStyle")));
			this.btnOK.Font = ((System.Drawing.Font)(resources.GetObject("btnOK.Font")));
			this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
			this.btnOK.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK.ImageAlign")));
			this.btnOK.ImageIndex = ((int)(resources.GetObject("btnOK.ImageIndex")));
			this.btnOK.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOK.ImeMode")));
			this.btnOK.Location = ((System.Drawing.Point)(resources.GetObject("btnOK.Location")));
			this.btnOK.Name = "btnOK";
			this.btnOK.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOK.RightToLeft")));
			this.btnOK.Size = ((System.Drawing.Size)(resources.GetObject("btnOK.Size")));
			this.btnOK.TabIndex = ((int)(resources.GetObject("btnOK.TabIndex")));
			this.btnOK.Text = resources.GetString("btnOK.Text");
			this.btnOK.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK.TextAlign")));
			this.btnOK.Visible = ((bool)(resources.GetObject("btnOK.Visible")));
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleDescription = ((string)(resources.GetObject("btnCancel.AccessibleDescription")));
			this.btnCancel.AccessibleName = ((string)(resources.GetObject("btnCancel.AccessibleName")));
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel.Anchor")));
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCancel.Dock")));
			this.btnCancel.Enabled = ((bool)(resources.GetObject("btnCancel.Enabled")));
			this.btnCancel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCancel.FlatStyle")));
			this.btnCancel.Font = ((System.Drawing.Font)(resources.GetObject("btnCancel.Font")));
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.ImageAlign")));
			this.btnCancel.ImageIndex = ((int)(resources.GetObject("btnCancel.ImageIndex")));
			this.btnCancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCancel.ImeMode")));
			this.btnCancel.Location = ((System.Drawing.Point)(resources.GetObject("btnCancel.Location")));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCancel.RightToLeft")));
			this.btnCancel.Size = ((System.Drawing.Size)(resources.GetObject("btnCancel.Size")));
			this.btnCancel.TabIndex = ((int)(resources.GetObject("btnCancel.TabIndex")));
			this.btnCancel.Text = resources.GetString("btnCancel.Text");
			this.btnCancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.TextAlign")));
			this.btnCancel.Visible = ((bool)(resources.GetObject("btnCancel.Visible")));
			// 
			// frmCustomZoom
			// 
			this.AcceptButton = this.btnOK;
			this.AccessibleDescription = ((string)(resources.GetObject("$this.AccessibleDescription")));
			this.AccessibleName = ((string)(resources.GetObject("$this.AccessibleName")));
			this.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("$this.Anchor")));
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnCancel;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnCancel,
																		  this.btnOK,
																		  this.lblValue,
																		  this.trkZoom});
			this.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("$this.Dock")));
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "frmCustomZoom";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.ShowInTaskbar = false;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Visible = ((bool)(resources.GetObject("$this.Visible")));
			((System.ComponentModel.ISupportInitialize)(this.trkZoom)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
	}
}


