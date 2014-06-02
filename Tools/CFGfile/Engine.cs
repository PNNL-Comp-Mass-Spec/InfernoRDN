#region Copyright

/*
 * File Name:	  Engine.cs
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
 * Standard namespaces included:
 * System, and its sub-namespaces
 * Text, Collections, IO, and Security.
 */

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Security;

#endregion

namespace Tools
{
  #region Enumerated Types

  /// <summary>
  /// The EngineStatus type is defined to describe the
  /// possible states of an Engine instance:
  /// INIT		  The Engine is being initialized (i.e.
  ///			  no attempt to read a cfg file has been
  ///			  performed yet);
  /// WORKING	  The Engine is currently reading a cfg file;
  /// OK		  The Engine has read a cfg file and no
  ///			  error occurred during the process;
  /// FAIL		  The Engine has attempted to read a cfg file,
  ///			  and some error occurred (e.g.: file not found,
  ///			  or some required parameter was missing from
  ///			  the cfg file); see the Error and the
  ///			  Details methods for additional details.
  /// </summary>
  public enum EngineStatus
  {
	INIT,
	WORKING,
	OK,
	FAIL
  }
  /// <summary>
  /// The ErrorCode type is defined to represent
  /// the possible errors placing an Engine instance
  /// in FAIL state:
  /// OK						  No error detected;
  /// BAD_FILENAME				  The name of the cfg file to be read
  ///							  includes illegal characters, or is otherwise
  ///							  unacceptable;
  /// NO_SUCH_FILE				  Could not locate/access the specified cfg file;
  /// SYS_ERROR					  System Error;
  /// MALFORMED_LINE			  Malformed line detected in the cfg file;
  /// MISSING_REQUIRED_PARAMETER  One or more required parameter(s) were missing
  ///							  from the cfg file.
  /// </summary>
  public enum ErrorCode
  {
	OK,
	BAD_FILENAME,
	NO_SUCH_FILE,
	SYS_ERROR,
	MALFORMED_LINE,
	MISSING_REQUIRED_PARAMETER
  }

  #endregion

  public class Engine
  {
	#region Static Methods

	/// <summary>
	/// Produces a human-readable form of the EngineStatus received
	/// as parameter.
	/// </summary>
	/// <param name="x">The EngineStatus to be transformed to human-readable form.</param>
	/// <returns>The human-readable string form of the EngineStatus x.</returns>
	public static string EngineStatusName(EngineStatus x)
	{
	  switch(x)
	  {
		case EngineStatus.INIT:		return "Initializing";
		case EngineStatus.WORKING:	return "Working";
		case EngineStatus.OK:		return "Ok";
		case EngineStatus.FAIL:		return "Failed";
		default:  return "Unrecognized Engine Status: "+x.ToString();
	  }
	}
	/// <summary>
	/// Produces a human-readable form of the ErrorCode received
	/// as parameter.
	/// </summary>
	/// <param name="x">The ErrorCode to be transformed to human-readable form.</param>
	/// <returns>The human-readable string form of the ErrorCode x.</returns>
	public static string ErrorCodeName(ErrorCode x)
	{
	  switch(x)
	  {
		case ErrorCode.OK:			  return "No Error";
		case ErrorCode.BAD_FILENAME:  return "Bad Filename";
		case ErrorCode.NO_SUCH_FILE:  return "No Such File";
		case ErrorCode.SYS_ERROR:	  return "System Error";
		case ErrorCode.MALFORMED_LINE:return "Malformed Line";
		case ErrorCode.MISSING_REQUIRED_PARAMETER:
		  return "Missing Required parameter(s)";
		default:  return "Unrecognized Error Code: "+x.ToString();
	  }
	}

	#endregion

	#region Private Fields

	/// <summary>
	/// The status of the Engine.
	/// </summary>
	private EngineStatus  status	  = EngineStatus.INIT;
	/// <summary>
	/// Error code describing the issue detected by the Engine.
	/// </summary>
	private ErrorCode	  errCode	  = ErrorCode.OK;
	/// <summary>
	/// Details regarding the error condition detected by the
	/// Engine.
	/// </summary>
	private string		  errDetails  = "";
	/// <summary>
	/// Name of the cfg file to be read.
	/// </summary>
	private string		  filename	  = "";
	/// <summary>
	/// Collection of the Parameters defined for the Engine.
	/// The Collection is organized as parameter_name => Parameter instance.
	/// </summary>
	private Hashtable	  parameters  = new Hashtable();

	#endregion

	#region Public Attributes

	/// <summary>
	/// Gets the status of the Engine.
	/// </summary>
	public EngineStatus	  Status  { get { return status;	  } }
	/// <summary>
	/// Gets the error code describing the current error.
	/// </summary>
	public ErrorCode	  Error	  { get { return errCode;	  } }
	/// <summary>
	/// Gets the details regarding the current error.
	/// </summary>
	public string		  Details { get { return errDetails;  } }
	/// <summary>
	/// Gets the name of the cfg file to be read by the Engine.
	/// </summary>
	public string		  Filename{ get { return filename;	  } }
	/// <summary>
	/// Gets the Collection of Parameters defined for the Engine,
	/// in the form of an Hashtable indexed by parameter name.
	/// </summary>
	public Hashtable	  Parameters
	{
	  get
	  {
		Hashtable r = new Hashtable();
		foreach(DictionaryEntry de in parameters)
		  r.Add(de.Key, ((Parameter)de.Value).Clone());
		return r;
	  }
	}
	/// <summary>
	/// Indexer: gets the Parameter with the specified name;
	/// if no such Parameter exists in the Engine, it returns null.
	/// </summary>
	public Parameter this[string pName]
	{
	  get
	  {
		if(!parameters.ContainsKey(pName))	return null;
		return ((Parameter)parameters[pName]).Clone();
	  }
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Creates a new instance of the Engine class,
	/// and sets it to read the specified filename.
	/// </summary>
	/// <param name="inFilename"></param>
	public Engine(string inFilename)
	{
	  filename	= inFilename;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Resets the Engine instance to its
	/// INIT Status; also reset the error code to OK.
	/// <exception cref="System.ApplicationException">Thrown if the Engine
	/// is currently WORKING.</exception>
	/// </summary>
	public void Reset()
	{
	  if(status==EngineStatus.WORKING)
		throw new ApplicationException("CFGLite: [Engine::Reset()]\n"+
		  "The Engine is currently Working.");

	  status	  = EngineStatus.INIT;
	  errCode	  = ErrorCode.OK;
	  errDetails  = "";
	}
	/// <summary>
	/// Changes the file the Engine is ready to read.
	/// <exception cref="System.ApplicationException">Thrown if the Engine
	/// is currently WORKING.</exception>
	/// </summary>
	/// <param name="inFilename">The name of the file that the Engine should
	/// be set to read on the next ReadFile request.</param>
	public void ChangeFile(string inFilename)
	{
	  if(status==EngineStatus.WORKING)
		throw new ApplicationException("CFGLite: [Engine::ChangeFile(string)]\n"+
		  "The Engine is currently Working.");

	  this.Reset();
	  filename = inFilename;
	}
	/// <summary>
	/// Clears the collection of Parameters defined in the Engine.
	/// <exception cref="System.ApplicationException">Thrown if the Engine
	/// is currently WORKING.</exception>
	/// </summary>
	public void ClearParameters()
	{
	  if(status==EngineStatus.WORKING)
		throw new ApplicationException("CFGLite: [Engine::ClearParameters()]\n"+
		  "The Engine is currently Working.");

	  parameters.Clear();
	}
	/// <summary>
	/// Adds the specified Parameter to the set held by the Engine.
	/// If another Parameter was defined in the Engine with the same name,
	/// the pre-existing Parameter is overwritten.
	/// <exception cref="System.ApplicationException">Thrown if the Engine
	/// is currently WORKING.</exception>
	/// </summary>
	/// <param name="p"></param>
	public void AddParameter(Parameter p)
	{
	  if(status==EngineStatus.WORKING)
		throw new ApplicationException("CFGLite: [Engine::AddParameter(Parameter)]\n"+
		  "The Engine is currently Working.");

	  //Overwrites any existing parameter with same name !
	  parameters[p.Name]=p.Clone();
	}
	/// <summary>
	/// Deletes the Parameter with the specified name.
	/// If no such Parameter is defined in the Engine, no exception
	/// is thrown.
	/// <exception cref="System.ApplicationException">Thrown if the Engine
	/// is currently WORKING.</exception>
	/// </summary>
	/// <param name="pName"></param>
	public void DelParameter(string pName)
	{
	  if(status==EngineStatus.WORKING)
		throw new ApplicationException("CFGLite: [Engine::DelParameter(string)]\n"+
		  "The Engine is currently Working.");

	  parameters.Remove(pName);
	}
	/// <summary>
	/// Requests the Engine to read the cfg file.
	/// The name of the cfg file should be already set in the Engine's
	/// filename private method (specified in the Engine's constructor,
	/// or in a call to ChangeFile).
	/// <exception cref="System.ApplicationException">Thrown if the Engine
	/// is currently WORKING, or in any state other than INIT or OK.</exception>
	/// </summary>
	/// <returns>EngineStatus.OK if the Engine encountered no erroneous condition;
	/// otherwise EngineStatus.FAIL is returned.</returns>
	public EngineStatus ReadFile()
	{
	  if(status==EngineStatus.WORKING)
		throw new ApplicationException("CFGLite: [Engine::ReadFile()]\n"+
		  "The Engine is currently Working.");
	  if(	status!=EngineStatus.INIT
		&&	status!=EngineStatus.OK	)
		throw new ApplicationException("CFGLite: [Engine::ReadFile()]\n"+
		  "The Engine is currently in its "+Engine.EngineStatusName(status)+" state.\n"+
		  "The Engine must be reset before a new cfg file may be read.");

	  StringBuilder sb = new StringBuilder();
	  status=EngineStatus.WORKING;

	  filename=filename.Trim();
	  if(filename.Length==0)
	  {
		sb.Append("The name of the cfg file to be read is null after trimming.");
		errDetails = sb.ToString();
		errCode = ErrorCode.BAD_FILENAME;
		status = EngineStatus.FAIL;
		return EngineStatus.FAIL;
	  }
	  try
	  {
		if(!File.Exists(filename))
		{
		  sb.Append("The cfg file to be read ('");
		  sb.Append(filename);
		  sb.Append("')\ndoes not exist, or could not be located.");
		  errDetails = sb.ToString();
		  errCode=ErrorCode.NO_SUCH_FILE;
		  status = EngineStatus.FAIL;
		  return EngineStatus.FAIL;
		}
	  }
	  catch(ArgumentException)
	  {
		sb.Append("The name of the cfg file to be read ('");
		sb.Append(filename);
		sb.Append("')\ncaused an 'ArgumentException' exception when the\n");
		sb.Append("CFGLite Engine attempted to verify if the file exists.\n");
		sb.Append("This is generally caused by a null file name or a file name or\n");
		sb.Append("including illegal characters.");
		errDetails = sb.ToString();
		errCode = ErrorCode.SYS_ERROR;
		status = EngineStatus.FAIL;
		return EngineStatus.FAIL;
	  }
	  catch(PathTooLongException)
	  {
		sb.Append("The name of the cfg file to be read ('");
		sb.Append(filename);
		sb.Append("')\nrepresents a path that is too long for the system.");
		errDetails=sb.ToString();
		errCode = ErrorCode.SYS_ERROR;
		status = EngineStatus.FAIL;
		return EngineStatus.FAIL;
	  }
	  catch(NotSupportedException)
	  {
		sb.Append("The name of the cfg file to be read ('");
		sb.Append(filename);
		sb.Append("')\ncontains a semi-colon (:).");
		errDetails=sb.ToString();
		errCode = ErrorCode.SYS_ERROR;
		status = EngineStatus.FAIL;
		return EngineStatus.FAIL;
	  }

	  //File exists.

	  StreamReader sr = null;
	  try
	  {
		sr = File.OpenText(filename);
		string line = "";
		line = sr.ReadLine();
		while(line!=null)
		{
		  ProcessLine(line);
		  if(status==EngineStatus.FAIL)
			return EngineStatus.FAIL;
		  line = sr.ReadLine();
		}//WEND

		CheckParameters();
		if(errCode==ErrorCode.OK)
		{
		  status = EngineStatus.OK;
		  return EngineStatus.OK;
		}
		else
		{
		  status = EngineStatus.FAIL;
		  return EngineStatus.FAIL;
		}
	  }
	  catch(SecurityException)
	  {
		sb.Append("The CFGLite Engine could not access the cfg file to be read\n('");
		sb.Append(filename);
		sb.Append("')\nbecause it does not have\n");
		sb.Append("sufficient access rights.");
		errDetails=sb.ToString();
		errCode = ErrorCode.SYS_ERROR;
		status = EngineStatus.FAIL;
		return EngineStatus.FAIL;
	  }
	  catch(DirectoryNotFoundException)
	  {
		sb.Append("The CFGLite Engine could not access the cfg file to be read\n('");
		sb.Append(filename);
		sb.Append("')\nbecause its parent directory could\n");
		sb.Append("not be located.");
		errDetails=sb.ToString();
		errCode = ErrorCode.SYS_ERROR;
		status = EngineStatus.FAIL;
		return EngineStatus.FAIL;
	  }
	  catch(OutOfMemoryException)
	  {
		sb.Append("An 'OutOfMemoryException' exception was raised\n");
		sb.Append("while reading the content of the cfg file\n('");
		sb.Append(filename);
		sb.Append("').");
		errDetails = sb.ToString();
		errCode = ErrorCode.SYS_ERROR;
		status = EngineStatus.FAIL;
		return EngineStatus.FAIL;
	  }
	  catch(IOException)
	  {
		sb.Append("An 'IOException' exception was raised\n");
		sb.Append("while reading the content of the cfg file\n('");
		sb.Append(filename);
		sb.Append("').");
		errDetails = sb.ToString();
		errCode = ErrorCode.SYS_ERROR;
		status = EngineStatus.FAIL;
		return EngineStatus.FAIL;
	  }
	  finally
	  {
		sr.Close();
	  }
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Processes a Line from the cfg file.
	/// If any error is encountered during the process,
	/// the method sets the Engine's errCode and errDetails
	/// private fields accordingly.
	/// Lines are assumed to be in parameter_name = parameter_value form.
	/// Spaces before and after parameter_name and parameter_value are skipped.
	/// Empty lines (after trimming) are skipped.
	/// Lines beginning with '#' (after trimming) are comments, and are skipped;
	/// For other lines:
	///	  if no '=' is found: error
	///	  if multiple '=': the first one is the separator; others are bundled
	///		in the parameter value;
	///	  if no parameter name or no parameter value is found: error
	///	  if parameter name is not one of the defined parameters, the line is skipped
	///	  if multiple lines define the same parameter: the last one overrides the previous ones.
	///	In general, when a well-formed line is processed, if the parameter_name matches
	///	the name of one of the parameters defined in the Engine instance, then the
	///	value extracted from the line is assigned to the Parameter.
	/// </summary>
	/// <param name="line">The line to be processed.</param>
	private void ProcessLine(string line)
	{

	  line=line.Trim();
	  if(line.Length<=0)  return;

	  if(line[0]=='#')	  return;

	  StringBuilder sb = new StringBuilder();

	  if(line.IndexOf('=')<0)
	  {
		status = EngineStatus.FAIL;
		sb.Append("Malformed line detected: no '=' character in the following line:\n");
		sb.Append(line);
		errDetails = sb.ToString();
		errCode=ErrorCode.MALFORMED_LINE;
		return;
	  }

	  string[] parts = line.Split(new char[] {'='}, 2);

	  if(parts[0]!=null)  parts[0]=parts[0].Trim();
	  if(parts[1]!=null)  parts[1]=parts[1].Trim();

	  if(parts[0]==null || parts[0].Length<=0)
	  {
		status = EngineStatus.FAIL;
		sb.Append("Malformed line detected: no <parameter name> in the following line:\n");
		sb.Append(line);
		errDetails = sb.ToString();
		errCode = ErrorCode.MALFORMED_LINE;
		return;
	  }
	  if(parts[1]==null || parts[1].Length<=0)
	  {
		status = EngineStatus.FAIL;
		sb.Append("Malformed line detected: no <parameter values> in the following line:\n");
		sb.Append(line);
		errDetails = sb.ToString();
		errCode = ErrorCode.MALFORMED_LINE;
		return;
	  }

	  if(!parameters.ContainsKey(parts[0]))	return;
	  Parameter p = (Parameter)parameters[parts[0]];
	  p.Val=parts[1];
	  parameters[parts[0]]=p;
	}
	/// <summary>
	/// Checks that all of the REQUIRED Parameters defined in the Engine
	/// instance have an assigned value.
	/// If one or more Required parameters have no value assigned, the
	/// method sets the Engine's errCode and errDetails accordingly.
	/// </summary>
	private void CheckParameters()
	{
	  StringBuilder sb = new StringBuilder("The following Required Parameters are missing from the\n");
	  sb.Append("cfg file read ('");
	  sb.Append(filename);
	  sb.Append("'):\n");

	  foreach(Parameter p in parameters.Values)
		if(	  p.Type==ParameterType.REQUIRED
		  &&  (p.Val==null || p.Val.Length<=0)	)
		{
		  errCode = ErrorCode.MISSING_REQUIRED_PARAMETER;
		  sb.Append(p.Name);
		  sb.Append(";\n");
		}
	  if(errCode==ErrorCode.MISSING_REQUIRED_PARAMETER)
		errDetails=sb.ToString();
	}

	#endregion
  }
}
