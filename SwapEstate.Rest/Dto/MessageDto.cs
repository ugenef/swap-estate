using System;

namespace SevenSem.Rest.Dto
{
    public class MessageDto
    {
        public string LoginFrom { get; set; }
        public string LoginTo { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public bool Watched { get; set; }
    }
}