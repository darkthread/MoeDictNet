using Darkthread.MoeDict;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MoeDictJsonConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            const string indexFile = "dict.index";
            const string dataFile = "dict.data";

            if (args.Length == 2)
            {
                //讀入sym.txt建立對照表
                //args[0] => sym.txt路徑
                foreach (string line in File.ReadAllLines(args[0]))
                {
                    string[] p = line.Split(' ');
                    Util.UnicodeDict.Add(p[0], p[1]);
                }

                JArray entries = JsonConvert.DeserializeObject<JArray>(
                    //置換{[8e79]}格式
                    Util.ConvToUnicode(
                    //args[1] => dict-revised.json路徑
                        File.ReadAllText(args[1])
                    ));
                List<Entry> dict = new List<Entry>();
                //處理重複文字
                Dictionary<string, int> test = new Dictionary<string, int>();
                foreach (var o in entries)
                {
                    var entry = new Entry(o.Value<JObject>());
                    string title = entry.Title;
                    Console.WriteLine(title);
                    if (entry.Title.StartsWith("{["))
                    {
                        Console.WriteLine("{0}->{1}",
                            title,
                            entry.Heteronyms.First().Definitions.First().Def);
                        continue;
                    }
                    //若已存在，計數器加1
                    if (test.ContainsKey(title))
                    {
                        test[title]++;
                        entry.Title += "[" + test[title] + "]";
                    }
                    else
                        test.Add(title, 1);
                    dict.Add(entry);
                }
                DataProvider.Save(dict, dataFile, indexFile);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("國語辭典載入測試");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                DataProvider dp = new DataProvider(dataFile, indexFile);
                sw.Stop();
                Console.WriteLine("辭典資料共載入{0:N0}筆，耗時{1:N0}ms",
                    dp.Index.Count, sw.ElapsedMilliseconds);
                Console.WriteLine("請輸入字詞進行檢索，不輸入按Enter可離開");
                while (true)
                {
                    string q = Console.ReadLine();
                    if (string.IsNullOrEmpty(q)) break;
                    var res = dp.Index.Where(o => o.Title.StartsWith(q)).ToArray();
                    Console.WriteLine("找到{0}筆: {1}",
                        res.Length, string.Join(",", res.Take(10).Select(o => o.Title).ToArray()));
                    if (res.Count() > 0)
                    {
                        var ent = dp.Read(res.First());
                        Console.WriteLine("【{0}】", ent.Title);
                        Console.WriteLine(ent.Heteronyms.First().Definitions.First().Def);
                    }

                }
            }
        }
    }
}
