using System.Linq;
using System.Threading.Tasks;
using SwapEstate.Chats.Abstract;
using SwapEstate.Chats.Abstract.Model;
using SwapEstate.Chats.Infra;
using SwapEstate.Chats.Infra.Ef;

namespace SwapEstate.Chats.Service
{
    internal class MessagesService : IMessagesService
    {
        private readonly IMessagesRepository _db;

        public MessagesService(IMessagesRepository db)
        {
            _db = db;
        }

        public async Task MarkReadAsync(string loginFrom, string loginTo)
        {
            var daos = await _db.FindAllAsync(loginFrom, loginTo).ConfigureAwait(false);
            foreach (var dto in daos)
            {
                dto.Watched = true;
            }

            await _db.UpdateAsync(daos).ConfigureAwait(false);
        }

        public Task SaveMessages(ChatMessage[] msgs)
        {
            return _db
                .CreateAsync(msgs
                    .Select(Convert)
                    .ToArray());
        }

        public async Task<ChatMessage[]> GetMessagesAsync(string loginFrom, string loginTo)
        {
            var daos = await _db.FindAllAsync(loginFrom, loginTo).ConfigureAwait(false);
            return daos
                .Select(Convert)
                .ToArray();
        }
        private ChatMessageDao Convert(ChatMessage chatMessage)
        {
            return new ChatMessageDao
            {
                LoginFrom = chatMessage.LoginFrom,
                LoginTo = chatMessage.LoginTo,
                Message = chatMessage.Message,
                Time = chatMessage.Time,
                Watched = chatMessage.Watched
            };
        }

        private ChatMessage Convert(ChatMessageDao chatMessageDao)
        {
            return new ChatMessage
            {
                LoginFrom = chatMessageDao.LoginFrom,
                LoginTo = chatMessageDao.LoginTo,
                Message = chatMessageDao.Message,
                Time = chatMessageDao.Time,
                Watched = chatMessageDao.Watched
            };
        }
    }
}