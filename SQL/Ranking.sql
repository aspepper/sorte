--truncate table [dbo].[MegasenaCounters];
select * from [dbo].[MegasenaCounters];


SELECT * FROM [dbo].[Megasenas] ORDER BY [DataConcurso] DESC;

SELECT max(Id) FROM [dbo].[Megasenas];
SELECT max(Id) FROM [dbo].[Sorteados];


DBCC CHECKIDENT ('Megasenas', RESEED, 2286);  
DBCC CHECKIDENT ('Sorteados', RESEED, 13698);  

DELETE FROM [dbo].[MegasenaCidades] WHERE [MegasenaId] IN (3286,4286,4287);
DELETE FROM [dbo].[Sorteados] WHERE [MegasenaId] IN (3286,4286,4287);
DELETE FROM [dbo].[Megasenas] WHERE [Id] IN (3286,4286,4287);

SELECT N.[Numero], N.[Ordem] 
FROM [dbo].[Megasenas] MS 
JOIN [dbo].[Sorteados] N ON N.[MegaSenaId] = MS.[Id] 
WHERE DataConcurso = '2020-07-22 00:00:00.0000000';

SELECT	MC.[DataConcurso],
		MC.[Numero],
		MC.[Quantidade],
		(SELECT COUNT(1) FROM [dbo].[MegasenaCounters] C WHERE C.[DataConcurso]=MC.[DataConcurso] AND C.[Quantidade]=0) AS [QTDZero],
		RANK() OVER (PARTITION BY MC.[DataConcurso] ORDER BY MC.[Quantidade] DESC, N.[Ordem]) AS [Rank]
FROM [dbo].[MegasenaCounters] MC
JOIN [dbo].[Megasenas] MS ON MS.[DataConcurso] = MC.[DataConcurso]
LEFT JOIN [dbo].[Sorteados] N ON N.[MegaSenaId] = MS.[Id] AND N.[Numero]=MC.[Numero]
WHERE MC.[Quantidade]>0
GROUP BY
		MC.[DataConcurso],
		MC.[Numero],
		MC.[Quantidade],
		N.[Ordem]
ORDER BY MC.[DataConcurso], MC.[Quantidade] DESC, N.[Ordem]
;

SELECT * FROM [dbo].[MegasenaCounters] MC 
WHERE MC.[DataConcurso] = '2020-07-22 00:00:00.0000000' ORDER BY MC.[Quantidade] DESC;

/*
53
10
5
23
4
33

50
28
58
29
59
31

*/


SELECT * FROM [dbo].[Megasenas];

--UPDATE [dbo].[Megasenas] SET [RateioQuina] = 0;




SELECT	MC.[DataConcurso],
		MC.[Numero],
		MC.[Quantidade]
FROM [dbo].[MegasenaCounters] MC
JOIN [dbo].[Megasenas] MS ON MS.[DataConcurso] = MC.[DataConcurso]
LEFT JOIN [dbo].[Sorteados] N ON N.[MegaSenaId] = MS.[Id] AND N.[Numero]=MC.[Numero]
WHERE MC.[Quantidade]>0
GROUP BY
	MC.[DataConcurso],
		MC.[Numero],
		MC.[Quantidade],
		N.[Ordem]
--HAVING RANK() OVER (PARTITION BY MC.[DataConcurso] ORDER BY MC.[Quantidade] DESC, N.[Ordem]) <=6
ORDER BY MC.[DataConcurso], MC.[Quantidade] DESC, N.[Ordem]
;

SELECT MAX([Quantidade]) FROM [dbo].[MegasenaCounters] WHERE [DataConcurso] = '2020-07-16 00:00:00.0000000';

SELECT 	* 
FROM 	[dbo].[MegasenaCounters] MC
LEFT JOIN 	[dbo].[Megasenas] MS ON MS.[DataConcurso] = MC.[DataConcurso]
ORDER BY 
		MC.[DataConcurso] DESC, 
		MC.[Quantidade] DESC
;




SELECT	TOP 10
		MS.[Concurso],
		CONVERT(VARCHAR(10),MS.[DataConcurso],103) AS [DataConcurso],
		CASE DATEPART(DW, MS.[DataConcurso]) 
		WHEN 1 THEN 'Domingo'
		WHEN 2 THEN 'Segunda'
		WHEN 3 THEN 'Terça'
		WHEN 4 THEN 'Quarta'
		WHEN 5 THEN 'Quinta'
		WHEN 6 THEN 'Sexta'
		WHEN 7 THEN 'Sábado'
		END [DayOfWeek],
		(SELECT SN1.[Numero] FROM [dbo].[Sorteados] SN1 WHERE SN1.[MegaSenaId] = MS.[Id] AND SN1.[Ordem]=1) AS N1,
		(SELECT SN2.[Numero] FROM [dbo].[Sorteados] SN2 WHERE SN2.[MegaSenaId] = MS.[Id] AND SN2.[Ordem]=2) AS N2,
		(SELECT SN3.[Numero] FROM [dbo].[Sorteados] SN3 WHERE SN3.[MegaSenaId] = MS.[Id] AND SN3.[Ordem]=3) AS N3,
		(SELECT SN4.[Numero] FROM [dbo].[Sorteados] SN4 WHERE SN4.[MegaSenaId] = MS.[Id] AND SN4.[Ordem]=4) AS N4,
		(SELECT SN5.[Numero] FROM [dbo].[Sorteados] SN5 WHERE SN5.[MegaSenaId] = MS.[Id] AND SN5.[Ordem]=5) AS N5,
		(SELECT SN6.[Numero] FROM [dbo].[Sorteados] SN6 WHERE SN6.[MegaSenaId] = MS.[Id] AND SN6.[Ordem]=6) AS N6,
		MS.[Ganhadores],
		MS.[Rateio],
		MS.[Acumulado],
		MS.[ValorAcumulado],
		(SELECT TOP 1 CID.[Cidade] FROM [dbo].[MegasenaCidades] CID WHERE CID.[MegaSenaId] = MS.[Id] ORDER BY CID.[Id] DESC) AS [Cidade],
		(SELECT TOP 1 CID.[UF] FROM [dbo].[MegasenaCidades] CID WHERE CID.[MegaSenaId] = MS.[Id] ORDER BY CID.[Id] DESC) AS [UF]
FROM	[dbo].[Megasenas] MS
--WHERE	MS.[DataConcurso] = '2020-07-16 00:00:00.0000000'
ORDER	BY MS.[DataConcurso] DESC;
;

SELECT * FROM [dbo].[MegasenaCidades];



SELECT	TOP 10
		MS.[Concurso],
		CONVERT(VARCHAR(10),MS.[DataConcurso],103) AS [DataConcurso],
		CASE DATEPART(DW, MS.[DataConcurso]) 
		WHEN 1 THEN 'Domingo'
		WHEN 2 THEN 'Segunda'
		WHEN 3 THEN 'Terça'
		WHEN 4 THEN 'Quarta'
		WHEN 5 THEN 'Quinta'
		WHEN 6 THEN 'Sexta'
		WHEN 7 THEN 'Sábado'
		END [DayOfWeek],
		(SELECT SN1.[Numero] FROM [dbo].[Sorteados] SN1 WHERE SN1.[MegaSenaId] = MS.[Id] AND SN1.[Ordem]=1) AS N1,
		(SELECT SN2.[Numero] FROM [dbo].[Sorteados] SN2 WHERE SN2.[MegaSenaId] = MS.[Id] AND SN2.[Ordem]=2) AS N2,
		(SELECT SN3.[Numero] FROM [dbo].[Sorteados] SN3 WHERE SN3.[MegaSenaId] = MS.[Id] AND SN3.[Ordem]=3) AS N3,
		(SELECT SN4.[Numero] FROM [dbo].[Sorteados] SN4 WHERE SN4.[MegaSenaId] = MS.[Id] AND SN4.[Ordem]=4) AS N4,
		(SELECT SN5.[Numero] FROM [dbo].[Sorteados] SN5 WHERE SN5.[MegaSenaId] = MS.[Id] AND SN5.[Ordem]=5) AS N5,
		(SELECT SN6.[Numero] FROM [dbo].[Sorteados] SN6 WHERE SN6.[MegaSenaId] = MS.[Id] AND SN6.[Ordem]=6) AS N6,
		MS.[Ganhadores],
		MS.[Rateio],
		MS.[Acumulado],
		MS.[ValorAcumulado],
		(SELECT TOP 1 CID.[Cidade] FROM [dbo].[MegasenaCidades] CID WHERE CID.[MegaSenaId] = MS.[Id] ORDER BY CID.[Id] DESC) AS [Cidade],
		(SELECT TOP 1 CID.[UF] FROM [dbo].[MegasenaCidades] CID WHERE CID.[MegaSenaId] = MS.[Id] ORDER BY CID.[Id] DESC) AS [UF],
		(SELECT TOP 1 CID.[Cidade] FROM [dbo].[MegasenaCidades] CID WHERE CID.[MegaSenaId] = MS.[Id] AND CID.[Id]<> (SELECT TOP 1 CID.[Id] FROM [dbo].[MegasenaCidades] CID WHERE CID.[MegaSenaId] = MS.[Id] ORDER BY CID.[Id] DESC) ORDER BY CID.[Id] DESC) AS [Cidade_2],
		(SELECT TOP 1 CID.[UF] FROM [dbo].[MegasenaCidades] CID WHERE CID.[MegaSenaId] = MS.[Id] AND CID.[Id]<> (SELECT TOP 1 CID.[Id] FROM [dbo].[MegasenaCidades] CID WHERE CID.[MegaSenaId] = MS.[Id] ORDER BY CID.[Id] DESC) ORDER BY CID.[Id] DESC) AS [UF_2]
FROM	[dbo].[Megasenas] MS
--WHERE	MS.[DataConcurso] = '2020-07-16 00:00:00.0000000'
ORDER	BY MS.[DataConcurso] DESC;
;

SELECT * FROM [dbo].[MegasenaCidades];

SELECT C.[Cidade], C.[UF], COUNT(1) As QTD FROM [dbo].[MegasenaCidades] C WHERE C.[Cidade]<>'' GROUP BY C.[Cidade], C.[UF] ORDER BY QTD DESC;



-- 10 mais 
SELECT
		*
FROM	(
		SELECT	MC.[DataConcurso],
				MC.[Numero],
				MC.[Quantidade],
				(SELECT COUNT(1) FROM [dbo].[MegasenaCounters] C WHERE C.[DataConcurso]=MC.[DataConcurso] AND C.[Quantidade]=0) AS [QTDZero],
				RANK() OVER (PARTITION BY MC.[DataConcurso] ORDER BY MC.[Quantidade] DESC, MC.[Id]) AS [Rank]
		FROM [dbo].[MegasenaCounters] MC
		JOIN (SELECT MAX([DataConcurso]) AS [DataConcurso] FROM [dbo].[Megasenas] WHERE [DataConcurso]<'2020-07-18 00:00:00.0000000') MS ON MS.[DataConcurso] = MC.[DataConcurso]
		WHERE MC.[Quantidade]>0
		GROUP BY
				MC.[DataConcurso],
				MC.[Numero],
				MC.[Quantidade],
				MC.[Id]
		) T
WHERE	T.[Rank] BETWEEN 1 AND 10
ORDER BY T.[DataConcurso], T.[Quantidade] DESC
;


-- 10 menos
SELECT
		*
FROM	(
		SELECT	MC.[DataConcurso],
				MC.[Numero],
				MC.[Quantidade],
				(SELECT COUNT(1) FROM [dbo].[MegasenaCounters] C WHERE C.[DataConcurso]=MC.[DataConcurso] AND C.[Quantidade]=0) AS [QTDZero],
				RANK() OVER (PARTITION BY MC.[DataConcurso] ORDER BY MC.[Quantidade] DESC, MC.[Id]) AS [Rank]
		FROM [dbo].[MegasenaCounters] MC
		JOIN (SELECT MAX([DataConcurso]) AS [DataConcurso] FROM [dbo].[Megasenas] WHERE [DataConcurso]<'2020-07-18 00:00:00.0000000') MS ON MS.[DataConcurso] = MC.[DataConcurso]
		WHERE MC.[Quantidade]>0
		GROUP BY
				MC.[DataConcurso],
				MC.[Numero],
				MC.[Quantidade],
				MC.[Id]
		) T
WHERE	T.[Rank] BETWEEN 51 AND 60
ORDER BY T.[DataConcurso], T.[Quantidade] DESC
;

SELECT TOP 3 [DataConcurso] FROM [dbo].[Megasenas] ORDER BY [DataConcurso] DESC;


SELECT * FROM [dbo].[MegasenaCidades];




