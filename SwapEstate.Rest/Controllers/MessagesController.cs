using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwapEstate.Chats.Abstract;
using SwapEstate.Chats.Abstract.Model;
using SevenSem.Rest.Constants;
using SevenSem.Rest.Dto;

namespace SevenSem.Rest.Controllers
{
    [Route(ApiVersion.V1 + "/messages"), Authorize]
    public class MessagesController : Controller
    {
        private readonly IMessagesService _domainService;

        public MessagesController(IMessagesService domainService)
        {
            _domainService = domainService;
        }

        [HttpPost, Route("send")]
        public Task Send([FromBody] MessageDto[] messages)
        {
            return _domainService
                .SaveMessages(messages
                    .Select(Convert)
                    .ToArray());
        }

        [HttpPost, Route("markRead")]
        public Task MarkAsRead(string loginFrom, string loginTo)
        {
            return _domainService
                .MarkReadAsync(loginFrom, loginTo);
        }

        [HttpGet, Route("getMessages")]
        public async Task<MessageDto[]> GetAllMessages(string login1, string login2)
        {
            var msg = await _domainService.GetMessagesAsync(login1, login2).ConfigureAwait(false);
            return msg.Select(Convert).ToArray();
        }

        private MessageDto Convert(ChatMessage chatMessage)
        {
            return new MessageDto
            {
                LoginFrom = chatMessage.LoginFrom,
                LoginTo = chatMessage.LoginTo,
                Message = chatMessage.Message,
                Time = chatMessage.Time,
                Watched = chatMessage.Watched
            };
        }

        private ChatMessage Convert(MessageDto chatMessageDto)
        {
            return new ChatMessage
            {
                LoginFrom = chatMessageDto.LoginFrom,
                LoginTo = chatMessageDto.LoginTo,
                Message = chatMessageDto.Message,
                Time = chatMessageDto.Time,
                Watched = chatMessageDto.Watched
            };
        }
    }
}