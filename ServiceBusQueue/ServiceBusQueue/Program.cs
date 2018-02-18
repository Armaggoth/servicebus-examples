using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Azure ServiceBus Namespace
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusQueue
{
    class Program
    {
        //connection string to the service bus, this can be found under the shared access key in the bus or queue where configured.
        //cdelgado = domain root (my bus's name) 
        //SharedAccessKeyName =  the shared access key name to use for permissions
        //SharedAccessKey = The key itself, found under the shared access key (key 1)
        //EntityPath = Queue name
        private static string _conn = "Endpoint=sb://cdelgado.servicebus.windows.net/;SharedAccessKeyName=MyPolicy;SharedAccessKey=NBa3KO2gb4mJ+bH4834ZUfEi55CGzirVgmIy168VgPA=;EntityPath=myqueue";
        //                             Endpoint=sb://cdelgado.servicebus.windows.net/;SharedAccessKeyName=MyPolicy;SharedAccessKey=NBa3KO2gb4mJ+bH4834ZUfEi55CGzirVgmIy168VgPA=
        //Queue client from the connection string
        private static QueueClient _client = QueueClient.CreateFromConnectionString(_conn);

        static void Main(string[] args)
        {
            //we can send json data here :)
            //SendMessage("hello world");
            ReadMessage();

        }

        //Send a message
        static void SendMessage(string msg)
        {
            var message = new BrokeredMessage(msg);
            _client.Send(message);
        }

        //Read and process a message
        static void ReadMessage()
        {

            var options = new OnMessageOptions
            {
                //If true, autocompletes the message as soon as it's read off the queue
                //If false, the Complete() method has to be called to complete the message.
                AutoComplete = false,
            };

            _client.OnMessage(m =>
            {
                //Get the string data outta the queue
                var msg = m.GetBody<string>();
                //"Process" the message
                if (msg == "hello world")
                {
                    Console.WriteLine(msg);
                    Console.ReadKey();
                    //Completes the message
                    m.Complete();
                }

            }, options);
        }
    }
}
