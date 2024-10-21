using System.IO;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Text;





string? userName = "";


using TcpClient tcpClient = new TcpClient();
Console.WriteLine($"Клиент  запущен");
await tcpClient.ConnectAsync("127.0.0.1", 13000);

var stream = tcpClient.GetStream();
StreamReader? Reader = null;
StreamWriter? Writer = null;

if (tcpClient.Connected)
{
  
    Reader = new StreamReader(tcpClient.GetStream());
    Writer = new StreamWriter(tcpClient.GetStream());
    if (Writer is null || Reader is null) return;



    byte[] buffer = new byte[256];

    int bytesRead = stream.Read(buffer, 0, buffer.Length);
    string loginRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    Console.Write(loginRequest);

    
    string login = Console.ReadLine();
    byte[] loginData = Encoding.UTF8.GetBytes(login);
    stream.Write(loginData, 0, loginData.Length);

    
    bytesRead = stream.Read(buffer, 0, buffer.Length);
    string passwordRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    Console.Write(passwordRequest);

 
    string password = Console.ReadLine();
    byte[] passwordData = Encoding.UTF8.GetBytes(password);
    stream.Write(passwordData, 0, passwordData.Length);


    bytesRead = stream.Read(buffer, 0, buffer.Length);
    string messVhod = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    Console.Write(messVhod);



    Task.Run(() => ReceiveMessageAsync(Reader));

    await SendMessageAsync(Writer);
}
else
    Console.WriteLine("Не удалось подключиться");
Writer?.Close();
Reader?.Close();


async Task SendMessageAsync(StreamWriter writer)
{
    await writer.WriteLineAsync(userName);
    await writer.FlushAsync();

    await Task.Delay(1000);
    Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter");

    while (true)
    {
        string? message = Console.ReadLine();
        await writer.WriteLineAsync(message);
        await writer.FlushAsync();
    }
}

async Task ReceiveMessageAsync(StreamReader reader)
{
    while (true)
    {
        try
        {
            
            string? message = await reader.ReadLineAsync();
            
            if (string.IsNullOrEmpty(message)) continue;
            Console.WriteLine(message);
        }
        catch
        {
            break;
        }
    }
}
async Task GetName(StreamReader reader)
{
    while (true)
    {
        try
        {

            string? message = await reader.ReadLineAsync();

            if (string.IsNullOrEmpty(message)) continue;
            Console.WriteLine(message);
        }
        catch
        {
            break;
        }
    }
}
