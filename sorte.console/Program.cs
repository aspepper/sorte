using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace sorte.console
{
    class Program
    {

        static private string barra = (Environment.OSVersion.Platform == PlatformID.Win32NT ? "\\" : "/");
        /// <summary>
        /// Executa rotinas para carga e cálculos estatísticos
        /// </summary>
        /// <param name="args">
        ///         load mega = Carga dos concursos da Megasena.
        ///         count = calcula a quantidade de cada dezena por concurso
        /// </param>
        static void Main(string[] args)
        {

            // Set Default Culture for DD/MM/YYYY
            CultureInfo ci = new CultureInfo("pt-BR");
            CultureInfo.CurrentCulture = ci;
            CultureInfo.CurrentUICulture = ci;

            if (args.Length == 0) { return; }

            switch (args[0])
            {
                case "load":
                    if (args[1] == "mega")
                    {
                        var downloadFile = GetFileFromURL("http://www1.caixa.gov.br/loterias/_arquivos/loterias/D_megase.zip", "Megasena");
                        string fileName = downloadFile.Result;

                        using ZipArchive archive = ZipFile.OpenRead(fileName);
                        var extractPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        extractPath += barra + "extract";
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

                                ExtractNumbersFromFile(destinationPath);

                            }
                        }
                    }
                    break;
                case "count":
                    CountNumbers();
                    break;
            }

        }

        #region "load mega"
        static async Task<string> GetFileFromURL(string url, string filename)
        {


            var downloadFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            downloadFolder += barra + "download";
            if (!Directory.Exists(downloadFolder))
            {
                Directory.CreateDirectory(downloadFolder);
            }

            filename += string.Format("{0}.zip",
                            DateTime.Now.ToString("yyyyMMddHHmmss")
                        );

            filename = downloadFolder + barra + filename;

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


        static private void ExtractNumbersFromFile(string filename)
        {

            var htmlDoc = new HtmlDocument();
            var enc = Encoding.GetEncoding(28591);
            htmlDoc.Load(filename, enc);
            var webtrs = htmlDoc.DocumentNode.SelectNodes("//tr");
            int count = 0;
            var Megasenas = new List<MegasenaRecord>();
            foreach (var trs in webtrs)
            {
                var webtds = trs.SelectNodes("//td");
                count++;
                int cols = 0;
                var record = new MegasenaRecord();
                int concursoCorrente = 0;
                int i = 0;
                double db;
                MegasenaCidadeRecord cidade = new MegasenaCidadeRecord();
                var maxCols = 21;
                foreach (var tds in webtds)
                {
                    string text = tds.InnerText.Replace("&nbsp", " ").Trim();

                    if (cols == 0 && !int.TryParse(text, out _)) { cols = 10; maxCols = 12; } // Cidade (Coluna)
                    if (cols == 1 && !DateTime.TryParse(text, out _)) { cols = 11; maxCols = 12; } // UF (Coluna)

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
                    if (cols == maxCols)
                    {
                        Megasenas.Add(record);
                        using var context = new SorteContext();
                        var recordMS = context
                            .Megasenas
                            .Include(e => e.Cidades)
                            .Include(e => e.NumerosSorteados)
                            .Where(e => e.Concurso == concursoCorrente).FirstOrDefault();
                        var AddFlag = recordMS == null;

                        if (AddFlag)
                        {
                            Console.WriteLine(string.Format("Processando Data {0} - Concurso {1}", record.DataConcurso, concursoCorrente));

                            recordMS = new Megasena()
                            {
                                DataConcurso = record.DataConcurso,
                                Concurso = record.Concurso,
                                ArredacaoTotal = record.ArredacaoTotal,
                                Ganhadores = record.Ganhadores,
                                Rateio = record.Rateio,
                                GanhadoresQuina = record.GanhadoresQuina,
                                RateioQuina = record.RateioQuina,
                                GanhadoresQuadra = record.GanhadoresQuadra,
                                RateioQuadra = record.RateioQuadra,
                                Acumulado = record.Acumulado,
                                ValorAcumulado = record.ValorAcumulado,
                                EstimativaPremio = record.EstimativaPremio,
                                AcumuladoVirada = record.AcumuladoVirada
                            };

                            foreach (var nrs in record.NumerosSorteados)
                            {
                                recordMS.NumerosSorteados.Add(new Sorteados()
                                {
                                    Numero = nrs.Numero,
                                    Ordem = nrs.Ordem
                                });
                            }

                        }

                        foreach (var cds in record.Cidades)
                        {
                            if (cds.Cidade.Trim().Length + cds.UF.Trim().Length > 0)
                            {
                                if (recordMS.Cidades.Where(e => e.Cidade == cds.Cidade && e.UF == cds.UF).FirstOrDefault() == null)
                                {
                                    recordMS.Cidades.Add(new MegasenaCidade()
                                    {
                                        Cidade = cds.Cidade,
                                        UF = cds.UF
                                    });
                                }
                            }
                        }

                        if (AddFlag) { context.Megasenas.Add(recordMS); }
                        else { context.Megasenas.Update(recordMS); }
                        context.SaveChanges();

                        count++;
                        cols = 0;
                        if (cols <= 10)
                        {
                            record = new MegasenaRecord();
                        }
                        maxCols = 21;
                    }
                }
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
        #endregion

        #region "count"

        private class MegasenaNumbers
        {
            public int Dez01 = 0;
            public int Dez02 = 0;
            public int Dez03 = 0;
            public int Dez04 = 0;
            public int Dez05 = 0;
            public int Dez06 = 0;
            public int Dez07 = 0;
            public int Dez08 = 0;
            public int Dez09 = 0;
            public int Dez10 = 0;
            public int Dez11 = 0;
            public int Dez12 = 0;
            public int Dez13 = 0;
            public int Dez14 = 0;
            public int Dez15 = 0;
            public int Dez16 = 0;
            public int Dez17 = 0;
            public int Dez18 = 0;
            public int Dez19 = 0;
            public int Dez20 = 0;
            public int Dez21 = 0;
            public int Dez22 = 0;
            public int Dez23 = 0;
            public int Dez24 = 0;
            public int Dez25 = 0;
            public int Dez26 = 0;
            public int Dez27 = 0;
            public int Dez28 = 0;
            public int Dez29 = 0;
            public int Dez30 = 0;
            public int Dez31 = 0;
            public int Dez32 = 0;
            public int Dez33 = 0;
            public int Dez34 = 0;
            public int Dez35 = 0;
            public int Dez36 = 0;
            public int Dez37 = 0;
            public int Dez38 = 0;
            public int Dez39 = 0;
            public int Dez40 = 0;
            public int Dez41 = 0;
            public int Dez42 = 0;
            public int Dez43 = 0;
            public int Dez44 = 0;
            public int Dez45 = 0;
            public int Dez46 = 0;
            public int Dez47 = 0;
            public int Dez48 = 0;
            public int Dez49 = 0;
            public int Dez50 = 0;
            public int Dez51 = 0;
            public int Dez52 = 0;
            public int Dez53 = 0;
            public int Dez54 = 0;
            public int Dez55 = 0;
            public int Dez56 = 0;
            public int Dez57 = 0;
            public int Dez58 = 0;
            public int Dez59 = 0;
            public int Dez60 = 0;
        }

        static private void CountNumbers()
        {
            var megasenaDezenas = new MegasenaNumbers();

            Console.WriteLine("Inicio do Processo de Count...");
            using var context = new SorteContext();
            foreach (var ms in context.Megasenas.Include(m => m.NumerosSorteados).OrderBy(e => e.DataConcurso))
            {
                foreach (var numero in ms.NumerosSorteados)
                {
                    if (numero.Numero == 1) { megasenaDezenas.Dez01++; }
                    if (numero.Numero == 2) { megasenaDezenas.Dez02++; }
                    if (numero.Numero == 3) { megasenaDezenas.Dez03++; }
                    if (numero.Numero == 4) { megasenaDezenas.Dez04++; }
                    if (numero.Numero == 5) { megasenaDezenas.Dez05++; }
                    if (numero.Numero == 6) { megasenaDezenas.Dez06++; }
                    if (numero.Numero == 7) { megasenaDezenas.Dez07++; }
                    if (numero.Numero == 8) { megasenaDezenas.Dez08++; }
                    if (numero.Numero == 9) { megasenaDezenas.Dez09++; }
                    if (numero.Numero == 10) { megasenaDezenas.Dez10++; }
                    if (numero.Numero == 11) { megasenaDezenas.Dez11++; }
                    if (numero.Numero == 12) { megasenaDezenas.Dez12++; }
                    if (numero.Numero == 13) { megasenaDezenas.Dez13++; }
                    if (numero.Numero == 14) { megasenaDezenas.Dez14++; }
                    if (numero.Numero == 15) { megasenaDezenas.Dez15++; }
                    if (numero.Numero == 16) { megasenaDezenas.Dez16++; }
                    if (numero.Numero == 17) { megasenaDezenas.Dez17++; }
                    if (numero.Numero == 18) { megasenaDezenas.Dez18++; }
                    if (numero.Numero == 19) { megasenaDezenas.Dez19++; }
                    if (numero.Numero == 20) { megasenaDezenas.Dez20++; }
                    if (numero.Numero == 21) { megasenaDezenas.Dez21++; }
                    if (numero.Numero == 22) { megasenaDezenas.Dez22++; }
                    if (numero.Numero == 23) { megasenaDezenas.Dez23++; }
                    if (numero.Numero == 24) { megasenaDezenas.Dez24++; }
                    if (numero.Numero == 25) { megasenaDezenas.Dez25++; }
                    if (numero.Numero == 26) { megasenaDezenas.Dez26++; }
                    if (numero.Numero == 27) { megasenaDezenas.Dez27++; }
                    if (numero.Numero == 28) { megasenaDezenas.Dez28++; }
                    if (numero.Numero == 29) { megasenaDezenas.Dez29++; }
                    if (numero.Numero == 30) { megasenaDezenas.Dez30++; }
                    if (numero.Numero == 31) { megasenaDezenas.Dez31++; }
                    if (numero.Numero == 32) { megasenaDezenas.Dez32++; }
                    if (numero.Numero == 33) { megasenaDezenas.Dez33++; }
                    if (numero.Numero == 34) { megasenaDezenas.Dez34++; }
                    if (numero.Numero == 35) { megasenaDezenas.Dez35++; }
                    if (numero.Numero == 36) { megasenaDezenas.Dez36++; }
                    if (numero.Numero == 37) { megasenaDezenas.Dez37++; }
                    if (numero.Numero == 38) { megasenaDezenas.Dez38++; }
                    if (numero.Numero == 39) { megasenaDezenas.Dez39++; }
                    if (numero.Numero == 40) { megasenaDezenas.Dez40++; }
                    if (numero.Numero == 41) { megasenaDezenas.Dez41++; }
                    if (numero.Numero == 42) { megasenaDezenas.Dez42++; }
                    if (numero.Numero == 43) { megasenaDezenas.Dez43++; }
                    if (numero.Numero == 44) { megasenaDezenas.Dez44++; }
                    if (numero.Numero == 45) { megasenaDezenas.Dez45++; }
                    if (numero.Numero == 46) { megasenaDezenas.Dez46++; }
                    if (numero.Numero == 47) { megasenaDezenas.Dez47++; }
                    if (numero.Numero == 48) { megasenaDezenas.Dez48++; }
                    if (numero.Numero == 49) { megasenaDezenas.Dez49++; }
                    if (numero.Numero == 50) { megasenaDezenas.Dez50++; }
                    if (numero.Numero == 51) { megasenaDezenas.Dez51++; }
                    if (numero.Numero == 52) { megasenaDezenas.Dez52++; }
                    if (numero.Numero == 53) { megasenaDezenas.Dez53++; }
                    if (numero.Numero == 54) { megasenaDezenas.Dez54++; }
                    if (numero.Numero == 55) { megasenaDezenas.Dez55++; }
                    if (numero.Numero == 56) { megasenaDezenas.Dez56++; }
                    if (numero.Numero == 57) { megasenaDezenas.Dez57++; }
                    if (numero.Numero == 58) { megasenaDezenas.Dez58++; }
                    if (numero.Numero == 59) { megasenaDezenas.Dez59++; }
                    if (numero.Numero == 60) { megasenaDezenas.Dez60++; }
                }

                using var dbcontext = new SorteContext();
                //var estatistica = dbcontext.EstatisticaMegasenas.Where(e => e.DataConcurso == ms.DataConcurso).FirstOrDefault();
                //var flagAdd = estatistica == null;
                //if (flagAdd) { estatistica = new EstatisticaMegasena(); }

                for (int i = 1; i <= 60; i++)
                {
                    var estatistica = dbcontext
                                            .MegasenaCounters
                                            .Where(e => e.DataConcurso == ms.DataConcurso && e.Numero == i)
                                            .FirstOrDefault();
                    if (estatistica == null)
                    {
                        Console.WriteLine(string.Format("Processando Concurso {0}", ms.Concurso));

                        dbcontext.MegasenaCounters.Add(
                            new MegasenaCounter
                            {
                                DataConcurso = ms.DataConcurso,
                                Numero = i,
                                Quantidade = i switch
                                {
                                    1 => megasenaDezenas.Dez01,
                                    2 => megasenaDezenas.Dez02,
                                    3 => megasenaDezenas.Dez03,
                                    4 => megasenaDezenas.Dez04,
                                    5 => megasenaDezenas.Dez05,
                                    6 => megasenaDezenas.Dez06,
                                    7 => megasenaDezenas.Dez07,
                                    8 => megasenaDezenas.Dez08,
                                    9 => megasenaDezenas.Dez09,
                                    10 => megasenaDezenas.Dez10,
                                    11 => megasenaDezenas.Dez11,
                                    12 => megasenaDezenas.Dez12,
                                    13 => megasenaDezenas.Dez13,
                                    14 => megasenaDezenas.Dez14,
                                    15 => megasenaDezenas.Dez15,
                                    16 => megasenaDezenas.Dez16,
                                    17 => megasenaDezenas.Dez17,
                                    18 => megasenaDezenas.Dez18,
                                    19 => megasenaDezenas.Dez19,
                                    20 => megasenaDezenas.Dez20,
                                    21 => megasenaDezenas.Dez21,
                                    22 => megasenaDezenas.Dez22,
                                    23 => megasenaDezenas.Dez23,
                                    24 => megasenaDezenas.Dez24,
                                    25 => megasenaDezenas.Dez25,
                                    26 => megasenaDezenas.Dez26,
                                    27 => megasenaDezenas.Dez27,
                                    28 => megasenaDezenas.Dez28,
                                    29 => megasenaDezenas.Dez29,
                                    30 => megasenaDezenas.Dez30,
                                    31 => megasenaDezenas.Dez31,
                                    32 => megasenaDezenas.Dez32,
                                    33 => megasenaDezenas.Dez33,
                                    34 => megasenaDezenas.Dez34,
                                    35 => megasenaDezenas.Dez35,
                                    36 => megasenaDezenas.Dez36,
                                    37 => megasenaDezenas.Dez37,
                                    38 => megasenaDezenas.Dez38,
                                    39 => megasenaDezenas.Dez39,
                                    40 => megasenaDezenas.Dez40,
                                    41 => megasenaDezenas.Dez41,
                                    42 => megasenaDezenas.Dez42,
                                    43 => megasenaDezenas.Dez43,
                                    44 => megasenaDezenas.Dez44,
                                    45 => megasenaDezenas.Dez45,
                                    46 => megasenaDezenas.Dez46,
                                    47 => megasenaDezenas.Dez47,
                                    48 => megasenaDezenas.Dez48,
                                    49 => megasenaDezenas.Dez49,
                                    50 => megasenaDezenas.Dez50,
                                    51 => megasenaDezenas.Dez51,
                                    52 => megasenaDezenas.Dez52,
                                    53 => megasenaDezenas.Dez53,
                                    54 => megasenaDezenas.Dez54,
                                    55 => megasenaDezenas.Dez55,
                                    56 => megasenaDezenas.Dez56,
                                    57 => megasenaDezenas.Dez57,
                                    58 => megasenaDezenas.Dez58,
                                    59 => megasenaDezenas.Dez59,
                                    60 => megasenaDezenas.Dez60,
                                    _ => 0
                                }
                            });

                    }

                }
                dbcontext.SaveChanges(true);

                //estatistica.DataConcurso = ms.DataConcurso;
                //estatistica.QuantDez01 = megasenaDezenas.Dez01;
                //estatistica.QuantDez02 = megasenaDezenas.Dez02;
                //estatistica.QuantDez03 = megasenaDezenas.Dez03;
                //estatistica.QuantDez04 = megasenaDezenas.Dez04;
                //estatistica.QuantDez05 = megasenaDezenas.Dez05;
                //estatistica.QuantDez06 = megasenaDezenas.Dez06;
                //estatistica.QuantDez07 = megasenaDezenas.Dez07;
                //estatistica.QuantDez08 = megasenaDezenas.Dez08;
                //estatistica.QuantDez09 = megasenaDezenas.Dez09;
                //estatistica.QuantDez10 = megasenaDezenas.Dez10;
                //estatistica.QuantDez11 = megasenaDezenas.Dez11;
                //estatistica.QuantDez12 = megasenaDezenas.Dez12;
                //estatistica.QuantDez13 = megasenaDezenas.Dez13;
                //estatistica.QuantDez14 = megasenaDezenas.Dez14;
                //estatistica.QuantDez15 = megasenaDezenas.Dez15;
                //estatistica.QuantDez16 = megasenaDezenas.Dez16;
                //estatistica.QuantDez17 = megasenaDezenas.Dez17;
                //estatistica.QuantDez18 = megasenaDezenas.Dez18;
                //estatistica.QuantDez19 = megasenaDezenas.Dez19;
                //estatistica.QuantDez20 = megasenaDezenas.Dez20;
                //estatistica.QuantDez21 = megasenaDezenas.Dez21;
                //estatistica.QuantDez22 = megasenaDezenas.Dez22;
                //estatistica.QuantDez23 = megasenaDezenas.Dez23;
                //estatistica.QuantDez24 = megasenaDezenas.Dez24;
                //estatistica.QuantDez25 = megasenaDezenas.Dez25;
                //estatistica.QuantDez26 = megasenaDezenas.Dez26;
                //estatistica.QuantDez27 = megasenaDezenas.Dez27;
                //estatistica.QuantDez28 = megasenaDezenas.Dez28;
                //estatistica.QuantDez29 = megasenaDezenas.Dez29;
                //estatistica.QuantDez30 = megasenaDezenas.Dez30;
                //estatistica.QuantDez31 = megasenaDezenas.Dez31;
                //estatistica.QuantDez32 = megasenaDezenas.Dez32;
                //estatistica.QuantDez33 = megasenaDezenas.Dez33;
                //estatistica.QuantDez34 = megasenaDezenas.Dez34;
                //estatistica.QuantDez35 = megasenaDezenas.Dez35;
                //estatistica.QuantDez36 = megasenaDezenas.Dez36;
                //estatistica.QuantDez37 = megasenaDezenas.Dez37;
                //estatistica.QuantDez38 = megasenaDezenas.Dez38;
                //estatistica.QuantDez39 = megasenaDezenas.Dez39;
                //estatistica.QuantDez40 = megasenaDezenas.Dez40;
                //estatistica.QuantDez41 = megasenaDezenas.Dez41;
                //estatistica.QuantDez42 = megasenaDezenas.Dez42;
                //estatistica.QuantDez43 = megasenaDezenas.Dez43;
                //estatistica.QuantDez44 = megasenaDezenas.Dez44;
                //estatistica.QuantDez45 = megasenaDezenas.Dez45;
                //estatistica.QuantDez46 = megasenaDezenas.Dez46;
                //estatistica.QuantDez47 = megasenaDezenas.Dez47;
                //estatistica.QuantDez48 = megasenaDezenas.Dez48;
                //estatistica.QuantDez49 = megasenaDezenas.Dez49;
                //estatistica.QuantDez50 = megasenaDezenas.Dez50;
                //estatistica.QuantDez51 = megasenaDezenas.Dez51;
                //estatistica.QuantDez52 = megasenaDezenas.Dez52;
                //estatistica.QuantDez53 = megasenaDezenas.Dez53;
                //estatistica.QuantDez54 = megasenaDezenas.Dez54;
                //estatistica.QuantDez55 = megasenaDezenas.Dez55;
                //estatistica.QuantDez56 = megasenaDezenas.Dez56;
                //estatistica.QuantDez57 = megasenaDezenas.Dez57;
                //estatistica.QuantDez58 = megasenaDezenas.Dez58;
                //estatistica.QuantDez59 = megasenaDezenas.Dez59;
                //estatistica.QuantDez60 = megasenaDezenas.Dez60;

                //if (flagAdd) { dbcontext.EstatisticaMegasenas.Add(estatistica); }
                //else { dbcontext.EstatisticaMegasenas.Update(estatistica); }

                //dbcontext.SaveChanges(true);
            }
            Console.WriteLine("Finalizando o Processo de Count.");


        }

        #endregion

    }

}
