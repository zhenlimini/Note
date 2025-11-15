## JK*客户 BIB和DSA数据接口说明

### 接口1：DSA Lifetime统计接口

**接口类型**：WebService（.asmx）
 ​**​功能描述​**​：根据DSA序列号列表，查询每个DSA下所有DUT的寿命统计信息（包括使用次数、Mask状态等）。

#### 请求地址

- **测试地址**：`http://szdevweb/Ares/WebServices/Maintenance/ReviseHIFIXIDStatus.asmx?op=GetDSAInfoInAres`
- **正式地址**：`[待补充]`

#### 请求参数

| 参数名        | 类型           | 说明                              | 数据来源                      |
| ------------- | -------------- | --------------------------------- | ----------------------------- |
| `DSASerialNo` | `List<string>` | DSA序列号列表（如`["C3342504"]`） | 工程注册的DSA号码（界面注册） |

#### 返回值

- **类型**：`string` 

  - 标志位|数据json/报错信息 

  - 标志位：Y表示正确获取到数据，N表示无法正确获取到数据

  - 数据json实际为`List<HifixCycleInfo>`的JSON序列化字符串

- 数据结构

  ```csharp
  public class HifixCycleInfo {
      public string DSASerialNo { get; set; }  // DSA序列号（例："C3342505"）
      public int DUT { get; set; }             // DUT总数（例：384）
      public int DUTNo { get; set; }            // 当前DUT序号（从1到DUT总数）
      public int Lifetimetarget { get; set; }   // Socket寿命目标值（单位：K，例：50）
      public bool IsDutMask { get; set; }       // DUT是否关闭（true=Mask，false=未Mask）
      public int TotalUsedCount { get; set; }  // DUT总使用次数（例：1562）
  }
  ```

#### 字段说明

| 字段             | 描述                | 例子       | 数据来源与逻辑                           |
| ---------------- | ------------------- | ---------- | ---------------------------------------- |
| `DSASerialNo`    | DSA序列号           | C3342505   | 请求参数传入                             |
| `DUT`            | DUT数量             | 384        | Ares系统Tools Data Manager查询（HI-FIX） |
| `DUTNo`          | DUT序号             | 1,2,...,N  | 按DUT总数从1开始递增排序                 |
| `Lifetimetarget` | Socket寿命目标（K） | 50         | Ares系统Hifix Information查询            |
| `IsDutMask`      | DUT是否Mask关闭     | true/false | Ares系统HIFIX CHECK：状态B=Y（true）     |
| `TotalUsedCount` | DUT总使用次数       | 1562       | Ares系统Socket Maintenance的Contact值    |

#### 请求示例

```json
["C3342504", "C3342505"]
```

#### 返回示例（JSON反序列化后）

```json
--正确返回示例：
Y|[
  {
    "DSASerialNo": "C3342504",
    "DUT": 384,
    "DUTNo": 1,
    "Lifetimetarget": 50,
    "IsDutMask": false,
    "TotalUsedCount": 1562
  },
  {
    "DSASerialNo": "C3342504",
    "DUT": 384,
    "DUTNo": 2,
    "Lifetimetarget": 50,
    "IsDutMask": false,
    "TotalUsedCount": 47000
  },
  // ... 更多DUT（共384行）
  {
    "DSASerialNo": "C3342505",
    "DUT": 256,
    "DUTNo": 1,
    "Lifetimetarget": 60,
    "IsDutMask": false,
    "TotalUsedCount": 8555
  }
]

--错误返回示例：
N|数据获取失败！ARES没有注册DUT信息！
```

------





### 接口2：BIB Lifetime统计接口

**接口类型**：WebAPI（HTTP POST）
**​功能描述​**​：按时间范围统计BIB板的生命周期数据（包括使用次数、维护记录等）。

#### 请求地址

- **测试地址**：`http://szautodev:8666/TBIE.API/api/TBIEinterface/B2B_BIBCycleInfo`
- **正式地址**：`[待补充]`

#### 请求参数

- 类型：

  ```
  BIBCycleParam
  ```

  ```csharp
  public class BIBCycleParam {
      public DateTime StartDate { get; set; }  // 统计开始日期（例：2023-01-01）
      public DateTime EndDate { get; set; }    // 统计结束日期（例：2023-01-07）
  }
  ```

#### 返回值

- **类型**：`Result<List<BIBStatusDTO>>`

- 数据结构：

  ```csharp
  public class Result<T>
  {
      /// <summary>
      /// 状态码
      /// </summary>
      public ResultCode Code { get; set; }
  
      /// <summary>
      /// 响应数据
      /// </summary>
      public T Data { get; set; }
  
      /// <summary>
      /// 消息
      /// </summary>
      public string Message { get; set; }
  }
  
  public enum ResultCode
  {
      /// <summary>
      /// 成功
      /// </summary>
      Success = 1,
  
      /// <summary>
      /// 错误
      /// </summary>
      Error = 2
  }
  
  public class BIBCycleInfo {
      public long SN { get; set; }               // 序号（从1递增）
      public string BoardID { get; set; }       // BIB内部板号（例："D28003"）
      public string BIB { get; set; }           // 原始板号（例："C8070003"）
      public string Owner { get; set; }          // 板归属（"GD"=客户资产，"PTN"=其他）
      public int TotalUsedCount { get; set; }    // 总使用次数（例：597）
      public decimal maskRate { get; set; }       // Mask率（例：0.02604）
      public DateTime AcidClearDate { get; set; }     // 酸洗/电分解时间（OADate数值需转换）
      public int AcidInsertCount { get; set; }         // 酸洗/电分解后使用次数（例：13）
      public DateTime MainTainDate { get; set; } // 最近电气检测时间（OADate数值需转换）
  }
  ```

#### 字段说明（按需求文档）

| 字段             | 描述                  | 例子     | 数据来源与逻辑                               |
| ---------------- | --------------------- | -------- | -------------------------------------------- |
| `SN`             | 序号                  | 1,2,...  | 每块BIB板一行，从1递增排序                   |
| `BoardID`        | BIB内部板号           | D28003   | TBIE系统生产记录（生产过*JKL/*JKH的BIB板）   |
| `BIB`            | 原始板号              | C8070003 | TBIE系统BI板注册页的Customer Board ID        |
| `Owner`          | 板归属                | GD/PTN   | TBIE系统PO列：客户资产="GD"，其他="PTN"      |
| `TotalUsedCount` | 总使用次数            | 597      | TBIE系统Insert history count列               |
| `maskRate`       | Mask率                | 0.02604  | TBIE系统Mask Rate列                          |
| `ACIDDate`       | 酸洗/电分解时间       | 2025/3/3 | TBIE系统酸洗/电分解时间列                    |
| `ACIDCount`      | 酸洗/电分解后使用次数 | 13       | TBIE系统酸洗/电分解次数列                    |
| `MainTainDate`   | 最近电气检测时间      | 2025/3/3 | TBIE系统BI板维修页Maintain Date列（近4个月） |

#### 请求示例

```json
{
  "startDate": "2024-01-01T08:38:02.266Z",
  "endDate": "2024-01-07T08:38:02.266Z"
}
```

#### 返回示例

```json
{
    "code":1,
    "message":"OK",
    "data":
    [
      {
        "SN": 1,
        "BoardID": "D28003",
        "BIB": "C8070003",
        "Owner": "GD",
        "TotalUsedCount": 597,
        "maskRate": 0.02604,
        "ACIDDate": "2025-03-17T00:00:00",
        "ACIDCount": 13,
        "MainTainDate": "2025-03-17T00:00:00"
      },
      // ... 其他BIB板数据
    ]
}
```

