using System.Collections.Generic;
using System.Linq;

namespace Domain.Generic
{
    public static class ValueObjectConversions
    {
        public static IEnumerable<T> ToValues<T>(this IEnumerable<ValueObject<T>> items) => 
            items.Select(items => items.Value);
    }
}
