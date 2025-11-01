## Tetser PGM参数设定查询 接口文档

Prepared by Zhen Li



#### API说明

- **接口地址**：

​	测试环境：http://szautodev:7002/

​	正式环境：http://szautoweb:7002/

​	HF正式环境：http://szautoap:7002/

- **请求方法：POST**



#### API LIST

#### API 1:Get PGM

- **说明**：用于请求目前数据库已保存的PGM数据

- **路径**：api/TesterPGM/GetPGMValueRequest

- **请求参数示例**：

  ```
  {
      "Device":"F9QRKAD-P0AAC8Q001",
      "Program":"DRSY4G4A",
      "Customer":"JKL",
      "Version":"3844QRKAS0006",
      "HandlerID":"50325QX"
  }
  ```

  请求参数为null或““则视为不作为查询条件

- **返回参数示例**：

  ```
  {
      "ResultCode": "Y",
      "Message": "Query Success",
      "Value": [
          {
              "Device": "F9QRKAD-P0AAC8Q001",
              "Program": "DRSY4G4A",
              "Customer": "JKL",
              "SubCustomer": "JKL",
              "Version": "3844QRKAS0006",
              "HandlerID": "50325QX",
              "SN": "PGMSN0001",
              "CreateDate": "2022/6/24 8:24:08",
              "ModifyDate": "2022/6/24 8:24:06",
              "CreateUser": "李震",
              "ModifyUser": "",
              "Enable": "Y",
              "Type": "PE",
              "FileIndex": "pefile",
              "KeyName": "ststg",
              "Value": "ft300",
              "Remark": "工站"
          },
          {
              "Device": "F9QRKAD-P0AAC8Q001",
              "Program": "DRSY4G4A",
              "Customer": "JKL",
              "SubCustomer": "JKL",
              "Version": "3844QRKAS0006",
              "HandlerID": "50325QX",
              "SN": "PGMSN0001",
              "CreateDate": "2022/6/24 8:24:08",
              "ModifyDate": "2022/6/24 8:24:06",
              "CreateUser": "李震",
              "ModifyUser": "",
              "Enable": "Y",
              "Type": "PE",
              "FileIndex": "pefile",
              "KeyName": "sttst",
              "Value": "t0316,t0308,t0309,t0305,t0312",
              "Remark": "release机台"
          }
       ]
  }
  ```


#### API 2:Set PGM

- **说明**：

  1. 主表By Device + Program + Customer + Version唯一绑定一列（主键），副表By Type + KeyName +FileIndex唯一绑定一列（主键），因此这七个参数不能为空；
  2. 主表无此主键绑定的数据，同时插入主表和副表；
  3. 主表已存在主键绑定的数据，副表不存在主键绑定的数据，仅插入副表；
  4. 主表已存在主键绑定的数据，副表存在主键绑定的数据，副表By 主键更新Value+Remark，主表By 主键更新ModifyUser+ModifyUserID+ModifyDate;
  5. 副表为List格式，便于多条数据注册与更新；

- **路径：**api/TesterPGM/SetPGMValueRequest

- **请求参数示例：**

  ```
  {
      "Device":"F9QRKAD-P0AAC8Q001",
      "Program":"DRSY4G4A",
      "Customer":"JKL",
      "SubCustomer":"JKL",
      "Version":"3844QRKAS0009",
      "HandlerID":"50325QX",
      "UserID":"16698",
      "UserName":"李震",
      "DetilValue":[
          { 
              "Type":"PE",
              "FileIndex":"pefile",
              "KeyName":"ststg",
              "Value":"ft200",
              "Remark":"测试工站"
          },
          {
              "Type":"PGM",
              "FileIndex":"pgm01",
              "KeyName":"FSCK",
              "Value":"3",
              "Remark":"PGM"
          },
          { 
              "Type":"PE",
              "FileIndex":"pefile",
              "KeyName":"sttst",
              "Value":"t0311,t0312",
              "Remark":"测试工站"
          }
      ]
  }
  ```

- **返回参数示例：**

  ```
  {
      "ResultCode": "Y",
      "Message": "Insert success"
  }
  ```

