## NIS主从服务操作说明

[TOC]

#### 1.NIS服务信息

- NIS主从模式需要在客户端配置两个NIS服务器信息，任一服务器出现异常时，会自动切换至另一台可用服务器；

- 当前SZ厂区的NIS主从服务器分别为**szlakers1；szlakers1-bak**；

- 新机台、账户导入操作只需要在**主服务器**szlakers1上操作，主服务器会将数据推送到从服务器；

  

#### 2.新机台导入

- 使用NISRlease工具进行导入，工具会同步数据关联**机台磁盘监控**；

- NISRlease工具所在路径：szlakers1 ： /auto/NISRleaseTool.sh

- NISRlease工具逻辑：

  1. 根据用户输入的IP和Host更新/etc/hosts文件；

  2. 更新机台磁盘监控机台清单文件machine_list.txt；

  3. 将Hosts文件与machine_list.txt通过FTP的模式更新到监控服务器szpistons；

  4. 通过到/var/yp make刷新NIS信息，此操作也会将信息同步到从服务器；

     

- 新机台导入服务端操作流程

  ```shell
  > cd /auto
  > ./NISRleaseTool.sh
  按照引导提示输入IP和Hostname即可
  ```

- 新机台导入客户端（机台端）配置

  ```shell
  #以下操作在root账户下进行
  #更改NIS主从服务器配置
  > vim /etc/yp.conf
  
  --OpenSuse
  #将文件内容更改为新的主从服务器配置，如下
  ypserver 172.23.5.59 
  ypserver 172.23.5.60
  #重启机台客户端NIS服务
  > rcypbind restart
  
  --CentOS7
  #将文件内容更改为新的主从服务器配置，如下
  domain payton.com.cn server 172.23.5.59 
  domain payton.com.cn server 172.23.5.60
  #重启机台客户端NIS服务
  > systemctl restart ypbind
  
  #验证当前NIS服务器是否配置成功
  > ypwhich
  #显示为如下任一个服务器为成功，其它服务器或者异常报错为失败
  szlakers1
  szlakers1-bak
  ```



#### 3.新量产账户导入

- 新账户导入NIS操作流程

  ```shell
  #使用以下命令以 root 用户身份执行 useradd 命令;
  #将 <username> 替换为你想要设置的用户名，将 <password> 替换为用户的密码，将 <uid> 替换为用户的 UID，将 <home_directory> 替换为用户的家目录路径，将 <login_shell> 替换为用户的登录 Shell
  > useradd -u <uid> -d <home_directory> -s <login_shell> <username>
  eg:
  > useradd -u 61001 -d /export/home/hac -s /bin/bash hacop
  #接下来，设置用户的密码。执行以下命令，将 <username> 替换为之前创建的用户名：
  > passwd <username>
  eg:
  > passwd hacop
  
  #通常系统会默认创建一个同名的组（Centos7），如果对于组没有特殊要求则无需进行下列操作
  ----
  #如果限定组ID，请使用如下指令更改
  >  groupmod -g <newgroupid> <groupname>
  ----
  
  #刷新NIS
  > cd /var/yp && make
  ```

  

#### 4.其他说明

- ##### CentOS7与OpenSuse加密方式不同导致新注册用户登录密码错误：

  1. /etc/shadow中保存着当前服务器的账户密码信息，其中明显可以发现CentOS7直接创建的加密后的密码规则与其它不同；通常，加密方式会以一个或多个标识符开头，例如 $1$ 表示 MD5 加密，$6$ 表示 SHA-512 加密等

  2. 此时直接登录这个账户时，账户虽然存在，但是却登录密码时显示错误；

  3. 这是由于SHA-512 是 CentOS 7 默认的密码加密算法，导致加密后的不适用于其它不使用加密的系统；

  4. 此时可以删除相关加密算法来实现；

     vim /etc/pam.d/system-auth

     删除红框中的内容后续注册信息即回归正常；
