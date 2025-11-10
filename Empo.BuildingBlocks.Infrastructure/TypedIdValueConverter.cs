using Empo.BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Empo.BuildingBlocks.Infrastructure;

public class TypedIdValueConverter<TTypedIdValue> : ValueConverter<TTypedIdValue, long>
    where TTypedIdValue : TypedIdValueBase
{
    public TypedIdValueConverter(ConverterMappingHints mappingHints = null)
        : base(id => id.Value, value => Create(value), mappingHints)
    {
    }

    private static TTypedIdValue Create(long id) => Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue;
}