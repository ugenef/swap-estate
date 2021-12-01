using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwapEstate.Chats.Infra.Ef
{
    [Table("chat_messages")]
    internal class ChatMessageDao
    {
        [Key]
        public long Id { get; set; }
        public string LoginFrom { get; set; }
        public string LoginTo { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public bool Watched { get; set; }
    }
}