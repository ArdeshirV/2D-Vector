#region Header
// https://github.com/ArdeshirV/2D-Vector
// Copyright 2002-2004 ArdeshirV, Licensed Under MIT


using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using ArdeshirV.Applications.Vector;
using ArdeshirV.Components.ScreenVector;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Applications.Vector
{
	/// <summary>
	/// Dialog form for to show vector properties.
	/// </summary>
	public class frmProperty :
		System.Windows.Forms.Form
	{
		#region variables

		private vector m_vct;
		private bool m_blnIsSucced;
		private readonly int m_intH;
		private readonly int m_intW;
		private System.Windows.Forms.Label lblI;
		private System.Windows.Forms.Label lblJ;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblLen;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblEndPoint;
		private System.Windows.Forms.Label lblStartPoint;
		private System.Windows.Forms.Label lblStartPointX;
		private System.Windows.Forms.Label lblStartPointY;
		private ArdeshirV.Components.NumericTextBox ntbEndX;
		private ArdeshirV.Components.NumericTextBox ntbEndY;
		private ArdeshirV.Components.NumericTextBox ntbStartX;
		private ArdeshirV.Components.NumericTextBox ntbStartY;

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
		/// <param name="vct">Selecteed vector</param>
		/// <param name="H">Screen vector height</param>
		public frmProperty(vector vct, int H, int W)
		{
			InitializeComponent();

			m_intH = H;
			m_intW = W;
			m_vct = vct;
			m_blnIsSucced = false;
			lblID.Text = "ID: " + vct.Name;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Vector ID: " + vct.Name + " Properties";
			ntbEndX.Value = vector.Tester(vct.EndPoint.X);
			ntbEndY.Value = vector.Tester(-vct.EndPoint.Y, H);
			ntbStartX.Value = vector.Tester(vct.StartPoint.X);
			ntbStartY.Value = vector.Tester(-vct.StartPoint.Y, H);
			LenUpdate();

#if !DEBUG
//			//OpacityChangerInterval = 40;
#endif
		} 

		#endregion
		//-------------------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Get or set Result of property changing.
		/// </summary>
		public vector Result
		{
			get
			{
				return m_vct;
			}
			set
			{
				m_vct = value;
			}
		}

		/// <summary>
		/// Get changing state.
		/// </summary>
		public bool Succed
		{
			get
			{
				return m_blnIsSucced;
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Event Handlers

		/// <summary>
		/// Occurs whenever up down control has been changed.
		/// </summary>
		/// <param name="sender">Numeric up down</param>
		/// <param name="e">Event argument</param>
		private void NumericTextBox_ValueChanged(object sender, System.EventArgs e)
		{
			try
			{
				LenUpdate();
			}
			catch
			{
			}

		}

		/// <summary>
		/// Occurs whenever OK button has been clicked.
		/// </summary>
		/// <param name="sender">OK button</param>
		/// <param name="e">Event argument</param>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(ntbStartX.Value == ntbEndX.Value &&
			   ntbStartY.Value == ntbEndY.Value)
			{
				DialogResult = DialogResult.Cancel;
				return;
			}

			m_vct = new vector(
				new PointD(vector.AntiTester(ntbStartX.Value, m_intH),
				vector.AntiTester(-ntbStartY.Value, m_intH)),
				new PointD(vector.AntiTester(ntbEndX.Value, m_intH),
				vector.AntiTester(-ntbEndY.Value, m_intH)));
			DialogResult = DialogResult.OK;
			m_blnIsSucced = true;
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Utility Function

		/// <summary>
		/// Length label update.
		/// </summary>
		private void LenUpdate()
		{
			vector l_vct = new vector(
				new PointD(ntbStartX.Value, -ntbStartY.Value),
				new PointD(ntbEndX.Value, -ntbEndY.Value));
			lblLen.Text = "Length: " + l_vct.Lenght;
			lblI.Text = "I: " + (ntbEndX.Value - ntbStartX.Value).ToString();
			lblJ.Text = "J: " + (ntbEndY.Value - ntbStartY.Value).ToString();
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Overrided function

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmProperty));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblEndPoint = new System.Windows.Forms.Label();
			this.lblStartPointX = new System.Windows.Forms.Label();
			this.lblStartPointY = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblLen = new System.Windows.Forms.Label();
			this.lblID = new System.Windows.Forms.Label();
			this.lblStartPoint = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblI = new System.Windows.Forms.Label();
			this.lblJ = new System.Windows.Forms.Label();
			this.ntbStartX = new ArdeshirV.Components.NumericTextBox();
			this.ntbStartY = new ArdeshirV.Components.NumericTextBox();
			this.ntbEndX = new ArdeshirV.Components.NumericTextBox();
			this.ntbEndY = new ArdeshirV.Components.NumericTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AccessibleDescription = ((string)(resources.GetObject("label1.AccessibleDescription")));
			this.label1.AccessibleName = ((string)(resources.GetObject("label1.AccessibleName")));
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = ((string)(resources.GetObject("label2.AccessibleDescription")));
			this.label2.AccessibleName = ((string)(resources.GetObject("label2.AccessibleName")));
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// lblEndPoint
			// 
			this.lblEndPoint.AccessibleDescription = ((string)(resources.GetObject("lblEndPoint.AccessibleDescription")));
			this.lblEndPoint.AccessibleName = ((string)(resources.GetObject("lblEndPoint.AccessibleName")));
			this.lblEndPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblEndPoint.Anchor")));
			this.lblEndPoint.AutoSize = ((bool)(resources.GetObject("lblEndPoint.AutoSize")));
			this.lblEndPoint.BackColor = System.Drawing.Color.Transparent;
			this.lblEndPoint.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblEndPoint.Dock")));
			this.lblEndPoint.Enabled = ((bool)(resources.GetObject("lblEndPoint.Enabled")));
			this.lblEndPoint.Font = ((System.Drawing.Font)(resources.GetObject("lblEndPoint.Font")));
			this.lblEndPoint.Image = ((System.Drawing.Image)(resources.GetObject("lblEndPoint.Image")));
			this.lblEndPoint.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEndPoint.ImageAlign")));
			this.lblEndPoint.ImageIndex = ((int)(resources.GetObject("lblEndPoint.ImageIndex")));
			this.lblEndPoint.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblEndPoint.ImeMode")));
			this.lblEndPoint.Location = ((System.Drawing.Point)(resources.GetObject("lblEndPoint.Location")));
			this.lblEndPoint.Name = "lblEndPoint";
			this.lblEndPoint.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblEndPoint.RightToLeft")));
			this.lblEndPoint.Size = ((System.Drawing.Size)(resources.GetObject("lblEndPoint.Size")));
			this.lblEndPoint.TabIndex = ((int)(resources.GetObject("lblEndPoint.TabIndex")));
			this.lblEndPoint.Text = resources.GetString("lblEndPoint.Text");
			this.lblEndPoint.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEndPoint.TextAlign")));
			this.lblEndPoint.Visible = ((bool)(resources.GetObject("lblEndPoint.Visible")));
			// 
			// lblStartPointX
			// 
			this.lblStartPointX.AccessibleDescription = ((string)(resources.GetObject("lblStartPointX.AccessibleDescription")));
			this.lblStartPointX.AccessibleName = ((string)(resources.GetObject("lblStartPointX.AccessibleName")));
			this.lblStartPointX.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblStartPointX.Anchor")));
			this.lblStartPointX.AutoSize = ((bool)(resources.GetObject("lblStartPointX.AutoSize")));
			this.lblStartPointX.BackColor = System.Drawing.Color.Transparent;
			this.lblStartPointX.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblStartPointX.Dock")));
			this.lblStartPointX.Enabled = ((bool)(resources.GetObject("lblStartPointX.Enabled")));
			this.lblStartPointX.Font = ((System.Drawing.Font)(resources.GetObject("lblStartPointX.Font")));
			this.lblStartPointX.Image = ((System.Drawing.Image)(resources.GetObject("lblStartPointX.Image")));
			this.lblStartPointX.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStartPointX.ImageAlign")));
			this.lblStartPointX.ImageIndex = ((int)(resources.GetObject("lblStartPointX.ImageIndex")));
			this.lblStartPointX.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblStartPointX.ImeMode")));
			this.lblStartPointX.Location = ((System.Drawing.Point)(resources.GetObject("lblStartPointX.Location")));
			this.lblStartPointX.Name = "lblStartPointX";
			this.lblStartPointX.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblStartPointX.RightToLeft")));
			this.lblStartPointX.Size = ((System.Drawing.Size)(resources.GetObject("lblStartPointX.Size")));
			this.lblStartPointX.TabIndex = ((int)(resources.GetObject("lblStartPointX.TabIndex")));
			this.lblStartPointX.Text = resources.GetString("lblStartPointX.Text");
			this.lblStartPointX.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStartPointX.TextAlign")));
			this.lblStartPointX.Visible = ((bool)(resources.GetObject("lblStartPointX.Visible")));
			// 
			// lblStartPointY
			// 
			this.lblStartPointY.AccessibleDescription = ((string)(resources.GetObject("lblStartPointY.AccessibleDescription")));
			this.lblStartPointY.AccessibleName = ((string)(resources.GetObject("lblStartPointY.AccessibleName")));
			this.lblStartPointY.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblStartPointY.Anchor")));
			this.lblStartPointY.AutoSize = ((bool)(resources.GetObject("lblStartPointY.AutoSize")));
			this.lblStartPointY.BackColor = System.Drawing.Color.Transparent;
			this.lblStartPointY.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblStartPointY.Dock")));
			this.lblStartPointY.Enabled = ((bool)(resources.GetObject("lblStartPointY.Enabled")));
			this.lblStartPointY.Font = ((System.Drawing.Font)(resources.GetObject("lblStartPointY.Font")));
			this.lblStartPointY.Image = ((System.Drawing.Image)(resources.GetObject("lblStartPointY.Image")));
			this.lblStartPointY.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStartPointY.ImageAlign")));
			this.lblStartPointY.ImageIndex = ((int)(resources.GetObject("lblStartPointY.ImageIndex")));
			this.lblStartPointY.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblStartPointY.ImeMode")));
			this.lblStartPointY.Location = ((System.Drawing.Point)(resources.GetObject("lblStartPointY.Location")));
			this.lblStartPointY.Name = "lblStartPointY";
			this.lblStartPointY.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblStartPointY.RightToLeft")));
			this.lblStartPointY.Size = ((System.Drawing.Size)(resources.GetObject("lblStartPointY.Size")));
			this.lblStartPointY.TabIndex = ((int)(resources.GetObject("lblStartPointY.TabIndex")));
			this.lblStartPointY.Text = resources.GetString("lblStartPointY.Text");
			this.lblStartPointY.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStartPointY.TextAlign")));
			this.lblStartPointY.Visible = ((bool)(resources.GetObject("lblStartPointY.Visible")));
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
			// lblLen
			// 
			this.lblLen.AccessibleDescription = ((string)(resources.GetObject("lblLen.AccessibleDescription")));
			this.lblLen.AccessibleName = ((string)(resources.GetObject("lblLen.AccessibleName")));
			this.lblLen.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblLen.Anchor")));
			this.lblLen.AutoSize = ((bool)(resources.GetObject("lblLen.AutoSize")));
			this.lblLen.BackColor = System.Drawing.Color.Silver;
			this.lblLen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLen.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblLen.Dock")));
			this.lblLen.Enabled = ((bool)(resources.GetObject("lblLen.Enabled")));
			this.lblLen.Font = ((System.Drawing.Font)(resources.GetObject("lblLen.Font")));
			this.lblLen.Image = ((System.Drawing.Image)(resources.GetObject("lblLen.Image")));
			this.lblLen.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLen.ImageAlign")));
			this.lblLen.ImageIndex = ((int)(resources.GetObject("lblLen.ImageIndex")));
			this.lblLen.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblLen.ImeMode")));
			this.lblLen.Location = ((System.Drawing.Point)(resources.GetObject("lblLen.Location")));
			this.lblLen.Name = "lblLen";
			this.lblLen.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblLen.RightToLeft")));
			this.lblLen.Size = ((System.Drawing.Size)(resources.GetObject("lblLen.Size")));
			this.lblLen.TabIndex = ((int)(resources.GetObject("lblLen.TabIndex")));
			this.lblLen.Text = resources.GetString("lblLen.Text");
			this.lblLen.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLen.TextAlign")));
			this.lblLen.Visible = ((bool)(resources.GetObject("lblLen.Visible")));
			// 
			// lblID
			// 
			this.lblID.AccessibleDescription = ((string)(resources.GetObject("lblID.AccessibleDescription")));
			this.lblID.AccessibleName = ((string)(resources.GetObject("lblID.AccessibleName")));
			this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblID.Anchor")));
			this.lblID.AutoSize = ((bool)(resources.GetObject("lblID.AutoSize")));
			this.lblID.BackColor = System.Drawing.Color.Transparent;
			this.lblID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblID.Dock")));
			this.lblID.Enabled = ((bool)(resources.GetObject("lblID.Enabled")));
			this.lblID.Font = ((System.Drawing.Font)(resources.GetObject("lblID.Font")));
			this.lblID.Image = ((System.Drawing.Image)(resources.GetObject("lblID.Image")));
			this.lblID.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblID.ImageAlign")));
			this.lblID.ImageIndex = ((int)(resources.GetObject("lblID.ImageIndex")));
			this.lblID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblID.ImeMode")));
			this.lblID.Location = ((System.Drawing.Point)(resources.GetObject("lblID.Location")));
			this.lblID.Name = "lblID";
			this.lblID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblID.RightToLeft")));
			this.lblID.Size = ((System.Drawing.Size)(resources.GetObject("lblID.Size")));
			this.lblID.TabIndex = ((int)(resources.GetObject("lblID.TabIndex")));
			this.lblID.Text = resources.GetString("lblID.Text");
			this.lblID.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblID.TextAlign")));
			this.lblID.Visible = ((bool)(resources.GetObject("lblID.Visible")));
			// 
			// lblStartPoint
			// 
			this.lblStartPoint.AccessibleDescription = ((string)(resources.GetObject("lblStartPoint.AccessibleDescription")));
			this.lblStartPoint.AccessibleName = ((string)(resources.GetObject("lblStartPoint.AccessibleName")));
			this.lblStartPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblStartPoint.Anchor")));
			this.lblStartPoint.AutoSize = ((bool)(resources.GetObject("lblStartPoint.AutoSize")));
			this.lblStartPoint.BackColor = System.Drawing.Color.Transparent;
			this.lblStartPoint.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblStartPoint.Dock")));
			this.lblStartPoint.Enabled = ((bool)(resources.GetObject("lblStartPoint.Enabled")));
			this.lblStartPoint.Font = ((System.Drawing.Font)(resources.GetObject("lblStartPoint.Font")));
			this.lblStartPoint.Image = ((System.Drawing.Image)(resources.GetObject("lblStartPoint.Image")));
			this.lblStartPoint.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStartPoint.ImageAlign")));
			this.lblStartPoint.ImageIndex = ((int)(resources.GetObject("lblStartPoint.ImageIndex")));
			this.lblStartPoint.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblStartPoint.ImeMode")));
			this.lblStartPoint.Location = ((System.Drawing.Point)(resources.GetObject("lblStartPoint.Location")));
			this.lblStartPoint.Name = "lblStartPoint";
			this.lblStartPoint.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblStartPoint.RightToLeft")));
			this.lblStartPoint.Size = ((System.Drawing.Size)(resources.GetObject("lblStartPoint.Size")));
			this.lblStartPoint.TabIndex = ((int)(resources.GetObject("lblStartPoint.TabIndex")));
			this.lblStartPoint.Text = resources.GetString("lblStartPoint.Text");
			this.lblStartPoint.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStartPoint.TextAlign")));
			this.lblStartPoint.Visible = ((bool)(resources.GetObject("lblStartPoint.Visible")));
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
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblI
			// 
			this.lblI.AccessibleDescription = ((string)(resources.GetObject("lblI.AccessibleDescription")));
			this.lblI.AccessibleName = ((string)(resources.GetObject("lblI.AccessibleName")));
			this.lblI.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblI.Anchor")));
			this.lblI.AutoSize = ((bool)(resources.GetObject("lblI.AutoSize")));
			this.lblI.BackColor = System.Drawing.Color.Silver;
			this.lblI.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblI.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblI.Dock")));
			this.lblI.Enabled = ((bool)(resources.GetObject("lblI.Enabled")));
			this.lblI.Font = ((System.Drawing.Font)(resources.GetObject("lblI.Font")));
			this.lblI.Image = ((System.Drawing.Image)(resources.GetObject("lblI.Image")));
			this.lblI.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblI.ImageAlign")));
			this.lblI.ImageIndex = ((int)(resources.GetObject("lblI.ImageIndex")));
			this.lblI.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblI.ImeMode")));
			this.lblI.Location = ((System.Drawing.Point)(resources.GetObject("lblI.Location")));
			this.lblI.Name = "lblI";
			this.lblI.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblI.RightToLeft")));
			this.lblI.Size = ((System.Drawing.Size)(resources.GetObject("lblI.Size")));
			this.lblI.TabIndex = ((int)(resources.GetObject("lblI.TabIndex")));
			this.lblI.Text = resources.GetString("lblI.Text");
			this.lblI.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblI.TextAlign")));
			this.lblI.Visible = ((bool)(resources.GetObject("lblI.Visible")));
			// 
			// lblJ
			// 
			this.lblJ.AccessibleDescription = ((string)(resources.GetObject("lblJ.AccessibleDescription")));
			this.lblJ.AccessibleName = ((string)(resources.GetObject("lblJ.AccessibleName")));
			this.lblJ.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblJ.Anchor")));
			this.lblJ.AutoSize = ((bool)(resources.GetObject("lblJ.AutoSize")));
			this.lblJ.BackColor = System.Drawing.Color.Silver;
			this.lblJ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblJ.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblJ.Dock")));
			this.lblJ.Enabled = ((bool)(resources.GetObject("lblJ.Enabled")));
			this.lblJ.Font = ((System.Drawing.Font)(resources.GetObject("lblJ.Font")));
			this.lblJ.Image = ((System.Drawing.Image)(resources.GetObject("lblJ.Image")));
			this.lblJ.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblJ.ImageAlign")));
			this.lblJ.ImageIndex = ((int)(resources.GetObject("lblJ.ImageIndex")));
			this.lblJ.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblJ.ImeMode")));
			this.lblJ.Location = ((System.Drawing.Point)(resources.GetObject("lblJ.Location")));
			this.lblJ.Name = "lblJ";
			this.lblJ.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblJ.RightToLeft")));
			this.lblJ.Size = ((System.Drawing.Size)(resources.GetObject("lblJ.Size")));
			this.lblJ.TabIndex = ((int)(resources.GetObject("lblJ.TabIndex")));
			this.lblJ.Text = resources.GetString("lblJ.Text");
			this.lblJ.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblJ.TextAlign")));
			this.lblJ.Visible = ((bool)(resources.GetObject("lblJ.Visible")));
			// 
			// ntbStartX
			// 
			this.ntbStartX.AccessibleDescription = ((string)(resources.GetObject("ntbStartX.AccessibleDescription")));
			this.ntbStartX.AccessibleName = ((string)(resources.GetObject("ntbStartX.AccessibleName")));
			this.ntbStartX.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ntbStartX.Anchor")));
			this.ntbStartX.AutoSize = ((bool)(resources.GetObject("ntbStartX.AutoSize")));
			this.ntbStartX.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ntbStartX.BackgroundImage")));
			this.ntbStartX.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ntbStartX.Dock")));
			this.ntbStartX.Enabled = ((bool)(resources.GetObject("ntbStartX.Enabled")));
			this.ntbStartX.Font = ((System.Drawing.Font)(resources.GetObject("ntbStartX.Font")));
			this.ntbStartX.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ntbStartX.ImeMode")));
			this.ntbStartX.Location = ((System.Drawing.Point)(resources.GetObject("ntbStartX.Location")));
			this.ntbStartX.Maximum = 0;
			this.ntbStartX.MaxLength = ((int)(resources.GetObject("ntbStartX.MaxLength")));
			this.ntbStartX.Minimum = 0;
			this.ntbStartX.Multiline = ((bool)(resources.GetObject("ntbStartX.Multiline")));
			this.ntbStartX.Name = "ntbStartX";
			this.ntbStartX.PasswordChar = ((char)(resources.GetObject("ntbStartX.PasswordChar")));
			this.ntbStartX.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ntbStartX.RightToLeft")));
			this.ntbStartX.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ntbStartX.ScrollBars")));
			this.ntbStartX.Size = ((System.Drawing.Size)(resources.GetObject("ntbStartX.Size")));
			this.ntbStartX.TabIndex = ((int)(resources.GetObject("ntbStartX.TabIndex")));
			this.ntbStartX.Text = resources.GetString("ntbStartX.Text");
			this.ntbStartX.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ntbStartX.TextAlign")));
			this.ntbStartX.Value = 0;
			this.ntbStartX.Visible = ((bool)(resources.GetObject("ntbStartX.Visible")));
			this.ntbStartX.WordWrap = ((bool)(resources.GetObject("ntbStartX.WordWrap")));
			this.ntbStartX.TextChanged += new System.EventHandler(this.NumericTextBox_ValueChanged);
			// 
			// ntbStartY
			// 
			this.ntbStartY.AccessibleDescription = ((string)(resources.GetObject("ntbStartY.AccessibleDescription")));
			this.ntbStartY.AccessibleName = ((string)(resources.GetObject("ntbStartY.AccessibleName")));
			this.ntbStartY.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ntbStartY.Anchor")));
			this.ntbStartY.AutoSize = ((bool)(resources.GetObject("ntbStartY.AutoSize")));
			this.ntbStartY.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ntbStartY.BackgroundImage")));
			this.ntbStartY.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ntbStartY.Dock")));
			this.ntbStartY.Enabled = ((bool)(resources.GetObject("ntbStartY.Enabled")));
			this.ntbStartY.Font = ((System.Drawing.Font)(resources.GetObject("ntbStartY.Font")));
			this.ntbStartY.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ntbStartY.ImeMode")));
			this.ntbStartY.Location = ((System.Drawing.Point)(resources.GetObject("ntbStartY.Location")));
			this.ntbStartY.Maximum = 0;
			this.ntbStartY.MaxLength = ((int)(resources.GetObject("ntbStartY.MaxLength")));
			this.ntbStartY.Minimum = 0;
			this.ntbStartY.Multiline = ((bool)(resources.GetObject("ntbStartY.Multiline")));
			this.ntbStartY.Name = "ntbStartY";
			this.ntbStartY.PasswordChar = ((char)(resources.GetObject("ntbStartY.PasswordChar")));
			this.ntbStartY.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ntbStartY.RightToLeft")));
			this.ntbStartY.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ntbStartY.ScrollBars")));
			this.ntbStartY.Size = ((System.Drawing.Size)(resources.GetObject("ntbStartY.Size")));
			this.ntbStartY.TabIndex = ((int)(resources.GetObject("ntbStartY.TabIndex")));
			this.ntbStartY.Text = resources.GetString("ntbStartY.Text");
			this.ntbStartY.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ntbStartY.TextAlign")));
			this.ntbStartY.Value = 0;
			this.ntbStartY.Visible = ((bool)(resources.GetObject("ntbStartY.Visible")));
			this.ntbStartY.WordWrap = ((bool)(resources.GetObject("ntbStartY.WordWrap")));
			this.ntbStartY.TextChanged += new System.EventHandler(this.NumericTextBox_ValueChanged);
			// 
			// ntbEndX
			// 
			this.ntbEndX.AccessibleDescription = ((string)(resources.GetObject("ntbEndX.AccessibleDescription")));
			this.ntbEndX.AccessibleName = ((string)(resources.GetObject("ntbEndX.AccessibleName")));
			this.ntbEndX.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ntbEndX.Anchor")));
			this.ntbEndX.AutoSize = ((bool)(resources.GetObject("ntbEndX.AutoSize")));
			this.ntbEndX.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ntbEndX.BackgroundImage")));
			this.ntbEndX.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ntbEndX.Dock")));
			this.ntbEndX.Enabled = ((bool)(resources.GetObject("ntbEndX.Enabled")));
			this.ntbEndX.Font = ((System.Drawing.Font)(resources.GetObject("ntbEndX.Font")));
			this.ntbEndX.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ntbEndX.ImeMode")));
			this.ntbEndX.Location = ((System.Drawing.Point)(resources.GetObject("ntbEndX.Location")));
			this.ntbEndX.Maximum = 0;
			this.ntbEndX.MaxLength = ((int)(resources.GetObject("ntbEndX.MaxLength")));
			this.ntbEndX.Minimum = 0;
			this.ntbEndX.Multiline = ((bool)(resources.GetObject("ntbEndX.Multiline")));
			this.ntbEndX.Name = "ntbEndX";
			this.ntbEndX.PasswordChar = ((char)(resources.GetObject("ntbEndX.PasswordChar")));
			this.ntbEndX.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ntbEndX.RightToLeft")));
			this.ntbEndX.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ntbEndX.ScrollBars")));
			this.ntbEndX.Size = ((System.Drawing.Size)(resources.GetObject("ntbEndX.Size")));
			this.ntbEndX.TabIndex = ((int)(resources.GetObject("ntbEndX.TabIndex")));
			this.ntbEndX.Text = resources.GetString("ntbEndX.Text");
			this.ntbEndX.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ntbEndX.TextAlign")));
			this.ntbEndX.Value = 0;
			this.ntbEndX.Visible = ((bool)(resources.GetObject("ntbEndX.Visible")));
			this.ntbEndX.WordWrap = ((bool)(resources.GetObject("ntbEndX.WordWrap")));
			this.ntbEndX.TextChanged += new System.EventHandler(this.NumericTextBox_ValueChanged);
			// 
			// ntbEndY
			// 
			this.ntbEndY.AccessibleDescription = ((string)(resources.GetObject("ntbEndY.AccessibleDescription")));
			this.ntbEndY.AccessibleName = ((string)(resources.GetObject("ntbEndY.AccessibleName")));
			this.ntbEndY.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ntbEndY.Anchor")));
			this.ntbEndY.AutoSize = ((bool)(resources.GetObject("ntbEndY.AutoSize")));
			this.ntbEndY.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ntbEndY.BackgroundImage")));
			this.ntbEndY.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ntbEndY.Dock")));
			this.ntbEndY.Enabled = ((bool)(resources.GetObject("ntbEndY.Enabled")));
			this.ntbEndY.Font = ((System.Drawing.Font)(resources.GetObject("ntbEndY.Font")));
			this.ntbEndY.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ntbEndY.ImeMode")));
			this.ntbEndY.Location = ((System.Drawing.Point)(resources.GetObject("ntbEndY.Location")));
			this.ntbEndY.Maximum = 0;
			this.ntbEndY.MaxLength = ((int)(resources.GetObject("ntbEndY.MaxLength")));
			this.ntbEndY.Minimum = 0;
			this.ntbEndY.Multiline = ((bool)(resources.GetObject("ntbEndY.Multiline")));
			this.ntbEndY.Name = "ntbEndY";
			this.ntbEndY.PasswordChar = ((char)(resources.GetObject("ntbEndY.PasswordChar")));
			this.ntbEndY.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ntbEndY.RightToLeft")));
			this.ntbEndY.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ntbEndY.ScrollBars")));
			this.ntbEndY.Size = ((System.Drawing.Size)(resources.GetObject("ntbEndY.Size")));
			this.ntbEndY.TabIndex = ((int)(resources.GetObject("ntbEndY.TabIndex")));
			this.ntbEndY.Text = resources.GetString("ntbEndY.Text");
			this.ntbEndY.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ntbEndY.TextAlign")));
			this.ntbEndY.Value = 0;
			this.ntbEndY.Visible = ((bool)(resources.GetObject("ntbEndY.Visible")));
			this.ntbEndY.WordWrap = ((bool)(resources.GetObject("ntbEndY.WordWrap")));
			this.ntbEndY.TextChanged += new System.EventHandler(this.NumericTextBox_ValueChanged);
			// 
			// frmProperty
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
																		  this.ntbEndY,
																		  this.ntbEndX,
																		  this.ntbStartY,
																		  this.ntbStartX,
																		  this.lblJ,
																		  this.lblI,
																		  this.label1,
																		  this.label2,
																		  this.lblEndPoint,
																		  this.lblStartPointX,
																		  this.lblStartPointY,
																		  this.btnCancel,
																		  this.lblLen,
																		  this.lblID,
																		  this.lblStartPoint,
																		  this.btnOK});
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
			this.Name = "frmProperty";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.ShowInTaskbar = false;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Visible = ((bool)(resources.GetObject("$this.Visible")));
			this.TextChanged += new System.EventHandler(this.NumericTextBox_ValueChanged);
			this.ResumeLayout(false);

		}

		#endregion
	}
}


