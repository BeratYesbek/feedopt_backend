using Core.Entity.Abstracts;

namespace Entity.Dtos
{
    public class FilterDto : IDto
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Type { get; set; }

        public string InputType { get; set; }

        public string FilterType { get; set; }

        public object Data { get; set; }

    }
}
