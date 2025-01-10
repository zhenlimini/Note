### ARES-OA 温补签批 接口说明

------

#### 1.接口地址：

SZ正式环境：http://szaresweb.ptn.gwkf.cn/Ares/WebServices/Maintaince/ARES_TempOffSet_OA.asmx

HF正式环境：http://hfaresweb.ptn.gwkf.cn/Ares/WebServices/Maintaince/ARES_TempOffSet_OA.asmx

测试环境：http://szdevqas.ptn.gwkf.cn/Ares/WebServices/Maintaince/ARES_TempOffSet_OA.asmx



#### 2.方法说明

#### 1.Disable_Ares_Data

- 功能描述：

用于在OA单提出后请求ARES执行功能；

失效正式环境的ARES数据、删除已存在的温补名的临时数据；

- 参数说明：

| 参数        | 类型   | 描述   |
| ----------- | ------ | ------ |
| sEQID       | string | 机台ID |
| sTempOffset | string | 温补名 |
| OANum       | string | OA单号 |

- 返回值说明：

| 类型   | 描述                | 示例             |
| ------ | ------------------- | ---------------- |
| string | 执行结果 + 异常信息 | Y\|     N\|ERROR |



#### 2.Enable_Ares_Data

- 功能描述：

用于OA单所有审批流程结束后请求ARES执行功能；

同步已上传的温补名的临时数据到正式环境，已存在则更新温补值，不存在则新插入；

- 参数说明：

| 参数        | 类型   | 描述   |
| ----------- | ------ | ------ |
| sEQID       | string | 机台ID |
| sTempOffset | string | 温补名 |
| OANum       | string | OA单号 |

- 返回值说明：

| 类型   | 描述                | 示例             |
| ------ | ------------------- | ---------------- |
| string | 执行结果 + 异常信息 | Y\|     N\|ERROR |

