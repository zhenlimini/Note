## NIS Master-slave Rlease Process - Tester

##### 1.自动化程序更新

```shell
#登录账户后进入家目录
> cd ..
#查看当前机台生产过的客户，客户家目录为三码，目录权限为xxxop
> ll
# 【循环】切换到对应的客户账户，例如此时要更新otn的程序，则切换账户到otnop，登录密码为otn.ptn123其他客户密码都为三码客户别加上.ptn123
> su - otnop
#创建一个临时目录用于存放脚本程序和执行
> mkdir zhenli
> cd zhenli
#将脚本移动到当前目录 + 执行脚本
> cp /prod/ft/zhenli/Test.sh . && ./Test.sh
```

##### 2.时间检查程序更新

```shell
#切换root账户
> su - root
输入密码：SZPayton.321
#查看时间检查程序位置
> which updatetime
/usr/local/bin/updatetime
#备份原有程序
> cp /usr/local/bin/updatetime /usr/local/bin/updatetime_bak
#更新时间检查程序
> cp /prod/ft/common/ptnmenu_linux/updatetime /usr/local/bin/
```

##### 3.NIS主从配置

```shell
#以下操作仍然在root账户下进行
#更改NIS主从服务器配置
> vim /etc/yp.conf

--OpenSuse
#正常情况下此时会显示szheats旧服务器信息
ypserver 172.23.4.52
#将文件内容更改为新的主从服务器配置，如下
ypserver 172.23.5.59
ypserver 172.23.5.60
#重启机台客户端NIS服务
> rcypbind restart

--CentOS7
#正常情况下此时会显示szheats旧服务器信息
domain payton.com.cn server 172.23.4.52
#将文件内容更改为新的主从服务器配置，如下
domain payton.com.cn server 172.23.5.59 
domain payton.com.cn server 172.23.5.60
#重启机台客户端NIS服务
> systemctl restart ypbind
```

##### 4.校验配置结果

```shell
#检查当前NIS服务器指向
> ypwhich
#显示为如下任一个服务器为成功，其它服务器或者异常报错为失败
szlakers1
szlakers1-bak
#切换账户检查登录情况，测试本台机器的量产客户登录、切换均正常，例如
> su - otnop
> su - fnlop
#在可视化界面登录账户，防止登录默认shell配置异常
```
