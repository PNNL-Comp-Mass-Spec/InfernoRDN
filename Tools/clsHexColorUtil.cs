using System;
using System.Drawing;

namespace DAnTE.Tools
{
    static class clsHexColorUtil
        //
        //	Util class to convert hexadecimal represenations of colors used in html 
        //  to .NET framework colors. Hexadecimal colors are defined in this notation:
        //  #8080FF
        //
        //	This code is free of copyrights and can be redistributed freely
        //
        //	Written by Sandro Todesco, 2002, sandro@todesco.com, 
        //	http://www.todesco.com
        //
    {
        public static string ReverseString(string inStr)
            //  Helper Method that reverses a string.
        {
            int counter;
            var outStr = "";
            for (counter = inStr.Length - 1; counter >= 0; counter--)
            {
                outStr = outStr + inStr[counter];
            }
            return outStr;
        }

        public static int HexToInt(string hexstr)
            //  This method converts a hexvalues string as 80FF into a integer.
            //	Note that you may not put a '#' at the beginning of string! There
            //  is not much error checking in this method. If the string does not
            //  represent a valid hexadecimal value it returns 0.
        {
            int counter;
            var hexint = 0;
            hexstr = hexstr.ToUpper();
            var hexarr = hexstr.ToCharArray();
            for (counter = hexarr.Length - 1; counter >= 0; counter--)
            {
                if ((hexarr[counter] >= '0') && (hexarr[counter] <= '9'))
                {
                    hexint += (hexarr[counter] - 48) * ((int)(Math.Pow(16, hexarr.Length - 1 - counter)));
                }
                else
                {
                    if ((hexarr[counter] >= 'A') && (hexarr[counter] <= 'F'))
                    {
                        hexint += (hexarr[counter] - 55) * ((int)(Math.Pow(16, hexarr.Length - 1 - counter)));
                    }
                    else
                    {
                        hexint = 0;
                        break;
                    }
                }
            }
            return hexint;
        }

        public static string IntToHex(int hexint)
            //  This method converts a integer into a hexadecimal string representing the
            //  int value. The returned string will look like this: 55FF. Note that there is
            //  no leading '#' in the returned string! 
        {
            var counter = 1;
            var hexstr = "";
            while (hexint + 15 > Math.Pow(16, counter - 1))
            {
                var reminder = (int)(hexint % Math.Pow(16, counter));
                reminder = (int)(reminder / Math.Pow(16, counter - 1));
                if (reminder <= 9)
                {
                    hexstr = hexstr + (char)(reminder + 48);
                }
                else
                {
                    hexstr = hexstr + (char)(reminder + 55);
                }
                hexint -= reminder;
                counter++;
            }
            return ReverseString(hexstr);
        }

        public static string IntToHex(int hexint, int length)
            //  This version of the IntToHex method returns a hexadecimal string representing the
            //  int value in the given minimum length. If the hexadecimal string is shorter then the
            //  length parameter the missing characters will be filled up with leading zeroes.
            //  Note that the returend string though is not truncated if the value exeeds the length!
        {
            var hexstr = IntToHex(hexint);
            var ret = "";
            if (hexstr.Length < length)
            {
                int counter;
                for (counter = 0; counter < (length - hexstr.Length); counter++)
                {
                    ret = ret + "0";
                }
            }
            return ret + hexstr;
        }

        public static Color HexToColor(string hexString)
            //  Translates a html hexadecimal definition of a color into a .NET Framework Color.
            //  The input string must start with a '#' character and be followed by 6 hexadecimal
            //  digits. The digits A-F are not case sensitive. If the conversion was not successfull
            //  the color white will be returned.
        {
            Color actColor;
            if (hexString.StartsWith("#") && (hexString.Length == 7))
            {
                var r = HexToInt(hexString.Substring(1, 2));
                var g = HexToInt(hexString.Substring(3, 2));
                var b = HexToInt(hexString.Substring(5, 2));
                actColor = Color.FromArgb(r, g, b);
            }
            else
            {
                actColor = Color.White;
            }
            return actColor;
        }

        public static string ColorToHex(Color actColor)
            //  Translates a .NET Framework Color into a string containing the html hexadecimal 
            //  representation of a color. The string has a leading '#' character that is followed 
            //  by 6 hexadecimal digits. 
        {
            return "#" + IntToHex(actColor.R, 2) + IntToHex(actColor.G, 2) + IntToHex(actColor.B, 2);
        }
    }
}