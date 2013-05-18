using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkthread.MoeDict
{
    /// <summary>
    /// 解釋
    /// </summary>
    [Serializable]
    public class Definition
    {
        /// <summary>
        /// 解釋
        /// </summary>
        public string Def { get; set; }
        /// <summary>
        /// 詞性
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 例句
        /// </summary>
        public string[] Examples { get; set; }
        /// <summary>
        /// 典藉例句
        /// </summary>
        public string[] Quotes { get; set; }
        /// <summary>
        /// 類語
        /// </summary>
        public string[] Synonyms { get; set; }
        /// <summary>
        /// 反義辭
        /// </summary>
        public string[] Antonyms { get; set; }
        /// <summary>
        /// 連結
        /// </summary>
        public string[] Links { get; set; }
        #region JSON
        public Definition(JObject jo)
        {
            foreach (var p in jo.Properties())
            {
                switch (p.Name)
                {
                    case "def":
                        Def = Util.GetString(p);
                        break;
                    case "example":
                        Examples = Util.GetStringArray(p);
                        break;
                    case "quote":
                        Quotes = Util.GetStringArray(p);
                        break;
                    case "synonyms":
                        Synonyms = Util.GetString(p).Split(',');
                        break;
                    case "antonyms":
                        Antonyms = Util.GetString(p).Split(',');
                        break;
                    case "type":
                        Type = Util.GetString(p);
                        break;
                    case "link":
                        Links = Util.GetStringArray(p);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        #endregion
    } 
}
