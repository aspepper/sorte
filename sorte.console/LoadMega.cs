using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace sorte.console
{
    public class LoadMega
    {

        private static readonly string barra = (Environment.OSVersion.Platform == PlatformID.Win32NT ? "\\" : "/");

        #region "load mega"
        public static async Task<string> GetFileFromURL(string url, string filename)
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


        public static void ExtractNumbersFromFile(string filename)
        {

            var htmlDoc = new HtmlDocument();
            var enc = Encoding.GetEncoding(28591);
            htmlDoc.Load(filename, enc);
            var webtrs = htmlDoc.DocumentNode.SelectNodes("//tr");
            int count = 0;
            var Megasenas = new List<MegasenaRecord>();
            var beginTime = DateTime.Now;
            Console.WriteLine(string.Format("Iniciando processo de extração dos números às {0}", beginTime.ToString("HH:mm:ss")));
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
                                    record = new MegasenaRecord();
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
                        case 14: if (double.TryParse(text, out db)) { record.RateioQuina = db; } break;
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
                        var UpdFlag = false;
                        var messageProcess = "";

                        if (AddFlag)
                        {
                            messageProcess = string.Format("Processando Data {0} - Concurso {1}", record.DataConcurso, concursoCorrente);

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
                        else
                        {
                            if (recordMS.ArredacaoTotal.HasValue && (!record.ArredacaoTotal.Equals(recordMS.ArredacaoTotal.Value))) 
                            { recordMS.ArredacaoTotal = record.ArredacaoTotal; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Arrecadação Total"; }
                            if (recordMS.Ganhadores.HasValue && (!record.Ganhadores.Equals(recordMS.Ganhadores.Value))) 
                            { recordMS.Ganhadores = record.Ganhadores; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Ganhadores"; }
                            if (recordMS.Rateio.HasValue && (!record.Rateio.Equals(recordMS.Rateio.Value))) 
                            { recordMS.Rateio = record.Rateio; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Rateio"; }
                            if (recordMS.GanhadoresQuina.HasValue && (recordMS.GanhadoresQuina.Value != record.GanhadoresQuina)) 
                            { recordMS.GanhadoresQuina = record.GanhadoresQuina; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Ganhadores Quina"; }
                            if (recordMS.RateioQuina.HasValue && (recordMS.RateioQuina.Value != record.RateioQuina)) 
                            { recordMS.RateioQuina = record.RateioQuina; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Rateio Quina"; }
                            if (recordMS.GanhadoresQuadra.HasValue && (recordMS.GanhadoresQuadra.Value != record.GanhadoresQuadra)) 
                            { recordMS.GanhadoresQuadra = record.GanhadoresQuadra; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Ganhadores Quadra"; }
                            if (recordMS.RateioQuadra.HasValue && (recordMS.RateioQuadra.Value != record.RateioQuadra)) 
                            { recordMS.RateioQuadra = record.RateioQuadra; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Rateio Quadra"; }
                            if (!string.IsNullOrEmpty(recordMS.Acumulado) && (recordMS.Acumulado != record.Acumulado)) 
                            { recordMS.Acumulado = record.Acumulado; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Acumulado"; }
                            if (recordMS.ValorAcumulado.HasValue && (recordMS.ValorAcumulado.Value != record.ValorAcumulado)) 
                            { recordMS.ValorAcumulado = record.ValorAcumulado; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Valor Acumulado"; }
                            if (recordMS.EstimativaPremio.HasValue && (recordMS.EstimativaPremio.Value != record.EstimativaPremio)) 
                            { recordMS.EstimativaPremio = record.EstimativaPremio; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Estimativa do Prêmio"; }
                            if (recordMS.AcumuladoVirada.HasValue && (recordMS.AcumuladoVirada.Value != record.AcumuladoVirada)) 
                            { recordMS.AcumuladoVirada = record.AcumuladoVirada; UpdFlag = true; messageProcess += (messageProcess.Length > 0 ? ", " : "") + "Atualizando Acumulado Virada"; }
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
                                    UpdFlag = true;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(messageProcess))
                        {
                            Console.WriteLine(string.Format("Processando Data {0} - Concurso {1} - {2}.", record.DataConcurso, concursoCorrente, messageProcess));
                            messageProcess = "";
                        }
                        if (AddFlag) { context.Megasenas.Add(recordMS); }
                        else
                        if (UpdFlag) { context.Megasenas.Update(recordMS); }
                        if (AddFlag || UpdFlag) { context.SaveChanges(); }

                        count++;
                        cols = 0;
                        maxCols = 21;
                    }
                }
                break;
            }
            Console.WriteLine(string.Format("Finalizando processo de extração dos números às {0}, levou {1} tempo de execução.",
                DateTime.Now.ToString("HH:mm:ss"),
                DateTime.Now.Subtract(beginTime).ToString()));

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

    }
}
