using CQRS.Queries;

namespace AccountMangmentService.Authentication.Queries
{
    /// <summary>
    /// User authentication query. If user can be authenticated query returns user token 
    /// </summary>
    public class UserAuthenticationQuery : IQuery<string>
    {
        public string Password { get; set; }

        public string UserName { get; set; }
    }
}