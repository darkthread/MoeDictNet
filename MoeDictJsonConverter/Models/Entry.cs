using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkthread.MoeDict
{
    /// <summary>
    /// 字詞條
    /// </summary>
    [Serializable]
    public class Entry
    {
        /// <summary>
        /// 發音
        /// </summary>
        public Heteronym[] Heteronyms { get; set; }
        /// <summary>
        /// 字詞條名稱
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 部首
        /// </summary>
        public string Radical { get; set; }
        /// <summary>
        /// 去部首筆畫
        /// </summary>
        public byte NonRadicalStrokes { get; set; }
        /// <summary>
        /// 總筆畫
        /// </summary>
        public byte Strokes { get; set; }
        #region JSON
        public Entry(JObject jo)
        {
            foreach (var p in jo.Properties())
            {
                switch (p.Name)
                {
                    case "heteronyms":
                        Heteronyms = ((JArray)p.Value).Select(o => new Heteronym(o.Value<JObject>())).ToArray();
                        break;
                    case "title":
                        Title = Util.GetString(p);
                        break;
                    case "radical":
                        Radical = Util.GetString(p);
                        break;
                    case "non_radical_stroke_count":
                        NonRadicalStrokes = byte.Parse(Util.GetString(p));
                        break;
                    case "stroke_count":
                        Strokes = byte.Parse(Util.GetString(p));
                        break;
                    default:
                        throw new NotImplementedException();
                }

            }
        }
        #endregion

    }
}
