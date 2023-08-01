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
//-------------------------------------------------------------------------------------
namespace ArdeshirV.Applications.Vector
{
	/// <summary>
	/// New Form Wizard for Create New Vector in Vector Project.
	/// </summary>
	public class frmNewVectorWizard :
		System.Windows.Forms.Form
	{
		#region Variables

		private readonly int r_intHeight;
		private vector m_pntResult;
		private System.Windows.Forms.Label lxlY;
		private System.Windows.Forms.Label lblI;
		private System.Windows.Forms.Label lblJ;
		private System.Windows.Forms.Label lblY2;
		private System.Windows.Forms.Label lblEnd;
		private System.Windows.Forms.Label lblStart;
		private OptionEventHandler m_dlgOption = null;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.RadioButton rdbSize;
		private System.Windows.Forms.RadioButton rdbPoint;
		private System.Windows.Forms.RadioButton rdbSizeAndMouse;
		private ArdeshirV.Components.NumericTextBox ptxI;
		private ArdeshirV.Components.NumericTextBox ptxJ;
		private ArdeshirV.Components.NumericTextBox ptxEndX;
		private ArdeshirV.Components.NumericTextBox ptxEndY;
		private ArdeshirV.Components.NumericTextBox ptxStartX;
		private ArdeshirV.Components.NumericTextBox ptxStartY;

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
		/// <param name="ParentHeight">Screen vector height</param>
		public frmNewVectorWizard(int intHeight)
		{
			InitializeComponent();

			r_intHeight = intHeight;
			StartPosition = FormStartPosition.CenterParent;

#if !DEBUG
			//OpacityChangerInterval = 40;
#endif
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Events

		/// <summary>
		/// Occurs whenever a check box & Create button has been clicked.
		/// </summary>
		public event OptionEventHandler OptionSelected
		{
			add
			{
				m_dlgOption += value;
			}
			remove
			{
				m_dlgOption -= value;
			}
		}
        
		#endregion
		//-------------------------------------------------------------------------------
		#region Event Handlers

		/// <summary>
		/// Occured whenever rdbPoint option has been selected.
		/// </summary>
		/// <param name="sender">Point check box</param>
		/// <param name="e">Event argument</param>
		private void rdbPoint_CheckedChanged(object sender, System.EventArgs e)
		{
			lxlY.Enabled = true;
			lblI.Enabled = false;
			lblJ.Enabled = false;
			ptxI.Enabled = false;
			ptxJ.Enabled = false;
			lblY2.Enabled = true;
			lblEnd.Enabled = true;
			ptxEndX.Enabled = true;
			ptxEndY.Enabled = true;
			lblStart.Enabled = true;
			ptxStartX.Enabled = true;
			ptxStartY.Enabled = true;
		}

		/// <summary>
		/// Occured whenever rdbSize option has been selected.
		/// </summary>
		/// <param name="sender">Size check box</param>
		/// <param name="e">Event argument</param>
		private void rdbSize_CheckedChanged(object sender, System.EventArgs e)
		{
			ptxI.Enabled = true;
			ptxJ.Enabled = true;
			lxlY.Enabled = true;
			lblI.Enabled = true;
			lblJ.Enabled = true;
			lblY2.Enabled = false;
			lblEnd.Enabled = false;
			lblStart.Enabled = true;
			ptxEndX.Enabled = false;
			ptxEndY.Enabled = false;
			ptxStartX.Enabled = true;
			ptxStartY.Enabled = true;
		}

		/// <summary>
		/// Occured whenever rdbSizeAndMouse option has been clicked.
		/// </summary>
		/// <param name="sender">Size & mouse check box</param>
		/// <param name="e">Event argument</param>
		private void rdbSizeAndMouse_CheckedChanged(object sender, System.EventArgs e)
		{
			lblI.Enabled = true;
			lblJ.Enabled = true;
			ptxI.Enabled = true;
			ptxJ.Enabled = true;
			lxlY.Enabled = false;
			lblY2.Enabled = false;
			lblEnd.Enabled = false;
			ptxEndX.Enabled = false;
			ptxEndY.Enabled = false;
			lblStart.Enabled = false;
			ptxStartX.Enabled = false;
			ptxStartY.Enabled = false;
		}

		/// <summary>
		/// Occured whenever Form has been laoded.
		/// </summary>
		/// <param name="sender">New vector wizard form</param>
		/// <param name="e">Event argument</param>
		private void frmNewVectorWizard_Load(object sender, System.EventArgs e)
		{
			lxlY.Enabled = false;
			lblI.Enabled = false;
			lblJ.Enabled = false;
			ptxI.Enabled = false;
			ptxJ.Enabled = false;
			lblEnd.Enabled = false;
			ptxEndX.Enabled = false;
			ptxEndY.Enabled = false;
			lblStart.Enabled = false;
			ptxStartX.Enabled = false;
			ptxStartY.Enabled = false;
		}		
		
		/// <summary>
		/// Occured whenever Create button has been clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			if(m_dlgOption == null)
				return;

			Close();

			PointD p1, p2;
			double	l_intJ = -ptxJ.Value,
					l_intEY = -ptxEndY.Value,
					l_intSY = -ptxStartY.Value;

			switch(WhichOption)
			{
				case 0:
					if((ptxStartX.Value == ptxEndX.Value &&
						l_intSY == l_intEY)||
						(ptxStartX.Value == 0 && ptxEndX.Value == 0 &&
						l_intSY == 0 && l_intEY == 0))
						return;
					p1 = new PointD(ptxStartX.Value * Global.s_intGeo,
						l_intSY * Global.s_intGeo);
					p2 = new PointD(ptxEndX.Value * Global.s_intGeo,
						l_intEY * Global.s_intGeo);
					m_pntResult = new vector(p1, p2);
					m_dlgOption(p1, p2, 0, 0, false);
					break;
				case 1:
					if(ptxI.Value == 0 && l_intJ == 0)
						return;
					p1 = new PointD(ptxStartX.Value * Global.s_intGeo,
						l_intSY * Global.s_intGeo);
					p2 = new PointD((((ptxStartX.Value + ptxI.Value)*
						Global.s_intGeo)), (l_intSY + l_intJ)* Global.s_intGeo);
					m_pntResult = new vector(p1, p2);
					m_dlgOption(p1, p2, 0, 0, false);
					break;
				case 2:
					if(ptxI.Value == 0 && l_intJ == 0)
						return;
					m_dlgOption(new PointD(0), new PointD(0),
						ptxI.Value * Global.s_intGeo,
						(l_intJ)* Global.s_intGeo, true);
					break;
#if DEBUG
				default:
					MessageBox.Show(this, "Unhadled Message");
					break;
#endif
			}
		}

		/// <summary>
		/// Occured whenever text of a Text Box has been changed.
		/// </summary>
		/// <param name="sender">Text box</param>
		/// <param name="e">Event argument</param>
		private void ForALlTextBoxes_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				Double.Parse(((TextBox)sender).Text);
			}
			catch(Exception exception)
			{
				if(exception.Message == "Input string was not in a correct format.")
				{
					((TextBox)sender).Focus();
					((TextBox)sender).Text = "";
#if DEBUG
					MessageBox.Show(this,"Enter numberical value.", "Warning",
						MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
				}
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Public Functions

		/// <summary>
		/// Get number of which option has been selected.
		/// </summary>
		public int WhichOption
		{
			get
			{
				if(rdbPoint.Checked)
					return 0;
				else if(rdbSize.Checked)
					return 1;
				else if(rdbSizeAndMouse.Checked)
					return 2;
				else
					return -1;
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Overrided functions

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmNewVectorWizard));
			this.rdbPoint = new System.Windows.Forms.RadioButton();
			this.rdbSize = new System.Windows.Forms.RadioButton();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.rdbSizeAndMouse = new System.Windows.Forms.RadioButton();
			this.lblY2 = new System.Windows.Forms.Label();
			this.lxlY = new System.Windows.Forms.Label();
			this.lblEnd = new System.Windows.Forms.Label();
			this.lblI = new System.Windows.Forms.Label();
			this.lblJ = new System.Windows.Forms.Label();
			this.lblStart = new System.Windows.Forms.Label();
			this.ptxStartX = new ArdeshirV.Components.NumericTextBox();
			this.ptxStartY = new ArdeshirV.Components.NumericTextBox();
			this.ptxEndX = new ArdeshirV.Components.NumericTextBox();
			this.ptxEndY = new ArdeshirV.Components.NumericTextBox();
			this.ptxI = new ArdeshirV.Components.NumericTextBox();
			this.ptxJ = new ArdeshirV.Components.NumericTextBox();
			this.SuspendLayout();
			// 
			// rdbPoint
			// 
			this.rdbPoint.AccessibleDescription = ((string)(resources.GetObject("rdbPoint.AccessibleDescription")));
			this.rdbPoint.AccessibleName = ((string)(resources.GetObject("rdbPoint.AccessibleName")));
			this.rdbPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rdbPoint.Anchor")));
			this.rdbPoint.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rdbPoint.Appearance")));
			this.rdbPoint.BackColor = System.Drawing.Color.Transparent;
			this.rdbPoint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rdbPoint.BackgroundImage")));
			this.rdbPoint.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rdbPoint.CheckAlign")));
			this.rdbPoint.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rdbPoint.Dock")));
			this.rdbPoint.Enabled = ((bool)(resources.GetObject("rdbPoint.Enabled")));
			this.rdbPoint.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rdbPoint.FlatStyle")));
			this.rdbPoint.Font = ((System.Drawing.Font)(resources.GetObject("rdbPoint.Font")));
			this.rdbPoint.Image = ((System.Drawing.Image)(resources.GetObject("rdbPoint.Image")));
			this.rdbPoint.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rdbPoint.ImageAlign")));
			this.rdbPoint.ImageIndex = ((int)(resources.GetObject("rdbPoint.ImageIndex")));
			this.rdbPoint.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rdbPoint.ImeMode")));
			this.rdbPoint.Location = ((System.Drawing.Point)(resources.GetObject("rdbPoint.Location")));
			this.rdbPoint.Name = "rdbPoint";
			this.rdbPoint.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rdbPoint.RightToLeft")));
			this.rdbPoint.Size = ((System.Drawing.Size)(resources.GetObject("rdbPoint.Size")));
			this.rdbPoint.TabIndex = ((int)(resources.GetObject("rdbPoint.TabIndex")));
			this.rdbPoint.Text = resources.GetString("rdbPoint.Text");
			this.rdbPoint.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rdbPoint.TextAlign")));
			this.rdbPoint.Visible = ((bool)(resources.GetObject("rdbPoint.Visible")));
			this.rdbPoint.CheckedChanged += new System.EventHandler(this.rdbPoint_CheckedChanged);
			// 
			// rdbSize
			// 
			this.rdbSize.AccessibleDescription = ((string)(resources.GetObject("rdbSize.AccessibleDescription")));
			this.rdbSize.AccessibleName = ((string)(resources.GetObject("rdbSize.AccessibleName")));
			this.rdbSize.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rdbSize.Anchor")));
			this.rdbSize.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rdbSize.Appearance")));
			this.rdbSize.BackColor = System.Drawing.Color.Transparent;
			this.rdbSize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rdbSize.BackgroundImage")));
			this.rdbSize.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rdbSize.CheckAlign")));
			this.rdbSize.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rdbSize.Dock")));
			this.rdbSize.Enabled = ((bool)(resources.GetObject("rdbSize.Enabled")));
			this.rdbSize.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rdbSize.FlatStyle")));
			this.rdbSize.Font = ((System.Drawing.Font)(resources.GetObject("rdbSize.Font")));
			this.rdbSize.Image = ((System.Drawing.Image)(resources.GetObject("rdbSize.Image")));
			this.rdbSize.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rdbSize.ImageAlign")));
			this.rdbSize.ImageIndex = ((int)(resources.GetObject("rdbSize.ImageIndex")));
			this.rdbSize.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rdbSize.ImeMode")));
			this.rdbSize.Location = ((System.Drawing.Point)(resources.GetObject("rdbSize.Location")));
			this.rdbSize.Name = "rdbSize";
			this.rdbSize.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rdbSize.RightToLeft")));
			this.rdbSize.Size = ((System.Drawing.Size)(resources.GetObject("rdbSize.Size")));
			this.rdbSize.TabIndex = ((int)(resources.GetObject("rdbSize.TabIndex")));
			this.rdbSize.Text = resources.GetString("rdbSize.Text");
			this.rdbSize.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rdbSize.TextAlign")));
			this.rdbSize.Visible = ((bool)(resources.GetObject("rdbSize.Visible")));
			this.rdbSize.CheckedChanged += new System.EventHandler(this.rdbSize_CheckedChanged);
			// 
			// btnCreate
			// 
			this.btnCreate.AccessibleDescription = ((string)(resources.GetObject("btnCreate.AccessibleDescription")));
			this.btnCreate.AccessibleName = ((string)(resources.GetObject("btnCreate.AccessibleName")));
			this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCreate.Anchor")));
			this.btnCreate.BackColor = System.Drawing.Color.Transparent;
			this.btnCreate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreate.BackgroundImage")));
			this.btnCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnCreate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCreate.Dock")));
			this.btnCreate.Enabled = ((bool)(resources.GetObject("btnCreate.Enabled")));
			this.btnCreate.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCreate.FlatStyle")));
			this.btnCreate.Font = ((System.Drawing.Font)(resources.GetObject("btnCreate.Font")));
			this.btnCreate.Image = ((System.Drawing.Image)(resources.GetObject("btnCreate.Image")));
			this.btnCreate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCreate.ImageAlign")));
			this.btnCreate.ImageIndex = ((int)(resources.GetObject("btnCreate.ImageIndex")));
			this.btnCreate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCreate.ImeMode")));
			this.btnCreate.Location = ((System.Drawing.Point)(resources.GetObject("btnCreate.Location")));
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCreate.RightToLeft")));
			this.btnCreate.Size = ((System.Drawing.Size)(resources.GetObject("btnCreate.Size")));
			this.btnCreate.TabIndex = ((int)(resources.GetObject("btnCreate.TabIndex")));
			this.btnCreate.Text = resources.GetString("btnCreate.Text");
			this.btnCreate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCreate.TextAlign")));
			this.btnCreate.Visible = ((bool)(resources.GetObject("btnCreate.Visible")));
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
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
			// rdbSizeAndMouse
			// 
			this.rdbSizeAndMouse.AccessibleDescription = ((string)(resources.GetObject("rdbSizeAndMouse.AccessibleDescription")));
			this.rdbSizeAndMouse.AccessibleName = ((string)(resources.GetObject("rdbSizeAndMouse.AccessibleName")));
			this.rdbSizeAndMouse.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rdbSizeAndMouse.Anchor")));
			this.rdbSizeAndMouse.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rdbSizeAndMouse.Appearance")));
			this.rdbSizeAndMouse.BackColor = System.Drawing.Color.Transparent;
			this.rdbSizeAndMouse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rdbSizeAndMouse.BackgroundImage")));
			this.rdbSizeAndMouse.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rdbSizeAndMouse.CheckAlign")));
			this.rdbSizeAndMouse.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rdbSizeAndMouse.Dock")));
			this.rdbSizeAndMouse.Enabled = ((bool)(resources.GetObject("rdbSizeAndMouse.Enabled")));
			this.rdbSizeAndMouse.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rdbSizeAndMouse.FlatStyle")));
			this.rdbSizeAndMouse.Font = ((System.Drawing.Font)(resources.GetObject("rdbSizeAndMouse.Font")));
			this.rdbSizeAndMouse.Image = ((System.Drawing.Image)(resources.GetObject("rdbSizeAndMouse.Image")));
			this.rdbSizeAndMouse.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rdbSizeAndMouse.ImageAlign")));
			this.rdbSizeAndMouse.ImageIndex = ((int)(resources.GetObject("rdbSizeAndMouse.ImageIndex")));
			this.rdbSizeAndMouse.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rdbSizeAndMouse.ImeMode")));
			this.rdbSizeAndMouse.Location = ((System.Drawing.Point)(resources.GetObject("rdbSizeAndMouse.Location")));
			this.rdbSizeAndMouse.Name = "rdbSizeAndMouse";
			this.rdbSizeAndMouse.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rdbSizeAndMouse.RightToLeft")));
			this.rdbSizeAndMouse.Size = ((System.Drawing.Size)(resources.GetObject("rdbSizeAndMouse.Size")));
			this.rdbSizeAndMouse.TabIndex = ((int)(resources.GetObject("rdbSizeAndMouse.TabIndex")));
			this.rdbSizeAndMouse.Text = resources.GetString("rdbSizeAndMouse.Text");
			this.rdbSizeAndMouse.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rdbSizeAndMouse.TextAlign")));
			this.rdbSizeAndMouse.Visible = ((bool)(resources.GetObject("rdbSizeAndMouse.Visible")));
			this.rdbSizeAndMouse.CheckedChanged += new System.EventHandler(this.rdbSizeAndMouse_CheckedChanged);
			// 
			// lblY2
			// 
			this.lblY2.AccessibleDescription = ((string)(resources.GetObject("lblY2.AccessibleDescription")));
			this.lblY2.AccessibleName = ((string)(resources.GetObject("lblY2.AccessibleName")));
			this.lblY2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblY2.Anchor")));
			this.lblY2.AutoSize = ((bool)(resources.GetObject("lblY2.AutoSize")));
			this.lblY2.BackColor = System.Drawing.Color.Transparent;
			this.lblY2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblY2.Dock")));
			this.lblY2.Enabled = ((bool)(resources.GetObject("lblY2.Enabled")));
			this.lblY2.Font = ((System.Drawing.Font)(resources.GetObject("lblY2.Font")));
			this.lblY2.Image = ((System.Drawing.Image)(resources.GetObject("lblY2.Image")));
			this.lblY2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblY2.ImageAlign")));
			this.lblY2.ImageIndex = ((int)(resources.GetObject("lblY2.ImageIndex")));
			this.lblY2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblY2.ImeMode")));
			this.lblY2.Location = ((System.Drawing.Point)(resources.GetObject("lblY2.Location")));
			this.lblY2.Name = "lblY2";
			this.lblY2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblY2.RightToLeft")));
			this.lblY2.Size = ((System.Drawing.Size)(resources.GetObject("lblY2.Size")));
			this.lblY2.TabIndex = ((int)(resources.GetObject("lblY2.TabIndex")));
			this.lblY2.Text = resources.GetString("lblY2.Text");
			this.lblY2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblY2.TextAlign")));
			this.lblY2.Visible = ((bool)(resources.GetObject("lblY2.Visible")));
			// 
			// lxlY
			// 
			this.lxlY.AccessibleDescription = ((string)(resources.GetObject("lxlY.AccessibleDescription")));
			this.lxlY.AccessibleName = ((string)(resources.GetObject("lxlY.AccessibleName")));
			this.lxlY.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lxlY.Anchor")));
			this.lxlY.AutoSize = ((bool)(resources.GetObject("lxlY.AutoSize")));
			this.lxlY.BackColor = System.Drawing.Color.Transparent;
			this.lxlY.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lxlY.Dock")));
			this.lxlY.Enabled = ((bool)(resources.GetObject("lxlY.Enabled")));
			this.lxlY.Font = ((System.Drawing.Font)(resources.GetObject("lxlY.Font")));
			this.lxlY.Image = ((System.Drawing.Image)(resources.GetObject("lxlY.Image")));
			this.lxlY.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lxlY.ImageAlign")));
			this.lxlY.ImageIndex = ((int)(resources.GetObject("lxlY.ImageIndex")));
			this.lxlY.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lxlY.ImeMode")));
			this.lxlY.Location = ((System.Drawing.Point)(resources.GetObject("lxlY.Location")));
			this.lxlY.Name = "lxlY";
			this.lxlY.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lxlY.RightToLeft")));
			this.lxlY.Size = ((System.Drawing.Size)(resources.GetObject("lxlY.Size")));
			this.lxlY.TabIndex = ((int)(resources.GetObject("lxlY.TabIndex")));
			this.lxlY.Text = resources.GetString("lxlY.Text");
			this.lxlY.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lxlY.TextAlign")));
			this.lxlY.Visible = ((bool)(resources.GetObject("lxlY.Visible")));
			// 
			// lblEnd
			// 
			this.lblEnd.AccessibleDescription = ((string)(resources.GetObject("lblEnd.AccessibleDescription")));
			this.lblEnd.AccessibleName = ((string)(resources.GetObject("lblEnd.AccessibleName")));
			this.lblEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblEnd.Anchor")));
			this.lblEnd.AutoSize = ((bool)(resources.GetObject("lblEnd.AutoSize")));
			this.lblEnd.BackColor = System.Drawing.Color.Transparent;
			this.lblEnd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblEnd.Dock")));
			this.lblEnd.Enabled = ((bool)(resources.GetObject("lblEnd.Enabled")));
			this.lblEnd.Font = ((System.Drawing.Font)(resources.GetObject("lblEnd.Font")));
			this.lblEnd.Image = ((System.Drawing.Image)(resources.GetObject("lblEnd.Image")));
			this.lblEnd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEnd.ImageAlign")));
			this.lblEnd.ImageIndex = ((int)(resources.GetObject("lblEnd.ImageIndex")));
			this.lblEnd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblEnd.ImeMode")));
			this.lblEnd.Location = ((System.Drawing.Point)(resources.GetObject("lblEnd.Location")));
			this.lblEnd.Name = "lblEnd";
			this.lblEnd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblEnd.RightToLeft")));
			this.lblEnd.Size = ((System.Drawing.Size)(resources.GetObject("lblEnd.Size")));
			this.lblEnd.TabIndex = ((int)(resources.GetObject("lblEnd.TabIndex")));
			this.lblEnd.Text = resources.GetString("lblEnd.Text");
			this.lblEnd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEnd.TextAlign")));
			this.lblEnd.Visible = ((bool)(resources.GetObject("lblEnd.Visible")));
			// 
			// lblI
			// 
			this.lblI.AccessibleDescription = ((string)(resources.GetObject("lblI.AccessibleDescription")));
			this.lblI.AccessibleName = ((string)(resources.GetObject("lblI.AccessibleName")));
			this.lblI.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblI.Anchor")));
			this.lblI.AutoSize = ((bool)(resources.GetObject("lblI.AutoSize")));
			this.lblI.BackColor = System.Drawing.Color.Transparent;
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
			this.lblJ.BackColor = System.Drawing.Color.Transparent;
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
			// lblStart
			// 
			this.lblStart.AccessibleDescription = ((string)(resources.GetObject("lblStart.AccessibleDescription")));
			this.lblStart.AccessibleName = ((string)(resources.GetObject("lblStart.AccessibleName")));
			this.lblStart.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblStart.Anchor")));
			this.lblStart.AutoSize = ((bool)(resources.GetObject("lblStart.AutoSize")));
			this.lblStart.BackColor = System.Drawing.Color.Transparent;
			this.lblStart.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblStart.Dock")));
			this.lblStart.Enabled = ((bool)(resources.GetObject("lblStart.Enabled")));
			this.lblStart.Font = ((System.Drawing.Font)(resources.GetObject("lblStart.Font")));
			this.lblStart.Image = ((System.Drawing.Image)(resources.GetObject("lblStart.Image")));
			this.lblStart.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStart.ImageAlign")));
			this.lblStart.ImageIndex = ((int)(resources.GetObject("lblStart.ImageIndex")));
			this.lblStart.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblStart.ImeMode")));
			this.lblStart.Location = ((System.Drawing.Point)(resources.GetObject("lblStart.Location")));
			this.lblStart.Name = "lblStart";
			this.lblStart.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblStart.RightToLeft")));
			this.lblStart.Size = ((System.Drawing.Size)(resources.GetObject("lblStart.Size")));
			this.lblStart.TabIndex = ((int)(resources.GetObject("lblStart.TabIndex")));
			this.lblStart.Text = resources.GetString("lblStart.Text");
			this.lblStart.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStart.TextAlign")));
			this.lblStart.Visible = ((bool)(resources.GetObject("lblStart.Visible")));
			// 
			// ptxStartX
			// 
			this.ptxStartX.AccessibleDescription = ((string)(resources.GetObject("ptxStartX.AccessibleDescription")));
			this.ptxStartX.AccessibleName = ((string)(resources.GetObject("ptxStartX.AccessibleName")));
			this.ptxStartX.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ptxStartX.Anchor")));
			this.ptxStartX.AutoSize = ((bool)(resources.GetObject("ptxStartX.AutoSize")));
			this.ptxStartX.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ptxStartX.BackgroundImage")));
			this.ptxStartX.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ptxStartX.Dock")));
			this.ptxStartX.Enabled = ((bool)(resources.GetObject("ptxStartX.Enabled")));
			this.ptxStartX.Font = ((System.Drawing.Font)(resources.GetObject("ptxStartX.Font")));
			this.ptxStartX.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ptxStartX.ImeMode")));
			this.ptxStartX.Location = ((System.Drawing.Point)(resources.GetObject("ptxStartX.Location")));
			this.ptxStartX.Maximum = 3.402823E+38F;
			this.ptxStartX.MaxLength = ((int)(resources.GetObject("ptxStartX.MaxLength")));
			this.ptxStartX.Minimum = 3.402823E+38F;
			this.ptxStartX.Multiline = ((bool)(resources.GetObject("ptxStartX.Multiline")));
			this.ptxStartX.Name = "ptxStartX";
			this.ptxStartX.PasswordChar = ((char)(resources.GetObject("ptxStartX.PasswordChar")));
			this.ptxStartX.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ptxStartX.RightToLeft")));
			this.ptxStartX.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ptxStartX.ScrollBars")));
			this.ptxStartX.Size = ((System.Drawing.Size)(resources.GetObject("ptxStartX.Size")));
			this.ptxStartX.TabIndex = ((int)(resources.GetObject("ptxStartX.TabIndex")));
			this.ptxStartX.Text = resources.GetString("ptxStartX.Text");
			this.ptxStartX.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ptxStartX.TextAlign")));
			this.ptxStartX.Value = 0F;
			this.ptxStartX.Visible = ((bool)(resources.GetObject("ptxStartX.Visible")));
			this.ptxStartX.WordWrap = ((bool)(resources.GetObject("ptxStartX.WordWrap")));
			// 
			// ptxStartY
			// 
			this.ptxStartY.AccessibleDescription = ((string)(resources.GetObject("ptxStartY.AccessibleDescription")));
			this.ptxStartY.AccessibleName = ((string)(resources.GetObject("ptxStartY.AccessibleName")));
			this.ptxStartY.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ptxStartY.Anchor")));
			this.ptxStartY.AutoSize = ((bool)(resources.GetObject("ptxStartY.AutoSize")));
			this.ptxStartY.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ptxStartY.BackgroundImage")));
			this.ptxStartY.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ptxStartY.Dock")));
			this.ptxStartY.Enabled = ((bool)(resources.GetObject("ptxStartY.Enabled")));
			this.ptxStartY.Font = ((System.Drawing.Font)(resources.GetObject("ptxStartY.Font")));
			this.ptxStartY.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ptxStartY.ImeMode")));
			this.ptxStartY.Location = ((System.Drawing.Point)(resources.GetObject("ptxStartY.Location")));
			this.ptxStartY.Maximum = 3.402823E+38F;
			this.ptxStartY.MaxLength = ((int)(resources.GetObject("ptxStartY.MaxLength")));
			this.ptxStartY.Minimum = 3.402823E+38F;
			this.ptxStartY.Multiline = ((bool)(resources.GetObject("ptxStartY.Multiline")));
			this.ptxStartY.Name = "ptxStartY";
			this.ptxStartY.PasswordChar = ((char)(resources.GetObject("ptxStartY.PasswordChar")));
			this.ptxStartY.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ptxStartY.RightToLeft")));
			this.ptxStartY.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ptxStartY.ScrollBars")));
			this.ptxStartY.Size = ((System.Drawing.Size)(resources.GetObject("ptxStartY.Size")));
			this.ptxStartY.TabIndex = ((int)(resources.GetObject("ptxStartY.TabIndex")));
			this.ptxStartY.Text = resources.GetString("ptxStartY.Text");
			this.ptxStartY.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ptxStartY.TextAlign")));
			this.ptxStartY.Value = 0F;
			this.ptxStartY.Visible = ((bool)(resources.GetObject("ptxStartY.Visible")));
			this.ptxStartY.WordWrap = ((bool)(resources.GetObject("ptxStartY.WordWrap")));
			// 
			// ptxEndX
			// 
			this.ptxEndX.AccessibleDescription = ((string)(resources.GetObject("ptxEndX.AccessibleDescription")));
			this.ptxEndX.AccessibleName = ((string)(resources.GetObject("ptxEndX.AccessibleName")));
			this.ptxEndX.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ptxEndX.Anchor")));
			this.ptxEndX.AutoSize = ((bool)(resources.GetObject("ptxEndX.AutoSize")));
			this.ptxEndX.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ptxEndX.BackgroundImage")));
			this.ptxEndX.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ptxEndX.Dock")));
			this.ptxEndX.Enabled = ((bool)(resources.GetObject("ptxEndX.Enabled")));
			this.ptxEndX.Font = ((System.Drawing.Font)(resources.GetObject("ptxEndX.Font")));
			this.ptxEndX.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ptxEndX.ImeMode")));
			this.ptxEndX.Location = ((System.Drawing.Point)(resources.GetObject("ptxEndX.Location")));
			this.ptxEndX.Maximum = 3.402823E+38F;
			this.ptxEndX.MaxLength = ((int)(resources.GetObject("ptxEndX.MaxLength")));
			this.ptxEndX.Minimum = 3.402823E+38F;
			this.ptxEndX.Multiline = ((bool)(resources.GetObject("ptxEndX.Multiline")));
			this.ptxEndX.Name = "ptxEndX";
			this.ptxEndX.PasswordChar = ((char)(resources.GetObject("ptxEndX.PasswordChar")));
			this.ptxEndX.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ptxEndX.RightToLeft")));
			this.ptxEndX.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ptxEndX.ScrollBars")));
			this.ptxEndX.Size = ((System.Drawing.Size)(resources.GetObject("ptxEndX.Size")));
			this.ptxEndX.TabIndex = ((int)(resources.GetObject("ptxEndX.TabIndex")));
			this.ptxEndX.Text = resources.GetString("ptxEndX.Text");
			this.ptxEndX.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ptxEndX.TextAlign")));
			this.ptxEndX.Value = 0F;
			this.ptxEndX.Visible = ((bool)(resources.GetObject("ptxEndX.Visible")));
			this.ptxEndX.WordWrap = ((bool)(resources.GetObject("ptxEndX.WordWrap")));
			// 
			// ptxEndY
			// 
			this.ptxEndY.AccessibleDescription = ((string)(resources.GetObject("ptxEndY.AccessibleDescription")));
			this.ptxEndY.AccessibleName = ((string)(resources.GetObject("ptxEndY.AccessibleName")));
			this.ptxEndY.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ptxEndY.Anchor")));
			this.ptxEndY.AutoSize = ((bool)(resources.GetObject("ptxEndY.AutoSize")));
			this.ptxEndY.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ptxEndY.BackgroundImage")));
			this.ptxEndY.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ptxEndY.Dock")));
			this.ptxEndY.Enabled = ((bool)(resources.GetObject("ptxEndY.Enabled")));
			this.ptxEndY.Font = ((System.Drawing.Font)(resources.GetObject("ptxEndY.Font")));
			this.ptxEndY.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ptxEndY.ImeMode")));
			this.ptxEndY.Location = ((System.Drawing.Point)(resources.GetObject("ptxEndY.Location")));
			this.ptxEndY.Maximum = 3.402823E+38F;
			this.ptxEndY.MaxLength = ((int)(resources.GetObject("ptxEndY.MaxLength")));
			this.ptxEndY.Minimum = 3.402823E+38F;
			this.ptxEndY.Multiline = ((bool)(resources.GetObject("ptxEndY.Multiline")));
			this.ptxEndY.Name = "ptxEndY";
			this.ptxEndY.PasswordChar = ((char)(resources.GetObject("ptxEndY.PasswordChar")));
			this.ptxEndY.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ptxEndY.RightToLeft")));
			this.ptxEndY.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ptxEndY.ScrollBars")));
			this.ptxEndY.Size = ((System.Drawing.Size)(resources.GetObject("ptxEndY.Size")));
			this.ptxEndY.TabIndex = ((int)(resources.GetObject("ptxEndY.TabIndex")));
			this.ptxEndY.Text = resources.GetString("ptxEndY.Text");
			this.ptxEndY.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ptxEndY.TextAlign")));
			this.ptxEndY.Value = 0F;
			this.ptxEndY.Visible = ((bool)(resources.GetObject("ptxEndY.Visible")));
			this.ptxEndY.WordWrap = ((bool)(resources.GetObject("ptxEndY.WordWrap")));
			// 
			// ptxI
			// 
			this.ptxI.AccessibleDescription = ((string)(resources.GetObject("ptxI.AccessibleDescription")));
			this.ptxI.AccessibleName = ((string)(resources.GetObject("ptxI.AccessibleName")));
			this.ptxI.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ptxI.Anchor")));
			this.ptxI.AutoSize = ((bool)(resources.GetObject("ptxI.AutoSize")));
			this.ptxI.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ptxI.BackgroundImage")));
			this.ptxI.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ptxI.Dock")));
			this.ptxI.Enabled = ((bool)(resources.GetObject("ptxI.Enabled")));
			this.ptxI.Font = ((System.Drawing.Font)(resources.GetObject("ptxI.Font")));
			this.ptxI.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ptxI.ImeMode")));
			this.ptxI.Location = ((System.Drawing.Point)(resources.GetObject("ptxI.Location")));
			this.ptxI.Maximum = 3.402823E+38F;
			this.ptxI.MaxLength = ((int)(resources.GetObject("ptxI.MaxLength")));
			this.ptxI.Minimum = 3.402823E+38F;
			this.ptxI.Multiline = ((bool)(resources.GetObject("ptxI.Multiline")));
			this.ptxI.Name = "ptxI";
			this.ptxI.PasswordChar = ((char)(resources.GetObject("ptxI.PasswordChar")));
			this.ptxI.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ptxI.RightToLeft")));
			this.ptxI.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ptxI.ScrollBars")));
			this.ptxI.Size = ((System.Drawing.Size)(resources.GetObject("ptxI.Size")));
			this.ptxI.TabIndex = ((int)(resources.GetObject("ptxI.TabIndex")));
			this.ptxI.Text = resources.GetString("ptxI.Text");
			this.ptxI.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ptxI.TextAlign")));
			this.ptxI.Value = 0F;
			this.ptxI.Visible = ((bool)(resources.GetObject("ptxI.Visible")));
			this.ptxI.WordWrap = ((bool)(resources.GetObject("ptxI.WordWrap")));
			// 
			// ptxJ
			// 
			this.ptxJ.AccessibleDescription = ((string)(resources.GetObject("ptxJ.AccessibleDescription")));
			this.ptxJ.AccessibleName = ((string)(resources.GetObject("ptxJ.AccessibleName")));
			this.ptxJ.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ptxJ.Anchor")));
			this.ptxJ.AutoSize = ((bool)(resources.GetObject("ptxJ.AutoSize")));
			this.ptxJ.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ptxJ.BackgroundImage")));
			this.ptxJ.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ptxJ.Dock")));
			this.ptxJ.Enabled = ((bool)(resources.GetObject("ptxJ.Enabled")));
			this.ptxJ.Font = ((System.Drawing.Font)(resources.GetObject("ptxJ.Font")));
			this.ptxJ.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ptxJ.ImeMode")));
			this.ptxJ.Location = ((System.Drawing.Point)(resources.GetObject("ptxJ.Location")));
			this.ptxJ.Maximum = 3.402823E+38F;
			this.ptxJ.MaxLength = ((int)(resources.GetObject("ptxJ.MaxLength")));
			this.ptxJ.Minimum = 3.402823E+38F;
			this.ptxJ.Multiline = ((bool)(resources.GetObject("ptxJ.Multiline")));
			this.ptxJ.Name = "ptxJ";
			this.ptxJ.PasswordChar = ((char)(resources.GetObject("ptxJ.PasswordChar")));
			this.ptxJ.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ptxJ.RightToLeft")));
			this.ptxJ.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ptxJ.ScrollBars")));
			this.ptxJ.Size = ((System.Drawing.Size)(resources.GetObject("ptxJ.Size")));
			this.ptxJ.TabIndex = ((int)(resources.GetObject("ptxJ.TabIndex")));
			this.ptxJ.Text = resources.GetString("ptxJ.Text");
			this.ptxJ.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ptxJ.TextAlign")));
			this.ptxJ.Value = 0F;
			this.ptxJ.Visible = ((bool)(resources.GetObject("ptxJ.Visible")));
			this.ptxJ.WordWrap = ((bool)(resources.GetObject("ptxJ.WordWrap")));
			// 
			// frmNewVectorWizard
			// 
			this.AcceptButton = this.btnCreate;
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
																		  this.ptxJ,
																		  this.ptxI,
																		  this.ptxEndY,
																		  this.ptxEndX,
																		  this.ptxStartY,
																		  this.ptxStartX,
																		  this.lblY2,
																		  this.lxlY,
																		  this.lblEnd,
																		  this.lblI,
																		  this.lblJ,
																		  this.lblStart,
																		  this.rdbSizeAndMouse,
																		  this.btnCancel,
																		  this.btnCreate,
																		  this.rdbSize,
																		  this.rdbPoint});
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
			this.Name = "frmNewVectorWizard";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.ShowInTaskbar = false;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Visible = ((bool)(resources.GetObject("$this.Visible")));
			this.Load += new System.EventHandler(this.frmNewVectorWizard_Load);
			this.ResumeLayout(false);

		}
		#endregion
	}
}


