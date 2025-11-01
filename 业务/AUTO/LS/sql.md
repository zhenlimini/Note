#### 抓取结批上传的BS数据

```sql
SELECT 
    ICO03, 
    LeadScan_TestReport.* 
FROM 
    LeadScan_TestReport WITH(NOLOCK)
LEFT JOIN 
    szmesdb.mesdb.DBO.ico_file WITH(NOLOCK) 
    ON ico_file.ico01 = LeadScan_TestReport.LotNo COLLATE Chinese_PRC_CI_AS
WHERE 
    Stage = 'LS200' 
    AND UpLoadTime > '2025-08-22 08:00:00' AND UpLoadTime < '2025-08-28 08:00:00'
    AND ico03 = 'AUSYFK'
```

