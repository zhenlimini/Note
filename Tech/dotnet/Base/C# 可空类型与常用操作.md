# C# 可空类型与常用操作

## 什么是可空类型？

可空类型（Nullable Types）允许值类型（如 int、double、bool 等）接受 null 值。在 C# 中，值类型通常不能为 null，但可空类型打破了这一限制。

## 声明可空类型

### 1. 使用语法糖 `?`
```csharp
int? nullableInt = null;
double? nullableDouble = 3.14;
bool? nullableBool = null;
DateTime? nullableDate = DateTime.Now;
```

### 2. 使用 `Nullable<T>` 结构
```csharp
Nullable<int> nullableInt = null;
Nullable<double> nullableDouble = 3.14;
```

## 常用操作和方法

### 1. 检查是否有值
```csharp
int? number = 42;

if (number.HasValue)
{
    Console.WriteLine($"值为: {number.Value}");
}
else
{
    Console.WriteLine("值为 null");
}

// 或者使用 null 检查
if (number != null)
{
    Console.WriteLine($"值为: {number.Value}");
}
```

### 2. 安全获取值
```csharp
int? nullableNumber = null;

// 使用 GetValueOrDefault() - 如果为null返回默认值
int result1 = nullableNumber.GetValueOrDefault(); // 返回 0
int result2 = nullableNumber.GetValueOrDefault(100); // 返回 100

// 使用 null 合并运算符 ??
int result3 = nullableNumber ?? 50; // 如果为null返回50

// 使用 null 条件运算符 ?.
string? text = null;
int? length = text?.Length; // 如果text为null，length也为null
```

### 3. 类型转换
```csharp
// 可空类型与非可空类型的转换
int? nullableInt = 10;
int normalInt = nullableInt.Value; // 直接取值（如果为null会抛出异常）

// 安全转换
int safeInt = nullableInt ?? 0;

// 使用 as 运算符（只适用于引用类型）
string text = "hello";
string? nullableText = text as string;
```

### 4. 算术运算
```csharp
int? a = 10;
int? b = null;
int? c = 5;

int? result1 = a + c;    // 15
int? result2 = a + b;    // null
int? result3 = b * c;    // null
```

### 5. 比较运算
```csharp
int? x = 10;
int? y = null;
int? z = 10;

Console.WriteLine(x == z);    // True
Console.WriteLine(x == y);    // False
Console.WriteLine(y == null); // True
Console.WriteLine(x > z);     // False
Console.WriteLine(x > y);     // False
```

## 实用模式和技巧

### 1. 链式 null 检查
```csharp
class Person
{
    public string? Name { get; set; }
    public Address? Address { get; set; }
}

class Address
{
    public string? City { get; set; }
}

Person? person = null;

// 传统方式
string? city = null;
if (person != null && person.Address != null)
{
    city = person.Address.City;
}

// 使用 null 条件运算符
string? modernCity = person?.Address?.City;
```

### 2. 空合并赋值运算符 `??=`
```csharp
int? maybeNumber = null;
maybeNumber ??= 10; // 如果为null则赋值为10
Console.WriteLine(maybeNumber); // 10

maybeNumber ??= 20; // 不为null，保持原值
Console.WriteLine(maybeNumber); // 10
```

### 3. 模式匹配
```csharp
object? value = 42;

if (value is int number)
{
    Console.WriteLine($"是整数: {number}");
}

if (value is null)
{
    Console.WriteLine("值为 null");
}

// 使用 switch 表达式
string result = value switch
{
    int i => $"整数: {i}",
    string s => $"字符串: {s}",
    null => "空值",
    _ => "其他类型"
};
```

### 4. 可空泛型
```csharp
List<int?> nullableList = new List<int?> { 1, null, 3, null, 5 };

// 过滤掉 null 值
List<int> nonNullList = nullableList
    .Where(x => x.HasValue)
    .Select(x => x!.Value)
    .ToList();

// 或者使用 OfType
List<int> anotherList = nullableList.OfType<int>().ToList();
```

## 实际应用示例

### 数据库数据读取
```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Age { get; set; }
    public DateTime? LastLogin { get; set; }
}

// 模拟从数据库读取数据
User user = GetUserFromDatabase(1);

// 安全处理可空字段
string ageDisplay = user.Age.HasValue 
    ? $"{user.Age} 岁" 
    : "年龄未知";

string lastLoginDisplay = user.LastLogin?.ToString("yyyy-MM-dd") 
    ?? "从未登录";

Console.WriteLine($"姓名: {user.Name}");
Console.WriteLine($"年龄: {ageDisplay}");
Console.WriteLine($"最后登录: {lastLoginDisplay}");
```

### 配置处理
```csharp
public class AppConfig
{
    public int? MaxConnections { get; set; }
    public int? TimeoutSeconds { get; set; }
}

AppConfig config = LoadConfig();

// 使用默认值
int maxConnections = config.MaxConnections ?? 100;
int timeout = config.TimeoutSeconds ?? 30;

Console.WriteLine($"最大连接数: {maxConnections}");
Console.WriteLine($"超时时间: {timeout}秒");
```

## 最佳实践

1. **明确意图**：使用可空类型明确表示某个值可能不存在
2. **尽早检查**：在方法开始处检查必要的可空参数
3. **提供默认值**：使用 `??` 运算符提供合理的默认值
4. **文档说明**：在公共 API 中明确标注可空参数和返回值
5. **谨慎使用**：不要过度使用可空类型，只在真正需要的地方使用

可空类型是 C# 中处理可能缺失值的强大工具，合理使用可以提高代码的健壮性和可读性。