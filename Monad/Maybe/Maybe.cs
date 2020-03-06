using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Functional.Monad.Maybe
{
    public class Maybe<T>
    {
        public class Binding : INotifyCompletion
        {
            private T Value { get; }

            public bool IsCompleted => true;

            public Binding(T val) => Value = val;

            public void OnCompleted(Action action) { }

            public T GetResult() => Value;
        }

        internal Maybe()
        {
            HasValue = false;
        }

        internal Maybe(T val)
        {
            Value = val;
            HasValue = true;
        }

        public static Maybe<T> Just(T x) =>
            new Maybe<T>(x);

        public static Maybe<T> Nothing() =>
            new Maybe<T>();

        public T Value { get; }
        public bool HasValue { get; }

        public Maybe<S> Map<S>(Func<T,S> f) => HasValue switch
        {
            false => Maybe<S>.Nothing(),
            true  => Maybe<S>.Just(f(Value))
        };

        internal static Maybe<T> Pure(T t) => new Maybe<T>(t);

        internal static Maybe<T> Join(Maybe<Maybe<T>> t) => t.Value;

        public Maybe<S> Bind<S>(Func<T,Maybe<S>> f) =>
            Map(f).MaybeJoin();

        public Binding GetAwaiter() {
            return new Binding(Value);
        }

        public static Maybe<T> Do(Func<Task<Maybe<T>>> f) =>
            Task.Run(f).GetAwaiter().GetResult();
    }
}
