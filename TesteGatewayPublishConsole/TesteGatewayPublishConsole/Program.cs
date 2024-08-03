using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace TesteGatewayPublishConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string read = Console.ReadLine();
            string queue = "prototype-hms-1";
            string exchange = "prototype-hms-1";
            string post = "client.post";
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var bytes = Encoding.UTF8.GetBytes("read");


            channel.ExchangeDeclare(exchange, "direct");

            channel.QueueDeclare(queue,
                false,
                false,
                false,
                null);
            channel.QueueBind(queue, 
                exchange, 
                post);

            channel.ExchangeBind(destination: exchange, 
                source: exchange, 
                routingKey: post);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ar, args) => {
                Console.WriteLine(Encoding.UTF8.GetString(args.Body.ToArray()));
            };

            while(read.Length > 0)
            {
                read = Console.ReadLine();
                channel.BasicPublish("prototype-hms-1", 
                    "client.post",
                    null, 
                    bytes);
                /*channel.BasicConsume(queue, true, consumer);*/
            }
        }
    }
}
