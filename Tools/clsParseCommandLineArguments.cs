using System;
using System.Collections.Generic;
using System.Text;

namespace DAnTE.Tools
{
    public class ProgramArguments
    {
        public string DNTfilename { get; private set; }

        public string LOGfilename { get; private set; }

        public bool ShowHelp { get; private set; }

        public ProgramArguments()
        {
            DNTfilename = null;
            LOGfilename = null;
            ShowHelp = false;
        }

        /// <summary>
        /// Parse the command line arguments
        /// </summary>
        /// <param name="args"></param>
        public ProgramArguments(string[] args)
            : this()
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                if (arg[0] != '-' && arg[0] != '/')
                    throw new ArgumentException(string.Concat("Invalid argument '", arg, "'"));

                switch (arg.TrimStart('-', '/').ToLower())
                {
                    case "f":
                        DNTfilename = TextArgument(args, ref i);
                        break;
                    case "l":
                        LOGfilename = TextArgument(args, ref i);
                        break;
                    case "?":
                    case "help":
                        ShowHelp = true;
                        break;
                }
            }
        }

        private string TextArgument(string[] args, ref int i)
        {
            if (args.Length < i + 2) 
                throw new ArgumentException();

            string value = args[++i];

            if (value[0] == '-' || value[0] == '/') 
                throw new ArgumentException();

            return value;
        }
    }
}
