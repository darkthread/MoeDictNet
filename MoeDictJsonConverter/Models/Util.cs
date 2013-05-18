using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Darkthread.MoeDict
{
    #region Tools
    public static class Util
    {
        public static string GetString(JProperty p)
        {
            return (string)p.Value;
        }
        public static string[] GetStringArray(JProperty p)
        {
            return ((JArray)p.Value).Select(o => o.Value<string>()).ToArray();
        }
        //REF: https://github.com/g0v/moedict-epub sym.txt
        public static Dictionary<string, string> UnicodeDict = new Dictionary<string, string>();
        /// <summary>
        /// 將{[8e79]}格式換成Unicode字元
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static string ConvToUnicode(string raw)
        {
            if (!raw.Contains("{[")) return raw;
            return Regex.Replace(raw, @"{\[(?<c>[0-9a-f]{4})\]}", (m) =>
            {
                string code = m.Groups["c"].Value;
                if (UnicodeDict.ContainsKey(code))
                    return UnicodeDict[code];
                return m.Value;
            });
        }

    } 
    #endregion
}