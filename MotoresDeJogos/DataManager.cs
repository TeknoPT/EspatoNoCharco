using System.Xml;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace MotoresDeJogos
{
    [Serializable]
    public struct Data
    {
        public int score;
        public int level;
    }

    class DataManager
    {
        // This file will be saved on "MotoresDeJogos\bin\Windows\x86\Debug" folder
        static string _fileName = "saveData.xml";

        public static Data Load()
        {
            if (!File.Exists(_fileName))
                return new Data()
                {
                    score = 0, level = 0
                };
            
            using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serilizer = new XmlSerializer(typeof(Data));

                Data data = (Data)serilizer.Deserialize(reader);

                return data;
            }
        }

        public static void Save(Data data)
        {
            using (var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(Data));

                serilizer.Serialize(writer, data);
            }
        }
    }
}
