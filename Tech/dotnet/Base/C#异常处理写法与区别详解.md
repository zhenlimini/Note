在C#中，异常处理是编写健壮应用程序的关键部分。下面详细说明C#异常捕捉与抛出的各种写法与区别。

## 1. 基本异常处理结构

### try-catch 块
```csharp
try
{
    // 可能抛出异常的代码
    int result = 10 / int.Parse("0");
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"除零错误: {ex.Message}");
}
catch (FormatException ex)
{
    Console.WriteLine($"格式错误: {ex.Message}");
}
```

### try-catch-finally 块
```csharp
FileStream file = null;
try
{
    file = File.Open("test.txt", FileMode.Open);
    // 文件操作
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"文件未找到: {ex.Message}");
}
finally
{
    file?.Close(); // 无论是否发生异常都会执行
}
```

## 2. 异常抛出方式

### 使用 throw 语句
```csharp
// 抛出当前捕获的异常（保留原始堆栈跟踪）
try
{
    // 某些操作
}
catch (Exception ex)
{
    // 记录日志等操作
    throw; // 推荐方式，保留原始堆栈信息
}
```

### 使用 throw ex（不推荐）
```csharp
try
{
    // 某些操作
}
catch (Exception ex)
{
    // 记录日志
    throw ex; // 不推荐！会重置堆栈跟踪
}
```

### 抛出新异常
```csharp
public void ProcessData(string data)
{
    if (string.IsNullOrEmpty(data))
    {
        throw new ArgumentException("数据不能为空", nameof(data));
    }
    
    if (data.Length < 5)
    {
        throw new InvalidOperationException("数据长度必须大于5");
    }
}
```

### 抛出包含内部异常的异常
```csharp
public void ReadConfiguration(string filePath)
{
    try
    {
        string content = File.ReadAllText(filePath);
        // 解析配置
    }
    catch (IOException ex)
    {
        throw new ConfigurationException("读取配置文件失败", ex);
    }
}
```

## 3. 自定义异常

```csharp
public class ConfigurationException : Exception
{
    public string ConfigurationPath { get; }
    
    public ConfigurationException() : base() { }
    
    public ConfigurationException(string message) : base(message) { }
    
    public ConfigurationException(string message, Exception innerException) 
        : base(message, innerException) { }
        
    public ConfigurationException(string message, string configPath) 
        : base(message)
    {
        ConfigurationPath = configPath;
    }
}

// 使用自定义异常
throw new ConfigurationException("无效的配置格式", "appsettings.json");
```

## 4. 异常过滤 (C# 6.0+)

```csharp
try
{
    // 某些操作
}
catch (HttpRequestException ex) when (ex.StatusCode == 404)
{
    Console.WriteLine("资源未找到");
}
catch (HttpRequestException ex) when (ex.StatusCode >= 500)
{
    Console.WriteLine("服务器错误");
}
catch (Exception ex) when (LogException(ex)) // 带副作用的过滤
{
    // 只有在LogException返回true时才会进入这个catch块
}

private bool LogException(Exception ex)
{
    Console.WriteLine($"记录异常: {ex.Message}");
    return true;
}
```

## 5. 使用 when 关键字的复杂过滤

```csharp
public void ProcessRequest(HttpRequest request)
{
    try
    {
        // 处理请求
    }
    catch (Exception ex) when (IsTransientError(ex))
    {
        Console.WriteLine(" transient错误，准备重试");
        // 重试逻辑
    }
    catch (Exception ex) when (IsBusinessError(ex))
    {
        Console.WriteLine("业务逻辑错误");
        throw new BusinessException("业务处理失败", ex);
    }
}

private bool IsTransientError(Exception ex)
{
    return ex is TimeoutException || 
           (ex is HttpRequestException httpEx && httpEx.Message.Contains("timeout"));
}

private bool IsBusinessError(Exception ex)
{
    return ex is ArgumentException || ex is InvalidOperationException;
}
```

## 6. 异步方法中的异常处理

```csharp
public async Task ProcessAsync()
{
    try
    {
        await SomeAsyncOperation();
        await AnotherAsyncOperation();
    }
    catch (HttpRequestException ex) when (ex.StatusCode == 404)
    {
        Console.WriteLine("异步操作资源未找到");
    }
    catch (AggregateException ae)
    {
        // 处理多个异常
        foreach (var e in ae.InnerExceptions)
        {
            Console.WriteLine($"聚合异常: {e.Message}");
        }
    }
}

public async Task<string> SafeGetStringAsync(string url)
{
    try
    {
        using var client = new HttpClient();
        return await client.GetStringAsync(url);
    }
    catch (HttpRequestException)
    {
        return string.Empty;
    }
}
```

## 7. 全局异常处理

### 控制台应用程序
```csharp
// 全局异常处理
AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
{
    Console.WriteLine($"未处理的异常: {e.ExceptionObject}");
    Environment.Exit(1);
};

// 特定异常处理
TaskScheduler.UnobservedTaskException += (sender, e) =>
{
    Console.WriteLine($"未观察的任务异常: {e.Exception}");
    e.SetObserved();
};
```

### ASP.NET Core 中的异常处理
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    
    // 自定义异常处理中间件
    app.UseMiddleware<CustomExceptionMiddleware>();
}
```

## 8. 最佳实践和区别总结

### throw vs throw ex 的区别
```csharp
void MethodA()
{
    try
    {
        MethodB();
    }
    catch (Exception ex)
    {
        Console.WriteLine("throw; 的堆栈跟踪:");
        Console.WriteLine(ex.StackTrace);
        throw; // 保留原始堆栈信息
    }
}

void MethodB()
{
    try
    {
        MethodC();
    }
    catch (Exception ex)
    {
        Console.WriteLine("throw ex; 的堆栈跟踪:");
        Console.WriteLine(ex.StackTrace);
        throw ex; // 重置堆栈信息，丢失MethodC的信息
    }
}
```

### 各种写法的适用场景

1. **catch 和 throw** - 当需要记录日志但让异常继续传播时
2. **catch 和 throw ex** - 几乎从不使用，会丢失堆栈信息
3. **创建新异常** - 当需要提供更具体的错误信息时
4. **异常过滤** - 当需要根据异常的具体条件进行不同处理时
5. **自定义异常** - 当需要表达特定的业务逻辑错误时

### 性能考虑
```csharp
// 好的做法：在进入方法时验证参数
public void ProcessData(string data)
{
    if (string.IsNullOrEmpty(data))
        throw new ArgumentNullException(nameof(data));
    
    // 主要逻辑
}

// 不好的做法：依赖异常处理正常流程
public int FindIndex(string[] array, string value)
{
    try
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
                return i;
        }
        throw new Exception("未找到"); // 不要用异常控制正常流程
    }
    catch
    {
        return -1;
    }
}
```

这些异常处理技术可以帮助你编写更健壮、可维护的C#代码，关键是根据具体场景选择合适的方法。