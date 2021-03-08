using System;

namespace Domain.Generic
{
    public struct Maybe
    {
        public static Maybe None => new Maybe();
    }

    public struct Maybe<T>
    {
        public static Maybe<T> None => new Maybe<T>();

        private readonly T value;
        private readonly bool isSome;

        public Maybe(T value)
        {
            this.value = value;

            isSome = true;
        }

        public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);

        public static implicit operator Maybe<T>(Maybe _) => new Maybe<T>();

        public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some) =>
            isSome ? some(value) : none();

        public void Match(Action none, Action<T> some)
        {
            if (isSome)
                some(value);
            else
                none();
        }
    }
}
