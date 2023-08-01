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
//-------------------------------------------------------------------------------------------
namespace ArdeshirV.Applications.Vector
{
	/// <summary>
	/// Options Form for Change Environment Option in Vector Project.
	/// </summary>
	public class Options :
		System.Windows.Forms.Form
	{
		#region Variables

		private System.Windows.Forms.ColorDialog ColorDialog;
		private System.Windows.Forms.Label StartVectorColorLabel;
		private System.Windows.Forms.Button ChangeEndColor;
		private System.Windows.Forms.Label EndVectorColorLabel;
		private System.Windows.Forms.Button ChangeHeaderColor;
		private System.Windows.Forms.Button ChangeBackColor;
		private System.Windows.Forms.Label BackColorLabel;
		private System.Windows.Forms.Button ChaneGridColor;
		private System.Windows.Forms.Label GridColorLabel;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabColor;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblclrEndVectorColor;
		private System.Windows.Forms.Label lblclrStartVectorColor;
		private System.Windows.Forms.Label lblclrGridColor;
		private System.Windows.Forms.Label lblclrBackColor1;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TrackBar trcGridHeightSize;
		private System.Windows.Forms.TrackBar trcGridWidthSize;
		private System.Windows.Forms.Label lblGridWidthSizeLabel;
		private System.Windows.Forms.Label lblGridHeightSizeLabel;
		private System.Windows.Forms.Label lblclrDotDotColor;
		private System.Windows.Forms.Button btnChangeDotDotVectors;
		private System.Windows.Forms.CheckBox ckbShowGrid;
		private System.Windows.Forms.Label lblclrActiveColor;
		private System.Windows.Forms.Button btnChangeActiveColor;
		private System.Windows.Forms.Label lblActiveColorLabel;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.CheckBox ckbShowNotifyIcon;
		private System.Windows.Forms.Button btnClearAllVector;
		private System.Windows.Forms.CheckBox ckbQuestionBeforDeleteAll;
		private System.Windows.Forms.CheckBox ckbQuestionBeforClose;
		private System.Windows.Forms.CheckBox ckbStartupMode;
		private System.Windows.Forms.CheckBox ckbShowName;
		private Environments_Variables m_Options;
		private System.Windows.Forms.Label lblMovingVectorColor;
		private System.Windows.Forms.Label lblclrResult;
		private System.Windows.Forms.Label lblResultLabel;
		private System.Windows.Forms.Button btnChangeResultColor;
		private System.Windows.Forms.TrackBar trcGeometry;
		private ScreenVector screenVector;
		private System.Windows.Forms.Label lblGeometry;
		private System.Windows.Forms.ImageList imgList;
		private System.Windows.Forms.GroupBox grpPreview;
		private System.ComponentModel.IContainer components;

		#endregion
		//-----------------------------------------------------------------------------------
		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public Options()
		{			
			InitializeComponent();

			SuspendLayout();
			tabColor.SuspendLayout();
			tabControl.SuspendLayout();
			tabGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(trcGeometry)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(trcGridWidthSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(trcGridHeightSize)).BeginInit();

			Opacity = 1;
			InitScreenVector();
			trcGeometry.Minimum = 1;
			trcGeometry.Maximum = 10;
			lblGeometry.Text = "Sca&le";
			//OpacityChangerInterval = -1;
			trcGridWidthSize.SetRange(2, 20);
			trcGridHeightSize.SetRange(2, 20);
			trcGridWidthSize.Value = 5;
			trcGridHeightSize.Value = 5;
			trcGridWidthSize.TickFrequency = 1;
			trcGridHeightSize.TickFrequency = 1;
			btnOK.BackColor = Color.Transparent;
			btnReset.BackColor = Color.Transparent;
			btnCancel.BackColor = Color.Transparent;
			StartPosition = FormStartPosition.CenterParent;
			btnClearAllVector.BackColor = Color.Transparent;

			((System.ComponentModel.ISupportInitialize)(trcGridHeightSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(trcGridWidthSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(trcGeometry)).EndInit();
			tabControl.ResumeLayout(false);
			tabGeneral.ResumeLayout(false);
			tabColor.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		//-----------------------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Get or set options for this class.
		/// </summary>
		public Environments_Variables Option
		{
			get
			{
				return this.m_Options;
			}
			set
			{
				m_Options = value;
				this.screenVector.Options = value;
				trcGridWidthSize.Value = (value.sizResolution.Width==0)?1:
					value.sizResolution.Width;
				trcGridHeightSize.Value = (value.sizResolution.Height==0)?1:
					value.sizResolution.Height;
				ckbStartupMode.Checked = /*m_Options.blnNewDocAtStartUp =*/ 
					value.blnNewDocAtStartUp;
				ckbQuestionBeforClose.Checked = 
					/*m_Options.blnQuestionBeforeCloseDocument = */
					value.blnQuestionBeforeCloseDocument;
				ckbQuestionBeforDeleteAll.Checked = 
					/*m_Options.blnQuestionBeforeDeleteAll =*/
					value.blnQuestionBeforeDeleteAll;
				ckbShowGrid.Checked = /*m_Options.blnShowGrid = */
					value.blnShowGrid;
				ckbShowName.Checked = /*m_Options.blnShowID =*/value.blnShowID;
				ckbShowNotifyIcon.Checked = 
					/*m_Options.blnShowNotifyIcon =*/value.blnShowNotifyIcon;
				lblclrActiveColor.BackColor =
					/*m_Options.clrActiveColor =*/value.clrActiveColor;
				lblclrDotDotColor.BackColor = 
					/*m_Options.clrDotDotVectorColor =*/value.clrDotDotVectorColor;
				lblclrEndVectorColor.BackColor = 
					/*m_Options.clrEndingVectorColor =*/value.clrEndingVectorColor;
				lblclrGridColor.BackColor = /*m_Options.clrGridColor =*/
					value.clrGridColor;
				lblclrStartVectorColor.BackColor = 
					/*m_Options.clrHeaderVectorColor =*/ value.clrHeaderVectorColor;
			}
		}
		#endregion
		//-----------------------------------------------------------------------------------
		#region Utility functions

		/// <summary>
		/// Initialization screen vector.
		/// </summary>
		void InitScreenVector()
		{
			screenVector = new ScreenVector();
			screenVector.Dock = DockStyle.Fill;
			grpPreview.Controls.Add(screenVector);
		}

		#endregion
		//-----------------------------------------------------------------------------------
		#region Form Event Handlers

		/// <summary>
		/// Occured whenever Form has been loaded.
		/// </summary>
		/// <param name="sender">Options form</param>
		/// <param name="e">Event argument</param>
		private void Options_Load(object sender, System.EventArgs e)
		{
			screenVector.Options = m_Options;
			ColorDialog.FullOpen = true;
			ckbShowGrid.Checked = m_Options.blnShowGrid;
			trcGridWidthSize.Value = m_Options.sizResolution.Width;
			trcGridHeightSize.Value = m_Options.sizResolution.Height;
			lblGridWidthSizeLabel.Text = "Grid Width Si&ze : " +
				trcGridWidthSize.Value.ToString();
			lblGridHeightSizeLabel.Text = "Grid Height Si&ze : " +
				trcGridHeightSize.Value.ToString();
			lblclrResult.BackColor = m_Options.clrResultColor;
			trcGeometry.Value = (int)m_Options.intGeo;
			lblGeometry.Text = "Sca&le : " + trcGeometry.Value.ToString();
			lblclrBackColor1.BackColor = m_Options.clrBackColor;
			m_Options.intGeo = Global.s_intGeo;

#if DEBUG
			//MessageBox.Show("&Geometry : " + this.m_Options.intGeo.ToString());
#endif
		}

		#endregion
		//-----------------------------------------------------------------------------------
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
		//-----------------------------------------------------------------------------------
		#region Button Event Handlers

		/// <summary>
		/// Occured whenever Change Grid Color button has been clicked.
		/// </summary>
		/// <param name="sender">Grid color changer</param>
		/// <param name="e">Event argument</param>
		private void ChaneGridColor_Click(object sender, System.EventArgs e)
		{
			if(ColorDialog.ShowDialog(this)== DialogResult.OK)
			{
				lblclrGridColor.BackColor = ColorDialog.Color;
				screenVector.GridColor = ColorDialog.Color;
			}
		}

		/// <summary>
		/// Occured whenever Change Back Color button has been clicked.
		/// </summary>
		/// <param name="sender">Back color changer</param>
		/// <param name="e">Event argument</param>
		private void ChangeBackColor_Click(object sender, System.EventArgs e)
		{
			if(ColorDialog.ShowDialog(this)== DialogResult.OK)
			{
				lblclrBackColor1.BackColor = ColorDialog.Color;
				screenVector.BackgroundColor = ColorDialog.Color;
			}
		}

		/// <summary>
		/// Occured whenever Change Header Color button has been clicked.
		/// </summary>
		/// <param name="sender">Header color changer</param>
		/// <param name="e">Event argument</param>
		private void ChangeHeaderColor_Click(object sender, System.EventArgs e)
		{
			if(ColorDialog.ShowDialog(this)== DialogResult.OK)
			{
				lblclrStartVectorColor.BackColor = ColorDialog.Color;
				screenVector.HeaderVectorColor = ColorDialog.Color;
			}
		}

		/// <summary>
		/// Occured whenever Change End Color button has been clicked.
		/// </summary>
		/// <param name="sender">End color changer</param>
		/// <param name="e">Event argument</param>
		private void ChangeEndColor_Click(object sender, System.EventArgs e)
		{
			if(ColorDialog.ShowDialog(this)== DialogResult.OK)
			{
				lblclrEndVectorColor.BackColor = ColorDialog.Color;
				screenVector.EndingVectorColor = ColorDialog.Color;
			}
		}

		/// <summary>
		/// Occured whenever Change Moving Color has been clicked.
		/// </summary>
		/// <param name="sender">Moving vector color changer</param>
		/// <param name="e">Event argument</param>
		private void btnChangeDotDotVectors_Click(object sender, System.EventArgs e)
		{
			if(ColorDialog.ShowDialog(this)== DialogResult.OK)
			{
				lblclrDotDotColor.BackColor = ColorDialog.Color;
				screenVector.MovingColor = ColorDialog.Color;
			}
		}

		/// <summary>
		/// Occured whenever Change Active Color has been changed.
		/// </summary>
		/// <param name="sender">Active vector color changer</param>
		/// <param name="e">Event argument</param>
		private void btnChangeActiveColor_Click(object sender, System.EventArgs e)
		{
			if(ColorDialog.ShowDialog(this)== DialogResult.OK)
			{
				lblclrActiveColor.BackColor = ColorDialog.Color;
				screenVector.ActiveColor = ColorDialog.Color;
			}
		}
		
		/// <summary>
		/// Occured whenever result color button has been clicked.
		/// </summary>
		/// <param name="sender">Result color changer</param>
		/// <param name="e">Event argument</param>
		private void btnChangeResultColor_Click(object sender, System.EventArgs e)
		{
			if(ColorDialog.ShowDialog(this)==System.Windows.Forms.DialogResult.OK)
			{
				lblclrResult.BackColor = ColorDialog.Color;
				screenVector.ResultColor = ColorDialog.Color;
			}
		}

		/// <summary>
		/// Occured whenever Reset button has been changed.
		/// </summary>
		/// <param name="sender">Reset button</param>
		/// <param name="e">Event argument</param>
		private void btnReset_Click(object sender, System.EventArgs e)
		{
			m_Options.LoadDefaultValues();
			ckbStartupMode.Checked = m_Options.blnNewDocAtStartUp;
			ckbQuestionBeforClose.Checked = m_Options.blnQuestionBeforeCloseDocument;
			ckbQuestionBeforDeleteAll.Checked = m_Options.blnQuestionBeforeDeleteAll;
			ckbShowGrid.Checked = m_Options.blnShowGrid;
			ckbShowName.Checked = m_Options.blnShowID;
			ckbShowNotifyIcon.Checked = m_Options.blnShowNotifyIcon;
			lblclrActiveColor.BackColor = m_Options.clrActiveColor;
			lblclrDotDotColor.BackColor = m_Options.clrDotDotVectorColor;
			lblclrEndVectorColor.BackColor = m_Options.clrEndingVectorColor;
			lblclrGridColor.BackColor = m_Options.clrGridColor;
			lblclrResult.BackColor = m_Options.clrResultColor;
			lblclrStartVectorColor.BackColor = m_Options.clrHeaderVectorColor;
			screenVector.Resolution = m_Options.sizResolution;
			ckbShowNotifyIcon.Checked = m_Options.blnShowNotifyIcon;
			trcGridWidthSize.Value = m_Options.sizResolution.Width;
			trcGridHeightSize.Value = m_Options.sizResolution.Height;
			lblGridWidthSizeLabel.Text = "Grid Width Si&ze : " +
				trcGridWidthSize.Value.ToString();
			lblGridHeightSizeLabel.Text = "Grid Height Si&ze : " +
				trcGridHeightSize.Value.ToString();
			trcGeometry.Value = (int)m_Options.intGeo;
			lblclrBackColor1.BackColor = m_Options.clrBackColor;
			lblGeometry.Text = "&Scale : " + trcGeometry.Value.ToString();
			screenVector.Options = m_Options;
		}

		/// <summary>
		/// Occured whenever OK button has been clicked.
		/// </summary>
		/// <param name="sender">OK button</param>
		/// <param name="e">Event argument</param>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			m_Options.blnShowID = ckbShowName.Checked;
			m_Options.intGeo = (float)trcGeometry.Value;
			m_Options.blnShowGrid = ckbShowGrid.Checked;
			m_Options.clrResultColor = lblclrResult.BackColor;
			m_Options.sizResolution = screenVector.Resolution;
			m_Options.clrGridColor = lblclrGridColor.BackColor;
			m_Options.clrBackColor = lblclrBackColor1.BackColor;
			m_Options.blnNewDocAtStartUp = ckbStartupMode.Checked;
			m_Options.clrActiveColor = lblclrActiveColor.BackColor;
			m_Options.blnShowNotifyIcon = ckbShowNotifyIcon.Checked;
			m_Options.blnShowNotifyIcon = ckbShowNotifyIcon.Checked;
			m_Options.clrDotDotVectorColor = lblclrDotDotColor.BackColor;
			m_Options.clrEndingVectorColor = lblclrEndVectorColor.BackColor;
			m_Options.clrHeaderVectorColor = lblclrStartVectorColor.BackColor;
			m_Options.blnQuestionBeforeCloseDocument = ckbQuestionBeforClose.Checked;
			m_Options.blnQuestionBeforeDeleteAll = ckbQuestionBeforDeleteAll.Checked;
			m_Options.sizResolution = new Size(trcGridWidthSize.Value,
				trcGridHeightSize.Value);

#if DEBUG
			//MessageBox.Show("Event button click for OK button occured.");
#endif
		}

		/// <summary>
		/// Occured whenever Cancel button has been clicked.
		/// </summary>
		/// <param name="sender">Cancel button</param>
		/// <param name="e">Event argument</param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Occured whenever Clear All Vectors button has been clicked.
		/// </summary>
		/// <param name="sender">Clear all vectors button</param>
		/// <param name="e">Event argument</param>
		private void btnClearAllVector_Click(object sender, System.EventArgs e)
		{
			screenVector.ClearAllVectors(false);
		}

		#endregion
		//-----------------------------------------------------------------------------------
		#region Track Bar Event handlers

		/// <summary>
		/// Occured whenever Geometry Scroll bar has been cahnged.
		/// </summary>
		/// <param name="sender">Geometry track bar</param>
		/// <param name="e">Event argument</param>
		private void trcGeometry_Scroll(object sender, System.EventArgs e)
		{
			lblGeometry.Text = "Sca&le : " + trcGeometry.Value.ToString();
		}

		/// <summary>
		/// Occured whenever Grid Height track bar has been changed. 
		/// </summary>
		/// <param name="sender">Grid height size track bar</param>
		/// <param name="e">Event argument</param>
		private void trcGridHeightSize_Scroll_1(object sender, System.EventArgs e)
		{
			lblGridHeightSizeLabel.Text = "Grid Height Si&ze :"+
				trcGridHeightSize.Value.ToString();
			screenVector.Resolution = new
				Size(screenVector.Resolution.Width, trcGridHeightSize.Value);
		}
		
		/// <summary>
		/// Occured whenever Grid Width track bar has been changed.
		/// </summary>
		/// <param name="sender">Grid width size track bar</param>
		/// <param name="e">Event argument</param>
		private void trcGridWidthSize_Scroll_1(object sender, System.EventArgs e)
		{
			lblGridWidthSizeLabel.Text = "Grid Width Si&ze :"+
				trcGridWidthSize.Value.ToString();
			screenVector.Resolution = new
				Size(trcGridWidthSize.Value, screenVector.Resolution.Height);
		}

		#endregion
		//-----------------------------------------------------------------------------------
		#region Check Box Event Handlers

		/// <summary>
		/// Occured whenever Show Grid Check Box has been changed.
		/// </summary>
		/// <param name="sender">Show grid check box</param>
		/// <param name="e">Event argument</param>
		private void ckbShowGrid_CheckedChanged(object sender, System.EventArgs e)
		{
			screenVector.ShowGrid =
			ChaneGridColor.Enabled =
			GridColorLabel.Enabled =
			lblclrGridColor.Enabled =
			trcGridWidthSize.Enabled =
			trcGridHeightSize.Enabled =
			lblGridWidthSizeLabel.Enabled =
			lblGridHeightSizeLabel.Enabled = ckbShowGrid.Checked;
		}
		
		/// <summary>
		/// Occured whenever Show Name Check Box has been changed.
		/// </summary>
		/// <param name="sender">Show name check box</param>
		/// <param name="e">Event argument</param>
		private void ckbShowName_CheckedChanged(object sender, System.EventArgs e)
		{
			screenVector.ShowName = ckbShowName.Checked;
		}

		#endregion
		//-----------------------------------------------------------------------------------
		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Options));
			this.ColorDialog = new System.Windows.Forms.ColorDialog();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.trcGeometry = new System.Windows.Forms.TrackBar();
			this.ckbShowName = new System.Windows.Forms.CheckBox();
			this.ckbStartupMode = new System.Windows.Forms.CheckBox();
			this.trcGridHeightSize = new System.Windows.Forms.TrackBar();
			this.trcGridWidthSize = new System.Windows.Forms.TrackBar();
			this.ckbQuestionBeforClose = new System.Windows.Forms.CheckBox();
			this.ckbQuestionBeforDeleteAll = new System.Windows.Forms.CheckBox();
			this.ckbShowNotifyIcon = new System.Windows.Forms.CheckBox();
			this.ckbShowGrid = new System.Windows.Forms.CheckBox();
			this.lblGridWidthSizeLabel = new System.Windows.Forms.Label();
			this.lblGridHeightSizeLabel = new System.Windows.Forms.Label();
			this.lblGeometry = new System.Windows.Forms.Label();
			this.tabColor = new System.Windows.Forms.TabPage();
			this.lblclrGridColor = new System.Windows.Forms.Label();
			this.lblclrResult = new System.Windows.Forms.Label();
			this.lblResultLabel = new System.Windows.Forms.Label();
			this.btnChangeResultColor = new System.Windows.Forms.Button();
			this.lblclrActiveColor = new System.Windows.Forms.Label();
			this.btnChangeActiveColor = new System.Windows.Forms.Button();
			this.lblActiveColorLabel = new System.Windows.Forms.Label();
			this.lblclrDotDotColor = new System.Windows.Forms.Label();
			this.lblclrEndVectorColor = new System.Windows.Forms.Label();
			this.lblclrStartVectorColor = new System.Windows.Forms.Label();
			this.lblclrBackColor1 = new System.Windows.Forms.Label();
			this.BackColorLabel = new System.Windows.Forms.Label();
			this.lblMovingVectorColor = new System.Windows.Forms.Label();
			this.btnChangeDotDotVectors = new System.Windows.Forms.Button();
			this.StartVectorColorLabel = new System.Windows.Forms.Label();
			this.ChangeEndColor = new System.Windows.Forms.Button();
			this.EndVectorColorLabel = new System.Windows.Forms.Label();
			this.ChangeHeaderColor = new System.Windows.Forms.Button();
			this.ChangeBackColor = new System.Windows.Forms.Button();
			this.ChaneGridColor = new System.Windows.Forms.Button();
			this.GridColorLabel = new System.Windows.Forms.Label();
			this.imgList = new System.Windows.Forms.ImageList(this.components);
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnClearAllVector = new System.Windows.Forms.Button();
			this.grpPreview = new System.Windows.Forms.GroupBox();
			this.tabControl.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trcGeometry)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trcGridHeightSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trcGridWidthSize)).BeginInit();
			this.tabColor.SuspendLayout();
			
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.AccessibleDescription = ((string)(resources.GetObject("tabControl.AccessibleDescription")));
			this.tabControl.AccessibleName = ((string)(resources.GetObject("tabControl.AccessibleName")));
			this.tabControl.Alignment = ((System.Windows.Forms.TabAlignment)(resources.GetObject("tabControl.Alignment")));
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabControl.Anchor")));
			this.tabControl.Appearance = ((System.Windows.Forms.TabAppearance)(resources.GetObject("tabControl.Appearance")));
			this.tabControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabControl.BackgroundImage")));
			this.tabControl.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.tabGeneral,
																					 this.tabColor});
			this.tabControl.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabControl.Dock")));
			this.tabControl.Enabled = ((bool)(resources.GetObject("tabControl.Enabled")));
			this.tabControl.Font = ((System.Drawing.Font)(resources.GetObject("tabControl.Font")));
			this.tabControl.ImageList = this.imgList;
			this.tabControl.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabControl.ImeMode")));
			this.tabControl.ItemSize = ((System.Drawing.Size)(resources.GetObject("tabControl.ItemSize")));
			this.tabControl.Location = ((System.Drawing.Point)(resources.GetObject("tabControl.Location")));
			this.tabControl.Name = "tabControl";
			this.tabControl.Padding = ((System.Drawing.Point)(resources.GetObject("tabControl.Padding")));
			this.tabControl.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabControl.RightToLeft")));
			this.tabControl.SelectedIndex = 0;
			this.tabControl.ShowToolTips = ((bool)(resources.GetObject("tabControl.ShowToolTips")));
			this.tabControl.Size = ((System.Drawing.Size)(resources.GetObject("tabControl.Size")));
			this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl.TabIndex = ((int)(resources.GetObject("tabControl.TabIndex")));
			this.tabControl.Text = resources.GetString("tabControl.Text");
			this.tabControl.Visible = ((bool)(resources.GetObject("tabControl.Visible")));
			// 
			// tabGeneral
			// 
			this.tabGeneral.AccessibleDescription = ((string)(resources.GetObject("tabGeneral.AccessibleDescription")));
			this.tabGeneral.AccessibleName = ((string)(resources.GetObject("tabGeneral.AccessibleName")));
			this.tabGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabGeneral.Anchor")));
			this.tabGeneral.AutoScroll = ((bool)(resources.GetObject("tabGeneral.AutoScroll")));
			this.tabGeneral.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabGeneral.AutoScrollMargin")));
			this.tabGeneral.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabGeneral.AutoScrollMinSize")));
			this.tabGeneral.BackColor = System.Drawing.SystemColors.Control;
			this.tabGeneral.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabGeneral.BackgroundImage")));
			this.tabGeneral.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.trcGeometry,
																					 this.ckbShowName,
																					 this.ckbStartupMode,
																					 this.trcGridHeightSize,
																					 this.trcGridWidthSize,
																					 this.ckbQuestionBeforClose,
																					 this.ckbQuestionBeforDeleteAll,
																					 this.ckbShowNotifyIcon,
																					 this.ckbShowGrid,
																					 this.lblGridWidthSizeLabel,
																					 this.lblGridHeightSizeLabel,
																					 this.lblGeometry});
			this.tabGeneral.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabGeneral.Dock")));
			this.tabGeneral.Enabled = ((bool)(resources.GetObject("tabGeneral.Enabled")));
			this.tabGeneral.Font = ((System.Drawing.Font)(resources.GetObject("tabGeneral.Font")));
			this.tabGeneral.ImageIndex = ((int)(resources.GetObject("tabGeneral.ImageIndex")));
			this.tabGeneral.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabGeneral.ImeMode")));
			this.tabGeneral.Location = ((System.Drawing.Point)(resources.GetObject("tabGeneral.Location")));
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabGeneral.RightToLeft")));
			this.tabGeneral.Size = ((System.Drawing.Size)(resources.GetObject("tabGeneral.Size")));
			this.tabGeneral.TabIndex = ((int)(resources.GetObject("tabGeneral.TabIndex")));
			this.tabGeneral.Text = resources.GetString("tabGeneral.Text");
			this.tabGeneral.ToolTipText = resources.GetString("tabGeneral.ToolTipText");
			this.tabGeneral.Visible = ((bool)(resources.GetObject("tabGeneral.Visible")));
			// 
			// trcGeometry
			// 
			this.trcGeometry.AccessibleDescription = ((string)(resources.GetObject("trcGeometry.AccessibleDescription")));
			this.trcGeometry.AccessibleName = ((string)(resources.GetObject("trcGeometry.AccessibleName")));
			this.trcGeometry.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("trcGeometry.Anchor")));
			this.trcGeometry.BackColor = System.Drawing.SystemColors.Control;
			this.trcGeometry.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("trcGeometry.BackgroundImage")));
			this.trcGeometry.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("trcGeometry.Dock")));
			this.trcGeometry.Enabled = ((bool)(resources.GetObject("trcGeometry.Enabled")));
			this.trcGeometry.Font = ((System.Drawing.Font)(resources.GetObject("trcGeometry.Font")));
			this.trcGeometry.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("trcGeometry.ImeMode")));
			this.trcGeometry.Location = ((System.Drawing.Point)(resources.GetObject("trcGeometry.Location")));
			this.trcGeometry.Minimum = 1;
			this.trcGeometry.Name = "trcGeometry";
			this.trcGeometry.Orientation = ((System.Windows.Forms.Orientation)(resources.GetObject("trcGeometry.Orientation")));
			this.trcGeometry.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("trcGeometry.RightToLeft")));
			this.trcGeometry.Size = ((System.Drawing.Size)(resources.GetObject("trcGeometry.Size")));
			this.trcGeometry.TabIndex = ((int)(resources.GetObject("trcGeometry.TabIndex")));
			this.trcGeometry.Text = resources.GetString("trcGeometry.Text");
			this.trcGeometry.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trcGeometry.Value = 1;
			this.trcGeometry.Visible = ((bool)(resources.GetObject("trcGeometry.Visible")));
			this.trcGeometry.Scroll += new System.EventHandler(this.trcGeometry_Scroll);
			// 
			// ckbShowName
			// 
			this.ckbShowName.AccessibleDescription = ((string)(resources.GetObject("ckbShowName.AccessibleDescription")));
			this.ckbShowName.AccessibleName = ((string)(resources.GetObject("ckbShowName.AccessibleName")));
			this.ckbShowName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ckbShowName.Anchor")));
			this.ckbShowName.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("ckbShowName.Appearance")));
			this.ckbShowName.BackColor = System.Drawing.Color.Transparent;
			this.ckbShowName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ckbShowName.BackgroundImage")));
			this.ckbShowName.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbShowName.CheckAlign")));
			this.ckbShowName.Checked = true;
			this.ckbShowName.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckbShowName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ckbShowName.Dock")));
			this.ckbShowName.Enabled = ((bool)(resources.GetObject("ckbShowName.Enabled")));
			this.ckbShowName.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ckbShowName.FlatStyle")));
			this.ckbShowName.Font = ((System.Drawing.Font)(resources.GetObject("ckbShowName.Font")));
			this.ckbShowName.Image = ((System.Drawing.Image)(resources.GetObject("ckbShowName.Image")));
			this.ckbShowName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbShowName.ImageAlign")));
			this.ckbShowName.ImageIndex = ((int)(resources.GetObject("ckbShowName.ImageIndex")));
			this.ckbShowName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ckbShowName.ImeMode")));
			this.ckbShowName.Location = ((System.Drawing.Point)(resources.GetObject("ckbShowName.Location")));
			this.ckbShowName.Name = "ckbShowName";
			this.ckbShowName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ckbShowName.RightToLeft")));
			this.ckbShowName.Size = ((System.Drawing.Size)(resources.GetObject("ckbShowName.Size")));
			this.ckbShowName.TabIndex = ((int)(resources.GetObject("ckbShowName.TabIndex")));
			this.ckbShowName.Text = resources.GetString("ckbShowName.Text");
			this.ckbShowName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbShowName.TextAlign")));
			this.ckbShowName.Visible = ((bool)(resources.GetObject("ckbShowName.Visible")));
			this.ckbShowName.CheckedChanged += new System.EventHandler(this.ckbShowName_CheckedChanged);
			// 
			// ckbStartupMode
			// 
			this.ckbStartupMode.AccessibleDescription = ((string)(resources.GetObject("ckbStartupMode.AccessibleDescription")));
			this.ckbStartupMode.AccessibleName = ((string)(resources.GetObject("ckbStartupMode.AccessibleName")));
			this.ckbStartupMode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ckbStartupMode.Anchor")));
			this.ckbStartupMode.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("ckbStartupMode.Appearance")));
			this.ckbStartupMode.BackColor = System.Drawing.Color.Transparent;
			this.ckbStartupMode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ckbStartupMode.BackgroundImage")));
			this.ckbStartupMode.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbStartupMode.CheckAlign")));
			this.ckbStartupMode.Checked = true;
			this.ckbStartupMode.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckbStartupMode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ckbStartupMode.Dock")));
			this.ckbStartupMode.Enabled = ((bool)(resources.GetObject("ckbStartupMode.Enabled")));
			this.ckbStartupMode.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ckbStartupMode.FlatStyle")));
			this.ckbStartupMode.Font = ((System.Drawing.Font)(resources.GetObject("ckbStartupMode.Font")));
			this.ckbStartupMode.Image = ((System.Drawing.Image)(resources.GetObject("ckbStartupMode.Image")));
			this.ckbStartupMode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbStartupMode.ImageAlign")));
			this.ckbStartupMode.ImageIndex = ((int)(resources.GetObject("ckbStartupMode.ImageIndex")));
			this.ckbStartupMode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ckbStartupMode.ImeMode")));
			this.ckbStartupMode.Location = ((System.Drawing.Point)(resources.GetObject("ckbStartupMode.Location")));
			this.ckbStartupMode.Name = "ckbStartupMode";
			this.ckbStartupMode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ckbStartupMode.RightToLeft")));
			this.ckbStartupMode.Size = ((System.Drawing.Size)(resources.GetObject("ckbStartupMode.Size")));
			this.ckbStartupMode.TabIndex = ((int)(resources.GetObject("ckbStartupMode.TabIndex")));
			this.ckbStartupMode.Text = resources.GetString("ckbStartupMode.Text");
			this.ckbStartupMode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbStartupMode.TextAlign")));
			this.ckbStartupMode.Visible = ((bool)(resources.GetObject("ckbStartupMode.Visible")));
			// 
			// trcGridHeightSize
			// 
			this.trcGridHeightSize.AccessibleDescription = ((string)(resources.GetObject("trcGridHeightSize.AccessibleDescription")));
			this.trcGridHeightSize.AccessibleName = ((string)(resources.GetObject("trcGridHeightSize.AccessibleName")));
			this.trcGridHeightSize.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("trcGridHeightSize.Anchor")));
			this.trcGridHeightSize.BackColor = System.Drawing.SystemColors.Control;
			this.trcGridHeightSize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("trcGridHeightSize.BackgroundImage")));
			this.trcGridHeightSize.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("trcGridHeightSize.Dock")));
			this.trcGridHeightSize.Enabled = ((bool)(resources.GetObject("trcGridHeightSize.Enabled")));
			this.trcGridHeightSize.Font = ((System.Drawing.Font)(resources.GetObject("trcGridHeightSize.Font")));
			this.trcGridHeightSize.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("trcGridHeightSize.ImeMode")));
			this.trcGridHeightSize.Location = ((System.Drawing.Point)(resources.GetObject("trcGridHeightSize.Location")));
			this.trcGridHeightSize.Maximum = 200;
			this.trcGridHeightSize.Minimum = 20;
			this.trcGridHeightSize.Name = "trcGridHeightSize";
			this.trcGridHeightSize.Orientation = ((System.Windows.Forms.Orientation)(resources.GetObject("trcGridHeightSize.Orientation")));
			this.trcGridHeightSize.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("trcGridHeightSize.RightToLeft")));
			this.trcGridHeightSize.Size = ((System.Drawing.Size)(resources.GetObject("trcGridHeightSize.Size")));
			this.trcGridHeightSize.TabIndex = ((int)(resources.GetObject("trcGridHeightSize.TabIndex")));
			this.trcGridHeightSize.Text = resources.GetString("trcGridHeightSize.Text");
			this.trcGridHeightSize.TickFrequency = 10;
			this.trcGridHeightSize.Value = 20;
			this.trcGridHeightSize.Visible = ((bool)(resources.GetObject("trcGridHeightSize.Visible")));
			this.trcGridHeightSize.Scroll += new System.EventHandler(this.trcGridHeightSize_Scroll_1);
			// 
			// trcGridWidthSize
			// 
			this.trcGridWidthSize.AccessibleDescription = ((string)(resources.GetObject("trcGridWidthSize.AccessibleDescription")));
			this.trcGridWidthSize.AccessibleName = ((string)(resources.GetObject("trcGridWidthSize.AccessibleName")));
			this.trcGridWidthSize.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("trcGridWidthSize.Anchor")));
			this.trcGridWidthSize.BackColor = System.Drawing.SystemColors.Control;
			this.trcGridWidthSize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("trcGridWidthSize.BackgroundImage")));
			this.trcGridWidthSize.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("trcGridWidthSize.Dock")));
			this.trcGridWidthSize.Enabled = ((bool)(resources.GetObject("trcGridWidthSize.Enabled")));
			this.trcGridWidthSize.Font = ((System.Drawing.Font)(resources.GetObject("trcGridWidthSize.Font")));
			this.trcGridWidthSize.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("trcGridWidthSize.ImeMode")));
			this.trcGridWidthSize.Location = ((System.Drawing.Point)(resources.GetObject("trcGridWidthSize.Location")));
			this.trcGridWidthSize.Maximum = 200;
			this.trcGridWidthSize.Minimum = 20;
			this.trcGridWidthSize.Name = "trcGridWidthSize";
			this.trcGridWidthSize.Orientation = ((System.Windows.Forms.Orientation)(resources.GetObject("trcGridWidthSize.Orientation")));
			this.trcGridWidthSize.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("trcGridWidthSize.RightToLeft")));
			this.trcGridWidthSize.Size = ((System.Drawing.Size)(resources.GetObject("trcGridWidthSize.Size")));
			this.trcGridWidthSize.TabIndex = ((int)(resources.GetObject("trcGridWidthSize.TabIndex")));
			this.trcGridWidthSize.Text = resources.GetString("trcGridWidthSize.Text");
			this.trcGridWidthSize.TickFrequency = 10;
			this.trcGridWidthSize.Value = 20;
			this.trcGridWidthSize.Visible = ((bool)(resources.GetObject("trcGridWidthSize.Visible")));
			this.trcGridWidthSize.Scroll += new System.EventHandler(this.trcGridWidthSize_Scroll_1);
			// 
			// ckbQuestionBeforClose
			// 
			this.ckbQuestionBeforClose.AccessibleDescription = ((string)(resources.GetObject("ckbQuestionBeforClose.AccessibleDescription")));
			this.ckbQuestionBeforClose.AccessibleName = ((string)(resources.GetObject("ckbQuestionBeforClose.AccessibleName")));
			this.ckbQuestionBeforClose.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ckbQuestionBeforClose.Anchor")));
			this.ckbQuestionBeforClose.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("ckbQuestionBeforClose.Appearance")));
			this.ckbQuestionBeforClose.BackColor = System.Drawing.Color.Transparent;
			this.ckbQuestionBeforClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ckbQuestionBeforClose.BackgroundImage")));
			this.ckbQuestionBeforClose.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbQuestionBeforClose.CheckAlign")));
			this.ckbQuestionBeforClose.Checked = true;
			this.ckbQuestionBeforClose.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckbQuestionBeforClose.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ckbQuestionBeforClose.Dock")));
			this.ckbQuestionBeforClose.Enabled = ((bool)(resources.GetObject("ckbQuestionBeforClose.Enabled")));
			this.ckbQuestionBeforClose.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ckbQuestionBeforClose.FlatStyle")));
			this.ckbQuestionBeforClose.Font = ((System.Drawing.Font)(resources.GetObject("ckbQuestionBeforClose.Font")));
			this.ckbQuestionBeforClose.Image = ((System.Drawing.Image)(resources.GetObject("ckbQuestionBeforClose.Image")));
			this.ckbQuestionBeforClose.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbQuestionBeforClose.ImageAlign")));
			this.ckbQuestionBeforClose.ImageIndex = ((int)(resources.GetObject("ckbQuestionBeforClose.ImageIndex")));
			this.ckbQuestionBeforClose.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ckbQuestionBeforClose.ImeMode")));
			this.ckbQuestionBeforClose.Location = ((System.Drawing.Point)(resources.GetObject("ckbQuestionBeforClose.Location")));
			this.ckbQuestionBeforClose.Name = "ckbQuestionBeforClose";
			this.ckbQuestionBeforClose.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ckbQuestionBeforClose.RightToLeft")));
			this.ckbQuestionBeforClose.Size = ((System.Drawing.Size)(resources.GetObject("ckbQuestionBeforClose.Size")));
			this.ckbQuestionBeforClose.TabIndex = ((int)(resources.GetObject("ckbQuestionBeforClose.TabIndex")));
			this.ckbQuestionBeforClose.Text = resources.GetString("ckbQuestionBeforClose.Text");
			this.ckbQuestionBeforClose.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbQuestionBeforClose.TextAlign")));
			this.ckbQuestionBeforClose.Visible = ((bool)(resources.GetObject("ckbQuestionBeforClose.Visible")));
			// 
			// ckbQuestionBeforDeleteAll
			// 
			this.ckbQuestionBeforDeleteAll.AccessibleDescription = ((string)(resources.GetObject("ckbQuestionBeforDeleteAll.AccessibleDescription")));
			this.ckbQuestionBeforDeleteAll.AccessibleName = ((string)(resources.GetObject("ckbQuestionBeforDeleteAll.AccessibleName")));
			this.ckbQuestionBeforDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ckbQuestionBeforDeleteAll.Anchor")));
			this.ckbQuestionBeforDeleteAll.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("ckbQuestionBeforDeleteAll.Appearance")));
			this.ckbQuestionBeforDeleteAll.BackColor = System.Drawing.Color.Transparent;
			this.ckbQuestionBeforDeleteAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ckbQuestionBeforDeleteAll.BackgroundImage")));
			this.ckbQuestionBeforDeleteAll.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbQuestionBeforDeleteAll.CheckAlign")));
			this.ckbQuestionBeforDeleteAll.Checked = true;
			this.ckbQuestionBeforDeleteAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckbQuestionBeforDeleteAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ckbQuestionBeforDeleteAll.Dock")));
			this.ckbQuestionBeforDeleteAll.Enabled = ((bool)(resources.GetObject("ckbQuestionBeforDeleteAll.Enabled")));
			this.ckbQuestionBeforDeleteAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ckbQuestionBeforDeleteAll.FlatStyle")));
			this.ckbQuestionBeforDeleteAll.Font = ((System.Drawing.Font)(resources.GetObject("ckbQuestionBeforDeleteAll.Font")));
			this.ckbQuestionBeforDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("ckbQuestionBeforDeleteAll.Image")));
			this.ckbQuestionBeforDeleteAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbQuestionBeforDeleteAll.ImageAlign")));
			this.ckbQuestionBeforDeleteAll.ImageIndex = ((int)(resources.GetObject("ckbQuestionBeforDeleteAll.ImageIndex")));
			this.ckbQuestionBeforDeleteAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ckbQuestionBeforDeleteAll.ImeMode")));
			this.ckbQuestionBeforDeleteAll.Location = ((System.Drawing.Point)(resources.GetObject("ckbQuestionBeforDeleteAll.Location")));
			this.ckbQuestionBeforDeleteAll.Name = "ckbQuestionBeforDeleteAll";
			this.ckbQuestionBeforDeleteAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ckbQuestionBeforDeleteAll.RightToLeft")));
			this.ckbQuestionBeforDeleteAll.Size = ((System.Drawing.Size)(resources.GetObject("ckbQuestionBeforDeleteAll.Size")));
			this.ckbQuestionBeforDeleteAll.TabIndex = ((int)(resources.GetObject("ckbQuestionBeforDeleteAll.TabIndex")));
			this.ckbQuestionBeforDeleteAll.Text = resources.GetString("ckbQuestionBeforDeleteAll.Text");
			this.ckbQuestionBeforDeleteAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbQuestionBeforDeleteAll.TextAlign")));
			this.ckbQuestionBeforDeleteAll.Visible = ((bool)(resources.GetObject("ckbQuestionBeforDeleteAll.Visible")));
			// 
			// ckbShowNotifyIcon
			// 
			this.ckbShowNotifyIcon.AccessibleDescription = ((string)(resources.GetObject("ckbShowNotifyIcon.AccessibleDescription")));
			this.ckbShowNotifyIcon.AccessibleName = ((string)(resources.GetObject("ckbShowNotifyIcon.AccessibleName")));
			this.ckbShowNotifyIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ckbShowNotifyIcon.Anchor")));
			this.ckbShowNotifyIcon.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("ckbShowNotifyIcon.Appearance")));
			this.ckbShowNotifyIcon.BackColor = System.Drawing.Color.Transparent;
			this.ckbShowNotifyIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ckbShowNotifyIcon.BackgroundImage")));
			this.ckbShowNotifyIcon.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbShowNotifyIcon.CheckAlign")));
			this.ckbShowNotifyIcon.Checked = true;
			this.ckbShowNotifyIcon.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckbShowNotifyIcon.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ckbShowNotifyIcon.Dock")));
			this.ckbShowNotifyIcon.Enabled = ((bool)(resources.GetObject("ckbShowNotifyIcon.Enabled")));
			this.ckbShowNotifyIcon.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ckbShowNotifyIcon.FlatStyle")));
			this.ckbShowNotifyIcon.Font = ((System.Drawing.Font)(resources.GetObject("ckbShowNotifyIcon.Font")));
			this.ckbShowNotifyIcon.Image = ((System.Drawing.Image)(resources.GetObject("ckbShowNotifyIcon.Image")));
			this.ckbShowNotifyIcon.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbShowNotifyIcon.ImageAlign")));
			this.ckbShowNotifyIcon.ImageIndex = ((int)(resources.GetObject("ckbShowNotifyIcon.ImageIndex")));
			this.ckbShowNotifyIcon.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ckbShowNotifyIcon.ImeMode")));
			this.ckbShowNotifyIcon.Location = ((System.Drawing.Point)(resources.GetObject("ckbShowNotifyIcon.Location")));
			this.ckbShowNotifyIcon.Name = "ckbShowNotifyIcon";
			this.ckbShowNotifyIcon.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ckbShowNotifyIcon.RightToLeft")));
			this.ckbShowNotifyIcon.Size = ((System.Drawing.Size)(resources.GetObject("ckbShowNotifyIcon.Size")));
			this.ckbShowNotifyIcon.TabIndex = ((int)(resources.GetObject("ckbShowNotifyIcon.TabIndex")));
			this.ckbShowNotifyIcon.Text = resources.GetString("ckbShowNotifyIcon.Text");
			this.ckbShowNotifyIcon.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbShowNotifyIcon.TextAlign")));
			this.ckbShowNotifyIcon.Visible = ((bool)(resources.GetObject("ckbShowNotifyIcon.Visible")));
			// 
			// ckbShowGrid
			// 
			this.ckbShowGrid.AccessibleDescription = ((string)(resources.GetObject("ckbShowGrid.AccessibleDescription")));
			this.ckbShowGrid.AccessibleName = ((string)(resources.GetObject("ckbShowGrid.AccessibleName")));
			this.ckbShowGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ckbShowGrid.Anchor")));
			this.ckbShowGrid.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("ckbShowGrid.Appearance")));
			this.ckbShowGrid.BackColor = System.Drawing.Color.Transparent;
			this.ckbShowGrid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ckbShowGrid.BackgroundImage")));
			this.ckbShowGrid.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbShowGrid.CheckAlign")));
			this.ckbShowGrid.Checked = true;
			this.ckbShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckbShowGrid.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ckbShowGrid.Dock")));
			this.ckbShowGrid.Enabled = ((bool)(resources.GetObject("ckbShowGrid.Enabled")));
			this.ckbShowGrid.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ckbShowGrid.FlatStyle")));
			this.ckbShowGrid.Font = ((System.Drawing.Font)(resources.GetObject("ckbShowGrid.Font")));
			this.ckbShowGrid.Image = ((System.Drawing.Image)(resources.GetObject("ckbShowGrid.Image")));
			this.ckbShowGrid.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbShowGrid.ImageAlign")));
			this.ckbShowGrid.ImageIndex = ((int)(resources.GetObject("ckbShowGrid.ImageIndex")));
			this.ckbShowGrid.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ckbShowGrid.ImeMode")));
			this.ckbShowGrid.Location = ((System.Drawing.Point)(resources.GetObject("ckbShowGrid.Location")));
			this.ckbShowGrid.Name = "ckbShowGrid";
			this.ckbShowGrid.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ckbShowGrid.RightToLeft")));
			this.ckbShowGrid.Size = ((System.Drawing.Size)(resources.GetObject("ckbShowGrid.Size")));
			this.ckbShowGrid.TabIndex = ((int)(resources.GetObject("ckbShowGrid.TabIndex")));
			this.ckbShowGrid.Text = resources.GetString("ckbShowGrid.Text");
			this.ckbShowGrid.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbShowGrid.TextAlign")));
			this.ckbShowGrid.Visible = ((bool)(resources.GetObject("ckbShowGrid.Visible")));
			this.ckbShowGrid.CheckedChanged += new System.EventHandler(this.ckbShowGrid_CheckedChanged);
			// 
			// lblGridWidthSizeLabel
			// 
			this.lblGridWidthSizeLabel.AccessibleDescription = ((string)(resources.GetObject("lblGridWidthSizeLabel.AccessibleDescription")));
			this.lblGridWidthSizeLabel.AccessibleName = ((string)(resources.GetObject("lblGridWidthSizeLabel.AccessibleName")));
			this.lblGridWidthSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblGridWidthSizeLabel.Anchor")));
			this.lblGridWidthSizeLabel.AutoSize = ((bool)(resources.GetObject("lblGridWidthSizeLabel.AutoSize")));
			this.lblGridWidthSizeLabel.BackColor = System.Drawing.Color.Transparent;
			this.lblGridWidthSizeLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblGridWidthSizeLabel.Dock")));
			this.lblGridWidthSizeLabel.Enabled = ((bool)(resources.GetObject("lblGridWidthSizeLabel.Enabled")));
			this.lblGridWidthSizeLabel.Font = ((System.Drawing.Font)(resources.GetObject("lblGridWidthSizeLabel.Font")));
			this.lblGridWidthSizeLabel.Image = ((System.Drawing.Image)(resources.GetObject("lblGridWidthSizeLabel.Image")));
			this.lblGridWidthSizeLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGridWidthSizeLabel.ImageAlign")));
			this.lblGridWidthSizeLabel.ImageIndex = ((int)(resources.GetObject("lblGridWidthSizeLabel.ImageIndex")));
			this.lblGridWidthSizeLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblGridWidthSizeLabel.ImeMode")));
			this.lblGridWidthSizeLabel.Location = ((System.Drawing.Point)(resources.GetObject("lblGridWidthSizeLabel.Location")));
			this.lblGridWidthSizeLabel.Name = "lblGridWidthSizeLabel";
			this.lblGridWidthSizeLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblGridWidthSizeLabel.RightToLeft")));
			this.lblGridWidthSizeLabel.Size = ((System.Drawing.Size)(resources.GetObject("lblGridWidthSizeLabel.Size")));
			this.lblGridWidthSizeLabel.TabIndex = ((int)(resources.GetObject("lblGridWidthSizeLabel.TabIndex")));
			this.lblGridWidthSizeLabel.Text = resources.GetString("lblGridWidthSizeLabel.Text");
			this.lblGridWidthSizeLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGridWidthSizeLabel.TextAlign")));
			this.lblGridWidthSizeLabel.Visible = ((bool)(resources.GetObject("lblGridWidthSizeLabel.Visible")));
			// 
			// lblGridHeightSizeLabel
			// 
			this.lblGridHeightSizeLabel.AccessibleDescription = ((string)(resources.GetObject("lblGridHeightSizeLabel.AccessibleDescription")));
			this.lblGridHeightSizeLabel.AccessibleName = ((string)(resources.GetObject("lblGridHeightSizeLabel.AccessibleName")));
			this.lblGridHeightSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblGridHeightSizeLabel.Anchor")));
			this.lblGridHeightSizeLabel.AutoSize = ((bool)(resources.GetObject("lblGridHeightSizeLabel.AutoSize")));
			this.lblGridHeightSizeLabel.BackColor = System.Drawing.Color.Transparent;
			this.lblGridHeightSizeLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblGridHeightSizeLabel.Dock")));
			this.lblGridHeightSizeLabel.Enabled = ((bool)(resources.GetObject("lblGridHeightSizeLabel.Enabled")));
			this.lblGridHeightSizeLabel.Font = ((System.Drawing.Font)(resources.GetObject("lblGridHeightSizeLabel.Font")));
			this.lblGridHeightSizeLabel.Image = ((System.Drawing.Image)(resources.GetObject("lblGridHeightSizeLabel.Image")));
			this.lblGridHeightSizeLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGridHeightSizeLabel.ImageAlign")));
			this.lblGridHeightSizeLabel.ImageIndex = ((int)(resources.GetObject("lblGridHeightSizeLabel.ImageIndex")));
			this.lblGridHeightSizeLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblGridHeightSizeLabel.ImeMode")));
			this.lblGridHeightSizeLabel.Location = ((System.Drawing.Point)(resources.GetObject("lblGridHeightSizeLabel.Location")));
			this.lblGridHeightSizeLabel.Name = "lblGridHeightSizeLabel";
			this.lblGridHeightSizeLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblGridHeightSizeLabel.RightToLeft")));
			this.lblGridHeightSizeLabel.Size = ((System.Drawing.Size)(resources.GetObject("lblGridHeightSizeLabel.Size")));
			this.lblGridHeightSizeLabel.TabIndex = ((int)(resources.GetObject("lblGridHeightSizeLabel.TabIndex")));
			this.lblGridHeightSizeLabel.Text = resources.GetString("lblGridHeightSizeLabel.Text");
			this.lblGridHeightSizeLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGridHeightSizeLabel.TextAlign")));
			this.lblGridHeightSizeLabel.Visible = ((bool)(resources.GetObject("lblGridHeightSizeLabel.Visible")));
			// 
			// lblGeometry
			// 
			this.lblGeometry.AccessibleDescription = ((string)(resources.GetObject("lblGeometry.AccessibleDescription")));
			this.lblGeometry.AccessibleName = ((string)(resources.GetObject("lblGeometry.AccessibleName")));
			this.lblGeometry.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblGeometry.Anchor")));
			this.lblGeometry.AutoSize = ((bool)(resources.GetObject("lblGeometry.AutoSize")));
			this.lblGeometry.BackColor = System.Drawing.Color.Transparent;
			this.lblGeometry.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblGeometry.Dock")));
			this.lblGeometry.Enabled = ((bool)(resources.GetObject("lblGeometry.Enabled")));
			this.lblGeometry.Font = ((System.Drawing.Font)(resources.GetObject("lblGeometry.Font")));
			this.lblGeometry.Image = ((System.Drawing.Image)(resources.GetObject("lblGeometry.Image")));
			this.lblGeometry.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGeometry.ImageAlign")));
			this.lblGeometry.ImageIndex = ((int)(resources.GetObject("lblGeometry.ImageIndex")));
			this.lblGeometry.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblGeometry.ImeMode")));
			this.lblGeometry.Location = ((System.Drawing.Point)(resources.GetObject("lblGeometry.Location")));
			this.lblGeometry.Name = "lblGeometry";
			this.lblGeometry.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblGeometry.RightToLeft")));
			this.lblGeometry.Size = ((System.Drawing.Size)(resources.GetObject("lblGeometry.Size")));
			this.lblGeometry.TabIndex = ((int)(resources.GetObject("lblGeometry.TabIndex")));
			this.lblGeometry.Text = resources.GetString("lblGeometry.Text");
			this.lblGeometry.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGeometry.TextAlign")));
			this.lblGeometry.Visible = ((bool)(resources.GetObject("lblGeometry.Visible")));
			// 
			// tabColor
			// 
			this.tabColor.AccessibleDescription = ((string)(resources.GetObject("tabColor.AccessibleDescription")));
			this.tabColor.AccessibleName = ((string)(resources.GetObject("tabColor.AccessibleName")));
			this.tabColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabColor.Anchor")));
			this.tabColor.AutoScroll = ((bool)(resources.GetObject("tabColor.AutoScroll")));
			this.tabColor.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabColor.AutoScrollMargin")));
			this.tabColor.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabColor.AutoScrollMinSize")));
			this.tabColor.BackColor = System.Drawing.Color.Transparent;
			this.tabColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabColor.BackgroundImage")));
			this.tabColor.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.lblclrGridColor,
																				   this.lblclrResult,
																				   this.lblResultLabel,
																				   this.btnChangeResultColor,
																				   this.lblclrActiveColor,
																				   this.btnChangeActiveColor,
																				   this.lblActiveColorLabel,
																				   this.lblclrDotDotColor,
																				   this.lblclrEndVectorColor,
																				   this.lblclrStartVectorColor,
																				   this.lblclrBackColor1,
																				   this.BackColorLabel,
																				   this.lblMovingVectorColor,
																				   this.btnChangeDotDotVectors,
																				   this.StartVectorColorLabel,
																				   this.ChangeEndColor,
																				   this.EndVectorColorLabel,
																				   this.ChangeHeaderColor,
																				   this.ChangeBackColor,
																				   this.ChaneGridColor,
																				   this.GridColorLabel});
			this.tabColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabColor.Dock")));
			this.tabColor.Enabled = ((bool)(resources.GetObject("tabColor.Enabled")));
			this.tabColor.Font = ((System.Drawing.Font)(resources.GetObject("tabColor.Font")));
			this.tabColor.ImageIndex = ((int)(resources.GetObject("tabColor.ImageIndex")));
			this.tabColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabColor.ImeMode")));
			this.tabColor.Location = ((System.Drawing.Point)(resources.GetObject("tabColor.Location")));
			this.tabColor.Name = "tabColor";
			this.tabColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabColor.RightToLeft")));
			this.tabColor.Size = ((System.Drawing.Size)(resources.GetObject("tabColor.Size")));
			this.tabColor.TabIndex = ((int)(resources.GetObject("tabColor.TabIndex")));
			this.tabColor.Text = resources.GetString("tabColor.Text");
			this.tabColor.ToolTipText = resources.GetString("tabColor.ToolTipText");
			this.tabColor.Visible = ((bool)(resources.GetObject("tabColor.Visible")));
			// 
			// lblclrGridColor
			// 
			this.lblclrGridColor.AccessibleDescription = ((string)(resources.GetObject("lblclrGridColor.AccessibleDescription")));
			this.lblclrGridColor.AccessibleName = ((string)(resources.GetObject("lblclrGridColor.AccessibleName")));
			this.lblclrGridColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblclrGridColor.Anchor")));
			this.lblclrGridColor.AutoSize = ((bool)(resources.GetObject("lblclrGridColor.AutoSize")));
			this.lblclrGridColor.BackColor = System.Drawing.Color.Silver;
			this.lblclrGridColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblclrGridColor.Dock")));
			this.lblclrGridColor.Enabled = ((bool)(resources.GetObject("lblclrGridColor.Enabled")));
			this.lblclrGridColor.Font = ((System.Drawing.Font)(resources.GetObject("lblclrGridColor.Font")));
			this.lblclrGridColor.Image = ((System.Drawing.Image)(resources.GetObject("lblclrGridColor.Image")));
			this.lblclrGridColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrGridColor.ImageAlign")));
			this.lblclrGridColor.ImageIndex = ((int)(resources.GetObject("lblclrGridColor.ImageIndex")));
			this.lblclrGridColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblclrGridColor.ImeMode")));
			this.lblclrGridColor.Location = ((System.Drawing.Point)(resources.GetObject("lblclrGridColor.Location")));
			this.lblclrGridColor.Name = "lblclrGridColor";
			this.lblclrGridColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblclrGridColor.RightToLeft")));
			this.lblclrGridColor.Size = ((System.Drawing.Size)(resources.GetObject("lblclrGridColor.Size")));
			this.lblclrGridColor.TabIndex = ((int)(resources.GetObject("lblclrGridColor.TabIndex")));
			this.lblclrGridColor.Text = resources.GetString("lblclrGridColor.Text");
			this.lblclrGridColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrGridColor.TextAlign")));
			this.lblclrGridColor.Visible = ((bool)(resources.GetObject("lblclrGridColor.Visible")));
			// 
			// lblclrResult
			// 
			this.lblclrResult.AccessibleDescription = ((string)(resources.GetObject("lblclrResult.AccessibleDescription")));
			this.lblclrResult.AccessibleName = ((string)(resources.GetObject("lblclrResult.AccessibleName")));
			this.lblclrResult.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblclrResult.Anchor")));
			this.lblclrResult.AutoSize = ((bool)(resources.GetObject("lblclrResult.AutoSize")));
			this.lblclrResult.BackColor = System.Drawing.Color.Olive;
			this.lblclrResult.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblclrResult.Dock")));
			this.lblclrResult.Enabled = ((bool)(resources.GetObject("lblclrResult.Enabled")));
			this.lblclrResult.Font = ((System.Drawing.Font)(resources.GetObject("lblclrResult.Font")));
			this.lblclrResult.Image = ((System.Drawing.Image)(resources.GetObject("lblclrResult.Image")));
			this.lblclrResult.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrResult.ImageAlign")));
			this.lblclrResult.ImageIndex = ((int)(resources.GetObject("lblclrResult.ImageIndex")));
			this.lblclrResult.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblclrResult.ImeMode")));
			this.lblclrResult.Location = ((System.Drawing.Point)(resources.GetObject("lblclrResult.Location")));
			this.lblclrResult.Name = "lblclrResult";
			this.lblclrResult.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblclrResult.RightToLeft")));
			this.lblclrResult.Size = ((System.Drawing.Size)(resources.GetObject("lblclrResult.Size")));
			this.lblclrResult.TabIndex = ((int)(resources.GetObject("lblclrResult.TabIndex")));
			this.lblclrResult.Text = resources.GetString("lblclrResult.Text");
			this.lblclrResult.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrResult.TextAlign")));
			this.lblclrResult.Visible = ((bool)(resources.GetObject("lblclrResult.Visible")));
			// 
			// lblResultLabel
			// 
			this.lblResultLabel.AccessibleDescription = ((string)(resources.GetObject("lblResultLabel.AccessibleDescription")));
			this.lblResultLabel.AccessibleName = ((string)(resources.GetObject("lblResultLabel.AccessibleName")));
			this.lblResultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblResultLabel.Anchor")));
			this.lblResultLabel.AutoSize = ((bool)(resources.GetObject("lblResultLabel.AutoSize")));
			this.lblResultLabel.BackColor = System.Drawing.Color.Transparent;
			this.lblResultLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblResultLabel.Dock")));
			this.lblResultLabel.Enabled = ((bool)(resources.GetObject("lblResultLabel.Enabled")));
			this.lblResultLabel.Font = ((System.Drawing.Font)(resources.GetObject("lblResultLabel.Font")));
			this.lblResultLabel.Image = ((System.Drawing.Image)(resources.GetObject("lblResultLabel.Image")));
			this.lblResultLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblResultLabel.ImageAlign")));
			this.lblResultLabel.ImageIndex = ((int)(resources.GetObject("lblResultLabel.ImageIndex")));
			this.lblResultLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblResultLabel.ImeMode")));
			this.lblResultLabel.Location = ((System.Drawing.Point)(resources.GetObject("lblResultLabel.Location")));
			this.lblResultLabel.Name = "lblResultLabel";
			this.lblResultLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblResultLabel.RightToLeft")));
			this.lblResultLabel.Size = ((System.Drawing.Size)(resources.GetObject("lblResultLabel.Size")));
			this.lblResultLabel.TabIndex = ((int)(resources.GetObject("lblResultLabel.TabIndex")));
			this.lblResultLabel.Text = resources.GetString("lblResultLabel.Text");
			this.lblResultLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblResultLabel.TextAlign")));
			this.lblResultLabel.Visible = ((bool)(resources.GetObject("lblResultLabel.Visible")));
			// 
			// btnChangeResultColor
			// 
			this.btnChangeResultColor.AccessibleDescription = ((string)(resources.GetObject("btnChangeResultColor.AccessibleDescription")));
			this.btnChangeResultColor.AccessibleName = ((string)(resources.GetObject("btnChangeResultColor.AccessibleName")));
			this.btnChangeResultColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnChangeResultColor.Anchor")));
			this.btnChangeResultColor.BackColor = System.Drawing.Color.Transparent;
			this.btnChangeResultColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChangeResultColor.BackgroundImage")));
			this.btnChangeResultColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnChangeResultColor.Dock")));
			this.btnChangeResultColor.Enabled = ((bool)(resources.GetObject("btnChangeResultColor.Enabled")));
			this.btnChangeResultColor.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnChangeResultColor.FlatStyle")));
			this.btnChangeResultColor.Font = ((System.Drawing.Font)(resources.GetObject("btnChangeResultColor.Font")));
			this.btnChangeResultColor.Image = ((System.Drawing.Image)(resources.GetObject("btnChangeResultColor.Image")));
			this.btnChangeResultColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnChangeResultColor.ImageAlign")));
			this.btnChangeResultColor.ImageIndex = ((int)(resources.GetObject("btnChangeResultColor.ImageIndex")));
			this.btnChangeResultColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnChangeResultColor.ImeMode")));
			this.btnChangeResultColor.Location = ((System.Drawing.Point)(resources.GetObject("btnChangeResultColor.Location")));
			this.btnChangeResultColor.Name = "btnChangeResultColor";
			this.btnChangeResultColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnChangeResultColor.RightToLeft")));
			this.btnChangeResultColor.Size = ((System.Drawing.Size)(resources.GetObject("btnChangeResultColor.Size")));
			this.btnChangeResultColor.TabIndex = ((int)(resources.GetObject("btnChangeResultColor.TabIndex")));
			this.btnChangeResultColor.Text = resources.GetString("btnChangeResultColor.Text");
			this.btnChangeResultColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnChangeResultColor.TextAlign")));
			this.btnChangeResultColor.Visible = ((bool)(resources.GetObject("btnChangeResultColor.Visible")));
			this.btnChangeResultColor.Click += new System.EventHandler(this.btnChangeResultColor_Click);
			// 
			// lblclrActiveColor
			// 
			this.lblclrActiveColor.AccessibleDescription = ((string)(resources.GetObject("lblclrActiveColor.AccessibleDescription")));
			this.lblclrActiveColor.AccessibleName = ((string)(resources.GetObject("lblclrActiveColor.AccessibleName")));
			this.lblclrActiveColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblclrActiveColor.Anchor")));
			this.lblclrActiveColor.AutoSize = ((bool)(resources.GetObject("lblclrActiveColor.AutoSize")));
			this.lblclrActiveColor.BackColor = System.Drawing.Color.Blue;
			this.lblclrActiveColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblclrActiveColor.Dock")));
			this.lblclrActiveColor.Enabled = ((bool)(resources.GetObject("lblclrActiveColor.Enabled")));
			this.lblclrActiveColor.Font = ((System.Drawing.Font)(resources.GetObject("lblclrActiveColor.Font")));
			this.lblclrActiveColor.Image = ((System.Drawing.Image)(resources.GetObject("lblclrActiveColor.Image")));
			this.lblclrActiveColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrActiveColor.ImageAlign")));
			this.lblclrActiveColor.ImageIndex = ((int)(resources.GetObject("lblclrActiveColor.ImageIndex")));
			this.lblclrActiveColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblclrActiveColor.ImeMode")));
			this.lblclrActiveColor.Location = ((System.Drawing.Point)(resources.GetObject("lblclrActiveColor.Location")));
			this.lblclrActiveColor.Name = "lblclrActiveColor";
			this.lblclrActiveColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblclrActiveColor.RightToLeft")));
			this.lblclrActiveColor.Size = ((System.Drawing.Size)(resources.GetObject("lblclrActiveColor.Size")));
			this.lblclrActiveColor.TabIndex = ((int)(resources.GetObject("lblclrActiveColor.TabIndex")));
			this.lblclrActiveColor.Text = resources.GetString("lblclrActiveColor.Text");
			this.lblclrActiveColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrActiveColor.TextAlign")));
			this.lblclrActiveColor.Visible = ((bool)(resources.GetObject("lblclrActiveColor.Visible")));
			// 
			// btnChangeActiveColor
			// 
			this.btnChangeActiveColor.AccessibleDescription = ((string)(resources.GetObject("btnChangeActiveColor.AccessibleDescription")));
			this.btnChangeActiveColor.AccessibleName = ((string)(resources.GetObject("btnChangeActiveColor.AccessibleName")));
			this.btnChangeActiveColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnChangeActiveColor.Anchor")));
			this.btnChangeActiveColor.BackColor = System.Drawing.Color.Transparent;
			this.btnChangeActiveColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChangeActiveColor.BackgroundImage")));
			this.btnChangeActiveColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnChangeActiveColor.Dock")));
			this.btnChangeActiveColor.Enabled = ((bool)(resources.GetObject("btnChangeActiveColor.Enabled")));
			this.btnChangeActiveColor.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnChangeActiveColor.FlatStyle")));
			this.btnChangeActiveColor.Font = ((System.Drawing.Font)(resources.GetObject("btnChangeActiveColor.Font")));
			this.btnChangeActiveColor.Image = ((System.Drawing.Image)(resources.GetObject("btnChangeActiveColor.Image")));
			this.btnChangeActiveColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnChangeActiveColor.ImageAlign")));
			this.btnChangeActiveColor.ImageIndex = ((int)(resources.GetObject("btnChangeActiveColor.ImageIndex")));
			this.btnChangeActiveColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnChangeActiveColor.ImeMode")));
			this.btnChangeActiveColor.Location = ((System.Drawing.Point)(resources.GetObject("btnChangeActiveColor.Location")));
			this.btnChangeActiveColor.Name = "btnChangeActiveColor";
			this.btnChangeActiveColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnChangeActiveColor.RightToLeft")));
			this.btnChangeActiveColor.Size = ((System.Drawing.Size)(resources.GetObject("btnChangeActiveColor.Size")));
			this.btnChangeActiveColor.TabIndex = ((int)(resources.GetObject("btnChangeActiveColor.TabIndex")));
			this.btnChangeActiveColor.Text = resources.GetString("btnChangeActiveColor.Text");
			this.btnChangeActiveColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnChangeActiveColor.TextAlign")));
			this.btnChangeActiveColor.Visible = ((bool)(resources.GetObject("btnChangeActiveColor.Visible")));
			this.btnChangeActiveColor.Click += new System.EventHandler(this.btnChangeActiveColor_Click);
			// 
			// lblActiveColorLabel
			// 
			this.lblActiveColorLabel.AccessibleDescription = ((string)(resources.GetObject("lblActiveColorLabel.AccessibleDescription")));
			this.lblActiveColorLabel.AccessibleName = ((string)(resources.GetObject("lblActiveColorLabel.AccessibleName")));
			this.lblActiveColorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblActiveColorLabel.Anchor")));
			this.lblActiveColorLabel.AutoSize = ((bool)(resources.GetObject("lblActiveColorLabel.AutoSize")));
			this.lblActiveColorLabel.BackColor = System.Drawing.Color.Transparent;
			this.lblActiveColorLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblActiveColorLabel.Dock")));
			this.lblActiveColorLabel.Enabled = ((bool)(resources.GetObject("lblActiveColorLabel.Enabled")));
			this.lblActiveColorLabel.Font = ((System.Drawing.Font)(resources.GetObject("lblActiveColorLabel.Font")));
			this.lblActiveColorLabel.Image = ((System.Drawing.Image)(resources.GetObject("lblActiveColorLabel.Image")));
			this.lblActiveColorLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblActiveColorLabel.ImageAlign")));
			this.lblActiveColorLabel.ImageIndex = ((int)(resources.GetObject("lblActiveColorLabel.ImageIndex")));
			this.lblActiveColorLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblActiveColorLabel.ImeMode")));
			this.lblActiveColorLabel.Location = ((System.Drawing.Point)(resources.GetObject("lblActiveColorLabel.Location")));
			this.lblActiveColorLabel.Name = "lblActiveColorLabel";
			this.lblActiveColorLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblActiveColorLabel.RightToLeft")));
			this.lblActiveColorLabel.Size = ((System.Drawing.Size)(resources.GetObject("lblActiveColorLabel.Size")));
			this.lblActiveColorLabel.TabIndex = ((int)(resources.GetObject("lblActiveColorLabel.TabIndex")));
			this.lblActiveColorLabel.Text = resources.GetString("lblActiveColorLabel.Text");
			this.lblActiveColorLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblActiveColorLabel.TextAlign")));
			this.lblActiveColorLabel.Visible = ((bool)(resources.GetObject("lblActiveColorLabel.Visible")));
			// 
			// lblclrDotDotColor
			// 
			this.lblclrDotDotColor.AccessibleDescription = ((string)(resources.GetObject("lblclrDotDotColor.AccessibleDescription")));
			this.lblclrDotDotColor.AccessibleName = ((string)(resources.GetObject("lblclrDotDotColor.AccessibleName")));
			this.lblclrDotDotColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblclrDotDotColor.Anchor")));
			this.lblclrDotDotColor.AutoSize = ((bool)(resources.GetObject("lblclrDotDotColor.AutoSize")));
			this.lblclrDotDotColor.BackColor = System.Drawing.Color.Yellow;
			this.lblclrDotDotColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblclrDotDotColor.Dock")));
			this.lblclrDotDotColor.Enabled = ((bool)(resources.GetObject("lblclrDotDotColor.Enabled")));
			this.lblclrDotDotColor.Font = ((System.Drawing.Font)(resources.GetObject("lblclrDotDotColor.Font")));
			this.lblclrDotDotColor.Image = ((System.Drawing.Image)(resources.GetObject("lblclrDotDotColor.Image")));
			this.lblclrDotDotColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrDotDotColor.ImageAlign")));
			this.lblclrDotDotColor.ImageIndex = ((int)(resources.GetObject("lblclrDotDotColor.ImageIndex")));
			this.lblclrDotDotColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblclrDotDotColor.ImeMode")));
			this.lblclrDotDotColor.Location = ((System.Drawing.Point)(resources.GetObject("lblclrDotDotColor.Location")));
			this.lblclrDotDotColor.Name = "lblclrDotDotColor";
			this.lblclrDotDotColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblclrDotDotColor.RightToLeft")));
			this.lblclrDotDotColor.Size = ((System.Drawing.Size)(resources.GetObject("lblclrDotDotColor.Size")));
			this.lblclrDotDotColor.TabIndex = ((int)(resources.GetObject("lblclrDotDotColor.TabIndex")));
			this.lblclrDotDotColor.Text = resources.GetString("lblclrDotDotColor.Text");
			this.lblclrDotDotColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrDotDotColor.TextAlign")));
			this.lblclrDotDotColor.Visible = ((bool)(resources.GetObject("lblclrDotDotColor.Visible")));
			// 
			// lblclrEndVectorColor
			// 
			this.lblclrEndVectorColor.AccessibleDescription = ((string)(resources.GetObject("lblclrEndVectorColor.AccessibleDescription")));
			this.lblclrEndVectorColor.AccessibleName = ((string)(resources.GetObject("lblclrEndVectorColor.AccessibleName")));
			this.lblclrEndVectorColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblclrEndVectorColor.Anchor")));
			this.lblclrEndVectorColor.AutoSize = ((bool)(resources.GetObject("lblclrEndVectorColor.AutoSize")));
			this.lblclrEndVectorColor.BackColor = System.Drawing.Color.Red;
			this.lblclrEndVectorColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblclrEndVectorColor.Dock")));
			this.lblclrEndVectorColor.Enabled = ((bool)(resources.GetObject("lblclrEndVectorColor.Enabled")));
			this.lblclrEndVectorColor.Font = ((System.Drawing.Font)(resources.GetObject("lblclrEndVectorColor.Font")));
			this.lblclrEndVectorColor.Image = ((System.Drawing.Image)(resources.GetObject("lblclrEndVectorColor.Image")));
			this.lblclrEndVectorColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrEndVectorColor.ImageAlign")));
			this.lblclrEndVectorColor.ImageIndex = ((int)(resources.GetObject("lblclrEndVectorColor.ImageIndex")));
			this.lblclrEndVectorColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblclrEndVectorColor.ImeMode")));
			this.lblclrEndVectorColor.Location = ((System.Drawing.Point)(resources.GetObject("lblclrEndVectorColor.Location")));
			this.lblclrEndVectorColor.Name = "lblclrEndVectorColor";
			this.lblclrEndVectorColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblclrEndVectorColor.RightToLeft")));
			this.lblclrEndVectorColor.Size = ((System.Drawing.Size)(resources.GetObject("lblclrEndVectorColor.Size")));
			this.lblclrEndVectorColor.TabIndex = ((int)(resources.GetObject("lblclrEndVectorColor.TabIndex")));
			this.lblclrEndVectorColor.Text = resources.GetString("lblclrEndVectorColor.Text");
			this.lblclrEndVectorColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrEndVectorColor.TextAlign")));
			this.lblclrEndVectorColor.Visible = ((bool)(resources.GetObject("lblclrEndVectorColor.Visible")));
			// 
			// lblclrStartVectorColor
			// 
			this.lblclrStartVectorColor.AccessibleDescription = ((string)(resources.GetObject("lblclrStartVectorColor.AccessibleDescription")));
			this.lblclrStartVectorColor.AccessibleName = ((string)(resources.GetObject("lblclrStartVectorColor.AccessibleName")));
			this.lblclrStartVectorColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblclrStartVectorColor.Anchor")));
			this.lblclrStartVectorColor.AutoSize = ((bool)(resources.GetObject("lblclrStartVectorColor.AutoSize")));
			this.lblclrStartVectorColor.BackColor = System.Drawing.Color.Green;
			this.lblclrStartVectorColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblclrStartVectorColor.Dock")));
			this.lblclrStartVectorColor.Enabled = ((bool)(resources.GetObject("lblclrStartVectorColor.Enabled")));
			this.lblclrStartVectorColor.Font = ((System.Drawing.Font)(resources.GetObject("lblclrStartVectorColor.Font")));
			this.lblclrStartVectorColor.Image = ((System.Drawing.Image)(resources.GetObject("lblclrStartVectorColor.Image")));
			this.lblclrStartVectorColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrStartVectorColor.ImageAlign")));
			this.lblclrStartVectorColor.ImageIndex = ((int)(resources.GetObject("lblclrStartVectorColor.ImageIndex")));
			this.lblclrStartVectorColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblclrStartVectorColor.ImeMode")));
			this.lblclrStartVectorColor.Location = ((System.Drawing.Point)(resources.GetObject("lblclrStartVectorColor.Location")));
			this.lblclrStartVectorColor.Name = "lblclrStartVectorColor";
			this.lblclrStartVectorColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblclrStartVectorColor.RightToLeft")));
			this.lblclrStartVectorColor.Size = ((System.Drawing.Size)(resources.GetObject("lblclrStartVectorColor.Size")));
			this.lblclrStartVectorColor.TabIndex = ((int)(resources.GetObject("lblclrStartVectorColor.TabIndex")));
			this.lblclrStartVectorColor.Text = resources.GetString("lblclrStartVectorColor.Text");
			this.lblclrStartVectorColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrStartVectorColor.TextAlign")));
			this.lblclrStartVectorColor.Visible = ((bool)(resources.GetObject("lblclrStartVectorColor.Visible")));
			// 
			// lblclrBackColor1
			// 
			this.lblclrBackColor1.AccessibleDescription = ((string)(resources.GetObject("lblclrBackColor1.AccessibleDescription")));
			this.lblclrBackColor1.AccessibleName = ((string)(resources.GetObject("lblclrBackColor1.AccessibleName")));
			this.lblclrBackColor1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblclrBackColor1.Anchor")));
			this.lblclrBackColor1.AutoSize = ((bool)(resources.GetObject("lblclrBackColor1.AutoSize")));
			this.lblclrBackColor1.BackColor = System.Drawing.Color.White;
			this.lblclrBackColor1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblclrBackColor1.Dock")));
			this.lblclrBackColor1.Enabled = ((bool)(resources.GetObject("lblclrBackColor1.Enabled")));
			this.lblclrBackColor1.Font = ((System.Drawing.Font)(resources.GetObject("lblclrBackColor1.Font")));
			this.lblclrBackColor1.Image = ((System.Drawing.Image)(resources.GetObject("lblclrBackColor1.Image")));
			this.lblclrBackColor1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrBackColor1.ImageAlign")));
			this.lblclrBackColor1.ImageIndex = ((int)(resources.GetObject("lblclrBackColor1.ImageIndex")));
			this.lblclrBackColor1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblclrBackColor1.ImeMode")));
			this.lblclrBackColor1.Location = ((System.Drawing.Point)(resources.GetObject("lblclrBackColor1.Location")));
			this.lblclrBackColor1.Name = "lblclrBackColor1";
			this.lblclrBackColor1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblclrBackColor1.RightToLeft")));
			this.lblclrBackColor1.Size = ((System.Drawing.Size)(resources.GetObject("lblclrBackColor1.Size")));
			this.lblclrBackColor1.TabIndex = ((int)(resources.GetObject("lblclrBackColor1.TabIndex")));
			this.lblclrBackColor1.Text = resources.GetString("lblclrBackColor1.Text");
			this.lblclrBackColor1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblclrBackColor1.TextAlign")));
			this.lblclrBackColor1.Visible = ((bool)(resources.GetObject("lblclrBackColor1.Visible")));
			// 
			// BackColorLabel
			// 
			this.BackColorLabel.AccessibleDescription = ((string)(resources.GetObject("BackColorLabel.AccessibleDescription")));
			this.BackColorLabel.AccessibleName = ((string)(resources.GetObject("BackColorLabel.AccessibleName")));
			this.BackColorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("BackColorLabel.Anchor")));
			this.BackColorLabel.AutoSize = ((bool)(resources.GetObject("BackColorLabel.AutoSize")));
			this.BackColorLabel.BackColor = System.Drawing.Color.Transparent;
			this.BackColorLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("BackColorLabel.Dock")));
			this.BackColorLabel.Enabled = ((bool)(resources.GetObject("BackColorLabel.Enabled")));
			this.BackColorLabel.Font = ((System.Drawing.Font)(resources.GetObject("BackColorLabel.Font")));
			this.BackColorLabel.Image = ((System.Drawing.Image)(resources.GetObject("BackColorLabel.Image")));
			this.BackColorLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("BackColorLabel.ImageAlign")));
			this.BackColorLabel.ImageIndex = ((int)(resources.GetObject("BackColorLabel.ImageIndex")));
			this.BackColorLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("BackColorLabel.ImeMode")));
			this.BackColorLabel.Location = ((System.Drawing.Point)(resources.GetObject("BackColorLabel.Location")));
			this.BackColorLabel.Name = "BackColorLabel";
			this.BackColorLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("BackColorLabel.RightToLeft")));
			this.BackColorLabel.Size = ((System.Drawing.Size)(resources.GetObject("BackColorLabel.Size")));
			this.BackColorLabel.TabIndex = ((int)(resources.GetObject("BackColorLabel.TabIndex")));
			this.BackColorLabel.Text = resources.GetString("BackColorLabel.Text");
			this.BackColorLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("BackColorLabel.TextAlign")));
			this.BackColorLabel.Visible = ((bool)(resources.GetObject("BackColorLabel.Visible")));
			// 
			// lblMovingVectorColor
			// 
			this.lblMovingVectorColor.AccessibleDescription = ((string)(resources.GetObject("lblMovingVectorColor.AccessibleDescription")));
			this.lblMovingVectorColor.AccessibleName = ((string)(resources.GetObject("lblMovingVectorColor.AccessibleName")));
			this.lblMovingVectorColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMovingVectorColor.Anchor")));
			this.lblMovingVectorColor.AutoSize = ((bool)(resources.GetObject("lblMovingVectorColor.AutoSize")));
			this.lblMovingVectorColor.BackColor = System.Drawing.Color.Transparent;
			this.lblMovingVectorColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMovingVectorColor.Dock")));
			this.lblMovingVectorColor.Enabled = ((bool)(resources.GetObject("lblMovingVectorColor.Enabled")));
			this.lblMovingVectorColor.Font = ((System.Drawing.Font)(resources.GetObject("lblMovingVectorColor.Font")));
			this.lblMovingVectorColor.Image = ((System.Drawing.Image)(resources.GetObject("lblMovingVectorColor.Image")));
			this.lblMovingVectorColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMovingVectorColor.ImageAlign")));
			this.lblMovingVectorColor.ImageIndex = ((int)(resources.GetObject("lblMovingVectorColor.ImageIndex")));
			this.lblMovingVectorColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMovingVectorColor.ImeMode")));
			this.lblMovingVectorColor.Location = ((System.Drawing.Point)(resources.GetObject("lblMovingVectorColor.Location")));
			this.lblMovingVectorColor.Name = "lblMovingVectorColor";
			this.lblMovingVectorColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMovingVectorColor.RightToLeft")));
			this.lblMovingVectorColor.Size = ((System.Drawing.Size)(resources.GetObject("lblMovingVectorColor.Size")));
			this.lblMovingVectorColor.TabIndex = ((int)(resources.GetObject("lblMovingVectorColor.TabIndex")));
			this.lblMovingVectorColor.Text = resources.GetString("lblMovingVectorColor.Text");
			this.lblMovingVectorColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMovingVectorColor.TextAlign")));
			this.lblMovingVectorColor.Visible = ((bool)(resources.GetObject("lblMovingVectorColor.Visible")));
			// 
			// btnChangeDotDotVectors
			// 
			this.btnChangeDotDotVectors.AccessibleDescription = ((string)(resources.GetObject("btnChangeDotDotVectors.AccessibleDescription")));
			this.btnChangeDotDotVectors.AccessibleName = ((string)(resources.GetObject("btnChangeDotDotVectors.AccessibleName")));
			this.btnChangeDotDotVectors.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnChangeDotDotVectors.Anchor")));
			this.btnChangeDotDotVectors.BackColor = System.Drawing.Color.Transparent;
			this.btnChangeDotDotVectors.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChangeDotDotVectors.BackgroundImage")));
			this.btnChangeDotDotVectors.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnChangeDotDotVectors.Dock")));
			this.btnChangeDotDotVectors.Enabled = ((bool)(resources.GetObject("btnChangeDotDotVectors.Enabled")));
			this.btnChangeDotDotVectors.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnChangeDotDotVectors.FlatStyle")));
			this.btnChangeDotDotVectors.Font = ((System.Drawing.Font)(resources.GetObject("btnChangeDotDotVectors.Font")));
			this.btnChangeDotDotVectors.Image = ((System.Drawing.Image)(resources.GetObject("btnChangeDotDotVectors.Image")));
			this.btnChangeDotDotVectors.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnChangeDotDotVectors.ImageAlign")));
			this.btnChangeDotDotVectors.ImageIndex = ((int)(resources.GetObject("btnChangeDotDotVectors.ImageIndex")));
			this.btnChangeDotDotVectors.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnChangeDotDotVectors.ImeMode")));
			this.btnChangeDotDotVectors.Location = ((System.Drawing.Point)(resources.GetObject("btnChangeDotDotVectors.Location")));
			this.btnChangeDotDotVectors.Name = "btnChangeDotDotVectors";
			this.btnChangeDotDotVectors.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnChangeDotDotVectors.RightToLeft")));
			this.btnChangeDotDotVectors.Size = ((System.Drawing.Size)(resources.GetObject("btnChangeDotDotVectors.Size")));
			this.btnChangeDotDotVectors.TabIndex = ((int)(resources.GetObject("btnChangeDotDotVectors.TabIndex")));
			this.btnChangeDotDotVectors.Text = resources.GetString("btnChangeDotDotVectors.Text");
			this.btnChangeDotDotVectors.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnChangeDotDotVectors.TextAlign")));
			this.btnChangeDotDotVectors.Visible = ((bool)(resources.GetObject("btnChangeDotDotVectors.Visible")));
			this.btnChangeDotDotVectors.Click += new System.EventHandler(this.btnChangeDotDotVectors_Click);
			// 
			// StartVectorColorLabel
			// 
			this.StartVectorColorLabel.AccessibleDescription = ((string)(resources.GetObject("StartVectorColorLabel.AccessibleDescription")));
			this.StartVectorColorLabel.AccessibleName = ((string)(resources.GetObject("StartVectorColorLabel.AccessibleName")));
			this.StartVectorColorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("StartVectorColorLabel.Anchor")));
			this.StartVectorColorLabel.AutoSize = ((bool)(resources.GetObject("StartVectorColorLabel.AutoSize")));
			this.StartVectorColorLabel.BackColor = System.Drawing.Color.Transparent;
			this.StartVectorColorLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("StartVectorColorLabel.Dock")));
			this.StartVectorColorLabel.Enabled = ((bool)(resources.GetObject("StartVectorColorLabel.Enabled")));
			this.StartVectorColorLabel.Font = ((System.Drawing.Font)(resources.GetObject("StartVectorColorLabel.Font")));
			this.StartVectorColorLabel.Image = ((System.Drawing.Image)(resources.GetObject("StartVectorColorLabel.Image")));
			this.StartVectorColorLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("StartVectorColorLabel.ImageAlign")));
			this.StartVectorColorLabel.ImageIndex = ((int)(resources.GetObject("StartVectorColorLabel.ImageIndex")));
			this.StartVectorColorLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("StartVectorColorLabel.ImeMode")));
			this.StartVectorColorLabel.Location = ((System.Drawing.Point)(resources.GetObject("StartVectorColorLabel.Location")));
			this.StartVectorColorLabel.Name = "StartVectorColorLabel";
			this.StartVectorColorLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("StartVectorColorLabel.RightToLeft")));
			this.StartVectorColorLabel.Size = ((System.Drawing.Size)(resources.GetObject("StartVectorColorLabel.Size")));
			this.StartVectorColorLabel.TabIndex = ((int)(resources.GetObject("StartVectorColorLabel.TabIndex")));
			this.StartVectorColorLabel.Text = resources.GetString("StartVectorColorLabel.Text");
			this.StartVectorColorLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("StartVectorColorLabel.TextAlign")));
			this.StartVectorColorLabel.Visible = ((bool)(resources.GetObject("StartVectorColorLabel.Visible")));
			// 
			// ChangeEndColor
			// 
			this.ChangeEndColor.AccessibleDescription = ((string)(resources.GetObject("ChangeEndColor.AccessibleDescription")));
			this.ChangeEndColor.AccessibleName = ((string)(resources.GetObject("ChangeEndColor.AccessibleName")));
			this.ChangeEndColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ChangeEndColor.Anchor")));
			this.ChangeEndColor.BackColor = System.Drawing.Color.Transparent;
			this.ChangeEndColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ChangeEndColor.BackgroundImage")));
			this.ChangeEndColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ChangeEndColor.Dock")));
			this.ChangeEndColor.Enabled = ((bool)(resources.GetObject("ChangeEndColor.Enabled")));
			this.ChangeEndColor.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ChangeEndColor.FlatStyle")));
			this.ChangeEndColor.Font = ((System.Drawing.Font)(resources.GetObject("ChangeEndColor.Font")));
			this.ChangeEndColor.Image = ((System.Drawing.Image)(resources.GetObject("ChangeEndColor.Image")));
			this.ChangeEndColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ChangeEndColor.ImageAlign")));
			this.ChangeEndColor.ImageIndex = ((int)(resources.GetObject("ChangeEndColor.ImageIndex")));
			this.ChangeEndColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ChangeEndColor.ImeMode")));
			this.ChangeEndColor.Location = ((System.Drawing.Point)(resources.GetObject("ChangeEndColor.Location")));
			this.ChangeEndColor.Name = "ChangeEndColor";
			this.ChangeEndColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ChangeEndColor.RightToLeft")));
			this.ChangeEndColor.Size = ((System.Drawing.Size)(resources.GetObject("ChangeEndColor.Size")));
			this.ChangeEndColor.TabIndex = ((int)(resources.GetObject("ChangeEndColor.TabIndex")));
			this.ChangeEndColor.Text = resources.GetString("ChangeEndColor.Text");
			this.ChangeEndColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ChangeEndColor.TextAlign")));
			this.ChangeEndColor.Visible = ((bool)(resources.GetObject("ChangeEndColor.Visible")));
			this.ChangeEndColor.Click += new System.EventHandler(this.ChangeEndColor_Click);
			// 
			// EndVectorColorLabel
			// 
			this.EndVectorColorLabel.AccessibleDescription = ((string)(resources.GetObject("EndVectorColorLabel.AccessibleDescription")));
			this.EndVectorColorLabel.AccessibleName = ((string)(resources.GetObject("EndVectorColorLabel.AccessibleName")));
			this.EndVectorColorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("EndVectorColorLabel.Anchor")));
			this.EndVectorColorLabel.AutoSize = ((bool)(resources.GetObject("EndVectorColorLabel.AutoSize")));
			this.EndVectorColorLabel.BackColor = System.Drawing.Color.Transparent;
			this.EndVectorColorLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("EndVectorColorLabel.Dock")));
			this.EndVectorColorLabel.Enabled = ((bool)(resources.GetObject("EndVectorColorLabel.Enabled")));
			this.EndVectorColorLabel.Font = ((System.Drawing.Font)(resources.GetObject("EndVectorColorLabel.Font")));
			this.EndVectorColorLabel.Image = ((System.Drawing.Image)(resources.GetObject("EndVectorColorLabel.Image")));
			this.EndVectorColorLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("EndVectorColorLabel.ImageAlign")));
			this.EndVectorColorLabel.ImageIndex = ((int)(resources.GetObject("EndVectorColorLabel.ImageIndex")));
			this.EndVectorColorLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("EndVectorColorLabel.ImeMode")));
			this.EndVectorColorLabel.Location = ((System.Drawing.Point)(resources.GetObject("EndVectorColorLabel.Location")));
			this.EndVectorColorLabel.Name = "EndVectorColorLabel";
			this.EndVectorColorLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("EndVectorColorLabel.RightToLeft")));
			this.EndVectorColorLabel.Size = ((System.Drawing.Size)(resources.GetObject("EndVectorColorLabel.Size")));
			this.EndVectorColorLabel.TabIndex = ((int)(resources.GetObject("EndVectorColorLabel.TabIndex")));
			this.EndVectorColorLabel.Text = resources.GetString("EndVectorColorLabel.Text");
			this.EndVectorColorLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("EndVectorColorLabel.TextAlign")));
			this.EndVectorColorLabel.Visible = ((bool)(resources.GetObject("EndVectorColorLabel.Visible")));
			// 
			// ChangeHeaderColor
			// 
			this.ChangeHeaderColor.AccessibleDescription = ((string)(resources.GetObject("ChangeHeaderColor.AccessibleDescription")));
			this.ChangeHeaderColor.AccessibleName = ((string)(resources.GetObject("ChangeHeaderColor.AccessibleName")));
			this.ChangeHeaderColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ChangeHeaderColor.Anchor")));
			this.ChangeHeaderColor.BackColor = System.Drawing.Color.Transparent;
			this.ChangeHeaderColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ChangeHeaderColor.BackgroundImage")));
			this.ChangeHeaderColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ChangeHeaderColor.Dock")));
			this.ChangeHeaderColor.Enabled = ((bool)(resources.GetObject("ChangeHeaderColor.Enabled")));
			this.ChangeHeaderColor.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ChangeHeaderColor.FlatStyle")));
			this.ChangeHeaderColor.Font = ((System.Drawing.Font)(resources.GetObject("ChangeHeaderColor.Font")));
			this.ChangeHeaderColor.Image = ((System.Drawing.Image)(resources.GetObject("ChangeHeaderColor.Image")));
			this.ChangeHeaderColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ChangeHeaderColor.ImageAlign")));
			this.ChangeHeaderColor.ImageIndex = ((int)(resources.GetObject("ChangeHeaderColor.ImageIndex")));
			this.ChangeHeaderColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ChangeHeaderColor.ImeMode")));
			this.ChangeHeaderColor.Location = ((System.Drawing.Point)(resources.GetObject("ChangeHeaderColor.Location")));
			this.ChangeHeaderColor.Name = "ChangeHeaderColor";
			this.ChangeHeaderColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ChangeHeaderColor.RightToLeft")));
			this.ChangeHeaderColor.Size = ((System.Drawing.Size)(resources.GetObject("ChangeHeaderColor.Size")));
			this.ChangeHeaderColor.TabIndex = ((int)(resources.GetObject("ChangeHeaderColor.TabIndex")));
			this.ChangeHeaderColor.Text = resources.GetString("ChangeHeaderColor.Text");
			this.ChangeHeaderColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ChangeHeaderColor.TextAlign")));
			this.ChangeHeaderColor.Visible = ((bool)(resources.GetObject("ChangeHeaderColor.Visible")));
			this.ChangeHeaderColor.Click += new System.EventHandler(this.ChangeHeaderColor_Click);
			// 
			// ChangeBackColor
			// 
			this.ChangeBackColor.AccessibleDescription = ((string)(resources.GetObject("ChangeBackColor.AccessibleDescription")));
			this.ChangeBackColor.AccessibleName = ((string)(resources.GetObject("ChangeBackColor.AccessibleName")));
			this.ChangeBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ChangeBackColor.Anchor")));
			this.ChangeBackColor.BackColor = System.Drawing.Color.Transparent;
			this.ChangeBackColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ChangeBackColor.BackgroundImage")));
			this.ChangeBackColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ChangeBackColor.Dock")));
			this.ChangeBackColor.Enabled = ((bool)(resources.GetObject("ChangeBackColor.Enabled")));
			this.ChangeBackColor.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ChangeBackColor.FlatStyle")));
			this.ChangeBackColor.Font = ((System.Drawing.Font)(resources.GetObject("ChangeBackColor.Font")));
			this.ChangeBackColor.Image = ((System.Drawing.Image)(resources.GetObject("ChangeBackColor.Image")));
			this.ChangeBackColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ChangeBackColor.ImageAlign")));
			this.ChangeBackColor.ImageIndex = ((int)(resources.GetObject("ChangeBackColor.ImageIndex")));
			this.ChangeBackColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ChangeBackColor.ImeMode")));
			this.ChangeBackColor.Location = ((System.Drawing.Point)(resources.GetObject("ChangeBackColor.Location")));
			this.ChangeBackColor.Name = "ChangeBackColor";
			this.ChangeBackColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ChangeBackColor.RightToLeft")));
			this.ChangeBackColor.Size = ((System.Drawing.Size)(resources.GetObject("ChangeBackColor.Size")));
			this.ChangeBackColor.TabIndex = ((int)(resources.GetObject("ChangeBackColor.TabIndex")));
			this.ChangeBackColor.Text = resources.GetString("ChangeBackColor.Text");
			this.ChangeBackColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ChangeBackColor.TextAlign")));
			this.ChangeBackColor.Visible = ((bool)(resources.GetObject("ChangeBackColor.Visible")));
			this.ChangeBackColor.Click += new System.EventHandler(this.ChangeBackColor_Click);
			// 
			// ChaneGridColor
			// 
			this.ChaneGridColor.AccessibleDescription = ((string)(resources.GetObject("ChaneGridColor.AccessibleDescription")));
			this.ChaneGridColor.AccessibleName = ((string)(resources.GetObject("ChaneGridColor.AccessibleName")));
			this.ChaneGridColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ChaneGridColor.Anchor")));
			this.ChaneGridColor.BackColor = System.Drawing.Color.Transparent;
			this.ChaneGridColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ChaneGridColor.BackgroundImage")));
			this.ChaneGridColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ChaneGridColor.Dock")));
			this.ChaneGridColor.Enabled = ((bool)(resources.GetObject("ChaneGridColor.Enabled")));
			this.ChaneGridColor.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ChaneGridColor.FlatStyle")));
			this.ChaneGridColor.Font = ((System.Drawing.Font)(resources.GetObject("ChaneGridColor.Font")));
			this.ChaneGridColor.Image = ((System.Drawing.Image)(resources.GetObject("ChaneGridColor.Image")));
			this.ChaneGridColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ChaneGridColor.ImageAlign")));
			this.ChaneGridColor.ImageIndex = ((int)(resources.GetObject("ChaneGridColor.ImageIndex")));
			this.ChaneGridColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ChaneGridColor.ImeMode")));
			this.ChaneGridColor.Location = ((System.Drawing.Point)(resources.GetObject("ChaneGridColor.Location")));
			this.ChaneGridColor.Name = "ChaneGridColor";
			this.ChaneGridColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ChaneGridColor.RightToLeft")));
			this.ChaneGridColor.Size = ((System.Drawing.Size)(resources.GetObject("ChaneGridColor.Size")));
			this.ChaneGridColor.TabIndex = ((int)(resources.GetObject("ChaneGridColor.TabIndex")));
			this.ChaneGridColor.Text = resources.GetString("ChaneGridColor.Text");
			this.ChaneGridColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ChaneGridColor.TextAlign")));
			this.ChaneGridColor.Visible = ((bool)(resources.GetObject("ChaneGridColor.Visible")));
			this.ChaneGridColor.Click += new System.EventHandler(this.ChaneGridColor_Click);
			// 
			// GridColorLabel
			// 
			this.GridColorLabel.AccessibleDescription = ((string)(resources.GetObject("GridColorLabel.AccessibleDescription")));
			this.GridColorLabel.AccessibleName = ((string)(resources.GetObject("GridColorLabel.AccessibleName")));
			this.GridColorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("GridColorLabel.Anchor")));
			this.GridColorLabel.AutoSize = ((bool)(resources.GetObject("GridColorLabel.AutoSize")));
			this.GridColorLabel.BackColor = System.Drawing.Color.Transparent;
			this.GridColorLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("GridColorLabel.Dock")));
			this.GridColorLabel.Enabled = ((bool)(resources.GetObject("GridColorLabel.Enabled")));
			this.GridColorLabel.Font = ((System.Drawing.Font)(resources.GetObject("GridColorLabel.Font")));
			this.GridColorLabel.Image = ((System.Drawing.Image)(resources.GetObject("GridColorLabel.Image")));
			this.GridColorLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("GridColorLabel.ImageAlign")));
			this.GridColorLabel.ImageIndex = ((int)(resources.GetObject("GridColorLabel.ImageIndex")));
			this.GridColorLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("GridColorLabel.ImeMode")));
			this.GridColorLabel.Location = ((System.Drawing.Point)(resources.GetObject("GridColorLabel.Location")));
			this.GridColorLabel.Name = "GridColorLabel";
			this.GridColorLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("GridColorLabel.RightToLeft")));
			this.GridColorLabel.Size = ((System.Drawing.Size)(resources.GetObject("GridColorLabel.Size")));
			this.GridColorLabel.TabIndex = ((int)(resources.GetObject("GridColorLabel.TabIndex")));
			this.GridColorLabel.Text = resources.GetString("GridColorLabel.Text");
			this.GridColorLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("GridColorLabel.TextAlign")));
			this.GridColorLabel.Visible = ((bool)(resources.GetObject("GridColorLabel.Visible")));
			// 
			// imgList
			// 
			this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imgList.ImageSize = ((System.Drawing.Size)(resources.GetObject("imgList.ImageSize")));
			this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
			this.imgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnOK
			// 
			this.btnOK.AccessibleDescription = ((string)(resources.GetObject("btnOK.AccessibleDescription")));
			this.btnOK.AccessibleName = ((string)(resources.GetObject("btnOK.AccessibleName")));
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOK.Anchor")));
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
			// btnCancel
			// 
			this.btnCancel.AccessibleDescription = ((string)(resources.GetObject("btnCancel.AccessibleDescription")));
			this.btnCancel.AccessibleName = ((string)(resources.GetObject("btnCancel.AccessibleName")));
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel.Anchor")));
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
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnReset
			// 
			this.btnReset.AccessibleDescription = ((string)(resources.GetObject("btnReset.AccessibleDescription")));
			this.btnReset.AccessibleName = ((string)(resources.GetObject("btnReset.AccessibleName")));
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnReset.Anchor")));
			this.btnReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReset.BackgroundImage")));
			this.btnReset.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnReset.Dock")));
			this.btnReset.Enabled = ((bool)(resources.GetObject("btnReset.Enabled")));
			this.btnReset.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnReset.FlatStyle")));
			this.btnReset.Font = ((System.Drawing.Font)(resources.GetObject("btnReset.Font")));
			this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
			this.btnReset.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnReset.ImageAlign")));
			this.btnReset.ImageIndex = ((int)(resources.GetObject("btnReset.ImageIndex")));
			this.btnReset.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnReset.ImeMode")));
			this.btnReset.Location = ((System.Drawing.Point)(resources.GetObject("btnReset.Location")));
			this.btnReset.Name = "btnReset";
			this.btnReset.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnReset.RightToLeft")));
			this.btnReset.Size = ((System.Drawing.Size)(resources.GetObject("btnReset.Size")));
			this.btnReset.TabIndex = ((int)(resources.GetObject("btnReset.TabIndex")));
			this.btnReset.Text = resources.GetString("btnReset.Text");
			this.btnReset.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnReset.TextAlign")));
			this.btnReset.Visible = ((bool)(resources.GetObject("btnReset.Visible")));
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnClearAllVector
			// 
			this.btnClearAllVector.AccessibleDescription = ((string)(resources.GetObject("btnClearAllVector.AccessibleDescription")));
			this.btnClearAllVector.AccessibleName = ((string)(resources.GetObject("btnClearAllVector.AccessibleName")));
			this.btnClearAllVector.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClearAllVector.Anchor")));
			this.btnClearAllVector.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllVector.BackgroundImage")));
			this.btnClearAllVector.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClearAllVector.Dock")));
			this.btnClearAllVector.Enabled = ((bool)(resources.GetObject("btnClearAllVector.Enabled")));
			this.btnClearAllVector.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnClearAllVector.FlatStyle")));
			this.btnClearAllVector.Font = ((System.Drawing.Font)(resources.GetObject("btnClearAllVector.Font")));
			this.btnClearAllVector.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllVector.Image")));
			this.btnClearAllVector.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClearAllVector.ImageAlign")));
			this.btnClearAllVector.ImageIndex = ((int)(resources.GetObject("btnClearAllVector.ImageIndex")));
			this.btnClearAllVector.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClearAllVector.ImeMode")));
			this.btnClearAllVector.Location = ((System.Drawing.Point)(resources.GetObject("btnClearAllVector.Location")));
			this.btnClearAllVector.Name = "btnClearAllVector";
			this.btnClearAllVector.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClearAllVector.RightToLeft")));
			this.btnClearAllVector.Size = ((System.Drawing.Size)(resources.GetObject("btnClearAllVector.Size")));
			this.btnClearAllVector.TabIndex = ((int)(resources.GetObject("btnClearAllVector.TabIndex")));
			this.btnClearAllVector.Text = resources.GetString("btnClearAllVector.Text");
			this.btnClearAllVector.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClearAllVector.TextAlign")));
			this.btnClearAllVector.Visible = ((bool)(resources.GetObject("btnClearAllVector.Visible")));
			this.btnClearAllVector.Click += new System.EventHandler(this.btnClearAllVector_Click);
			// 
			// grpPreview
			// 
			this.grpPreview.AccessibleDescription = ((string)(resources.GetObject("grpPreview.AccessibleDescription")));
			this.grpPreview.AccessibleName = ((string)(resources.GetObject("grpPreview.AccessibleName")));
			this.grpPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpPreview.Anchor")));
			this.grpPreview.BackColor = System.Drawing.Color.Transparent;
			this.grpPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpPreview.BackgroundImage")));
			this.grpPreview.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpPreview.Dock")));
			this.grpPreview.Enabled = ((bool)(resources.GetObject("grpPreview.Enabled")));
			this.grpPreview.Font = ((System.Drawing.Font)(resources.GetObject("grpPreview.Font")));
			this.grpPreview.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpPreview.ImeMode")));
			this.grpPreview.Location = ((System.Drawing.Point)(resources.GetObject("grpPreview.Location")));
			this.grpPreview.Name = "grpPreview";
			this.grpPreview.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpPreview.RightToLeft")));
			this.grpPreview.Size = ((System.Drawing.Size)(resources.GetObject("grpPreview.Size")));
			this.grpPreview.TabIndex = ((int)(resources.GetObject("grpPreview.TabIndex")));
			this.grpPreview.TabStop = false;
			this.grpPreview.Text = resources.GetString("grpPreview.Text");
			this.grpPreview.Visible = ((bool)(resources.GetObject("grpPreview.Visible")));
			// 
			// Options
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
																		  this.btnClearAllVector,
																		  this.tabControl,
																		  this.btnReset,
																		  this.btnCancel,
																		  this.btnOK,
																		  this.grpPreview});
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
			this.Name = "Options";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.ShowInTaskbar = false;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Visible = ((bool)(resources.GetObject("$this.Visible")));
			this.Load += new System.EventHandler(this.Options_Load);
			this.tabControl.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trcGeometry)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trcGridHeightSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trcGridWidthSize)).EndInit();
			this.tabColor.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
	}
}


