using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using StressBall;

Random randomSpeed = new Random();
int nextTest = 1;
Console.WriteLine("UDP Client");

using (UdpClient socket = new UdpClient())
{
    while (true)
    {
        string sensorName = "UDPTest" + nextTest++;

        StressBallData stressBallData = new StressBallData()
        {
            Id = 100,
            //Speed = (randomSpeed.Next(30, 150)).ToString(),
            DateTimeNow = new DateTime(2022, 10, 3)
        };
        string message = JsonSerializer.Serialize(stressBallData);
        byte[] data = Encoding.UTF8.GetBytes(message);
        Console.WriteLine("Broadcaster sent " + message);

        socket.Send(data, data.Length, "127.0.0.1", 10100);
        Thread.Sleep(3000);
    }
}