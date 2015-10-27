using System.Collections.Generic;

namespace Infrastructure.Messages
{
    public interface IMessageService
    {
        void AddMessage(string text, MessageTypes type);
        List<Message> Messages { get; }
    }
}
