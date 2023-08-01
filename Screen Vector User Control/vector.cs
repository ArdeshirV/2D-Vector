#region Header
// https://github.com/ArdeshirV/2D-Vector
// Copyright 2002-2004 ArdeshirV, Licensed Under MIT


using System;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Components.ScreenVector
{
	/// <summary>
	/// Global variables.
	/// </summary>
	public struct Global
	{
		/// <summary>
		/// Vector scale.
		/// </summary>
		public static double s_intGeo = 1;
	}


	/// <summary>
	/// Point with double quality.
	/// </summary>
	public struct PointD
	{
		#region Variables

		private double m_dblX;
		private double m_dblY;

		#endregion
		//-------------------------------------------------------------------------------
		#region Constructor

		/// <summary>
		/// Constructor make X and Y equal to value.
		/// </summary>
		/// <param name="Value"></param>
		public PointD(double Value)
		{
			m_dblX = m_dblY = Value;
		}

		/// <summary>
		/// Constructor convert point to pointd.
		/// </summary>
		/// <param name="point">Point</param>
		public PointD(Point point)
		{
			m_dblX = point.X;
			m_dblY = point.Y;
		}

		/// <summary>
		/// Constructor convert pointf to pointd.
		/// </summary>
		/// <param name="point">PointF</param>
		public PointD(PointF point)
		{
			m_dblX = point.X;
			m_dblY = point.Y;
		}

		/// <summary>
		/// Constructor with X and Y coordinate.
		/// </summary>
		/// <param name="X">X coordinate</param>
		/// <param name="Y">y coordinate</param>
		public PointD(double X, double Y)
		{
			m_dblX = X;
			m_dblY = Y;
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Gets or sets X coordinate.
		/// </summary>
		public double X
		{
			get
			{
				return m_dblX;
			}
			set
			{
				m_dblX = value;
			}
		}

		/// <summary>
		/// Gets or sets Y coordinate.
		/// </summary>
		public double Y
		{
			get
			{
				return m_dblY;
			}
			set
			{
				m_dblY = value;
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Public functions

		/// <summary>
		/// Round pointd to pointf.
		/// </summary>
		/// <returns>Rounded pointd</returns>
		public PointF GetPointF()
		{
			return new PointF((float)m_dblX, (float)m_dblY);
		}

		/// <summary>
		/// Round pointd to point.
		/// </summary>
		/// <returns>Rounded pointd</returns>
		public Point GetPoint()
		{
			return new Point((int)m_dblX, (int)m_dblY);
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Overrided functions

		/// <summary>
		/// Gets current point value in string.
		/// </summary>
		/// <returns>Point value</returns>
		public override string ToString()
		{
			return m_dblX.ToString() + ", " + m_dblY.ToString();
		}

		/// <summary>
		/// Test is equal first parameter with second parameter.
		/// </summary>
		/// <param name="obj">First object</param>
		/// <returns>True if current object is equal with obj</returns>
		public override bool Equals(object obj)
		{
			try
			{
				PointD l_pntObj = (PointD)obj;

				return l_pntObj.m_dblX == m_dblX && l_pntObj.m_dblY == m_dblY;
			}
			catch
			{
				return obj.Equals(this);
			}
		}

		/// <summary>
		/// Get has code.
		/// </summary>
		/// <returns>Has code</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Operators

		/// <summary>
		/// True is p1 equal to p2.
		/// </summary>
		/// <param name="p1">PointD</param>
		/// <param name="p2">PointD</param>
		/// <returns>True if p1 is equal to p2.</returns>
		public static bool operator==(PointD p1, PointD p2)
		{
			return p1.m_dblX == p2.m_dblX && p1.m_dblY == p2.m_dblY;
		}
		
		/// <summary>
		/// True is not p1 equal to p2.
		/// </summary>
		/// <param name="p1">PointD</param>
		/// <param name="p2">PointD</param>
		/// <returns>True if p1 is not equal to p2.</returns>
		public static bool operator!=(PointD p1, PointD p2)
		{
			return p1.m_dblX != p2.m_dblX && p1.m_dblY != p2.m_dblY;
		}

		#endregion
	}


	/// <summary>
	/// Serializable Class Vector for encapsolate a Vector.
	/// </summary>
	[
		Serializable
	]
	public struct vector
	{
		#region Variables Definations:
		        
		/// <summary>
		/// ID.
		/// </summary>
		[
			XmlAttribute("vector ID")
		]
		private int m_intID;

		/// <summary>
		/// Start Point of vector.
		/// </summary>
		[
			XmlAttribute("start point")
		]
		private PointD m_pntStartPosition;

		/// <summary>
		/// End Point of vector.
		/// </summary>
		[
			XmlAttribute("end point")
		]
		private PointD m_pntEndPosition;


		#endregion
		//-------------------------------------------------------------------------------
		#region Constructors

		/// <summary>
		/// Public Constructor for create template vector.
		/// </summary>
		/// <param name="start">Start point</param>
		/// <param name="end">End point</param>
		public vector(PointD start, PointD end)
		{
			m_intID = -10;
			m_pntEndPosition = end;
			m_pntStartPosition = start;
		}

		/// <summary>
		/// Internal constructor for create vector with
		///  start point & end point & vector ID
		/// </summary>
		/// <param name="ID">Identifier</param>
		/// <param name="start">Start point</param>
		/// <param name="end">End point</param>
		internal vector(int ID, PointD start, PointD end)
		{
			m_intID = ID;
			m_pntEndPosition = end;
			m_pntStartPosition = start;
		}

		/// <summary>
		/// Create vector from othr vector with new ID.
		/// </summary>
		/// <param name="ID">Identifier</param>
		/// <param name="vct">New vector</param>
		public vector(int ID, vector vct)
		{
			m_intID = ID;
			m_pntEndPosition = vct.EndPoint;
			m_pntStartPosition = vct.StartPoint;
		}

		/// <summary>
		/// Create vector with ID & x1,y1,x2,y2 in scaler mode.
		/// </summary>
		/// <param name="ID">Identifier</param>
		/// <param name="x1">(X,y)-(x,y)</param>
		/// <param name="y1">(x,Y)-(x,y)</param>
		/// <param name="x2">(x,y)-(X,y)</param>
		/// <param name="y2">(x,y)-(x,Y)</param>
		internal vector(int ID, double x1, double y1, double x2, double y2)
		{
			m_pntStartPosition = new PointD(x1, x2);
			m_pntEndPosition = new PointD(y1, y2);
			m_intID = ID;
		}

		/// <summary>
		/// Create vector with i & j.
		/// </summary>
		/// <param name="i">I</param>
		/// <param name="j">J</param>
		private vector(double i, double j)
		{
			m_pntStartPosition = new PointD(0, 0);
			m_pntEndPosition = new PointD(i, -j);
			m_intID = -7;
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Overrided Functions

		/// <summary>
		/// Return string like vector in user mode.
		/// </summary>
		/// <returns>Vector in string</returns>
		public override string ToString()
		{
			return
				"[(" + vector.Tester(m_pntStartPosition.X).ToString() +
				", " + vector.Tester(m_pntStartPosition.Y).ToString() + ")"+
				"-(" + vector.Tester(m_pntStartPosition.X).ToString() +
				", " + vector.Tester(m_pntStartPosition.Y).ToString() + ")]";
		}

		/// <summary>
		/// Compare vectors.
		/// </summary>
		/// <param name="obj">Vector objectS</param>
		/// <returns>True if obj was same current object</returns>
		public override bool Equals(object obj)
		{
			try
			{
				return this == (vector)obj;
			}
			catch
			{
				return obj.Equals(this);
			}
		}

		/// <summary>
		/// Get has code.
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode()
		{
			return ID;
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Gets first point that vector become strat.
		/// </summary>
		public PointD StartPoint
		{
			get
			{
				return m_pntStartPosition;
			}
			set
			{
				m_pntStartPosition = value;
			}
		}

		/// <summary>
		/// Gets End Point of vector.
		/// </summary>
		public PointD EndPoint
		{
			get
			{
				return m_pntEndPosition;
			}
			set
			{
				m_pntEndPosition = value;
			}
		}

		/// <summary>
		/// Gets ID for vector.
		/// </summary>
		public int ID
		{
			set
			{
				m_intID = value;
			}
			get
			{
				return m_intID;
			}
		}

		/// <summary>
		/// Gets I in real mode.
		/// </summary>
		public double I
		{
			get
			{
				return (m_pntEndPosition.X - m_pntStartPosition.X);
			}
		}

		/// <summary>
		/// Gets I in user mode.
		/// </summary>
		public string SI
		{
			get
			{
				return vector.Tester(m_pntEndPosition.X - 
					m_pntStartPosition.X).ToString();
			}
		}

		/// <summary>
		/// Gets J in real mode.
		/// </summary>
		public double J
		{
			get
			{
				return (m_pntEndPosition.Y - m_pntStartPosition.Y);
			}
		}

		/// <summary>
		/// Gets J in user mode.
		/// </summary>
		public string SJ
		{
			get
			{
				return vector.Tester(m_pntStartPosition.Y - 
					m_pntEndPosition.Y).ToString();
			}
		}

		/// <summary>
		/// Gets ID like string.
		/// </summary>
		public string Name
		{
			get
			{
				return (m_intID+1).ToString();
			}
		}

		/// <summary>
		/// Gets Lenght of vector.
		/// </summary>
		public double VectorLenght
		{
			get
			{
				double
					l_dblExper1 = (m_pntStartPosition.X - m_pntEndPosition.X),
					l_dblExper2 = (m_pntStartPosition.Y - m_pntEndPosition.Y);

				return (Math.Sqrt((l_dblExper1 * l_dblExper1)+
								  (l_dblExper2 * l_dblExper2)));
			}
		}

		/// <summary>
		/// Gets Lenght of vector in string.
		/// </summary>
		public string Lenght
		{
			get
			{
				double l_dblExper1 = (m_pntStartPosition.X-m_pntEndPosition.X),
					   l_dblExper2 = (m_pntStartPosition.Y-m_pntEndPosition.Y);

				return vector.Tester((Math.Sqrt((
					(l_dblExper1*l_dblExper1)) +
					(l_dblExper2*l_dblExper2)))).ToString();
			}
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region Operators
/*
		/// <summary>
		/// return result of Add two vector together.
		/// </summary>
		/// <param name="vct1">First vector operand</param>
		/// <param name="vct2">Second vector operand</param>
		/// <returns>Result vector</returns>
		public static vector operator+(vector vct1, vector vct2)
		{
			return new vector(-2,
				vct1.StartPoint.X + vct2.StartPoint.X,
				-vct1.StartPoint.Y - vct2.StartPoint.Y,
				vct1.EndPoint.X + vct2.EndPoint.X,
				-vct1.EndPoint.Y - vct2.EndPoint.Y);
		}

		/// <summary>
		/// return result of Subtract two vector.
		/// </summary>
		/// <param name="vct1">First vector operand</param>
		/// <param name="vct2">Second vector operand</param>
		/// <returns>Result vector</returns>
		public static vector operator-(vector vct1, vector vct2)
		{
			return new vector(-1,
				vct1.StartPoint.X - vct2.StartPoint.X,
				vct2.StartPoint.Y - vct1.StartPoint.Y,
				vct1.EndPoint.X - vct2.EndPoint.X,
				vct2.EndPoint.Y - vct1.EndPoint.Y);
		}
*/
		/// <summary>
		/// Negative.
		/// </summary>
		/// <param name="v">Vector operand</param>
		/// <returns>Result vector</returns>
		public static vector operator-(vector v)
		{
			return new vector(v.ID,
				-v.StartPoint.X, -v.StartPoint.Y,
				-v.EndPoint.X, -v.EndPoint.Y);
		}

		/// <summary>
		/// Special add two vector.
		/// </summary>
		/// <param name="v">First vector operand</param>
		/// <param name="w">Second vector operand</param>
		/// <returns>Result vector</returns>
		public static vector operator+(vector v, vector w)
		{
			return new vector(v.I + w.I, -v.J - w.J);
		}

		/// <summary>
		/// Special subtract two vector.
		/// </summary>
		/// <param name="v">First vector operand</param>
		/// <param name="w">Second vector operand</param>
		/// <returns>Result vector</returns>
		public static vector operator-(vector v, vector w)
		{
			return new vector(v.I - w.I, w.J - v.J);
		}

		/// <summary>
		/// Indexer return lenght of vector.
		/// </summary>
		public double this[vector v]
		{
			get
			{
				return v.VectorLenght;
			}
		}

		/// <summary>
		/// Compare vectors.
		/// </summary>
		/// <param name="v">First vector operand</param>
		/// <param name="w">Second vector operand</param>
		/// <returns>Result vector</returns>
		public static bool operator!=(vector v, vector w)
		{
			return v.EndPoint != w.EndPoint && v.StartPoint != w.StartPoint;
		}

		/// <summary>
		/// Compare vectors.
		/// </summary>
		/// <param name="v">First vector operand</param>
		/// <param name="w">Second vector operand</param>
		/// <returns>Result vector</returns>
		public static bool operator==(vector v, vector w)
		{
			return v.EndPoint == w.EndPoint && v.StartPoint == w.StartPoint;
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region public functions

		/// <summary>
		/// True is vector is empty.
		/// </summary>
		/// <returns>True if empty</returns>
		public bool IsEmpty()
		{
			return	m_pntEndPosition.X == 0 && m_pntEndPosition.Y == 0 &&
					m_pntStartPosition.X == 0 && m_pntStartPosition.Y == 0;
		}

		/// <summary>
		/// Convert to Real Coordinant.
		/// </summary>
		/// <param name="pnt">Point for convert</param>
		/// <param name="W">Width</param>
		/// <param name="H">Height</param>
		/// <returns>Converted point</returns>
		public static PointD Convert(PointD pnt, int W, int H)
		{
			return new PointD(pnt.X - W / 2, pnt.Y - H / 2);
		}

		/// <summary>
		/// Convert real point to screen user point.
		/// </summary>
		/// <param name="pnt">Point for convert</param>
		public static string ConvertYinPoint(PointD pnt)
		{
			return (new PointD(pnt.X, -pnt.Y)).ToString();
		}

		/// <summary>
		/// convert computer x to user x.
		/// </summary>
		/// <param name="x">X</param>
		/// <returns>Tested</returns>
		public static double Tester(double x)
		{
			return x / Global.s_intGeo;
		}

		/// <summary>
		/// Convert computer y to user Y with Client Heigth.
		/// </summary>
		/// <param name="y">Y</param>
		/// <param name="ClientHeigth">Client height</param>
		/// <returns>Tested</returns>
		public static double Tester(double y, int ClientHeigth)
		{
			return y / Global.s_intGeo;
		}

		/// <summary>
		/// Convert user x to computer X.
		/// </summary>
		/// <param name="x">X</param>
		/// <returns>Anti tested</returns>
		public static double AntiTester(double x)
		{
			return x * Global.s_intGeo;
		}

		/// <summary>
		/// Convert user y to Computer Y with Client Heigth.
		/// </summary>
		/// <param name="y">Y</param>
		/// <param name="ClientHeigth">Client height</param>
		/// <returns>Anti tested</returns>
		public static double AntiTester(double y, int ClientHeigth)
		{
			return y * Global.s_intGeo;
		}

		#endregion
	}
}


