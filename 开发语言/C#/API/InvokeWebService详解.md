## InvokeWebService详解

##### 1.代码解释

- 方法名：InvokeWebService
- 参数：url（Web服务的URL地址）、classname（Web服务的类名）、methodname（Web服务的方法名）、args（Web服务方法的参数）
- 返回值：object类型，表示调用Web服务方法后的返回值

该方法的主要流程如下：

- 根据传入的Web服务URL地址获取Web服务的WSDL描述。
- 使用System.Web.Services.Description和System.CodeDom命名空间的类来生成客户端代理类的代码。
- 使用CSharpCodeProvider编译代理类的代码。
- 使用Assembly类来加载代理类并实例化该类，获取Web服务方法的MethodInfo，使用Activator实例化该方法所在的类，并调用该方法。



##### 2.技术细节

- 使用 WebClient 类获取 WebService 的 WSDL 文件：使用 WebClient 类可以很方便地获取 WebService 的 WSDL 文件。WSDL 文件是 WebService 的接口定义文件，包含了 WebService 的所有方法、参数和返回值等信息。通过获取 WSDL 文件，我们可以了解 WebService 的接口定义，并将其导入到代码命名空间和编译单元中。
- 使用 ServiceDescriptionImporter 类将 WSDL 文件导入到代码中：ServiceDescriptionImporter 类可以将 WSDL 文件中的信息导入到代码命名空间和编译单元中，使我们能够通过代码访问 WebService 的接口。使用 ServiceDescriptionImporter 类需要指定 WSDL 文件、命名空间和编译单元等信息。
- 使用 CodeCompileUnit 类和 CodeNamespace 类生成客户端代理类代码：CodeCompileUnit 类和 CodeNamespace 类可以生成客户端代理类代码。客户端代理类是一个代理对象，用于调用 WebService 的方法。生成客户端代理类代码需要指定命名空间和编译单元等信息。
- 使用 CSharpCodeProvider 类编译代理类：CSharpCodeProvider 类可以编译客户端代理类代码。编译客户端代理类代码需要指定编译参数，例如是否生成可执行文件、是否生成内存中程序集等。
- 使用 Activator.CreateInstance 方法创建代理对象实例：Activator.CreateInstance 方法可以根据类型信息创建一个对象实例。在这段代码中，使用 Activator.CreateInstance 方法创建代理对象的实例。
- 使用 MethodInfo.Invoke 方法调用代理对象的方法：MethodInfo.Invoke 方法可以调用对象的方法。在这段代码中，使用 MethodInfo.Invoke 方法调用代理对象的方法。