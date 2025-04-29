using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // RabbitMQ connection setup
            var factory = new ConnectionFactory() { HostName = "host.docker.internal", UserName = "root", Password = "123123" }; // Adjust hostname as needed
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            // Declare a queue
            string queueName = "demo";
            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            // 設定 prefetch count = 1，確保一次只處理一個 message
            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine("Connected to RabbitMQ. Waiting for messages...");
            // Set up a consumer to receive messages
            var consumer = new AsyncEventingBasicConsumer(channel);
            var random = new Random(); // Initialize Random instance
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");

                int delay = random.Next(1000, 5001); // Generate random delay in milliseconds (3000-5000)
                await Task.Delay(delay); // Use Task.Delay for asynchronous delay

                // 手動確認 message
                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);

            await Task.Delay(-1); // 永遠不結束
        }
    }
}