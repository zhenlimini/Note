## Socket/HTTP/TCPIP 关联/使用/实例

#### 1.Socket

- Socket 库可以用于实现基于 TCP/IP 协议的网络应用程序，包括 HTTP 协议。HTTP 是基于 TCP/IP 协议的应用层协议，它使用 TCP 协议提供可靠的数据传输服务。通过使用 Socket 库，我们可以在应用层直接操作 TCP 协议，从而实现 HTTP 协议。

- 在使用 Socket 库实现 HTTP 时，我们需要手动处理 HTTP 请求和响应的格式和内容。HTTP 请求通常包含请求行、请求头和请求体三个部分，而 HTTP 响应通常包含状态行、响应头和响应体三个部分。我们可以通过解析请求和响应的字符串，获取其中的各个部分，从而进行必要的处理和操作。

  ```C#
  string host = "example.com";
  int port = 80;
  string path = "/index.html";
  using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
  {
      socket.Connect(host, port);
      string request = $"GET {path} HTTP/1.1\r\nHost: {host}\r\n\r\n";
      byte[] buffer = Encoding.UTF8.GetBytes(request);
      socket.Send(buffer);
      buffer = new byte[1024];
      int count = socket.Receive(buffer);
      string response = Encoding.UTF8.GetString(buffer, 0, count);
      Console.WriteLine(response);
  }
  ```

- 