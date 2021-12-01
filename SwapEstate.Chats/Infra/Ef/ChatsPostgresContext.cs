using System.Linq;
using Microsoft.EntityFrameworkCore;
using SwapEstate.Chats.Abstract.Model;

namespace SwapEstate.Chats.Infra.Ef
{
    internal class ChatsPostgresContext : DbContext
    {
        private readonly string _connString;

        public ChatsPostgresContext(string connString)
        {
            _connString = connString;
            ChatMessageDaos = base.Set<ChatMessageDao>();
        }
        public IQueryable<ChatMessageDao> ChatMessageDaos { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connString);
        }
        
        public DbSet<ChatMessageDao> ChatMessages { get; set; }
    }
}