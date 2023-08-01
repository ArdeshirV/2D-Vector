#region Header
// https://github.com/ArdeshirV/2D-Vector
// Copyright 2002-2004 ArdeshirV, Licensed Under MIT


using System;
//using ArdeshirV.Utility;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using ArdeshirV.Applications.Vector;
using ArdeshirV.Components.ScreenVector;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Applications.Vector
{
	/// <summary>
	/// Child Form in Vector Project.
	/// </summary>
	public class frmChild :
		System.Windows.Forms.Form
	{
		#region Variables

		private double m_intI = 0;
		private double m_intJ = 0;
		private bool m_blnIsSaved;
		private vector m_vctResult;
		private PageSettings m_pgsSetting;
		private string[] m_strStatusBarToolTips;
		private EventHandler m_Delegate_OnClosed;
		private static int s_intCountOfWindow = 1;
		private Point m_pntMouseDown = Point.Empty;
		private bool m_blnWaitForMouseDown = false;
		private System.Windows.Forms.ToolTip tltToolTip;
		private System.Windows.Forms.MainMenu mnuMainMenu;
		private System.Windows.Forms.MenuItem mnuChildSeperator1;
		private System.Windows.Forms.MenuItem mnuChildFile;
		private System.Windows.Forms.MenuItem mnuChildFileSave;
		private System.Windows.Forms.MenuItem mnuChildFileSaveAs;
		private System.Windows.Forms.MenuItem mnuChildFileSaveAPicture;
		private System.Windows.Forms.MenuItem mnuChildVector;
		private System.Windows.Forms.MenuItem mnuChildVectorNew;
		private System.Windows.Forms.MenuItem mnuChildVectorDeleteAll;
		private System.Windows.Forms.MenuItem mnuChildVectorDeleteLast;
		private System.Windows.Forms.MenuItem mnuChildVectorProperties;
		private System.Windows.Forms.MenuItem mnuChildFileSeperator2;
		private System.Windows.Forms.MenuItem mnuMakeCenter;
		private System.Windows.Forms.MenuItem mnuVectorSeperator;
		private System.Windows.Forms.MenuItem mnuAdd;
		private System.Windows.Forms.MenuItem mnuChildFileSeperator3;
		private System.Windows.Forms.MenuItem mnuChildFilePrintSetup;
		private System.Windows.Forms.MenuItem mnuChildFilePrint;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mnuReset;
		private System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
		private System.Windows.Forms.MenuItem mnuChildFilePrintPreview;
		private System.Windows.Forms.ContextMenu ctxMainContextMenu;
		private ScreenVector m_scrScreenVector;
		private System.Windows.Forms.MenuItem CtxMenu100;
		private System.Windows.Forms.MenuItem CtxMenu90;
		private System.Windows.Forms.MenuItem CtxMenu80;
		private System.Windows.Forms.MenuItem CtxMenu70;
		private System.Windows.Forms.MenuItem CtxMenu60;
		private System.Windows.Forms.MenuItem CtxMenu50;
		private System.Windows.Forms.MenuItem CtxMenu40;
		private System.Windows.Forms.MenuItem CtxMenu30;
		private System.Windows.Forms.MenuItem CtxMenu20;
		private System.Windows.Forms.MenuItem CtxMenuS2;
		private System.Windows.Forms.MenuItem CtxMenu110;
		private System.Windows.Forms.MenuItem CtxMenu120;
		private System.Windows.Forms.MenuItem CtxMenu130;
		private System.Windows.Forms.MenuItem CtxMenu140;
		private System.Windows.Forms.MenuItem CtxMenu150;
		private System.Windows.Forms.MenuItem menuItem26;
		private System.Windows.Forms.MenuItem CtxMenu160;
		private System.Windows.Forms.MenuItem CtxMenu170;
		private System.Windows.Forms.MenuItem CtxMenu180;
		private System.Windows.Forms.MenuItem CtxMenu190;
		private System.Windows.Forms.MenuItem CtxMenu200;
		private System.Windows.Forms.MenuItem CtxMenuS1;
		private System.Windows.Forms.MenuItem CtxMenuCustom;
		private System.Windows.Forms.MenuItem CtxMenuS3;
		private System.Windows.Forms.MenuItem CtxMenu10;
		private System.Windows.Forms.Button btnCenter;
		private System.Windows.Forms.Button btnResetToNormal;
		private System.Windows.Forms.Button btnCalculator;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDeleteLast;
		private System.Windows.Forms.Button btnDeleteAll;
		private System.Windows.Forms.GroupBox grpVectorChooser;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnProperties;
		private System.Windows.Forms.ComboBox cmbVectorSelector;
		private System.Windows.Forms.Label lblCommentForComboBox;
		private System.Windows.Forms.StatusBarPanel pnlLenghtOfCurrentVector;
		private System.Windows.Forms.StatusBarPanel pnlTotalVectors;
		private System.Windows.Forms.StatusBarPanel pnlMousePosition;
		private System.Windows.Forms.StatusBarPanel pnlResult;
		private System.Windows.Forms.StatusBarPanel pnlZoom;
		private System.Windows.Forms.StatusBarPanel pnlArrangeModeNormal;
		private System.Windows.Forms.StatusBarPanel pnlArrangeModeCenter;
		private System.Windows.Forms.StatusBarPanel pnlArrangeModeAdd;
		private System.Windows.Forms.StatusBar stbStatusBar;
		private System.Windows.Forms.MenuItem mnuChildTools;
		private System.Windows.Forms.MenuItem mnuChildToolsZoom;
		private System.Windows.Forms.MenuItem mnuChildToolsCalculator;
		private System.Windows.Forms.MenuItem mnuChildToolsS1;
		private System.Windows.Forms.MenuItem mnuChildVectorDeleteSelected;
		private System.Windows.Forms.MenuItem mnuVectorSeperatorM;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components;

		#endregion
		//-------------------------------------------------------------------------------
		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public frmChild(bool IsSaved, bool IsToolTipActive)
		{
			InitializeComponent();

			ScreenVectorInit();
			s_intCountOfWindow++;
			m_blnIsSaved = IsSaved;
			m_strStatusBarToolTips = new string[8];

			for(int l_intIndexer = 0; l_intIndexer < m_strStatusBarToolTips.Length; l_intIndexer++)
				m_strStatusBarToolTips[l_intIndexer] = stbStatusBar.Panels[l_intIndexer].ToolTipText;

			StatusBarToolTip(tltToolTip.Active = IsToolTipActive);
			m_pgsSetting = new PageSettings(new PrinterSettings());

#if !DEBUG
			//Opacity = 1.0;
			//DialogMode = false;
			//OpacityChangerInterval = -1;
			//BackgroundMode = LinearGradientMode.ForwardDiagonal;
#endif
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Events

		/// <summary>
		/// Occured whenever form has been closed.
		/// </summary>
		public event EventHandler ChildClosed
		{
			add
			{
				m_Delegate_OnClosed += value;
			}
			remove
			{
				m_Delegate_OnClosed -= value;
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Encapsulate Screen Vector Component.
		/// </summary>
		public ScreenVector Screen
		{
			get
			{
				return m_scrScreenVector;
			}
			set
			{
				m_scrScreenVector = value;
			}
		}

		/// <summary>
		/// Gets or sets tooltip activation.
		/// </summary>
		public bool IsActiveToolTip
		{
			get
			{
				return tltToolTip.Active;
			}
			set
			{
				StatusBarToolTip(tltToolTip.Active = value);
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Event Handlers

		/// <summary>
		/// Occured whenever child form has been loaded.
		/// </summary>
		/// <param name="sender">Child form</param>
		/// <param name="e">Event argument</param>
		private void frmChild_Load(object sender, System.EventArgs e)
		{
			ResetButtonEnabled();
		}

		/// <summary>
		/// Occurs before program has been closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void frmChild_Closing(object sender, CancelEventArgs e)
		{
			if(m_scrScreenVector.Modified && ((frmMainVectorForm)ParentForm).
				Option.blnQuestionBeforeCloseDocument)
			{
				System.IO.FileInfo info = new System.IO.FileInfo((string)Tag);
				switch(MessageBox.Show(this, "Save changes to " +
				                       
					//ArdeshirV.Utility.GlabalUtility.GetFileName((string)Tag, 7)
					info.Name + "?",
					"Vector Warning", MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Exclamation))
				{
					case DialogResult.Yes:
						mnuChildFileSave_Click(null, null);
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						return;
					case DialogResult.No:
						break;
				}
			}
		}
		
		/// <summary>
		/// Occurs whenever form has been closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void frmChild_Closed(object sender, System.EventArgs e)
		{
			if(m_Delegate_OnClosed != null)
				m_Delegate_OnClosed(this, null);
		}

		/// <summary>
		///Occurs whenever File Save Menu has been clicked(Save Current Document to file)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
        public void mnuChildFileSave_Click(object sender, System.EventArgs e)
		{
			if(m_blnIsSaved)
			{
				System.IO.FileInfo info = new System.IO.FileInfo((string)Tag);
				Text = 
					//ArdeshirV.Utility.GlabalUtility.GetFileName((string)Tag, 7)
					info.Name;
				Save();
			}
			else
				mnuChildFileSaveAs_Click(null, null);
		}

		/// <summary>
		/// Occurs whenever File Save as has been clicked(Save Current Document to file)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void mnuChildFileSaveAs_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog l_sfdSaveDialog = new SaveFileDialog();
			l_sfdSaveDialog.Title = "Save Current Vector Document";
			l_sfdSaveDialog.Filter = 
				"Vector Documents (*.vector)|*.vector|All Files (*.*)|*.*";
			l_sfdSaveDialog.DefaultExt = "vector";
			l_sfdSaveDialog.FileName = (string)Tag;
			if(l_sfdSaveDialog.ShowDialog(this) == DialogResult.OK)
			{
				Tag = l_sfdSaveDialog.FileName;
				Text = //ArdeshirV.Utility.GlabalUtility.GetFileName((string)
					//(Tag = l_sfdSaveDialog.FileName), 7);
					
					GetFileName(l_sfdSaveDialog.FileName);
				Save();
			}
		}

		/// <summary>
		/// Occurs whenever File Save a Image Clicked(Save Current Screen Image to file).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		public void mnuChildFileSaveAPicture_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog l_sfdSaveDialog = new SaveFileDialog();
			l_sfdSaveDialog.Title = "Save Active Screen as an Image";
			l_sfdSaveDialog.Filter = "JPEG (*.jpg)|*.jpg";
			l_sfdSaveDialog.FileName = GetFileName((string)Tag) + " Image.jpg";  //ArdeshirV.Utility.GlabalUtility.GetFileName((string)Tag, 7) +	" Image.jpg";

			if(l_sfdSaveDialog.ShowDialog(this) == DialogResult.OK)
				m_scrScreenVector.SaveScreenToFile(l_sfdSaveDialog.FileName);
		}

		/// <summary>
		/// Occurs whenever File Print Setup has been clicked(to show Setup Printer page)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		public void mnuChildFilePrintSetup_Click(object sender, System.EventArgs e)
		{
			PageSetupDialog l_psdSetup = new PageSetupDialog();

			try
			{
				if(m_pgsSetting == null)
					m_pgsSetting = new PageSettings();

				l_psdSetup.PageSettings = m_pgsSetting;
				l_psdSetup.ShowDialog(this);
			}
			catch(Exception exp)
			{
				MessageBox.Show(this, exp.Message, "Printing Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Occurs when ever print preview has been clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		public void mnuChildFilePrintPreview_Click(object sender, System.EventArgs e)
		{
			PrintPreviewDialog l_dlgPreview = new PrintPreviewDialog();

			try
			{
				l_dlgPreview.SetDesktopBounds(
					System.Windows.Forms.Screen.PrimaryScreen.Bounds.Top,
					System.Windows.Forms.Screen.PrimaryScreen.Bounds.Left,
					System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
					System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
				l_dlgPreview.Owner = ParentForm;
				l_dlgPreview.UseAntiAlias = true;
				l_dlgPreview.Document = m_pdcPrintDocument;
				l_dlgPreview.ShowDialog(this);
			}
			catch(Exception exp)
			{
				MessageBox.Show(this, exp.Message, "Printing Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Occurs whenever File Print has been clicked(Print Current Document).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		public void mnuChildFilePrint_Click(object sender, EventArgs e)
		{
			try
			{
				PrintScreenVector();
			}
			catch(Exception exp)
			{
				MessageBox.Show(this, exp.Message, "Printing Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Occurs wheneve vector popup menu drop down(for set radio menu checke).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void mnuChildVector_Popup(object sender, EventArgs e)
		{
			Checker(m_scrScreenVector.Mode);

			if(m_scrScreenVector.IsThereAnyVector)
			{
				mnuVectorSeperatorM.Visible =
				mnuChildVectorDeleteAll.Visible =
				mnuChildVectorDeleteLast.Visible = true;

				mnuChildSeperator1.Visible =
				mnuChildVectorProperties.Visible =
				mnuChildVectorDeleteSelected.Visible =
				cmbVectorSelector.SelectedIndex != -1;
			}
			else
			{
				mnuChildSeperator1.Visible =
				mnuVectorSeperatorM.Visible =
				mnuChildVectorDeleteAll.Visible =
				mnuChildVectorProperties.Visible =
				mnuChildVectorDeleteLast.Visible =
				mnuChildVectorDeleteSelected.Visible = false;
			}

			if(m_scrScreenVector.Mode == VectorScreenMode.Plus)
				ResultPanelUpdate(m_vctResult);
		}
		/// <summary>
		/// Occured whenever tools menu item has been clicked.
		/// </summary>
		/// <param name="sender">Tools menu item</param>
		/// <param name="e">Event argument</param>
		private void mnuChildTools_Popup(object sender, EventArgs e)
		{
			mnuChildToolsCalculator.Visible = m_scrScreenVector.IsThereAnyVector;
		}

		/// <summary>
		/// Draw menu item.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void Draw_Menu_Items(object sender,	DrawItemEventArgs e)
		{
			((frmMainVectorForm)ParentForm).Draw_Menu_Items(sender, e);
		}

		/// <summary>
		/// Measure menu items.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void Measure_Menu_Items(object sender, MeasureItemEventArgs e)
		{
			((frmMainVectorForm)ParentForm).Draw_Measure_Items(sender, e);
		}

		/// <summary>
		/// Occurs whenever Calculate Button has been clicked(for operate on vectors).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void btnCalculator_Click(object sender, System.EventArgs e)
		{
			if(m_scrScreenVector.Count <= 0)
				return;

			frmCalculator l_frmCalculate = new frmCalculator(
				m_scrScreenVector.Items, m_scrScreenVector.ActiveNumber, m_scrScreenVector.Height,
				tltToolTip.Active, ((frmMainVectorForm)ParentForm).Option.blnCaculatorBell);
			l_frmCalculate.ShowDialog(this);

			if(l_frmCalculate.Succed && l_frmCalculate.Items.Count > 0)
			{
				foreach(vector vct in l_frmCalculate.Items)
					m_scrScreenVector.AddNewVector(vct);

				pnlTotalVectors.Text = m_scrScreenVector.Count.ToString();
			}

			Environments_Variables l_env = ((frmMainVectorForm)ParentForm).Option;
			l_env.blnCaculatorBell = l_frmCalculate.SoundActive;
			((frmMainVectorForm)ParentForm).Option = l_env;
		}

		/// <summary>
		/// Occurs whenever Delete All has been Clicked(for delete all vector).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void btnDeleteAll_Click(object sender, System.EventArgs e)
		{
			if(!m_scrScreenVector.ClearAllVectors(((frmMainVectorForm)
				ParentForm).Option.blnQuestionBeforeDeleteAll))
				return;

			m_scrScreenVector.ActiveNumber = -1;
			ResetVectorSelector();
		}

		/// <summary>
		/// Occurs whenever delete last button has been clicked(for delete last vector 
		/// that created).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void btnDeleteLast_Click(object sender, System.EventArgs e)
		{
			m_scrScreenVector.DeleteLast();

			if(m_scrScreenVector.Count == m_scrScreenVector.ActiveNumber)
				m_scrScreenVector.ActiveNumber--;

			ResetVectorSelector();
		}

		/// <summary>
		/// Occurs whenever Add button has been clicked(for add new vector with custom
		/// properties).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			m_scrScreenVector.Mode = VectorScreenMode.Plus;
			Checker(VectorScreenMode.Plus);
		}

		/// <summary>
		/// Occurs whenever Center buttn has been clicked(start position for all 
		/// vectors convert to center screen vector).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void btnCenter_Click(object sender, System.EventArgs e)
		{
			m_scrScreenVector.Mode = VectorScreenMode.Center;
			Checker(VectorScreenMode.Center);
		}

		/// <summary>
		/// Occurs whenever Reset button has been clicked(Reset all vectors to normal 
		/// position).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void btnResetToNormal_Click(object sender, System.EventArgs e)
		{
			m_scrScreenVector.Mode = VectorScreenMode.Normal;
			Checker(VectorScreenMode.Normal);
		}

		/// <summary>
		/// Occurs whenever delete button has been clicked(delete activated vector).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
#if DEBUG
			MessageBox.Show(this.cmbVectorSelector.SelectedIndex.ToString());
#endif
			if(m_scrScreenVector.ActiveNumber == -1)
				return;

			m_scrScreenVector.DeleteAt(m_scrScreenVector.ActiveNumber);

			switch(m_scrScreenVector.ActiveNumber)
			{
				case 0:
					if(m_scrScreenVector.Count <= 0)
						m_scrScreenVector.ActiveNumber = -1;
					break;
				default:
					m_scrScreenVector.ActiveNumber--;
					break;
			}

			ResetVectorSelector();
		}

		/// <summary>
		/// Occurs whenever properties button has been clicked(get properties for 
		/// active vector).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void btnProperties_Click(object sender, System.EventArgs e)
		{
			if(m_scrScreenVector.ActiveNumber == -1)
				return;

			frmProperty l_frmProperty =
				new frmProperty(
				m_scrScreenVector[m_scrScreenVector.ActiveNumber],
				m_scrScreenVector.Width, m_scrScreenVector.Height);
			l_frmProperty.ShowDialog(this);

			if(l_frmProperty.Succed)
			{
				vector l_vctResult = l_frmProperty.Result;

				if(!l_vctResult.Equals(m_scrScreenVector[m_scrScreenVector.ActiveNumber]))
					m_scrScreenVector.SetModifiedFlag();

				l_vctResult.ID =
					m_scrScreenVector[m_scrScreenVector.ActiveNumber].ID;
				m_scrScreenVector[m_scrScreenVector.ActiveNumber] =
					l_vctResult;
			}

			l_frmProperty.Dispose();
			ResetVectorSelector();
		}

		/// <summary>
		/// Occurs whenever new button has been clicked(Cerate new wizard form).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void btnNew_Click(object sender, System.EventArgs e)
		{
			frmNewVectorWizard l_frmNew =
				new frmNewVectorWizard(m_scrScreenVector.Height);
			l_frmNew.OptionSelected += new OptionEventHandler(CreateVector);
			l_frmNew.ShowDialog(this);
		}

		/// <summary>
		/// Occurs when ever zoom has been changed.
		/// </summary>
		private void screenVector_ZoomChanged()
		{
			pnlZoom.Text = "%" + ((int)(m_scrScreenVector.Zoom * 100)).ToString();
		}

		/// <summary>
		/// Occurs whenever Mouse has been Leaved Screen Vector(reset status bar panels).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void screenVector_MouseLeave(object sender, System.EventArgs e)
		{
			pnlMousePosition.Text = "";
		}

		/// <summary>
		/// Occurs whenever Mouse has been moved over screen vector(rest status bar 
		/// panels).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void screenVector_MouseMove(object sender, MouseEventArgs e)
		{
			PointD l_pnt = vector.Convert(new PointD(e.X, e.Y),
				m_scrScreenVector.Width, m_scrScreenVector.Height);
			pnlMousePosition.Text = vector.Tester(l_pnt.X).ToString() + 
					", " + vector.Tester(-l_pnt.Y).ToString();

			if(e.Button == MouseButtons.Left)
			{
				float	l_fltI = (float)vector.Tester(e.X - m_pntMouseDown.X),
						l_fltJ = (float)vector.Tester(m_pntMouseDown.Y - e.Y);

				pnlLenghtOfCurrentVector.Text = "I= " + l_fltI +  ", J= " + l_fltJ +
					", Length = " + (float)Math.Sqrt(l_fltI * l_fltI + l_fltJ * l_fltJ);
			}
			else
				SetActiveVector();
		}

		/// <summary>
		/// Occurs whenever mouse button has been up(reset status bar panels).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void screenVector_MouseUp(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				ResetVectorSelector();
				pnlTotalVectors.Text = m_scrScreenVector.Count.ToString();
			}

			if(m_scrScreenVector.ActiveNumber == -1)
				pnlLenghtOfCurrentVector.Text = "";
			else
				SetActiveVector();
		}

		/// <summary>
		/// Occurs whenever Mouse button has been pressed(create vector from here or 
		/// create start point for none vector).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void screenVector_MouseDown(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				m_pntMouseDown.X = e.X;
				m_pntMouseDown.Y = e.Y;

				if(m_blnWaitForMouseDown)
				{
					m_scrScreenVector.AddNewVector(vector.Convert(
						new PointD(e.X, e.Y),
						m_scrScreenVector.Width, m_scrScreenVector.Height),
						vector.Convert(new PointD(e.X + m_intI, e.Y + m_intJ),
						m_scrScreenVector.Width, m_scrScreenVector.Height));
					m_blnWaitForMouseDown = false;
					ResetVectorSelector();
					m_scrScreenVector.Cursor = Cursors.Cross;
					cmbVectorSelector.SelectedIndex =
						cmbVectorSelector.Items.Count - 1;
					cmbVectorSelector.Focus();
				}
			}
		}

		/// <summary>
		/// Occurs whenever mouse has been entered to screen vector(reset active vector).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void screenVector_MouseEnter(object sender, System.EventArgs e)
		{
			SetActiveVector();
		}

		/// <summary>
		/// Occurs whenever document has been changed.
		/// </summary>
		private void screenVecotor_DocumentModified()
		{
			ResetButtonEnabled();
			Text = GetFileName((string)Tag) + "*";  //ArdeshirV.Utility.GlabalUtility.GetFileName((string)Tag, 7) + "*";
		}

		/// <summary>
		/// for Change Active vector.
		/// </summary>
		/// <param name="ActiveIndex"></param>
		private void SetActiveVector()
		{
			pnlLenghtOfCurrentVector.Text =
				(m_scrScreenVector.ActiveNumber == -1)? "" :
				"I= " + (float)m_scrScreenVector[m_scrScreenVector.ActiveNumber].I +
				", J= " + (float)-m_scrScreenVector[m_scrScreenVector.ActiveNumber].J +
				", Length = " + (float)m_scrScreenVector[m_scrScreenVector.ActiveNumber].VectorLenght;
		}

		/// <summary>
		/// Occured whenever document from file has been loaded.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void screenVector_LoadNewDocument(object sender, System.EventArgs e)
		{
			ResetVectorSelector();
		}

		/// <summary>
		/// Occured whenever user has been added all vectors.
		/// </summary>
		/// <param name="v">Result vector</param>
		public void ResultPanelUpdate(vector v)
		{
			if(m_scrScreenVector.Mode == VectorScreenMode.Plus)
				pnlResult.Text = "I = " + (float)v.I + ", J = " +
					(float)-v.J + ", Length = " + (float)v.VectorLenght;

			m_vctResult = v;
		}

		/// <summary>
		/// Occurs whenever mouse has been entered to combobox(rest combobox list 
		/// with new value and reset status bar panels).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void cmbVectorSelector_Enter(object sender, System.EventArgs e)
		{
			ResetVectorSelector();
		}

		/// <summary>
		/// Occurs whenever selection has been changed(reset active vector).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void cmbVectorSelecto_SelectedIndexChanged(object sender, EventArgs e)
		{
			m_scrScreenVector.ActiveNumber = ((ComboBox)sender).SelectedIndex;
			ResetButtonEnabled();
		}

		/// <summary>
		/// occur when file print.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void OnPrintPage(object sender, PrintPageEventArgs e)
		{
			try
			{
				Graphics g = e.Graphics;
				m_pdcPrintDocument.DocumentName = Text;
				g.DrawImage(m_scrScreenVector.ScreeenImage, e.MarginBounds);
			}
			finally
			{
				e.HasMorePages = false;
			}
		}

		/// <summary>
		/// Occurs whenever each Context Menu Items has been clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Event argument</param>
		private void Ctx_Click(object sender, System.EventArgs e)
		{
			MenuItem l_mnu = (MenuItem)sender;
#if DEBUG
			MessageBox.Show(l_mnu.Index.ToString() + "  " + l_mnu.Text.Substring(1));
#endif
			if(l_mnu.Index == 23)
				ShowZoomDialig();
			else
				m_scrScreenVector.Zoom = double.Parse(l_mnu.Text.Substring(1))/ 100;
		}

		/// <summary>
		/// Occured whenever zoom size has been changed.
		/// </summary>
		/// <param name="sender">Child form</param>
		/// <param name="e">Event argument</param>
		private void mnuChildToolsZoom_Click(object sender, EventArgs e)
		{
			ShowZoomDialig();
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Utility Functions

		/// <summary>
		/// Make active or deactive status bar tool tip.
		/// </summary>
		/// <param name="bln">Status bar tool tip ativity</param>
		private void StatusBarToolTip(bool blnActivity)
		{
			if(blnActivity)
				for(int l_intIndexer = 0; l_intIndexer < m_strStatusBarToolTips.Length; l_intIndexer++)
					stbStatusBar.Panels[l_intIndexer].ToolTipText = m_strStatusBarToolTips[l_intIndexer];
			else
				for(int l_intIndexer = 0; l_intIndexer < m_strStatusBarToolTips.Length; l_intIndexer++)
					stbStatusBar.Panels[l_intIndexer].ToolTipText = "";
		}

		/// <summary>
		/// for handled create vecctor event in (New Wizard Form).
		/// </summary>
		/// <param name="StartPoint">Start point</param>
		/// <param name="EndPoint">End point</param>
		/// <param name="I">I coordinate</param>
		/// <param name="J">J coordinate</param>
		private void CreateVector(PointD StartPoint, PointD EndPoint,
								  double I, double J, bool bln)
		{
			if(!StartPoint.Equals(EndPoint) && !bln)
			{
				cmbVectorSelector.SelectedIndex = cmbVectorSelector.Items.Count-1;
				m_scrScreenVector.AddNewVector(StartPoint,EndPoint);
				ResetVectorSelector();
				cmbVectorSelector.Focus();
			}
			else if(bln)
			{
				m_intI = I;
				m_intJ = J;
				m_blnWaitForMouseDown = true;
				m_scrScreenVector.Cursor = System.Windows.Forms.Cursors.NoMove2D;
			}
		}

		/// <summary>
		/// Show zoom dialog for change current zoom size.
		/// </summary>
		private void ShowZoomDialig()
		{
			frmCustomZoom l_frmCustomZoom =
				new frmCustomZoom((int)(m_scrScreenVector.Zoom * 100));
			l_frmCustomZoom.ShowDialog(this);

			if(l_frmCustomZoom.IsChangedZoom)
				m_scrScreenVector.Zoom =
					(double)((double)l_frmCustomZoom.Value / 100);
		}

		/// <summary>
		/// Checke all radio menu items.
		/// </summary>
		/// <param name="index"></param>
		private void Checker(VectorScreenMode index)
		{
			pnlResult.Text = "";
			btnAdd.Enabled = true;
			mnuAdd.Checked = false;
			mnuReset.Checked = false;
			btnCenter.Enabled = true;
			mnuMakeCenter.Checked = false;
			btnResetToNormal.Enabled = true;
			btnAdd.FlatStyle = FlatStyle.Standard;
			btnCenter.FlatStyle = FlatStyle.Standard;
			btnResetToNormal.FlatStyle = FlatStyle.Standard;
			ChooseArrangeModePanel(index);

			switch(index)
			{
				case VectorScreenMode.Normal:
					mnuReset.Checked = true;
					btnResetToNormal.Enabled = false;
					btnResetToNormal.FlatStyle = FlatStyle.Flat;
					break;
				case VectorScreenMode.Center:
					btnCenter.Enabled = false;
					mnuMakeCenter.Checked = true;
					btnCenter.FlatStyle = FlatStyle.Flat;
					break;
				case VectorScreenMode.Plus:
					mnuAdd.Checked = true;
					btnAdd.Enabled = false;
					btnAdd.FlatStyle = FlatStyle.Flat;
					break;
#if DEBUG
				default:
				 throw new Exception(
					"Error in [void Checker(...) in switch statement]");
#endif
			}
		}

		/// <summary>
		/// Save Current Document to File.
		/// </summary>
		private void Save()
		{
			m_scrScreenVector.SaveToFile((string)Tag);
			m_blnIsSaved = true;
		}

		/// <summary>
		/// Run for print current screen.
		/// </summary>
		/// <param name="ChildForm"></param>
		private void PrintScreenVector()
		{
			PrintDialog l_dlgPrint = new PrintDialog();
			l_dlgPrint.Document = m_pdcPrintDocument;

			if(l_dlgPrint.ShowDialog(this) == DialogResult.OK)
			{
				try
				{
					if(m_pgsSetting != null)
						m_pdcPrintDocument.DefaultPageSettings = m_pgsSetting;

					m_pdcPrintDocument.Print();
				}
				catch
				{
				}
				finally
				{
					l_dlgPrint.Dispose();
				}
			}
		}

		/// <summary>
		/// initial Screen Vector.
		/// </summary>
		private void ScreenVectorInit()
		{
			SuspendLayout();

			m_scrScreenVector = new ScreenVector();
			m_scrScreenVector.ContextMenu = ctxMainContextMenu;
			m_scrScreenVector.Size = new Size(ClientSize.Width - 8, 
				ClientSize.Height - (stbStatusBar.Height + 50));
			m_scrScreenVector.Location = new Point(4, 46);
			m_scrScreenVector.Anchor = AnchorStyles.Bottom | AnchorStyles.Top |
				AnchorStyles.Left | AnchorStyles.Right;
			Controls.Add(m_scrScreenVector);
			m_scrScreenVector.AddAllVectors +=
				new Plused(ResultPanelUpdate);
			m_scrScreenVector.MouseDown +=
				new MouseEventHandler(screenVector_MouseDown);
			m_scrScreenVector.MouseEnter +=
				new EventHandler(screenVector_MouseEnter);
			m_scrScreenVector.MouseMove +=
				new MouseEventHandler(screenVector_MouseMove);
			m_scrScreenVector.MouseLeave +=
				new EventHandler(screenVector_MouseLeave);
			m_scrScreenVector.MouseUp +=
				new MouseEventHandler(screenVector_MouseUp);
			m_scrScreenVector.SelectVector +=
				new ReActive(SetActiveVector);
			m_scrScreenVector.DocumentModified +=
				new ReActive(screenVecotor_DocumentModified);
			m_scrScreenVector.ZoomChanged +=
				new ReActive(screenVector_ZoomChanged);
			m_scrScreenVector.OnLoadFromFile +=
				new EventHandler(screenVector_LoadNewDocument);

			ResumeLayout();
		}

		/// <summary>
		/// Choose the best of panle for current arrange modes.
		/// </summary>
		/// <param name="Index">Arrange mode</param>
		private void ChooseArrangeModePanel(VectorScreenMode Index)
		{
			SuspendLayout();

			pnlArrangeModeAdd.Width = 0;
			pnlArrangeModeCenter.Width = 0;
			pnlArrangeModeNormal.Width = 0;

			switch(Index)
			{
				case VectorScreenMode.Normal:
					pnlArrangeModeNormal.Width = 67;
					break;
				case VectorScreenMode.Center:
					pnlArrangeModeCenter.Width = 67;
					break;
				case VectorScreenMode.Plus:
					pnlArrangeModeAdd.Width = 67;
					break;
			}

			ResumeLayout();
		}

		/// <summary>
		/// Reset vector selector with vector array.
		/// </summary>
		private void ResetVectorSelector()
		{
			cmbVectorSelector.Items.Clear();
			int[] arr = m_scrScreenVector.GetArrOfIDs();

			foreach(int i in arr)
				cmbVectorSelector.Items.Add(i.ToString());

			pnlTotalVectors.Text = m_scrScreenVector.Count.ToString();
			cmbVectorSelector.SelectedIndex = m_scrScreenVector.ActiveNumber;
			ResetButtonEnabled();
		}

		/// <summary>
		/// Reset enabled propery of some buttons.
		/// </summary>
		private void ResetButtonEnabled()
		{
			if(Screen.IsThereAnyVector)
			{
				btnDeleteAll.Enabled =
				btnCalculator.Enabled =
				btnDeleteLast.Enabled = true;

				btnDelete.Enabled =
				btnProperties.Enabled =
				cmbVectorSelector.SelectedIndex != -1;
			}
			else
			{
				btnDelete.Enabled =
				btnDeleteAll.Enabled =
				btnProperties.Enabled =
				btnCalculator.Enabled =
				btnDeleteLast.Enabled = false;
			}
		}
		
		private string GetFileName(string fullPathName) {
			return new System.IO.FileInfo(fullPathName).Name;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmChild));
			this.stbStatusBar = new System.Windows.Forms.StatusBar();
			this.pnlMousePosition = new System.Windows.Forms.StatusBarPanel();
			this.pnlLenghtOfCurrentVector = new System.Windows.Forms.StatusBarPanel();
			this.pnlResult = new System.Windows.Forms.StatusBarPanel();
			this.pnlTotalVectors = new System.Windows.Forms.StatusBarPanel();
			this.pnlZoom = new System.Windows.Forms.StatusBarPanel();
			this.pnlArrangeModeNormal = new System.Windows.Forms.StatusBarPanel();
			this.pnlArrangeModeCenter = new System.Windows.Forms.StatusBarPanel();
			this.pnlArrangeModeAdd = new System.Windows.Forms.StatusBarPanel();
			this.mnuMainMenu = new System.Windows.Forms.MainMenu();
			this.mnuChildFile = new System.Windows.Forms.MenuItem();
			this.mnuChildFileSave = new System.Windows.Forms.MenuItem();
			this.mnuChildFileSaveAs = new System.Windows.Forms.MenuItem();
			this.mnuChildFileSaveAPicture = new System.Windows.Forms.MenuItem();
			this.mnuChildFileSeperator2 = new System.Windows.Forms.MenuItem();
			this.mnuChildFilePrintSetup = new System.Windows.Forms.MenuItem();
			this.mnuChildFilePrintPreview = new System.Windows.Forms.MenuItem();
			this.mnuChildFilePrint = new System.Windows.Forms.MenuItem();
			this.mnuChildFileSeperator3 = new System.Windows.Forms.MenuItem();
			this.mnuChildVector = new System.Windows.Forms.MenuItem();
			this.mnuChildVectorNew = new System.Windows.Forms.MenuItem();
			this.mnuVectorSeperatorM = new System.Windows.Forms.MenuItem();
			this.mnuChildVectorDeleteSelected = new System.Windows.Forms.MenuItem();
			this.mnuChildVectorDeleteLast = new System.Windows.Forms.MenuItem();
			this.mnuChildVectorDeleteAll = new System.Windows.Forms.MenuItem();
			this.mnuVectorSeperator = new System.Windows.Forms.MenuItem();
			this.mnuReset = new System.Windows.Forms.MenuItem();
			this.mnuMakeCenter = new System.Windows.Forms.MenuItem();
			this.mnuAdd = new System.Windows.Forms.MenuItem();
			this.mnuChildSeperator1 = new System.Windows.Forms.MenuItem();
			this.mnuChildVectorProperties = new System.Windows.Forms.MenuItem();
			this.mnuChildTools = new System.Windows.Forms.MenuItem();
			this.mnuChildToolsZoom = new System.Windows.Forms.MenuItem();
			this.mnuChildToolsCalculator = new System.Windows.Forms.MenuItem();
			this.mnuChildToolsS1 = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.ctxMainContextMenu = new System.Windows.Forms.ContextMenu();
			this.CtxMenu10 = new System.Windows.Forms.MenuItem();
			this.CtxMenu20 = new System.Windows.Forms.MenuItem();
			this.CtxMenu30 = new System.Windows.Forms.MenuItem();
			this.CtxMenu40 = new System.Windows.Forms.MenuItem();
			this.CtxMenu50 = new System.Windows.Forms.MenuItem();
			this.CtxMenu60 = new System.Windows.Forms.MenuItem();
			this.CtxMenu70 = new System.Windows.Forms.MenuItem();
			this.CtxMenu80 = new System.Windows.Forms.MenuItem();
			this.CtxMenu90 = new System.Windows.Forms.MenuItem();
			this.CtxMenuS1 = new System.Windows.Forms.MenuItem();
			this.CtxMenu100 = new System.Windows.Forms.MenuItem();
			this.CtxMenuS2 = new System.Windows.Forms.MenuItem();
			this.CtxMenu110 = new System.Windows.Forms.MenuItem();
			this.CtxMenu120 = new System.Windows.Forms.MenuItem();
			this.CtxMenu130 = new System.Windows.Forms.MenuItem();
			this.CtxMenu140 = new System.Windows.Forms.MenuItem();
			this.CtxMenu150 = new System.Windows.Forms.MenuItem();
			this.CtxMenu160 = new System.Windows.Forms.MenuItem();
			this.CtxMenu170 = new System.Windows.Forms.MenuItem();
			this.CtxMenu180 = new System.Windows.Forms.MenuItem();
			this.CtxMenu190 = new System.Windows.Forms.MenuItem();
			this.CtxMenu200 = new System.Windows.Forms.MenuItem();
			this.CtxMenuS3 = new System.Windows.Forms.MenuItem();
			this.CtxMenuCustom = new System.Windows.Forms.MenuItem();
			this.tltToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.btnCenter = new System.Windows.Forms.Button();
			this.btnResetToNormal = new System.Windows.Forms.Button();
			this.btnCalculator = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDeleteLast = new System.Windows.Forms.Button();
			this.btnDeleteAll = new System.Windows.Forms.Button();
			this.grpVectorChooser = new System.Windows.Forms.GroupBox();
			this.cmbVectorSelector = new System.Windows.Forms.ComboBox();
			this.lblCommentForComboBox = new System.Windows.Forms.Label();
			this.btnProperties = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
			this.menuItem26 = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.pnlMousePosition)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlLenghtOfCurrentVector)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlResult)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlTotalVectors)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlZoom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlArrangeModeNormal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlArrangeModeCenter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlArrangeModeAdd)).BeginInit();
			this.grpVectorChooser.SuspendLayout();
			this.SuspendLayout();
			// 
			// stbStatusBar
			// 
			this.stbStatusBar.AccessibleDescription = ((string)(resources.GetObject("stbStatusBar.AccessibleDescription")));
			this.stbStatusBar.AccessibleName = ((string)(resources.GetObject("stbStatusBar.AccessibleName")));
			this.stbStatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("stbStatusBar.Anchor")));
			this.stbStatusBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stbStatusBar.BackgroundImage")));
			this.stbStatusBar.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("stbStatusBar.Dock")));
			this.stbStatusBar.Enabled = ((bool)(resources.GetObject("stbStatusBar.Enabled")));
			this.stbStatusBar.Font = ((System.Drawing.Font)(resources.GetObject("stbStatusBar.Font")));
			this.stbStatusBar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("stbStatusBar.ImeMode")));
			this.stbStatusBar.Location = ((System.Drawing.Point)(resources.GetObject("stbStatusBar.Location")));
			this.stbStatusBar.Name = "stbStatusBar";
			this.stbStatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																							this.pnlMousePosition,
																							this.pnlLenghtOfCurrentVector,
																							this.pnlResult,
																							this.pnlTotalVectors,
																							this.pnlZoom,
																							this.pnlArrangeModeNormal,
																							this.pnlArrangeModeCenter,
																							this.pnlArrangeModeAdd});
			this.stbStatusBar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("stbStatusBar.RightToLeft")));
			this.stbStatusBar.ShowPanels = true;
			this.stbStatusBar.Size = ((System.Drawing.Size)(resources.GetObject("stbStatusBar.Size")));
			this.stbStatusBar.TabIndex = ((int)(resources.GetObject("stbStatusBar.TabIndex")));
			this.stbStatusBar.Text = resources.GetString("stbStatusBar.Text");
			this.tltToolTip.SetToolTip(this.stbStatusBar, resources.GetString("stbStatusBar.ToolTip"));
			this.stbStatusBar.Visible = ((bool)(resources.GetObject("stbStatusBar.Visible")));
			// 
			// pnlMousePosition
			// 
			this.pnlMousePosition.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlMousePosition.Alignment")));
			this.pnlMousePosition.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.pnlMousePosition.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlMousePosition.Icon")));
			this.pnlMousePosition.MinWidth = ((int)(resources.GetObject("pnlMousePosition.MinWidth")));
			this.pnlMousePosition.Text = resources.GetString("pnlMousePosition.Text");
			this.pnlMousePosition.ToolTipText = resources.GetString("pnlMousePosition.ToolTipText");
			this.pnlMousePosition.Width = ((int)(resources.GetObject("pnlMousePosition.Width")));
			// 
			// pnlLenghtOfCurrentVector
			// 
			this.pnlLenghtOfCurrentVector.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlLenghtOfCurrentVector.Alignment")));
			this.pnlLenghtOfCurrentVector.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.pnlLenghtOfCurrentVector.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlLenghtOfCurrentVector.Icon")));
			this.pnlLenghtOfCurrentVector.MinWidth = ((int)(resources.GetObject("pnlLenghtOfCurrentVector.MinWidth")));
			this.pnlLenghtOfCurrentVector.Text = resources.GetString("pnlLenghtOfCurrentVector.Text");
			this.pnlLenghtOfCurrentVector.ToolTipText = resources.GetString("pnlLenghtOfCurrentVector.ToolTipText");
			this.pnlLenghtOfCurrentVector.Width = ((int)(resources.GetObject("pnlLenghtOfCurrentVector.Width")));
			// 
			// pnlResult
			// 
			this.pnlResult.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlResult.Alignment")));
			this.pnlResult.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.pnlResult.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlResult.Icon")));
			this.pnlResult.MinWidth = ((int)(resources.GetObject("pnlResult.MinWidth")));
			this.pnlResult.Text = resources.GetString("pnlResult.Text");
			this.pnlResult.ToolTipText = resources.GetString("pnlResult.ToolTipText");
			this.pnlResult.Width = ((int)(resources.GetObject("pnlResult.Width")));
			// 
			// pnlTotalVectors
			// 
			this.pnlTotalVectors.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlTotalVectors.Alignment")));
			this.pnlTotalVectors.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.pnlTotalVectors.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlTotalVectors.Icon")));
			this.pnlTotalVectors.MinWidth = ((int)(resources.GetObject("pnlTotalVectors.MinWidth")));
			this.pnlTotalVectors.Text = resources.GetString("pnlTotalVectors.Text");
			this.pnlTotalVectors.ToolTipText = resources.GetString("pnlTotalVectors.ToolTipText");
			this.pnlTotalVectors.Width = ((int)(resources.GetObject("pnlTotalVectors.Width")));
			// 
			// pnlZoom
			// 
			this.pnlZoom.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlZoom.Alignment")));
			this.pnlZoom.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlZoom.Icon")));
			this.pnlZoom.MinWidth = ((int)(resources.GetObject("pnlZoom.MinWidth")));
			this.pnlZoom.Text = resources.GetString("pnlZoom.Text");
			this.pnlZoom.ToolTipText = resources.GetString("pnlZoom.ToolTipText");
			this.pnlZoom.Width = ((int)(resources.GetObject("pnlZoom.Width")));
			// 
			// pnlArrangeModeNormal
			// 
			this.pnlArrangeModeNormal.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlArrangeModeNormal.Alignment")));
			this.pnlArrangeModeNormal.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlArrangeModeNormal.Icon")));
			this.pnlArrangeModeNormal.MinWidth = ((int)(resources.GetObject("pnlArrangeModeNormal.MinWidth")));
			this.pnlArrangeModeNormal.Text = resources.GetString("pnlArrangeModeNormal.Text");
			this.pnlArrangeModeNormal.ToolTipText = resources.GetString("pnlArrangeModeNormal.ToolTipText");
			this.pnlArrangeModeNormal.Width = ((int)(resources.GetObject("pnlArrangeModeNormal.Width")));
			// 
			// pnlArrangeModeCenter
			// 
			this.pnlArrangeModeCenter.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlArrangeModeCenter.Alignment")));
			this.pnlArrangeModeCenter.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlArrangeModeCenter.Icon")));
			this.pnlArrangeModeCenter.MinWidth = ((int)(resources.GetObject("pnlArrangeModeCenter.MinWidth")));
			this.pnlArrangeModeCenter.Text = resources.GetString("pnlArrangeModeCenter.Text");
			this.pnlArrangeModeCenter.ToolTipText = resources.GetString("pnlArrangeModeCenter.ToolTipText");
			this.pnlArrangeModeCenter.Width = ((int)(resources.GetObject("pnlArrangeModeCenter.Width")));
			// 
			// pnlArrangeModeAdd
			// 
			this.pnlArrangeModeAdd.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlArrangeModeAdd.Alignment")));
			this.pnlArrangeModeAdd.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlArrangeModeAdd.Icon")));
			this.pnlArrangeModeAdd.MinWidth = ((int)(resources.GetObject("pnlArrangeModeAdd.MinWidth")));
			this.pnlArrangeModeAdd.Text = resources.GetString("pnlArrangeModeAdd.Text");
			this.pnlArrangeModeAdd.ToolTipText = resources.GetString("pnlArrangeModeAdd.ToolTipText");
			this.pnlArrangeModeAdd.Width = ((int)(resources.GetObject("pnlArrangeModeAdd.Width")));
			// 
			// mnuMainMenu
			// 
			this.mnuMainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mnuChildFile,
																						this.mnuChildVector,
																						this.mnuChildTools});
			this.mnuMainMenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mnuMainMenu.RightToLeft")));
			// 
			// mnuChildFile
			// 
			this.mnuChildFile.Enabled = ((bool)(resources.GetObject("mnuChildFile.Enabled")));
			this.mnuChildFile.Index = 0;
			this.mnuChildFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnuChildFileSave,
																						 this.mnuChildFileSaveAs,
																						 this.mnuChildFileSaveAPicture,
																						 this.mnuChildFileSeperator2,
																						 this.mnuChildFilePrintSetup,
																						 this.mnuChildFilePrintPreview,
																						 this.mnuChildFilePrint,
																						 this.mnuChildFileSeperator3});
			this.mnuChildFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.mnuChildFile.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildFile.Shortcut")));
			this.mnuChildFile.ShowShortcut = ((bool)(resources.GetObject("mnuChildFile.ShowShortcut")));
			this.mnuChildFile.Text = resources.GetString("mnuChildFile.Text");
			this.mnuChildFile.Visible = ((bool)(resources.GetObject("mnuChildFile.Visible")));
			// 
			// mnuChildFileSave
			// 
			this.mnuChildFileSave.Enabled = ((bool)(resources.GetObject("mnuChildFileSave.Enabled")));
			this.mnuChildFileSave.Index = 0;
			this.mnuChildFileSave.OwnerDraw = true;
			this.mnuChildFileSave.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildFileSave.Shortcut")));
			this.mnuChildFileSave.ShowShortcut = ((bool)(resources.GetObject("mnuChildFileSave.ShowShortcut")));
			this.mnuChildFileSave.Text = resources.GetString("mnuChildFileSave.Text");
			this.mnuChildFileSave.Visible = ((bool)(resources.GetObject("mnuChildFileSave.Visible")));
			this.mnuChildFileSave.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildFileSave.Click += new System.EventHandler(this.mnuChildFileSave_Click);
			this.mnuChildFileSave.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildFileSaveAs
			// 
			this.mnuChildFileSaveAs.Enabled = ((bool)(resources.GetObject("mnuChildFileSaveAs.Enabled")));
			this.mnuChildFileSaveAs.Index = 1;
			this.mnuChildFileSaveAs.OwnerDraw = true;
			this.mnuChildFileSaveAs.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildFileSaveAs.Shortcut")));
			this.mnuChildFileSaveAs.ShowShortcut = ((bool)(resources.GetObject("mnuChildFileSaveAs.ShowShortcut")));
			this.mnuChildFileSaveAs.Text = resources.GetString("mnuChildFileSaveAs.Text");
			this.mnuChildFileSaveAs.Visible = ((bool)(resources.GetObject("mnuChildFileSaveAs.Visible")));
			this.mnuChildFileSaveAs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildFileSaveAs.Click += new System.EventHandler(this.mnuChildFileSaveAs_Click);
			this.mnuChildFileSaveAs.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildFileSaveAPicture
			// 
			this.mnuChildFileSaveAPicture.Enabled = ((bool)(resources.GetObject("mnuChildFileSaveAPicture.Enabled")));
			this.mnuChildFileSaveAPicture.Index = 2;
			this.mnuChildFileSaveAPicture.OwnerDraw = true;
			this.mnuChildFileSaveAPicture.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildFileSaveAPicture.Shortcut")));
			this.mnuChildFileSaveAPicture.ShowShortcut = ((bool)(resources.GetObject("mnuChildFileSaveAPicture.ShowShortcut")));
			this.mnuChildFileSaveAPicture.Text = resources.GetString("mnuChildFileSaveAPicture.Text");
			this.mnuChildFileSaveAPicture.Visible = ((bool)(resources.GetObject("mnuChildFileSaveAPicture.Visible")));
			this.mnuChildFileSaveAPicture.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildFileSaveAPicture.Click += new System.EventHandler(this.mnuChildFileSaveAPicture_Click);
			this.mnuChildFileSaveAPicture.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildFileSeperator2
			// 
			this.mnuChildFileSeperator2.Enabled = ((bool)(resources.GetObject("mnuChildFileSeperator2.Enabled")));
			this.mnuChildFileSeperator2.Index = 3;
			this.mnuChildFileSeperator2.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildFileSeperator2.Shortcut")));
			this.mnuChildFileSeperator2.ShowShortcut = ((bool)(resources.GetObject("mnuChildFileSeperator2.ShowShortcut")));
			this.mnuChildFileSeperator2.Text = resources.GetString("mnuChildFileSeperator2.Text");
			this.mnuChildFileSeperator2.Visible = ((bool)(resources.GetObject("mnuChildFileSeperator2.Visible")));
			// 
			// mnuChildFilePrintSetup
			// 
			this.mnuChildFilePrintSetup.Enabled = ((bool)(resources.GetObject("mnuChildFilePrintSetup.Enabled")));
			this.mnuChildFilePrintSetup.Index = 4;
			this.mnuChildFilePrintSetup.OwnerDraw = true;
			this.mnuChildFilePrintSetup.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildFilePrintSetup.Shortcut")));
			this.mnuChildFilePrintSetup.ShowShortcut = ((bool)(resources.GetObject("mnuChildFilePrintSetup.ShowShortcut")));
			this.mnuChildFilePrintSetup.Text = resources.GetString("mnuChildFilePrintSetup.Text");
			this.mnuChildFilePrintSetup.Visible = ((bool)(resources.GetObject("mnuChildFilePrintSetup.Visible")));
			this.mnuChildFilePrintSetup.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildFilePrintSetup.Click += new System.EventHandler(this.mnuChildFilePrintSetup_Click);
			this.mnuChildFilePrintSetup.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildFilePrintPreview
			// 
			this.mnuChildFilePrintPreview.Enabled = ((bool)(resources.GetObject("mnuChildFilePrintPreview.Enabled")));
			this.mnuChildFilePrintPreview.Index = 5;
			this.mnuChildFilePrintPreview.OwnerDraw = true;
			this.mnuChildFilePrintPreview.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildFilePrintPreview.Shortcut")));
			this.mnuChildFilePrintPreview.ShowShortcut = ((bool)(resources.GetObject("mnuChildFilePrintPreview.ShowShortcut")));
			this.mnuChildFilePrintPreview.Text = resources.GetString("mnuChildFilePrintPreview.Text");
			this.mnuChildFilePrintPreview.Visible = ((bool)(resources.GetObject("mnuChildFilePrintPreview.Visible")));
			this.mnuChildFilePrintPreview.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildFilePrintPreview.Click += new System.EventHandler(this.mnuChildFilePrintPreview_Click);
			this.mnuChildFilePrintPreview.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildFilePrint
			// 
			this.mnuChildFilePrint.Enabled = ((bool)(resources.GetObject("mnuChildFilePrint.Enabled")));
			this.mnuChildFilePrint.Index = 6;
			this.mnuChildFilePrint.OwnerDraw = true;
			this.mnuChildFilePrint.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildFilePrint.Shortcut")));
			this.mnuChildFilePrint.ShowShortcut = ((bool)(resources.GetObject("mnuChildFilePrint.ShowShortcut")));
			this.mnuChildFilePrint.Text = resources.GetString("mnuChildFilePrint.Text");
			this.mnuChildFilePrint.Visible = ((bool)(resources.GetObject("mnuChildFilePrint.Visible")));
			this.mnuChildFilePrint.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildFilePrint.Click += new System.EventHandler(this.mnuChildFilePrint_Click);
			this.mnuChildFilePrint.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildFileSeperator3
			// 
			this.mnuChildFileSeperator3.Enabled = ((bool)(resources.GetObject("mnuChildFileSeperator3.Enabled")));
			this.mnuChildFileSeperator3.Index = 7;
			this.mnuChildFileSeperator3.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildFileSeperator3.Shortcut")));
			this.mnuChildFileSeperator3.ShowShortcut = ((bool)(resources.GetObject("mnuChildFileSeperator3.ShowShortcut")));
			this.mnuChildFileSeperator3.Text = resources.GetString("mnuChildFileSeperator3.Text");
			this.mnuChildFileSeperator3.Visible = ((bool)(resources.GetObject("mnuChildFileSeperator3.Visible")));
			// 
			// mnuChildVector
			// 
			this.mnuChildVector.Enabled = ((bool)(resources.GetObject("mnuChildVector.Enabled")));
			this.mnuChildVector.Index = 1;
			this.mnuChildVector.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.mnuChildVectorNew,
																						   this.mnuVectorSeperatorM,
																						   this.mnuChildVectorDeleteSelected,
																						   this.mnuChildVectorDeleteLast,
																						   this.mnuChildVectorDeleteAll,
																						   this.mnuVectorSeperator,
																						   this.mnuReset,
																						   this.mnuMakeCenter,
																						   this.mnuAdd,
																						   this.mnuChildSeperator1,
																						   this.mnuChildVectorProperties});
			this.mnuChildVector.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildVector.Shortcut")));
			this.mnuChildVector.ShowShortcut = ((bool)(resources.GetObject("mnuChildVector.ShowShortcut")));
			this.mnuChildVector.Text = resources.GetString("mnuChildVector.Text");
			this.mnuChildVector.Visible = ((bool)(resources.GetObject("mnuChildVector.Visible")));
			this.mnuChildVector.Popup += new System.EventHandler(this.mnuChildVector_Popup);
			// 
			// mnuChildVectorNew
			// 
			this.mnuChildVectorNew.Enabled = ((bool)(resources.GetObject("mnuChildVectorNew.Enabled")));
			this.mnuChildVectorNew.Index = 0;
			this.mnuChildVectorNew.OwnerDraw = true;
			this.mnuChildVectorNew.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildVectorNew.Shortcut")));
			this.mnuChildVectorNew.ShowShortcut = ((bool)(resources.GetObject("mnuChildVectorNew.ShowShortcut")));
			this.mnuChildVectorNew.Text = resources.GetString("mnuChildVectorNew.Text");
			this.mnuChildVectorNew.Visible = ((bool)(resources.GetObject("mnuChildVectorNew.Visible")));
			this.mnuChildVectorNew.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildVectorNew.Click += new System.EventHandler(this.btnNew_Click);
			this.mnuChildVectorNew.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuVectorSeperatorM
			// 
			this.mnuVectorSeperatorM.Enabled = ((bool)(resources.GetObject("mnuVectorSeperatorM.Enabled")));
			this.mnuVectorSeperatorM.Index = 1;
			this.mnuVectorSeperatorM.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuVectorSeperatorM.Shortcut")));
			this.mnuVectorSeperatorM.ShowShortcut = ((bool)(resources.GetObject("mnuVectorSeperatorM.ShowShortcut")));
			this.mnuVectorSeperatorM.Text = resources.GetString("mnuVectorSeperatorM.Text");
			this.mnuVectorSeperatorM.Visible = ((bool)(resources.GetObject("mnuVectorSeperatorM.Visible")));
			// 
			// mnuChildVectorDeleteSelected
			// 
			this.mnuChildVectorDeleteSelected.Enabled = ((bool)(resources.GetObject("mnuChildVectorDeleteSelected.Enabled")));
			this.mnuChildVectorDeleteSelected.Index = 2;
			this.mnuChildVectorDeleteSelected.OwnerDraw = true;
			this.mnuChildVectorDeleteSelected.RadioCheck = true;
			this.mnuChildVectorDeleteSelected.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildVectorDeleteSelected.Shortcut")));
			this.mnuChildVectorDeleteSelected.ShowShortcut = ((bool)(resources.GetObject("mnuChildVectorDeleteSelected.ShowShortcut")));
			this.mnuChildVectorDeleteSelected.Text = resources.GetString("mnuChildVectorDeleteSelected.Text");
			this.mnuChildVectorDeleteSelected.Visible = ((bool)(resources.GetObject("mnuChildVectorDeleteSelected.Visible")));
			this.mnuChildVectorDeleteSelected.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildVectorDeleteSelected.Click += new System.EventHandler(this.btnDelete_Click);
			this.mnuChildVectorDeleteSelected.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildVectorDeleteLast
			// 
			this.mnuChildVectorDeleteLast.Enabled = ((bool)(resources.GetObject("mnuChildVectorDeleteLast.Enabled")));
			this.mnuChildVectorDeleteLast.Index = 3;
			this.mnuChildVectorDeleteLast.OwnerDraw = true;
			this.mnuChildVectorDeleteLast.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildVectorDeleteLast.Shortcut")));
			this.mnuChildVectorDeleteLast.ShowShortcut = ((bool)(resources.GetObject("mnuChildVectorDeleteLast.ShowShortcut")));
			this.mnuChildVectorDeleteLast.Text = resources.GetString("mnuChildVectorDeleteLast.Text");
			this.mnuChildVectorDeleteLast.Visible = ((bool)(resources.GetObject("mnuChildVectorDeleteLast.Visible")));
			this.mnuChildVectorDeleteLast.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildVectorDeleteLast.Click += new System.EventHandler(this.btnDeleteLast_Click);
			this.mnuChildVectorDeleteLast.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildVectorDeleteAll
			// 
			this.mnuChildVectorDeleteAll.Enabled = ((bool)(resources.GetObject("mnuChildVectorDeleteAll.Enabled")));
			this.mnuChildVectorDeleteAll.Index = 4;
			this.mnuChildVectorDeleteAll.OwnerDraw = true;
			this.mnuChildVectorDeleteAll.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildVectorDeleteAll.Shortcut")));
			this.mnuChildVectorDeleteAll.ShowShortcut = ((bool)(resources.GetObject("mnuChildVectorDeleteAll.ShowShortcut")));
			this.mnuChildVectorDeleteAll.Text = resources.GetString("mnuChildVectorDeleteAll.Text");
			this.mnuChildVectorDeleteAll.Visible = ((bool)(resources.GetObject("mnuChildVectorDeleteAll.Visible")));
			this.mnuChildVectorDeleteAll.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildVectorDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
			this.mnuChildVectorDeleteAll.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuVectorSeperator
			// 
			this.mnuVectorSeperator.Enabled = ((bool)(resources.GetObject("mnuVectorSeperator.Enabled")));
			this.mnuVectorSeperator.Index = 5;
			this.mnuVectorSeperator.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuVectorSeperator.Shortcut")));
			this.mnuVectorSeperator.ShowShortcut = ((bool)(resources.GetObject("mnuVectorSeperator.ShowShortcut")));
			this.mnuVectorSeperator.Text = resources.GetString("mnuVectorSeperator.Text");
			this.mnuVectorSeperator.Visible = ((bool)(resources.GetObject("mnuVectorSeperator.Visible")));
			// 
			// mnuReset
			// 
			this.mnuReset.Checked = true;
			this.mnuReset.Enabled = ((bool)(resources.GetObject("mnuReset.Enabled")));
			this.mnuReset.Index = 6;
			this.mnuReset.RadioCheck = true;
			this.mnuReset.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuReset.Shortcut")));
			this.mnuReset.ShowShortcut = ((bool)(resources.GetObject("mnuReset.ShowShortcut")));
			this.mnuReset.Text = resources.GetString("mnuReset.Text");
			this.mnuReset.Visible = ((bool)(resources.GetObject("mnuReset.Visible")));
			this.mnuReset.Click += new System.EventHandler(this.btnResetToNormal_Click);
			// 
			// mnuMakeCenter
			// 
			this.mnuMakeCenter.Enabled = ((bool)(resources.GetObject("mnuMakeCenter.Enabled")));
			this.mnuMakeCenter.Index = 7;
			this.mnuMakeCenter.RadioCheck = true;
			this.mnuMakeCenter.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuMakeCenter.Shortcut")));
			this.mnuMakeCenter.ShowShortcut = ((bool)(resources.GetObject("mnuMakeCenter.ShowShortcut")));
			this.mnuMakeCenter.Text = resources.GetString("mnuMakeCenter.Text");
			this.mnuMakeCenter.Visible = ((bool)(resources.GetObject("mnuMakeCenter.Visible")));
			this.mnuMakeCenter.Click += new System.EventHandler(this.btnCenter_Click);
			// 
			// mnuAdd
			// 
			this.mnuAdd.Enabled = ((bool)(resources.GetObject("mnuAdd.Enabled")));
			this.mnuAdd.Index = 8;
			this.mnuAdd.RadioCheck = true;
			this.mnuAdd.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuAdd.Shortcut")));
			this.mnuAdd.ShowShortcut = ((bool)(resources.GetObject("mnuAdd.ShowShortcut")));
			this.mnuAdd.Text = resources.GetString("mnuAdd.Text");
			this.mnuAdd.Visible = ((bool)(resources.GetObject("mnuAdd.Visible")));
			this.mnuAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// mnuChildSeperator1
			// 
			this.mnuChildSeperator1.Enabled = ((bool)(resources.GetObject("mnuChildSeperator1.Enabled")));
			this.mnuChildSeperator1.Index = 9;
			this.mnuChildSeperator1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildSeperator1.Shortcut")));
			this.mnuChildSeperator1.ShowShortcut = ((bool)(resources.GetObject("mnuChildSeperator1.ShowShortcut")));
			this.mnuChildSeperator1.Text = resources.GetString("mnuChildSeperator1.Text");
			this.mnuChildSeperator1.Visible = ((bool)(resources.GetObject("mnuChildSeperator1.Visible")));
			// 
			// mnuChildVectorProperties
			// 
			this.mnuChildVectorProperties.Enabled = ((bool)(resources.GetObject("mnuChildVectorProperties.Enabled")));
			this.mnuChildVectorProperties.Index = 10;
			this.mnuChildVectorProperties.OwnerDraw = true;
			this.mnuChildVectorProperties.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildVectorProperties.Shortcut")));
			this.mnuChildVectorProperties.ShowShortcut = ((bool)(resources.GetObject("mnuChildVectorProperties.ShowShortcut")));
			this.mnuChildVectorProperties.Text = resources.GetString("mnuChildVectorProperties.Text");
			this.mnuChildVectorProperties.Visible = ((bool)(resources.GetObject("mnuChildVectorProperties.Visible")));
			this.mnuChildVectorProperties.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildVectorProperties.Click += new System.EventHandler(this.btnProperties_Click);
			this.mnuChildVectorProperties.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildTools
			// 
			this.mnuChildTools.Enabled = ((bool)(resources.GetObject("mnuChildTools.Enabled")));
			this.mnuChildTools.Index = 2;
			this.mnuChildTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.mnuChildToolsZoom,
																						  this.mnuChildToolsCalculator,
																						  this.mnuChildToolsS1});
			this.mnuChildTools.MergeOrder = 1;
			this.mnuChildTools.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.mnuChildTools.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildTools.Shortcut")));
			this.mnuChildTools.ShowShortcut = ((bool)(resources.GetObject("mnuChildTools.ShowShortcut")));
			this.mnuChildTools.Text = resources.GetString("mnuChildTools.Text");
			this.mnuChildTools.Visible = ((bool)(resources.GetObject("mnuChildTools.Visible")));
			this.mnuChildTools.Popup += new System.EventHandler(this.mnuChildTools_Popup);
			// 
			// mnuChildToolsZoom
			// 
			this.mnuChildToolsZoom.Enabled = ((bool)(resources.GetObject("mnuChildToolsZoom.Enabled")));
			this.mnuChildToolsZoom.Index = 0;
			this.mnuChildToolsZoom.OwnerDraw = true;
			this.mnuChildToolsZoom.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildToolsZoom.Shortcut")));
			this.mnuChildToolsZoom.ShowShortcut = ((bool)(resources.GetObject("mnuChildToolsZoom.ShowShortcut")));
			this.mnuChildToolsZoom.Text = resources.GetString("mnuChildToolsZoom.Text");
			this.mnuChildToolsZoom.Visible = ((bool)(resources.GetObject("mnuChildToolsZoom.Visible")));
			this.mnuChildToolsZoom.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildToolsZoom.Click += new System.EventHandler(this.mnuChildToolsZoom_Click);
			this.mnuChildToolsZoom.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildToolsCalculator
			// 
			this.mnuChildToolsCalculator.Enabled = ((bool)(resources.GetObject("mnuChildToolsCalculator.Enabled")));
			this.mnuChildToolsCalculator.Index = 1;
			this.mnuChildToolsCalculator.OwnerDraw = true;
			this.mnuChildToolsCalculator.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildToolsCalculator.Shortcut")));
			this.mnuChildToolsCalculator.ShowShortcut = ((bool)(resources.GetObject("mnuChildToolsCalculator.ShowShortcut")));
			this.mnuChildToolsCalculator.Text = resources.GetString("mnuChildToolsCalculator.Text");
			this.mnuChildToolsCalculator.Visible = ((bool)(resources.GetObject("mnuChildToolsCalculator.Visible")));
			this.mnuChildToolsCalculator.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuChildToolsCalculator.Click += new System.EventHandler(this.btnCalculator_Click);
			this.mnuChildToolsCalculator.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// mnuChildToolsS1
			// 
			this.mnuChildToolsS1.Enabled = ((bool)(resources.GetObject("mnuChildToolsS1.Enabled")));
			this.mnuChildToolsS1.Index = 2;
			this.mnuChildToolsS1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuChildToolsS1.Shortcut")));
			this.mnuChildToolsS1.ShowShortcut = ((bool)(resources.GetObject("mnuChildToolsS1.ShowShortcut")));
			this.mnuChildToolsS1.Text = resources.GetString("mnuChildToolsS1.Text");
			this.mnuChildToolsS1.Visible = ((bool)(resources.GetObject("mnuChildToolsS1.Visible")));
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = ((bool)(resources.GetObject("menuItem1.Enabled")));
			this.menuItem1.Index = -1;
			this.menuItem1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem1.Shortcut")));
			this.menuItem1.ShowShortcut = ((bool)(resources.GetObject("menuItem1.ShowShortcut")));
			this.menuItem1.Text = resources.GetString("menuItem1.Text");
			this.menuItem1.Visible = ((bool)(resources.GetObject("menuItem1.Visible")));
			// 
			// ctxMainContextMenu
			// 
			this.ctxMainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							   this.CtxMenu10,
																							   this.CtxMenu20,
																							   this.CtxMenu30,
																							   this.CtxMenu40,
																							   this.CtxMenu50,
																							   this.CtxMenu60,
																							   this.CtxMenu70,
																							   this.CtxMenu80,
																							   this.CtxMenu90,
																							   this.CtxMenuS1,
																							   this.CtxMenu100,
																							   this.CtxMenuS2,
																							   this.CtxMenu110,
																							   this.CtxMenu120,
																							   this.CtxMenu130,
																							   this.CtxMenu140,
																							   this.CtxMenu150,
																							   this.CtxMenu160,
																							   this.CtxMenu170,
																							   this.CtxMenu180,
																							   this.CtxMenu190,
																							   this.CtxMenu200,
																							   this.CtxMenuS3,
																							   this.CtxMenuCustom});
			this.ctxMainContextMenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ctxMainContextMenu.RightToLeft")));
			// 
			// CtxMenu10
			// 
			this.CtxMenu10.Enabled = ((bool)(resources.GetObject("CtxMenu10.Enabled")));
			this.CtxMenu10.Index = 0;
			this.CtxMenu10.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu10.Shortcut")));
			this.CtxMenu10.ShowShortcut = ((bool)(resources.GetObject("CtxMenu10.ShowShortcut")));
			this.CtxMenu10.Text = resources.GetString("CtxMenu10.Text");
			this.CtxMenu10.Visible = ((bool)(resources.GetObject("CtxMenu10.Visible")));
			this.CtxMenu10.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu20
			// 
			this.CtxMenu20.Enabled = ((bool)(resources.GetObject("CtxMenu20.Enabled")));
			this.CtxMenu20.Index = 1;
			this.CtxMenu20.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu20.Shortcut")));
			this.CtxMenu20.ShowShortcut = ((bool)(resources.GetObject("CtxMenu20.ShowShortcut")));
			this.CtxMenu20.Text = resources.GetString("CtxMenu20.Text");
			this.CtxMenu20.Visible = ((bool)(resources.GetObject("CtxMenu20.Visible")));
			this.CtxMenu20.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu30
			// 
			this.CtxMenu30.Enabled = ((bool)(resources.GetObject("CtxMenu30.Enabled")));
			this.CtxMenu30.Index = 2;
			this.CtxMenu30.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu30.Shortcut")));
			this.CtxMenu30.ShowShortcut = ((bool)(resources.GetObject("CtxMenu30.ShowShortcut")));
			this.CtxMenu30.Text = resources.GetString("CtxMenu30.Text");
			this.CtxMenu30.Visible = ((bool)(resources.GetObject("CtxMenu30.Visible")));
			this.CtxMenu30.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu40
			// 
			this.CtxMenu40.Enabled = ((bool)(resources.GetObject("CtxMenu40.Enabled")));
			this.CtxMenu40.Index = 3;
			this.CtxMenu40.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu40.Shortcut")));
			this.CtxMenu40.ShowShortcut = ((bool)(resources.GetObject("CtxMenu40.ShowShortcut")));
			this.CtxMenu40.Text = resources.GetString("CtxMenu40.Text");
			this.CtxMenu40.Visible = ((bool)(resources.GetObject("CtxMenu40.Visible")));
			this.CtxMenu40.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu50
			// 
			this.CtxMenu50.Enabled = ((bool)(resources.GetObject("CtxMenu50.Enabled")));
			this.CtxMenu50.Index = 4;
			this.CtxMenu50.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu50.Shortcut")));
			this.CtxMenu50.ShowShortcut = ((bool)(resources.GetObject("CtxMenu50.ShowShortcut")));
			this.CtxMenu50.Text = resources.GetString("CtxMenu50.Text");
			this.CtxMenu50.Visible = ((bool)(resources.GetObject("CtxMenu50.Visible")));
			this.CtxMenu50.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu60
			// 
			this.CtxMenu60.Enabled = ((bool)(resources.GetObject("CtxMenu60.Enabled")));
			this.CtxMenu60.Index = 5;
			this.CtxMenu60.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu60.Shortcut")));
			this.CtxMenu60.ShowShortcut = ((bool)(resources.GetObject("CtxMenu60.ShowShortcut")));
			this.CtxMenu60.Text = resources.GetString("CtxMenu60.Text");
			this.CtxMenu60.Visible = ((bool)(resources.GetObject("CtxMenu60.Visible")));
			this.CtxMenu60.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu70
			// 
			this.CtxMenu70.Enabled = ((bool)(resources.GetObject("CtxMenu70.Enabled")));
			this.CtxMenu70.Index = 6;
			this.CtxMenu70.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu70.Shortcut")));
			this.CtxMenu70.ShowShortcut = ((bool)(resources.GetObject("CtxMenu70.ShowShortcut")));
			this.CtxMenu70.Text = resources.GetString("CtxMenu70.Text");
			this.CtxMenu70.Visible = ((bool)(resources.GetObject("CtxMenu70.Visible")));
			this.CtxMenu70.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu80
			// 
			this.CtxMenu80.Enabled = ((bool)(resources.GetObject("CtxMenu80.Enabled")));
			this.CtxMenu80.Index = 7;
			this.CtxMenu80.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu80.Shortcut")));
			this.CtxMenu80.ShowShortcut = ((bool)(resources.GetObject("CtxMenu80.ShowShortcut")));
			this.CtxMenu80.Text = resources.GetString("CtxMenu80.Text");
			this.CtxMenu80.Visible = ((bool)(resources.GetObject("CtxMenu80.Visible")));
			this.CtxMenu80.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu90
			// 
			this.CtxMenu90.Enabled = ((bool)(resources.GetObject("CtxMenu90.Enabled")));
			this.CtxMenu90.Index = 8;
			this.CtxMenu90.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu90.Shortcut")));
			this.CtxMenu90.ShowShortcut = ((bool)(resources.GetObject("CtxMenu90.ShowShortcut")));
			this.CtxMenu90.Text = resources.GetString("CtxMenu90.Text");
			this.CtxMenu90.Visible = ((bool)(resources.GetObject("CtxMenu90.Visible")));
			this.CtxMenu90.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenuS1
			// 
			this.CtxMenuS1.Enabled = ((bool)(resources.GetObject("CtxMenuS1.Enabled")));
			this.CtxMenuS1.Index = 9;
			this.CtxMenuS1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenuS1.Shortcut")));
			this.CtxMenuS1.ShowShortcut = ((bool)(resources.GetObject("CtxMenuS1.ShowShortcut")));
			this.CtxMenuS1.Text = resources.GetString("CtxMenuS1.Text");
			this.CtxMenuS1.Visible = ((bool)(resources.GetObject("CtxMenuS1.Visible")));
			// 
			// CtxMenu100
			// 
			this.CtxMenu100.Enabled = ((bool)(resources.GetObject("CtxMenu100.Enabled")));
			this.CtxMenu100.Index = 10;
			this.CtxMenu100.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu100.Shortcut")));
			this.CtxMenu100.ShowShortcut = ((bool)(resources.GetObject("CtxMenu100.ShowShortcut")));
			this.CtxMenu100.Text = resources.GetString("CtxMenu100.Text");
			this.CtxMenu100.Visible = ((bool)(resources.GetObject("CtxMenu100.Visible")));
			this.CtxMenu100.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenuS2
			// 
			this.CtxMenuS2.Enabled = ((bool)(resources.GetObject("CtxMenuS2.Enabled")));
			this.CtxMenuS2.Index = 11;
			this.CtxMenuS2.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenuS2.Shortcut")));
			this.CtxMenuS2.ShowShortcut = ((bool)(resources.GetObject("CtxMenuS2.ShowShortcut")));
			this.CtxMenuS2.Text = resources.GetString("CtxMenuS2.Text");
			this.CtxMenuS2.Visible = ((bool)(resources.GetObject("CtxMenuS2.Visible")));
			// 
			// CtxMenu110
			// 
			this.CtxMenu110.Enabled = ((bool)(resources.GetObject("CtxMenu110.Enabled")));
			this.CtxMenu110.Index = 12;
			this.CtxMenu110.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu110.Shortcut")));
			this.CtxMenu110.ShowShortcut = ((bool)(resources.GetObject("CtxMenu110.ShowShortcut")));
			this.CtxMenu110.Text = resources.GetString("CtxMenu110.Text");
			this.CtxMenu110.Visible = ((bool)(resources.GetObject("CtxMenu110.Visible")));
			this.CtxMenu110.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu120
			// 
			this.CtxMenu120.Enabled = ((bool)(resources.GetObject("CtxMenu120.Enabled")));
			this.CtxMenu120.Index = 13;
			this.CtxMenu120.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu120.Shortcut")));
			this.CtxMenu120.ShowShortcut = ((bool)(resources.GetObject("CtxMenu120.ShowShortcut")));
			this.CtxMenu120.Text = resources.GetString("CtxMenu120.Text");
			this.CtxMenu120.Visible = ((bool)(resources.GetObject("CtxMenu120.Visible")));
			this.CtxMenu120.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu130
			// 
			this.CtxMenu130.Enabled = ((bool)(resources.GetObject("CtxMenu130.Enabled")));
			this.CtxMenu130.Index = 14;
			this.CtxMenu130.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu130.Shortcut")));
			this.CtxMenu130.ShowShortcut = ((bool)(resources.GetObject("CtxMenu130.ShowShortcut")));
			this.CtxMenu130.Text = resources.GetString("CtxMenu130.Text");
			this.CtxMenu130.Visible = ((bool)(resources.GetObject("CtxMenu130.Visible")));
			this.CtxMenu130.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu140
			// 
			this.CtxMenu140.Enabled = ((bool)(resources.GetObject("CtxMenu140.Enabled")));
			this.CtxMenu140.Index = 15;
			this.CtxMenu140.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu140.Shortcut")));
			this.CtxMenu140.ShowShortcut = ((bool)(resources.GetObject("CtxMenu140.ShowShortcut")));
			this.CtxMenu140.Text = resources.GetString("CtxMenu140.Text");
			this.CtxMenu140.Visible = ((bool)(resources.GetObject("CtxMenu140.Visible")));
			this.CtxMenu140.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu150
			// 
			this.CtxMenu150.Enabled = ((bool)(resources.GetObject("CtxMenu150.Enabled")));
			this.CtxMenu150.Index = 16;
			this.CtxMenu150.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu150.Shortcut")));
			this.CtxMenu150.ShowShortcut = ((bool)(resources.GetObject("CtxMenu150.ShowShortcut")));
			this.CtxMenu150.Text = resources.GetString("CtxMenu150.Text");
			this.CtxMenu150.Visible = ((bool)(resources.GetObject("CtxMenu150.Visible")));
			this.CtxMenu150.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu160
			// 
			this.CtxMenu160.Enabled = ((bool)(resources.GetObject("CtxMenu160.Enabled")));
			this.CtxMenu160.Index = 17;
			this.CtxMenu160.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu160.Shortcut")));
			this.CtxMenu160.ShowShortcut = ((bool)(resources.GetObject("CtxMenu160.ShowShortcut")));
			this.CtxMenu160.Text = resources.GetString("CtxMenu160.Text");
			this.CtxMenu160.Visible = ((bool)(resources.GetObject("CtxMenu160.Visible")));
			this.CtxMenu160.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu170
			// 
			this.CtxMenu170.Enabled = ((bool)(resources.GetObject("CtxMenu170.Enabled")));
			this.CtxMenu170.Index = 18;
			this.CtxMenu170.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu170.Shortcut")));
			this.CtxMenu170.ShowShortcut = ((bool)(resources.GetObject("CtxMenu170.ShowShortcut")));
			this.CtxMenu170.Text = resources.GetString("CtxMenu170.Text");
			this.CtxMenu170.Visible = ((bool)(resources.GetObject("CtxMenu170.Visible")));
			this.CtxMenu170.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu180
			// 
			this.CtxMenu180.Enabled = ((bool)(resources.GetObject("CtxMenu180.Enabled")));
			this.CtxMenu180.Index = 19;
			this.CtxMenu180.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu180.Shortcut")));
			this.CtxMenu180.ShowShortcut = ((bool)(resources.GetObject("CtxMenu180.ShowShortcut")));
			this.CtxMenu180.Text = resources.GetString("CtxMenu180.Text");
			this.CtxMenu180.Visible = ((bool)(resources.GetObject("CtxMenu180.Visible")));
			this.CtxMenu180.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu190
			// 
			this.CtxMenu190.Enabled = ((bool)(resources.GetObject("CtxMenu190.Enabled")));
			this.CtxMenu190.Index = 20;
			this.CtxMenu190.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu190.Shortcut")));
			this.CtxMenu190.ShowShortcut = ((bool)(resources.GetObject("CtxMenu190.ShowShortcut")));
			this.CtxMenu190.Text = resources.GetString("CtxMenu190.Text");
			this.CtxMenu190.Visible = ((bool)(resources.GetObject("CtxMenu190.Visible")));
			this.CtxMenu190.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenu200
			// 
			this.CtxMenu200.Enabled = ((bool)(resources.GetObject("CtxMenu200.Enabled")));
			this.CtxMenu200.Index = 21;
			this.CtxMenu200.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenu200.Shortcut")));
			this.CtxMenu200.ShowShortcut = ((bool)(resources.GetObject("CtxMenu200.ShowShortcut")));
			this.CtxMenu200.Text = resources.GetString("CtxMenu200.Text");
			this.CtxMenu200.Visible = ((bool)(resources.GetObject("CtxMenu200.Visible")));
			this.CtxMenu200.Click += new System.EventHandler(this.Ctx_Click);
			// 
			// CtxMenuS3
			// 
			this.CtxMenuS3.Enabled = ((bool)(resources.GetObject("CtxMenuS3.Enabled")));
			this.CtxMenuS3.Index = 22;
			this.CtxMenuS3.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenuS3.Shortcut")));
			this.CtxMenuS3.ShowShortcut = ((bool)(resources.GetObject("CtxMenuS3.ShowShortcut")));
			this.CtxMenuS3.Text = resources.GetString("CtxMenuS3.Text");
			this.CtxMenuS3.Visible = ((bool)(resources.GetObject("CtxMenuS3.Visible")));
			// 
			// CtxMenuCustom
			// 
			this.CtxMenuCustom.Enabled = ((bool)(resources.GetObject("CtxMenuCustom.Enabled")));
			this.CtxMenuCustom.Index = 23;
			this.CtxMenuCustom.OwnerDraw = true;
			this.CtxMenuCustom.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("CtxMenuCustom.Shortcut")));
			this.CtxMenuCustom.ShowShortcut = ((bool)(resources.GetObject("CtxMenuCustom.ShowShortcut")));
			this.CtxMenuCustom.Text = resources.GetString("CtxMenuCustom.Text");
			this.CtxMenuCustom.Visible = ((bool)(resources.GetObject("CtxMenuCustom.Visible")));
			this.CtxMenuCustom.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.CtxMenuCustom.Click += new System.EventHandler(this.Ctx_Click);
			this.CtxMenuCustom.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Measure_Menu_Items);
			// 
			// btnCenter
			// 
			this.btnCenter.AccessibleDescription = ((string)(resources.GetObject("btnCenter.AccessibleDescription")));
			this.btnCenter.AccessibleName = ((string)(resources.GetObject("btnCenter.AccessibleName")));
			this.btnCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCenter.Anchor")));
			this.btnCenter.BackColor = System.Drawing.Color.Transparent;
			this.btnCenter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCenter.BackgroundImage")));
			this.btnCenter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCenter.Dock")));
			this.btnCenter.Enabled = ((bool)(resources.GetObject("btnCenter.Enabled")));
			this.btnCenter.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCenter.FlatStyle")));
			this.btnCenter.Font = ((System.Drawing.Font)(resources.GetObject("btnCenter.Font")));
			this.btnCenter.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnCenter.Image")));
			this.btnCenter.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCenter.ImageAlign")));
			this.btnCenter.ImageIndex = ((int)(resources.GetObject("btnCenter.ImageIndex")));
			this.btnCenter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCenter.ImeMode")));
			this.btnCenter.Location = ((System.Drawing.Point)(resources.GetObject("btnCenter.Location")));
			this.btnCenter.Name = "btnCenter";
			this.btnCenter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCenter.RightToLeft")));
			this.btnCenter.Size = ((System.Drawing.Size)(resources.GetObject("btnCenter.Size")));
			this.btnCenter.TabIndex = ((int)(resources.GetObject("btnCenter.TabIndex")));
			this.btnCenter.Text = resources.GetString("btnCenter.Text");
			this.btnCenter.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCenter.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnCenter, resources.GetString("btnCenter.ToolTip"));
			this.btnCenter.Visible = ((bool)(resources.GetObject("btnCenter.Visible")));
			this.btnCenter.Click += new System.EventHandler(this.btnCenter_Click);
			// 
			// btnResetToNormal
			// 
			this.btnResetToNormal.AccessibleDescription = ((string)(resources.GetObject("btnResetToNormal.AccessibleDescription")));
			this.btnResetToNormal.AccessibleName = ((string)(resources.GetObject("btnResetToNormal.AccessibleName")));
			this.btnResetToNormal.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnResetToNormal.Anchor")));
			this.btnResetToNormal.BackColor = System.Drawing.Color.Transparent;
			this.btnResetToNormal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnResetToNormal.BackgroundImage")));
			this.btnResetToNormal.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnResetToNormal.Dock")));
			this.btnResetToNormal.Enabled = ((bool)(resources.GetObject("btnResetToNormal.Enabled")));
			this.btnResetToNormal.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnResetToNormal.FlatStyle")));
			this.btnResetToNormal.Font = ((System.Drawing.Font)(resources.GetObject("btnResetToNormal.Font")));
			this.btnResetToNormal.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnResetToNormal.Image")));
			this.btnResetToNormal.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnResetToNormal.ImageAlign")));
			this.btnResetToNormal.ImageIndex = ((int)(resources.GetObject("btnResetToNormal.ImageIndex")));
			this.btnResetToNormal.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnResetToNormal.ImeMode")));
			this.btnResetToNormal.Location = ((System.Drawing.Point)(resources.GetObject("btnResetToNormal.Location")));
			this.btnResetToNormal.Name = "btnResetToNormal";
			this.btnResetToNormal.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnResetToNormal.RightToLeft")));
			this.btnResetToNormal.Size = ((System.Drawing.Size)(resources.GetObject("btnResetToNormal.Size")));
			this.btnResetToNormal.TabIndex = ((int)(resources.GetObject("btnResetToNormal.TabIndex")));
			this.btnResetToNormal.Text = resources.GetString("btnResetToNormal.Text");
			this.btnResetToNormal.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnResetToNormal.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnResetToNormal, resources.GetString("btnResetToNormal.ToolTip"));
			this.btnResetToNormal.Visible = ((bool)(resources.GetObject("btnResetToNormal.Visible")));
			this.btnResetToNormal.Click += new System.EventHandler(this.btnResetToNormal_Click);
			// 
			// btnCalculator
			// 
			this.btnCalculator.AccessibleDescription = ((string)(resources.GetObject("btnCalculator.AccessibleDescription")));
			this.btnCalculator.AccessibleName = ((string)(resources.GetObject("btnCalculator.AccessibleName")));
			this.btnCalculator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCalculator.Anchor")));
			this.btnCalculator.BackColor = System.Drawing.Color.Transparent;
			this.btnCalculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCalculator.BackgroundImage")));
			this.btnCalculator.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCalculator.Dock")));
			this.btnCalculator.Enabled = ((bool)(resources.GetObject("btnCalculator.Enabled")));
			this.btnCalculator.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCalculator.FlatStyle")));
			this.btnCalculator.Font = ((System.Drawing.Font)(resources.GetObject("btnCalculator.Font")));
			this.btnCalculator.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnCalculator.Image")));
			this.btnCalculator.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCalculator.ImageAlign")));
			this.btnCalculator.ImageIndex = ((int)(resources.GetObject("btnCalculator.ImageIndex")));
			this.btnCalculator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCalculator.ImeMode")));
			this.btnCalculator.Location = ((System.Drawing.Point)(resources.GetObject("btnCalculator.Location")));
			this.btnCalculator.Name = "btnCalculator";
			this.btnCalculator.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCalculator.RightToLeft")));
			this.btnCalculator.Size = ((System.Drawing.Size)(resources.GetObject("btnCalculator.Size")));
			this.btnCalculator.TabIndex = ((int)(resources.GetObject("btnCalculator.TabIndex")));
			this.btnCalculator.Text = resources.GetString("btnCalculator.Text");
			this.btnCalculator.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCalculator.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnCalculator, resources.GetString("btnCalculator.ToolTip"));
			this.btnCalculator.Visible = ((bool)(resources.GetObject("btnCalculator.Visible")));
			this.btnCalculator.Click += new System.EventHandler(this.btnCalculator_Click);
			// 
			// btnNew
			// 
			this.btnNew.AccessibleDescription = ((string)(resources.GetObject("btnNew.AccessibleDescription")));
			this.btnNew.AccessibleName = ((string)(resources.GetObject("btnNew.AccessibleName")));
			this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnNew.Anchor")));
			this.btnNew.BackColor = System.Drawing.Color.Transparent;
			this.btnNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNew.BackgroundImage")));
			this.btnNew.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnNew.Dock")));
			this.btnNew.Enabled = ((bool)(resources.GetObject("btnNew.Enabled")));
			this.btnNew.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnNew.FlatStyle")));
			this.btnNew.Font = ((System.Drawing.Font)(resources.GetObject("btnNew.Font")));
			this.btnNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnNew.Image")));
			this.btnNew.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnNew.ImageAlign")));
			this.btnNew.ImageIndex = ((int)(resources.GetObject("btnNew.ImageIndex")));
			this.btnNew.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnNew.ImeMode")));
			this.btnNew.Location = ((System.Drawing.Point)(resources.GetObject("btnNew.Location")));
			this.btnNew.Name = "btnNew";
			this.btnNew.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnNew.RightToLeft")));
			this.btnNew.Size = ((System.Drawing.Size)(resources.GetObject("btnNew.Size")));
			this.btnNew.TabIndex = ((int)(resources.GetObject("btnNew.TabIndex")));
			this.btnNew.Text = resources.GetString("btnNew.Text");
			this.btnNew.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnNew.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnNew, resources.GetString("btnNew.ToolTip"));
			this.btnNew.Visible = ((bool)(resources.GetObject("btnNew.Visible")));
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
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
			// btnDeleteLast
			// 
			this.btnDeleteLast.AccessibleDescription = ((string)(resources.GetObject("btnDeleteLast.AccessibleDescription")));
			this.btnDeleteLast.AccessibleName = ((string)(resources.GetObject("btnDeleteLast.AccessibleName")));
			this.btnDeleteLast.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDeleteLast.Anchor")));
			this.btnDeleteLast.BackColor = System.Drawing.Color.Transparent;
			this.btnDeleteLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteLast.BackgroundImage")));
			this.btnDeleteLast.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDeleteLast.Dock")));
			this.btnDeleteLast.Enabled = ((bool)(resources.GetObject("btnDeleteLast.Enabled")));
			this.btnDeleteLast.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDeleteLast.FlatStyle")));
			this.btnDeleteLast.Font = ((System.Drawing.Font)(resources.GetObject("btnDeleteLast.Font")));
			this.btnDeleteLast.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnDeleteLast.Image")));
			this.btnDeleteLast.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteLast.ImageAlign")));
			this.btnDeleteLast.ImageIndex = ((int)(resources.GetObject("btnDeleteLast.ImageIndex")));
			this.btnDeleteLast.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDeleteLast.ImeMode")));
			this.btnDeleteLast.Location = ((System.Drawing.Point)(resources.GetObject("btnDeleteLast.Location")));
			this.btnDeleteLast.Name = "btnDeleteLast";
			this.btnDeleteLast.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDeleteLast.RightToLeft")));
			this.btnDeleteLast.Size = ((System.Drawing.Size)(resources.GetObject("btnDeleteLast.Size")));
			this.btnDeleteLast.TabIndex = ((int)(resources.GetObject("btnDeleteLast.TabIndex")));
			this.btnDeleteLast.Text = resources.GetString("btnDeleteLast.Text");
			this.btnDeleteLast.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteLast.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnDeleteLast, resources.GetString("btnDeleteLast.ToolTip"));
			this.btnDeleteLast.Visible = ((bool)(resources.GetObject("btnDeleteLast.Visible")));
			this.btnDeleteLast.Click += new System.EventHandler(this.btnDeleteLast_Click);
			// 
			// btnDeleteAll
			// 
			this.btnDeleteAll.AccessibleDescription = ((string)(resources.GetObject("btnDeleteAll.AccessibleDescription")));
			this.btnDeleteAll.AccessibleName = ((string)(resources.GetObject("btnDeleteAll.AccessibleName")));
			this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDeleteAll.Anchor")));
			this.btnDeleteAll.BackColor = System.Drawing.Color.Transparent;
			this.btnDeleteAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteAll.BackgroundImage")));
			this.btnDeleteAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDeleteAll.Dock")));
			this.btnDeleteAll.Enabled = ((bool)(resources.GetObject("btnDeleteAll.Enabled")));
			this.btnDeleteAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDeleteAll.FlatStyle")));
			this.btnDeleteAll.Font = ((System.Drawing.Font)(resources.GetObject("btnDeleteAll.Font")));
			this.btnDeleteAll.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnDeleteAll.Image")));
			this.btnDeleteAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteAll.ImageAlign")));
			this.btnDeleteAll.ImageIndex = ((int)(resources.GetObject("btnDeleteAll.ImageIndex")));
			this.btnDeleteAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDeleteAll.ImeMode")));
			this.btnDeleteAll.Location = ((System.Drawing.Point)(resources.GetObject("btnDeleteAll.Location")));
			this.btnDeleteAll.Name = "btnDeleteAll";
			this.btnDeleteAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDeleteAll.RightToLeft")));
			this.btnDeleteAll.Size = ((System.Drawing.Size)(resources.GetObject("btnDeleteAll.Size")));
			this.btnDeleteAll.TabIndex = ((int)(resources.GetObject("btnDeleteAll.TabIndex")));
			this.btnDeleteAll.Text = resources.GetString("btnDeleteAll.Text");
			this.btnDeleteAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteAll.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnDeleteAll, resources.GetString("btnDeleteAll.ToolTip"));
			this.btnDeleteAll.Visible = ((bool)(resources.GetObject("btnDeleteAll.Visible")));
			this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
			// 
			// grpVectorChooser
			// 
			this.grpVectorChooser.AccessibleDescription = ((string)(resources.GetObject("grpVectorChooser.AccessibleDescription")));
			this.grpVectorChooser.AccessibleName = ((string)(resources.GetObject("grpVectorChooser.AccessibleName")));
			this.grpVectorChooser.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpVectorChooser.Anchor")));
			this.grpVectorChooser.BackColor = System.Drawing.Color.Transparent;
			this.grpVectorChooser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpVectorChooser.BackgroundImage")));
			this.grpVectorChooser.Controls.AddRange(new System.Windows.Forms.Control[] {
																						   this.cmbVectorSelector,
																						   this.lblCommentForComboBox,
																						   this.btnProperties,
																						   this.btnDelete});
			this.grpVectorChooser.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpVectorChooser.Dock")));
			this.grpVectorChooser.Enabled = ((bool)(resources.GetObject("grpVectorChooser.Enabled")));
			this.grpVectorChooser.Font = ((System.Drawing.Font)(resources.GetObject("grpVectorChooser.Font")));
			this.grpVectorChooser.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpVectorChooser.ImeMode")));
			this.grpVectorChooser.Location = ((System.Drawing.Point)(resources.GetObject("grpVectorChooser.Location")));
			this.grpVectorChooser.Name = "grpVectorChooser";
			this.grpVectorChooser.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpVectorChooser.RightToLeft")));
			this.grpVectorChooser.Size = ((System.Drawing.Size)(resources.GetObject("grpVectorChooser.Size")));
			this.grpVectorChooser.TabIndex = ((int)(resources.GetObject("grpVectorChooser.TabIndex")));
			this.grpVectorChooser.TabStop = false;
			this.grpVectorChooser.Text = resources.GetString("grpVectorChooser.Text");
			this.tltToolTip.SetToolTip(this.grpVectorChooser, resources.GetString("grpVectorChooser.ToolTip"));
			this.grpVectorChooser.Visible = ((bool)(resources.GetObject("grpVectorChooser.Visible")));
			// 
			// cmbVectorSelector
			// 
			this.cmbVectorSelector.AccessibleDescription = ((string)(resources.GetObject("cmbVectorSelector.AccessibleDescription")));
			this.cmbVectorSelector.AccessibleName = ((string)(resources.GetObject("cmbVectorSelector.AccessibleName")));
			this.cmbVectorSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmbVectorSelector.Anchor")));
			this.cmbVectorSelector.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbVectorSelector.BackgroundImage")));
			this.cmbVectorSelector.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmbVectorSelector.Dock")));
			this.cmbVectorSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbVectorSelector.Enabled = ((bool)(resources.GetObject("cmbVectorSelector.Enabled")));
			this.cmbVectorSelector.Font = ((System.Drawing.Font)(resources.GetObject("cmbVectorSelector.Font")));
			this.cmbVectorSelector.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmbVectorSelector.ImeMode")));
			this.cmbVectorSelector.IntegralHeight = ((bool)(resources.GetObject("cmbVectorSelector.IntegralHeight")));
			this.cmbVectorSelector.ItemHeight = ((int)(resources.GetObject("cmbVectorSelector.ItemHeight")));
			this.cmbVectorSelector.Location = ((System.Drawing.Point)(resources.GetObject("cmbVectorSelector.Location")));
			this.cmbVectorSelector.MaxDropDownItems = ((int)(resources.GetObject("cmbVectorSelector.MaxDropDownItems")));
			this.cmbVectorSelector.MaxLength = ((int)(resources.GetObject("cmbVectorSelector.MaxLength")));
			this.cmbVectorSelector.Name = "cmbVectorSelector";
			this.cmbVectorSelector.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmbVectorSelector.RightToLeft")));
			this.cmbVectorSelector.Size = ((System.Drawing.Size)(resources.GetObject("cmbVectorSelector.Size")));
			this.cmbVectorSelector.TabIndex = ((int)(resources.GetObject("cmbVectorSelector.TabIndex")));
			this.cmbVectorSelector.Text = resources.GetString("cmbVectorSelector.Text");
			this.tltToolTip.SetToolTip(this.cmbVectorSelector, resources.GetString("cmbVectorSelector.ToolTip"));
			this.cmbVectorSelector.Visible = ((bool)(resources.GetObject("cmbVectorSelector.Visible")));
			this.cmbVectorSelector.SelectedIndexChanged += new System.EventHandler(this.cmbVectorSelecto_SelectedIndexChanged);
			this.cmbVectorSelector.Enter += new System.EventHandler(this.cmbVectorSelector_Enter);
			// 
			// lblCommentForComboBox
			// 
			this.lblCommentForComboBox.AccessibleDescription = ((string)(resources.GetObject("lblCommentForComboBox.AccessibleDescription")));
			this.lblCommentForComboBox.AccessibleName = ((string)(resources.GetObject("lblCommentForComboBox.AccessibleName")));
			this.lblCommentForComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCommentForComboBox.Anchor")));
			this.lblCommentForComboBox.AutoSize = ((bool)(resources.GetObject("lblCommentForComboBox.AutoSize")));
			this.lblCommentForComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
			this.lblCommentForComboBox.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCommentForComboBox.Dock")));
			this.lblCommentForComboBox.Enabled = ((bool)(resources.GetObject("lblCommentForComboBox.Enabled")));
			this.lblCommentForComboBox.Font = ((System.Drawing.Font)(resources.GetObject("lblCommentForComboBox.Font")));
			this.lblCommentForComboBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCommentForComboBox.Image = ((System.Drawing.Image)(resources.GetObject("lblCommentForComboBox.Image")));
			this.lblCommentForComboBox.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCommentForComboBox.ImageAlign")));
			this.lblCommentForComboBox.ImageIndex = ((int)(resources.GetObject("lblCommentForComboBox.ImageIndex")));
			this.lblCommentForComboBox.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCommentForComboBox.ImeMode")));
			this.lblCommentForComboBox.Location = ((System.Drawing.Point)(resources.GetObject("lblCommentForComboBox.Location")));
			this.lblCommentForComboBox.Name = "lblCommentForComboBox";
			this.lblCommentForComboBox.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCommentForComboBox.RightToLeft")));
			this.lblCommentForComboBox.Size = ((System.Drawing.Size)(resources.GetObject("lblCommentForComboBox.Size")));
			this.lblCommentForComboBox.TabIndex = ((int)(resources.GetObject("lblCommentForComboBox.TabIndex")));
			this.lblCommentForComboBox.Text = resources.GetString("lblCommentForComboBox.Text");
			this.lblCommentForComboBox.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCommentForComboBox.TextAlign")));
			this.tltToolTip.SetToolTip(this.lblCommentForComboBox, resources.GetString("lblCommentForComboBox.ToolTip"));
			this.lblCommentForComboBox.Visible = ((bool)(resources.GetObject("lblCommentForComboBox.Visible")));
			// 
			// btnProperties
			// 
			this.btnProperties.AccessibleDescription = ((string)(resources.GetObject("btnProperties.AccessibleDescription")));
			this.btnProperties.AccessibleName = ((string)(resources.GetObject("btnProperties.AccessibleName")));
			this.btnProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnProperties.Anchor")));
			this.btnProperties.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnProperties.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProperties.BackgroundImage")));
			this.btnProperties.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnProperties.Dock")));
			this.btnProperties.Enabled = ((bool)(resources.GetObject("btnProperties.Enabled")));
			this.btnProperties.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnProperties.FlatStyle")));
			this.btnProperties.Font = ((System.Drawing.Font)(resources.GetObject("btnProperties.Font")));
			this.btnProperties.ForeColor = System.Drawing.Color.Black;
			this.btnProperties.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnProperties.Image")));
			this.btnProperties.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProperties.ImageAlign")));
			this.btnProperties.ImageIndex = ((int)(resources.GetObject("btnProperties.ImageIndex")));
			this.btnProperties.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnProperties.ImeMode")));
			this.btnProperties.Location = ((System.Drawing.Point)(resources.GetObject("btnProperties.Location")));
			this.btnProperties.Name = "btnProperties";
			this.btnProperties.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnProperties.RightToLeft")));
			this.btnProperties.Size = ((System.Drawing.Size)(resources.GetObject("btnProperties.Size")));
			this.btnProperties.TabIndex = ((int)(resources.GetObject("btnProperties.TabIndex")));
			this.btnProperties.Text = resources.GetString("btnProperties.Text");
			this.btnProperties.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProperties.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnProperties, resources.GetString("btnProperties.ToolTip"));
			this.btnProperties.Visible = ((bool)(resources.GetObject("btnProperties.Visible")));
			this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = ((string)(resources.GetObject("btnDelete.AccessibleDescription")));
			this.btnDelete.AccessibleName = ((string)(resources.GetObject("btnDelete.AccessibleName")));
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelete.Anchor")));
			this.btnDelete.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
			this.btnDelete.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelete.Dock")));
			this.btnDelete.Enabled = ((bool)(resources.GetObject("btnDelete.Enabled")));
			this.btnDelete.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelete.FlatStyle")));
			this.btnDelete.Font = ((System.Drawing.Font)(resources.GetObject("btnDelete.Font")));
			this.btnDelete.ForeColor = System.Drawing.Color.Black;
			this.btnDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.ImageAlign")));
			this.btnDelete.ImageIndex = ((int)(resources.GetObject("btnDelete.ImageIndex")));
			this.btnDelete.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelete.ImeMode")));
			this.btnDelete.Location = ((System.Drawing.Point)(resources.GetObject("btnDelete.Location")));
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelete.RightToLeft")));
			this.btnDelete.Size = ((System.Drawing.Size)(resources.GetObject("btnDelete.Size")));
			this.btnDelete.TabIndex = ((int)(resources.GetObject("btnDelete.TabIndex")));
			this.btnDelete.Text = resources.GetString("btnDelete.Text");
			this.btnDelete.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.TextAlign")));
			this.tltToolTip.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
			this.btnDelete.Visible = ((bool)(resources.GetObject("btnDelete.Visible")));
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// m_pdcPrintDocument
			// 
			this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.OnPrintPage);
			// 
			// menuItem26
			// 
			this.menuItem26.Enabled = ((bool)(resources.GetObject("menuItem26.Enabled")));
			this.menuItem26.Index = -1;
			this.menuItem26.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem26.Shortcut")));
			this.menuItem26.ShowShortcut = ((bool)(resources.GetObject("menuItem26.ShowShortcut")));
			this.menuItem26.Text = resources.GetString("menuItem26.Text");
			this.menuItem26.Visible = ((bool)(resources.GetObject("menuItem26.Visible")));
			// 
			// frmChild
			// 
			this.AccessibleDescription = ((string)(resources.GetObject("$this.AccessibleDescription")));
			this.AccessibleName = ((string)(resources.GetObject("$this.AccessibleName")));
			this.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("$this.Anchor")));
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnCenter,
																		  this.btnResetToNormal,
																		  this.btnCalculator,
																		  this.btnNew,
																		  this.btnAdd,
																		  this.btnDeleteLast,
																		  this.btnDeleteAll,
																		  this.grpVectorChooser,
																		  this.stbStatusBar});
			this.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("$this.Dock")));
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.Menu = this.mnuMainMenu;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "frmChild";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.ShowInTaskbar = false;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Tag = "";
			this.Text = resources.GetString("$this.Text");
			this.tltToolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
			this.Visible = ((bool)(resources.GetObject("$this.Visible")));
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmChild_Closing);
			this.Load += new System.EventHandler(this.frmChild_Load);
			this.Closed += new System.EventHandler(this.frmChild_Closed);
			((System.ComponentModel.ISupportInitialize)(this.pnlMousePosition)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlLenghtOfCurrentVector)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlResult)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlTotalVectors)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlZoom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlArrangeModeNormal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlArrangeModeCenter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlArrangeModeAdd)).EndInit();
			this.grpVectorChooser.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	}
}


