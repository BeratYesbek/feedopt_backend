using Core.Entity.Abstracts;
using Entity.Concretes;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace Entity.Dtos.Filter
{
    public class AdvertFilterDto : IDto
    {
        public int UserId { get; set; } = default;

        public int[] AnimalSpeciesId { get; set; }

        public int[] AdvertCategoryId { get; set; }

        public int[] AnimalCategoryId { get; set; }

        public int[] ColorId { get; set; }

        public int[] AgeId { get; set; }

        public int Distance { get; set; } = 1000000;

        public Gender[] Gender { get; set; }

        public Status[] Status { get; set; } =  { Concretes.Status.Active };

    }
}
