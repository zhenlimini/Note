# Git 原理及操作培训文档



## 一、Git 基础概念

### 1. 核心区域：工作区、暂存区、本地仓库、远程仓库

Git 通过四个核心区域实现版本控制，各区域职责与关联如下：

#### （1）工作区（Working Directory）

- **定义**：项目目录中用户可见且直接操作的部分，包含所有项目文件和子目录（如 F:\GitRepo\PTN_Common 目录）。

- **文件状态**：

- - 未跟踪（Untracked）：新创建文件，未被 Git 管理。

- - 已修改（Modified）：已跟踪文件被修改但未暂存。

- - 已暂存（Staged）：通过 git add 加入暂存区，等待提交。

- - 未修改（Unmodified）：与本地仓库最新提交一致，无变更。

#### （2）暂存区（Staging Area）

- **定义**：介于工作区与本地仓库之间的缓冲区域，用于选择性控制下次提交的内容。

- **核心作用**：

- - 避免意外提交未完成代码或调试日志。

- - 支持部分提交（仅提交暂存的修改）。

- **与工作区对比**：

| 对比维度 | 工作区                   | 暂存区                     |
| -------- | ------------------------ | -------------------------- |
| 是否可见 | 直接可见（文件系统目录） | 不可见（Git 内部虚拟区域） |
| 修改来源 | 开发者手动编辑           | 通过 git add 从工作区导入  |
| 提交影响 | 未暂存修改不会被提交     | 仅暂存内容会被提交         |

#### （3）本地仓库（Local Repository）

- **定义**：存储在本地 .git 隐藏目录中的完整版本库，包含所有提交历史、分支、标签等信息。

- **核心特点**：

- - 位置：项目根目录下的 .git 文件夹（含 hooks、objects、refs 等子目录）。

- - 提交机制：通过 git commit 将暂存区变更永久保存，每个提交生成唯一 SHA-1 哈希值（如 a1b2c3d）。

#### （4）远程仓库（Remote Repository）

- **定义**：托管在服务器（如 GitLab、GitBlit）上的共享仓库，是团队协作的代码同步中心。

- **核心作用**：

- - 团队协作：实现多开发者代码同步。

- - 数据备份：防止本地仓库丢失。

- **示例**：沛顿科技远程仓库 PTN_Automation，地址为 ssh://henryzhou@szdevweb:29418/PTN_Automation.git。

#### （5）区域关联逻辑

```mermaid
graph LR
A[工作区] -->|git add| B[暂存区]
B -->|git commit| C[本地仓库]
C -->|git push| D[远程仓库]
D -->|git pull/clone| C
```



### 2. 对象模型

Git 是**内容寻址文件系统**，核心由四种对象构成，通过 SHA-1 哈希唯一标识，存储于 .git/objects 目录。

#### （1）Blob（二进制大对象）

- **作用**：存储文件的完整原始数据（文本 / 二进制），不包含文件名或目录信息。

- **生成时机**：执行 git add 时创建；文件内容不变则复用已有 Blob（相同内容 → 相同哈希）。

- **存储优化**：

- - 采用 zlib 压缩。

- - 定期通过 git gc 打包为 .pack 文件，使用增量压缩（delta compression）减少占用。

| 场景         | Blob 存储行为         |
| ------------ | --------------------- |
| 文件内容变化 | 新建完整 Blob         |
| 文件内容不变 | 复用旧 Blob           |
| 存储优化手段 | 压缩 + Pack 文件 + GC |

#### （2）Tree（目录对象）

- **作用**：表示目录结构，记录文件与子目录的关联（类似文件系统目录）。

- **核心特点**：

- - 层次结构：可包含子 Tree（子目录）和 Blob（文件）。

- - 不可变性：修改后生成新 Tree 对象。

- - 高效对比：通过哈希值快速判断目录结构是否变更。

| 对象类型 | 作用     | 示例                         |
| -------- | -------- | ---------------------------- |
| Tree     | 代表目录 | 包含 src/ 子目录和 README.md |
| Blob     | 代表文件 | 存储 README.md 内容          |
| Commit   | 提交快照 | 指向顶级 Tree                |

#### （3）Commit（提交对象）

- **作用**：记录项目某一时间点的完整状态，是版本历史的原子单位。

- **包含信息**：

- - 唯一 SHA-1 哈希值。

- - 指向顶级 Tree 对象（目录结构）。

- - 父 Commit 指针（形成历史链）。

- - 作者 / 提交者信息（姓名、邮箱、时间戳）。

- - 提交消息（描述变更）。

- **核心特性**：不可变性（内容变更会生成新哈希）、链式结构（支持分支与合并）。

#### （4）Tag（标签对象）

- **作用**：为特定 Commit 打上永久性标记，常用于版本发布（如 v1.0.0）。

- **与分支区别**：Tag 创建后不移动，分支随提交动态更新。

- **常用指令**：

- - 创建附注标签：git tag -a v1.0 -m "Release version 1.0"

- - 推送标签到远程：git push origin v1.0

### 3. 快照机制

#### （1）定义

Git 每次提交时，将项目状态保存为**完整文件快照**（类似 “拍照片”），而非传统版本控制（如 SVN）的差异（delta）存储。

#### （2）工作原理

- 提交时，Git 检查每个文件的 SHA-1 哈希：

- - 未修改文件：仅保留指向旧 Blob 的引用，不重复存储。

- - 修改文件：存储新版本 Blob。

- 效率保障：通过引用复用、压缩、GC 实现高效存储。

#### （3）优势

- **性能优势**：快速分支切换（仅切换指针）、快速历史查看（直接访问快照）。

- **数据完整性**：每个文件 / 提交的哈希值唯一，微小改动会改变哈希，防止历史被篡改。

## 二、Git 基础操作

### 1. 本地仓库管理

#### （1）git init：初始化本地仓库

- **作用**：在当前目录创建 .git 文件夹，初始化新 Git 仓库。

- **与** **git clone** **区别**：

| 对比维度 | git init         | git clone            |
| -------- | ---------------- | -------------------- |
| 数据来源 | 创建全新仓库     | 复制现有远程仓库     |
| 使用场景 | 本地新项目初始化 | 获取远程已有项目代码 |
| 网络依赖 | 无需网络         | 需要网络             |

#### （2）git clone：克隆远程仓库

- **作用**：下载远程仓库完整代码到本地，生成与远程关联的本地仓库。

- **示例**：

```
# SSH 协议克隆
git clone ssh://henryzhou@szdevweb:29418/PTN_Automation.git
# HTTP 协议克隆
git clone http://henryzhou@szdevweb:8888/r/PTN_Automation.git
```

#### （3）git add：暂存工作区变更

- **作用**：将工作区的新增、修改、删除操作暂存到暂存区，为 git commit 做准备。

- **常用场景**：

- - 暂存单个文件：git add <file>

- - 暂存所有变更：git add .

#### （4）git commit：提交到本地仓库

- **作用**：将暂存区变更永久保存到本地仓库，生成新 Commit 对象。

- **示例**：

```
git commit -m "XXJS024-2025005689 外包批号常规合并规则Rev.3卡控更新"
```

- **提交记录**：包含哈希值、作者、时间戳（可通过 git log 查看）。

#### （5）git status：查看文件状态

- **作用**：显示工作区与暂存区的文件状态（未跟踪 / 已修改 / 已暂存），提交前必查。

- **示例输出**：

```
Changes to be committed:
  (use "git restore --staged <file>..." to unstage)
  modified:   StephenHuang.txt

Changes not staged for commit:
  (use "git add <file>..." to update what will be committed)
  modified:   test.txt
```

#### （6）git log：查看提交历史

- **作用**：展示提交历史，包含哈希值、作者、时间、提交消息。

- **常用参数**：

- - 简化输出：git log --oneline

- - 显示分支关系：git log --graph

- - 查看指定作者：git log --author="Quinta"

### 2. 撤销与回退

#### （1）git restore：恢复文件状态

- **作用**：撤销工作区或暂存区的变更，仅适用于已跟踪文件。

- **常用指令**：

1. 1. git restore --staged <file>：将暂存区文件恢复到工作区（取消暂存）。

1. 1. git restore <file>：丢弃工作区修改（恢复到最近提交状态）。

1. 1. git restore <file>：恢复已删除的已跟踪文件。

#### （2）git reset：回退版本

- **作用**：根据 Commit ID 回退到历史版本，可修改分支指针位置。

- **常用指令**：

1. 1. git reset <commit-id>：回退到指定提交，提交后文件重置到工作区。

1. 1. git reset --soft HEAD~1：撤销最后一次提交，但保留工作区与暂存区变更。

- **注意**：慎用 git reset --hard，会永久丢弃后续提交。

#### （3）git revert：安全撤销提交

- **作用**：通过创建**新 Commit** 反向抵消历史提交的变更，不修改原有历史（团队协作首选）。

- **常用指令**：

1. 1. 撤销单次提交：git revert <commit-hash>

1. 1. 撤销多次提交：git revert <commit1> <commit3>（跳过中间提交）。

- **示例流程**：

原历史：版本1 → 版本2 → 版本3

执行 git revert 版本3 后：版本1 → 版本2 → 版本3 → 版本4（抵消版本3）

## 三、分支管理

### 1. 分支基础

#### （1）HEAD 引用

- **定义**：Git 中 “当前位置指示器”，指向当前所在分支或提交。

- **两种状态**：

1. 1. **指向分支**（默认）：HEAD → 分支 → 最新提交（如 HEAD -> master -> a1b2c3d）。

1. 1. **分离 HEAD 状态**：直接指向提交哈希（如检出历史版本时），新提交为 “游离提交”，需手动绑定分支。

| 操作                  | HEAD 变化                  | 注意事项             |
| --------------------- | -------------------------- | -------------------- |
| git commit            | 与分支同步向前移动         | 无风险               |
| git checkout <commit> | 进入分离 HEAD 状态         | 新提交需手动绑定分支 |
| git reset HEAD~1      | 回退分支与 HEAD 到历史提交 | 丢弃后续提交（慎用） |

#### （2）Branch 分支

- **定义**：“平行时间线”，本质是指向 Commit 的可移动指针，默认分支为 main 或 master。

- **存储位置**：.git/refs/heads/ 目录，每个分支对应一个存储 Commit 哈希的文件。

- **常用指令**：

1. 1. 创建分支：git branch feature-x

1. 1. 创建并切换：git checkout -b feature-x（或 git switch -c feature-x）

1. 1. 查看本地分支：git branch

1. 1. 查看所有分支（含远程）：git branch -a

1. 1. 删除分支：git branch -d feature-x（需切换到其他分支）

### 2. 分支合并（Merge）

- **定义**：创建新的 “合并 Commit”，整合两个分支的历史，保留原有 Commit 哈希。

- **核心逻辑**：三方合并（当前分支最新提交 + 目标分支最新提交 + 共同祖先提交）。

- **操作流程**：

```
git checkout main          # 切换到主分支
git merge feature-branch   # 合并特性分支到主分支
```

- **图示**：

合并前：main（C0→C1→C2）、feature（C0→C1→C3）

合并后：main（C0→C1→C2→C4（合并C2与C3））

### 3. 变基（Rebase）

- **定义**：将当前分支的 Commit “重放” 到目标分支上，重写历史（生成新 Commit 哈希）。

- **核心逻辑**：提取当前分支的变更补丁，在目标分支最新提交上重新应用。

- **操作流程**：

```
git checkout feature-branch  # 切换到特性分支
git rebase main              # 基于 main 重放 feature-branch 提交
git checkout main            # 切换回主分支
git merge feature-branch     # 快进合并（无新 Commit）
```

- **与 Merge 区别**：

- - Merge：保留历史分支结构，生成合并 Commit。

- - Rebase：历史线性化，无合并 Commit，但修改历史（不建议用于已推送的分支）。

## 四、远程协作

### 1. 远程仓库核心概念

- **远程仓库（Remote Repository）**：云端托管的 Git 仓库（如 GitLab），团队共享代码的中心。

- **本地仓库（Local Repository）**：开发者本地的仓库，与远程仓库通过 “远程连接” 同步。

- **远程连接（Remote）**：本地与远程的关联配置，默认名称为 origin。

### 2. 远程操作指令

#### （1）git remote：管理远程连接

- **作用**：查看、添加、删除远程仓库配置，不存储代码，仅记录地址与名称。

- **常用指令**：

1. 1. 查看远程仓库及地址：git remote -v

1. 1. 查看远程仓库详情：git remote show origin

1. 1. 添加远程仓库：git remote add <name> <url>

1. 1. 删除远程仓库：git remote remove <name>

- **示例**：

```
# 添加 SSH 远程仓库
git remote add origin ssh://stephenhuang@172.23.3.59:29418/GitExam.git
# 查看远程地址
git remote -v
# 输出：
origin  ssh://stephenhuang@172.23.3.59:29418/GitExam.git (fetch)
origin  ssh://stephenhuang@172.23.3.59:29418/GitExam.git (push)
```

#### （2）git fetch：获取远程更新

- **作用**：从远程仓库同步最新数据到本地，但**不合并到工作区**（仅更新本地远程跟踪分支）。

- **常用指令**：

1. 1. 获取所有远程更新：git fetch origin

1. 1. 获取指定分支更新：git fetch origin feature-branch

1. 1. 获取远程标签：git fetch --tags

#### （3）git pull：拉取并合并

- **作用**：等价于 git fetch + git merge，从远程拉取更新并自动合并到当前分支。

- **常用指令**：

1. 1. 拉取默认远程当前分支：git pull

1. 1. 拉取指定远程分支：git pull origin feature-branch

#### （4）git push：推送本地变更

- **作用**：将本地仓库的 Commit、分支等推送到远程仓库，实现代码共享。

- **常用指令**：

1. 1. 推送当前分支到远程同名分支：git push

1. 1. 推送指定分支到远程：git push origin <local-branch>:<remote-branch>

- **示例**：

```
# 提交本地变更
git commit -m "修改文件 StephenHuang.txt"
# 推送到远程 master 分支
git push origin master
```

## 五、GitLab 团队协作与工作流

### 1. 团队协作模型

#### （1）集中式工作流（当前使用）

- **特点**：

1. 1. 所有开发者直接在 master 分支提交代码，无长期功能分支。

1. 1. 通过 git push/pull 同步代码，需先拉取最新代码再推送。

- **缺点**：

- - 直接污染主分支：问题代码可能直接提交到生产分支。

- - 缺乏流程控制：无强制代码审查（CR），难以保证代码规范。

#### （2）GitLab 代码提交工作流（未来规划）

- **核心流程**（7 步）：

1. 1. **创建分支**：从 master 分支创建特性分支（如 feature-xxx）。

1. 1. **开发功能**：在特性分支上迭代开发，多次 git commit。

1. 1. **推送分支**：将特性分支推送到远程 GitLab 仓库。

1. 1. **创建合并请求（MR）**：发起 MR，请求合并到 master 分支。

1. 1. **代码审查**：团队成员评审代码，提出修改建议。

1. 1. **合并 MR**：通过审查后，维护者将特性分支合并到 master。

1. 1. **删除分支**：合并完成后，删除远程与本地的特性分支。

### 2. GitLab 介绍

#### （1）核心功能

- **源代码管理**：托管 Git 仓库，支持多用户协作与版本控制。

- **CI/CD 自动化**：内置持续集成 / 持续交付，自动化构建、测试、部署。

- **项目管理**：含问题跟踪、看板、里程碑，管理项目进度。

- **代码审查**：通过 MR 实现强制代码审查，保障代码质量。

#### （2）GitLab vs GitBlit

| 功能 / 特性 | GitLab                                | GitBlit                         |
| ----------- | ------------------------------------- | ------------------------------- |
| 部署方式    | 支持 Docker、K8s、Linux 包、SaaS 版本 | 单 JAR 文件部署，依赖 Java 环境 |
| 用户界面    | 现代化 Web UI，功能复杂但直观         | 简洁 Web UI，功能较少           |
| 配置复杂度  | 高（需配置数据库、Redis、CI/CD 等）   | 低（开箱即用，配置简单）        |

### 3. GitLab 使用手册

- **管理员手册**：包含仓库创建、用户权限管理、CI/CD 配置等。

- **用户手册**：包含分支创建、MR 发起、代码审查等操作指南。

## 六、高级技巧与工具

### 1. 效率工具

#### （1）git stash：储藏未提交变更

- **作用**：临时保存工作区 / 暂存区的未提交变更，恢复工作区到 “干净” 状态（用于切换分支或紧急修复）。

- **特点**：仅本地有效，不随 git push 上传到远程。

- **常用指令**：

1. 1. 储藏变更：git stash save "备注信息"

1. 1. 查看储藏列表：git stash list

1. 1. 应用最新储藏：git stash pop

1. 1. 删除储藏：git stash drop <stash-id>

#### （2）git tag：版本标记

- **作用**：为重要 Commit 打标签（如版本发布 v1.0.0），便于回溯。

- **常用指令**：

1. 1. 创建附注标签：git tag -a v1.0.0 -m "Release version 1.0.0"

1. 1. 切换到标签版本：git checkout v1.0.0

1. 1. 推送标签到远程：git push origin v1.0.0

1. 1. 删除本地标签：git tag -d v1.0.0

1. 1. 删除远程标签：git push origin :refs/tags/v1.0.0

#### （3）git show：查看对象详情

- **作用**：展示 Commit、Tag、Branch 等 Git 对象的详细信息（含元数据、代码差异）。

- **常用指令**：

1. 1. 查看最新提交：git show

1. 1. 查看指定 Commit：git show <commit-hash>

1. 1. 查看指定标签：git show v1.0.0

#### （4）.gitignore 配置

- **定义**：纯文本文件，指定 Git 需忽略的文件 / 目录（不纳入版本控制）。

- **适用场景**：临时文件、编译产物（如 bin/、obj/）、敏感信息（如 *.env）。

- **基本语法**：

| 规则       | 示例           | 说明                         |
| ---------- | -------------- | ---------------------------- |
| /filename  | /debug.log     | 忽略根目录下的 debug.log     |
| filename   | temp.txt       | 忽略所有目录的 temp.txt      |
| directory/ | build/         | 忽略所有 build 目录          |
| *.ext      | *.log          | 忽略所有 .log 文件           |
| !filename  | !important.log | 不忽略 important.log（例外） |
| #          | # 注释         | 注释行，Git 忽略             |

### 2. 图形化工具：SourceTree

- **定义**：Atlassian 开发的免费 Git 图形化客户端（支持 Windows/macOS），适合不熟悉命令行的用户。

- **核心功能**：

- - 可视化提交历史、分支关系。

- - 一键执行提交、拉取、推送、合并等操作。

- - 支持储藏、标签、MR 管理。

- **资源**：提供安装包与使用手册，降低 Git 学习成本。