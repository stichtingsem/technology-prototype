using System;

namespace Domain.Generic
{
    public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
    {
        readonly T value;

        protected ValueObject(T value) => this.value = value;

        public bool Equals(ValueObject<T> other) => other.value.Equals(value);

        public override bool Equals(object obj) => Equals(obj as ValueObject<T>);

        public override int GetHashCode() => value.GetHashCode();

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right) => left.Equals(right);

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right) => !(left == right);
        
        public static implicit operator T(ValueObject<T> valueObject) => valueObject.value;
    }
}
