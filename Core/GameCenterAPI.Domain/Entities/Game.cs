using GameCenterAPI.Domain.Entities.Base;

namespace GameCenterAPI.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
