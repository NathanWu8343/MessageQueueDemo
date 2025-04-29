using RabbitMQ.Client;
using System.Text;
using System;
using System.Diagnostics; // Ensure Random is available

namespace Producer
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            int pid = Process.GetCurrentProcess().Id;

            // RabbitMQ connection setup
            var factory = new ConnectionFactory() { HostName = "host.docker.internal", UserName = "root", Password = "123123" }; // Adjust hostname as needed
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            // Declare a queue
            string queueName = "demo";
            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            Console.WriteLine("Connected to RabbitMQ. Sending messages...");

            var random = new Random(); // Initialize Random instance

            // Infinite loop to send data with random delay
            int messageCount = 0;
            while (true)
            {
                string message = $"[{pid}] Message {++messageCount}";
                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);

                Console.WriteLine($"Sent: {message}");
                int delay = random.Next(0, 1001); // Generate random delay in milliseconds (0-1000)
                await Task.Delay(delay); // Use Task.Delay for asynchronous delay
            }
        }
    }
}