#region Header
// https://github.com/ArdeshirV/2D-Vector
// Copyright 2002-2004 ArdeshirV, Licensed Under MIT


using System;
using System.IO;
using System.Xml;
//using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;
using ArdeshirV.Components.ScreenVector;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Components.ScreenVector
{
	/// <summary>
	/// Vector Screen Class:
	/// </summary>
	public class ScreenVector : System.Windows.Forms.UserControl
	{
		#region Variables

		private double m_dblZoom;
		private bool m_blnModified;
		private bool m_blnIsMoving;
		private bool m_blnShowName;
		private bool m_blnShowGrid;
		private Size m_sizResolution;
		private Size m_sizClientSize;
		private int m_intActiveVector;
		private Color m_clrResultColor;
		private VectorScreenMode m_vsmMode;
		private Color m_clrHeaderVectorColor;
		private Color m_clrEndingVectorColor;
		private PointD m_pntLastMousePosition;
		private PointD m_pntLeftMouseButtonDown;
		private Plused m_dlg_Plus = null;
		private System.EventHandler m_dlgLoadFromFile = null;
		private ReActive m_Delegate_Activate = null;
		private ReActive m_Delegate_Modified = null;
		private ReActive m_Delegate_ZoomChanged = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

/*ArrayList class used here for more information about ArrayList click here:
ms-help://MS.VSCC/MS.MSDNVS/cpref/html/frlrfSystemCollectionsArrayListClassTopic.htm*/
		/// <summary>
		/// Array of all vectors.
		/// </summary>
		private ArrayList m_arlVectors;

		/// <summary>
		/// Independent bitmap.
		/// </summary>
		private Bitmap m_bmpSpace;

		//below Allocated All Variable drawing:
		/// <summary>
		/// for draw none vector
		/// </summary>
		private Pen m_penMoving;

		/// <summary>
		/// Pen for draw grids
		/// </summary>
		private Pen	m_penGridPen;

		/// <summary>
		/// Pen for draw selected vector.
		/// </summary>
		private Pen m_penActive;

		private Brush m_brsBackground;
		/// <summary>
		/// Font for vector IDs.
		/// </summary>
		private static Font m_fntDefault;

		/// <summary>
		/// Set background brush.
		/// </summary>
		private Brush m_brsFontColor;

		#endregion
		//-------------------------------------------------------------------------------
		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public ScreenVector()
		{
			InitializeComponent();
			init();
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Events

		/// <summary>
		/// Occurs whenever all vectors has been added.
		/// </summary>
		public event Plused AddAllVectors
		{
			add
			{
				m_dlg_Plus += value;
			}
			remove
			{
				m_dlg_Plus -= value;
			}
		}

		/// <summary>
		/// Occurs whenever document load from file.
		/// </summary>
		public event EventHandler OnLoadFromFile
		{
			add
			{
				m_dlgLoadFromFile += value;
			}
			remove
			{
				m_dlgLoadFromFile -= value;
			}
		}

		/// <summary>
		/// Occurs whenever Active Vector has been changed.
		/// </summary>
		public event ReActive SelectVector
		{
			add
			{
				m_Delegate_Activate += value;
			}
			remove
			{
				m_Delegate_Activate -= value;
			}
		}

		/// <summary>
		/// Occurs whenever document has been changed.
		/// </summary>
		public event ReActive DocumentModified
		{
			add
			{
				m_Delegate_Modified += value;
			}
			remove
			{
				m_Delegate_Modified -= value;
			}
		}

		/// <summary>
		/// Occurs whenever zoom has been changed.
		/// </summary>
		public event ReActive ZoomChanged
		{
			add
			{
				m_Delegate_ZoomChanged += value;
			}
			remove
			{
				m_Delegate_ZoomChanged -= value;
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Indexer

		/// <summary>
		/// Indexer for Get or set vectors.
		/// </summary>
		public vector this[int Index]
		{
			get
			{
				return (vector)m_arlVectors[Index];
			}
			set 
			{
				m_arlVectors[Index] = value;
				Invalidate();
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Gets true if there is vector or vectors.
		/// </summary>
		[
			Browsable(false)
		]
		public bool IsThereAnyVector
		{
			get
			{
				return m_arlVectors.Count > 0;
			}
		}

		/// <summary>
		/// Gets or sets Color for Moving vector(none vector).
		/// </summary>
		[
			Category("Appearance"),
			Description("Color for Moving vector.")
		]
		public Color MovingColor
		{
			get
			{
				return m_penMoving.Color;
			}
			set
			{
				m_penMoving = new Pen(value);
				m_penMoving.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets Back Color.
		/// </summary>
		[
			Category("Appearance"),
			Description("Back color.")
		]
		public Color BackgroundColor
		{
			get
			{
				return this.BackColor;
			}
			set
			{
				this.BackColor = value;
				m_brsBackground = new SolidBrush(value);
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets screen vector mode.
		/// </summary>
		[
			Category("Appearance"),
			Description("screen vector mode.")
		]
		public VectorScreenMode Mode
		{
			get
			{
				return m_vsmMode;
			}
			set
			{
				m_vsmMode = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets active vector color.
		/// </summary>
		[
			Category("Appearance"),
			Description("active vector color.")
		]
		public Color ActiveColor
		{
			get
			{
				return m_penActive.Color;
			}
			set
			{
				m_penActive = new Pen(value);
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets grid color.
		/// </summary>
		[
			Category("Appearance"),
			Description("Grid color.")
		]
		public Color GridColor
		{
			get
			{
				return m_penGridPen.Color;
			}
			set
			{
				m_penGridPen = new Pen(value);;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets color for frist point of vector.
		/// </summary>
		[
			Category("Appearance"),
			Description("Vector head color.")
		]
		public Color HeaderVectorColor
		{
			get
			{
				return m_clrHeaderVectorColor;
			}
			set
			{
				m_clrHeaderVectorColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets color for end point of vector.
		/// </summary>
		[
			Category("Appearance"),
			Description("Vector end color.")
		]
		public Color EndingVectorColor
		{
			get
			{
				return m_clrEndingVectorColor;
			}
			set
			{
				m_clrEndingVectorColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets Color for result vector.
		/// </summary>
		[
			Category("Appearance"),
			Description("Color of result vector.")
		]
		public Color ResultColor
		{
			set
			{
				m_clrResultColor = (value);
				Invalidate();
			}
			get
			{
				return m_clrResultColor;
			}
		}

		/// <summary>
		/// Gets or sets Width and Heigth of grids.
		/// </summary>
		[
			Category("Special"),
			Description("value of horizontal and vertical for grid.")
		]
		public Size Resolution
		{
			get
			{
				return m_sizResolution;
			}
			set
			{
				m_sizResolution = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets show ID ability.
		/// </summary>
		[
			Category("Special"),
			Description("Show ID ability")
		]
		public bool ShowName
		{
			get
			{
				return m_blnShowName;
			}
			set
			{
				m_blnShowName = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets show grid ability.
		/// </summary>
		[
			Category("Special"),
			Description("Show grid ability.")
		]
		public bool ShowGrid
		{
			get
			{
				return m_blnShowGrid;
			}
			set
			{
				m_blnShowGrid = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets all options for screen vector.
		/// </summary>
		[
			Browsable(false)//Don't show this property in properties list.
		]
		public Environments_Variables Options
		{
			set
			{
				m_clrHeaderVectorColor = value.clrHeaderVectorColor;
				m_clrEndingVectorColor = value.clrEndingVectorColor;
				m_penMoving.Color = value.clrDotDotVectorColor;
				m_penGridPen.Color = value.clrGridColor;
				m_penActive.Color = value.clrActiveColor;
				BackColor = value.clrBackColor;
				m_clrResultColor = value.clrResultColor;
				m_brsBackground = new SolidBrush(this.BackColor);
				m_sizResolution = value.sizResolution;
				m_blnShowName = value.blnShowID;
				m_blnShowGrid = value.blnShowGrid;
				Global.s_intGeo = value.intGeo;
				m_penMoving.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
				/*	no useful for this class:
					= value.blnShowNotifyIcon;//Default Value
					= value.blnShowStatusBar;//...
					= value.blnShowToolBar;//...
					= value.blnQuestionBeforeCloseDocument;//...
					= value.blnQuestionBeforeDeleteAll;//...
					= value.blnNewDocAtStartUp;//...*/
				Invalidate();
			}
			get
			{
				Environments_Variables env;
				env.clrHeaderVectorColor = m_clrHeaderVectorColor;
				env.clrEndingVectorColor = m_clrEndingVectorColor;
				env.clrDotDotVectorColor = m_penMoving.Color;
				env.clrGridColor = m_penGridPen.Color;
				env.clrActiveColor = m_penActive.Color;
				env.clrBackColor = BackColor;
				env.clrResultColor = m_clrResultColor;
				env.clrBackColor = BackColor;
				env.sizResolution = m_sizResolution;
				env.blnShowID = m_blnShowName;
				env.blnShowGrid = m_blnShowGrid;
				env.intGeo = Global.s_intGeo;
				
				// no useful for this class:
				env.blnShowNotifyIcon = false;
				env.blnShowStatusBar= false;
				env.blnShowToolBar = false;
				env.blnQuestionBeforeCloseDocument = false;
				env.blnQuestionBeforeDeleteAll = false;
				env.blnNewDocAtStartUp = false;
				env.sizClientSize = Size.Empty;
				env.blnMainFrameMaximized = false;
				env.blnChildMaximized = false;
				env.blnIsActiveToolTip = false;
				env.blnCaculatorBell = false;

				return env;
			}
		}

		/// <summary>
		/// Gets current screen image.
		/// </summary>
		[
			Browsable(false)
		]
		public Image ScreeenImage
		{
			get
			{
				return Image.FromHbitmap(m_bmpSpace.GetHbitmap());
			}
		}
		
		/// <summary>
		/// Gets Count of vectors.
		/// </summary>
		[
			Browsable(false)
		]
		public int Count
		{
			get
			{
				return this.m_arlVectors.Count;
			}
		}
		
		/// <summary>
		/// Gets or sets arr of vectors.
		/// </summary>
		[
			Browsable(false)
		]
		public vector[] Items
		{
			set
			{
				if(value == null) 
					return;

				m_arlVectors.Clear();
				vector[] l_vct = (vector[])value;

				if(l_vct != null)
					m_arlVectors.AddRange(l_vct);
			}
			get
			{
				vector[] l_vct = new vector[m_arlVectors.Count];
				m_arlVectors.CopyTo(l_vct);

				return l_vct;
			}
		}

		/// <summary>
		/// Gets document state.
		/// </summary>
		[
			Browsable(false)
		]
		public bool Modified
		{
			get
			{
				return m_blnModified;
			}
		}
		
		/// <summary>
		/// Gets or sets number of active vector.
		/// </summary>
		[
			Browsable(false)
		]
		public int ActiveNumber
		{
			get
			{
				return m_intActiveVector;
			}
			set
			{
				m_intActiveVector = value;

				if(m_Delegate_Activate != null)
					m_Delegate_Activate();

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets Zoom for screen.
		/// </summary>
		[
			Browsable(false)
		]
		public double Zoom
		{
			get
			{
				return m_dblZoom;
			}
			set
			{
				m_dblZoom = value;

				if(m_Delegate_ZoomChanged != null)
					m_Delegate_ZoomChanged();

				Invalidate();
			}
		}

		#endregion 
		//-------------------------------------------------------------------------------
		#region Event Handlers

		/// <summary>
		/// Occured whenever Scren has been dirty.
		/// </summary>
		/// <param name="sender">Screen vector</param>
		/// <param name="e">Event argument</param>
		private void OnPaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics l_grpIndependent;
			//	For More Information About Bitmaps Click Here:
			//	ms-help://MS.VSCC/MS.MSDNVS/gdicpp/gdip_cpp_classes_01b_8lkj.htm
			m_bmpSpace = new Bitmap(ClientRectangle.Width,
				ClientRectangle.Height, e.Graphics);
			l_grpIndependent = Graphics.FromImage(m_bmpSpace);

			try
			{
				DrawScreen(l_grpIndependent);
				//For More Information About DrawImageUnscaled Click Here:
//ms-help://MS.VSCC/MS.MSDNVS/cpref/html/frlrfSystemDrawingGraphicsClassDrawImageUnscaledTopic.htm
				e.Graphics.DrawImageUnscaled(m_bmpSpace, 0, 0);
			}
			finally
			{
				l_grpIndependent.Dispose();
			}
		}

		/// <summary>
		/// If Change Size of Screen Vector or form that have screen vector-
		/// Doke Property for Screen Vector is true below method will be call.
		/// </summary>
		/// <param name="sender">Screen vector</param>
		/// <param name="e">Event argument</param>
		private void OnResize(object sender, System.EventArgs e)
		{
			m_sizClientSize = Size;
			Invalidate();
		}

		/// <summary>
		/// Occured whenever mouse has been moved.
		/// </summary>
		/// <param name="sender">Screen vector</param>
		/// <param name="e">Event argument</param>
		private void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			m_pntLastMousePosition = new PointD(e.X, e.Y);

			if(!m_blnIsMoving)
				m_pntLeftMouseButtonDown = m_pntLastMousePosition;
	
			if(e.Button == MouseButtons.Left)
			{	
				m_blnIsMoving = ClientRectangle.Contains(m_pntLastMousePosition.GetPoint());
				Invalidate();
			}
		}

		/// <summary>
		/// Occured whenever mouse buttn has been down.
		/// </summary>
		/// <param name="sender">Screen vector</param>
		/// <param name="e">Event argument</param>
		private void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				m_dblZoom = 1.0;

				if(m_Delegate_ZoomChanged != null)
					m_Delegate_ZoomChanged();

				m_pntLeftMouseButtonDown = new PointD(e.X, e.Y);
				m_blnIsMoving = true;
				Invalidate();
			}
		}

		/// <summary>
		/// Occured whenever mouse button has been up.
		/// </summary>
		/// <param name="sender">Screen vector</param>
		/// <param name="e">Event argument</param>
		private void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( e.Button == System.Windows.Forms.MouseButtons.Left &&
				(e.X != m_pntLeftMouseButtonDown.X ||
				e.Y != m_pntLeftMouseButtonDown.Y )&&
				ClientRectangle.Contains(e.X,e.Y))
			{
				m_pntLastMousePosition = new PointD(e.X, e.Y);
				AddNewVector(vector.Convert(m_pntLeftMouseButtonDown,
					Width, Height),
					vector.Convert(m_pntLastMousePosition, Width, Height));
				m_intActiveVector = m_arlVectors.Count - 1;
				SetModifiedFlag();
				Invalidate();
			}
			m_blnIsMoving = false;
		}

		/// <summary>
		/// Occured whenever mouse has been leaved.
		/// </summary>
		/// <param name="sender">Screen vector</param>
		/// <param name="e">Event argument</param>
		private void ScreenVector_MouseLeave(object sender, System.EventArgs e)
		{
			m_blnIsMoving = false;
		}

		/// <summary>
		/// Occured whenever Screen Vector has been Load.
		/// </summary>
		/// <param name="sender">Screen vector</param>
		/// <param name="e">Event argument</param>
		private void ScreenVector_Load(object sender, System.EventArgs e)
		{
			if(m_Delegate_ZoomChanged != null)
				m_Delegate_ZoomChanged();
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Public Fuctions

		/// <summary>
		/// Save current Screen image to bitmap file.
		/// </summary>
		/// <param name="filename">Image file name</param>
		public void SaveScreenToFile(string filename)
		{
			m_bmpSpace.Save(filename, ImageFormat.Jpeg);
		}

		/// <summary>
		/// Gets array of vector IDs.
		/// </summary>
		/// <returns>Identifiers</returns>
		public int[] GetArrOfIDs()
		{
			int[] l_aintArr = new int[m_arlVectors.Count];

			for(int i = 0; i < m_arlVectors.Count; i++)
				l_aintArr[i] = ((vector)m_arlVectors[i]).ID + 1;

			return l_aintArr;
		}

		/// <summary>
		/// Load vector document from disk.
		/// </summary>
		/// <param name="str">File name</param>
		public void LoadFromFile(string str)
		{
			vector[] l_arrvct;
			vector_saver svr = new vector_saver();
			svr.LoadFromFile(str, out l_arrvct);
			MakeNegativeY(ref l_arrvct, false);
			m_arlVectors.Clear();

			if(l_arrvct != null)
				m_arlVectors.AddRange(l_arrvct);

			if(m_dlgLoadFromFile != null)
				m_dlgLoadFromFile(null, null);
		}

		/// <summary>
		/// Save current document (vectors) to file.
		/// </summary>
		/// <param name="FileName">File name</param>
		public void SaveToFile(string FileName)
		{
			vector[] l_vctArr = new vector[m_arlVectors.Count];
			m_arlVectors.CopyTo(l_vctArr);
			vector_saver l_svr = new vector_saver();
			MakeNegativeY(ref l_vctArr, true);
			l_svr.SaveToFile(FileName, l_vctArr);
			m_blnModified = false;

#if DEBUG
			MessageBox.Show("(SaveToFile) from Screen Vector");
#endif
		}

		/// <summary>
		/// Gets current image on screen.
		/// </summary>
		/// <returns>Current image</returns>
		public Bitmap GetScreenImage()
		{
			Invalidate();

			return m_bmpSpace;
		}

		/// <summary>
		/// Delete vector at Index.
		/// </summary>
		/// <param name="Index">Vector index for delete</param>
		public void DeleteAt(int Index)
		{
			m_arlVectors.RemoveAt(Index);
			SetModifiedFlag();
			Invalidate();
		}

		/// <summary>
		/// Add new vector to Vector Array.
		/// </summary>
		/// <param name="start">Start point</param>
		/// <param name="end">End point</param>
		public void AddNewVector(PointD start, PointD end)
		{
			if(start.X == end.X && start.Y == end.Y)
#if DEBUG
			{
				throw new Exception("Hints: in ScreenVector.AddNewVector(...) method" +
					" try to add none vector to vector list.");
#else
				return;
#endif
#if DEBUG
			}
#endif
			m_arlVectors.Add(new 
				vector(
				(m_arlVectors.Count > 0)?
				((vector)m_arlVectors[m_arlVectors.Count - 1]).ID + 1: 0,
				start, end));
			SetModifiedFlag();
			Invalidate();
		}

		/// <summary>
		/// Add new vector to vector array.
		/// </summary>
		/// <param name="vct">new vector</param>
		/// <param name="IsSpecialVector">Is Special Vector?</param>
		public void AddNewVector(vector vct, bool bln)
		{
			vector v = new vector(
				(bln)?(vct.ID):(m_arlVectors.Count > 0)?
				((vector)m_arlVectors[m_arlVectors.Count - 1]).ID + 1:0, vct);
			m_arlVectors.Add(v);
			SetModifiedFlag();
			Invalidate();
		}

		/// <summary>
		/// Add new vector to vector array.
		/// </summary>
		/// <param name="vct">New vector</param>
		public void AddNewVector(vector vct)
		{
			vector v = new vector(
				(m_arlVectors.Count > 0)?
				((vector)m_arlVectors[m_arlVectors.Count - 1]).ID + 1:0, vct);
			m_arlVectors.Add(v);
			SetModifiedFlag();
			Invalidate();
		}

		/// <summary>
		/// Delete all vectors on screen & repaint.
		/// </summary>
		/// <param name="Question">Show message box?</param>
		/// <returns>Delete all or no?</returns>
		public bool ClearAllVectors(bool Question)
		{
			if(m_arlVectors.Count == 0)
				return false;

			if((!Question) || (MessageBox.Show
				("Are you sure you want to Delete all Vectors",	"Warning",
				MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation)==DialogResult.Yes))
			{
				m_arlVectors.Capacity = m_arlVectors.Count + 10;
				m_arlVectors.Clear();
				SetModifiedFlag();
				Invalidate();
				GC.Collect(0);

				return true;
			}
			else
			{
				if(m_Delegate_Activate != null)
					m_Delegate_Activate();

				return false;
			}
			
		}

		/// <summary>
		/// Delete last vector that created.
		/// </summary>
		public void DeleteLast()
		{
			if(m_arlVectors.Count > 0)
			{
				m_arlVectors.RemoveRange(m_arlVectors.Count - 1, 1);
				SetModifiedFlag();
				Invalidate();
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Utility Functions

		/// <summary>
		/// Change y sign of vectors.
		/// </summary>
		/// <param name="vcts">vectors</param>
		private void MakeNegativeY(ref vector[] vcts, bool IsSaving)
		{
			for(int l_intIndexer = 0; l_intIndexer < vcts.Length; l_intIndexer++)
				vcts[l_intIndexer] = new vector(vcts[l_intIndexer].ID + ((IsSaving)? 1: -1),
					new PointD(vcts[l_intIndexer].StartPoint.X, -vcts[l_intIndexer].StartPoint.Y),
					new PointD(vcts[l_intIndexer].EndPoint.X, -vcts[l_intIndexer].EndPoint.Y));
		}

		/// <summary>
		/// Initialization all variables
		/// </summary>
		private void init()
		{
			m_dblZoom = 1;
			m_blnShowName = true;
			m_blnModified = false;
			m_blnIsMoving = false;
			m_sizClientSize = Size;
			m_intActiveVector = -1;
			BackColor = Color.White;
			m_arlVectors = new ArrayList();
			m_clrResultColor = (Color.Gold);
			m_penActive = new Pen(Color.Blue);
			m_penMoving = new Pen(Color.Red,1);
			m_penGridPen = new Pen(Color.Blue);
			m_sizResolution = new Size(100,100);
			m_vsmMode = VectorScreenMode.Normal;
			m_clrEndingVectorColor = (Color.Gold);
			m_clrHeaderVectorColor = (Color.Black);
			m_fntDefault = new Font("MS Sans Serif", 8);
			m_brsFontColor = new SolidBrush(Color.Black);
			m_brsBackground = new SolidBrush(Color.White);
			m_penMoving.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
		}

		/// <summary>
		/// Main Drawing Method, All Driwing code is calling from here.
		/// </summary>
		/// <param name="g">Independent graphics object</param>
		private void DrawScreen(Graphics g)
		{
			int l_intIsActivate;

			// For More Information About Matrix Class in GDI+ Click here:
			// ms-help://MS.VSCC/MS.MSDNVS/gdicpp/gdip_cpp_classes_02f_13n7.htm
			Matrix l_mtxTranslator = new Matrix();

			// For More Information About SmoothingMode in GDI+ Click here:
			// ms-help://MS.VSCC/MS.MSDNVS/gdicpp/cpp_aboutgdip02_7ewk.htm
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			try
			{
				g.FillRectangle(m_brsBackground, ClientRectangle);

				if(m_blnShowGrid)
				{
					int l_intIndexer,
						l_intTemplateValue,
						l_intTemplateIndexer,
						l_intMidleWidth = Width / 2,
						l_intMidleHeight = Height / 2;

					for(l_intIndexer = 1; (l_intTemplateIndexer =
						l_intIndexer * m_sizResolution.Width) < Width; l_intIndexer++)
					{
						l_intTemplateValue = l_intTemplateIndexer + l_intMidleWidth;
						g.DrawLine(m_penGridPen, l_intTemplateValue, 0,
							l_intTemplateValue, Height);
						l_intTemplateValue = l_intMidleWidth - l_intTemplateIndexer;
						g.DrawLine(m_penGridPen, l_intTemplateValue, 0,
							l_intTemplateValue, Height);
					}

					for(l_intIndexer = 1; (l_intTemplateIndexer =
						l_intIndexer * m_sizResolution.Height) < Height; l_intIndexer++)
					{
						l_intTemplateValue = l_intTemplateIndexer + l_intMidleHeight;
						g.DrawLine(m_penGridPen, 0, l_intTemplateValue,
							Width, l_intTemplateValue);
						l_intTemplateValue = l_intMidleHeight - l_intTemplateIndexer;
						g.DrawLine(m_penGridPen, 0, l_intTemplateValue,
							Width, l_intTemplateValue);
					}

					g.DrawLine(Pens.Black, l_intMidleWidth, 0, l_intMidleWidth, Height);
					g.DrawLine(Pens.Black, 0, l_intMidleHeight, Width, l_intMidleHeight);
				}

				if(m_blnIsMoving)
					g.DrawLine(m_penMoving, m_pntLeftMouseButtonDown.GetPoint(),
						m_pntLastMousePosition.GetPoint());

				if(m_arlVectors.Count <= 0)
					return;

				l_mtxTranslator.Translate((Width + 1)/ 2, (Height + 1)/ 2);

				if(m_dblZoom != 1)
				{
					float l_fltZoom = (float)m_dblZoom;
					l_mtxTranslator.Scale(l_fltZoom, l_fltZoom);
				}

				g.MultiplyTransform(l_mtxTranslator);

				switch(m_vsmMode)
				{
					case VectorScreenMode.Normal:
						l_intIsActivate = 0;
						foreach(vector vct in m_arlVectors)
							Draw_Vector(g, l_intIsActivate++ ==
								m_intActiveVector, vct);
						break;

					case VectorScreenMode.Center:
						l_intIsActivate = 0;
						foreach(vector vct in m_arlVectors)
							Draw_Vector(g, l_intIsActivate++ ==
								m_intActiveVector, new vector(vct.ID,
								new PointD(0,0), new PointD(vct.I, vct.J)));
						break;

					case VectorScreenMode.Plus:
						l_intIsActivate = 0;
						vector l_vctFirst = (vector)m_arlVectors[0],
							l_vctCenter = new vector(new PointD(0), new PointD(0)),
							l_vctResult = l_vctCenter;
						foreach(vector vct in m_arlVectors)
						{
							l_vctResult.StartPoint = l_vctResult.EndPoint;
							l_vctResult.EndPoint = new PointD(l_vctResult.EndPoint.X +
								vct.I, l_vctResult.EndPoint.Y + vct.J);
							l_vctResult.ID = vct.ID;
							Draw_Vector(g, l_intIsActivate++ == 
								m_intActiveVector, l_vctResult);
						}
						vector l_vctAdd = new vector(new PointD(0),
							l_vctResult.EndPoint);
						if(l_vctResult.EndPoint.X != 0 && l_vctResult.EndPoint.Y != 0)
							Draw_Vector(g, "+", m_clrResultColor, l_vctAdd);
						if(m_dlg_Plus != null)
							m_dlg_Plus(l_vctAdd);
						break;
				}
			}
			catch(Exception
#if DEBUG
							e
#endif
				)
			{
#if DEBUG
				MessageBox.Show(this, e.Message, "Error", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
#endif
			}
			finally
			{
				l_mtxTranslator.Dispose();
			}
		}

		/// <summary>
		/// Draw vector on screen.
		/// </summary>
		/// <param name="g">Independent graphics object</param>
		/// <param name="IsActive">is active vector?</param>
		/// <param name="vct">Vector for drawing</param>
		private void Draw_Vector(Graphics g, bool IsActive, vector vct)
		{
			// For More Information About LinearGradientBrush Click Here:
			// ms-help://MS.VSCC/MS.MSDNVS/gdicpp/gdip_cpp_classes_02e_0tv7.htm
			LinearGradientBrush l_lbr = new LinearGradientBrush(vct.StartPoint.GetPointF(),
				vct.EndPoint.GetPointF(), m_clrHeaderVectorColor, m_clrEndingVectorColor);
			Pen l_pen = new Pen(l_lbr);
			float	l_intX = (vct.StartPoint.GetPointF().X + vct.EndPoint.GetPointF().X)/ 2,
					l_intY = (vct.StartPoint.GetPointF().Y + vct.EndPoint.GetPointF().Y)/ 2;

			if(IsActive)
				g.DrawLine(m_penActive, vct.StartPoint.GetPointF().X,
					vct.StartPoint.GetPointF().Y, l_intX, l_intY);
			else
				g.DrawLine(l_pen, vct.StartPoint.GetPointF().X,
					vct.StartPoint.GetPointF().Y, l_intX, l_intY);

			g.DrawLine(l_pen,l_intX, l_intY,
				vct.EndPoint.GetPointF().X,	vct.EndPoint.GetPointF().Y);

			if(m_blnShowName)
				g.DrawString(vct.Name, m_fntDefault,
					m_brsFontColor, l_intX - 5, l_intY - 5);

			l_pen.Dispose();
			l_lbr.Dispose();
		}

		/// <summary>
		/// Drawing Special vector.
		/// </summary>
		/// <param name="g">Independent graphics object</param>
		/// <param name="VectorName">Vector ID</param>
		/// <param name="pen">Vector color and width</param>
		/// <param name="vct">Vector for drawing</param>
		private void Draw_Vector(Graphics g, string VectorName, Color clr, vector vct)
		{
			// For More Information About LinearGradientBrush Click Here:S
			// ms-help://MS.VSCC/MS.MSDNVS/gdicpp/gdip_cpp_classes_02e_0tv7.htm
			LinearGradientBrush l_lbr = new LinearGradientBrush(vct.StartPoint.GetPointF(),
				vct.EndPoint.GetPointF(), clr, m_clrEndingVectorColor);
			Pen l_pen = new Pen(l_lbr);
			float	l_intX = (vct.StartPoint.GetPointF().X + vct.EndPoint.GetPointF().X)/ 2,
					l_intY = (vct.StartPoint.GetPointF().Y + vct.EndPoint.GetPointF().Y)/ 2;

			g.DrawLine(l_pen, vct.StartPoint.GetPointF().X,
				vct.StartPoint.GetPointF().Y, l_intX, l_intY);
			g.DrawLine(l_pen, l_intX, l_intY,
				vct.EndPoint.GetPointF().X, vct.EndPoint.GetPointF().Y);

			if(m_blnShowName)
				g.DrawString(VectorName, m_fntDefault,
					m_brsFontColor, l_intX - 5, l_intY - 5);

			l_pen.Dispose();
			l_lbr.Dispose();
		}

		/// <summary>
		/// Make current document dirty.
		/// </summary>
		public void SetModifiedFlag()
		{
			m_blnModified = true;

			if(m_Delegate_Modified != null)
				m_Delegate_Modified();
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Overrided Functions

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
				if(components != null)
					components.Dispose();

			base.Dispose(disposing);
		}

		/// <summary>
		/// Override OnPaintBackground method.
		/// </summary>
		/// <param name="e">Event argument</param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Component Designer generated code

		private void InitializeComponent()
		{
			this.Cursor = System.Windows.Forms.Cursors.Cross;
			this.Name = "ScreenVector";
			this.Size = new System.Drawing.Size(392, 236);
			this.Resize += new System.EventHandler(this.OnResize);
			this.Load += new System.EventHandler(this.ScreenVector_Load);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);

		}

		#endregion
	}
}


