using Core.Common.Security;
using Data.Core;
using System;
using System.Linq;
using System.Security.Authentication;
using Data.Core.Entities;

namespace Service.Bank.Extensions
{
    public static class UserExtension
    {
        public static void AuthenticateUser(this User user, string userName, string password)
        {
            AuthenticateUser(user, userName, password, new DefaultPasswordHasher());
        }

        public static void AuthenticateUser(this User user, string userName, string password,
            IPasswordHasher passwordHasher)
        {
            if (IsAnyIncorrect(userName, password) || user.Name != userName)
                throw new InvalidCredentialException("Wrong user name or password.");

            var userPasswordBytes = Convert.FromBase64String(user.Password);

            var salt = userPasswordBytes
                .Skip(passwordHasher.PasswordHashLength)
                .ToArray();

            var givenPasswordHash = passwordHasher.HashPassword(password, salt);

            if (givenPasswordHash != user.Password)
                throw new InvalidCredentialException("Wrong user name or password.");
        }

        private static bool IsAnyIncorrect(string userName, string password)
            => string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password);
    }
}