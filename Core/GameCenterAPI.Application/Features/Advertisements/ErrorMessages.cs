namespace GameCenterAPI.Application.Features.Advertisements
{
    public static class ErrorMessages
    {
        public static string AdvetisementsNotFound = "İstekle eşleşen herhangi bir ilan bulunamadı.";
        public static string AdvetisementNotFoundById = "Bu id ile eşleşen herhangi bir ilan bulunamadı.";
        public static string UnknownErrorWhenAdvetisementAdded = "İlan eklenirken bilinmeyen bir hata ile karşılaşıldı.";
        public static string UnknownErrorWhenAdvetisementDeleted = "İlan silinirken bilinmeyen bir hata ile karşılaşıldı.";
        public static string ValidationFailed = "Eklenmeye çalışılan ilan bilgisinin bir/bazı/hepsi değeri/değerleri/* doğrulanamamıştır.";
        public static string UserCanNotFound = "Girilen kullanıcı ID'si ile eşleşen bir kullanıcı bulanamadı.";
        public static string GameCanNotFound = "Girilen oyun ID'si ile eşleşen bir oyun bulunamadı.";
    }
}
