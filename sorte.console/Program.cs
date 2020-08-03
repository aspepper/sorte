using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
                        var downloadFile = LoadMega.GetFileFromURL("http://www1.caixa.gov.br/loterias/_arquivos/loterias/D_megase.zip", "Megasena");
                        string fileName = downloadFile.Result;

                        using ZipArchive archive = ZipFile.OpenRead(fileName);
                        var extractPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        extractPath += barra + "extract";
                        if (!Directory.Exists(extractPath))
                        {
                            Directory.CreateDirectory(extractPath);
                        }

                        //process
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.FullName.EndsWith(".htm", StringComparison.OrdinalIgnoreCase))
                            {
                                string fileExt = Path.GetExtension(entry.FullName);
                                string fileNoExt = Path.GetFileNameWithoutExtension(entry.FullName);
                                string destinationPath = Path.GetFullPath(Path.Combine(extractPath, string.Format("{0}{1}{2}", fileNoExt, DateTime.Now.ToString("yyyyMMddHHmmss"), fileExt)));

                                if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                                    entry.ExtractToFile(destinationPath);

                                LoadMega.ExtractNumbersFromFile(destinationPath);

                            }
                        }
                    }
                    break;
                case "process":
                    var processPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    processPath += barra + "extract";

                    var directory = new DirectoryInfo(processPath);
                    var processFile = directory.GetFiles()
                                      .OrderByDescending(f => f.LastWriteTime)
                                      .First();

                    LoadMega.ExtractNumbersFromFile(processFile.FullName);
                    break;
                case "count":
                    Analytics.CountNumbers();
                    break;
                case "analise":
                    Analytics.AnaliseInteligente();
                    break;
            }

        }

    }

}
