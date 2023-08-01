namespace GameCenterAPI.Application.Features.Games
{
    public static class ErrorMessages
    {
        public static string GamesNotFound = "İstekle eşleşen herhangi bir oyun bulunamadı.";
        public static string GamesNotFoundById = "Bu id ile eşleşen herhangi bir oyun bulunamadı.";
        public static string GameNameAlreadyExist = "Bu isim ile zaten bir oyun bulunmaktadır.";
        public static string UnknownErrorWhenGameAdded = "Oyun eklenirken bilinmeyen bir hata ile karşılaşıldı.";
        public static string UnknownErrorWhenGameDeleted = "Oyun silinirken bilinmeyen bir hata ile karşılaşıldı.";
        public static string UpdatedGameNameCanNotBeSameWithOld = "Güncellenen oyun adı eskisi ile aynı olamaz.";
    }
}
