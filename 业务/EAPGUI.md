### 开发手册

#### EAP

##### CMHostCommon

- ParsingAndSendMsg  GUI->EAP数据处理
  - 增加对于TxName的判定；指定ScenarioName;
- XXXXReply
  - 增加返回给GUI的信息格式处理



##### COMESInterface

- EventHandler
  - 增加对于Reply关键字的判定，并调用CMHostCommon.XXXXReply



##### Program

- 增加静态关键字，分别表示Relpy和Send的信息



##### SdiEqpSpecificScenarioSupport

- 声明与触发scenario



##### Scenario_MES_XXXXX/Scenario_EQS_XXXXX

- 新建一个Scenario；
- MES
  - 处理来自GUI的信息并且组成与SMD相同的格式
  - 处理来自HOST的信息并组成想要发送给