using System.Threading.Tasks;
using SwapEstate.Chats.Infra.Ef;

namespace SwapEstate.Chats.Infra
{
    internal interface IMessagesRepository
    {
        Task CreateAsync(ChatMessageDao[] messageDaos);
        Task<ChatMessageDao[]> FindAllAsync(string loginFrom, string loginTo);
        Task UpdateAsync(ChatMessageDao[] messageDaos);
    }
}