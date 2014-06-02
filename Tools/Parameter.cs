#region Copyright

/*
 * File Name:	  Parameter.cs
 * Version:		  1.0, 24-AUG-2K3
 * Author:		  Frank "Olorin" Rizzi, fkh1000@yahoo.com
 * 
 * Copyright:	  This code belongs to the CFGLite project.
 *				  All of the code in the CFGLite project is
 *				  provided "as-is", for non-commercial purpose.
 *				  Feel free to use the code as you see fit
 *				  for any non-commercial purpose, provided you
 *				  credit the author in your source code.
 *				  Since the CFGLite project is provided
 *				  "as-is", the author does not assume any
 *				  responsibility for any problem you may incur
 *				  by using the code in the project.
 *				  Feedback is appreciated (see the
 *				  "Contact" section below), and the
 *				  author will provided proper credit as needed.
 *				  If you intend to use this code in any commercial
 *				  application, you must contact the author and
 *				  receive proper authorization.
 * 
 * Contact:		  Frank "Olorin" Rizzi: fkh1000@yahoo.com
 * 
 * History:
 * v.1.0		  24-AUG-2K3
 *				  First Draft, by FR.
 */

#endregion

#region External Dependencies

/*
 * The System namespace is included by default.
 * The Text namespace is included to import the
 * StringBuilder class.
 */
using System;
using System.Text;

#endregion

namespace DAnTE.Tools
{
  #region Enumerated Types

  /// <summary>
  /// The ParameterType enum is defined to describe
  /// the type of a Parameter. Parameters belong to one
  /// of two categories:
  /// REQUIRED Parameters:	these parameters should be
  ///						included in the cfg files read;
  /// OPTIONAL Parameters:	these parameters may or may not be
  ///						found in the cfg file read.
  /// </summary>
  public enum ParameterType
  {
	OPTIONAL,
	REQUIRED
  }

  #endregion

  /// <summary>
  /// The Parameter class is used in the CFGLite project to represent
  /// parameters to be recognized while reading a cfg file.
  /// </summary>
  public class Parameter
  {
	#region Static Methods

	/// <summary>
	/// The ParameterType method returns a human-readable
	/// form of the ParameterType received as parameter.
	/// </summary>
	/// <param name="x">The ParameterType to be transformed to human-readable form.</param>
	/// <returns>A human-readable string form of the ParameterType received as parameter.</returns>
	public static string ParameterTypeName(ParameterType x)
	{
	  switch(x)
	  {
		case ParameterType.OPTIONAL:	return "Optional";
		case ParameterType.REQUIRED:	return "Required";
		default:  return "Unrecognized Parameter Type: "+x.ToString();
	  }
	}
	/// <summary>
	/// The ParameterTypeShortName method returns a brief version of the
	/// ParameterType received as parameter, suitable for reference in a human-readable
	/// message.
	/// </summary>
	/// <param name="x">The ParameterType whose short name should be returned.</param>
	/// <returns>The short name of the ParameterType x, in the form of a string.</returns>
	public static string ParameterTypeShortName(ParameterType x)
	{
	  switch(x)
	  {
		case ParameterType.OPTIONAL:	return "[O]";
		case ParameterType.REQUIRED:	return "[R]";
		default:  return "[?]";
	  }
	}

	#endregion

	#region Static Fields

	/// <summary>
	/// The NEW_PARAMETER string is used as name of a Parameter instance
	/// for which no name was provided. If this name collides with the
	/// name of a Parameter you need to represent, you may incur in problems.
	/// </summary>
	public static string  NEW_PARAMETER	= "[New Parameter]";

	#endregion

	#region Private Fields

	/// <summary>
	/// The type of the parameter.
	/// </summary>
	private ParameterType type	  = ParameterType.OPTIONAL;
	/// <summary>
	/// The name of the parameter.
	/// </summary>
	private	string		  name	  = "";
	/// <summary>
	/// The value of the parameter.
	/// </summary>
	private string		  val	  = "";

	#endregion

	#region Public Attributes

	/// <summary>
	/// Gets the type of the parameter.
	/// </summary>
	public ParameterType  Type	{ get { return type;  } }
	/// <summary>
	/// Gets the name of the parameter.
	/// </summary>
	public string		  Name	{ get { return name;  } }
	/// <summary>
	/// Gets or Sets the value of the parameter.
	/// </summary>
	public string		  Val
	{
	  get { return val;  }
	  set { val = value; }
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Creates a new instance of the Parameter class. The NEW_PARAMETER
	/// field is used for the new Parameter's name.
	/// </summary>
	public Parameter()
	{
	  name = Parameter.NEW_PARAMETER;
	}
	/// <summary>
	/// Creates a new instance of the Parameter class, with the
	/// specified name. The Parameter is, by default, Optional.
	/// </summary>
	/// <param name="inName">The name for the new Parameter.</param>
	public Parameter(string inName)
	{
	  name	=inName;
	}
	/// <summary>
	/// Creates a new instance of the Parameter class, with the
	/// specified name and type.
	/// </summary>
	/// <param name="inName">The name for the new Parameter.</param>
	/// <param name="inType">The type of the new Parameter.</param>
	public Parameter(string inName, ParameterType inType)
	{
	  name	= inName;
	  type	= inType;
	}
	/// <summary>
	/// Creates a new instance of the Parameter class, with the
	/// specified name and value. The Parameter is, by default, Optional.
	/// Use this constructor if you wish to define a default
	/// value for an Optional Parameter.
	/// </summary>
	/// <param name="inName">The name of the new Parameter.</param>
	/// <param name="inVal">The value of the new Parameter.</param>
	public Parameter(string inName, string inVal)
	{
	  name	= inName;
	  val	= inVal;
	}
	/// <summary>
	/// Creates a new instance of the Parameter class, with the
	/// specified name, type, and value.
	/// </summary>
	/// <param name="inName">The name of the new Parameter.</param>
	/// <param name="inType">The type of the new Parameter.</param>
	/// <param name="inVal">The value of the new Parameter.</param>
	public Parameter(string inName, ParameterType inType, string inVal)
	{
	  name	= inName;
	  type	= inType;
	  val	= inVal;
	}
	/// <summary>
	/// Copy-Constructor: creates a new instance of the Parameter
	/// class, as a deep copy of the Parameter passed as parameter.
	/// </summary>
	/// <param name="rhs">The Parameter to be copied.</param>
	public Parameter(Parameter rhs)
	{
	  name	  = rhs.name;
	  type	  = rhs.type;
	  val	  = rhs.val;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Creates a deep copy of this Parameter.
	/// </summary>
	/// <returns>A deep copy of this Parameter as a new Parameter instance.</returns>
	public Parameter Clone()
	{ return new Parameter(this); }

	/// <summary>
	/// Overrides the inherited GetHashCode() method.
	/// </summary>
	/// <returns></returns>
	public override int GetHashCode()
	{ return this.ToString().GetHashCode(); }
	/// <summary>
	/// Overrides the inherited ToString() method.
	/// Produces a human-readable form of the Parameter instance.
	/// The format of the string produced is as follows:
	/// parameter_name type_short_code [{parameter_value}]
	/// The parameter_value is included only if one is defined
	/// for the instance.
	/// The type_short_code is the short code produced by
	/// the ParameterTypeShortCode method.
	/// </summary>
	/// <returns>A human readable string describing the Parameter.</returns>
	public override string ToString()
	{
	  StringBuilder sb = new StringBuilder(name);
	  sb.Append(" ");
	  sb.Append(Parameter.ParameterTypeShortName(type));
	  if(val!=null && val.Length>0)
	  {
		sb.Append(" {");
		sb.Append(val);
		sb.Append("}");
	  }
	  return sb.ToString();
	}

	#endregion

  }
}
