
using Core.Entity.Abstracts;

namespace Entity.Dtos
{
    public class DashboardDto : IDto
    {
        public int AdvertQuantity { get; set; }
        public int ActiveAdvertQuantity { get; set; }
        public int UserQuantity { get; set; }
        public int PendingAdvertQuantity { get; set; }
    }
}
