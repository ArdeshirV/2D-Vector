#region Header
// https://github.com/ArdeshirV/2D-Vector
// Copyright 2002-2004 ArdeshirV, Licensed Under MIT


using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Printing;
using ArdeshirV.Applications.Vector;
using ArdeshirV.Components.ScreenVector;
using System.Runtime.InteropServices;

#endregion
//----------------------------------------------------------------------------------------
namespace ArdeshirV.Applications.Vector
{
	/// <summary>
	/// Calculator form.
	/// </summary>
	public class frmCalculator :
		System.Windows.Forms.Form
	{
		#region Variables


		private bool m_blnSucced;
		private vector m_vctResult;
		private readonly int m_intH;
		private readonly int m_intActive;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnSub;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.ColumnHeader I;
		private System.Windows.Forms.ColumnHeader J;
		private System.Windows.Forms.Label lblOutput;
		private System.Windows.Forms.ColumnHeader ID;
		private System.Windows.Forms.ColumnHeader Len;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnCompute;
		private System.Windows.Forms.Button btnClearAll;
		private System.Windows.Forms.ToolTip tltToolTip;
		private System.Windows.Forms.GroupBox grpChooser;
		private System.Windows.Forms.Button btnBackSpace;
		private System.Windows.Forms.TextBox txtCalculate;
		private System.Windows.Forms.ColumnHeader EndPoint;
		private System.Windows.Forms.StatusBar stbStatusbar;
		private System.Windows.Forms.ImageList imgVectorMode;
		private System.Windows.Forms.ColumnHeader StartPoint;
		private System.Windows.Forms.ListView lstVectorChooser;
		private System.Windows.Forms.Button btnAddToVectorsList;
		private System.Windows.Forms.StatusBarPanel sbpMainPanel;

		// ArrayList used below for more information about click below link now!
		// ms-help://MS.VSCC/MS.MSDNVS/cpref/html/frlrfSystemCollectionsArrayListClassTopic.htm

        /// <summary>
		/// Vector keeper.
		/// </summary>
		private ArrayList m_vectors;

		/// <summary>
		/// Calculated vectors.
		/// </summary>
		private ArrayList m_arlTemp;

		/// <summary>
		/// Actions.
		/// </summary>
		private ArrayList m_arl;

		/// <summary>
		/// Last vector id in screen vector.
		/// </summary>
		private readonly int m_intLastVectorID;
		private System.Windows.Forms.CheckBox m_chkSound;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components;

		#endregion
		//-------------------------------------------------------------------------------
		#region Constructor

		/// <summary>
		/// Constructor with parameters.
		/// </summary>
		/// <param name="Items">Current vectors</param>
		/// <param name="ActiveItem">Number of active vector</param>
		/// <param name="H">Screen vector height</param>
		public frmCalculator(vector[] Items, int ActiveItem, int H, bool ActiveToolTip, bool blnCaculatorBell)
		{
			InitializeComponent();

			if(Items == null)
				Close();

			m_arl = new ArrayList();
			m_arlTemp = new ArrayList();
			m_vectors = new ArrayList(Items.Length);

			m_intH = H;
			Checker(false);
			m_blnSucced = false;
			ButtonEnabled(false);
			m_intActive = ActiveItem;
			m_vectors.AddRange(Items);
			btnCompute.Enabled = false;
			tltToolTip.Active = ActiveToolTip;
			btnAddToVectorsList.Enabled = false;
			m_chkSound.Checked = blnCaculatorBell;
			StartPosition = FormStartPosition.CenterParent;
			UpdateStatusBar(StatusbarCommands.SelectVector);
			m_intLastVectorID = Items[Items.Length - 1].ID + 1;

#if !DEBUG
			//OpacityChangerInterval = 40;
#endif
		}
		#endregion
		//-------------------------------------------------------------------------------
		#region Extern functions

		[DllImport("Kernel32.dll")]
		public static extern Int16 Beep(Int32 Freequency, Int32 Duration);

		#endregion
		//-------------------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Gets sound activity.
		/// </summary>
		public bool SoundActive
		{
			get
			{
				return m_chkSound.Checked;
			}
		}

		/// <summary>
		/// Gets new array of vectors old items and new calculated vectors.
		/// </summary>
		public ArrayList Items
		{
			get
			{
				return m_arlTemp;
			}
		}

		/// <summary>
		/// Gets true if calculating was succesful.
		/// </summary>
		public bool Succed
		{
			get
			{
				return m_blnSucced;
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Event Handlers

		/// <summary>
		/// Occured whenever form has been loaded.
		/// </summary>
		/// <param name="sender">Calculator form</param>
		/// <param name="e">Event argument</param>
		private void frmCalculator_Load(object sender, System.EventArgs e)
		{
			UpdateVectorList();
		}

		/// <summary>
		/// Occured whenever Add button has been clicked 
		/// (Add two vector that has beed selected).
		/// </summary>
		/// <param name="sender">Add button</param>
		/// <param name="e">Event argument</param>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if(m_chkSound.Checked)
				Beep(200, 100);

			if(ButtonsWork(false))
				return;

			m_arl.Add("+");
			txtCalculate.Text += " + ";
			UpdateStatusBar(StatusbarCommands.SelectVector);
		}

		/// <summary>
		/// Occured whenever Sub button has been clicked 
		/// (Subtract two vector that has beed selected).
		/// </summary>
		/// <param name="sender">Sub button</param>
		/// <param name="e">Event argument</param>
		private void btnSub_Click(object sender, System.EventArgs e)
		{
			if(m_chkSound.Checked)
				Beep(300, 100);

			if(ButtonsWork(false))
				return;

			m_arl.Add("-");
			txtCalculate.Text += " - ";
			UpdateStatusBar(StatusbarCommands.SelectVector);
		}

		/// <summary>
		/// Occured whenever Compute button has been clicked (Compute Last calculation).
		/// </summary>
		/// <param name="sender">Campute button</param>
		/// <param name="e">Event argument</param>
		private void btnCompute_Click(object sender, System.EventArgs e)
		{
			if(m_chkSound.Checked)
				Beep(400, 100);

			if(m_arl.Count % 2 == 0 || ButtonsWork(false) || m_arl.Count <= 2)
			{
				((Button)sender).Enabled = false;
				return;
			}

			int l_intNextVector = (int)m_arl[2];
			vector l_vctResult = ((vector)m_vectors[(int)m_arl[0]]);

			for(int i = 1;  i < m_arl.Count ; i += 2)
			{
				switch((string)m_arl[i])
				{
					case "+":
						l_vctResult += ((vector)m_vectors[l_intNextVector]);
						break;
					case "-":
						l_vctResult -= ((vector)m_vectors[l_intNextVector]);
						break;
				}

				if(m_arl.Count >= i + 3)
					l_intNextVector = (int)m_arl[i + 3];
			}

			if(l_vctResult.I == 0 && l_vctResult.J == 0)
			{
				txtCalculate.Text = "None";
				return;
			}

			m_vctResult = l_vctResult;
			lblOutput.Text =
				"Start:" + "(" + vector.Tester(
				m_vctResult.StartPoint.X).ToString() + "," +
				(-vector.Tester(m_vctResult.StartPoint.Y, m_intH)).ToString() + ")" +
				", End:" + "(" + vector.Tester(
				m_vctResult.EndPoint.X).ToString() + "," +
				(-vector.Tester(m_vctResult.EndPoint.Y, m_intH)).ToString() + ")" +
				",  I:" + m_vctResult.SI + ",  J:" + m_vctResult.SJ +
				", Length:" + m_vctResult.Lenght;

			btnAddToVectorsList.Enabled = true;
			UpdateStatusBar(StatusbarCommands.SelectAddToVector);
		}

		/// <summary>
		/// Occured whenever an item on list box has been selected. 
		/// </summary>
		/// <param name="sender">Vector chooser</param>
		/// <param name="e">Event argument</param>
		private void lstVectorChooser_DoubleClick(object sender, System.EventArgs e)
		{
			if(m_chkSound.Checked)
				Beep(100, 100);

			if(txtCalculate.Text.EndsWith("]"))
				return;

			txtCalculate.Text += "[" +
				((vector)m_vectors[lstVectorChooser.SelectedItems[0].Index]).SI
				+ "," + ((vector)m_vectors[
				lstVectorChooser.SelectedItems[0].Index]).SJ + "]";

			Checker(true);
			ButtonEnabled(true);
			UpdateStatusBar(StatusbarCommands.SelectCommand);
			m_arl.Add(lstVectorChooser.SelectedItems[0].Index);
		}

		/// <summary>
		/// Occured whenever back space button has been clicked (Erase last action).
		/// </summary>
		/// <param name="sender">Back space button</param>
		/// <param name="e">Event argument</param>
		private void btnBackSpace_Click(object sender, System.EventArgs e)
		{
			if(m_chkSound.Checked)
				Beep(500, 100);

			if(txtCalculate.Text == "")
			{
				Checker(false);
				return;
			}

			if(txtCalculate.Text.EndsWith(" "))
			{
				txtCalculate.Text = 
					txtCalculate.Text.Remove(txtCalculate.Text.Length - 3, 3);
				ButtonEnabled(true);
				UpdateStatusBar(StatusbarCommands.SelectCommand);
			}
			else
			{
				int l_intIndex = txtCalculate.Text.LastIndexOf("[");
				txtCalculate.Text = txtCalculate.Text.Remove(l_intIndex,
					txtCalculate.Text.Length - l_intIndex);
				ButtonEnabled(false);
				UpdateStatusBar(StatusbarCommands.SelectVector);
			}

			if(txtCalculate.Text == "")
				Checker(false);

			m_arl.RemoveAt(m_arl.Count - 1);
		}

		/// <summary>
		/// Occured whenever Clear button has been clicked (Clear calculate label).
		/// </summary>
		/// <param name="sender">Clear all button</param>
		/// <param name="e">Event argument</param>
		private void btnClearAll_Click(object sender, System.EventArgs e)
		{
			if(m_chkSound.Checked)
				Beep(600, 100);

			m_arl.Clear();
			Checker(false);
			ButtonEnabled(false);
			txtCalculate.Text = "";
			UpdateStatusBar(StatusbarCommands.SelectVector);
		}

		/// <summary>
		/// Add current calculated vector to vector list.
		/// </summary>
		/// <param name="sender">Add to vector list</param>
		/// <param name="e">Event argument</param>
		private void btnAddToVectorsList_Click(object sender, System.EventArgs e)
		{
			if(m_chkSound.Checked)
				Beep(700, 100);

			m_arl.Clear();
			lblOutput.Text = "";
			txtCalculate.Text = "";
			m_vectors.Add(m_vctResult);
			m_arlTemp.Add(m_vctResult);
			btnAddToVectorsList.Enabled = false;
			UpdateStatusBar(StatusbarCommands.SelectVector);
			UpdateVectorList();
		}
		
		/// <summary>
		/// Occured whenever OK button has been cliceked.
		/// </summary>
		/// <param name="sender">OK button</param>
		/// <param name="e">Event argument</param>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			m_blnSucced = true;
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Utility functions

		/// <summary>
		/// Set Enabled property for some controls.
		/// </summary>
		/// <param name="bln">Enabled mode</param>
		private void Checker(bool bln)
		{
			btnClearAll.Enabled =
			btnBackSpace.Enabled = bln;
		}

		/// <summary>
		/// Set Enabled for some button controls.
		/// </summary>
		/// <param name="bln">Enabled mode</param>
		private void ButtonEnabled(bool bln)
		{
			btnAdd.Enabled =
			btnSub.Enabled = bln;

			if(m_arl.Count > 0)
				btnCompute.Enabled = bln;
		}

		/// <summary>
		/// Do someworks that is important in several event handler of buttons.
		/// </summary>
		/// <param name="bln">Enabled mode</param>
		/// <returns>return true if end with "]"</returns>
		private bool ButtonsWork(bool bln)
		{
			if(!txtCalculate.Text.EndsWith("]"))
				return true;

			ButtonEnabled(bln);
			Checker(true);
			return false;
		}

		/// <summary>
		/// Update vectors list.
		/// </summary>
		private void UpdateVectorList()
		{
			ListViewItem Item;
			lstVectorChooser.Items.Clear();
			int l_intNewVct = m_intLastVectorID + 1;

			for(int i = 0; i < m_vectors.Count ; i++)
			{
				Item = new ListViewItem(
					(((vector)m_vectors[i]).ID == -7)? 
					(l_intNewVct++).ToString():((vector)m_vectors[i]).Name,
					(m_intActive == i)? 1: 0);
				Item.SubItems.Add(((vector)m_vectors[i]).Lenght);
				Item.SubItems.Add("(" + vector.Tester(
					((vector)m_vectors[i]).StartPoint.X).ToString() + "," +
					(-vector.Tester(
					((vector)m_vectors[i]).StartPoint.Y, m_intH)).ToString() + ")");
				Item.SubItems.Add("(" + vector.Tester(
					((vector)m_vectors[i]).EndPoint.X).ToString() + "," +
					(-vector.Tester(
					((vector)m_vectors[i]).EndPoint.Y, m_intH)).ToString() + ")");
				Item.SubItems.Add(((vector)m_vectors[i]).SI);
				Item.SubItems.Add(((vector)m_vectors[i]).SJ);
				lstVectorChooser.Items.Add(Item);
			}
		}

		/// <summary>
		/// Update status bar with a command.
		/// </summary>
		/// <param name="command">Command for update status bar</param>
		private void UpdateStatusBar(StatusbarCommands command)
		{
			switch(command)
			{
				case StatusbarCommands.None:
					sbpMainPanel.Text = "None";
					break;
				case StatusbarCommands.SelectVector:
					sbpMainPanel.Text = "Select a vector from vector list" +
						" with double click on vector ID.";
					break;
				case StatusbarCommands.SelectCommand:
					sbpMainPanel.Text =
						"Select a command from right command panel.";
					break;
				case StatusbarCommands.SelectAddToVector:
					sbpMainPanel.Text = "Select \"Add to Vector List\" butt" +
						"on, if you need have result vector with other vectors.";
					break;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmCalculator));
			this.imgVectorMode = new System.Windows.Forms.ImageList(this.components);
			this.txtCalculate = new System.Windows.Forms.TextBox();
			this.tltToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.lblOutput = new System.Windows.Forms.Label();
			this.btnAddToVectorsList = new System.Windows.Forms.Button();
			this.btnCompute = new System.Windows.Forms.Button();
			this.btnClearAll = new System.Windows.Forms.Button();
			this.btnBackSpace = new System.Windows.Forms.Button();
			this.btnSub = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.lstVectorChooser = new System.Windows.Forms.ListView();
			this.ID = new System.Windows.Forms.ColumnHeader();
			this.Len = new System.Windows.Forms.ColumnHeader();
			this.StartPoint = new System.Windows.Forms.ColumnHeader();
			this.EndPoint = new System.Windows.Forms.ColumnHeader();
			this.I = new System.Windows.Forms.ColumnHeader();
			this.J = new System.Windows.Forms.ColumnHeader();
			this.grpChooser = new System.Windows.Forms.GroupBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.stbStatusbar = new System.Windows.Forms.StatusBar();
			this.sbpMainPanel = new System.Windows.Forms.StatusBarPanel();
			this.m_chkSound = new System.Windows.Forms.CheckBox();
			this.grpChooser.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sbpMainPanel)).BeginInit();
			this.SuspendLayout();
			// 
			// imgVectorMode
			// 
			this.imgVectorMode.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imgVectorMode.ImageSize = ((System.Drawing.Size)(resources.GetObject("imgVectorMode.ImageSize")));
			this.imgVectorMode.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgVectorMode.ImageStream")));
			this.imgVectorMode.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// txtCalculate
			// 
			this.txtCalculate.AccessibleDescription = ((string)(resources.GetObject("txtCalculate.AccessibleDescription")));
			this.txtCalculate.AccessibleName = ((string)(resources.GetObject("txtCalculate.AccessibleName")));
			this.txtCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCalculate.Anchor")));
			this.txtCalculate.AutoSize = ((bool)(resources.GetObject("txtCalculate.AutoSize")));
			this.txtCalculate.BackColor = System.Drawing.Color.Silver;
			this.txtCalculate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCalculate.BackgroundImage")));
			this.txtCalculate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCalculate.Dock")));
			this.txtCalculate.Enabled = ((bool)(resources.GetObject("txtCalculate.Enabled")));
			this.txtCalculate.Font = ((System.Drawing.Font)(resources.GetObject("txtCalculate.Font")));
			this.txtCalculate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCalculate.ImeMode")));
			this.txtCalculate.Location = ((System.Drawing.Point)(resources.GetObject("txtCalculate.Location")));
			this.txtCalculate.MaxLength = ((int)(resources.GetObject("txtCalculate.MaxLength")));
			this.txtCalculate.Multiline = ((bool)(resources.GetObject("txtCalculate.Multiline")));
			this.txtCalculate.Name = "txtCalculate";
			this.txtCalculate.PasswordChar = ((char)(resources.GetObject("txtCalculate.PasswordChar")));
			this.txtCalculate.ReadOnly = true;
			this.txtCalculate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCalculate.RightToLeft")));
			this.txtCalculate.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCalculate.ScrollBars")));
			this.txtCalculate.Size = ((System.Drawing.Size)(resources.GetObject("txtCalculate.Size")));
			this.txtCalculate.TabIndex = ((int)(resources.GetObject("txtCalculate.TabIndex")));
			this.txtCalculate.Text = resources.GetString("txtCalculate.Text");
			this.txtCalculate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCalculate.TextAlign")));
			this.tltToolTip.SetToolTip(this.txtCalculate, resources.GetString("txtCalculate.ToolTip"));
			this.txtCalculate.Visible = ((bool)(resources.GetObject("txtCalculate.Visible")));
			this.txtCalculate.WordWrap = ((bool)(resources.GetObject("txtCalculate.WordWrap")));
			// 
			// lblOutput
			// 
			this.lblOutput.AccessibleDescription = ((string)(resources.GetObject("lblOutput.AccessibleDescription")));
			this.lblOutput.AccessibleName = ((string)(resources.GetObject("lblOutput.AccessibleName")));
			this.lblOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblOutput.Anchor")));
			this.lblOutput.AutoSize = ((bool)(resources.GetObject("lblOutput.AutoSize")));
			this.lblOutput.BackColor = System.Drawing.Color.Silver;
			this.lblOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblOutput.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblOutput.Dock")));
			this.lblOutput.Enabled = ((bool)(resources.GetObject("lblOutput.Enabled")));
			this.lblOutput.Font = ((System.Drawing.Font)(resources.GetObject("lblOutput.Font")));
			this.lblOutput.Image = ((System.Drawing.Image)(resources.GetObject("lblOutput.Image")));
			this.lblOutput.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblOutput.ImageAlign")));
			this.lblOutput.ImageIndex = ((int)(resources.GetObject("lblOutput.ImageIndex")));
			this.lblOutput.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblOutput.ImeMode")));
			this.lblOutput.Location = ((System.Drawing.Point)(resources.GetObject("lblOutput.Location")));
			this.lblOutput.Name = "lblOutput";
			this.lblOutput.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblOutput.RightToLeft")));
			this.lblOutput.Size = ((System.Drawing.Size)(resources.GetObject("lblOutput.Size")));
			this.lblOutput.TabIndex = ((int)(resources.GetObject("lblOutput.TabIndex")));
			this.lblOutput.Text = resources.GetString("lblOutput.Text");
			this.lblOutput.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblOutput.TextAlign")));
			this.tltToolTip.SetToolTip(this.lblOutput, resources.GetString("lblOutput.ToolTip"));
			this.lblOutput.Visible = ((bool)(resources.GetObject("lblOutput.Visible")));
			// 
			// btnAddToVectorsList
			// 
			this.btnAddToVectorsList.AccessibleDescription = ((string)(resources.GetObject("btnAddToVectorsList.AccessibleDescription")));
			this.btnAddToVectorsList.AccessibleName = ((string)(resources.GetObject("btnAddToVectorsList.AccessibleName")));
			this.btnAddToVectorsList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAddToVectorsList.Anchor")));
			this.btnAddToVectorsList.BackColor = System.Drawing.Color.Transparent;
			this.btnAddToVectorsList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddToVectorsList.BackgroundImage")));
			this.btnAddToVectorsList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAddToVectorsList.Dock")));
			this.btnAddToVectorsList.Enabled = ((bool)(resources.GetObject("btnAddToVectorsList.Enabled")));
			this.btnAddToVectorsList.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAddToVectorsList.FlatStyle")));
			this.btnAddToVectorsList.Font = ((System.Drawing.Font)(resources.GetObject("btnAddToVectorsList.Font")));
			this.btnAddToVectorsList.Image = ((System.Drawing.Image)(resources.GetObject("btnAddToVectorsList.Image")));
			this.btnAddToVectorsList.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddToVectorsList.ImageAlign")));
			this.btnAddToVectorsList.ImageIndex = ((int)(resources.GetObject("btnAddToVectorsList.ImageIndex")));
			this.btnAddToVectorsList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAddToVectorsList.ImeMode")));
			this.btnAddToVectorsList.Location = ((System.Drawing.Point)(resources.GetObject("btnAddToVectorsList.Location")));
			this.btnAddToVectorsList.Name = "btnAddToVectorsList";
			this.btnAddToVectorsList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAddToVectorsList.RightToLeft")));
			this.btnAddToVectorsList.Size = ((System.Drawing.Size)(resources.GetObject("btnAddToVectorsList.Size")));
			this.btnAddToVectorsList.TabIndex = ((int)(resources.GetObject("btnAddToVectorsList.TabIndex")));
			this.btnAddToVectorsList.Text = resources.GetString("btnAddToVectorsList.Text");
			this.btnAddToVectorsList.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddToVectorsList.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnAddToVectorsList, resources.GetString("btnAddToVectorsList.ToolTip"));
			this.btnAddToVectorsList.Visible = ((bool)(resources.GetObject("btnAddToVectorsList.Visible")));
			this.btnAddToVectorsList.Click += new System.EventHandler(this.btnAddToVectorsList_Click);
			// 
			// btnCompute
			// 
			this.btnCompute.AccessibleDescription = ((string)(resources.GetObject("btnCompute.AccessibleDescription")));
			this.btnCompute.AccessibleName = ((string)(resources.GetObject("btnCompute.AccessibleName")));
			this.btnCompute.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCompute.Anchor")));
			this.btnCompute.BackColor = System.Drawing.Color.Transparent;
			this.btnCompute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCompute.BackgroundImage")));
			this.btnCompute.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCompute.Dock")));
			this.btnCompute.Enabled = ((bool)(resources.GetObject("btnCompute.Enabled")));
			this.btnCompute.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCompute.FlatStyle")));
			this.btnCompute.Font = ((System.Drawing.Font)(resources.GetObject("btnCompute.Font")));
			this.btnCompute.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnCompute.Image")));
			this.btnCompute.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCompute.ImageAlign")));
			this.btnCompute.ImageIndex = ((int)(resources.GetObject("btnCompute.ImageIndex")));
			this.btnCompute.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCompute.ImeMode")));
			this.btnCompute.Location = ((System.Drawing.Point)(resources.GetObject("btnCompute.Location")));
			this.btnCompute.Name = "btnCompute";
			this.btnCompute.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCompute.RightToLeft")));
			this.btnCompute.Size = ((System.Drawing.Size)(resources.GetObject("btnCompute.Size")));
			this.btnCompute.TabIndex = ((int)(resources.GetObject("btnCompute.TabIndex")));
			this.btnCompute.Text = resources.GetString("btnCompute.Text");
			this.btnCompute.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCompute.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnCompute, resources.GetString("btnCompute.ToolTip"));
			this.btnCompute.Visible = ((bool)(resources.GetObject("btnCompute.Visible")));
			this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
			// 
			// btnClearAll
			// 
			this.btnClearAll.AccessibleDescription = ((string)(resources.GetObject("btnClearAll.AccessibleDescription")));
			this.btnClearAll.AccessibleName = ((string)(resources.GetObject("btnClearAll.AccessibleName")));
			this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClearAll.Anchor")));
			this.btnClearAll.BackColor = System.Drawing.Color.Transparent;
			this.btnClearAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAll.BackgroundImage")));
			this.btnClearAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClearAll.Dock")));
			this.btnClearAll.Enabled = ((bool)(resources.GetObject("btnClearAll.Enabled")));
			this.btnClearAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnClearAll.FlatStyle")));
			this.btnClearAll.Font = ((System.Drawing.Font)(resources.GetObject("btnClearAll.Font")));
			this.btnClearAll.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAll.Image")));
			this.btnClearAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClearAll.ImageAlign")));
			this.btnClearAll.ImageIndex = ((int)(resources.GetObject("btnClearAll.ImageIndex")));
			this.btnClearAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClearAll.ImeMode")));
			this.btnClearAll.Location = ((System.Drawing.Point)(resources.GetObject("btnClearAll.Location")));
			this.btnClearAll.Name = "btnClearAll";
			this.btnClearAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClearAll.RightToLeft")));
			this.btnClearAll.Size = ((System.Drawing.Size)(resources.GetObject("btnClearAll.Size")));
			this.btnClearAll.TabIndex = ((int)(resources.GetObject("btnClearAll.TabIndex")));
			this.btnClearAll.Text = resources.GetString("btnClearAll.Text");
			this.btnClearAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClearAll.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnClearAll, resources.GetString("btnClearAll.ToolTip"));
			this.btnClearAll.Visible = ((bool)(resources.GetObject("btnClearAll.Visible")));
			this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
			// 
			// btnBackSpace
			// 
			this.btnBackSpace.AccessibleDescription = ((string)(resources.GetObject("btnBackSpace.AccessibleDescription")));
			this.btnBackSpace.AccessibleName = ((string)(resources.GetObject("btnBackSpace.AccessibleName")));
			this.btnBackSpace.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnBackSpace.Anchor")));
			this.btnBackSpace.BackColor = System.Drawing.Color.Transparent;
			this.btnBackSpace.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBackSpace.BackgroundImage")));
			this.btnBackSpace.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnBackSpace.Dock")));
			this.btnBackSpace.Enabled = ((bool)(resources.GetObject("btnBackSpace.Enabled")));
			this.btnBackSpace.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnBackSpace.FlatStyle")));
			this.btnBackSpace.Font = ((System.Drawing.Font)(resources.GetObject("btnBackSpace.Font")));
			this.btnBackSpace.Image = ((System.Drawing.Image)(resources.GetObject("btnBackSpace.Image")));
			this.btnBackSpace.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnBackSpace.ImageAlign")));
			this.btnBackSpace.ImageIndex = ((int)(resources.GetObject("btnBackSpace.ImageIndex")));
			this.btnBackSpace.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnBackSpace.ImeMode")));
			this.btnBackSpace.Location = ((System.Drawing.Point)(resources.GetObject("btnBackSpace.Location")));
			this.btnBackSpace.Name = "btnBackSpace";
			this.btnBackSpace.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnBackSpace.RightToLeft")));
			this.btnBackSpace.Size = ((System.Drawing.Size)(resources.GetObject("btnBackSpace.Size")));
			this.btnBackSpace.TabIndex = ((int)(resources.GetObject("btnBackSpace.TabIndex")));
			this.btnBackSpace.Text = resources.GetString("btnBackSpace.Text");
			this.btnBackSpace.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnBackSpace.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnBackSpace, resources.GetString("btnBackSpace.ToolTip"));
			this.btnBackSpace.Visible = ((bool)(resources.GetObject("btnBackSpace.Visible")));
			this.btnBackSpace.Click += new System.EventHandler(this.btnBackSpace_Click);
			// 
			// btnSub
			// 
			this.btnSub.AccessibleDescription = ((string)(resources.GetObject("btnSub.AccessibleDescription")));
			this.btnSub.AccessibleName = ((string)(resources.GetObject("btnSub.AccessibleName")));
			this.btnSub.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSub.Anchor")));
			this.btnSub.BackColor = System.Drawing.Color.Transparent;
			this.btnSub.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSub.BackgroundImage")));
			this.btnSub.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSub.Dock")));
			this.btnSub.Enabled = ((bool)(resources.GetObject("btnSub.Enabled")));
			this.btnSub.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSub.FlatStyle")));
			this.btnSub.Font = ((System.Drawing.Font)(resources.GetObject("btnSub.Font")));
			this.btnSub.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnSub.Image")));
			this.btnSub.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSub.ImageAlign")));
			this.btnSub.ImageIndex = ((int)(resources.GetObject("btnSub.ImageIndex")));
			this.btnSub.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSub.ImeMode")));
			this.btnSub.Location = ((System.Drawing.Point)(resources.GetObject("btnSub.Location")));
			this.btnSub.Name = "btnSub";
			this.btnSub.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSub.RightToLeft")));
			this.btnSub.Size = ((System.Drawing.Size)(resources.GetObject("btnSub.Size")));
			this.btnSub.TabIndex = ((int)(resources.GetObject("btnSub.TabIndex")));
			this.btnSub.Text = resources.GetString("btnSub.Text");
			this.btnSub.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSub.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnSub, resources.GetString("btnSub.ToolTip"));
			this.btnSub.Visible = ((bool)(resources.GetObject("btnSub.Visible")));
			this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = ((string)(resources.GetObject("btnAdd.AccessibleDescription")));
			this.btnAdd.AccessibleName = ((string)(resources.GetObject("btnAdd.AccessibleName")));
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAdd.Anchor")));
			this.btnAdd.BackColor = System.Drawing.Color.Transparent;
			this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
			this.btnAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAdd.Dock")));
			this.btnAdd.Enabled = ((bool)(resources.GetObject("btnAdd.Enabled")));
			this.btnAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAdd.FlatStyle")));
			this.btnAdd.Font = ((System.Drawing.Font)(resources.GetObject("btnAdd.Font")));
			this.btnAdd.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.ImageAlign")));
			this.btnAdd.ImageIndex = ((int)(resources.GetObject("btnAdd.ImageIndex")));
			this.btnAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAdd.ImeMode")));
			this.btnAdd.Location = ((System.Drawing.Point)(resources.GetObject("btnAdd.Location")));
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAdd.RightToLeft")));
			this.btnAdd.Size = ((System.Drawing.Size)(resources.GetObject("btnAdd.Size")));
			this.btnAdd.TabIndex = ((int)(resources.GetObject("btnAdd.TabIndex")));
			this.btnAdd.Text = resources.GetString("btnAdd.Text");
			this.btnAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnAdd, resources.GetString("btnAdd.ToolTip"));
			this.btnAdd.Visible = ((bool)(resources.GetObject("btnAdd.Visible")));
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// lstVectorChooser
			// 
			this.lstVectorChooser.AccessibleDescription = ((string)(resources.GetObject("lstVectorChooser.AccessibleDescription")));
			this.lstVectorChooser.AccessibleName = ((string)(resources.GetObject("lstVectorChooser.AccessibleName")));
			this.lstVectorChooser.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("lstVectorChooser.Alignment")));
			this.lstVectorChooser.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lstVectorChooser.Anchor")));
			this.lstVectorChooser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lstVectorChooser.BackgroundImage")));
			this.lstVectorChooser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.ID,
																							   this.Len,
																							   this.StartPoint,
																							   this.EndPoint,
																							   this.I,
																							   this.J});
			this.lstVectorChooser.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lstVectorChooser.Dock")));
			this.lstVectorChooser.Enabled = ((bool)(resources.GetObject("lstVectorChooser.Enabled")));
			this.lstVectorChooser.Font = ((System.Drawing.Font)(resources.GetObject("lstVectorChooser.Font")));
			this.lstVectorChooser.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstVectorChooser.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lstVectorChooser.ImeMode")));
			this.lstVectorChooser.LabelWrap = ((bool)(resources.GetObject("lstVectorChooser.LabelWrap")));
			this.lstVectorChooser.LargeImageList = this.imgVectorMode;
			this.lstVectorChooser.Location = ((System.Drawing.Point)(resources.GetObject("lstVectorChooser.Location")));
			this.lstVectorChooser.MultiSelect = false;
			this.lstVectorChooser.Name = "lstVectorChooser";
			this.lstVectorChooser.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lstVectorChooser.RightToLeft")));
			this.lstVectorChooser.Size = ((System.Drawing.Size)(resources.GetObject("lstVectorChooser.Size")));
			this.lstVectorChooser.SmallImageList = this.imgVectorMode;
			this.lstVectorChooser.TabIndex = ((int)(resources.GetObject("lstVectorChooser.TabIndex")));
			this.lstVectorChooser.Text = resources.GetString("lstVectorChooser.Text");
			this.tltToolTip.SetToolTip(this.lstVectorChooser, resources.GetString("lstVectorChooser.ToolTip"));
			this.lstVectorChooser.View = System.Windows.Forms.View.Details;
			this.lstVectorChooser.Visible = ((bool)(resources.GetObject("lstVectorChooser.Visible")));
			this.lstVectorChooser.DoubleClick += new System.EventHandler(this.lstVectorChooser_DoubleClick);
			// 
			// ID
			// 
			this.ID.Text = resources.GetString("ID.Text");
			this.ID.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ID.TextAlign")));
			this.ID.Width = ((int)(resources.GetObject("ID.Width")));
			// 
			// Len
			// 
			this.Len.Text = resources.GetString("Len.Text");
			this.Len.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("Len.TextAlign")));
			this.Len.Width = ((int)(resources.GetObject("Len.Width")));
			// 
			// StartPoint
			// 
			this.StartPoint.Text = resources.GetString("StartPoint.Text");
			this.StartPoint.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("StartPoint.TextAlign")));
			this.StartPoint.Width = ((int)(resources.GetObject("StartPoint.Width")));
			// 
			// EndPoint
			// 
			this.EndPoint.Text = resources.GetString("EndPoint.Text");
			this.EndPoint.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("EndPoint.TextAlign")));
			this.EndPoint.Width = ((int)(resources.GetObject("EndPoint.Width")));
			// 
			// I
			// 
			this.I.Text = resources.GetString("I.Text");
			this.I.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("I.TextAlign")));
			this.I.Width = ((int)(resources.GetObject("I.Width")));
			// 
			// J
			// 
			this.J.Text = resources.GetString("J.Text");
			this.J.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("J.TextAlign")));
			this.J.Width = ((int)(resources.GetObject("J.Width")));
			// 
			// grpChooser
			// 
			this.grpChooser.AccessibleDescription = ((string)(resources.GetObject("grpChooser.AccessibleDescription")));
			this.grpChooser.AccessibleName = ((string)(resources.GetObject("grpChooser.AccessibleName")));
			this.grpChooser.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpChooser.Anchor")));
			this.grpChooser.BackColor = System.Drawing.Color.Transparent;
			this.grpChooser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpChooser.BackgroundImage")));
			this.grpChooser.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.lstVectorChooser});
			this.grpChooser.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpChooser.Dock")));
			this.grpChooser.Enabled = ((bool)(resources.GetObject("grpChooser.Enabled")));
			this.grpChooser.Font = ((System.Drawing.Font)(resources.GetObject("grpChooser.Font")));
			this.grpChooser.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpChooser.ImeMode")));
			this.grpChooser.Location = ((System.Drawing.Point)(resources.GetObject("grpChooser.Location")));
			this.grpChooser.Name = "grpChooser";
			this.grpChooser.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpChooser.RightToLeft")));
			this.grpChooser.Size = ((System.Drawing.Size)(resources.GetObject("grpChooser.Size")));
			this.grpChooser.TabIndex = ((int)(resources.GetObject("grpChooser.TabIndex")));
			this.grpChooser.TabStop = false;
			this.grpChooser.Text = resources.GetString("grpChooser.Text");
			this.tltToolTip.SetToolTip(this.grpChooser, resources.GetString("grpChooser.ToolTip"));
			this.grpChooser.Visible = ((bool)(resources.GetObject("grpChooser.Visible")));
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
			this.tltToolTip.SetToolTip(this.btnOK, resources.GetString("btnOK.ToolTip"));
			this.btnOK.Visible = ((bool)(resources.GetObject("btnOK.Visible")));
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
			this.tltToolTip.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
			this.btnCancel.Visible = ((bool)(resources.GetObject("btnCancel.Visible")));
			// 
			// stbStatusbar
			// 
			this.stbStatusbar.AccessibleDescription = ((string)(resources.GetObject("stbStatusbar.AccessibleDescription")));
			this.stbStatusbar.AccessibleName = ((string)(resources.GetObject("stbStatusbar.AccessibleName")));
			this.stbStatusbar.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("stbStatusbar.Anchor")));
			this.stbStatusbar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stbStatusbar.BackgroundImage")));
			this.stbStatusbar.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("stbStatusbar.Dock")));
			this.stbStatusbar.Enabled = ((bool)(resources.GetObject("stbStatusbar.Enabled")));
			this.stbStatusbar.Font = ((System.Drawing.Font)(resources.GetObject("stbStatusbar.Font")));
			this.stbStatusbar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("stbStatusbar.ImeMode")));
			this.stbStatusbar.Location = ((System.Drawing.Point)(resources.GetObject("stbStatusbar.Location")));
			this.stbStatusbar.Name = "stbStatusbar";
			this.stbStatusbar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																							this.sbpMainPanel});
			this.stbStatusbar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("stbStatusbar.RightToLeft")));
			this.stbStatusbar.ShowPanels = true;
			this.stbStatusbar.Size = ((System.Drawing.Size)(resources.GetObject("stbStatusbar.Size")));
			this.stbStatusbar.SizingGrip = false;
			this.stbStatusbar.TabIndex = ((int)(resources.GetObject("stbStatusbar.TabIndex")));
			this.stbStatusbar.Text = resources.GetString("stbStatusbar.Text");
			this.tltToolTip.SetToolTip(this.stbStatusbar, resources.GetString("stbStatusbar.ToolTip"));
			this.stbStatusbar.Visible = ((bool)(resources.GetObject("stbStatusbar.Visible")));
			// 
			// sbpMainPanel
			// 
			this.sbpMainPanel.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("sbpMainPanel.Alignment")));
			this.sbpMainPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpMainPanel.Icon = ((System.Drawing.Icon)(resources.GetObject("sbpMainPanel.Icon")));
			this.sbpMainPanel.MinWidth = ((int)(resources.GetObject("sbpMainPanel.MinWidth")));
			this.sbpMainPanel.Text = resources.GetString("sbpMainPanel.Text");
			this.sbpMainPanel.ToolTipText = resources.GetString("sbpMainPanel.ToolTipText");
			this.sbpMainPanel.Width = ((int)(resources.GetObject("sbpMainPanel.Width")));
			// 
			// m_chkSound
			// 
			this.m_chkSound.AccessibleDescription = ((string)(resources.GetObject("m_chkSound.AccessibleDescription")));
			this.m_chkSound.AccessibleName = ((string)(resources.GetObject("m_chkSound.AccessibleName")));
			this.m_chkSound.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_chkSound.Anchor")));
			this.m_chkSound.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("m_chkSound.Appearance")));
			this.m_chkSound.BackColor = System.Drawing.Color.Transparent;
			this.m_chkSound.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_chkSound.BackgroundImage")));
			this.m_chkSound.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_chkSound.CheckAlign")));
			this.m_chkSound.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_chkSound.Dock")));
			this.m_chkSound.Enabled = ((bool)(resources.GetObject("m_chkSound.Enabled")));
			this.m_chkSound.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_chkSound.FlatStyle")));
			this.m_chkSound.Font = ((System.Drawing.Font)(resources.GetObject("m_chkSound.Font")));
			this.m_chkSound.Image = ((System.Drawing.Image)(resources.GetObject("m_chkSound.Image")));
			this.m_chkSound.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_chkSound.ImageAlign")));
			this.m_chkSound.ImageIndex = ((int)(resources.GetObject("m_chkSound.ImageIndex")));
			this.m_chkSound.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_chkSound.ImeMode")));
			this.m_chkSound.Location = ((System.Drawing.Point)(resources.GetObject("m_chkSound.Location")));
			this.m_chkSound.Name = "m_chkSound";
			this.m_chkSound.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_chkSound.RightToLeft")));
			this.m_chkSound.Size = ((System.Drawing.Size)(resources.GetObject("m_chkSound.Size")));
			this.m_chkSound.TabIndex = ((int)(resources.GetObject("m_chkSound.TabIndex")));
			this.m_chkSound.Text = resources.GetString("m_chkSound.Text");
			this.m_chkSound.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_chkSound.TextAlign")));
			this.tltToolTip.SetToolTip(this.m_chkSound, resources.GetString("m_chkSound.ToolTip"));
			this.m_chkSound.Visible = ((bool)(resources.GetObject("m_chkSound.Visible")));
			// 
			// frmCalculator
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
																		  this.m_chkSound,
																		  this.stbStatusbar,
																		  this.btnCancel,
																		  this.btnOK,
																		  this.lblOutput,
																		  this.btnCompute,
																		  this.btnClearAll,
																		  this.btnBackSpace,
																		  this.btnSub,
																		  this.btnAdd,
																		  this.txtCalculate,
																		  this.grpChooser,
																		  this.btnAddToVectorsList});
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
			this.Name = "frmCalculator";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.ShowInTaskbar = false;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.tltToolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
			this.Visible = ((bool)(resources.GetObject("$this.Visible")));
			this.Load += new System.EventHandler(this.frmCalculator_Load);
			this.grpChooser.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.sbpMainPanel)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
	}

	/// <summary>
	/// Status bar commands.
	/// </summary>
	internal enum StatusbarCommands
	{
		None = 0,
		SelectVector = 1,
		SelectCommand = 2,
		SelectAddToVector = 3
	}
}


