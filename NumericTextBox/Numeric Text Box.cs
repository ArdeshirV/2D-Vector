#region Header
// https://github.com/ArdeshirV/2D-Vector
// Copyright 2002-2004 ArdeshirV, Licensed Under MIT


using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

#endregion
//-----------------------------------------------------------------------------
namespace ArdeshirV.Components
{
	/// <summary>
	/// Numeric text box.
	/// </summary>
	public class NumericTextBox : System.Windows.Forms.TextBox
	{
		#region Variables

		private double m_dblValue;
		private double m_dblMaximum;
		private double m_dblMinimum;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion
		//---------------------------------------------------------------------
		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public NumericTextBox()
		{
			Text = "0.0";
			m_dblValue = 0.0F;
			m_dblMaximum = double.MaxValue;
			m_dblMinimum = double.MinValue;
			TextChanged += new EventHandler(Text_Changed);
		}

		#endregion
		//---------------------------------------------------------------------
		#region Properties

		/// <summary>
		/// Gets or sets value of numeric text box.
		/// </summary>
		public double Value
		{
			get
			{
				return m_dblValue;
			}
			set
			{
				Text = (m_dblValue = value).ToString();
			}
		}

		/// <summary>
		/// Gets or sets maximum value.
		/// </summary>
		public double Maximum
		{
			get
			{
				return m_dblMaximum;
			}
			set
			{
				m_dblMaximum = value;
			}
		}

		/// <summary>
		/// Gets or sets minimum value.
		/// </summary>
		public double Minimum
		{
			get
			{
				return m_dblMinimum;
			}
			set
			{
				m_dblMinimum = value;
			}
		}

		#endregion
		//---------------------------------------------------------------------
		#region Event handlers

		/// <summary>
		/// Occured whenever text value has been changed.
		/// </summary>
		/// <param name="sender">This</param>
		/// <param name="e">Event argument</param>
		private void Text_Changed(object sender, EventArgs e)
		{
			try
			{
				if(Text == "-" || Text == "+")
					return;

				m_dblValue = double.Parse(Text);
			}
			catch
			{
				ResetFocus();
			}
		}

		#endregion
		//---------------------------------------------------------------------
		#region Utility functions

		/// <summary>
		/// Set focus and make text empty.
		/// </summary>
		private void ResetFocus()
		{
			if(Text.Length > 1)
				Text = Text.Substring(0, Text.Length - 1);
			else
				Text = "";

			Focus();
		}

		#endregion
		//---------------------------------------------------------------------
		#region Overrided functions

		/// <summary>
		/// Occured whenever control lost focus.
		/// </summary>
		/// <param name="e">Event arument</param>
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);

			if(Text == "")
				Text = "0.0";
		}

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
	}
}


