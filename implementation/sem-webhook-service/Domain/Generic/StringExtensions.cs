using System;

namespace Domain.Generic
{
    public static class StringExtensions
    {
        public static Guid ToGuid(this string input) => new Guid(input);
    }
}
