using Core.Entity.Abstracts;
using System;

namespace Core.Extensions
{
    public static class EntityMapExtension
    {
        public static IEntity Map(this IEntity entity, IDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var properties = dto.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.GetValue(dto) is not null and not "" and not 0)
                     entity.GetType().GetProperty(property.Name)?.SetValue(entity, property.GetValue(dto), null);
             
                
            }

            return entity;
        }
    }
}
