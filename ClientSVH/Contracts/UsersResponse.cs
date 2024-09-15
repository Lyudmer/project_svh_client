namespace ClientSVH.Contracts
{
    public record UsersResponse(
          Guid id,
          string username,
          string email,
          string password
          );

}
