JAM & ERROR
---
### **1. 数据来源与基础计算**
#### **数据来源**
- **数据库表**：
  - `iqd_file`：Jam/Error数据
  - `ilk_file`：批次数据
  - `ema_file`：设备数据
- **关键字段**：
  - `iqd01`：记录类型（`HANDLER JAM`/`HANDLER ERROR`）
  - `iqd02`：记录内容（格式：`JAM_33;15,K620;15,...`）
  - `ilk00`：批次ID（关联`iqd_file`）
#### **基础计算逻辑**
1. **解析原始记录**：
   - **Jam Count**：从`iqd01`包含`HANDLER JAM`的记录中解析：
     ```csharp
     string[] Arr = drLS["iqd02"].ToString().Split(';');
     foreach (string code in Arr.Skip(1)) {
         string[] ArrCode = code.Split(',');
         int _cnt = 0;
         if (Int32.TryParse(ArrCode[0], out _cnt)) {
             Cnt += _cnt; // 累加Jam次数
         }
     }
     ```
   - **Error Count**：从`iqd01`包含`HANDLER ERROR`的记录中解析，**过滤特定错误代码**：
     ```csharp
     if (errorCode != "EA0034" && errorCode != "EA0035" /*...其他过滤代码*/) {
         // 计入Error Count
     }
     ```
2. **批次级汇总**：
   - 每个批次（`ilk_file`）的Jam/Error Count累加到`dtList`表：
     ```csharp
     drList["JAM_COUNT"] = Cnt.ToString(); // 存储批次Jam Count
     drList["ERROR_COUNT"] = Cnt.ToString(); // 存储批次Error Count
     ```
---
### **2. 设备级汇总（EQID + Handler Type）**
#### **分组计算**
- 按`EQID`和`HandlerType`分组，汇总所有批次的Jam/Error Count：
  ```csharp
  DataTable dtEH = dtList.DefaultView.ToTable(true, "ema04", "HandlerType");
  foreach (DataRow dr in dtEH.Rows) {
      int JamCount = 0, ErrCount = 0;
      foreach (DataRow drLS in dtList.Select("ema04='" + dr["ema04"] + "'")) {
          JamCount += Convert.ToInt32(drLS["JAM_COUNT"]);
          ErrCount += Convert.ToInt32(drLS["ERROR_COUNT"]);
      }
      dr["JamCount"] = JamCount;
      dr["ErrorCount"] = ErrCount;
  }
  ```
#### **MTBJ/MTBE/MTBF计算**
- **MTBJ**（平均故障间隔时间）：
  ```csharp
  double mtbj = 24 / (double)(JamCount + 1); // 公式：24小时/(Jam次数+1)
  ```
- **MTBE**（平均错误间隔时间）：
  ```csharp
  double mtbe = 24 / (double)(ErrCount + 1); // 公式：24小时/(Error次数+1)
  ```
- **MTBF**（平均失效间隔时间）：
  ```csharp
  double mtbf = 24 / (double)(JamCount + ErrCount + 1); // 公式：24小时/(总故障次数+1)
  ```

