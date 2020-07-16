using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace sorte.console
{
    class Program
    {
        static void Main(string[] args)
        {

            var downloadFile = getFileFromURL("http://www1.caixa.gov.br/loterias/_arquivos/loterias/D_megase.zip", "Megasena");
            string fileName = downloadFile.Result;

            using ZipArchive archive = ZipFile.OpenRead(fileName);
            var extractPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            extractPath += "\\extract";
            if (!Directory.Exists(extractPath))
            {
                Directory.CreateDirectory(extractPath);
            }

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".htm", StringComparison.OrdinalIgnoreCase))
                {
                    string fileExt = Path.GetExtension(entry.FullName);
                    string fileNoExt = Path.GetFileNameWithoutExtension(entry.FullName);
                    string destinationPath = Path.GetFullPath(Path.Combine(extractPath, string.Format("{0}{1}{2}", fileNoExt, DateTime.Now.ToString("yyyyMMddHHmmss"), fileExt)));


                    if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                        entry.ExtractToFile(destinationPath);

                    extractNumbersFromFile(destinationPath);

                }
            }
        }

        static async Task<string> getFileFromURL(string url, string filename)
        {
            var downloadFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            downloadFolder += "\\download";
            if (!Directory.Exists(downloadFolder))
            {
                Directory.CreateDirectory(downloadFolder);
            }

            filename += string.Format("{0}.zip",
                            DateTime.Now.ToString("yyyyMMddHHmmss")
                        );

            filename = downloadFolder + "\\" + filename;

            using var client = new HttpClient();
            try
            {
                Console.WriteLine(string.Format("Baixando o arquivo {0} de hoje ...", filename));
                HttpResponseMessage response = await client.GetAsync(url);
                string zip = await response.Content.ReadAsStringAsync();
                byte[] xmlByteArray = await response.Content.ReadAsByteArrayAsync();
                using (Stream xmlStream = await response.Content.ReadAsStreamAsync()) { }
                File.WriteAllBytes(filename, xmlByteArray);
                Console.WriteLine("Sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return filename;
        }


        static List<string> scrap = new List<string>();

        static private void extractNumbersFromFile(string filename)
        {

            var htmlDoc = new HtmlDocument();
            var enc = htmlDoc.DetectEncoding(filename);
            htmlDoc.Load(filename, enc);
            var webtrs = htmlDoc.DocumentNode.SelectNodes("//tr");
            int count = 1;
            StringBuilder sbLine;
            var Megasenas = new List<MegasenaRecord>();
            foreach (var trs in webtrs)
            {
                var webtds = trs.SelectNodes("//td");
                count++;
                sbLine = new StringBuilder();
                int cols = 0;
                var record = new MegasenaRecord();
                int concursoCorrente = 0;
                int i = 0;
                double db;
                MegasenaCidadeRecord cidade = new MegasenaCidadeRecord();
                foreach (var tds in webtds)
                {
                    string text = tds.InnerText;
                    switch (cols)
                    {
                        case 0: // Concurso
                            if (int.TryParse(text, out i))
                            {
                                record.Concurso = i;
                                if (concursoCorrente != i)
                                {
                                    concursoCorrente = i;
                                }
                                else
                                {
                                    i = 0;
                                }
                            }
                            else
                            {
                                i = 0;
                            }
                            break;
                        //Data Sorteio
                        case 1: if (DateTime.TryParse(text, out DateTime dt)) { record.DataConcurso = dt; } break;
                        //1ª Dezena
                        case 2: if (int.TryParse(text, out i)) { record.NumerosSorteados.Add(new NumeroRecord() { Numero = i, Ordem = 1 }); } break;
                        //2ª Dezena
                        case 3: if (int.TryParse(text, out i)) { record.NumerosSorteados.Add(new NumeroRecord() { Numero = i, Ordem = 2 }); } break;
                        //3ª Dezena
                        case 4: if (int.TryParse(text, out i)) { record.NumerosSorteados.Add(new NumeroRecord() { Numero = i, Ordem = 3 }); } break;
                        //4ª Dezena
                        case 5: if (int.TryParse(text, out i)) { record.NumerosSorteados.Add(new NumeroRecord() { Numero = i, Ordem = 4 }); } break;
                        //5ª Dezena
                        case 6: if (int.TryParse(text, out i)) { record.NumerosSorteados.Add(new NumeroRecord() { Numero = i, Ordem = 5 }); } break;
                        //6ª Dezena
                        case 7: if (int.TryParse(text, out i)) { record.NumerosSorteados.Add(new NumeroRecord() { Numero = i, Ordem = 6 }); } break;
                        //Arrecadacao_Total
                        case 8: if (double.TryParse(text, out db)) { record.ArredacaoTotal = db; } break;
                        //Ganhadores_Sena
                        case 9: if (int.TryParse(text, out i)) { record.Ganhadores = i; } break;
                        //Cidade
                        case 10:
                            cidade = new MegasenaCidadeRecord
                            {
                                Cidade = text
                            }; break;
                        //UF
                        case 11: cidade.UF = text; record.Cidades.Add(cidade); break;
                        //Rateio_Sena
                        case 12: if (double.TryParse(text, out db)) { record.Rateio = db; } break;
                        //Ganhadores_Quina
                        case 13: if (int.TryParse(text, out i)) { record.GanhadoresQuina = i; } break;
                        //Rateio_Quina
                        case 14: if (double.TryParse(text, out db)) { record.RateioQuadra = db; } break;
                        //Ganhadores_Quadra
                        case 15: if (int.TryParse(text, out i)) { record.GanhadoresQuadra = i; } break;
                        //Rateio_Quadra
                        case 16: if (double.TryParse(text, out db)) { record.RateioQuadra = db; } break;
                        //Acumulado
                        case 17: record.Acumulado = text; break;
                        //Valor_Acumulado
                        case 18: if (double.TryParse(text, out db)) { record.ValorAcumulado = db; } break;
                        //Estimativa_Prêmio
                        case 19: if (double.TryParse(text, out db)) { record.EstimativaPremio = db; } break;
                        //Acumulado_Mega_da_Virada
                        case 20: if (double.TryParse(text, out db)) { record.AcumuladoVirada = db; } break;
                    }
                    cols++;
                    scrap.Add(text);
                    sbLine.Append(text + "; ");
                    if (cols == 21)
                    {
                        if (i != 0)
                        {
                            Megasenas.Add(record);
                        }
                        else
                        {
                            //Cidades apenas
                        }
                        count++;
                        cols = 0;
                        sbLine = new StringBuilder();
                        if (cols <= 10)
                        {
                            record = new MegasenaRecord();
                        }
                    }
                }
                Console.WriteLine(string.Format("{0}-{1}", count, sbLine.ToString()));
                if (count > 10) { break; }
            }

            htmlDoc.OptionAutoCloseOnEnd = true;
        }

        class MegasenaRecord
        {
            public int Id { get; set; }

            public int Concurso { get; set; }

            public DateTime DataConcurso { get; set; }

            public double ArredacaoTotal { get; set; }

            public int Ganhadores { get; set; }

            public double Rateio { get; set; }

            public int GanhadoresQuina { get; set; }

            public double RateioQuina { get; set; }

            public int GanhadoresQuadra { get; set; }

            public double RateioQuadra { get; set; }

            public string Acumulado { get; set; }

            public double ValorAcumulado { get; set; }

            public double EstimativaPremio { get; set; }

            public double AcumuladoVirada { get; set; }

            public DateTime DataInclusao { get; set; } = DateTime.Now;

            public IList<NumeroRecord> NumerosSorteados { get; set; } = new List<NumeroRecord>();
            public IList<MegasenaCidadeRecord> Cidades { get; set; } = new List<MegasenaCidadeRecord>();

        }

        public class NumeroRecord
        {
            public int Id { get; set; }

            public int Numero { get; set; }

            public int Ordem { get; set; }

        }

        public class MegasenaCidadeRecord
        {

            public int Id { get; set; }

            public int MegasenaId { get; set; }

            public string Cidade { get; set; }

            public string UF { get; set; }

        }

    }
}
