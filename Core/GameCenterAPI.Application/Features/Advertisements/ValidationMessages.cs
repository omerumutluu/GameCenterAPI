namespace GameCenterAPI.Application.Features.Advertisements
{
    public class ValidationMessages
    {
        public static string TitleNotNullMessage = "Başlık boş geçilemez!";
        public static string TitleCanMinimum3Letter = "Başlık en az 3 karakterden oluşmalıdır!";
        public static string TitleCanMaximum100Letter = "Başlık en fazla 100 karakterden oluşmalıdır!";

        public static string DescriptionNotNullMessage = "Açıklama boş geçilemez!";
        public static string DescriptionCanMinimum3Letter = "Açıklama en az 3 karakterden oluşmalıdır!";
        public static string DescriptionCanMaximum400Letter = "Açıklama en fazla 400 karakterden oluşmalıdır!";
    
        public static string GameIdCanNotBeNull = "Oyun bilgisi boş geçilemez!";

        public static string UserIdCanNotBeNull = "Kullanıcı bilgisi boş geçilemez!";

        public static string ThumbnailCanNotBeNull = "Kapak resmi boş geçilemez!";

        public static string PriceCanNotBeNull = "Fiyat bilgisi boş geçilemez!";
        public static string PriceMustBeGreaterThan0 = "Fiyat bilgisi negatif bir değer alamaz!";

        public static string DeliveryHourCanNotBeNull = "Teslimat süresi bilgisi boş geçilemez!";
        public static string DeliveryHourMustBeGreaterThan0 = "Teslimat süresi bilgisi negatif bir değer alamaz!";

    }
}
