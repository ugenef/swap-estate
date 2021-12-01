using System.Threading.Tasks;
using SwapEstate.Chats.Abstract.Model;

namespace SwapEstate.Chats.Abstract
{
    public interface IMessagesService
    {
        Task MarkReadAsync(string loginFrom, string loginTo);
        Task SaveMessages(ChatMessage[] msgs);
        Task<ChatMessage[]> GetMessagesAsync(string loginFrom, string loginTo);
    }
}