
namespace Entity.Concretes
{
    public enum Gender
    {
        Male = 0,
        Female = 1
    }

    public class Animal
    {
        public string AnimalName { get; set; }

        public int ColorId { get; set; }

        public int AgeId { get; set; }

        public Gender Gender { get; set; }
    }
}
