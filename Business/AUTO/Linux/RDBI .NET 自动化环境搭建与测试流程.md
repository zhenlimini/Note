## RDBI .NET 自动化环境搭建与测试流程

#### 1.熟悉功能点、系统架构，请参考以下文档：

http://szspapp1/SharedDocLib/Forms/AllItems.aspx?RootFolder=%2FSharedDocLib%2F%E6%B2%9B%E9%A1%BF%E7%A7%91%E6%8A%80%EF%BC%88%E6%B7%B1%E5%9C%B3%EF%BC%89%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8%2FMIS%2FCIM%2FAutomation%2FAutomation%2F1%E3%80%81%E9%87%8D%E7%82%B9%E9%A1%B9%E7%9B%AE%E7%AE%A1%E7%90%86%2F%E9%A1%B9%E7%9B%AE%E6%96%87%E4%BB%B6%2FRDBI%E8%87%AA%E5%8A%A8%E5%8C%96%E4%BB%A3%E7%A0%81%E9%87%8D%E6%9E%84



#### 2.程序部署流程

- ##### 文件配置信息修改

  AutomationCall：修改自动更新.NET程序的FTP地址及账户密码；

  AutomationForRDBI：修改配置文件（App.config）中的WEBAPI地址；

  RDBIAutomationWebAPI：修改配置文件（web.config）中的数据库链接；

- ##### WEBAPI搭建

  生成代码无异常后，部署到IIS，深圳地址为 szautoweb:6011 ; 建议部署到镜像的HF地址（hfautoweb:6011）;

- ##### 客户端程序生成

  客户端程序生成后，部署到FTP；深圳FTP路径为（/prod/ft/common/RDBIAutomation）；HF建议部署到同一地址；

- ##### 数据库配置信息修改

​		检查每一笔配置化信息，调整为对应的HF环境（包括但不限于：IP地址、账户密码、厂别信息等）；



#### 3.机台环境搭建流程

- 参照如下文档在HF机台搭建.NET运行环境：

  [Linux机台 .Netcore环境搭建.pdf](http://szspapp1/_layouts/15/WopiFrame.aspx?sourcedoc={8375450A-180E-4DDF-8119-F0C03E05E514}&file=Linux机台 .Netcore环境搭建.pdf&action=default)



#### 4.测试流程

- ##### 提前确认如下条件是否满足
  
  1. 自动化客户端程序：/prod/ft/common/RDBIAutomation/Program下存放了.NET的dll及相关程序；
  2. 自动化呼叫程序：/prod/ft/common/RDBIAutomation/Program/AutoCall下存放了test_start&test_end；
  3. WEBAPI程序：hfautoweb:6011机台能都否正常访问；
  4. 机台安装了dotnet环境，机台测试路径放置了新版本的menu程序；
- ##### 开批测试
  
  1. 测试开批过程中无报错；
  2. 重点检查Recipe文件、bmap文件、ld文件内容；
  3. 带出的测试UI显示信息正确；
- ##### Pretest
  
  1. Pretest过程中、开始结束无报错；
  2. Pretest结束正确带出测试结果、Summary信息；
- ##### Maintest
  
  1. 用户点击maintest开始无报错；
- ##### LotEnd
  
  1. Lotend过程中无报错；
  2. 解析测试报表结果、分BIN数量、UI显示正常；
  3. 回传客户文件正常、Fmerge结果正常；
  4. TBIE PretestMap、BI Map信息正常，卸板无报错；