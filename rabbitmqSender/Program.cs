using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Sernder App";

IConnection cnn = factory.CreateConnection ();

IModel  channel = cnn.CreateModel ();

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName,ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName,exchangeName, routingKey, null);

byte[] messageBodyByte = Encoding.UTF8.GetBytes("Yaka Vaqef");
channel.BasicPublish(exchangeName, routingKey, null, messageBodyByte);

channel.Close();
cnn.Close();
