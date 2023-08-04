namespace GameCenterAPI.Application.Features.Auth
{
    public class ErrorMessages
    {
        public static string EmailOrUsernameIsExistError = "Bu kullanıcı adı veya email ile zaten bir kullanıcı bulunmaktadır.";
        public static string ValidationFailed = "Kayıt olmaya çalışan kullanıcı bilgisinin bir/bazı/tüm değeri/değerleri/değerleri doğrulanamamıştır.";
        public static string UnknownErrorWhenUserRegister = "Kullanıcı kayıt edilirken bilinmeyen bir hatayla karşılaşıldı.";
        public static string UnknownErrorWhenUserLogin = "Giriş işlemi sırasında bilinmeyen bir hatayla karşılaşıldı.";
        public static string EmailOrUsernameFalse = "Girdiğiniz kullanıcı adı veya email ile eşleşen bir kullanıcı bulunamadı.";
        public static string PasswordIsFalse = "Girdiğiniz şifre hatalı.";
        public static string DefaultError = "Bilinmeyen bir hata oluştu.";
        public static string ConcurrencyFailure = "İyimser eşzamanlılık hatası, nesne değiştirildi.";
        public static string InvalidToken = "Hatalı token girişi.";
        public static string RecoveryCodeRedemptionFailed = "Kurtarma kodu kullanımı başarısız oldu.";
        public static string PasswordMissMatch = "Şifreler eşleşmemektedir.";
        public static string LoginAlreadyAssociated = "Bu girişe sahip bir kullanıcı zaten var.";
        public static string InvalidUserName = "Geçersiz bir kullanıcı adı girildi.";
        public static string InvalidEmail = "Geçersiz bir email girildi.";
        public static string DuplicateUserName = "Bu kullanıcı adı zaten mevcut.";
        public static string DuplicateEmail = "Bu email zaten mevcut.";
        public static string InvalidRoleName = "Geçersiz bir rol ismi girildi.";
        public static string DuplicateRoleName = "Bu rol zaten mevcut.";
        public static string UserAlreadyHasPassword = "Kullanıcının zaten ayarlanmış bir şifresi mevcut.";
        public static string UserLockoutNotEnabled="Bu kullanıcı için kilitleme etkin değil.";
        public static string UserAlreadyInRole = "Kullanıcı zaten bu role sahip.";
        public static string UserNotInRole = "Kullanıcı bu role sahip değil.";
        public static string PasswordTooShort = "Parola daha uzun olmalıdır.";
        public static string PasswordRequiresUniqueChars = "Parola farklı karakterler içermelidir.";
        public static string PasswordRequiresNonAlphanumeric = "Parola en az bir alfasayısal olmayan karakter içermelidir.";
        public static string PasswordRequiresDigit = "Parola en az bir rakam içermelidir.";
        public static string PasswordRequiresLower = "Parola en az bir küçük harf içermelidir";
        public static string PasswordRequiresUpper = "Parola en az bir büyük harf içermelidir";
        public static string UserCanNotFoundByRefreshToken = "Bu refresh token' a sahip bir kullanıcı bulunmamaktadır.";
        public static string RefreshTokenExpired = "Refresh token kullanım süresi bitmiştir.";
        public static string UnknownErrorWhenRefreshTokenUpdate = "Refresh token güncellenirken bilinmeyen bir hatayla karşılaşıldı.";
        public static string UserCanNotFoundById = "Bu id ile eşleşen bir kullanıcı bulunamadı";
        public static string UnknownErrorWhenMailConfirm = "Mail doğrulama sırasında bilinmeyen bir hata ile karşılaşıldı.";
        public static string InvalidEmailConfirmToken = "Hatalı email token.";
    }
}
