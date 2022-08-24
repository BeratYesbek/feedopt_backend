using Core.Entity.Abstracts;
using Entity.concretes;

namespace Entity.Dtos;

public class AnimalSpeciesDto : IDto
{
    public int Id { get; set; }
    public string Kind { get; set; }
    public AnimalCategory AnimalCategory { get; set; }
}