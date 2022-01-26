namespace Server.BL.Abstractions
{
    public interface IUserStatusAnnouncer
    {
        void AnnounceConnection(string userName);

        void AnnounceDisconnection(string userName);
    }
}
