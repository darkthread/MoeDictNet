using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkthread.MoeDict
{
    /// <summary>
    /// 發音
    /// </summary>
    [Serializable]
    public class Heteronym
    {
        /// <summary>
        /// 注音
        /// </summary>
        public string Bopomofo { get; set; }
        /// <summary>
        /// 注音二式
        /// </summary>
        public string Bopomofo2 { get; set; }
        /// <summary>
        /// 漢語拼音
        /// </summary>
        public string Pinyin { get; set; }
        /// <summary>
        /// 解釋
        /// </summary>
        public Definition[] Definitions { get; set; }
        #region JSON
        public Heteronym(JObject jo)
        {
            foreach (var p in jo.Properties())
            {
                switch (p.Name)
                {
                    case "bopomofo":
                        Bopomofo = Util.GetString(p);
                        break;
                    case "bopomofo2":
                        Bopomofo2 = Util.GetString(p);
                        break;
                    case "pinyin":
                        Pinyin = Util.GetString(p);
                        break;
                    case "definitions":
                        Definitions = ((JArray)p.Value).Select(o => new Definition(o.Value<JObject>())).ToArray();
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        #endregion
    }
}
