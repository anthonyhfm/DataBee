using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bee
{
    public class BeeReader
    {

        private string localPath { get; set; }

        public BeeReader(string path)
        {
            localPath = path;
        }

        public object DataToObject(byte[] args)
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                ms.Write(args, 0, args.Length);
                ms.Seek(0, SeekOrigin.Begin);

                return bf.Deserialize(ms);
            }
        }

        public object GetObject(string objectName)
        {
            string content = File.ReadAllText(localPath);

            string[] fileLines = content.Split('\n');

            int i = 0;

            foreach (string e in fileLines)
            {
                if(fileLines[i].Contains("name:[".ToLower()))
                {
                    string line = fileLines[i].ToLower();

                    if (fileLines[i].Contains($"name:[{'"'}{objectName.ToLower()}{'"'}]"))
                    {
                        string nextLine = fileLines[i + 1];

                        if (nextLine.Contains("data:".ToLower()))
                        {
                            nextLine = nextLine.Replace("(data:", "");
                            nextLine = nextLine.Replace(")", "");
                            nextLine = nextLine.Replace("[", "");
                            nextLine = nextLine.Replace("]", "");
                            nextLine = nextLine.Replace(" ", "");

                            string[] contents = nextLine.Split();

                            string[] data = contents[3].Split(',');

                            List<byte> byteList = new List<byte>();

                            foreach(string s in data)
                            {
                                try
                                {
                                    byteList.Add(Convert.ToByte(s));
                                }
                                catch { }
                            }

                            return DataToObject(byteList.ToArray());
                        }
                    }
                }

                i++;
            }

            return 0;
        }

    }
}
