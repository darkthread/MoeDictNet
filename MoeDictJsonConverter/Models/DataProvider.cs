using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Darkthread.MoeDict
{
    [Serializable]
    public class PosInfo
    {
        public string Title;
        public int Pos;
        public int Len;
    }

    public class DataProvider
    {
 
        FileStream fs;
        BinaryFormatter bf = new BinaryFormatter();
        public List<PosInfo> Index;
        public DataProvider(string dataFile, string indexFile)
        {
            fs = new FileStream(dataFile, FileMode.Open);
            using (FileStream fsIdx = new FileStream(indexFile, FileMode.Open))
            {
                Index = bf.Deserialize(fsIdx) as List<PosInfo>;
            }
        }

        public void Close()
        {
            fs.Close();
        }

        public Entry Read(PosInfo pos)
        {
            fs.Seek(pos.Pos, SeekOrigin.Begin);
            byte[] buff = new byte[pos.Len];
            fs.Read(buff, 0, buff.Length);
            MemoryStream ms = new MemoryStream(buff);
            return (Entry)bf.Deserialize(ms);
        }

        public static void Save(List<Entry> dict, string dataFile, string indexFile)
        {
            List<PosInfo> lst = new List<PosInfo>();
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(dataFile, FileMode.Create))
            {
                foreach (var ent in dict)
                {
                    MemoryStream ms = new MemoryStream();
                    bf.Serialize(ms, ent);
                    byte[] buff = ms.ToArray();
                    PosInfo pos = new PosInfo();
                    pos.Title = ent.Title;
                    pos.Pos = (int)fs.Position;
                    pos.Len = buff.Length;
                    fs.Write(buff, 0, buff.Length);
                    lst.Add(pos);
                }
            }
            using (FileStream fs = new FileStream(indexFile, FileMode.Create))
            {
                bf.Serialize(fs, lst);
            }
        }
    }
}
