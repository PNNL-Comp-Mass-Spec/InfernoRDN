using System;
using System.Collections.Generic;
using System.Text;

namespace DAnTE.Tools
{
    public class ProgramArguments
    {
        private string dntFilename;
        private string logFilename;

        public string DNTfilename
        {
            get { return dntFilename; }
        }

        public string LOGfilename
        {
            get { return logFilename; }
        }

        public ProgramArguments()
        {
            dntFilename = null;
            logFilename = null;
        }

        public ProgramArguments(string[] args) : this()
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                if (arg[0] != '-' && arg[0] != '/')
                    throw new ArgumentException(String.Concat("Invalid argument '", arg, "'"));

                switch (arg.TrimStart('-', '/'))
                {
                    case "f":
                        dntFilename = TextArgument(args, ref i);
                        break;
                    case "l":
                        logFilename = TextArgument(args, ref i);
                        break;
                }
            }
        }

        private string TextArgument(string[] args, ref int i)
        {
            if (args.Length < i + 2) throw new ArgumentException();
            string value = args[++i];

            if (value[0] == '-' || value[0] == '/') throw new ArgumentException();
            return value;
        }
    }
}
