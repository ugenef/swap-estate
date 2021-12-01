using System;

namespace SwapEstate.Chats.Abstract.Model
{
    public class ChatMessage
    {
        public string LoginFrom { get; set; }
        public string LoginTo { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public bool Watched { get; set; }
    }
}