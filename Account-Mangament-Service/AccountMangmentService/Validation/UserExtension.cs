using System;
using System.IdentityModel.Tokens;
using System.Linq;
using Core.Common.Security;
using Data.Core;

namespace Service.Bank.Validation
{
    public static class UserExtension
    {
        /// <exception cref="SecurityTokenException"> Incorrect user name or password </exception>
        public static void AuthenticateUser(this User user, string userName, string password)
        {
            AuthenticateUser(user, userName, password, new DefaultPasswordHasher());
        }

        /// <exception cref="SecurityTokenException"> Incorrect user name or password </exception>
        public static void AuthenticateUser(this User user, string userName, string password, IPasswordHasher passwordHasher)
        {
            if (IsAnyIncorrect(userName, password) || user.Name != userName)
                throw new SecurityTokenException("Wrong user name or password.");

            var userPasswordBytes = Convert.FromBase64String(user.Password);

            var salt = userPasswordBytes
                .Skip(passwordHasher.PasswordHashLength)
                .ToArray();

            var givenPasswordHash = passwordHasher.HashPassword(password, salt);

            if (givenPasswordHash != user.Password)
                throw new SecurityTokenException("Wrong user name or password.");
        }

        private static bool IsAnyIncorrect(string userName, string password) => string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password);
    }
}