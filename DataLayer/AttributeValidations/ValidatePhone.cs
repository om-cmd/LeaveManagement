using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLayer.AttributeValidations
{
    /// <summary>
    /// Validates phone numbers to ensure they meet specific criteria.
    /// </summary>
    public class ValidatePhone : ValidationAttribute
    {
        /// <inheritdoc />
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var phone = value as string;
            if (string.IsNullOrWhiteSpace(phone))
            {
                return new ValidationResult("Phone number cannot be null or empty.");
            }

            int minLength = 10;
            int maxLength = 12;
            if (phone.Length > maxLength)
            {
                return new ValidationResult("Phone number length should not exceed the maximum length.");
            }
            if (phone.Length < minLength)
            {
                return new ValidationResult("Phone number should be longer than the minimum length.");
            }
            var prefix = phone.Substring(0, 2);
            if (prefix != "98" && prefix != "97" && prefix != "96")
            {
                return new ValidationResult("Phone number should start with 97, 96, or 98.");
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Validates email addresses to ensure they are in a valid format.
    /// </summary>
    public class ValidateEmail : ValidationAttribute
    {
        /// <inheritdoc />
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var email = value as string;
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult("Email should not be empty.");
            }

            if (!IsValidEmail(email))
            {
                return new ValidationResult("Email is not in a valid format.");
            }

            return ValidationResult.Success;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                var regex = new Regex(pattern);
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Validates passwords to ensure they meet specific complexity requirements.
    /// </summary>
    public class ValidatePassword : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _minUniqueCharacters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePassword"/> class with optional parameters.
        /// </summary>
        /// <param name="minLength">The minimum length of the password.</param>
        /// <param name="minUniqueCharacters">The minimum number of unique characters required in the password.</param>
        public ValidatePassword(int minLength = 8, int minUniqueCharacters = 3)
        {
            _minLength = minLength;
            _minUniqueCharacters = minUniqueCharacters;
        }

        /// <inheritdoc />
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))
            {
                return new ValidationResult("Password cannot be empty.");
            }

            if (password.Length < _minLength)
            {
                return new ValidationResult($"Password must be at least {_minLength} characters long.");
            }

            if (password.Distinct().Count() < _minUniqueCharacters)
            {
                return new ValidationResult($"Password must contain at least {_minUniqueCharacters} unique characters.");
            }

            // Additional complexity checks
            if (!ContainsUppercaseLetter(password))
            {
                return new ValidationResult("Password must contain at least one uppercase letter.");
            }

            if (!ContainsLowercaseLetter(password))
            {
                return new ValidationResult("Password must contain at least one lowercase letter.");
            }

            if (!ContainsDigit(password))
            {
                return new ValidationResult("Password must contain at least one digit.");
            }

            if (IsCommonPassword(password))
            {
                return new ValidationResult("Password is too common or weak.");
            }

            return ValidationResult.Success;
        }

        private bool ContainsUppercaseLetter(string password)
        {
            return password.Any(char.IsUpper);
        }

        private bool ContainsLowercaseLetter(string password)
        {
            return password.Any(char.IsLower);
        }

        private bool ContainsDigit(string password)
        {
            return password.Any(char.IsDigit);
        }

        // Method to check against common or weak passwords 
        private bool IsCommonPassword(string password)
        {
            string[] commonPasswords = { "password", "123456", "qwerty", "abc123", "bokoChoda" };

            return commonPasswords.Contains(password.ToLower());
        }
    }
}
