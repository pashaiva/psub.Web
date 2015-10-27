namespace Infrastructure.Messages
{
    public enum MessageTypes
    {
        Success,
        Info,
        Warning,
        Danger
    }

    public class Message
    {
        public string Text { get; set; }
        public MessageTypes Type { get; set; }

        public Message(string text, MessageTypes type)
        {
            Text = text;
            Type = type;
        }
    }
}
