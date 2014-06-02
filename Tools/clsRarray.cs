using System;

namespace Tools
{
	/// <summary>
	/// Summary description for clsRarray.
	/// </summary>
	public class clsRarray
	{
		public double[,] matrix ; 
		public string[] rowNames ; 
		public string[] colHeaders ;

		public clsRarray()
		{
			matrix = null ;
			rowNames = null ;
			colHeaders = null ;
		}

		public clsRarray(double[,] mat, string[] rows, string[] cols)
		{
			matrix = mat ;
			rowNames = rows ;
			colHeaders = cols ;
		}
	}
}
