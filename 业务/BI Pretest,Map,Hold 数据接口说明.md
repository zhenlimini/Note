# BI Pretest/Map/Hold 数据接口说明

Ver 2.0  更新RDBIHold板需求的更改；

Prepared by Zhen Li

### API 说明

- **接口地址**: 

1. http://szautodev:7001/（测试） 
2. http://szautoweb:7001/（正式环境)

 

### 支持请求方法

- **POST**：在服务器创建资源



### API List

------

#### API 1: BI MAP数据请求

- **请求路径**：api/Tseapp/BiMapRequest
- **请求说明**：BI_data 数据请求接口录入
- **请求方法**： POST
- **请求JSON参数**： 

| 请求参数名    | 参数说明      | 是否为空 | 数据类型 |
| ------------- | ------------- | -------- | -------- |
| LotID         | PTN_LOT       | 否       | String   |
| Stage         | 工站信息      | 否       | String   |
| Input         | 输入数量      | 否       | Int      |
| Customer      | 客户别        | 否       | String   |
| CLotID        | CLotID        | 否       | String   |
| DeviceName    | Device        | 否       | String   |
| PType         | PType         | 否       | String   |
| StartTime     | 开始时间      | 否       | DateTime |
| EndTime       | 结束时间      | 否       | DateTime |
| Program       | Program       | 否       | String   |
| Ver           | Ver           | 否       | String   |
| LotProcess    | MBT\|TBT      | 否       | String   |
| Mask          | Mask          | 否       | String   |
| Oven          | Oven          | 否       | String   |
| Slot          | 槽位          | 否       | Int      |
| CusBoardID    | 客户板号      | 否       | String   |
| BoardID       | 板号          | 否       | String   |
| CBoardType    | 客户版类型    | 否       | String   |
| Yield         | 良率          | 否       | String   |
| BoardTotal    | 数量          | 否       | Int      |
| Pass          | Pass数量      | 否       | Int      |
| Fail          | Fail数量      | 否       | Int      |
| BINNum        | Bin数量       | 否       | List     |
| LineFail      | LineFail      | 否       | Int      |
| BoardMap      | BoardMap      | 否       | String   |
| MapRow        | MapRow        | 否       | Int      |
| MapCol        | MapCol        | 否       | Int      |
| PassFail      | Pass/Fail     | 否       | String   |
| SocketMaskMap | SocketMaskMap | 否       | String   |
| MachineType   | 设备类型      | 否       | String   |
| CateBinMap    | CBinMap       | 否       | String   |
| CateBINNum    | CateBin数量   | 否       | List     |

- **请求参数实例:**


```
{
    "LotID":"C1BN01337-02",
    "Stage":"BI110",
    "Input":9679,
    "Customer":"HSIEPA",
    "CLotID":"1JN888000",
    "DeviceName":"EDJ2108BCBG-JZ-PC2-A4F40",
    "PType":"DDR4",
    "StartTime":"2021-12-22 10:10:10",
    "EndTime":"2021-12-22 12:12:12",
    "Program":"NG14GBM3",
    "Ver":"*",
    "LotProcess":"MBT",
    "Mask":"ZP02",
    "Oven":"A52021",
    "Slot":12,
    "CusBoardID":"4D1E0023",
    "BoardID":"BTCD9235",
    "CBoardType":"4D1A",
    "Yield":"0.94017",
    "BoardTotal":123,
    "Pass":110,
    "Fail":200,
    "BINNum": [
		1,
		2,
		3
	],
    "LineFail":10,
    "BoardMap":"111111111111111111111111111",
    "MapRow":12,
    "MapCol":12,
    "PassFail":"PFFFFFFFFFFFFFFFFFFFFFFFFFFFFF",
    "SocketMaskMap":"string",
    "MachineType":"AF8652",
    "CateBinMap":"string",
    "CateBINNum": [
		1,
		2,
		3
	]
}
```

-  **返回JSON参数**

| 返回数据参数名 | 参数说明                            | 是否为空 | 数据类型 |
| :------------- | ----------------------------------- | -------- | -------- |
| ResultCode     | 状态代码(200表示成功，其他表示失败) | 否       | String   |
| Message        | 信息（报错信息）                    | 否       | String   |

- **返回数据例子**

```
{
    "ResultCode": "200",
    "Message": "" 
}
```

------



#### API 2: BI Pretest数据请求

- **请求路径**：api/Tseapp/BiPretestRequest
- **请求说明**：BI_Pretest 数据请求接口录入
- **请求方法**： POST
- **请求JSON参数**： 

| 请求参数名      | 参数说明   | 是否为空 | 数据类型 |
| --------------- | ---------- | -------- | -------- |
| LotID           | PTN_LOT    | 否       | String   |
| PretestCount    | 预测次数   | 否       | Int      |
| Stage           | 工站信息   | 否       | String   |
| Oven            | Oven       | 否       | String   |
| Slot            | 槽位       | 否       | Int      |
| BoardID         | 板号       | 否       | String   |
| CustomerBoardID | 客户板号   | 否       | String   |
| CBoardType      | 客户板类型 | 否       | String   |
| Yield           | 良率       | 否       | String   |
| LineFail        | LineFail   | 否       | Int      |
| Input           | 输入数量   | 否       | Int      |
| Customer        | 客户别     | 否       | String   |
| CLotID          | CLotID     | 否       | String   |
| DeviceName      | Device     | 否       | String   |
| PType           | PType      | 否       | String   |
| StartTime       | 开始时间   | 否       | DateTime |
| EndTime         | 结束时间   | 否       | DateTime |
| Program         | Program    | 否       | String   |
| Ver             | Ver        | 否       | String   |
| LotProcess      | MBT\|TBT   | 否       | String   |
| Mask            | Mask       | 否       | String   |
| BoardMap        | BoardMap   | 否       | String   |
| TotalDUT        | TotalDUT   | 否       | Int      |
| NODUT           | NODUT      | 否       | Int      |
| PassDUT         | PassDUT    | 否       | Int      |
| FailDUT         | FailDUT    | 否       | Int      |
| SocketFail      | SocketFail | 否       | Int      |
| MapRow          | MapRow     | 否       | Int      |
| MapCol          | MapCol     | 否       | Int      |
| IRNO            | IR机台号   | 否       | String   |
| MachineType     | 设备类型   | 否       | String   |



- **请求参数实例:**


```

{
    "LotID":"C1BN01337-02",
    "PretestCount":1,
    "Stage":"BI110",
    "Oven":"A52021",
    "Slot":12,
    "BoardID":"BTCD9235",
    "CustomerBoardID":"4D1E0023",
    "CBoardType":"4D1A",
    "Yield":"0.94017",
    "LineFail":3,
    "Input":9679,
    "Customer":"HSIEPA",
    "CLotID":"1JN888000",
    "DeviceName":"EDJ2108BCBG-JZ-PC2-A4F40",
    "PType":"DDR4",
    "StartTime":"2021-12-22 10:10:10",
    "EndTime":"2021-12-22 12:12:12",
    "Program":"NG14GBM3",
    "Ver":"*",
    "LotProcess":"MBT",
    "Mask":"ZP02",
    "BoardMap":"PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP",
    "TotalDUT":352,
    "NODUT":4,
    "PassDUT":346,
    "FailDUT":0,
    "SocketFail":2,
    "MapRow":12,
    "MapCol":12,
    "IRNO":"D1101",
    "MachineType":"AF8652"
}
```

-  **参数说明：**
   -  PretestCount：传入参数为-1时，由接口自行通过数据库计算；
   -  IRNO：可传入参数为“”或者null，由接口自行填充；

-  **返回JSON参数**

| 返回数据参数名 | 参数说明                            | 是否为空 | 数据类型 |
| :------------- | ----------------------------------- | -------- | -------- |
| ResultCode     | 状态代码(200表示成功，其他表示失败) | 否       | String   |
| Message        | 信息（报错信息）                    | 否       | String   |

- **返回数据例子**

```
{
    "ResultCode": "200",
    "Message": "" 
}
```



------

#### API3:RDBI Hold请求

- **请求路径**：api/BIHoldBoard/BiHoldRequest
- **请求说明**：Hold板请求
- **请求方法**：POST
- **请求JSON参数**：

| 请求参数名 | 参数说明     | 可否为空 | 数据类型 |
| ---------- | ------------ | -------- | -------- |
| BoardID    | 板号         | 否       | string   |
| EQID       | 机台号       | 可为空   | string   |
| LotID      | Lot          | 可为空   | string   |
| OPID       | Hold板人工号 | 否       | string   |
| HoldReason | Hold板理由   | 否       | string   |

- **请求参数示例：**

```
{
  "BoardID": "D48055",
  "EQID": "H56001",
  "LotID": "123456789012",
  "OPID": "16698",
  "HoldReason": "DUTOFF超标"
}
```

- **返回JSON参数**：

| 返回数据参数名 | 参数说明                            | 是否为空 | 数据类型 |
| :------------- | ----------------------------------- | -------- | -------- |
| ResultCode     | 状态代码(200表示成功，其他表示失败) | 否       | String   |
| Message        | 信息（报错信息）                    | 否       | String   |

- **返回参数示例：**

```
{
    "ResultCode": "200",
    "Message": "Hold Board Success"
}
```

