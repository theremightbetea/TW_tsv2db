using Microsoft.VisualBasic;

namespace EtruscanUnitDb
{
        public class ReadCSV
        {
                public string[][] content;
                public ReadCSV(string filePath)
                { 
                        try{
                                content = File.ReadLines(filePath).Select(x => x.Split('\t')).ToArray();
                        }
                        catch(Exception e)
                        {
                                Console.WriteLine(e.ToString());
                        }
                }
        }
}