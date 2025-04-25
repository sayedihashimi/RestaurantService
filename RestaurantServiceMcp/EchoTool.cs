using ModelContextProtocol.Server;
using System.ComponentModel;

[McpServerToolType]
public static class EchoTool{
    [McpServerTool, Description("Echos the message back to the client.")]
    public static string Echo(string message) => $"Hello from C#: {message}";

    public static string ReverseEcho(string message)=> $"Hello from C#: {message.Reverse()}";
}