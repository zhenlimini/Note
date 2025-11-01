## DLL缺失的运行状态

### 案例场景

- C# .NET 我编译了一个程序后，将一个依赖的dll从其中删除，正常运行后出现报错；假设我有一个类Class1 有一个方法Method1，在这个方法的中间一段调用了这个我删除的dll的方法，那么什么时候会报错？ 是在程序一开始就报错，还是到我实例化Class1时报错，还是我刚调用Method1就报错；还是Method1程序逻辑真正到达调用的具体那个dll的方法时才报错？
- 基本结论：在调用方法时会立即报错，而不是等到执行到具体使用缺失 DLL 的代码时才报错；



### 结论与原理

1. **按需加载程序集**：
   - .NET 采用按需加载程序集的机制，即只有在真正用到某个类型时，才会去加载包含该类型的程序集。因此，在实例化时，如果没有用到缺失的类型，程序不会报错。
2. **JIT 编译器的行为**：
   - JIT（即时编译器）在方法第一次被调用时会编译整个方法的代码。在这个过程中，JIT 编译器会检查方法中用到的所有类型。
   - 如果方法中引用的类型定义在缺失的 DLL 中，JIT 编译器会尝试加载该程序集。如果找不到，会在方法调用时抛出 `FileNotFoundException`，而不是等到执行到具体的代码行时才报错。
3. **类型检查与代码生成**：
   - JIT 编译器需要在编译方法时确定所有类型的信息，以便生成正确的机器码。如果某个类型不存在，JIT 编译器无法继续编译该方法，因此在方法调用时就抛出异常。
4. **反射加载的差异**：
   - 如果使用反射来动态加载类型或程序集，报错的时间点会有所不同。例如，使用 `Type.GetType()` 或 `Assembly.Load()` 时，可能在运行时才会报错，具体取决于加载逻辑。



### 总结与其他说明

1. **JIT编译与IL代码**： 在C#程序中，所有的代码首先被编译成中间语言（IL）代码，然后在运行时通过JIT（即时编译器）将这些IL代码转换为平台特定的机器码。JIT编译是惰性执行的，只有当实际调用某个方法时，JIT才会尝试将其转换为机器码。
2. **条件分支中的方法调用**：
   - **`if (flag)`：** 当你定义了 `var flag = false;`，在这种情况下，`JsonConvert.SerializeObject("")` 是不会被调用的。然而，编译器依然需要检查 `TestDLL` 方法是否可能包含对 `JsonConvert.SerializeObject` 的调用。如果你没有正确地剔除它，编译器可能不会在IL中做足够的优化来剔除这种静态引用。因此，当你进入 `TestDLL` 方法时，它实际上会尝试解析这个方法调用，而由于缺少依赖项（即 `Newtonsoft.Json.dll`），就会抛出异常。
   - **`if (false)`：** 在这种情况下，`JsonConvert.SerializeObject("")` 被包裹在一个永远不会执行的条件分支中。IL编译器知道这一点，因此它会优化掉 `JsonConvert.SerializeObject` 的调用。最终生成的IL代码中，根本就没有对 `JsonConvert.SerializeObject` 的任何引用，程序也不会尝试加载 `Newtonsoft.Json.dll`，所以你不会遇到缺少DLL的错误。
3. **条件编译分支**：即使 `flag` 是 `false`，JIT 编译器仍然可能解析到 `JsonConvert.SerializeObject` 的调用。如果没有通过编译时优化（如 `if (false)` 的情况），它会在运行时尝试解析该方法，这会导致程序集找不到的问题。
4. **JIT的延迟执行**：当你调用 `TestDLL` 方法时，JIT 编译器会尝试解析 `JsonConvert.SerializeObject`，但如果 `Newtonsoft.Json.dll` 不存在，它就会报错。
5. **优化与条件分支**：如果条件永远不成立（例如 `if (false)`），JIT编译器在生成IL代码时会完全忽略该方法调用，因此不会尝试加载缺失的DLL。



### .NetCore与.NetFramework的不同现象

在我使用.NetCore+Nuget包管理工具时，我即使删除了生成目录下面的dll依旧可以运行；最终发现是由于.runtimeconfig.dev.json文件的存在，**在运行目录下不存在时依旧会去其他目录寻找依赖**；

案例说明如下：

这个文件 `.runtimeconfig.dev.json` 是 .NET Core 项目中一个与运行时配置相关的文件。它的作用是配置运行时的附加设置，特别是一些与程序集查找路径、调试和开发环境相关的选项。

#### 文件内容解析

```json
{
  "runtimeOptions": {
    "additionalProbingPaths": [
      "C:\\Users\\zhen li\\.dotnet\\store\\|arch|\\|tfm|",
      "C:\\Users\\zhen li\\.nuget\\packages",
      "E:\\Program Files (x86)\\Microsoft Visual Studio\\Shared\\NuGetPackages"
    ]
  }
}
```

##### 1. **`runtimeOptions`**:

`runtimeOptions` 是 .NET Core 应用程序的运行时配置选项部分。它包含与运行时行为相关的设置，例如如何查找和加载程序集。

##### 2. **`additionalProbingPaths`**:

`additionalProbingPaths` 配置项指定了附加的程序集查找路径。当 .NET Core 在运行时尝试加载程序集时，除了默认的查找路径（例如应用程序目录或项目输出目录），它还会在这些额外的路径中查找。

这三个路径的含义分别是：

- **`C:\\Users\\zhen li\\.dotnet\\store\\|arch|\\|tfm|`**:
  这是 .NET Core 的本地 NuGet 包存储目录，包含已安装的 SDK 和运行时相关的组件。这是 .NET Core 在构建和运行应用程序时存储已下载和缓存的包的位置，`|arch|` 和 `|tfm|` 会根据系统架构和目标框架进行替换。比如 `|arch|` 会替换为 `x64` 或 `x86`，`|tfm|` 会替换为目标框架名称（如 `netcoreapp3.0`）。

- **`C:\\Users\\zhen li\\.nuget\\packages`**:
  这是 NuGet 的默认包缓存位置。所有通过 NuGet 安装的包都存储在此目录中。这意味着如果你在项目中通过 NuGet 引用某个包，.NET Core 会首先查找这个目录，以便快速找到并加载已经下载的包。

- **`E:\\Program Files (x86)\\Microsoft Visual Studio\\Shared\\NuGetPackages`**:
  这是 Microsoft Visual Studio 使用的共享 NuGet 包目录。如果你在 Visual Studio 中安装了 NuGet 包，并且 Visual Studio 的配置指向这个路径，那么 .NET Core 会在此路径下查找依赖的包。

##### 总结

`LessonlearnTest.runtimeconfig.dev.json` 文件的 `additionalProbingPaths` 配置项告诉 .NET Core 在应用程序运行时额外查找程序集的路径。这些路径包括了系统的 NuGet 包缓存目录、.NET SDK 存储路径和 Visual Studio 的共享包目录。这样一来，.NET Core 就能根据这些路径找到依赖项，即使它们不在应用程序的默认目录中。简而言之它帮助 .NET Core 在多个目录中查找需要的程序集和依赖项，确保在不同的环境中都能找到所需的库。

