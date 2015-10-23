using System;

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
            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];

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
                    default:
                        if (arg.ToUpper().StartsWith("/L:"))
                            LOGfilename = TextArgument(arg);
                        else if (arg.ToUpper().StartsWith("/F:"))
                            DNTfilename = TextArgument(arg);

                        break;

                }
            }
        }

        private string TextArgument(string argument)
        {
            var value = argument.Substring(3);
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }

        private string TextArgument(string[] args, ref int i)
        {
            if (args.Length < i + 2) 
                throw new ArgumentException();

            var value = args[++i];

            if (value[0] == '-' || value[0] == '/') 
                throw new ArgumentException();

            return value;
        }
    }
}
