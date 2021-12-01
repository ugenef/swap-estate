namespace SwapEstate.Chats.Infra.Ef
{
    internal class ContextFactory : IContextFactory
    {
        private readonly string _connString;

        public ContextFactory(string connString)
        {
            _connString = connString;
        }

        public ChatsPostgresContext Get()
        {
            return new ChatsPostgresContext(_connString);
        }
    }
}