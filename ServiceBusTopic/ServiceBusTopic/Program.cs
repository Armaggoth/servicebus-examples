using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusTopic
{
    class Program
    {
        //Connection string
        private static string _conn = "Endpoint=sb://cdelgado.servicebus.windows.net/;SharedAccessKeyName=MyPolicy;SharedAccessKey=NBa3KO2gb4mJ+bH4834ZUfEi55CGzirVgmIy168VgPA=";
        private static string _topic = "mytopic";
        
        static void Main(string[] args)
        {
            //SendMessage(" Hello World!");
            ReadMessage();
            Console.ReadLine();
        }
        
        static void SendMessage(string message)
        {
            var topicClient = TopicClient.CreateFromConnectionString(_conn, _topic);
            var msg = new BrokeredMessage(message);
            topicClient.Send(msg);
        }

        static void ReadMessage()
        {
            var subClient = SubscriptionClient.CreateFromConnectionString(_conn, _topic, "ConsoleApp");
            subClient.OnMessage(m =>
            {
                Console.WriteLine(m.GetBody<string>());
            });
        }
    }
}
