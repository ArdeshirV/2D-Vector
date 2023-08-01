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
using System.Drawing.Drawing2D;
using ArdeshirV.Components.ScreenVector;
using System.Runtime.Serialization.Formatters.Binary;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Components.ScreenVector
{
	/// <summary>
	/// Utility Struct for Get & Set Options between Options Dialog & Main Form.
	/// </summary>
	[
		Serializable
	]
	public struct Environments_Variables
	{
		#region Variables

		public bool blnCaculatorBell;

		/// <summary>
		/// Is tool tip active?
		/// </summary>
		public bool blnIsActiveToolTip;

		/// <summary>
		/// Color for start position of vectors.
		/// </summary>
		public Color clrHeaderVectorColor;

		/// <summary>
		/// Color for end position fo vectors.
		/// </summary>
		public Color clrEndingVectorColor;

		/// <summary>
		/// Color for moving vectors(not compeleted vectors).
		/// </summary>
		public Color clrDotDotVectorColor;

		/// <summary>
		/// Color for grid lines on screen vector.
		/// </summary>
		public Color clrGridColor;

		/// <summary>
		/// Screen vector background color.
		/// </summary>
		public Color clrBackColor;

		/// <summary>
		/// Active vector color.
		/// </summary>
		public Color clrActiveColor;

		/// <summary>
		/// Color for result vector(like result of add all vectors).
		/// </summary>
		public Color clrResultColor;

		/// <summary>
		/// Grid resolution Hrizontaly & Verticaly.
		/// </summary>
		public Size sizResolution;

		/// <summary>
		/// Client size of main frame.
		/// </summary>
		public Size sizClientSize;

		/// <summary>
		/// Show vector ID at startup.
		/// </summary>
		public bool blnShowID;

		/// <summary>
		/// Show grid at startup.
		/// </summary>
		public bool blnShowGrid;

		/// <summary>
		/// Show status bar at startup.
		/// </summary>
		public bool blnShowStatusBar;

		/// <summary>
		/// Show tool bar at startup.
		/// </summary>
		public bool blnShowToolBar;

		/// <summary>
		/// Show child form in maximized mode.
		/// </summary>
		public bool blnChildMaximized;

		/// <summary>
		/// Ask question before delete all vector on screen vector.
		/// </summary>
		public bool blnQuestionBeforeDeleteAll;

		/// <summary>
		/// Ask question before close open documents(modified documents).
		/// </summary>
		public bool blnQuestionBeforeCloseDocument;

		/// <summary>
		/// Create new vector document at startup.
		/// </summary>
		public bool blnNewDocAtStartUp;

		/// <summary>
		/// Show main frame in maximized at startup.
		/// </summary>
		public bool blnMainFrameMaximized;

		/// <summary>
		/// Show notify icon in windows task bar.
		/// </summary>
		public bool blnShowNotifyIcon;

		/// <summary>
		/// Vector scale.
		/// </summary>
		public double intGeo;
		
		#endregion
		//-------------------------------------------------------------------------------
		#region Public Functions

		/// <summary>
		/// Load Environment variables from disk(return true if succesful).
		/// </summary>
		/// <param name="ProFile">Profile name</param>
		/// <returns>Load from profile or default value?</returns>
		public bool LoadProfile(string ProFile, bool UseDefaultValue)
		{
			if(System.IO.File.Exists(ProFile))
			{
				try
				{
					BinaryFormatter l_formatter = new BinaryFormatter();
					Stream l_stmStream = new FileStream(ProFile,FileMode.Open,
						FileAccess.Read, FileShare.Read);
					this = (Environments_Variables)
						(l_formatter.Deserialize(l_stmStream));
					l_stmStream.Close();

					return true;
				}
				catch(System.IO.IOException exp)
				{
					MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK,
						MessageBoxIcon.Error);

					return false;
				}
			}
			else if(UseDefaultValue)
				LoadDefaultValues();

			return false;
		}

		/// <summary>
		/// Save Current Environment Variables to disk.
		/// </summary>
		/// <param name="ProFile">Profile name</param>
		public void SaveProfile(string ProFile)
		{
			BinaryFormatter l_formatter = new BinaryFormatter();
			Stream l_stmStream = new FileStream(ProFile, FileMode.Create,
				FileAccess.Write, FileShare.None);

			if(l_formatter != null && l_stmStream != null)
			{
				l_formatter.Serialize(l_stmStream,this);
				l_stmStream.Close();
			}
		}

		/// <summary>
		/// Allocating Environment variables with default values.
		/// </summary>
		public void LoadDefaultValues()
		{
			intGeo = 1;
			blnShowID = true;
			blnShowGrid = true;
			blnShowToolBar = true;
			blnShowStatusBar = true;
			blnCaculatorBell = true;
			blnShowNotifyIcon = true;
			blnChildMaximized = false;
			blnNewDocAtStartUp = true;
			blnIsActiveToolTip = true;
			clrActiveColor = Color.Red;
			blnMainFrameMaximized = true;
			clrGridColor = Color.LightGray;
			clrResultColor = Color.Magenta;
			sizResolution = new Size(10, 10);
			clrDotDotVectorColor = Color.Blue;
			blnQuestionBeforeDeleteAll = true;
			sizClientSize = new Size(1000, 700);
			clrHeaderVectorColor = Color.Yellow;
			blnQuestionBeforeCloseDocument = true;
			clrEndingVectorColor = Color.DarkGreen;
			clrBackColor = Color.FromArgb(200, 255, 255);
		}

		#endregion
	}
}


