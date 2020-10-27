using System;
using System.Security.Cryptography;

namespace TechnocomShared.Authentication
{
    public class PasswordManager
    {
        private const string AllowedLowerCaseChars = "abcdefghijklmnopqrstuvwzyz";
        private const string AllowedUpperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWZYZ";
        private const string AllowedNumbers = "0123456789";
        //private const string AllowedSpecialCharacters = "~`!@#$%^&*()_-+={}[]|:;,.?/";
        private const string AllowedSpecialCharacters = "!@#$-+&()_{}.";
        private const int MinLength = 8;
        //private const int MaxLength = 20;
        private const int MaxLength = 12;
        private readonly ISpecification<string> _compositeSpecification;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordManager"/> class.
        /// </summary>
        public PasswordManager()
        {
            var lengthSpecification =
                new PredicateSpecification<string>(s => s.Length >= MinLength && s.Length <= MaxLength);
            var lowerCaseSpecification =
                new PredicateSpecification<string>(s => s.IndexOfAny(AllowedLowerCaseChars.ToCharArray()) > -1);
            var upperCaseSpecification =
                new PredicateSpecification<string>(s => s.IndexOfAny(AllowedUpperCaseChars.ToCharArray()) > -1);
            var numberSpecification =
                new PredicateSpecification<string>(s => s.IndexOfAny(AllowedNumbers.ToCharArray()) > -1);
            var specialCharactersSpecification =
                new PredicateSpecification<string>(s => s.IndexOfAny(AllowedSpecialCharacters.ToCharArray()) > -1);
            _compositeSpecification = lengthSpecification && lowerCaseSpecification && upperCaseSpecification &&
                                      numberSpecification && specialCharactersSpecification;
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool ValidatePassword(string password)
        {
            return _compositeSpecification.IsSatisfiedBy(password);
        }

        public string GeneratePassword()
        {
            var charGroups = new[]
                                 {
                                     AllowedLowerCaseChars.ToCharArray(),
                                     AllowedUpperCaseChars.ToCharArray(),
                                     AllowedNumbers.ToCharArray(),
                                     AllowedSpecialCharacters.ToCharArray()
                                 };

            var charsLeftInGroup = new int[charGroups.Length];

            for (var i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            var leftGroupsOrder = new int[charGroups.Length];

            for (var i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            var randomBytes = new byte[4];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            var seed = (randomBytes[0] & 0x7f) << 24 |
                       randomBytes[1] << 16 |
                       randomBytes[2] << 8 |
                       randomBytes[3];

            var random = new Random(seed);

            var password = new char[random.Next(MinLength, MaxLength + 1)];

            var lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            for (var i = 0; i < password.Length; i++)
            {
                var nextLeftGroupsOrderIdx = lastLeftGroupsOrderIdx == 0 ? 0 : random.Next(0, lastLeftGroupsOrderIdx);
                var nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];
                var lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                var nextCharIdx = lastCharIdx == 0 ? 0 : random.Next(0, lastCharIdx + 1);
                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] = charGroups[nextGroupIdx].Length;
                else
                {
                    if (lastCharIdx != nextCharIdx)
                    {
                        var temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] = charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    charsLeftInGroup[nextGroupIdx]--;
                }

                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                else
                {
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        var temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] = leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    lastLeftGroupsOrderIdx--;
                }
            }
            return new string(password);
        }
    }
}