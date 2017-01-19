using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Core.Common.Security;
using FluentAssertions;
using Xunit;

namespace SharedTests.Security
{
    public class DefaultPasswordHasherTest
    {
        [Theory]
        [InlineData("DogesEqualityAndHonor", new byte[] { 66, 122, 12, 66, 180, 23, 84, 77, 66, 3, 93, 168, 246, 71, 166, 156 }, "3MmtbNTbJeSmWngoY7l6gsqB1pVCegxCtBdUTUIDXaj2R6ac")]
        [InlineData("VeryLongAndHardPassword12345TooHardTooLong", new byte[] { 66, 122, 12, 66, 180, 23, 84, 77, 66, 3, 93, 168, 246, 71, 166, 156 }, "3YO3MUJhYIFywnGinPb8in4ZxmpCegxCtBdUTUIDXaj2R6ac")]
        [InlineData("s", new byte[] { 66, 122, 12, 66, 180, 23, 84, 77, 66, 3, 93, 168, 246, 71, 166, 156 }, "fq89z8Gw4v5WPAAOjvSdTU4fUV1CegxCtBdUTUIDXaj2R6ac")]
        public void HashPassword_CorectPasswordSaltedHashing_ShouldReturnSaltedPasswordHash(string password, byte[] salt, string saltedHash)
        {
            var passwordHasher = new DefaultPasswordHasher();
            var passwordHash = passwordHasher.HashPassword(password, salt);

            passwordHash.Should()
                .NotBeNullOrEmpty()
                .And.HaveLength(48)
                .And.BeEquivalentTo(saltedHash);
        }

        [Theory]
        [InlineData("Doge1234", new byte[] { 1, 2, 3 })]
        [InlineData("VeryLongAndHardPassword12345TooHardTooLong", new byte[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 22, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 })]
        [InlineData("s", null)]
        [InlineData("dsadsadasdad", new byte[] { })]
        public void HashPassword_CorrectPasswordAndCorrectCustomSalt_ShouldReturnSaltedPasswordHash(string password, byte[] salt)
        {
            var passwordHasher = new DefaultPasswordHasher();
            Action hashPasswordAction = () => passwordHasher.HashPassword(password, salt);
            hashPasswordAction.ShouldThrow<ArgumentException>("Wrong salt");
        }

        [Theory]
        [InlineData("VeryLongAndHardPassword12345TooHardTooLong", new byte[] { 66, 122, 225, 66, 180, 168, 84, 77, 66, 3, 93, 168, 246, 71, 166, 156 })]
        [InlineData("dsadsadasdad", new byte[] { 66, 122, 12, 66, 180, 23, 84, 77, 66, 3, 93, 168, 246, 71, 166, 156 })]
        public void HashPassword_CorrectPasswordAndIncorectSalt_ShouldThrowArgumentException(string password, byte[] salt)
        {
            var passwordHasher = new DefaultPasswordHasher();
            var saltedHash = passwordHasher.HashPassword(password, salt);

            var bytes = Convert.FromBase64String(saltedHash);
            bytes.Skip(bytes.Length - salt.Length)
                .Except(salt)
                .Should().BeEmpty();
        }

        [Theory]
        [InlineData("Doge1234")]
        [InlineData("VeryLongAndHardPassword12345TooHardTooLong")]
        [InlineData("s")]
        public void HashPassword_CorrectPasswordHashingWithGeneratedSalt_ShouldReturnSaltedPasswordHash(string password)
        {
            var passwordHasher = new DefaultPasswordHasher();
            var passwordHash = passwordHasher.HashPassword(password);

            passwordHash.Should()
                .NotBeNullOrEmpty()
                .And.HaveLength(48);
        }

        [Theory]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("")]
        public void HashPassword_IncorectPasswordHashing_ShouldThrowArgumentException(string password)
        {
            var passwordHasher = new DefaultPasswordHasher();
            Action hashPasswordAction = () => passwordHasher.HashPassword(password);
            hashPasswordAction.ShouldThrow<ArgumentException>("Wrong password");
        }
    }
}