## AI视觉检测数据采集 - API文档说明

#### API说明

- **接口地址**：

测试环境：http://szdevqas.ptn.gwkf.cn/Ares/WEBAPI/EquipmentKitCheck

正式环境：http://szaresweb.ptn.gwkf.cn/Ares/WEBAPI/EquipmentKitCheck

- **请求方法**：POST



#### API LIST

##### API Common

- 返回值说明：

  | 名称    | 类型   | 描述         | 备注                  |
  | ------- | ------ | ------------ | --------------------- |
  | Code    | int    | 状态码       | 1为成功，其它均为失败 |
  | Message | string | 请求状态信息 |                       |
  | Data    | obj    | 数据         |                       |

- 返回值示例：

  ```json
  {"code": 1,"message": "OK","data": "1573e784bedd4de6bef26bb6074658e6"}
  {"Code":7,"Message":"文件上传失败","data":null}
  {"code":1,"message":"OK","data":null}
  ```



##### API1：KitCheckResults

- 请求地址：/api/KitCheck/UploadKitCheckResults

- 参数说明：

  | 名称           | 类型     | 描述         | 备注         |
  | -------------- | -------- | ------------ | ------------ |
  | Product        | string   | 器件类型     |              |
  | PartID         | string   | 器件ID       |              |
  | Result         | string   | 检测结果     | PASS或者FAIL |
  | UploadDateTime | DateTime | 结果上传时间 |              |
  | Location       | string   | 具体坐标     | 例如：3-6    |
  | NGType         | string   | 缺陷类型     | 例如：花瓣NG |
  | OP             | string   | 操作员       |              |
  | EQID           | string   | 机台号       |              |

- 请求参数示例：


```json
{
  "product": "T-Tray",
  "partID": "MF15.0*10.0-A1",
  "result": "PASS",
  "location": "NA",
  "ngType": "NA",
  "op": "123456",
  "eqid": "M8801",
  "uploadDateTime": "2024-03-10 07:44:35.064"
}
```

- 返回值Data说明：ID(string)，表示与该信息绑定的ID号

  

##### API2：UploadFileSingalWithID

- 请求地址：/api/File/UploadFileSingalWithID

- 参数说明：

  | 名称 | 类型   | 描述             | 备注       |
  | ---- | ------ | ---------------- | ---------- |
  | file | 文件流 | NG文件           |            |
  | id   | string | 文件对应的信息ID | 从API1获得 |

- 返回值Data说明：null，无需返回数据