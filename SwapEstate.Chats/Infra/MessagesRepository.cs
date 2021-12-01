using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwapEstate.Chats.Infra.Ef;

namespace SwapEstate.Chats.Infra
{
    internal class MessagesRepository : IMessagesRepository
    {
        private readonly IContextFactory _contextFactory;

        public MessagesRepository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(ChatMessageDao[] messageDaos)
        {
            using (var context = _contextFactory.Get())
            {
                context.ChatMessages.AddRange(messageDaos);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<ChatMessageDao[]> FindAllAsync(string loginFrom, string loginTo)
        {
            using (var context = _contextFactory.Get())
            {
                var msgs = await context
                    .Set<ChatMessageDao>()
                    .AsNoTracking()
                    .Where(m => m.LoginFrom == loginFrom && m.LoginTo == loginTo)
                    .ToArrayAsync()
                    .ConfigureAwait(false);
                return msgs;
            }
        }

        public async Task UpdateAsync(ChatMessageDao[] messageDaos)
        {
            using (var context = _contextFactory.Get())
            {
                context.UpdateRange(messageDaos);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}