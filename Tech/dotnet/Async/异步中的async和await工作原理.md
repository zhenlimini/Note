## 异步中的async和await工作原理

### async和await

- 在传统的同步编程模型中，线程在执行代码时会阻塞，直到某个操作完成才继续执行。比如，当调用一个网络请求时，程序会在等待响应时停下来，不能做其他工作。

- 而异步编程则允许代码在等待某些操作（比如网络请求、文件I/O等）时不阻塞主线程，而是将这些操作交给操作系统或其他线程来处理，自己可以继续执行其他任务。
- **`async`**：在方法前加上 `async` 关键字后，表示该方法是异步的，并且会返回一个 `Task` 或 `Task<T>`。这意味着方法内部可能包含异步操作。
- **`await`**：`await` 用来等待一个异步操作的完成，并在操作完成后恢复执行后续代码。`await` 只能在 `async` 方法中使用。



### 异步的工作原理

为了更好地理解 `async` 和 `await` 的底层原理，我们可以分为以下几个步骤：

##### 1.1. **编译器生成状态机**

当你使用 `async` 和 `await` 时，C# 编译器会将 `async` 方法转换为一个状态机。这个状态机将管理异步操作的执行流程，包括控制流的切换和恢复。

- **同步代码**：原本的方法代码会被保留，转化为一个状态机的一个部分。
- **异步操作**：在遇到 `await` 时，编译器会将异步操作（比如 `Task`）拆解开来，并标记为“挂起”状态。执行会返回到调用者，让出控制权。

编译后的代码通过状态机管理异步任务的执行，并通过一个特殊的 `MoveNext` 方法（通常是通过 `TaskAwaiter` 或 `AsyncTaskMethodBuilder` 实现）来进行任务调度。



##### 1.2. **状态机的执行过程**

1. **初始调用**：当调用一个 `async` 方法时，方法中的代码会首先执行，直到遇到 `await` 为止。
2. **遇到 `await`**：如果await操作返回的是一个未完成的任务，程序会暂停该方法的执行并退出当前的执行上下文。
   - 任务的执行会被挂起，并且线程会被释放给其他任务执行。
   - `await` 会返回一个 `TaskAwaiter` 对象，这个对象会跟踪任务的完成状态。
3. **任务完成**：当异步操作完成时，`TaskAwaiter` 会通知该方法的状态机，告诉它任务已经完成。方法会通过调用 `MoveNext` 方法恢复执行，继续从 `await` 后的地方开始执行。
4. **恢复执行**：一旦异步操作完成并且 `await` 返回，控制流会恢复，继续执行后续的代码。



##### 1.3. **同步和异步的切换**

一个 `async` 方法的执行实际上是分成了多个小的部分，通常通过一个由状态机和 `MoveNext` 方法来进行管理。异步代码的核心就是能够在运行时不阻塞当前线程，让线程能够快速返回并继续执行其他操作。

**简化流程**：

1. 主线程调用 `async` 方法并开始执行。
2. 执行到 `await` 时，当前线程会退出，等待异步操作。
3. 异步操作完成后，线程会重新执行该方法，继续执行 `await` 后的代码。



##### 1.4. **线程池和调度**

虽然 `async` 和 `await` 看起来是异步的，它们并不意味着每个 `async` 方法都需要新的线程。底层使用的是线程池中的线程，并且通过任务调度来避免不必要的线程切换。

- 如果 `await` 后的操作是在 **UI 线程** 或 **主线程** 中执行，它会回到原来的上下文（通常是通过 `SynchronizationContext` 来实现的）。
- 如果没有指定上下文，操作会在任何线程池线程上继续执行。

这种机制减少了上下文切换的成本，提高了性能。



### 异步的流程运行示例

异步编程中最需弄清的是控制流是如何从方法移动到方法的。 下图可引导你完成此过程：

![](https://learn.microsoft.com/zh-cn/dotnet/csharp/asynchronous-programming/media/task-asynchronous-programming-model/navigation-trace-async-program.png#lightbox)

关系图中的数字对应于以下步骤，在调用方法调用异步方法时启动。

1. 调用方法调用并等待 `GetUrlContentLengthAsync` 异步方法。

2. `GetUrlContentLengthAsync` 可创建 [HttpClient](https://learn.microsoft.com/zh-cn/dotnet/api/system.net.http.httpclient) 实例并调用 [GetStringAsync](https://learn.microsoft.com/zh-cn/dotnet/api/system.net.http.httpclient.getstringasync) 异步方法以下载网站内容作为字符串。

3. `GetStringAsync` 中发生了某种情况，该情况挂起了它的进程。 可能必须等待网站下载或一些其他阻止活动。 为避免阻止资源，`GetStringAsync` 会将控制权出让给其调用方 `GetUrlContentLengthAsync`。

   `GetStringAsync` 返回 [Task](https://learn.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task-1)，其中 `TResult` 为字符串，并且 `GetUrlContentLengthAsync` 将任务分配给 `getStringTask` 变量。 该任务表示调用 `GetStringAsync` 的正在进行的进程，其中承诺当工作完成时产生实际字符串值。

4. 由于尚未等待 `getStringTask`，因此，`GetUrlContentLengthAsync` 可以继续执行不依赖于 `GetStringAsync` 得出的最终结果的其他工作。 该任务由对同步方法 `DoIndependentWork` 的调用表示。

5. `DoIndependentWork` 是完成其工作并返回其调用方的同步方法。

6. `GetUrlContentLengthAsync` 已运行完毕，可以不受 `getStringTask` 的结果影响。 接下来，`GetUrlContentLengthAsync` 需要计算并返回已下载的字符串的长度，但该方法只有在获得字符串的情况下才能计算该值。

   因此，`GetUrlContentLengthAsync` 使用一个 await 运算符来挂起其进度，并把控制权交给调用 `GetUrlContentLengthAsync` 的方法。 `GetUrlContentLengthAsync` 将 `Task<int>` 返回给调用方。 该任务表示对产生下载字符串长度的整数结果的一个承诺。

   *如果 `GetStringAsync`（因此 `getStringTask`）在 `GetUrlContentLengthAsync` 等待前完成，则控制会保留在 `GetUrlContentLengthAsync` 中。 如果异步调用过程 `getStringTask` 已完成，并且 `GetUrlContentLengthAsync` 不必等待最终结果，则挂起然后返回到 `GetUrlContentLengthAsync` 将造成成本浪费。*

   在调用方法中，处理模式会继续。 在等待结果前，调用方可以开展不依赖于 `GetUrlContentLengthAsync` 结果的其他工作，否则就需等待片刻。 调用方法等待 `GetUrlContentLengthAsync`，而 `GetUrlContentLengthAsync` 等待 `GetStringAsync`。

7. `GetStringAsync` 完成并生成一个字符串结果。 字符串结果不是通过按你预期的方式调用 `GetStringAsync` 所返回的。 （记住，该方法已返回步骤 3 中的一个任务）。相反，字符串结果存储在表示 `getStringTask` 方法完成的任务中。 await 运算符从 `getStringTask` 中检索结果。 赋值语句将检索到的结果赋给 `contents`。

8. 当 `GetUrlContentLengthAsync` 具有字符串结果时，该方法可以计算字符串长度。 然后，`GetUrlContentLengthAsync` 工作也将完成，并且等待事件处理程序可继续使用。 在此主题结尾处的完整示例中，可确认事件处理程序检索并打印长度结果的值。 如果你不熟悉异步编程，请花 1 分钟时间考虑同步行为和异步行为之间的差异。 当其工作完成时（第 5 步）会返回一个同步方法，但当其工作挂起时（第 3 步和第 6 步），异步方法会返回一个任务值。 在异步方法最终完成其工作时，任务会标记为已完成，而结果（如果有）将存储在任务中。