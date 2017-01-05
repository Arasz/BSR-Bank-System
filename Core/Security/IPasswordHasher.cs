﻿namespace Shared.Security
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Creates secure salted password hash. Salt is generated by secure random generator 
        /// </summary>
        /// <param name="password"> Plain text password </param>
        /// <returns> Base 64 string password hash combined with salt </returns>
        string HashPassword(string password);

        /// <summary>
        /// Creates secure salted password hash. Salt should by generated by secure random generator. 
        /// </summary>
        /// <param name="password"> Plain text password </param>
        /// <param name="salt"> Random salt </param>
        /// <returns> Base 64 string password hash combined with salt </returns>
        string HashPassword(string password, byte[] salt);
    }
}