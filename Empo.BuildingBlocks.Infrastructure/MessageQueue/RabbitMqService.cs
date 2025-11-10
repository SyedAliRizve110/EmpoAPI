using Empo.BuildingBlocks.Application.Configuration.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Empo.BuildingBlocks.Infrastructure.MessageQueue;



public class RabbitMqService : IRabbitMqService
{
    private readonly RabbitMqConfiguration _configuration;
    public RabbitMqService(IOptions<RabbitMqConfiguration> options)
    {
        _configuration = options.Value;
    }
    public async Task<IConnection> CreateConnection()
    {
        ConnectionFactory connection = new ConnectionFactory()
        {
            UserName = _configuration.Username,
            Password = _configuration.Password,
            HostName = _configuration.HostName
        };
        using IConnection _connection = await connection.CreateConnectionAsync();
        return _connection;
    }

    public void SendMessage(dynamic _body, string _queueName)
    {
        using var connection = CreateConnection();
        using var channel = connection.Result.CreateChannelAsync();
        channel.Result.QueueDeclareAsync(queue: _queueName,
                 durable: false,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null);

        var json = JsonConvert.SerializeObject(_body);
        var body = Encoding.UTF8.GetBytes(json);
        channel.Result.BasicPublishAsync(exchange: "", routingKey: _queueName, mandatory: false, basicProperties: null, body: body);
    }
}
