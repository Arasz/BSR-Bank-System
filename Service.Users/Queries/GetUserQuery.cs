﻿using Core.CQRS.Queries;
using Data.Core;

namespace Service.UserAccount.Queries
{
    public class GetUserQuery : IQuery<User>
    {
        public GetUserQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }
    }
}