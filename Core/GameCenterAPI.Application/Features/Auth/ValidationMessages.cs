using System.Data;

namespace GameCenterAPI.Application.Features.Auth
{
    public class ValidationMessages
    {
        public static string UsernameCanNotBeNull = "Kullanıcı adı boş geçilemez!";
        public static string UsernameCanBeMinimum4Letter = "Kullanıcı adı minimum 4 karakter içermelidir!";
        public static string UsernameCanBeMaximum20Letter = "Kullanıcı adı maximum 20 karakter içermelidir!";

        public static string FirstNameCanNotBeNull = "İsim boş geçilemez!";
        public static string FirstNameCanBeMinimum3Letter = "İsim minimum 3 karakter içermelidir!";
        public static string FirstNameCanBeMaximum20Letter = "İsim maximum 20 karakter içermelidir!";

        public static string LastNameCanNotBeNull = "Soyad boş geçilemez!";
        public static string LastNameCanBeMinimum3Letter = "Soyad minimum 3 karakter içermelidir!";
        public static string LastNameCanBeMaximum20Letter = "Soyad maximum 20 karakter içermelidir!";

        public static string PhoneNumberCanNotBeNull = "Telefon numarası boş geçilemez!";
        public static string PhoneNumberMustBe11Digit = "Telefon numarası 11 karakter olmalıdır!";

        public static string EmailCanNotBeNull = "Email boş geçilemez!";
        public static string EmailMustBeValidEmailFormat = "Email geçerli bir mail formatında olmalıdır!";

        public static string PasswordCanNotBeNull = "Şifre boş geçilemez!";
        public static string PasswordCanBeMinimum8Letter = "Şifre minimum 8 karakter içermelidir!";
        public static string PasswordCanBeMaximum20Letter = "Şifre maximum 20 karakter içermelidir!";
        public static string PasswordMustContainsCapitalLetter = "Şifre en az bir tane büyük harf içermelidir!";
        public static string PasswordMustContainsLowerLetter = "Şifre en az bir tane küçük harf içermelidir!";
        public static string PasswordMustContainsDigit = "Şifre en az bir tane rakam içermelidir!";
    }
}

//DefaultError
//ConcurrencyFailure
//PasswordMismatch
//InvalidToken
//RecoveryCodeRedemptionFailed
//LoginAlreadyAssociated
//InvalidUserName(string userName)
//InvalidEmail(string email)
//DuplicateUserName(string userName)
//DuplicateEmail(string email)
//InvalidRoleName(string role)
//DuplicateRoleName(string role)
//UserAlreadyHasPassword
//UserLockoutNotEnabled
//UserAlreadyInRole(string role)
//UserNotInRole(string role)
//PasswordTooShort(int length)
//PasswordRequiresUniqueChars(int uniqueChars)
//PasswordRequiresNonAlphanumeric
//PasswordRequiresDigit
//PasswordRequiresLower
//PasswordRequiresUpper
