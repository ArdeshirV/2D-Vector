#region Header
// https://github.com/ArdeshirV/2D-Vector
// Copyright 2002-2004 ArdeshirV, Licensed Under MIT


using System;
//using ArdeshirV.Utility;
using System.Drawing;
using System.Resources;
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
	/// Main MDI Form for Vector Project.
	/// </summary>
	public class frmMainVectorForm : System.Windows.Forms.Form
	{
		#region Variables

		private string[] m_strStatusBarToolTips = new string[2];
		private System.Windows.Forms.MainMenu mnuMainMenu;
		private System.Windows.Forms.MenuItem mnuFileMenu;
		private System.Windows.Forms.MenuItem mnuFileNewMenu;
		private System.Windows.Forms.MenuItem mnuFileOpenMenu;
		private System.Windows.Forms.MenuItem mnuFileExitMenu;
		private System.Windows.Forms.MenuItem mnuViewMenu;
		private System.Windows.Forms.MenuItem mnuViewStatusBarMenu;
		private System.Windows.Forms.MenuItem mnuViewToolBarMenu;
		private System.Windows.Forms.MenuItem mnuToolsMenu;
		private System.Windows.Forms.MenuItem mnuWindowMenu;
		private System.Windows.Forms.MenuItem mnuHelpMenu;
		private System.Windows.Forms.MenuItem mnuHelpAboutMenu;
		private System.Windows.Forms.MenuItem mnuToolsOptionMenu;
		private System.Windows.Forms.MenuItem mnuWindowCascadeMenu;
		private System.Windows.Forms.MenuItem mnuWindowTileHorizontallyMenu;
		private System.Windows.Forms.MenuItem mnuWindowTileVerticallyMenu;
		private System.Windows.Forms.MenuItem mnuWindowCloseMenu;
		private System.Windows.Forms.MenuItem mnuWindowCloseAllMenu;
		private System.Windows.Forms.MenuItem mnuWindowSeperator1Menu;
		private System.Windows.Forms.NotifyIcon ntiNotifyIcon;
		private System.Windows.Forms.ContextMenu mnuMainForNotifycationIcon;
		private System.Windows.Forms.MenuItem ctxmnuAbout;
		private System.Windows.Forms.MenuItem ctxmnuSeperator;
		private System.Windows.Forms.MenuItem ctxmnuExit;
		private System.Windows.Forms.ToolBar tlbMainToolBar;
		private System.Windows.Forms.ToolBarButton btnNew;
		private System.Windows.Forms.ToolBarButton btnOpen;
		private System.Windows.Forms.ToolBarButton btnSave;
		private System.Windows.Forms.ToolBarButton btnPrint;
		private System.Windows.Forms.ToolBarButton btnPrintSetup;
		private System.Windows.Forms.ToolBarButton btnAbout;
		private System.Windows.Forms.ToolBarButton btnOptions;
		private System.Windows.Forms.ToolBarButton btnS1;
		private System.Windows.Forms.ToolBarButton btnS2;
		private System.Windows.Forms.ToolBarButton btnSaveAsPicture;
		private System.Windows.Forms.StatusBar stbMainStatusBar;
		private System.Windows.Forms.ImageList imgImageListForToolbar;
		private System.Windows.Forms.Timer m_timTimer;
		private System.Windows.Forms.ToolTip tltMainTooltip;
		private System.Windows.Forms.StatusBarPanel pnlCountOfWindow;
		private System.Windows.Forms.ToolBarButton btnPrintPreview;
		private System.Windows.Forms.MenuItem mnuFileSeperator1;
		private System.Windows.Forms.MenuItem mnuFileSaveAllMenu;
		private System.Windows.Forms.ToolBarButton btnSaveAll;
		private System.Windows.Forms.ToolBarButton btnS3;
		private System.Windows.Forms.StatusBarPanel pnlDateAndTime;
		private System.Windows.Forms.StatusBarPanel pnlCommentsText;

		/// <summary>
		/// Readonly field for keeping Profile Path & Name.
		/// </summary>
		private readonly string ProfileName;

		/// <summary>
		/// Save all important properties.
		/// Defination for this structure is in "Environment_Variable.cs" File.
		/// </summary>
		private Environments_Variables m_Options;
		private System.Windows.Forms.MenuItem mnuViewToolTipMenu;

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
		/// <param name="CommandLine">Command Line</param>
		public frmMainVectorForm(string[] args)
		{
			InitializeComponent();

			for(int l_intIndexer = 0; l_intIndexer < m_strStatusBarToolTips.Length; l_intIndexer++)
				m_strStatusBarToolTips[l_intIndexer] = stbMainStatusBar.Panels[l_intIndexer + 1].ToolTipText;

			ProfileName = Application.StartupPath //.LocalUserAppDataPath
				+ "\\Vector_Profile.ini";
			m_Options.LoadProfile(ProfileName, true);
			WindowState = (m_Options.blnMainFrameMaximized)?
				System.Windows.Forms.FormWindowState.Maximized:
				System.Windows.Forms.FormWindowState.Normal;
			ClientSize = m_Options.sizClientSize;
			ShowToolTip = m_Options.blnIsActiveToolTip;
			tlbMainToolBar.Visible = m_Options.blnShowToolBar;
			ntiNotifyIcon.Visible = m_Options.blnShowNotifyIcon;
			stbMainStatusBar.Visible = m_Options.blnShowStatusBar;
			

			if(args.Length != 0) {
				foreach(string filename in args) {
					CreateNewWindow(
						//ArdeshirV.Utility.GlabalUtility.GetFileName(filename, 7),
						GetFileName(filename),
						filename, true).Screen.LoadFromFile(filename);
					//MessageBox.Show(GetFileName(filename));
				}
			} else if(m_Options.blnNewDocAtStartUp)
				mnuFileNewMenu_Click(null, null);

			m_timTimer = new Timer();
			m_timTimer.Interval = 100;
			m_timTimer.Enabled = true;
			m_timTimer.Tick += new EventHandler(OnTimerTicker);
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Get or Set Environment Variable.
		/// </summary>
		public Environments_Variables Option
		{
			get
			{
				return m_Options;
			}
			set
			{
				m_Options = value;
			}
		}

		/// <summary>
		/// Gets or sets main panle in status bar for showing help about current action.
		/// </summary>
		public string HelpString
		{
			get
			{
				return stbMainStatusBar.Text;
			}
			set
			{
				stbMainStatusBar.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets Tool tip property.
		/// </summary>
		public bool ShowToolTip
		{
			get
			{
				return m_Options.blnIsActiveToolTip;
			}
			set
			{
				bool l_blnTollbarVisibility = tlbMainToolBar.Visible;

				tltMainTooltip.Active =
				mnuViewToolTipMenu.Checked =
				tlbMainToolBar.ShowToolTips =
				m_Options.blnIsActiveToolTip = value;

				foreach(frmChild frm in MdiChildren)
					((frmChild)frm).IsActiveToolTip = value;

				if(value)
					for(int l_intIndexer = 0; l_intIndexer < m_strStatusBarToolTips.Length; l_intIndexer++)
						stbMainStatusBar.Panels[l_intIndexer + 1].ToolTipText = m_strStatusBarToolTips[l_intIndexer];
				else
					for(int l_intIndexer = 0; l_intIndexer < m_strStatusBarToolTips.Length; l_intIndexer++)
						stbMainStatusBar.Panels[l_intIndexer + 1].ToolTipText = "";

				tlbMainToolBar.Visible = l_blnTollbarVisibility;
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Startup method

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			try
			{
				Application.Run(new frmMainVectorForm(args));

				return 0;
			}
#if DEBUG
			catch(Exception	exp)
#else
			catch
#endif
			{
#if DEBUG
				MessageBox.Show(exp.Message + "\n" + "Stack Trace:\n"  + exp.StackTrace,
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
				return -1;
			}	
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Event Handlers

		/// <summary>
		/// Occured whenever File menu has been clicked.
		/// </summary>
		/// <param name="sender">File menu</param>
		/// <param name="e">Event argument</param>
		private void mnuFileMenu_Popup(object sender, System.EventArgs e)
		{
			mnuFileSaveAllMenu.Visible = MdiChildren.Length > 0;
		}

		/// <summary>
		/// Event Handler for File | New Menu. (Create New Vector Document)
		/// </summary>
		/// <param name="sender">File new menu</param>
		/// <param name="e">Event argument</param>
		private void mnuFileNewMenu_Click(object sender, System.EventArgs e)
		{
			string l_strName = "Untitled-" +
				((MdiChildren.Length + 1).ToString());
			frmChild l_frm = CreateNewWindow(l_strName,
				l_strName + ".vector", false);

			if(l_frm == null)
				throw new OutOfMemoryException("Unable to create window");

			if(MdiChildren.Length <= 1)
				l_frm.WindowState = (m_Options.blnChildMaximized)?
					FormWindowState.Maximized : FormWindowState.Normal;
		}

		/// <summary>
		/// Event Handler for File | Open Menu. (Open a Vector Document from disk)
		/// </summary>
		/// <param name="sender">File open menu</param>
		/// <param name="e">Event argument</param>
		private void mnuFileOpenMenu_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog l_dlgOpenFile = new OpenFileDialog();
			l_dlgOpenFile.Title = "Open Vector Document";
			l_dlgOpenFile.Filter = 
				"Vector Documents (*.vector)|*.vector|All Files (*.*)|*.*";
			l_dlgOpenFile.DefaultExt = "vector";
			l_dlgOpenFile.Multiselect = true;
			frmChild l_frmChildHandle = null;

			if(l_dlgOpenFile.ShowDialog(this) == DialogResult.OK)
			{
				foreach(string FileName in l_dlgOpenFile.FileNames)
				{
					if((l_frmChildHandle = CreateNewWindow(
					//ArdeshirV.Utility.GlabalUtility.GetFileName(FileName, 7),
					GetFileName(FileName), 
					FileName, true))== null)
						throw new OutOfMemoryException("Unable to create window");
					else
						l_frmChildHandle.Screen.LoadFromFile(FileName);
				}
			}
		}

		/// <summary>
		/// Event Handler File | Save All menu.
		/// </summary>
		/// <param name="sender">File save all menu</param>
		/// <param name="e">Event argument</param>
		private void mnuFileSaveAllMenu_Click(object sender, System.EventArgs e)
		{
			foreach(Form frm in MdiChildren)
				((frmChild)frm).mnuChildFileSave_Click(null, null);
		}

		/// <summary>
		/// Event Handler File | Exit menu. (Exit from Application)
		/// </summary>
		/// <param name="sender">File exit menu</param>
		/// <param name="e">Event argument</param>
		private void mnuFileExitMenu_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Event Handler for View | Tool Bar Menu. (Show/Hide Tool Bar)
		/// </summary>
		/// <param name="sender">View tool bar menu</param>
		/// <param name="e">Event argument</param>
		private void mnuViewToolBarMenu_Click(object sender, System.EventArgs e)
		{
			tlbMainToolBar.Visible =
			mnuViewToolBarMenu.Checked =
			!mnuViewToolBarMenu.Checked;
		}

		/// <summary>
		/// Event Handler for View | Status bar Menu. (Show/Hide Status Bar)
		/// </summary>
		/// <param name="sender">View status bar menu</param>
		/// <param name="e">Event argument</param>
		private void mnuViewStatusBarMenu_Click(object sender, System.EventArgs e)
		{
			stbMainStatusBar.Visible =
			mnuViewStatusBarMenu.Checked =
			!mnuViewStatusBarMenu.Checked;
		}

		/// <summary>
		/// Occured whenever view show tool tip menu item has been clicked.
		/// </summary>
		/// <param name="sender">Show tool tip menu</param>
		/// <param name="e">Event argument</param>
		private void mnuViewToolTipMenu_Click(object sender, System.EventArgs e)
		{
			ShowToolTip = !mnuViewToolTipMenu.Checked;
		}

		/// <summary>
		/// Event Handler for Tools | Options Menu. (Gonfigration Application)
		/// </summary>
		/// <param name="sender">Tools options menu</param>
		/// <param name="e">Event argument</param>
		private void mnuToolsOptionMenu_Click(object sender, System.EventArgs e)
		{
			Options l_frmOption = new Options();
			l_frmOption.Option = m_Options;

			if(l_frmOption.ShowDialog(this)== DialogResult.OK)
			{
				m_Options = l_frmOption.Option;

				foreach(frmChild frm in MdiChildren)
				{
					frm.Screen.Options = m_Options;
					frm.ResultPanelUpdate(new vector(
						new PointD(0), new PointD(0)));
				}

				ntiNotifyIcon.Visible = l_frmOption.Option.blnShowNotifyIcon;
			}	
		}

		/// <summary>
		/// Event Handler for Window | Cascade Menu. (Cascade child windows)
		/// </summary>
		/// <param name="sender">Window cascade menu</param>
		/// <param name="e">Event argument</param>
		private void mnuWindowCascadeMenu_Click(object sender, System.EventArgs e)
		{
			LayoutMdi(MdiLayout.Cascade);
		}

		/// <summary>
		/// Event Handler for Window | Tile Horizontal Menu.
		/// </summary>
		/// <param name="sender">Window tile horizontally</param>
		/// <param name="e">Event argument</param>
		private void mnuWindowTileHorizontallyMenu_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.TileHorizontal);
		}

		/// <summary>
		/// Event Handler for Window | Tile Vertical Menu.
		/// </summary>
		/// <param name="sender">Window tile vertically</param>
		/// <param name="e">Event argument</param>
		private void mnuWindowTileVerticallyMenu_Click(object sender, System.EventArgs e)
		{
			LayoutMdi(MdiLayout.TileVertical);
		}

		/// <summary>
		/// Event Handler for Window | Close Menu. (Close Active Child Form)
		/// </summary>
		/// <param name="sender">Window close menu</param>
		/// <param name="e">Event argument</param>
		private void mnuWindowCloseMenu_Click(object sender, System.EventArgs e)
		{
			if(MdiChildren.Length == 0)
				return;

			ActiveMdiChild.Close();
		}

		/// <summary>
		/// Event Handler for Window | Close All Menu. (Close All Child Windows)
		/// </summary>
		/// <param name="sender">Window close all menu</param>
		/// <param name="e">Event argument</param>
		private void mnuWindowCloseAllMenu_Click(object sender, System.EventArgs e)
		{
			int l_intCount = MdiChildren.Length;

			for(int i = 0;i < l_intCount;i++)
				MdiChildren[0].Close();
		}

		/// <summary>
		/// Event Handler for Help | Abou Menu.(Show AboutBox for showing Version & etc..
		/// </summary>
		/// <param name="sender">Help about menu</param>
		/// <param name="e">Event argument</param>
		private void mnuHelpAboutMenu_Click(object sender, System.EventArgs e)
		{
			frmAbout l_frmAbout = new frmAbout();
			l_frmAbout.ShowDialog(this);
		}

		/// <summary>
		/// Draw owner draw menu.
		/// </summary>
		/// <param name="sender">Menu Item</param>
		/// <param name="e">Event Argument</param>
		public void Draw_Menu_Items(object sender, DrawItemEventArgs e)
		{
			try
			{
				MenuItem l_mnu = (MenuItem)sender;
				Font l_fnt = new Font("Tahoma", 8);
				string l_strShortcut = "";
				int l_intImageIndex = -1;
				Color l_clrText = SystemColors.MenuText;
				Rectangle l_rctBounds = new Rectangle(e.Bounds.Left + 20, e.Bounds.Top + 2,
					e.Bounds.Right + 1, e.Bounds.Bottom - 2);
				StringFormat l_stf = new StringFormat();
				float[] l_fltTS = {0};
				l_stf.SetTabStops(90, l_fltTS);
				l_stf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
				l_stf.FormatFlags = System.Drawing.StringFormatFlags.DisplayFormatControl;

				if((e.State & DrawItemState.Selected)== DrawItemState.Selected)
				{
					e.DrawBackground();
					l_clrText = SystemColors.HighlightText;
				}
				else
					e.Graphics.FillRectangle(SystemBrushes.Menu, e.Bounds);

				switch(l_mnu.Text)
				{
					case "&New":
						l_intImageIndex = 0;
						l_strShortcut = "\tCtrl+N";
						break;
					case "&Open...":
						l_intImageIndex = 1;
						l_strShortcut = "\tCtrl+O";
						break;
					case "&Save":
						l_intImageIndex = 2;
						l_strShortcut = "\tCtrl+S";
						break;
					case "Sa&ve All":
						l_intImageIndex = 19;
						l_strShortcut = "\tCtrl+Shift+S";
						break;
					case "Save &Image...":
						l_intImageIndex = 3;
						l_strShortcut = "\tCtrl+I";
						break;
					case "&Print...":
						l_intImageIndex = 4;
						l_strShortcut = "\tCtrl+P";
						break;
					case "Print Pre&view...":
						l_intImageIndex = 5;
						l_strShortcut = "\tCtrl+Shift+V";
						break;
					case "Print &Setup...":
						l_intImageIndex = 6;
						l_strShortcut = "\tCtrl+Shift+P";
						break;
					case "Op&tions...":
						l_intImageIndex = 7;
						l_strShortcut = "\tCtrl+T";
						break;
					case "&About Vector...":
						l_intImageIndex = 8;
						l_strShortcut = "\tF1";
						break;
					case "E&xit":
						l_strShortcut = "\tCtrl+Q";
						break;
					case "Save &As...":
						break;
					case "&Cascade":
						l_intImageIndex = 9;
						l_strShortcut = "\tCtrl+D";
						break;
					case "&Tile Horizontally":
						l_intImageIndex = 10;
						break;
					case "T&ile Vertically":
						l_intImageIndex = 11;
						break;
					case "C&lose":
						l_intImageIndex = 12;
						l_strShortcut = "\tCtrl+X";
						break;
					case "Close &All":
						l_intImageIndex = 13;
						break;
					case "&Create New Vector...":
						l_intImageIndex = 14;
						l_strShortcut = "                               Ctrl+E";
						break;
					case "Delete &All Vectors":
						l_intImageIndex = 16;
						break;
					case "Delete &Last Created Vector":
						l_intImageIndex = 15;
						break;
					case "&Calculator...":
						l_intImageIndex = 17;
						l_strShortcut = "\tCtrl+U";
						break;
					case "P&roperties...":
						l_intImageIndex = 18;
						break;
					case "&Zoom...":
						l_intImageIndex = 20;
						l_strShortcut = "\tCtrl+Z";
						break;
					case "Delete &Selected Vector":
						l_intImageIndex = 21;
						break;
#if DEBUG
				default:
					MessageBox.Show("See Draw_Menu_Items catched unknown" +
						" menu item.\nUnknown menu item: " + l_mnu.Text);
					break;
#endif
				}

				if(l_intImageIndex != -1)
					e.Graphics.DrawImageUnscaled(
						imgImageListForToolbar.Images[l_intImageIndex],
						e.Bounds.Left + 1, e.Bounds.Top + 1);

				e.Graphics.DrawString(l_mnu.Text + l_strShortcut, l_fnt,
					new SolidBrush(l_clrText), l_rctBounds, l_stf);
			}
#if DEBUG
			catch(Exception exp)
#else
			catch
#endif
			{
#if DEBUG
				MessageBox.Show(exp.Message + "\nStack Trace: " + exp.StackTrace);
#endif
			}
		}
		
		private string GetFileName(string FullFilePathName) {
			return new System.IO.FileInfo(FullFilePathName).Name;
		}

		/// <summary>
		/// Measure owner draw menu.
		/// </summary>
		/// <param name="sender">Menu item</param>
		/// <param name="e">Event argument</param>
		public void Draw_Measure_Items(object sender, MeasureItemEventArgs e)
		{
			try
			{
				MenuItem l_mnu = (MenuItem)sender;
				StringFormat l_stf = new StringFormat();
				l_stf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;
				Size l_siz = e.Graphics.MeasureString(l_mnu.Text, Font,
					new Point(0), l_stf).ToSize();;
				e.ItemHeight = Math.Max(l_siz.Height + 2,
					SystemInformation.SmallIconSize.Height + 2);
				MenuItem l_mnuParent = (MenuItem)l_mnu.Parent;

				if(l_mnuParent.Equals(mnuFileMenu))
					e.ItemWidth = 150;
				else if(l_mnuParent.Equals(mnuHelpMenu))
					e.ItemWidth = 120;
				else if(l_mnuParent.Equals(mnuToolsMenu))
					e.ItemWidth = 140;
				else if(l_mnuParent.Equals(mnuWindowMenu))
					e.ItemWidth = 120;
				else
					e.ItemWidth = 120;
			}
#if DEBUG
			catch(Exception exp)
#else
			catch
#endif
			{
#if DEBUG
				MessageBox.Show(exp.Message + "\nStack Trace: " + exp.StackTrace);
#endif
			}
		}

		/// <summary>
		/// Event Handler for Main ToolBar Buttons.
		/// </summary>
		/// <param name="sender">Tool bar button</param>
		/// <param name="e">Event argument</param>
		private void tlbMainToolBar_ButtonClick(object sender, 
			System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			bool l_blnChildExist = MdiChildren.Length != 0;
			frmChild l_frmChild =
				(l_blnChildExist)?((frmChild)ActiveMdiChild) : null;

			switch(tlbMainToolBar.Buttons.IndexOf(e.Button))
			{
				//File New
				case 0:
					mnuFileNewMenu_Click(null, null);
					break;
				//File Open:
				case 1:
					this.mnuFileOpenMenu_Click(null, null);
					break;
				//Save:
				case 2:
					if(l_blnChildExist)
						l_frmChild.mnuChildFileSave_Click(null, null);
					break;
				// Save all open document to disk.
				case 3:
					if(l_blnChildExist)
						mnuFileSaveAllMenu_Click(null, null);
					break;
				// Take picture from current document asn save to disk.
				case 4:
					if(l_blnChildExist)
						l_frmChild.mnuChildFileSaveAPicture_Click(null, null);
					break;
				//Seperator:
				//case 5: 
				//	break;
				//Print:
				case 6:
					if(l_blnChildExist)
						l_frmChild.mnuChildFilePrint_Click(null, null);
					break;
				//Print Preview:
				case 7:
					if(l_blnChildExist)
						l_frmChild.mnuChildFilePrintPreview_Click(null, null);
					break;
				//Print Setup:
				case 8:
					if(l_blnChildExist)
						l_frmChild.mnuChildFilePrintSetup_Click(null, null);
					break;
				//Seperator:
				//case 9: 
				//	break;
				//Option:
				case 10:
					mnuToolsOptionMenu_Click(null, null);
					break;
				//Seperator:
				//case 9:
				//	break;
				//About:
				case 12:
					mnuHelpAboutMenu_Click(null, null);
					break;
#if	DEBUG
				//Unknown Button:
				default:
					throw new Exception("Unhandlered Toolbar Button");
#endif
			}
		}

		/// <summary>
		/// Event Handler for Context Menu that popup from notify icon:
		/// </summary>
		/// <param name="sender">About menu</param>
		/// <param name="e">Event argument</param>
		private void ctxmnuAbout_Click(object sender, System.EventArgs e)
		{
			if(!frmAbout.Exists)
			{
				frmAbout l_frmAbout = new frmAbout();
				l_frmAbout.StartPosition = FormStartPosition.CenterScreen;
				l_frmAbout.ShowDialog(this);
			}
		}

		/// <summary>
		/// Occured whenever notification context menu has been popuped.
		/// </summary>
		/// <param name="sender">Notify context menu</param>
		/// <param name="e">Event argument</param>
		private void mnuMainForNotifycationIcon_Popup(object sender, System.EventArgs e)
		{
            ctxmnuAbout.Enabled = !frmAbout.Exists;
		}

		/// <summary>
		/// Event Handler run whenever Mouse Move over the Notify icon
		/// </summary>
		/// <param name="sender">Notify icon</param>
		/// <param name="e">Event argument</param>
		private void ntiNotifyIcon_MouseMove(object sender, MouseEventArgs e)
		{
			string l_str;

			if(MdiChildren.Length > 0)
			{
				l_str = ActiveMdiChild.Text;

				if(l_str.Length > 40)
					l_str = "Current Document: [..." +
						l_str.Substring(l_str.Length-30) + "]";
				else
					l_str = "Current Document: [" + l_str + "]";
			}
			else	
				l_str = "Vector";

			ntiNotifyIcon.Text = l_str;
		}

		/// <summary>
		/// Occured whenever notify icon has been double clicked.
		/// </summary>
		/// <param name="sender">Notify icon</param>
		/// <param name="e">Event argument</param>
		private void ntiNotifyIcon_DoubleClick(object sender, System.EventArgs e)
		{
			TopMost = true;
			TopMost = false;
			WindowState = (m_Options.blnMainFrameMaximized) ?
				FormWindowState.Maximized: FormWindowState.Normal;
		}

		/// <summary>
		/// Occured whenever main form has been closed.
		/// </summary>
		/// <param name="sender">Main form</param>
		/// <param name="e">Event argument</param>
		private void frmMainVectorForm_Closed(object sender, System.EventArgs e)
		{
			if(MdiChildren.Length > 0)
                m_Options.blnChildMaximized = ActiveMdiChild.WindowState ==
					FormWindowState.Maximized;

			m_Options.blnShowToolBar = tlbMainToolBar.Visible;
			m_Options.blnShowNotifyIcon = ntiNotifyIcon.Visible;
			m_Options.blnIsActiveToolTip = tltMainTooltip.Active;
			m_Options.blnShowStatusBar = stbMainStatusBar.Visible;

			if(!(m_Options.blnMainFrameMaximized = 
			(WindowState == FormWindowState.Maximized)))
				m_Options.sizClientSize = ClientSize;
			
			m_Options.SaveProfile(ProfileName);
			ntiNotifyIcon.Visible = false;
		}

		/// <summary>
		/// Occured whenever child form closed.
		/// </summary>
		/// <param name="sender">Child Form</param>
		/// <param name="e">Event Argument</param>
		private void ChildClosed(object sender,EventArgs e)
		{
			if(MdiChildren.Length <= 2)
			{
				btnSaveAll.Visible = false;
				mnuFileSaveAllMenu.Visible = false;

				if(MdiChildren.Length <= 1)
					btnS1.Visible =
					btnSave.Visible =
					btnPrint.Visible =
					mnuWindowMenu.Visible =
					btnPrintSetup.Visible =
					btnPrintPreview.Visible =
					btnSaveAsPicture.Visible = false;
			}

			pnlCountOfWindow.Text = (MdiChildren.Length - 1).ToString();
		}

		/// <summary>
		/// Occured every 100 milisecond.
		/// </summary>
		/// <param name="sender">Timer Object</param>
		/// <param name="e">Event Argument</param>
		private void OnTimerTicker(object sender, EventArgs e)
		{
			pnlDateAndTime.Text = DateTime.Now.ToString();
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Utility Functions

		/// <summary>
		/// Create New Child mdi window(form) with name & caption.
		/// </summary>
		/// <param name="FormText">Child Form Text</param>
		/// <param name="FileName">File Path Name</param>
		/// <returns>an Instance of new window</returns>
		private frmChild CreateNewWindow(string FormText, string FileName, bool IsSaved)
		{
			frmChild l_frmChild = new frmChild(IsSaved, tltMainTooltip.Active);

			l_frmChild.Tag = FileName;
			l_frmChild.Text = FormText;
			l_frmChild.MdiParent = this;
			l_frmChild.Screen.Options = m_Options;
			l_frmChild.Show();
			pnlCountOfWindow.Text = (MdiChildren.Length).ToString();
			l_frmChild.ChildClosed += new EventHandler(ChildClosed);
			btnS1.Visible =
			btnSave.Visible =
			btnPrint.Visible =
			mnuWindowMenu.Visible =
			btnPrintSetup.Visible =
			btnPrintPreview.Visible =
			btnSaveAsPicture.Visible = true;
			btnS1.Style = ToolBarButtonStyle.PushButton;
			btnS1.Style = ToolBarButtonStyle.Separator;

			if(MdiChildren.Length > 1)
			{
				btnSaveAll.Visible = true;
				mnuFileSaveAllMenu.Visible = true;
			}

			return l_frmChild;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMainVectorForm));
			this.mnuMainMenu = new System.Windows.Forms.MainMenu();
			this.mnuFileMenu = new System.Windows.Forms.MenuItem();
			this.mnuFileNewMenu = new System.Windows.Forms.MenuItem();
			this.mnuFileOpenMenu = new System.Windows.Forms.MenuItem();
			this.mnuFileSeperator1 = new System.Windows.Forms.MenuItem();
			this.mnuFileSaveAllMenu = new System.Windows.Forms.MenuItem();
			this.mnuFileExitMenu = new System.Windows.Forms.MenuItem();
			this.mnuViewMenu = new System.Windows.Forms.MenuItem();
			this.mnuViewToolBarMenu = new System.Windows.Forms.MenuItem();
			this.mnuViewStatusBarMenu = new System.Windows.Forms.MenuItem();
			this.mnuViewToolTipMenu = new System.Windows.Forms.MenuItem();
			this.mnuToolsMenu = new System.Windows.Forms.MenuItem();
			this.mnuToolsOptionMenu = new System.Windows.Forms.MenuItem();
			this.mnuWindowMenu = new System.Windows.Forms.MenuItem();
			this.mnuWindowCascadeMenu = new System.Windows.Forms.MenuItem();
			this.mnuWindowTileHorizontallyMenu = new System.Windows.Forms.MenuItem();
			this.mnuWindowTileVerticallyMenu = new System.Windows.Forms.MenuItem();
			this.mnuWindowSeperator1Menu = new System.Windows.Forms.MenuItem();
			this.mnuWindowCloseMenu = new System.Windows.Forms.MenuItem();
			this.mnuWindowCloseAllMenu = new System.Windows.Forms.MenuItem();
			this.mnuHelpMenu = new System.Windows.Forms.MenuItem();
			this.mnuHelpAboutMenu = new System.Windows.Forms.MenuItem();
			this.tlbMainToolBar = new System.Windows.Forms.ToolBar();
			this.btnNew = new System.Windows.Forms.ToolBarButton();
			this.btnOpen = new System.Windows.Forms.ToolBarButton();
			this.btnSave = new System.Windows.Forms.ToolBarButton();
			this.btnSaveAll = new System.Windows.Forms.ToolBarButton();
			this.btnSaveAsPicture = new System.Windows.Forms.ToolBarButton();
			this.btnS1 = new System.Windows.Forms.ToolBarButton();
			this.btnPrint = new System.Windows.Forms.ToolBarButton();
			this.btnPrintPreview = new System.Windows.Forms.ToolBarButton();
			this.btnPrintSetup = new System.Windows.Forms.ToolBarButton();
			this.btnS2 = new System.Windows.Forms.ToolBarButton();
			this.btnOptions = new System.Windows.Forms.ToolBarButton();
			this.btnS3 = new System.Windows.Forms.ToolBarButton();
			this.btnAbout = new System.Windows.Forms.ToolBarButton();
			this.imgImageListForToolbar = new System.Windows.Forms.ImageList(this.components);
			this.ntiNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.mnuMainForNotifycationIcon = new System.Windows.Forms.ContextMenu();
			this.ctxmnuAbout = new System.Windows.Forms.MenuItem();
			this.ctxmnuSeperator = new System.Windows.Forms.MenuItem();
			this.ctxmnuExit = new System.Windows.Forms.MenuItem();
			this.stbMainStatusBar = new System.Windows.Forms.StatusBar();
			this.pnlCommentsText = new System.Windows.Forms.StatusBarPanel();
			this.pnlCountOfWindow = new System.Windows.Forms.StatusBarPanel();
			this.pnlDateAndTime = new System.Windows.Forms.StatusBarPanel();
			this.tltMainTooltip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pnlCommentsText)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlCountOfWindow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlDateAndTime)).BeginInit();
			this.SuspendLayout();
			// 
			// mnuMainMenu
			// 
			this.mnuMainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mnuFileMenu,
																						this.mnuViewMenu,
																						this.mnuToolsMenu,
																						this.mnuWindowMenu,
																						this.mnuHelpMenu});
			this.mnuMainMenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mnuMainMenu.RightToLeft")));
			// 
			// mnuFileMenu
			// 
			this.mnuFileMenu.Enabled = ((bool)(resources.GetObject("mnuFileMenu.Enabled")));
			this.mnuFileMenu.Index = 0;
			this.mnuFileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mnuFileNewMenu,
																						this.mnuFileOpenMenu,
																						this.mnuFileSeperator1,
																						this.mnuFileSaveAllMenu,
																						this.mnuFileExitMenu});
			this.mnuFileMenu.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.mnuFileMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuFileMenu.Shortcut")));
			this.mnuFileMenu.ShowShortcut = ((bool)(resources.GetObject("mnuFileMenu.ShowShortcut")));
			this.mnuFileMenu.Text = resources.GetString("mnuFileMenu.Text");
			this.mnuFileMenu.Visible = ((bool)(resources.GetObject("mnuFileMenu.Visible")));
			this.mnuFileMenu.Popup += new System.EventHandler(this.mnuFileMenu_Popup);
			// 
			// mnuFileNewMenu
			// 
			this.mnuFileNewMenu.Enabled = ((bool)(resources.GetObject("mnuFileNewMenu.Enabled")));
			this.mnuFileNewMenu.Index = 0;
			this.mnuFileNewMenu.OwnerDraw = true;
			this.mnuFileNewMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuFileNewMenu.Shortcut")));
			this.mnuFileNewMenu.ShowShortcut = ((bool)(resources.GetObject("mnuFileNewMenu.ShowShortcut")));
			this.mnuFileNewMenu.Text = resources.GetString("mnuFileNewMenu.Text");
			this.mnuFileNewMenu.Visible = ((bool)(resources.GetObject("mnuFileNewMenu.Visible")));
			this.mnuFileNewMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuFileNewMenu.Click += new System.EventHandler(this.mnuFileNewMenu_Click);
			this.mnuFileNewMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// mnuFileOpenMenu
			// 
			this.mnuFileOpenMenu.Enabled = ((bool)(resources.GetObject("mnuFileOpenMenu.Enabled")));
			this.mnuFileOpenMenu.Index = 1;
			this.mnuFileOpenMenu.OwnerDraw = true;
			this.mnuFileOpenMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuFileOpenMenu.Shortcut")));
			this.mnuFileOpenMenu.ShowShortcut = ((bool)(resources.GetObject("mnuFileOpenMenu.ShowShortcut")));
			this.mnuFileOpenMenu.Text = resources.GetString("mnuFileOpenMenu.Text");
			this.mnuFileOpenMenu.Visible = ((bool)(resources.GetObject("mnuFileOpenMenu.Visible")));
			this.mnuFileOpenMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuFileOpenMenu.Click += new System.EventHandler(this.mnuFileOpenMenu_Click);
			this.mnuFileOpenMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// mnuFileSeperator1
			// 
			this.mnuFileSeperator1.Enabled = ((bool)(resources.GetObject("mnuFileSeperator1.Enabled")));
			this.mnuFileSeperator1.Index = 2;
			this.mnuFileSeperator1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuFileSeperator1.Shortcut")));
			this.mnuFileSeperator1.ShowShortcut = ((bool)(resources.GetObject("mnuFileSeperator1.ShowShortcut")));
			this.mnuFileSeperator1.Text = resources.GetString("mnuFileSeperator1.Text");
			this.mnuFileSeperator1.Visible = ((bool)(resources.GetObject("mnuFileSeperator1.Visible")));
			// 
			// mnuFileSaveAllMenu
			// 
			this.mnuFileSaveAllMenu.Enabled = ((bool)(resources.GetObject("mnuFileSaveAllMenu.Enabled")));
			this.mnuFileSaveAllMenu.Index = 3;
			this.mnuFileSaveAllMenu.OwnerDraw = true;
			this.mnuFileSaveAllMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuFileSaveAllMenu.Shortcut")));
			this.mnuFileSaveAllMenu.ShowShortcut = ((bool)(resources.GetObject("mnuFileSaveAllMenu.ShowShortcut")));
			this.mnuFileSaveAllMenu.Text = resources.GetString("mnuFileSaveAllMenu.Text");
			this.mnuFileSaveAllMenu.Visible = ((bool)(resources.GetObject("mnuFileSaveAllMenu.Visible")));
			this.mnuFileSaveAllMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuFileSaveAllMenu.Click += new System.EventHandler(this.mnuFileSaveAllMenu_Click);
			this.mnuFileSaveAllMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// mnuFileExitMenu
			// 
			this.mnuFileExitMenu.Enabled = ((bool)(resources.GetObject("mnuFileExitMenu.Enabled")));
			this.mnuFileExitMenu.Index = 4;
			this.mnuFileExitMenu.MergeOrder = 1;
			this.mnuFileExitMenu.OwnerDraw = true;
			this.mnuFileExitMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuFileExitMenu.Shortcut")));
			this.mnuFileExitMenu.ShowShortcut = ((bool)(resources.GetObject("mnuFileExitMenu.ShowShortcut")));
			this.mnuFileExitMenu.Text = resources.GetString("mnuFileExitMenu.Text");
			this.mnuFileExitMenu.Visible = ((bool)(resources.GetObject("mnuFileExitMenu.Visible")));
			this.mnuFileExitMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuFileExitMenu.Click += new System.EventHandler(this.mnuFileExitMenu_Click);
			this.mnuFileExitMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// mnuViewMenu
			// 
			this.mnuViewMenu.Enabled = ((bool)(resources.GetObject("mnuViewMenu.Enabled")));
			this.mnuViewMenu.Index = 1;
			this.mnuViewMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mnuViewToolBarMenu,
																						this.mnuViewStatusBarMenu,
																						this.mnuViewToolTipMenu});
			this.mnuViewMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuViewMenu.Shortcut")));
			this.mnuViewMenu.ShowShortcut = ((bool)(resources.GetObject("mnuViewMenu.ShowShortcut")));
			this.mnuViewMenu.Text = resources.GetString("mnuViewMenu.Text");
			this.mnuViewMenu.Visible = ((bool)(resources.GetObject("mnuViewMenu.Visible")));
			// 
			// mnuViewToolBarMenu
			// 
			this.mnuViewToolBarMenu.Checked = true;
			this.mnuViewToolBarMenu.Enabled = ((bool)(resources.GetObject("mnuViewToolBarMenu.Enabled")));
			this.mnuViewToolBarMenu.Index = 0;
			this.mnuViewToolBarMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuViewToolBarMenu.Shortcut")));
			this.mnuViewToolBarMenu.ShowShortcut = ((bool)(resources.GetObject("mnuViewToolBarMenu.ShowShortcut")));
			this.mnuViewToolBarMenu.Text = resources.GetString("mnuViewToolBarMenu.Text");
			this.mnuViewToolBarMenu.Visible = ((bool)(resources.GetObject("mnuViewToolBarMenu.Visible")));
			this.mnuViewToolBarMenu.Click += new System.EventHandler(this.mnuViewToolBarMenu_Click);
			// 
			// mnuViewStatusBarMenu
			// 
			this.mnuViewStatusBarMenu.Checked = true;
			this.mnuViewStatusBarMenu.Enabled = ((bool)(resources.GetObject("mnuViewStatusBarMenu.Enabled")));
			this.mnuViewStatusBarMenu.Index = 1;
			this.mnuViewStatusBarMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuViewStatusBarMenu.Shortcut")));
			this.mnuViewStatusBarMenu.ShowShortcut = ((bool)(resources.GetObject("mnuViewStatusBarMenu.ShowShortcut")));
			this.mnuViewStatusBarMenu.Text = resources.GetString("mnuViewStatusBarMenu.Text");
			this.mnuViewStatusBarMenu.Visible = ((bool)(resources.GetObject("mnuViewStatusBarMenu.Visible")));
			this.mnuViewStatusBarMenu.Click += new System.EventHandler(this.mnuViewStatusBarMenu_Click);
			// 
			// mnuViewToolTipMenu
			// 
			this.mnuViewToolTipMenu.Checked = true;
			this.mnuViewToolTipMenu.Enabled = ((bool)(resources.GetObject("mnuViewToolTipMenu.Enabled")));
			this.mnuViewToolTipMenu.Index = 2;
			this.mnuViewToolTipMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuViewToolTipMenu.Shortcut")));
			this.mnuViewToolTipMenu.ShowShortcut = ((bool)(resources.GetObject("mnuViewToolTipMenu.ShowShortcut")));
			this.mnuViewToolTipMenu.Text = resources.GetString("mnuViewToolTipMenu.Text");
			this.mnuViewToolTipMenu.Visible = ((bool)(resources.GetObject("mnuViewToolTipMenu.Visible")));
			this.mnuViewToolTipMenu.Click += new System.EventHandler(this.mnuViewToolTipMenu_Click);
			// 
			// mnuToolsMenu
			// 
			this.mnuToolsMenu.Enabled = ((bool)(resources.GetObject("mnuToolsMenu.Enabled")));
			this.mnuToolsMenu.Index = 2;
			this.mnuToolsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnuToolsOptionMenu});
			this.mnuToolsMenu.MergeOrder = 1;
			this.mnuToolsMenu.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.mnuToolsMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuToolsMenu.Shortcut")));
			this.mnuToolsMenu.ShowShortcut = ((bool)(resources.GetObject("mnuToolsMenu.ShowShortcut")));
			this.mnuToolsMenu.Text = resources.GetString("mnuToolsMenu.Text");
			this.mnuToolsMenu.Visible = ((bool)(resources.GetObject("mnuToolsMenu.Visible")));
			// 
			// mnuToolsOptionMenu
			// 
			this.mnuToolsOptionMenu.Enabled = ((bool)(resources.GetObject("mnuToolsOptionMenu.Enabled")));
			this.mnuToolsOptionMenu.Index = 0;
			this.mnuToolsOptionMenu.MergeOrder = 1;
			this.mnuToolsOptionMenu.OwnerDraw = true;
			this.mnuToolsOptionMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuToolsOptionMenu.Shortcut")));
			this.mnuToolsOptionMenu.ShowShortcut = ((bool)(resources.GetObject("mnuToolsOptionMenu.ShowShortcut")));
			this.mnuToolsOptionMenu.Text = resources.GetString("mnuToolsOptionMenu.Text");
			this.mnuToolsOptionMenu.Visible = ((bool)(resources.GetObject("mnuToolsOptionMenu.Visible")));
			this.mnuToolsOptionMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuToolsOptionMenu.Click += new System.EventHandler(this.mnuToolsOptionMenu_Click);
			this.mnuToolsOptionMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// mnuWindowMenu
			// 
			this.mnuWindowMenu.Enabled = ((bool)(resources.GetObject("mnuWindowMenu.Enabled")));
			this.mnuWindowMenu.Index = 3;
			this.mnuWindowMenu.MdiList = true;
			this.mnuWindowMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.mnuWindowCascadeMenu,
																						  this.mnuWindowTileHorizontallyMenu,
																						  this.mnuWindowTileVerticallyMenu,
																						  this.mnuWindowSeperator1Menu,
																						  this.mnuWindowCloseMenu,
																						  this.mnuWindowCloseAllMenu});
			this.mnuWindowMenu.MergeOrder = 1;
			this.mnuWindowMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuWindowMenu.Shortcut")));
			this.mnuWindowMenu.ShowShortcut = ((bool)(resources.GetObject("mnuWindowMenu.ShowShortcut")));
			this.mnuWindowMenu.Text = resources.GetString("mnuWindowMenu.Text");
			this.mnuWindowMenu.Visible = ((bool)(resources.GetObject("mnuWindowMenu.Visible")));
			// 
			// mnuWindowCascadeMenu
			// 
			this.mnuWindowCascadeMenu.Enabled = ((bool)(resources.GetObject("mnuWindowCascadeMenu.Enabled")));
			this.mnuWindowCascadeMenu.Index = 0;
			this.mnuWindowCascadeMenu.OwnerDraw = true;
			this.mnuWindowCascadeMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuWindowCascadeMenu.Shortcut")));
			this.mnuWindowCascadeMenu.ShowShortcut = ((bool)(resources.GetObject("mnuWindowCascadeMenu.ShowShortcut")));
			this.mnuWindowCascadeMenu.Text = resources.GetString("mnuWindowCascadeMenu.Text");
			this.mnuWindowCascadeMenu.Visible = ((bool)(resources.GetObject("mnuWindowCascadeMenu.Visible")));
			this.mnuWindowCascadeMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuWindowCascadeMenu.Click += new System.EventHandler(this.mnuWindowCascadeMenu_Click);
			this.mnuWindowCascadeMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// mnuWindowTileHorizontallyMenu
			// 
			this.mnuWindowTileHorizontallyMenu.Enabled = ((bool)(resources.GetObject("mnuWindowTileHorizontallyMenu.Enabled")));
			this.mnuWindowTileHorizontallyMenu.Index = 1;
			this.mnuWindowTileHorizontallyMenu.OwnerDraw = true;
			this.mnuWindowTileHorizontallyMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuWindowTileHorizontallyMenu.Shortcut")));
			this.mnuWindowTileHorizontallyMenu.ShowShortcut = ((bool)(resources.GetObject("mnuWindowTileHorizontallyMenu.ShowShortcut")));
			this.mnuWindowTileHorizontallyMenu.Text = resources.GetString("mnuWindowTileHorizontallyMenu.Text");
			this.mnuWindowTileHorizontallyMenu.Visible = ((bool)(resources.GetObject("mnuWindowTileHorizontallyMenu.Visible")));
			this.mnuWindowTileHorizontallyMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuWindowTileHorizontallyMenu.Click += new System.EventHandler(this.mnuWindowTileHorizontallyMenu_Click);
			this.mnuWindowTileHorizontallyMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// mnuWindowTileVerticallyMenu
			// 
			this.mnuWindowTileVerticallyMenu.Enabled = ((bool)(resources.GetObject("mnuWindowTileVerticallyMenu.Enabled")));
			this.mnuWindowTileVerticallyMenu.Index = 2;
			this.mnuWindowTileVerticallyMenu.OwnerDraw = true;
			this.mnuWindowTileVerticallyMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuWindowTileVerticallyMenu.Shortcut")));
			this.mnuWindowTileVerticallyMenu.ShowShortcut = ((bool)(resources.GetObject("mnuWindowTileVerticallyMenu.ShowShortcut")));
			this.mnuWindowTileVerticallyMenu.Text = resources.GetString("mnuWindowTileVerticallyMenu.Text");
			this.mnuWindowTileVerticallyMenu.Visible = ((bool)(resources.GetObject("mnuWindowTileVerticallyMenu.Visible")));
			this.mnuWindowTileVerticallyMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuWindowTileVerticallyMenu.Click += new System.EventHandler(this.mnuWindowTileVerticallyMenu_Click);
			this.mnuWindowTileVerticallyMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// mnuWindowSeperator1Menu
			// 
			this.mnuWindowSeperator1Menu.Enabled = ((bool)(resources.GetObject("mnuWindowSeperator1Menu.Enabled")));
			this.mnuWindowSeperator1Menu.Index = 3;
			this.mnuWindowSeperator1Menu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuWindowSeperator1Menu.Shortcut")));
			this.mnuWindowSeperator1Menu.ShowShortcut = ((bool)(resources.GetObject("mnuWindowSeperator1Menu.ShowShortcut")));
			this.mnuWindowSeperator1Menu.Text = resources.GetString("mnuWindowSeperator1Menu.Text");
			this.mnuWindowSeperator1Menu.Visible = ((bool)(resources.GetObject("mnuWindowSeperator1Menu.Visible")));
			// 
			// mnuWindowCloseMenu
			// 
			this.mnuWindowCloseMenu.Enabled = ((bool)(resources.GetObject("mnuWindowCloseMenu.Enabled")));
			this.mnuWindowCloseMenu.Index = 4;
			this.mnuWindowCloseMenu.OwnerDraw = true;
			this.mnuWindowCloseMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuWindowCloseMenu.Shortcut")));
			this.mnuWindowCloseMenu.ShowShortcut = ((bool)(resources.GetObject("mnuWindowCloseMenu.ShowShortcut")));
			this.mnuWindowCloseMenu.Text = resources.GetString("mnuWindowCloseMenu.Text");
			this.mnuWindowCloseMenu.Visible = ((bool)(resources.GetObject("mnuWindowCloseMenu.Visible")));
			this.mnuWindowCloseMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuWindowCloseMenu.Click += new System.EventHandler(this.mnuWindowCloseMenu_Click);
			this.mnuWindowCloseMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// mnuWindowCloseAllMenu
			// 
			this.mnuWindowCloseAllMenu.Enabled = ((bool)(resources.GetObject("mnuWindowCloseAllMenu.Enabled")));
			this.mnuWindowCloseAllMenu.Index = 5;
			this.mnuWindowCloseAllMenu.OwnerDraw = true;
			this.mnuWindowCloseAllMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuWindowCloseAllMenu.Shortcut")));
			this.mnuWindowCloseAllMenu.ShowShortcut = ((bool)(resources.GetObject("mnuWindowCloseAllMenu.ShowShortcut")));
			this.mnuWindowCloseAllMenu.Text = resources.GetString("mnuWindowCloseAllMenu.Text");
			this.mnuWindowCloseAllMenu.Visible = ((bool)(resources.GetObject("mnuWindowCloseAllMenu.Visible")));
			this.mnuWindowCloseAllMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuWindowCloseAllMenu.Click += new System.EventHandler(this.mnuWindowCloseAllMenu_Click);
			this.mnuWindowCloseAllMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// mnuHelpMenu
			// 
			this.mnuHelpMenu.Enabled = ((bool)(resources.GetObject("mnuHelpMenu.Enabled")));
			this.mnuHelpMenu.Index = 4;
			this.mnuHelpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mnuHelpAboutMenu});
			this.mnuHelpMenu.MergeOrder = 1;
			this.mnuHelpMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuHelpMenu.Shortcut")));
			this.mnuHelpMenu.ShowShortcut = ((bool)(resources.GetObject("mnuHelpMenu.ShowShortcut")));
			this.mnuHelpMenu.Text = resources.GetString("mnuHelpMenu.Text");
			this.mnuHelpMenu.Visible = ((bool)(resources.GetObject("mnuHelpMenu.Visible")));
			// 
			// mnuHelpAboutMenu
			// 
			this.mnuHelpAboutMenu.Enabled = ((bool)(resources.GetObject("mnuHelpAboutMenu.Enabled")));
			this.mnuHelpAboutMenu.Index = 0;
			this.mnuHelpAboutMenu.OwnerDraw = true;
			this.mnuHelpAboutMenu.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuHelpAboutMenu.Shortcut")));
			this.mnuHelpAboutMenu.ShowShortcut = ((bool)(resources.GetObject("mnuHelpAboutMenu.ShowShortcut")));
			this.mnuHelpAboutMenu.Text = resources.GetString("mnuHelpAboutMenu.Text");
			this.mnuHelpAboutMenu.Visible = ((bool)(resources.GetObject("mnuHelpAboutMenu.Visible")));
			this.mnuHelpAboutMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Draw_Menu_Items);
			this.mnuHelpAboutMenu.Click += new System.EventHandler(this.mnuHelpAboutMenu_Click);
			this.mnuHelpAboutMenu.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.Draw_Measure_Items);
			// 
			// tlbMainToolBar
			// 
			this.tlbMainToolBar.AccessibleDescription = ((string)(resources.GetObject("tlbMainToolBar.AccessibleDescription")));
			this.tlbMainToolBar.AccessibleName = ((string)(resources.GetObject("tlbMainToolBar.AccessibleName")));
			this.tlbMainToolBar.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tlbMainToolBar.Anchor")));
			this.tlbMainToolBar.Appearance = ((System.Windows.Forms.ToolBarAppearance)(resources.GetObject("tlbMainToolBar.Appearance")));
			this.tlbMainToolBar.AutoSize = ((bool)(resources.GetObject("tlbMainToolBar.AutoSize")));
			this.tlbMainToolBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbMainToolBar.BackgroundImage")));
			this.tlbMainToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																							  this.btnNew,
																							  this.btnOpen,
																							  this.btnSave,
																							  this.btnSaveAll,
																							  this.btnSaveAsPicture,
																							  this.btnS1,
																							  this.btnPrint,
																							  this.btnPrintPreview,
																							  this.btnPrintSetup,
																							  this.btnS2,
																							  this.btnOptions,
																							  this.btnS3,
																							  this.btnAbout});
			this.tlbMainToolBar.ButtonSize = ((System.Drawing.Size)(resources.GetObject("tlbMainToolBar.ButtonSize")));
			this.tlbMainToolBar.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tlbMainToolBar.Dock")));
			this.tlbMainToolBar.DropDownArrows = ((bool)(resources.GetObject("tlbMainToolBar.DropDownArrows")));
			this.tlbMainToolBar.Enabled = ((bool)(resources.GetObject("tlbMainToolBar.Enabled")));
			this.tlbMainToolBar.Font = ((System.Drawing.Font)(resources.GetObject("tlbMainToolBar.Font")));
			this.tlbMainToolBar.ImageList = this.imgImageListForToolbar;
			this.tlbMainToolBar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tlbMainToolBar.ImeMode")));
			this.tlbMainToolBar.Location = ((System.Drawing.Point)(resources.GetObject("tlbMainToolBar.Location")));
			this.tlbMainToolBar.Name = "tlbMainToolBar";
			this.tlbMainToolBar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tlbMainToolBar.RightToLeft")));
			this.tlbMainToolBar.ShowToolTips = ((bool)(resources.GetObject("tlbMainToolBar.ShowToolTips")));
			this.tlbMainToolBar.Size = ((System.Drawing.Size)(resources.GetObject("tlbMainToolBar.Size")));
			this.tlbMainToolBar.TabIndex = ((int)(resources.GetObject("tlbMainToolBar.TabIndex")));
			this.tlbMainToolBar.TextAlign = ((System.Windows.Forms.ToolBarTextAlign)(resources.GetObject("tlbMainToolBar.TextAlign")));
			this.tltMainTooltip.SetToolTip(this.tlbMainToolBar, resources.GetString("tlbMainToolBar.ToolTip"));
			this.tlbMainToolBar.Visible = ((bool)(resources.GetObject("tlbMainToolBar.Visible")));
			this.tlbMainToolBar.Wrappable = ((bool)(resources.GetObject("tlbMainToolBar.Wrappable")));
			this.tlbMainToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tlbMainToolBar_ButtonClick);
			// 
			// btnNew
			// 
			this.btnNew.Enabled = ((bool)(resources.GetObject("btnNew.Enabled")));
			this.btnNew.ImageIndex = ((int)(resources.GetObject("btnNew.ImageIndex")));
			this.btnNew.Text = resources.GetString("btnNew.Text");
			this.btnNew.ToolTipText = resources.GetString("btnNew.ToolTipText");
			this.btnNew.Visible = ((bool)(resources.GetObject("btnNew.Visible")));
			// 
			// btnOpen
			// 
			this.btnOpen.Enabled = ((bool)(resources.GetObject("btnOpen.Enabled")));
			this.btnOpen.ImageIndex = ((int)(resources.GetObject("btnOpen.ImageIndex")));
			this.btnOpen.Text = resources.GetString("btnOpen.Text");
			this.btnOpen.ToolTipText = resources.GetString("btnOpen.ToolTipText");
			this.btnOpen.Visible = ((bool)(resources.GetObject("btnOpen.Visible")));
			// 
			// btnSave
			// 
			this.btnSave.Enabled = ((bool)(resources.GetObject("btnSave.Enabled")));
			this.btnSave.ImageIndex = ((int)(resources.GetObject("btnSave.ImageIndex")));
			this.btnSave.Text = resources.GetString("btnSave.Text");
			this.btnSave.ToolTipText = resources.GetString("btnSave.ToolTipText");
			this.btnSave.Visible = ((bool)(resources.GetObject("btnSave.Visible")));
			// 
			// btnSaveAll
			// 
			this.btnSaveAll.Enabled = ((bool)(resources.GetObject("btnSaveAll.Enabled")));
			this.btnSaveAll.ImageIndex = ((int)(resources.GetObject("btnSaveAll.ImageIndex")));
			this.btnSaveAll.Text = resources.GetString("btnSaveAll.Text");
			this.btnSaveAll.ToolTipText = resources.GetString("btnSaveAll.ToolTipText");
			this.btnSaveAll.Visible = ((bool)(resources.GetObject("btnSaveAll.Visible")));
			// 
			// btnSaveAsPicture
			// 
			this.btnSaveAsPicture.Enabled = ((bool)(resources.GetObject("btnSaveAsPicture.Enabled")));
			this.btnSaveAsPicture.ImageIndex = ((int)(resources.GetObject("btnSaveAsPicture.ImageIndex")));
			this.btnSaveAsPicture.Text = resources.GetString("btnSaveAsPicture.Text");
			this.btnSaveAsPicture.ToolTipText = resources.GetString("btnSaveAsPicture.ToolTipText");
			this.btnSaveAsPicture.Visible = ((bool)(resources.GetObject("btnSaveAsPicture.Visible")));
			// 
			// btnS1
			// 
			this.btnS1.Enabled = ((bool)(resources.GetObject("btnS1.Enabled")));
			this.btnS1.ImageIndex = ((int)(resources.GetObject("btnS1.ImageIndex")));
			this.btnS1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.btnS1.Text = resources.GetString("btnS1.Text");
			this.btnS1.ToolTipText = resources.GetString("btnS1.ToolTipText");
			this.btnS1.Visible = ((bool)(resources.GetObject("btnS1.Visible")));
			// 
			// btnPrint
			// 
			this.btnPrint.Enabled = ((bool)(resources.GetObject("btnPrint.Enabled")));
			this.btnPrint.ImageIndex = ((int)(resources.GetObject("btnPrint.ImageIndex")));
			this.btnPrint.Text = resources.GetString("btnPrint.Text");
			this.btnPrint.ToolTipText = resources.GetString("btnPrint.ToolTipText");
			this.btnPrint.Visible = ((bool)(resources.GetObject("btnPrint.Visible")));
			// 
			// btnPrintPreview
			// 
			this.btnPrintPreview.Enabled = ((bool)(resources.GetObject("btnPrintPreview.Enabled")));
			this.btnPrintPreview.ImageIndex = ((int)(resources.GetObject("btnPrintPreview.ImageIndex")));
			this.btnPrintPreview.Text = resources.GetString("btnPrintPreview.Text");
			this.btnPrintPreview.ToolTipText = resources.GetString("btnPrintPreview.ToolTipText");
			this.btnPrintPreview.Visible = ((bool)(resources.GetObject("btnPrintPreview.Visible")));
			// 
			// btnPrintSetup
			// 
			this.btnPrintSetup.Enabled = ((bool)(resources.GetObject("btnPrintSetup.Enabled")));
			this.btnPrintSetup.ImageIndex = ((int)(resources.GetObject("btnPrintSetup.ImageIndex")));
			this.btnPrintSetup.Text = resources.GetString("btnPrintSetup.Text");
			this.btnPrintSetup.ToolTipText = resources.GetString("btnPrintSetup.ToolTipText");
			this.btnPrintSetup.Visible = ((bool)(resources.GetObject("btnPrintSetup.Visible")));
			// 
			// btnS2
			// 
			this.btnS2.Enabled = ((bool)(resources.GetObject("btnS2.Enabled")));
			this.btnS2.ImageIndex = ((int)(resources.GetObject("btnS2.ImageIndex")));
			this.btnS2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.btnS2.Text = resources.GetString("btnS2.Text");
			this.btnS2.ToolTipText = resources.GetString("btnS2.ToolTipText");
			this.btnS2.Visible = ((bool)(resources.GetObject("btnS2.Visible")));
			// 
			// btnOptions
			// 
			this.btnOptions.Enabled = ((bool)(resources.GetObject("btnOptions.Enabled")));
			this.btnOptions.ImageIndex = ((int)(resources.GetObject("btnOptions.ImageIndex")));
			this.btnOptions.Text = resources.GetString("btnOptions.Text");
			this.btnOptions.ToolTipText = resources.GetString("btnOptions.ToolTipText");
			this.btnOptions.Visible = ((bool)(resources.GetObject("btnOptions.Visible")));
			// 
			// btnS3
			// 
			this.btnS3.Enabled = ((bool)(resources.GetObject("btnS3.Enabled")));
			this.btnS3.ImageIndex = ((int)(resources.GetObject("btnS3.ImageIndex")));
			this.btnS3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.btnS3.Text = resources.GetString("btnS3.Text");
			this.btnS3.ToolTipText = resources.GetString("btnS3.ToolTipText");
			this.btnS3.Visible = ((bool)(resources.GetObject("btnS3.Visible")));
			// 
			// btnAbout
			// 
			this.btnAbout.Enabled = ((bool)(resources.GetObject("btnAbout.Enabled")));
			this.btnAbout.ImageIndex = ((int)(resources.GetObject("btnAbout.ImageIndex")));
			this.btnAbout.Text = resources.GetString("btnAbout.Text");
			this.btnAbout.ToolTipText = resources.GetString("btnAbout.ToolTipText");
			this.btnAbout.Visible = ((bool)(resources.GetObject("btnAbout.Visible")));
			// 
			// imgImageListForToolbar
			// 
			this.imgImageListForToolbar.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgImageListForToolbar.ImageSize = ((System.Drawing.Size)(resources.GetObject("imgImageListForToolbar.ImageSize")));
			this.imgImageListForToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgImageListForToolbar.ImageStream")));
			this.imgImageListForToolbar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ntiNotifyIcon
			// 
			this.ntiNotifyIcon.ContextMenu = this.mnuMainForNotifycationIcon;
			this.ntiNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("ntiNotifyIcon.Icon")));
			this.ntiNotifyIcon.Text = resources.GetString("ntiNotifyIcon.Text");
			this.ntiNotifyIcon.Visible = ((bool)(resources.GetObject("ntiNotifyIcon.Visible")));
			this.ntiNotifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ntiNotifyIcon_MouseMove);
			this.ntiNotifyIcon.DoubleClick += new System.EventHandler(this.ntiNotifyIcon_DoubleClick);
			// 
			// mnuMainForNotifycationIcon
			// 
			this.mnuMainForNotifycationIcon.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																									   this.ctxmnuAbout,
																									   this.ctxmnuSeperator,
																									   this.ctxmnuExit});
			this.mnuMainForNotifycationIcon.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mnuMainForNotifycationIcon.RightToLeft")));
			this.mnuMainForNotifycationIcon.Popup += new System.EventHandler(this.mnuMainForNotifycationIcon_Popup);
			// 
			// ctxmnuAbout
			// 
			this.ctxmnuAbout.Enabled = ((bool)(resources.GetObject("ctxmnuAbout.Enabled")));
			this.ctxmnuAbout.Index = 0;
			this.ctxmnuAbout.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("ctxmnuAbout.Shortcut")));
			this.ctxmnuAbout.ShowShortcut = ((bool)(resources.GetObject("ctxmnuAbout.ShowShortcut")));
			this.ctxmnuAbout.Text = resources.GetString("ctxmnuAbout.Text");
			this.ctxmnuAbout.Visible = ((bool)(resources.GetObject("ctxmnuAbout.Visible")));
			this.ctxmnuAbout.Click += new System.EventHandler(this.ctxmnuAbout_Click);
			// 
			// ctxmnuSeperator
			// 
			this.ctxmnuSeperator.Enabled = ((bool)(resources.GetObject("ctxmnuSeperator.Enabled")));
			this.ctxmnuSeperator.Index = 1;
			this.ctxmnuSeperator.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("ctxmnuSeperator.Shortcut")));
			this.ctxmnuSeperator.ShowShortcut = ((bool)(resources.GetObject("ctxmnuSeperator.ShowShortcut")));
			this.ctxmnuSeperator.Text = resources.GetString("ctxmnuSeperator.Text");
			this.ctxmnuSeperator.Visible = ((bool)(resources.GetObject("ctxmnuSeperator.Visible")));
			// 
			// ctxmnuExit
			// 
			this.ctxmnuExit.Enabled = ((bool)(resources.GetObject("ctxmnuExit.Enabled")));
			this.ctxmnuExit.Index = 2;
			this.ctxmnuExit.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("ctxmnuExit.Shortcut")));
			this.ctxmnuExit.ShowShortcut = ((bool)(resources.GetObject("ctxmnuExit.ShowShortcut")));
			this.ctxmnuExit.Text = resources.GetString("ctxmnuExit.Text");
			this.ctxmnuExit.Visible = ((bool)(resources.GetObject("ctxmnuExit.Visible")));
			this.ctxmnuExit.Click += new System.EventHandler(this.mnuFileExitMenu_Click);
			// 
			// stbMainStatusBar
			// 
			this.stbMainStatusBar.AccessibleDescription = ((string)(resources.GetObject("stbMainStatusBar.AccessibleDescription")));
			this.stbMainStatusBar.AccessibleName = ((string)(resources.GetObject("stbMainStatusBar.AccessibleName")));
			this.stbMainStatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("stbMainStatusBar.Anchor")));
			this.stbMainStatusBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stbMainStatusBar.BackgroundImage")));
			this.stbMainStatusBar.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("stbMainStatusBar.Dock")));
			this.stbMainStatusBar.Enabled = ((bool)(resources.GetObject("stbMainStatusBar.Enabled")));
			this.stbMainStatusBar.Font = ((System.Drawing.Font)(resources.GetObject("stbMainStatusBar.Font")));
			this.stbMainStatusBar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("stbMainStatusBar.ImeMode")));
			this.stbMainStatusBar.Location = ((System.Drawing.Point)(resources.GetObject("stbMainStatusBar.Location")));
			this.stbMainStatusBar.Name = "stbMainStatusBar";
			this.stbMainStatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																								this.pnlCommentsText,
																								this.pnlCountOfWindow,
																								this.pnlDateAndTime});
			this.stbMainStatusBar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("stbMainStatusBar.RightToLeft")));
			this.stbMainStatusBar.ShowPanels = true;
			this.stbMainStatusBar.Size = ((System.Drawing.Size)(resources.GetObject("stbMainStatusBar.Size")));
			this.stbMainStatusBar.TabIndex = ((int)(resources.GetObject("stbMainStatusBar.TabIndex")));
			this.stbMainStatusBar.Text = resources.GetString("stbMainStatusBar.Text");
			this.tltMainTooltip.SetToolTip(this.stbMainStatusBar, resources.GetString("stbMainStatusBar.ToolTip"));
			this.stbMainStatusBar.Visible = ((bool)(resources.GetObject("stbMainStatusBar.Visible")));
			// 
			// pnlCommentsText
			// 
			this.pnlCommentsText.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlCommentsText.Alignment")));
			this.pnlCommentsText.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.pnlCommentsText.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlCommentsText.Icon")));
			this.pnlCommentsText.MinWidth = ((int)(resources.GetObject("pnlCommentsText.MinWidth")));
			this.pnlCommentsText.Text = resources.GetString("pnlCommentsText.Text");
			this.pnlCommentsText.ToolTipText = resources.GetString("pnlCommentsText.ToolTipText");
			this.pnlCommentsText.Width = ((int)(resources.GetObject("pnlCommentsText.Width")));
			// 
			// pnlCountOfWindow
			// 
			this.pnlCountOfWindow.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlCountOfWindow.Alignment")));
			this.pnlCountOfWindow.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlCountOfWindow.Icon")));
			this.pnlCountOfWindow.MinWidth = ((int)(resources.GetObject("pnlCountOfWindow.MinWidth")));
			this.pnlCountOfWindow.Text = resources.GetString("pnlCountOfWindow.Text");
			this.pnlCountOfWindow.ToolTipText = resources.GetString("pnlCountOfWindow.ToolTipText");
			this.pnlCountOfWindow.Width = ((int)(resources.GetObject("pnlCountOfWindow.Width")));
			// 
			// pnlDateAndTime
			// 
			this.pnlDateAndTime.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("pnlDateAndTime.Alignment")));
			this.pnlDateAndTime.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.pnlDateAndTime.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlDateAndTime.Icon")));
			this.pnlDateAndTime.MinWidth = ((int)(resources.GetObject("pnlDateAndTime.MinWidth")));
			this.pnlDateAndTime.Text = resources.GetString("pnlDateAndTime.Text");
			this.pnlDateAndTime.ToolTipText = resources.GetString("pnlDateAndTime.ToolTipText");
			this.pnlDateAndTime.Width = ((int)(resources.GetObject("pnlDateAndTime.Width")));
			// 
			// tltMainTooltip
			// 
			this.tltMainTooltip.ShowAlways = true;
			// 
			// frmMainVectorForm
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
																		  this.stbMainStatusBar,
																		  this.tlbMainToolBar});
			this.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("$this.Dock")));
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.IsMdiContainer = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.Menu = this.mnuMainMenu;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "frmMainVectorForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.tltMainTooltip.SetToolTip(this, resources.GetString("$this.ToolTip"));
			this.Visible = ((bool)(resources.GetObject("$this.Visible")));
			this.Closed += new System.EventHandler(this.frmMainVectorForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.pnlCommentsText)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlCountOfWindow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlDateAndTime)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
	}
}


