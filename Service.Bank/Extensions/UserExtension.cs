using Core.Common.Security;
using Data.Core.Entities;
using System;
using System.Linq;
using System.Security.Authentication;

namespace Service.Bank.Extensions
{
    public static class UserExtension
    {
        /// <summary>
        /// Authenticates user. If user can't be authenticated throws exception. 
        /// </summary>
        /// <exception cref="InvalidCredentialException"> Wrong user name or password. </exception>
        public static void AuthenticateUser(this User user, string userName, string password)
        {
            AuthenticateUser(user, userName, password, new DefaultPasswordHasher());
        }

        /// <summary>
        /// Authenticates user. If user can't be authenticated throws exception. 
        /// </summary>
        /// <param name="user"> Authenticated user </param>
        /// <param name="password"> Given password </param>
        /// <param name="userName"> Given username </param>
        /// <param name="passwordHasher"> Hasher used for hashing </param>
        /// <exception cref="InvalidCredentialException"> Wrong user name or password. </exception>
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