using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bee
{
    public class BeeWriter
    {
        private List<BeeSingle> singles = new List<BeeSingle>();

        public void AddSingle(BeeSingle e)
        {
            if(!string.IsNullOrEmpty(e.localName) && !string.IsNullOrEmpty(e.localName)) singles.Add(e);
        }

        private byte[] GetObjectBytes(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                bf.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        private string processData(object arg)
        {
            string outputString = "";

            foreach(byte e in GetObjectBytes(arg))
            {
                outputString = outputString + Convert.ToString(e) + ", ";
            }

            return outputString;
        }

        public void SaveFile(string filePath)
        {
            string tab = "	";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("[");

                foreach (BeeSingle e in singles)
                {
                    writer.WriteLine(tab + "{");

                    writer.WriteLine(tab + tab + "(name:[" + '"' + e.localName.ToLower() + '"' + "])");

                    writer.WriteLine(tab + tab + tab + "(data:[" + processData(e.localData) + "])");

                    writer.WriteLine(tab + "},");
                }


                writer.WriteLine("]");

                writer.Close();
            }
        }
    }
}
