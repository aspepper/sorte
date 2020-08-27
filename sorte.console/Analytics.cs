using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace sorte.console
{
    public class Analytics
    {

        private static readonly string barra = (Environment.OSVersion.Platform == PlatformID.Win32NT ? "\\" : "/");

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

        static private void AddDezena(ref MegasenaNumbers megasenaDezenas, Sorteados numero)
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

        static public void CountNumbersEstatisticaMegasenas()
        {
            var megasenaDezenas = new MegasenaNumbers();

            var timeBegin = DateTime.Now;
            Console.WriteLine(string.Format("Inicio do Processo de Count às {0}...", timeBegin.ToString("HH:mm:ss")));
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
                var estatistica = dbcontext.EstatisticaMegasenas.Where(e => e.DataConcurso == ms.DataConcurso).FirstOrDefault();
                var flagAdd = estatistica == null;
                if (flagAdd) { estatistica = new EstatisticaMegasena(); }

                estatistica.DataConcurso = ms.DataConcurso;
                estatistica.QuantDez01 = megasenaDezenas.Dez01;
                estatistica.QuantDez02 = megasenaDezenas.Dez02;
                estatistica.QuantDez03 = megasenaDezenas.Dez03;
                estatistica.QuantDez04 = megasenaDezenas.Dez04;
                estatistica.QuantDez05 = megasenaDezenas.Dez05;
                estatistica.QuantDez06 = megasenaDezenas.Dez06;
                estatistica.QuantDez07 = megasenaDezenas.Dez07;
                estatistica.QuantDez08 = megasenaDezenas.Dez08;
                estatistica.QuantDez09 = megasenaDezenas.Dez09;
                estatistica.QuantDez10 = megasenaDezenas.Dez10;
                estatistica.QuantDez11 = megasenaDezenas.Dez11;
                estatistica.QuantDez12 = megasenaDezenas.Dez12;
                estatistica.QuantDez13 = megasenaDezenas.Dez13;
                estatistica.QuantDez14 = megasenaDezenas.Dez14;
                estatistica.QuantDez15 = megasenaDezenas.Dez15;
                estatistica.QuantDez16 = megasenaDezenas.Dez16;
                estatistica.QuantDez17 = megasenaDezenas.Dez17;
                estatistica.QuantDez18 = megasenaDezenas.Dez18;
                estatistica.QuantDez19 = megasenaDezenas.Dez19;
                estatistica.QuantDez20 = megasenaDezenas.Dez20;
                estatistica.QuantDez21 = megasenaDezenas.Dez21;
                estatistica.QuantDez22 = megasenaDezenas.Dez22;
                estatistica.QuantDez23 = megasenaDezenas.Dez23;
                estatistica.QuantDez24 = megasenaDezenas.Dez24;
                estatistica.QuantDez25 = megasenaDezenas.Dez25;
                estatistica.QuantDez26 = megasenaDezenas.Dez26;
                estatistica.QuantDez27 = megasenaDezenas.Dez27;
                estatistica.QuantDez28 = megasenaDezenas.Dez28;
                estatistica.QuantDez29 = megasenaDezenas.Dez29;
                estatistica.QuantDez30 = megasenaDezenas.Dez30;
                estatistica.QuantDez31 = megasenaDezenas.Dez31;
                estatistica.QuantDez32 = megasenaDezenas.Dez32;
                estatistica.QuantDez33 = megasenaDezenas.Dez33;
                estatistica.QuantDez34 = megasenaDezenas.Dez34;
                estatistica.QuantDez35 = megasenaDezenas.Dez35;
                estatistica.QuantDez36 = megasenaDezenas.Dez36;
                estatistica.QuantDez37 = megasenaDezenas.Dez37;
                estatistica.QuantDez38 = megasenaDezenas.Dez38;
                estatistica.QuantDez39 = megasenaDezenas.Dez39;
                estatistica.QuantDez40 = megasenaDezenas.Dez40;
                estatistica.QuantDez41 = megasenaDezenas.Dez41;
                estatistica.QuantDez42 = megasenaDezenas.Dez42;
                estatistica.QuantDez43 = megasenaDezenas.Dez43;
                estatistica.QuantDez44 = megasenaDezenas.Dez44;
                estatistica.QuantDez45 = megasenaDezenas.Dez45;
                estatistica.QuantDez46 = megasenaDezenas.Dez46;
                estatistica.QuantDez47 = megasenaDezenas.Dez47;
                estatistica.QuantDez48 = megasenaDezenas.Dez48;
                estatistica.QuantDez49 = megasenaDezenas.Dez49;
                estatistica.QuantDez50 = megasenaDezenas.Dez50;
                estatistica.QuantDez51 = megasenaDezenas.Dez51;
                estatistica.QuantDez52 = megasenaDezenas.Dez52;
                estatistica.QuantDez53 = megasenaDezenas.Dez53;
                estatistica.QuantDez54 = megasenaDezenas.Dez54;
                estatistica.QuantDez55 = megasenaDezenas.Dez55;
                estatistica.QuantDez56 = megasenaDezenas.Dez56;
                estatistica.QuantDez57 = megasenaDezenas.Dez57;
                estatistica.QuantDez58 = megasenaDezenas.Dez58;
                estatistica.QuantDez59 = megasenaDezenas.Dez59;
                estatistica.QuantDez60 = megasenaDezenas.Dez60;

                if (flagAdd) { dbcontext.EstatisticaMegasenas.Add(estatistica); }
                else { dbcontext.EstatisticaMegasenas.Update(estatistica); }

                dbcontext.SaveChanges(true);
            }
            Console.WriteLine(string.Format("Finalizando o Processo de Count, às {0}, levou {1} tempo de execução.",
                DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.Subtract(timeBegin).ToString()));

        }

        static public void CountNumbers()
        {
            var megasenaDezenas = new MegasenaNumbers();

            var timeBegin = DateTime.Now;
            Console.WriteLine(string.Format("Inicio do Processo de Count às {0}...", timeBegin.ToString("HH:mm:ss")));
            using var context = new SorteContext();
            foreach (var ms in context.Megasenas.Include(m => m.NumerosSorteados).OrderBy(e => e.DataConcurso))
            {
                foreach (var numero in ms.NumerosSorteados)
                {
                    AddDezena(ref megasenaDezenas, numero);
                }

                using var dbcontext = new SorteContext();
                bool flagAdd = false;
                for (int i = 1; i <= 60; i++)
                {
                    var estatistica = dbcontext
                                            .MegasenaCounters
                                            .Where(e => e.DataConcurso == ms.DataConcurso && e.Numero == i)
                                            .FirstOrDefault();
                    if (estatistica == null)
                    {
                        Console.WriteLine(string.Format("Processando Concurso {0} - Número {1}", ms.Concurso, i));
                        flagAdd = true;
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
                if (flagAdd) { dbcontext.SaveChanges(true); }

            }
            Console.WriteLine(string.Format("Finalizando o Processo de Count, às {0}, levou {1} tempo de execução.",
                DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.Subtract(timeBegin).ToString()));

        }

        #endregion

        #region "Analise Inteligente"

        static public void AnaliseInteligente()
        {
            var pontos = new List<List<List<int>>>();

            using var t = new TransactionScope(TransactionScopeOption.Required,
                                                new TransactionOptions
                                                {
                                                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                                });

            using var contextDb = new SorteContext();
            var row = 0;

            var numeros = new List<int>();
            var timeBegin = DateTime.Now;
            Console.WriteLine(string.Format("Inicio do Processo de Análise às {0}...", timeBegin.ToString("HH:mm:ss")));

            foreach (var ms in contextDb.Megasenas.Include(m => m.NumerosSorteados).OrderByDescending(e => e.DataConcurso))
            {
                row++;

                var nbToAnal = ms.NumerosSorteados
                                 .Where(n => !numeros.Contains(n.Numero))
                                 .OrderBy(n => n.Ordem);
                if (nbToAnal.Count() == 0) { break; }
                foreach (var numeroSorteado in nbToAnal)
                {
                    var num = new List<List<int>>()
                    {
                        new List<int> { numeroSorteado.Numero, ms.Concurso }
                    };
                    numeros.Add(numeroSorteado.Numero);
                    foreach (var msAnt in contextDb.Megasenas
                                                    .Include(m => m.NumerosSorteados
                                                                   .OrderBy(n => n.Ordem))
                                                    .Where(m => m.Concurso < ms.Concurso)
                                                    .OrderByDescending(e => e.DataConcurso))
                    {
                        foreach (var numeroSortAnt in msAnt.NumerosSorteados)
                        {
                            if (numeroSortAnt.Numero == numeroSorteado.Numero)
                            {
                                num.Add(new List<int> { numeroSorteado.Numero, msAnt.Concurso });
                            }
                        }
                        if (num.Count >= 25) { break; }
                    }
                    pontos.Add(num);
                }

                if (row == 500) { break; }

            }

            int currentNum = 0;
            var csvFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            csvFolder += barra + "csv";
            if (!Directory.Exists(csvFolder)) { Directory.CreateDirectory(csvFolder); }
            var fileCSV = Path.Combine(csvFolder, string.Format("analytics{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmss")));
            using StreamWriter outputFile = new StreamWriter(fileCSV, false, new UTF8Encoding(true));
            foreach (var item in pontos)
            {
                foreach (var item2 in item)
                {
                    var linha = "";
                    int num = 0;
                    foreach (var valor in item2)
                    {
                        if (num == 0) { num = valor; }
                        if (currentNum != num)
                        {
                            linha = string.Format("{0}", valor.ToString("00"));
                            currentNum = valor;
                            Console.WriteLine("");
                            outputFile.WriteLine("");
                        }
                        else
                        {
                            if (valor != currentNum)
                            {
                                linha += string.Format(";{0}", valor);
                            }
                        }
                    }
                    if (currentNum != num)
                    {
                        currentNum = num;
                    }

                    Console.Write(linha);
                    outputFile.Write(linha);

                }
            }
            outputFile.WriteLine("");
            outputFile.Close();
            Console.WriteLine("");

            Console.WriteLine(string.Format("Finalizando o Análise de Count, às {0}, levou {1} tempo de execução.",
                    DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.Subtract(timeBegin).ToString()));

        }

        #endregion

    }
}
