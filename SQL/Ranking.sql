--truncate table [dbo].[MegasenaCounters];
select * from [dbo].[MegasenaCounters];

SELECT * FROM [dbo].[Megasenas] MS JOIN [dbo].[Sorteados] N ON N.[MegaSenaId] = MS.[Id] WHERE DataConcurso = '2020-07-16 00:00:00.0000000';

SELECT MC.[DataConcurso],
		MC.[Numero],
		MC.[Quantidade],
		(SELECT COUNT(1) FROM [dbo].[MegasenaCounters] C WHERE C.[DataConcurso]=MC.[DataConcurso] AND C.[Quantidade]=0) AS [QTDZero],
		RANK() OVER (PARTITION BY MC.[DataConcurso] ORDER BY MC.[Quantidade] DESC, N.[Ordem]) AS [Rank]
FROM [dbo].[MegasenaCounters] MC
JOIN [dbo].[Megasenas] MS ON MS.[DataConcurso] = MC.[DataConcurso]
JOIN [dbo].[Sorteados] N ON N.[MegaSenaId] = MS.[Id] AND N.[Numero]=MC.[Numero]
WHERE MC.[Quantidade]>0
GROUP BY
	MC.[DataConcurso],
		MC.[Numero],
		MC.[Quantidade],
		N.[Ordem]
--HAVING RANK() OVER (PARTITION BY MC.[DataConcurso] ORDER BY MC.[Quantidade] DESC, N.[Ordem]) <=6
ORDER BY MC.[DataConcurso], MC.[Quantidade] DESC, N.[Ordem]
;







