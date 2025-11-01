### Linux机台 .Netcore环境搭建

------

#### 1.基础信息

- SDK版本：NET Core SDK for CentOS7 3.0.103

- 本地系统版本：CentOS Linux release 7.4.1708 (Core) 

- [SDK包官方地址](https://dotnet.microsoft.com/download)

- 运行时版本：

  Microsoft.AspNetCore.App 3.0.3 [/usr/local/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.NETCore.App 3.0.3 [/usr/local/dotnet/shared/Microsoft.NETCore.App]

- [官方参考文档](https://learn.microsoft.com/zh-cn/dotnet/core/install/linux-centos)



#### 2.搭建步骤

*由于机台无法直接联网使用在线安装方式，此处采用的是离线解压安装*

1. 切换至超级账户执行操作

   ```
   su - root
   ```

2. 切换路径

   ```
   cd /usr/local
   ```

3. 新建文件夹

   ```
   mkdir dotnet
   cd ./dotnet
   ```

4. FTP下载文件至目标路径

5. 解压压缩文件

   ```
   tar zxf dotnet-sdk-3.0.103-linux-x64.tar.gz .
   ```

6. 建立软链接

   ```
   ln -s /usr/local/dotnet/dotnet /usr/bin
   ```

7. 检查dotnet信息

   ```
   dotnet --info
   ```

   