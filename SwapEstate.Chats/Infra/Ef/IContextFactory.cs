namespace SwapEstate.Chats.Infra.Ef
{
    internal interface IContextFactory
    {
        ChatsPostgresContext Get();
    }
}