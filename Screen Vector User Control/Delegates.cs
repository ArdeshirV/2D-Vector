#region Header
// https://github.com/ArdeshirV/2D-Vector
// Copyright 2002-2004 ArdeshirV, Licensed Under MIT


using System;
using System.Drawing;
using ArdeshirV.Components.ScreenVector;

#endregion
//----------------------------------------------------------------------------
namespace ArdeshirV.Components.ScreenVector
{
	#region Delegete Defination :
	/// <summary>
	/// Reactive for when Active Vector changed or other events.
	/// </summary>
	public delegate void ReActive();

	/// <summary>
	/// Occured whenever (New Wizard Form) Closed with Create Button.
	/// </summary>
	public delegate void OptionEventHandler(PointD p1 , PointD p2,
											double i, double j, bool bln);

	/// <summary>
	/// Occured whenever Add all vectors.
	/// </summary>
	public delegate void Plused(vector clr);

	#endregion
}


