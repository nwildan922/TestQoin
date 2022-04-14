using System;
using System.Threading.Tasks;

namespace Task3.RabbitMQ
{
    public interface IBus
    {
        Task SendAsync<T>(string queue, T message);
    }
}
