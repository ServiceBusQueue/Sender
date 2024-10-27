using Azure.Messaging.ServiceBus;
class Program
{
    // Create a Service Bus client
    private const string connectionString = "<ConnectioonString />";
    private const string queueName = "<QueuName />";

    static async Task Main(string[] args)
    {
        await SendMessagesAsync();
    }

    static async Task SendMessagesAsync()
    {
        // Create a Service Bus client
        await using var client = new ServiceBusClient(connectionString);
        // Create a sender for the queue
        ServiceBusSender sender = client.CreateSender(queueName);

        try
        {
            for (int i = 0; i < 10; i++)
            {
                // Create a message
                ServiceBusMessage message = new ServiceBusMessage($"Message {i}");
                // Send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent: Message {i}");
            }
        }
        finally
        {
            // Dispose the sender
            await sender.DisposeAsync();
        }
    }
}