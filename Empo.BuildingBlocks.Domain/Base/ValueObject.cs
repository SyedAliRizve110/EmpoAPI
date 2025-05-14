using System.Reflection;

namespace Empo.BuildingBlocks.Domain.Base;

public abstract class ValueObject : IEquatable<ValueObject>
{ 
    private List<PropertyInfo> _properties;
    private List<FieldInfo> _fields;

    public static bool operator ==(ValueObject obj1, ValueObject obj2)
    {
        if (object.Equals(obj1, null))
        {
            if (object.Equals(obj2 , null))
            {
                return true;
            }

            return false;
        }
        return obj1.Equals(obj2);
    }

    public static bool operator !=(ValueObject obj1, ValueObject obj2)
    {
        return !(obj1 == obj2);
    }

    public bool Equals(ValueObject obj)
    {
        return Equals(obj as object);
    }

    //public override bool Equals(object obj)
    //{
    //    if (obj == null || GetType() != obj.GetType())
    //    {
    //        return false;
    //    }

    //    return GetPropertis()
    //}
}
