#### 修改告警人 MES

```sql
SELECT TOP 100 * FROM tbJobWarningDefinition
WHERE jwdAlarmTitle LIKE '%机台T-Tray数量不足告警%'

BEGIN TRAN
UPDATE tbJobWarningDefinition SET jwdAlarmHandler = '23014727|23220229|23012956'
WHERE jwdAlarmTitle LIKE '%机台T-Tray数量不足告警%'
ROLLBACK
```

