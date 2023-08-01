using GameCenterAPI.Domain.Entities.Base;
using GameCenterAPI.Domain.Identity;

namespace GameCenterAPI.Domain.Entities
{
    public class Advertisement : BaseEntity
    {
        public AppUser User { get; set; }
        public Game Game { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public List<string> Medias { get; set; } = new List<string>();
        public float Price { get; set; }
        public DateTime ExpiredAt { get; set; }
        public int DeliveryHour { get; set; }
    }
}