namespace AuthService.Domain
{
    public class SessionLimitExceeded() : Exception("Maximum session count exceeded.");
}