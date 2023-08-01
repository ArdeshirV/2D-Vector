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
using System.Xml.Serialization;
using ArdeshirV.Components.ScreenVector;
using System.Runtime.Serialization.Formatters.Binary;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Components.ScreenVector
{
	/// <summary>
	/// Vectors kepper.
	/// </summary>
	[
		XmlRoot("vectors")
	]
	public class vectors
	{
		#region Variables

		[
			System.Xml.Serialization.XmlAttribute("vector version")
		]
		private const float m_intVersion = 3.0F;

		private ArrayList m_vctArrayList;

		#endregion
		//-------------------------------------------------------------------------------
		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public vectors()
		{
			m_vctArrayList = new ArrayList();
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Utility functions

		/// <summary>
		/// Add new vector.
		/// </summary>
		/// <param name="vctNew">New vector</param>
		public void Add(vector vctNew)
		{
			m_vctArrayList.Add(vctNew);
		}

		/// <summary>
		/// Add new item.
		/// </summary>
		/// <param name="vct">New vector</param>
		public void AddItem(vector vct)
		{
			m_vctArrayList.Add(vct);
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Gets or sets items.
		/// </summary>
		[
			XmlElement("vector")
		]
		public vector[] Items
		{
			get
			{
				vector[] l_vctVectors = new vector[m_vctArrayList.Count];

				m_vctArrayList.CopyTo(l_vctVectors);

				return l_vctVectors;
			}
			set
			{
				if(value != null)
				{
					m_vctArrayList.Clear();

					foreach(vector vct in value)
						m_vctArrayList.Add(vct);
				}
			}
		}

		#endregion
	}

	/// <summary>
	/// This class created for save and load files.
	/// </summary>
	public class vector_saver
	{
		#region Public Functions

		/// <summary>
		/// Save arr to file with [filename] name.
		/// </summary>
		/// <param name="filename">File name</param>
		/// <param name="arr">Screen vector</param>
		public void SaveToFile(string filename, vector[] arr)
		{
			TextWriter l_txwWriter = new StreamWriter(filename);

			try
			{
				vectors l_vct = new vectors();

				l_vct.Items = arr;
				XmlSerializer l_xmlSerializer = new XmlSerializer(typeof(vectors));
				l_xmlSerializer.Serialize(l_txwWriter, l_vct);
			}
			finally
			{
				l_txwWriter.Close();
			}
		}

		/// <summary>
		/// Load arr from file with [filename] name.
		/// </summary>
		/// <param name="filename">File name</param>
		/// <param name="arr">vectors</param>
		public void LoadFromFile(string filename, out vector[] arr)
		{
			arr = null;
			TextReader l_txtReader = new StreamReader(filename);

			try
			{
				XmlSerializer l_xmlSerializer = new XmlSerializer(typeof(vectors));

				vectors l_vct = (vectors)l_xmlSerializer.Deserialize(l_txtReader);
				arr = l_vct.Items;
			}
			catch(System.Exception exp)
			{
				MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			finally
			{
				l_txtReader.Close();
			}
		}

		#endregion
	}
}


