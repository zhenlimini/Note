### Git搭建使用教程

------

#### 1.Git软件的安装

Git工具的中文镜像地址：

[镜像地址]: https://npm.taobao.org/mirrors/git-for-windows/



#### 2.Git的初始化

配置邮箱及用户名

```
git config --global user.name "Your Name"`（注意前边是“- -global”，有两个横线）
git config --global user.email "email@example.com"
```



#### **3.GitHub SSH配置**

[SSH配置教程]: https://blog.csdn.net/weixin_41087220/article/details/118100443



#### 4.Git Bash

- 初始化

```
git init
```

- 远端仓库绑定

```
git remote add + 名字 + 连接地址
git remote -v
git remote remove origin
```

- 仓库克隆

```
git clone git@github.com:LiZhen999999/Personal.git
```

- 提交

```
git add
git commit -m "修改注释"
```

*Git Add*

```
git add +文件名.文件类型 ，将某个文件加到缓存区
git add +文件名.文件类型 ... 文件名.文件类型 ，将n个文件添加到缓存区
git add xx文件夹/*.html，将xx文件夹下的所有的html文件添加到缓存区。
git add *hhh ，将以hhh结尾的文件的所有修改添加到暂存区
git add Hello* ，将所有以Hello开头的文件的修改添加到暂存区
git add -u ，提交被修改(modified)和被删除(deleted)文件，不包括新文件(new)
git add .，提交新文件(new)和被修改(modified)文件，不包括被删除(deleted)文件
…
git add -A，提交所有变化。
```

- 拉取

```
git pull 仓库名称 分支
git fetch + git merge
```

- 推送

```
git push -u 仓库名称 分支
git push 名称 分支
```



https://blog.csdn.net/weixin_41087220/article/details/118099800

https://blog.csdn.net/weixin_41087220/article/details/118100443
