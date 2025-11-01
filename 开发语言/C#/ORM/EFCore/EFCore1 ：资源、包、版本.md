# EFCore1 ：资源、包、版本

[TOC]

### 资源

- [Entity Framework Core 概述 - EF Core | Microsoft Learn](https://learn.microsoft.com/zh-cn/ef/core/)
- [ASP.NET Core 中的 Razor Pages 和 Entity Framework Core - 第 1 个教程（共 8 个） | Microsoft Learn](https://learn.microsoft.com/zh-cn/aspnet/core/data/ef-rp/intro?view=aspnetcore-9.0&tabs=visual-studio)



### 包

#### **一、核心基础包**

1. **`Microsoft.EntityFrameworkCore`**
   - 作用：EF Core的核心库，包含数据上下文（DbContext）、实体映射、变更追踪等基础功能。所有EF Core项目必须安装该包，它提供了ORM的核心框架。
   - **功能**：支持实体类定义、LINQ查询、事务管理等基础操作。
2. **`Microsoft.EntityFrameworkCore.Relational`**
   - 作用：为关系型数据库（如SQL Server、MySQL等）提供共享组件，例如表结构映射、迁移支持等。安装核心包后会自动包含此包，无需单独安装。

------

#### **二、数据库提供程序包（根据数据库选择其一）**

1. **`Microsoft.EntityFrameworkCore.SqlServer`**
   - 作用：针对SQL Server数据库的驱动程序，支持SQL Server特有功能（如分页语OFFSET FETCH）。
   - **适用场景**：使用SQL Server数据库时必装。
2. **`Pomelo.EntityFrameworkCore.MySql`**
   - 作用：MySQL数据库的官方推荐驱动程序，支持MySQL 8.0及以上版本。
   - **替代方案**：其他数据库如PostgreSQL需安装对应的包（如`Npgsql.EntityFrameworkCore.PostgreSQL`）。

------

#### **三、开发工具包**

1. **`Microsoft.EntityFrameworkCore.Design`**

   - 作用：设计时工具支持，用于生成迁移文件（Migrations）、反向工程（Scaffold-DbContext）等操作。需在开发环境安装。

2. **`Microsoft.EntityFrameworkCore.Tools`**

   - 作用：提供命令行工具（如Add-Migration、Update-Database），需配合NuGet包管理器控制台或.NET CLI使用。

   - 常用命令：

     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```



### 各版本差异性

以下是 **.NET/EF Core 版本差异导致的常见问题总结**：

---

#### 一、依赖版本兼容性问题
1. **EF Core 与 .NET 版本不匹配**  
   - 旧版 EF Core（如 3.1）可能不支持新版 .NET（如 .NET 6+），导致运行时错误 。  
   - **解决方案**：通过 `dotnet tool install` 指定与 .NET 版本对应的 EF Core 工具包，或升级 EF Core 到兼容版本。

2. **SQL Server 证书验证策略变更**  
   - EF Core 6+ 默认启用 `Encrypt=True` 并强制验证证书链，导致自签名证书连接失败 。  
   - **解决方案**：在连接字符串中添加 `TrustServerCertificate=True` 或安装可信证书。

---

#### 二、中断性变更（Breaking Changes）
3. **EF Core 3.x+ 的查询处理差异**  
   - EF Core 3.x 重构了查询管道，导致旧版 LINQ 查询（如客户端求值）抛出异常 。  
   - **示例**：`OrderBy(client_side_method())` 在 3.x 中会报错，需改用数据库可翻译的表达式。

4. **.NET 8 中 DML 触发器限制**  
   - EF Core 8 在插入/更新操作中，若目标表有触发器且使用 `OUTPUT` 子句，会抛出 SQL 语法错误 。  
   - **解决方案**：避免在触发器表中使用 `OUTPUT` 子句，或改用原始 SQL。

---

#### 三、NULL 值处理差异
5. **严格 NULL 检查（EF Core 6+）**  
   - 新版本默认启用可空引用类型检查，若实体属性未标注为可空（如 `string?`），但数据库字段允许 NULL，会抛出 `Data is Null` 异常 。  
   - **解决方案**：  
     - 在实体类中标注可空类型（`DateTime?`）。  
     - 启用 C# 可空注解（`<Nullable>enable</Nullable>`）。

---

#### 四、迁移与模型配置问题
6. **EF Core 迁移策略变化**  
   - EF Core 5+ 默认启用 `MigrationsHistoryTable` 的新格式，导致旧迁移脚本无法复用 。  
   - **解决方案**：清理旧迁移并重新生成，或手动调整迁移历史记录表。

7. **从 EF6 迁移到 EF Core 的模型差异**  
   - EF Core 不支持 EDMX 文件，需手动将模型转换为代码优先模式 。  
   - **关键点**：检查实体关系、索引和约束的显式配置。

---

#### 五、并发控制与性能
8. **乐观锁（Version 字段）实现差异**  
   - EF Core 使用 `[Timestamp]` 或 `IsRowVersion()` 标记并发令牌，但旧版（如 EF6）可能依赖 `RowVersion` 属性 。  
   - **解决方案**：统一使用 EF Core 的 `[Timestamp]` 注解。

9. **原始 SQL 执行格式化错误**  
   - EF Core 7+ 对参数化查询更严格，未正确转义的 SQL 参数可能导致 `FormatException` 。  
   - **示例**：`FromSqlRaw("SELECT * FROM Table WHERE Id = {0}", id)` 需改用参数化占位符。

---

#### 六、其他兼容性问题
10. **IServiceProvider 警告**  
    - EF Core 3+ 在依赖注入时可能抛出 `More than twenty 'IServiceProvider'` 警告，影响性能 。  
    - **解决方案**：优化服务注册逻辑，避免重复注入。

11. **加密与 TLS 版本强制要求**  
    - 新版 SqlClient 默认使用 TLS 1.2+，若服务器仅支持旧版 TLS 会导致连接失败 。  
    - **解决方案**：在代码中显式设置 `ServicePointManager.SecurityProtocol`。

---

#### 升级建议
1. **版本矩阵检查**：参考 [.NET/EF Core 兼容性文档](https://docs.microsoft.com/en-us/ef/core/miscellaneous/platforms) 确保版本匹配。  
2. **迁移测试**：使用 `dotnet ef migrations script` 生成 SQL 脚本验证兼容性。  
3. **启用 Nullable 引用类型**：统一代码与数据库的 NULL 语义。  
4. **监控中断性变更日志**：关注 EF Core 官方 [Breaking Changes](https://docs.microsoft.com/en-us/ef/core/what-is-new/breaking-changes) 文档。  

