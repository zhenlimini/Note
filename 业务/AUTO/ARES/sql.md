#### BY客户+机台Group机台RUN状态数据

```sql
DECLARE @d0 DATE = '2025-07-01 00:00:00';
DECLARE @d1 DATE = '2025-08-01 00:00:00';

SELECT ema04 AS AresID, ema05 AS MESID, ema07 AS EQType, ema08 AS EQKind, emf15 AS ControlCode,
       SUM(RateTime) AS [TestTime(H)]
FROM (
    SELECT ema04, emf15, ema05, ema07, ema08,
           CAST(DATEDIFF(SECOND, emf02, emf03) / 3600.0 AS DECIMAL(18, 6)) AS RateTime
    FROM (
        SELECT emf01,
               CASE WHEN emf02 < @d0 THEN @d0 ELSE emf02 END AS emf02,
               CASE WHEN emf03 > @d1 OR emf03 IS NULL THEN @d1 ELSE emf03 END AS emf03,
               emf05, emf06, emf15
        FROM   emf_file
        WHERE  emf02 <  @d0 AND (emf03 > @d0 OR emf03 IS NULL)
        UNION
        SELECT emf01, emf02,
               CASE WHEN emf03 > @d1 OR emf03 IS NULL THEN @d1 ELSE emf03 END AS emf03,
               emf05, emf06, emf15
        FROM   emf_file
        WHERE  emf02 BETWEEN @d0 AND @d1
    ) emf
    RIGHT JOIN dbo.ema_file ON ema04 = emf01
    WHERE ema03 = 'FT' AND emf05 = '00' AND emf06 IN ('00', '03')
    GROUP BY emf05, emf02, emf03, ema04, ema03, emf15, ema05, ema07, ema08
) A
GROUP BY ema04, emf15, ema07, ema08, ema05
ORDER BY ema04, emf15
```

#### BY客户+机台+温度Group机台RUN状态数据

```SQL
--SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @d0 DATE = '2025-07-01 00:00:00';
DECLARE @d1 DATE = '2025-08-01 00:00:00';

SELECT ema04 AS AresID, ema05 AS MESID, ema07 AS EQType, ema08 AS EQKind, emf15 AS ControlCode,
       SUM(RateTime) AS [TestTime(H)],eml20 AS [Temp]
       FROM(
    SELECT ema04, emf15, ema05, ema07, ema08,emf02, emf03,
           CAST(DATEDIFF(SECOND, emf02, emf03) / 3600.0 AS DECIMAL(18, 6)) AS RateTime,eml20
    FROM (
        SELECT emf01,
               CASE WHEN emf02 < @d0 THEN @d0 ELSE emf02 END AS emf02,
               CASE WHEN emf03 > @d1 OR emf03 IS NULL THEN @d1 ELSE emf03 END AS emf03,
               emf05, emf06, emf15,emf11
        FROM   emf_file
        WHERE  emf02 <  @d0 AND (emf03 > @d0 OR emf03 IS NULL)
        UNION
        SELECT emf01, emf02,
               CASE WHEN emf03 > @d1 OR emf03 IS NULL THEN @d1 ELSE emf03 END AS emf03,
               emf05, emf06, emf15,emf11
        FROM   emf_file
        WHERE  emf02 BETWEEN @d0 AND @d1
    ) emf
    RIGHT JOIN dbo.ema_file ON ema04 = emf01
    LEFT JOIN dbo.eml_file ON eml04 = emf11
    WHERE ema03 = 'FT' AND emf05 = '00' AND emf06 IN ('00', '03')
    GROUP BY emf05, emf02, emf03, ema04, ema03, emf15, ema05, ema07, ema08 ,eml20
) A
GROUP BY ema04, emf15, ema07, ema08, ema05,eml20
ORDER BY ema04, emf15
```

#### 抓取PGM INFO

```sql
SELECT CreateDate,Type,FileIndex,KeyName,value,*
FROM [dbo].[tb_PGM_MainInfo]
RIGHT JOIN [dbo].[tb_PGM_DetilParameter] ON tb_PGM_MainInfo.SN = tb_PGM_DetilParameter.SN 
WHERE Version = 'tcd416g8hht1' AND Type = 'pe' and FileIndex = 'pefile' AND KeyName = 'sttst'
ORDER BY tb_PGM_MainInfo.CreateDate
```

#### 抓取预计结批时间

```sql
select top 15 * from (
select  ROW_NUMBER() over(partition by emm04 order by emm05) as rowNum,* from emm_file 
inner join eml_file on emm04=eml04 and eml07='00' and eml08='00' and eml10 is not null and eml13 <> 0
inner join ema_file on eml05=ema04 and ema08='B/I Oven' and ema04='H56022-2'
inner join MESDB.dbo.icr_file on icr01=emm05 and icr03 = emm13 and icr12='D83FR2HT' and icr13='[T.R043]' and emm12 = 'D83FR2HT'
)a 
where rowNum = 1
order by eml01 desc
													
select top 15 * from (
select  ROW_NUMBER() over(partition by emm04 order by emm05) as rowNum,* from emm_file 
inner join eml_file on emm04=eml04 and eml07='00' and eml08='00' and eml10 is not null and eml13 <> 0
inner join ema_file on eml05=ema04 and ema08='B/I Oven' and ema07='H5620'
inner join MESDB.dbo.icr_file on icr01=emm05 and icr03 = emm13 and icr12='D83FR2HT' and icr13='[T.R043]' and emm12 = 'D83FR2HT'
)a 
where rowNum = 1
order by eml01 desc         
							
SELECT * from mesdb.dbo.igk_file where igk03 = 'BI300' and igk05 LIKE 'BI总时间%' and
igk01 = 'ACSFNL' and igk02 = 'CC4S8G08V-F7'  and igkmsk = 'PJ02' order by igk08 desc
```

#### EQ ACT Information

```sql
DECLARE @d0 DATE = '2025-07-01';
DECLARE @d1 DATE = '2025-07-03';

SELECT
    emm04               AS [S/N],
    emi06s              AS [RunType],
    emr01               AS [Lot],
    emr01               AS [C/Lot],          -- 原 SQL 已定义别名 CLot
    emm13               AS [Stage],
    emr02               AS [Customer],
    emr03               AS [PROD.],
    emr04               AS [PinCount],
    emr05               AS [Device],
    CAST(NULL AS VARCHAR(100)) AS [prod_desc],-- 若实际表无此列，用 NULL 占位
    CAST(NULL AS VARCHAR(100)) AS [Production_Desc],
    CAST(NULL AS VARCHAR(100)) AS [Package Size],
    end05               AS [Device Type],
    emr06               AS [Ext.],
    emm12               AS [Program],
    CAST(NULL AS VARCHAR(100)) AS [Program_Version],
    emm16               AS [Mask],
    emm07               AS [LotQty],
    emm08               AS [InputQty],
    emm09               AS [GoodQty],
    emm10               AS [FailQty],
    emm11               AS [DamageQty],
    emm14               AS [Other],
    emm15               AS [Auto R/T],
    CASE
        WHEN emm08 = 0 OR emm08 <= emm14 THEN 0
        ELSE emm09 * 100.0 / (emm08 - emm14)
    END                 AS [Yield],
    eml11               AS [Dut],
    eml12               AS [DutOff],
    eml13               AS [TestTime],
    eml15 * 1000.0      AS [eml15],          -- 原 SQL 已乘 1000
    eml09               AS [BeginTime],
    eml10               AS [EndTime],
    CAST(NULL AS DATETIME)     AS [LotEnd],  -- 若无此列，用 NULL 占位
    CAST(NULL AS VARCHAR(20))  AS [Temp]     -- 若无此列，用 NULL 占位
FROM emi_file
JOIN eml_file ON eml07 = emi04 AND eml08 = emi05
JOIN emm_file ON eml04 = emm04
JOIN eme_file ON eme04 = eml05
JOIN emr_file ON emm05 = emr01
LEFT JOIN end_file ON emr05 = end04
INNER JOIN ema_file ON ema04 = eml05
WHERE ema08 = 'Tester'
  AND eml10 BETWEEN @d0 AND @d1
  AND emi10 = 'FT' AND emi06s = 'R1'
ORDER BY eml09 DESC;
```

#### PMRS_TEMP

```sql
SELECT    epn09,epn03,epm03,* FROM    dbo.epm_file
LEFT  JOIN dbo.epn_file ON epm04 = epn07
LEFT JOIN dbo.epo_file ON epn04 = epo04
WHERE epn04 LIKE 'F%' AND epo06 = 'HKFKTA' AND epo07 = 'DRAM' AND epo08 = 'DDR3'
AND epo11 = '4Gb' AND epo12 = 'D' AND epo13 = 'WBGA' AND epo15 = '8'
AND epo18 = '10.6X7.5X1.2' 
AND epn05 = 'M0301-1' 
AND epn06 = 'C3340101'

SELECT TOP 100 * FROM epn_file WHERE epn07 = 'PMRSF250700029'

SELECT TOP 100 * FROM epm_file --WHERE epm18 <> ''
WHERE epm04 = 'PMRSF220500361'

SELECT TOP 100 * FROM epn_file WHERE epn04 = 'F000209' AND epn05 = 'M0301-1' AND epn09 = '88'
AND epn06 = 'C3340101'

SELECT TOP 100 * FROM epn_file WHERE epn07 IN
('PMRSF220500361','PMRSF220500362','PMRSF220500185')


BEGIN TRAN
DELETE FROM epn_file WHERE epn07 IN
('PMRSF220500361','PMRSF220500362','PMRSF220500185')
ROLLBACK
```

#### 转机单跳过USB卡控

```sql
BEGIN TRAN
insert into FT_3309DutHistoryInfo (MachineNo, TesterNo,[DutNo],DutInfo,[DutTime], [ChangeKitorder],[Remark],[UploadTime], [UploadNum], [IsEffectived])
values('MH803-1','','','',GETDATE(),'2510160009','CIM UFS PASS',GETDATE(),'23016698','Y')
ROLLBACK
```

