using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gwrsyncronizer.Model;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using NLog;


namespace gwrsyncronizer.DataReader
{
    public class ReadFileData
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public List<Gwr> ReadFileCsv(string file)
        {
            //logger.Info("Read {0}", file);

            var reader = new StreamReader(File.OpenRead(file));
            List<Gwr> gwrList = new List<Gwr>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var lineValues = line.Split(';');
                gwrList.Add(new Gwr
                {
                    EGID = lineValues[0],
                    EDID = lineValues[1],
                    GDEKT = lineValues[2],
                    GDENR = lineValues[3],
                    GDENAME = lineValues[4],
                    STRNAME = lineValues[5],
                    DEINR = lineValues[6],
                    PLZ4 = lineValues[7],
                    PLZZ = lineValues[8],
                    PLZNAME = lineValues[9],
                    GKODE = lineValues[10],
                    GKODN = lineValues[11],
                    STRSP = lineValues[12]
                });
            }
            return gwrList;
        }

        public string ReadFilePdf(string file)
        {
            //logger.Info("Read {0}", file);
            using (PdfReader reader = new PdfReader(file))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }
    }
}
