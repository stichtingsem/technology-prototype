using System;

namespace Domain.Generic
{
    public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
    {
        public T Value { get; }

        protected ValueObject(T value) => Value = value;

        public bool Equals(ValueObject<T> other) => other.Value.Equals(Value);

        public override bool Equals(object obj) => Equals(obj as ValueObject<T>);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right) => left.Equals(right);

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right) => !(left == right);

        public override string ToString() => Value.ToString();

        public static implicit operator T(ValueObject<T> valueObject) => valueObject.Value;
    }
}
