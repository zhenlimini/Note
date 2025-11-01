# 【B】GitHub教程 Git Bash详细教程

[![CSDN首页](https://img-home.csdnimg.cn/images/20201124032511.png)](https://www.csdn.net/)

- [博客](https://blog.csdn.net/)
- [下载](https://download.csdn.net/)
- [学习](https://edu.csdn.net/)
- [社区](https://bbs.csdn.net/)
- [GitCode ![img](https://img-home.csdnimg.cn/images/20221027045535.png)](https://gitcode.net/csdn_codechina/2.14-3.14?utm_source=csdn_toolbar)
- [云服务](https://dev-portal.csdn.net/welcome?utm_source=toolbar)
- [猿如意](https://devbit.csdn.net/?source=csdn_toolbar)



 搜索

[![img](https://profile.csdnimg.cn/7/A/1/2_m0_74414426)](https://blog.csdn.net/m0_74414426)

[会员中心 ![img](https://img-home.csdnimg.cn/images/20210918025138.gif)](https://mall.csdn.net/vip)

[消息](https://i.csdn.net/#/msg/index)

[历史](https://i.csdn.net/#/user-center/history)

[创作中心](https://mp.csdn.net/)

[发布](https://mp.csdn.net/edit)

# GitHub教程 Git Bash详细教程

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/reprint.png)

[云想衣裳，花想容](https://blog.csdn.net/weixin_41087220)![img](https://csdnimg.cn/release/blogv2/dist/pc/img/newCurrentTime2.png)于 2021-06-22 09:30:42 发布![img](https://csdnimg.cn/release/blogv2/dist/pc/img/articleReadEyes2.png)2827![img](https://csdnimg.cn/release/blogv2/dist/pc/img/tobarCollect2.png) 收藏 21

分类专栏： [未来之路](https://blog.csdn.net/weixin_41087220/category_11151685.html) 文章标签： [git](https://so.csdn.net/so/search/s.do?q=git&t=all&o=vip&s=&l=&f=&viparticle=)

版权

[![img](https://img-blog.csdnimg.cn/20201014180756925.png?x-oss-process=image/resize,m_fixed,h_64,w_64)未来之路专栏收录该内容](https://blog.csdn.net/weixin_41087220/category_11151685.html)

6 篇文章0 订阅

订阅专栏

------



### 文章目录

- [1 下载安装](https://blog.csdn.net/weixin_41087220/article/details/118099800#1__7)
- [2 设置用户](https://blog.csdn.net/weixin_41087220/article/details/118099800#2__22)
- [3 本地文件夹的操作](https://blog.csdn.net/weixin_41087220/article/details/118099800#3__29)
- - - - [3.1 进入文件夹](https://blog.csdn.net/weixin_41087220/article/details/118099800#31__30)
      - [3.2 查看](https://blog.csdn.net/weixin_41087220/article/details/118099800#32__43)
      - [3.3 退出文件夹](https://blog.csdn.net/weixin_41087220/article/details/118099800#33__50)
      - [3.4 新建、删除](https://blog.csdn.net/weixin_41087220/article/details/118099800#34__56)
  - [4 仓库设置](https://blog.csdn.net/weixin_41087220/article/details/118099800#4__69)
  - - - [4.1 初始化本地仓库](https://blog.csdn.net/weixin_41087220/article/details/118099800#41__73)
      - [4.2 新建远程仓库](https://blog.csdn.net/weixin_41087220/article/details/118099800#42__79)
      - [4.3 建立连接](https://blog.csdn.net/weixin_41087220/article/details/118099800#43__89)
      - [4.4 文件上传](https://blog.csdn.net/weixin_41087220/article/details/118099800#44__114)
      - [4.5 文件下拉](https://blog.csdn.net/weixin_41087220/article/details/118099800#45__181)
      - [4.5 文件克隆](https://blog.csdn.net/weixin_41087220/article/details/118099800#45__222)



------

**这个主要介绍Git Bash的使用教程。**

# 1 下载安装

首先抛一个Windows用户的下载链接：[Git for windows](https://git-scm.com/download/win)
下载下来之后直接安装，除了下图选第一个，其他的不用改，直接next就行。
下图这个是决定**是否把git命令放到path中**，你不用理解path是什么，反正你如果你加入到path之后，gitbash可能会变得反应特别慢。
选这个`use git from git bash only`就是告诉他不要给我加到path中
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200427140725822.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)
安装完成后我的电脑上是显示了这么三个东西的。
![这里写图片描述](https://imgconvert.csdnimg.cn/aHR0cDovL2ltZy5ibG9nLmNzZG4ubmV0LzIwMTgwMTE3MTQwNjQ4Mjg2?x-oss-process=image/format,png)

- Git CMD：
  　　Git CMD我并没用过，但是我查了一下它是什么。据别人说，Git中的Bash是基于CMD的，在CMD的基础上增添一些新的命令与功能。所以建议在使用的时候，用Bash更加方便。
- Git GUI：
  　　其次就是Git GUI，Git GUI是Git Bash的替代品，他为Windows用户提供了更简便易懂的图形界面。（但是相比GitHub Desktop这个桌面版客户端，_(:3 」∠)我觉得Git GUI也没什么用。）
- Git Bash：
  　　最后是Git Bash，Git Bash是命令行操作，官方介绍有一句就是“让*nix用户感到宾至如归。”（(;´༎ຶД༎ຶ`) 当然了，萌新用户使用了就肥肠憋屈。）

------

# 2 设置用户

下载之后打开是这个样子的，**第一件事设置用户**。注意这个不是登录哦，是给你的电脑设置一个用户，等你上传的时候，告诉远程仓库是谁上传的而已。

`git config --global user.name "Your Name"`（注意前边是“- -global”，有两个横线）
`git config --global user.email "email@example.com"`
![在这里插入图片描述](https://img-blog.csdnimg.cn/20190221153029477.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)

------

# 3 本地文件夹的操作

#### 3.1 进入文件夹

**首先你可以试着打开你本地仓库的文件夹。**
比如我要打开E:\code有两种方法

1. 直接在电脑上打开那个文件夹，然后在文件夹空白处右键选择Git Bash here
   ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425212231974.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)
2. 在Git Bash中输入路径
   **注意！** 使用cd命令进入到目录中时，在Git-Bash中应该使用斜线”/”， 而不是反斜线”\”
   ①可以逐个输入文件夹名（在文件夹名称前要加cd ）
   ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425212622870.png)
   ②也可以直接输入一个完整的文件夹路径
   ![在这里插入图片描述](https://img-blog.csdnimg.cn/2020042521274988.png)

#### 3.2 查看

- 当前目录
  你输入命令之前上边有一行字，后边那段黄色的就是你所在的文件夹位置。你也可以输入`$ pwd`，回车之后进行查看。![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425212932504.png)
- 查看当前文件夹都有什么文件`$ ls`
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425214521860.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)

#### 3.3 退出文件夹

**当然你也可能进错文件夹，要学会回退。**
`$ cd ..` 点和cd之间有空格

就可以回退到上一个文件夹。
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425213155563.png)

#### 3.4 新建、删除

`$ mkdir +文件夹名字` 只能新建文件夹
我在E盘的code文件夹下新建一个front-end文件夹。建完之后打开文件夹看看创建成功了嗷。
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425213412889.png)
`touch +文件名` 只能新建文件
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425213838737.png)

`$ rm 文件名.文件类型` 删除文件
![在这里插入图片描述](https://img-blog.csdnimg.cn/2020042521420798.png)
`$ rm -r 文件夹`删除文件夹 ，注意这个要回到上一级文件夹才可以删。比如我要删除front-end文件夹，front-end在code里边，我就要在code目录下删除。
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425215023480.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)

------

## 4 仓库设置

你既然学git，那就是想要把本地的代码放到远程仓库托管。
托管就是，代码是小朋友，你就是他父母，你把它丢到托儿所，让托儿所帮你管。怎么去托儿所，总不能一生下来就在托儿所。你得把孩子从家里送过去吧。放学了你得把孩子接回来吧。（当然这个例子不太恰当。）
那你需要一个本地存储代码的地方（家里），你还需要一个远程仓库（托儿所）

#### 4.1 初始化本地仓库

进入到你想建立本地仓库的文件夹，它可以是空的，你建好了之后再写代码。里边也可以有东西，直接建就好。
`$ git init`
我用个空文件夹做示范：E:\code\front-end
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425215413426.png)
初始化成功之后，你的文件夹里就会多出.git的隐藏文件。 (●′ω`●)千万不要乱删，你如果看他烦你就设置一下不显示隐藏文件。

#### 4.2 新建远程仓库

打开[github](https://so.csdn.net/so/search?q=github&spm=1001.2101.3001.7020)右上角，点击new repository
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425220537666.png)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425221100741.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)

- **说一下仓库的私有和公有**
  本来也是个开源社区，很多大佬们都把自己的东西开源了，也就是放在共有仓库里，可以让人查阅。也鼓励大家使用共有仓库 (๑•́ ₃•̀๑)。
  以前使用私有仓库是付费， 或者你可以**申请学生认证**获得私有仓库的使用权。但是现在2020年3月份的时候我收到github的邮件，邮件里边说现在已经开放私有仓库的使用了。
- **说一下学生认证**
  以前github的教育认证可以让学生和教育者免费使用私有仓库，并且还有许多其他的优惠政策。比如github的一些付费功能，教育认证之后会有巨大的折扣。

#### 4.3 建立连接

孩子在家里，你能用意念让他直接飞到托儿所吗，显然不可能，那你总得把他送过去，或者用校车之类的吧。
那现在就得想办法建立远程仓库和本地仓库的连接。
**4.3.1** 配置SSH，给孩子联系好校车。
[ヽ(･ω･｡)ﾉ点击进入《SSH Key配置教程》](https://blog.csdn.net/weixin_41087220/article/details/118100443)
**4.3.2** 配置完SSH，你就可以使用SSH连接了。
注意，你是仓库的主人你才能使用SSH连接，如果你不是仓库主人，只是某个项目的成员，那你只能使用HTTPS连接。
不管使用哪一种连接方式，都是一样操作，现在我就用SSH链接了。复制红框框里的代码。
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425223427978.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)
**4.3.3** 在你的本地仓库打开gitbash。
`$ git remote add + 名字 + 连接地址`
连接地址就是你刚才复制的那块。
我下边写的就是添加一个叫origin的远程仓库。
![在这里插入图片描述](https://img-blog.csdnimg.cn/2020042522372583.png)

- 名字origin：你在往远程仓库推送的时候，你会说我要推给谁，总得给它起个名字。（你把孩子送去托儿所，你总得告诉司机是哪个托儿所吧）并且你以后可能会一个**本地仓库连接多个远程仓库**（这是后话），所以必须起名字加以区分。
- 补充一下：你的本地仓库可以链接多个原厂仓库，github毕竟是国外的，有时候访问起来会很慢，因此你可以连接到国内的仓库上，比如gitee之类的。

**4.3.4** 添加之后没有任何提示，如果你想确定是否成功了，你可以再输一遍，这时候他会提示你刚才已经设置过了。
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425224311204.png)
上边的方法能用但是比较笨(…•˘_˘•…)，所以你得学个高端一点的。
`$ git remote -v`
测试一下，看到没。显示我已经添加了叫origin的仓库。一个push一个fetch，就是一个把代码推到远程仓库，一个把代码从远程仓库取回来。这两个一定是成对存在的。
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425224532801.png)
**补充一点**：如果你以后不想连接这个远程仓库了，只需要输入`git remote remove + 名字`即可。比如我刚才添加的远程仓库代称是origin，那我就要写：`git remote remove origin`

#### 4.4 文件上传

**4.4.1 git add** 将**修改的文件**添加暂存区，也就是将要提交的文件的信息添加到索引库中。（看不懂没关系，现在来说这个不重要）。
什么是修改的文件，你新建、更改、删除文件都是修改。
git add有好多种。下边我介绍一下，**看看就行，对现在来说记住最后一条就可以了**：

- `$ git add +文件名.文件类型` ，将某个文件加到缓存区
- `$ git add +文件名.文件类型 ... 文件名.文件类型` ，将n个文件添加到缓存区
- `$ git add xx文件夹/*.html`，将xx文件夹下的所有的html文件添加到缓存区。
- `$ git add *hhh` ，将以hhh结尾的文件的所有修改添加到暂存区
- `$ git add Hello*` ，将所有以Hello开头的文件的修改添加到暂存区
- `git add -u` ，提交被修改(modified)和被删除(deleted)文件，不包括新文件(new)
- `git add .`，提交新文件(new)和被修改(modified)文件，不包括被删除(deleted)文件
- …
- **`git add -A`**，提交**所有变化**。git add前几条都可以记不住，这个必须记住！！！

![在这里插入图片描述](https://img-blog.csdnimg.cn/2020042523142923.png)
我现在在本地仓库新建一个文件叫readme.md，现在我将它添加到缓存区。（没错虽然图里是read，但是我就是要创建叫readme的文件，往后看就明白了）
![在这里插入图片描述](https://img-blog.csdnimg.cn/2020042523160420.png)

**4.4.2 git commit**命令将索引的当前内容与描述更改的用户和日志消息一起存储在新的提交中。（看不懂没关系，现在来说这个不重要）。**你现在可以简单的理解为给你刚才add的东西加一个备注，你上传到远程仓库之后，修改的文件后边会显示这个备注**
`$ git commit -m "修改注释"`
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425231655738.png)
一定要加`-m`，否则会进入vim编辑器。对于不熟悉的人来说会很麻烦。所以还是加上`-m`。
**4.4.3 git push**激动的搓搓小手，终于要把文件推到远程仓库了。
向一个空的新仓库中推文件：`$ git push -u 仓库名称 分支`

- 仓库名称：刚才我添加连接的时候，给仓库起名叫origin
- 分支：你现在只有主分支，所以分支直接写master。以后合作项目的时候，成员之间建了不同的分支，你就可以往你自己的分支上推。

![在这里插入图片描述](https://img-blog.csdnimg.cn/2020042523273249.png)
我们第一次推送master分支时，加上 `–u`参数才会把本地的master分支和远程的master分支关联起来，就是告诉远程仓库的master分支，我的本地仓库和是对着你的哦，不是对着别的分支的哦。
只有第一次推的时候需要加上`-u`，以后的推送只输入：
`$ git push 名称 分支`
还有一个`$ git push origin master -f` 强制推送，如果你某次推送失败，git bash报错，你懒得处理错误，你就可以用这个。但是有风险，因为报错90%是因为你本地仓库和远程仓库数据发生冲突，使用这个会直接用本地数据覆盖掉远程数据，可能损失数据哦。

现在你去网页版刷新一下，就可以看到你本地仓库的东西都在那里了。并且文件后边写着你在commit步骤中添加的注释。
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425232830821.png)
**4.4.4 其他**
(눈_눈)我缓缓打出一个问号，我的commit怎么多写了一个e？我文件名字叫readme我怎么就写了read
先来看看怎么查看自己的提交记录？虽然写错了查看提交记录也没用。我就是单纯想让你们多学一条命令：
`$ git log`
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425233153587.png)
提交记录里也显示我就是多写了一个e。

- **怎么抢救一下commit的注释？**
  `$ git commit --amend -m "修改的内容"`
- **那怎么抢救一下文件名？**
  直接修改文件名重新提交就可以啦。
  git add -A —> git commit -m “修改文件名” —> git push origin master
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200425234758848.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)
  **？push时候报错？**
  这是因为github觉得我的本地仓库和远程仓库冲突了：
  因为我刚才在本地修改了上一次的commit信息。（**后边详细解释**）
  `$ git push origin master -f` 这个`-f`就是force，强制推送。

推完之后看看你的远程仓库，文件名改了，文件名后边的注释也是我第二次commit的注释。上边有个commit选项，
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426000055519.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426000141998.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)

- 补充解释

  - `$ git commit --amend`的作用：
    github不管你做错了啥，他都会给你保存的，就是即使你改了，你的错误记录永远存在！但是使用git commit --amend，你可以神不知鬼不觉悄咪咪修改你的错误commit注释，╭(●｀∀´●)╯只有天知地知你知。

  - push时候报错：
    github你可以理解为差额备份，就是你本地提交上去之后，它备份起来。你本地修改了，它会对你修改的部分继续备份。**也就是说在你这次修改之前，本地仓库应该和远程仓库一模一样。**
    但是我刚才强行修改了**上次**的commit注释信息。**现在**本地仓库里；

    - 上次的commit是“新建了readme”，
      使用git log看一下，本地仓库上次的提交注释确实是改变了。![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426000758827.png)
    - 但是远程仓库上次的commit是“readmee”。

    我修改的是**上次**的commit，所以我**这次**推的时候github就认为这次修改之前的本地仓库和远程仓库不一样，因此就会报错说我数据冲突。

#### 4.5 文件下拉

上边push报错，我自己知道数据差在哪里，所以使用了强制推送。但是在团队合作中，push报错，那铁定是你队友修改了远程仓库，如果你再强制上传，那你就是毁了你队友的代码。所以如何保证在你修改之前，自己的文件跟远程仓库一致呢。
**方法1：** `$ git pull 仓库名称`

**尝试一下**
比如我现在跑到我的远程仓库修改了readme：

- 点要修改的文件，进去之后点击编辑。
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/2020042600232964.png)
- 写内容
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426002415930.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)
- 写完内容提交
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426002504415.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)
- 这时候我已经修改成功了
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/2020042600253253.png)
- git pull完成之后打开本地的readme，发现hello world已经进来了嗷。
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426002753886.png)
- git log看一下，commit的记录也显示了。
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426003150161.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)

**方法2：**
`$ git fetch` + `$ git merge`

**尝试一下，**

- 这次我又在远程仓库加一行字
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426003311703.png)
- git fetch，看起来数据也是拉下来了，要 git merge干嘛。**然鹅！** 事情是这样的，git fetch之后，我打开本地文件，发现内容没变
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426003343639.png)
- 那继续git merge，这之后本地文件内容才改变！
  ![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426003557953.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)

我在提示一下，我在fetch之后，不止没有修改本地文件，就连git log也没显示我下拉文件了，但是merge之后就都显示了。**我们可以认为 pull = fetch+merge。git fetch 并没更改本地仓库的代码，只是拉取了远程数据，git merge才执行合并数据。**

回想一下你刚才是怎么push到远程的

- git add添加到上传缓存区
- git commit给缓存区的内容添加备注，此时本地的commit修改啦，但是远程的commit和文件都没修改。
- git push 修改远程文件和commit信息

而你下拉文件过程

- git fetch 将数据拉下来，但是没修改本地的commit和文件
- git merge 改变本地数据

#### 4.5 文件克隆

下拉仓库学会了，那克隆呢？
克隆就是你本地上没有，你直接把远程仓库的东西搞下来。
我现在有一个完整仓库，点击右边的绿色按钮。
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426005441787.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426005607223.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzM2NjY3MTcw,size_16,color_FFFFFF,t_70)

- 如果你只想看看源码，那你可以直接选download zip，下载源码压缩包。
- 如果你使用的是git desktop，那你就选open in desktop
- 如果你想学克隆你就继续看，很简单的就一句。

仓库是你自己的，你就使用SSH连接，不是你自己的，你没权限你就切换到HTTPS，再复制地址。
它克隆下来是一个文件夹，你想把文件夹放哪里就在哪打开gitbash
`$ git clone 加上你刚才的地址`
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426010529711.png)
我在E:\code\vue里执行了克隆。
(｡◕ˇ∀ˇ◕）。下载完成后，打开这个文件夹，就发现里边有个文件夹了。文件夹名字就是远程仓库的名字。
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426010757476.png)
还记得git remote -v吗？用它看一下你下下来的本地仓库连接上那个远程仓库没。
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200426011008859.png)
已经连接了嗷。放心使用吧。

------

## 本文为转载文章，转载仅做保存使用，尊重原创

版权声明：本文为博主原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接和本声明。
本文链接：https://blog.csdn.net/qq_36667170/article/details/79085301
————————————————
版权声明：本文为CSDN博主「LolitaAnn」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。

文章知识点与官方知识档案匹配，可进一步学习相关知识

[Git技能树](https://edu.csdn.net/skill/git/?utm_source=csdn_ai_skill_tree_blog)[首页](https://edu.csdn.net/skill/git/?utm_source=csdn_ai_skill_tree_blog)[概览](https://edu.csdn.net/skill/git/?utm_source=csdn_ai_skill_tree_blog)3522 人正在系统学习中

[![img](https://profile.csdnimg.cn/A/8/3/3_weixin_41087220)云想衣裳，花想容](https://blog.csdn.net/weixin_41087220)

[关注](javascript:;)

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/newHeart2021Black.png)4
- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/newUnHeart2021Black.png)
- [![img](https://csdnimg.cn/release/blogv2/dist/pc/img/newCollectBlack.png)21](javascript:;)
- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/newComment2021Black.png)0
- 
- [![img](https://csdnimg.cn/release/blogv2/dist/pc/img/newShareBlack.png)](javascript:;)

专栏目录

*Git**教程* *Git* *Bash**详细**教程*

[https://lolitasian.github.io/](https://blog.csdn.net/qq_36667170)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 17万+

　　作为一个萌新，我翻遍了网上的*Git* *Bash**教程*，可能因为我理解力比较差，经常看不懂*教程*上在说什么。 (。-`ω´-)所以我决定自己一边摸索一边记录，写*教程*造福那些理解力跟我一样差的人…… 第一篇*教程*会涉及如下内容（按照一般人的使用流程）： 下载、登录*Git* *Bash* 如何在*Git* *Bash*中进入或者退出文件夹 如何建立本地仓库 配置SSH key 如何建立本地仓库和远程仓库的连接 ...

*Git* *Bash* - *bash*: *git*: command not found

最新发布

[mango46的博客](https://blog.csdn.net/mango46)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 1741

*Git* *Bash* - *bash*: *git*: command not found



Windows *git*-*bash*安装与使用_bitcsljl的博客_*git*-*bash*

1-22

*git*-*bash*是一个适用于Microsoft Windows环境的应用程序,它为*Git*命令行体验提供了一个仿真层;相当于在window上通过*git* *bash*这个模拟的Unix命令行的终端做*git*相关的版本控制。Windows的*git*安装包自带*git*-*bash*软件。 下载*git* 下载链接:*Git*-...

*git*中*Bash*基本操作命令_正怒月神的博客_*git**bash*指令

2-5

*git* remote -v:远程库*详细*信息 *git* branch -r , *git* branch -a 查看远程分支 *git* push 将当前分支推送到远程对应的分支(若远程无对应分支,则推送无效) *git* push origin dev 将分支dev提交到远程origin/dev(远程没有则创建, 远程...

*github*与*Git* *bash**详细**教程*

[Beibei1127zhao的博客](https://blog.csdn.net/Beibei1127zhao)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 907

** *github*与*Git* *bash**详细**教程*** 第一步下载成功 *Git* *bash*以后先在本地创建ssh key（秘钥） 接下来就直接写 *Git* *bash* 命令行了 创建ssh key 命令行： ssh-keygen-t rsa-C “yonghuming” , &quot;your_email@youremail.com&quot;（引号后为创建*github*的用户名和邮箱地址） 大概三次回车以后会生成秘钥 秘钥就在...

*git*使用*教程**git*-*bash*（ssh版）*GitHub*远程仓库连接

[qq_45122568的博客](https://blog.csdn.net/qq_45122568)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 3255

食用指南一 下载安装*git* *bash*二 创建*github*仓库三 SSH keys配置二级目录三级目录 *git*有两种连接方式：ssh和https。 ssh使用前连接配置好SSH key后就不需要每次都输入账号密码，比较方便 一 下载安装*git* *bash* 下载连接：*git*官网 安装*教程*可以参考这篇写的非常的*详细*： https://blog.csdn.net/sanxd/article/details/82624127（*git*下载安装和*github*账号注册） 二 创建*github*仓库 创建*github*账号后，

*Git* *bash*使用方法(简单,有图有代码上手快)_kunjar的博客_*git*...

2-2

一、打开要上传的文件夹 在此处右键单击*git* *bash*: 二、初始化*git*仓库 代码如下: *git* init 下面是我当时的操作: 三、打开自己的*git*仓库地址 代码如下(示例): *git* remote add origin https://*github*.comxxxxxxx 下面是我当时的操作: ...

*git* *bash*命令_雨中世界的博客_*git* *bash* 命令

1-29

*git* *bash* 修改用户 *git* config --global user.name “用户名” *git* config --global user,email “邮箱” 基本操作 *git*status查看状态 *git* diff 查看不同 *git* add . 添加当前所有文件



*git* *bash*和*github*连接

[matthewchen123的博客](https://blog.csdn.net/matthewchen123)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 993

*Git* *Bash* *git* *bash*是在Windows下的命令行工具，基于msys GNU环境，用来进行*git*分布式版本控制工具。主要用于*git*版本控制，上传下载项目代码。 连接*github* 第一步：若是首次安装使用*git*，使用*git* *bash*先配置*github*的用户名称和邮箱 *git* config --global user.name "your name" *git* config --global user.email "your_email@youremail.com" 第二步：检查是否有ssh目录及对

*Git* *详细*安装*教程*（详解 *Git* 安装过程的每一个步骤）

热门推荐

[mukes的博客](https://blog.csdn.net/mukes)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 60万+

*Git* 是个免费的开源分布式版本控制系统，下载地址为*git*-scm.com 或者 *git*forwindows.org，本文介绍 *Git*-2.35.1.2-64-bit.exe 版本的安装方法，本文 13w 阅读量，3000收藏，值得一看。

*Git*常用操作(*git* *bash*)_赵妖镜zs的博客

1-29

五、*Git*的拉取操作# 表示将远程仓库的内容文件同步更新拉取到本地库。 *git* pull origin master 1 2六、使用*Git*在本地进行版本追溯切换# xxxxxxx表示不同版本的版本编号。这个指令代表将本地库切换至指定版本。 *git* reset --hard...

*Git* *Bash*使用*详细**教程*_心生未凉的博客

2-2

需要从网上下载一个,然后进行默认安装即可。安装完成后,在开始菜单里面找到 "*Git* --> *Git* *Bash*",如下: 会弹出一个类似的命令窗口的东西,就说明*Git*安装成功。如下: 安装完成后,还需要最后一步设置,在命令行输入如下: ...

*GitHub*使用*git**Bash*配置用户名和邮箱和本地操作一

[wei94940202的博客](https://blog.csdn.net/wei94940202)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 3444

打开已经安装好的*Bash* 输入 ：*git* config --global user.name "写入自己的*GitHub*用户名" 接着再输入 *git* config --global user.email "写入自己的*GitHub*注册邮箱" 如果两行输入后都没有报错，说明配置成功了 创建本地仓库： 在电脑系统盘里面创...

*git**bash*上传代码*教程*

[Legend105CC](https://blog.csdn.net/weixin_43094275)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 689

标题 1.进入xxx文件夹, 鼠标右键操作如下步骤： 右键*Git* *Bash*进入*git*命令行 2.在本地创建ssh key $ ssh-keygen -t rsa -C "your_email@youremail.com" 直接点回车，说明会在默认文件id_rsa上生成ssh key。注意看路径，得去找文件！！！！ 然后系统要求输入密码，直接按回车表示不设密码 重复密码时也是直接回车。我自己设置了个密码 之后提示你shh key已经生成成功。 然后我们进入刚才说的路径下查看ssh key文件。 打开id_

*git* *Bash* 命令行大全_haogemr的博客_*git* *bash*命令行大全

1-29

*git* checkout -t origin/dev 使用-t参数,它默认会在本地建立一个和远程分支名字一样的分支7、实现在*Git* *Bash* 中用 SublimeText 打开文件新建一个文件命名为你想要的命令,比如 subl(注意不能有后缀名),内容:#!/bin/sh "C:\...

用*Git* *Bash* 上传本地文件到*github*

[kaluosifa的博客](https://blog.csdn.net/kaluosifa)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 602

参考：https://blog.csdn.net/xiangwanpeng/article/details/54178653

*GITHUB*之*GIT* *BASH*使用*教程*

[Universe](https://blog.csdn.net/IT_dreamer1993)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 1567

maybe yes 发表于2014-10-25 12:12 原文链接 : http://blog.lmlphp.com/archives/7/The_use_tutorial_of_*git*_*bash*_and_how_to_start_with_*github* 来自 : LMLPHP后院 写在前面 这篇文章写完后，感觉不是很满意，漏掉了一些常用的命令忘记写，如“gi

Windows下*git*和*github*的使用图文*详细**教程*

[weixin_52270081的博客](https://blog.csdn.net/weixin_52270081)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 1959

这里介绍windows下的*git*和*GitHub*使用。 linux下*git*和*github*搭建使用*教程*参考： https://blog.csdn.net/weixin_52270081/article/details/119140724 1、注册gihub账号 *github*官网：https://*github*.com/自行创建即可。 登录，create repository新建仓库一个测试库test，创建完成。 2、*git*的安装 *git*官方网站：https://*git*-scm.com/ 选择Windows版本下

*git* *bash*的安装和配置*教程*

[xmt1139057136的专栏](https://blog.csdn.net/xmt1139057136)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 7791

分布式 : *Git*版本控制系统是一个分布式的系统, 是用来保存工程源代码历史状态的命令行工具;保存点 : *git*的保存点可以追踪源码中的文件, 并能得到某一个时间点上的整个工程项目额状态; 可以在该保存点将多人提交的源码合并, 也可以会退到某一个保存点上;*Git*离线操作性 :*Git*可以离线进行代码提交, 因此它称得上是完全的分布式处理, *Git*所有的操作不需要在线进行; 这意味着*Git*的速度要比S

*Git* 、*Git* *Bash*、*GitHub*、*Bash*、Shell之间的关系与区别

[subtlelzl的博客](https://blog.csdn.net/subtlelzl)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 1161

*git* 是一个版本控制工具 *github* 是一个用*git*做版本控制的项目托管平台。 *git* *bash* 以下内容来自这个网站的翻译，我觉得英语解释的更贴切点，有兴趣的可以进去看看，我也把该段的英语原文附上，英语好的可以读一读 *Git* *Bash*是用于Microsoft Windows环境的应用程序，它为*Git*命令行体验提供了一个仿真层。*Git* *Bash*是一个软件包，可以在Windows操作系统上安装*Bash*，一些常见的*bash*实用程序和*Git*。 （*Git* *Bash* is a package that insta

*github* *Git* *bash*方式与*GIt* Gui方式

[yue_luo_的博客](https://blog.csdn.net/yue_luo_)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 6732

进入*Github*首页，新建一个项目 复制https地址 首先右键你的项目，如果你之前安装*git*成功的话，右键会出现两个新选项，分别为*Git* Gui Here,*Git* *Bash* Here,这里我们选择*Git* *Bash* Here，为了把*github*上面的仓库克隆到本地 ，输入以下代码 *git* clone 复制的地址 本地项目文件夹下面就会多出个以仓库为名的文件夹（以下称之为X），我...

*Github* 中*Git* *Bash* Here 的用法

[Paulin的博客](https://blog.csdn.net/u014430370)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 2799

切换到用户： *git* config --global user.name "Paulin-peng" *git* config --global user.email "penghanlin93@163.com" 1. *git* config --global user.name "Paulin-peng" 或者 *git* config --global user.name "penghan

超级*详细*的*Git**Bash*使用*教程*01：下载、安装（适合小白）

[goog_man的博客](https://blog.csdn.net/goog_man)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 2万+

*Git**Bash*下载、安装、使用*教程*01 一、*Git**Bash*下载 1、推荐从官网下载：http://www.*git*-scm.com/download/ （国外网站，虽说不用翻q，但是40多MB的我下载好几次没有下载下来，最新版本2.22.0） 2、百度网盘（好男人用的版本：2.9.3）：链接：https://pan.baidu.com/s/1M6xMRfj5xUmfYDyXM6Jn4g 提取码：vp...

*Github*与*git* *bash*的使用

[west_three_boy的博客](https://blog.csdn.net/west_three_boy)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 910

将*Github*上的项目下载到本地 一、*Git* *bash*使用命令*git* clone https://*github*.com/liangyuyi/helloworld.*git* 二、直接根据网址下载压缩包 *Git* *Bash* 切换回上一级目录 cd .. ，查看该目录下的文档 ls -al 将本地项目上传到*github*仓库：http://blog.csdn.net/lengqi010

Windows安装*git* *bash* 同步jupyter notebook到*github*

[m0_37442062的博客](https://blog.csdn.net/m0_37442062)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 2243

首先注册*github*，并新建仓库； 其次，在windows系统上下载安装*git* *bash*，参考：windows下*Git* *BASH*安装和新建仓库后的使用说明： 接下来它会提示你输入用户名和密码，用户名就是你在*github*上注册的名字，密码就是你的登录密码 更多*git*使用见Windows下 *Git* *Bash*下*git*的使用 如何删除上传到*github*的文件呢： *Git* 如...

*git* *bash* 的基本使用方法(一)

[bafeiq的博客](https://blog.csdn.net/bafeiq)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 1万+

使用*git* *bash*窗口命令行进行简单的克隆,推送,拉取等操作。

查看用*git**bash*下载的*github*文件位置

[m0_53683419的博客](https://blog.csdn.net/m0_53683419)

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 681

查看用*git**bash*下载的*github*文件位置

### “相关推荐”对你有帮助么？

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/npsFeel1.png)

  非常没帮助

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/npsFeel2.png)

  没帮助

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/npsFeel3.png)

  一般

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/npsFeel4.png)

  有帮助

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/npsFeel5.png)

  非常有帮助

©️2022 CSDN 皮肤主题：书香水墨 设计师：CSDN官方博客 [返回首页](https://blog.csdn.net/)

- [关于我们](https://www.csdn.net/company/index.html#about)
- [招贤纳士](https://www.csdn.net/company/index.html#recruit)
- [商务合作](https://marketing.csdn.net/questions/Q2202181741262323995)
- [寻求报道](https://marketing.csdn.net/questions/Q2202181748074189855)
- ![img](https://g.csdnimg.cn/common/csdn-footer/images/tel.png)400-660-0108
- ![img](https://g.csdnimg.cn/common/csdn-footer/images/email.png)[kefu@csdn.net](mailto:webmaster@csdn.net)
- ![img](https://g.csdnimg.cn/common/csdn-footer/images/cs.png)[在线客服](https://csdn.s2.udesk.cn/im_client/?web_plugin_id=29181)
- 工作时间 8:30-22:00

- [公安备案号11010502030143](http://www.beian.gov.cn/portal/registerSystemInfo?recordcode=11010502030143)
- [京ICP备19004658号](http://beian.miit.gov.cn/publish/query/indexFirst.action)
- [京网文〔2020〕1039-165号](https://csdnimg.cn/release/live_fe/culture_license.png)
- [经营性网站备案信息](https://csdnimg.cn/cdn/content-toolbar/csdn-ICP.png)
- [北京互联网违法和不良信息举报中心](http://www.bjjubao.org/)
- [家长监护](https://download.csdn.net/tutelage/home)
- [网络110报警服务](http://www.cyberpolice.cn/)
- [中国互联网举报中心](http://www.12377.cn/)
- [Chrome商店下载](https://chrome.google.com/webstore/detail/csdn开发者助手/kfkdboecolemdjodhmhmcibjocfopejo?hl=zh-CN)
- [账号管理规范](https://blog.csdn.net/blogdevteam/article/details/126135357)
- [版权与免责声明](https://www.csdn.net/company/index.html#statement)
- [版权申诉](https://blog.csdn.net/blogdevteam/article/details/90369522)
- [出版物许可证](https://img-home.csdnimg.cn/images/20220705052819.png)
- [营业执照](https://img-home.csdnimg.cn/images/20210414021142.jpg)
- ©1999-2023北京创新乐知网络技术有限公司

[![img](https://profile.csdnimg.cn/A/8/3/3_weixin_41087220)](https://blog.csdn.net/weixin_41087220)

[云想衣裳，花想容](https://blog.csdn.net/weixin_41087220)

码龄5年[![img](https://csdnimg.cn/identity/nocErtification.png) 暂无认证](https://i.csdn.net/#/uc/profile?utm_source=14998968)







- 1万+

  访问

- [![img](https://csdnimg.cn/identity/blog2.png)](https://blog.csdn.net/blogdevteam/article/details/103478461)

  等级

- 336

  积分

- 7

  粉丝

- 23

  获赞

- 9

  评论

- 51

  收藏

![勤写标兵](https://csdnimg.cn/medal/qixiebiaobing4@240.png)

[私信](https://im.csdn.net/chat/weixin_41087220)

关注

![img](https://csdnimg.cn/cdn/content-toolbar/csdn-sou.png?v=1587021042)

### 热门文章

- [JavaScript---FileReader、Blob、File 文件处理对象 ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 3822](https://blog.csdn.net/weixin_41087220/article/details/89250643)
- [GitHub教程 Git Bash详细教程 ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 2814](https://blog.csdn.net/weixin_41087220/article/details/118099800)
- [区块链---双花问题 ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 1996](https://blog.csdn.net/weixin_41087220/article/details/106938664)
- [React---回调函数的this绑定 ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 624](https://blog.csdn.net/weixin_41087220/article/details/88550367)
- [Spring Boot 中使用 @Transactional 注解配置事务管理 ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/readCountWhite.png) 598](https://blog.csdn.net/weixin_41087220/article/details/118517064)

### 分类专栏

- ![img](https://img-blog.csdnimg.cn/20190918135101160.png?x-oss-process=image/resize,m_fixed,h_64,w_64)模块 -- 区块链

  1篇

- ![img](https://img-blog.csdnimg.cn/20201014180756925.png?x-oss-process=image/resize,m_fixed,h_64,w_64)未来之路

  6篇

- ![img](https://img-blog.csdnimg.cn/20201014180756754.png?x-oss-process=image/resize,m_fixed,h_64,w_64)模块 -- 思想

  2篇

- ![img](https://img-blog.csdnimg.cn/20201014180756754.png?x-oss-process=image/resize,m_fixed,h_64,w_64)模块 -- 计算机

  1篇

- ![img](https://img-blog.csdnimg.cn/20201014180756757.png?x-oss-process=image/resize,m_fixed,h_64,w_64)后端 -- Maven

  2篇

- ![img](https://img-blog.csdnimg.cn/20190918140053667.png?x-oss-process=image/resize,m_fixed,h_64,w_64)后端 -- Spring

  1篇

- ![img](https://img-blog.csdnimg.cn/20201014180756754.png?x-oss-process=image/resize,m_fixed,h_64,w_64)后端 -- FTP

  1篇

- ![img](https://img-blog.csdnimg.cn/20190927151043371.png?x-oss-process=image/resize,m_fixed,h_64,w_64)前端 -- JavaScript

  9篇

- ![img](https://img-blog.csdnimg.cn/20190918140145169.png?x-oss-process=image/resize,m_fixed,h_64,w_64)前端 -- Js框架

  1篇

- ![img](https://img-blog.csdnimg.cn/2019092715111047.png?x-oss-process=image/resize,m_fixed,h_64,w_64)后端 -- Java基础

  5篇

![img](https://csdnimg.cn/release/blogv2/dist/pc/img/arrowDownWhite.png)

### 最新评论

- 什么是 JWT -- JSON WEB TOKEN

  [大脸怪1: ](https://blog.csdn.net/hbbbbf)感谢分享，涨姿势了！![表情包](https://g.csdnimg.cn/static/face/emoji/055.png)

- React---回调函数的this绑定

  [itwangyang520: ](https://blog.csdn.net/itwangyang520)我推荐最好用第二种，这样子，感觉代码解决看起来舒服一些

- JavaScript---异步函数async内部运行机制

  [itwangyang520: ](https://blog.csdn.net/itwangyang520)我最近也在研究这个，也写了博客，还望您指点迷津！

- JavaScript---深拷贝之内存问题

  [itwangyang520: ](https://blog.csdn.net/itwangyang520)滥用闭包也是可以导致内存泄漏

- JavaScript---深拷贝之内存问题

  [itwangyang520: ](https://blog.csdn.net/itwangyang520)栈 堆 队列，你没有讲清楚。。。可以在js再详细一点

### 您愿意向朋友推荐“博客详情页”吗？

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/npsFeel1.png)

  强烈不推荐

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/npsFeel2.png)

  不推荐

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/npsFeel3.png)

  一般般

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/npsFeel4.png)

  推荐

- ![img](https://csdnimg.cn/release/blogv2/dist/pc/img/npsFeel5.png)

  强烈推荐

### 最新文章

- [RPC - 远程过程调用](https://blog.csdn.net/weixin_41087220/article/details/118672035)
- [Spring Boot 中使用 @Transactional 注解配置事务管理](https://blog.csdn.net/weixin_41087220/article/details/118517064)
- [Thread --- InheritableThreadLocal 类](https://blog.csdn.net/weixin_41087220/article/details/118336029)

[2021年8篇](https://blog.csdn.net/weixin_41087220?type=blog&year=2021&month=07)

[2020年9篇](https://blog.csdn.net/weixin_41087220?type=blog&year=2020&month=08)

[2019年9篇](https://blog.csdn.net/weixin_41087220?type=blog&year=2019&month=05)

<iframe id="aswift_1" name="aswift_1" sandbox="allow-forms allow-popups allow-popups-to-escape-sandbox allow-same-origin allow-scripts allow-top-navigation-by-user-activation" width="300" height="600" frameborder="0" marginwidth="0" marginheight="0" vspace="0" hspace="0" allowtransparency="true" scrolling="no" src="https://googleads.g.doubleclick.net/pagead/ads?us_privacy=1YNN&amp;client=ca-pub-1076724771190722&amp;output=html&amp;h=600&amp;slotname=4787882818&amp;adk=944469691&amp;adf=748340847&amp;pi=t.ma~as.4787882818&amp;w=300&amp;fwrn=4&amp;fwrnh=100&amp;lmt=1676379689&amp;rafmt=1&amp;format=300x600&amp;url=https%3A%2F%2Fblog.csdn.net%2Fweixin_41087220%2Farticle%2Fdetails%2F118099800&amp;fwr=0&amp;fwrattr=true&amp;rpe=1&amp;resp_fmts=4&amp;wgl=1&amp;uach=WyJXaW5kb3dzIiwiMTQuMC4wIiwieDg2IiwiIiwiMTA5LjAuNTQxNC4xMjAiLFtdLGZhbHNlLG51bGwsIjY0IixbWyJOb3RfQSBCcmFuZCIsIjk5LjAuMC4wIl0sWyJHb29nbGUgQ2hyb21lIiwiMTA5LjAuNTQxNC4xMjAiXSxbIkNocm9taXVtIiwiMTA5LjAuNTQxNC4xMjAiXV0sZmFsc2Vd&amp;dt=1676379688184&amp;bpp=2&amp;bdt=954&amp;idt=835&amp;shv=r20230209&amp;mjsv=m202302080101&amp;ptt=9&amp;saldr=aa&amp;abxe=1&amp;cookie=ID%3Df7ec37aa30fbe31c-2228a78ab7d60041%3AT%3D1664089132%3ART%3D1664089132%3AS%3DALNI_Mbf6_LoNQfee7oceTWXqnNTdNDhRQ&amp;gpic=UID%3D000009df0b42ae28%3AT%3D1664089132%3ART%3D1676299130%3AS%3DALNI_MbMDfrf-EmUW0KC66-qBwOsgkQxHQ&amp;prev_fmts=0x0&amp;nras=1&amp;correlator=3048464391185&amp;frm=20&amp;pv=1&amp;ga_vid=1648976904.1676379689&amp;ga_sid=1676379689&amp;ga_hid=878466558&amp;ga_fc=0&amp;u_tz=480&amp;u_his=2&amp;u_h=934&amp;u_w=1494&amp;u_ah=886&amp;u_aw=1494&amp;u_cd=24&amp;u_sd=1.5&amp;dmc=8&amp;adx=84&amp;ady=1806&amp;biw=1477&amp;bih=815&amp;scr_x=0&amp;scr_y=1000&amp;eid=44759875%2C44759926%2C44759842%2C31071755%2C31072259%2C31072382&amp;oid=2&amp;pvsid=49060145746197&amp;tmod=2082536961&amp;uas=0&amp;nvt=1&amp;eae=0&amp;fc=1920&amp;brdim=0%2C0%2C0%2C0%2C1494%2C0%2C1494%2C886%2C1494%2C815&amp;vis=1&amp;rsz=%7C%7CpeE%7C&amp;abl=CS&amp;pfx=0&amp;fu=128&amp;bc=31&amp;ifi=2&amp;uci=a!2&amp;fsb=1&amp;xpc=CYolaKc6Qp&amp;p=https%3A//blog.csdn.net&amp;dtd=839" data-google-container-id="a!2" data-google-query-id="CJWRpf-Ilf0CFXVEwgUdnyEDIg" data-load-complete="true" style="box-sizing: border-box; outline: 0px; margin: 0px; padding: 0px; font-weight: normal; left: 0px; top: 0px; border: 0px; width: 300px; height: 600px;"></iframe>

![img](https://kunyu.csdn.net/1.png?p=57&adId=1015190&a=1015190&c=0&k=GitHub%E6%95%99%E7%A8%8B%20Git%20Bash%E8%AF%A6%E7%BB%86%E6%95%99%E7%A8%8B&spm=1001.2101.3001.5001&articleId=118099800&d=1&t=3&u=bf2111d5c24f4aeeacb5e90cef4ebf94)

### 目录

1. [文章目录](https://blog.csdn.net/weixin_41087220/article/details/118099800#t0)
2. [1 下载安装](https://blog.csdn.net/weixin_41087220/article/details/118099800#t1)
3. [2 设置用户](https://blog.csdn.net/weixin_41087220/article/details/118099800#t2)
4. [3 本地文件夹的操作](https://blog.csdn.net/weixin_41087220/article/details/118099800#t3)
5. 1. [4 仓库设置](https://blog.csdn.net/weixin_41087220/article/details/118099800#t4)
   2. [本文为转载文章，转载仅做保存使用，尊重原创](https://blog.csdn.net/weixin_41087220/article/details/118099800#t5)



![img](https://csdnimg.cn/release/blogv2/dist/pc/img/iconShowDirectory.png)![img](https://csdnimg.cn/release/blogv2/dist/pc/img/iconSideBeta.png)![img](https://csdnimg.cn/release/blogv2/dist/pc/img/iconHideSide.png)![img](https://csdnimg.cn/release/blogv2/dist/pc/img/iconSideBeta.png)![img](https://g.csdnimg.cn/side-toolbar/3.4/images/guide.png)![img](https://g.csdnimg.cn/side-toolbar/3.4/images/kefu.png)举报![img](https://g.csdnimg.cn/side-toolbar/3.4/images/fanhuidingbucopy.png)

[![img](https://csdnimg.cn/release/blogv2/dist/pc/img/newarticleComment1White.png)评论](javascript:;)[![img](https://csdnimg.cn/release/blogv2/dist/pc/img/newcNoteWhite.png)笔记](javascript:;)