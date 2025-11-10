using RabbitMQ.Client;

namespace Empo.BuildingBlocks.Application.Configuration.Services;

public interface IRabbitMqService
{
    Task<IConnection> CreateConnection();


    void SendMessage(dynamic _body, string _queueName);
}
