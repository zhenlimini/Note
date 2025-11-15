## Tester自动化新框架需求分析

### 基本信息

- **代码架构： Java JDK1.5 Swing**
- **组成部分**
  - **GUI**：界面部分
  - **MCS**：机台逻辑处理部分	



### GUI

- **MainStartForm**

  - LotInfoPanel   Lot+工号输入界面

  - TestModePanel 测试模式选择界面

- **TestRecordCheckForm** 测试记录确认界面

- **TestQtyInputForm** 测试颗粒输入界面

- **FailTestSettingForm** 手动重测次数选择界面



### MCS

- **PGM**

  PGM下载、PGM/Table文件处理、测试模式、自动化程序配置

- **MES**

  MES信息获取、校验、信息存储

- **FILE**

  客户需求文件下载、自动化报表文件生成

- **DUT**

  DUT管控、设定、告警

- **FS&Handler**

  FS设定、获取；GPIB设定、获取

- **Machine**

  机台基本信息获取、校验、设定



---

MCS - PGM

1. **GetTableInfo**: 解密并上传Table文件，从MES获取Table信息并存储到`ProductionBO.cnfgTableBO`中。
2. **GetPgmInfo**: 解密并上传PGM文件，从MES获取PGM信息并存储到`ProductionBO.cnfgPgmBO`中。
3. **PrepareFssetuserpro**: 准备并写入`fssetuserpro.txt`文件，设置测试相关的路径和程序。
4. **JudgeSelect**: 根据选择的测试模式设置测试参数，并保存测试模式信息到MES。
5. **TestModeCheck**: 检查用户输入的测试模式是否合法，确保手动重测批次与测试目录下批次信息一致。
6. **CheckTestRecord**: 检查当前批次是否有初测记录，若有则提示用户确认是否继续初测。
7. **GetAndCheckHifix**: 读取并对比HIFIX信息，确保客户HIFIX与MES中的一致。
8. **PGMDownload**: 下载PGM文件，若为CNTY测试且用户为otnop则跳过。
9. **CheckPGMFileList**: 检查PGM下载的文件是否正确，确保测试路径下存在`release.asc`文件。



---

MCS - MES

1. **GetMESWipInfo**: 从MES获取WIP（在制品）信息并存储到`ProductionBO.mesLotInfoBO`中。
2. **PMRS**: 调用MES接口检查PMRS（生产管理资源系统）状态。
3. **PMRS_TEMP**: 调用MES接口检查临时PMRS状态。
4. **TempCheck**: 检查当前温度是否在MES设定的温度范围内，超出范围则抛出异常。
5. **SaveOtnStatusRecord**: 保存OTN状态记录到MES系统。
6. **StatusChange**: 更新设备状态并记录开始时间。
7. **SaveHifixToSystem**: 将Hifix信息保存到MES系统。
8. **CheckPgmverWithCheckin**: 检查当前程序版本是否与MES中的Checkin版本一致。
9. **CheckTempOffSET**: 检查温度偏移设置是否与MES一致。
10. **parseTempOffset**: 解析温度偏移数据并返回列表。
11. **CheckContactForce**: 检查接触力是否在允许范围内。
12. **Getotnwaferinfo**: 从MES获取客户LOT的Wafer信息并存储到`ProductionBO.mesLotInfoBO`中。
13. **GetPGMVersion**: 从MES获取当前程序版本并存储到`ProductionBO.mesLotInfoBO`中。
14. **CheckJobFileWithMes**: 检查Job文件是否与MES中的一致。
15. **CheckMCCheckin**: 检查当前设备是否与MES中的Checkin设备一致。
16. **CheckPgmParamWithMes**: 检查程序参数是否与MES中的一致。
17. **CheckSkipRetest**: 检查是否需要跳过重测，并设置相应的标志。
18. **CheckLastLotBin7Count**: 检查上一个LOT的BIN7数量是否符合要求。



---

MCS - FILE

1. **OTN_ManualFile_Download**: 下载OTN相关的DG、FTCS和F_DG文件到工作路径。
2. **BaseDirAddAndDel**: 创建并清理多个基础目录，删除指定类型的旧文件。
3. **DownloadChipInfoFile**: 下载CHIPINFO文件到工作路径。
4. **DownloadSFGFile**: 下载SFG文件到工作路径。
5. **GetOtnDGFileList**: 下载OTN的DG文件列表到工作路径。
6. **GetFT5FileList**: 下载FT5相关的文件到工作路径。
7. **GetJKRDBIFileList**: 下载JK* FT的RDBI数据文件到工作路径。
8. **GetLastPathFileList**: 下载JKL和JKH的PPJA、PPKA数据文件到工作路径。
9. **GetSite_mergeFile**: 下载site_merge文件到工作路径。
10. **CreateRecipeFile**: 通过Web服务创建Recipe文件。
11. **CreateBase_TD_DUTFile**: 通过Web服务创建Base和Tdraw文件，并调用`ptnCommDutOffFile`方法。
12. **ptnCommDutOffFile**: 将DUT关闭信息写入日志文件。



---

MCS - DUT

1. **DutOffWarning**: 检查DUT超标告警，若超标则提示技术人员输入工号确认。
2. **TDDutCloseDut**: 根据首次touch down结果关闭DUT，并设置相关符号。
3. **GetDutMapping**: 获取所有规则处理后的DUTMAP并更新到`ProductionBO.dutInfo`。
4. **GetHandlerDutInfo**: 获取Handler的DUT状态，并调整状态字符串长度以匹配预期格式。
5. **SetDutToMC**: 将DUT状态设置到机台，确保DUT控制命令执行成功。
6. **SaveBinMonitorInfo**: 保存Bin监控信息到MES系统。
7. **DRJOADutControl**: 若为DRJOA批次，则根据DUT状态生成二进制DUT字符串并写入文件。



---

MCS - FS&Handler


1. **FSStart**: 检查并启动FS系统，若未启动则尝试启动。
2. **CheckFSUser**: 检查FS系统的运行用户是否与当前用户一致，不一致则抛出异常。
3. **CheckFSMark**: 检查LOT_MARK标记并清除所有FS符号，设置工作目录。
4. **CheckFSPath**: 检查FS系统的当前路径是否与本地路径一致，不一致则抛出异常。
5. **FSClear**: 执行一系列FS命令以清除计数器、配置和用户程序设置。
6. **FSSet**: 设置FS系统的测试模式、工作路径、程序路径、Socket文件等，并启动测试程序。
7. **ComparePGMVer**: 比较OTN程序版本是否与MES中的一致，不一致则抛出异常。
8. **ResetFKInfo**: 重置FKINFO并清除计数器。
9. **SENDLOG**: 通过FTP下载日志配置文件，并根据重测次数设置FS日志功能。
10. **ptn_menu_fsset_log**: 通过FS命令设置日志功能，包括通过、失败、功能和DC日志的开关。
11. **ptnMenuFssetLog**: 根据DataLog设置FS日志功能，并启动日志记录。
12. **GetTTrayInfo**: 获取T-Tray的数量并更新到`ProductionBO.machineInfoVO`。
13. **GetHandlerType**: 获取Handler的类型并更新到`ProductionBO.machineInfoVO`。
14. **GetJobFile**: 获取Job文件名称并更新到`ProductionBO.machineInfoVO`。
15. **CheckFSAgain**: 再次检查FS系统，确保FKINFO设置正确，并设置CAL.INIT.标志。
16. **SetPWLevel**: 设置Handler的密码等级（仅适用于M6242和M6243类型）。
17. **HandlerDataClear**: 清除Handler的LOT、重测和ACM重测数据。
18. **HandlerSet**: 根据测试模式和Handler类型设置Handler参数，启动Handler。
19. **auto_stocker_set**: 获取自动存储器的设置并应用到Handler。
20. **FirstTrayCheck**: 执行First Tray检查功能，确保第一个Tray正确。
21. **DoubleTestSet**: 设置DTESTMODE和DOUBLETEST。
22. **FSSymbolInfoAdd**: 将MES信息添加到FS符号中，如ST_STEP、RETEST_NUM等。
23. **AutoLotEnd**: 自动设置LOT结束功能，适用于M6242和M6243类型的Handler。



---

MCS - Machine

1. **CheckProcess**: 检查是否有其他TesterGUI进程正在运行，若有则抛出异常。
2. **ChangeWorkingDir**: 根据LOTNO更改工作目录，并更新到`ProductionBO.machineInfoVO.WorkPath`。
3. **GetConstantData**: 从MES获取测试机的常量数据并存储到`ProductionBO.config`。
4. **GetMachineBaseInfo**: 获取机器基本信息，包括主机名、HandlerIP等，并更新到`ProductionBO.machineInfoVO`。
5. **CheckMachineTime**: 检查机器时间是否与服务器时间同步，若超过5秒则抛出异常。
6. **CheckMachineStatus**: 检查机器状态是否可用，若不可用则抛出异常。
7. **CheckMachineConfig**: 检查机器配置信息，包括FS版本、系统版本等，并与MES进行比对。
8. **CopyPBIDFile**: 复制PBID文件到当前工作目录。
9. **CheckSiteStatus**: 检查Site状态，确保所有Site都已初始化。
10. **MenuShow**: 根据环境变量设置菜单显示类型。
11. **CheckDiskFree**: 检查磁盘剩余空间，若低于配置的阈值则抛出异常。
