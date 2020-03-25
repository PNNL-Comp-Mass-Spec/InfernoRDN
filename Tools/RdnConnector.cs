using System;
using RDotNet;
using RDotNet.Internals;

namespace DAnTE.Tools
{
    public class RdnConnector
    {
        /// <summary>
        /// Connection to the R Engine
        /// </summary>
        /// <remarks>
        /// This is an unmanaged DLL.
        /// To access it, we need to reference NuGet package DynamicInterop by Jean-Michel Perraud
        /// The R.NET NuGet package should auto-download the correct version of DynamicInterop.dll (v0.8.1)
        /// </remarks>
        private static REngine _engine;

        private static void MakeEngine()
        {
            if (_engine == null)
            {
                REngine.SetEnvironmentVariables();
                _engine = REngine.GetInstance();
                _engine.Initialize();
            }
        }

        private static void KillEngine()
        {
            if (_engine != null)
            {
                // you should always dispose of the REngine properly.
                // After disposing of the engine, you cannot reinitialize nor reuse it
                _engine.Dispose();
            }
        }

        public void Init(string s)
        {
            MakeEngine();
            clsRCmdLog.Init();
            clsRCmdLog.EnableCommentLog = false;
            clsRCmdLog.EnableTrace = true;
            clsRCmdLog.LogRComment(string.Format("Init:{0}", s));
        }

        public void EvaluateNoReturn(string rcmd)
        {
            try
            {
                if (_engine == null)
                {
                    throw new Exception("Could not connect to R");
                }
                var res = _engine.Evaluate(rcmd);
                if (res.Type == SymbolicExpressionType.CharacterVector)
                {
                    //var a = res.AsCharacter().ToArray();
                    //s = string.Join("||", a);
                }
                clsRCmdLog.LogRCommand(rcmd);
            }
            catch (Exception ex)
            {
                // Note that exception "Value cannot be null" tends to occur if we try to load a session file (.dnt file) using a separate thread
                clsRCmdLog.LogRComment(string.Format("EvaluateNoReturn ERROR {0} -> {1}", rcmd, ex.Message));
                throw;
            }
        }

        private SymbolicExpression GetSym(string name)
        {
            SymbolicExpression sym;
            try
            {
                sym = _engine.GetSymbol(name);
                clsRCmdLog.LogRComment(string.Format("RdnConnectorClass.GetSym:{0} -> {1}", name, sym.Type.ToString()));
            }
            catch (Exception ex)
            {
                clsRCmdLog.LogRComment(string.Format("GetSym:{0} -> error:{1}", name, ex.Message));
                throw;
            }
            return sym;
        }

        public bool GetSymbolAsBool(string name)
        {
            var sym = GetSym(name);
            var i = sym.AsLogical();
            return i[0];
        }

        public string[] GetSymbolAsStrings(string name)
        {
            var sym = GetSym(name);
            return sym.AsVector().AsCharacter().ToArray();
        }

        public string[,] GetSymbolAsStringMatrix(string name)
        {
            var sym = GetSym(name);
            return CharacterMatrixToStringArray(sym.AsCharacterMatrix());
        }

        public double[] GetSymbolAsNumbers(string name)
        {
            var sym = GetSym(name);
            return sym.AsVector().AsNumeric().ToArray();
        }

        public double[,] GetSymbolAsNumberMatrix(string name)
        {
            var sym = GetSym(name);
            return sym.AsNumericMatrix().ToArray();
        }

        public void SetSymbolNumberMatrix(string name, double[,] value)
        {
            clsRCmdLog.LogRComment(string.Format("RdnConnectorClass.SetSymbolNumberMatrix:{0}", name));
            var sym = _engine.CreateNumericMatrix(value);
            _engine.SetSymbol(name, sym);
        }

        public void SetSymbolCharVector(string name, string[] value)
        {
            clsRCmdLog.LogRComment(string.Format("RdnConnectorClass.SetSymbolCharVector:{0}", name));
            var sym = _engine.CreateCharacterVector(value);
            _engine.SetSymbol(name, sym);
        }

        public void SetSymbolCharMatrix(string name, string[,] value)
        {
            clsRCmdLog.LogRComment(string.Format("RdnConnectorClass.SetSymbolCharMatrix:{0}", name));
            var sym = _engine.CreateCharacterMatrix(value);
            _engine.SetSymbol(name, sym);
        }

        public void SetSymbol(string name, object value)
        {
        }

        /* //-//
            public void SetCharacterOutputDevice(IStatConnectorCharacterDevice dev)
            {
              Console.WriteLine(string.Format("RdnConnectorClass.SetCharacterOutputDevice:{0}", dev.ToString()));
            }
        */

        public void Close()
        {
            clsRCmdLog.LogRComment("Close");
            KillEngine();
        }

        #region Utility Methods

        /// <summary>
        /// Returns 2D string array from given R.Net CharacterMatrix
        /// (this is a patch because RDotNet.CharacterMatrix.GetArrayFast is not implemented)
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        private static string[,] CharacterMatrixToStringArray(CharacterMatrix mat)
        {
            var r = mat.RowCount;
            var c = mat.ColumnCount;
            var z = new string[r, c];
            for (var ri = 0; ri < r; ri++)
            {
                for (var ci = 0; ci < c; ci++)
                {
                    z[ri, ci] = mat[ri, ci];
                }
            }
            return z;
        }

        #endregion
    }
}