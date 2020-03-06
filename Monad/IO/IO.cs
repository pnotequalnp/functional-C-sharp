using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Functional.Monad.IO
{
    public class IO<T>
    {
        public class Binding : INotifyCompletion
        {
            private T Value { get; }

            public bool IsCompleted => true;

            public Binding(T val) => Value = val;

            public void OnCompleted(Action action) { }

            public T GetResult() => Value;
        }

        private IO(Func<T> val) => Value = val;

        private Func<T> Value { get; }

        public IO<S> Map<S>(Func<T,S> f) =>
            new IO<S>(() => f(Value()));

        public static IO<T> Pure(T t) => new IO<T>(() => t);

        public static IO<T> Pure(Func<T> f) => new IO<T>(f);

        internal static IO<T> Join(IO<IO<T>> t) =>
            new IO<T>(() => t.Value().Value());

        public IO<S> Bind<S>(Func<T,IO<S>> f) => Map(f).IOJoin();

        public Binding GetAwaiter() {
            return new Binding(Value());
        }

        public static IO<T> Do(Func<Task<IO<T>>> f) =>
            IO<IO<T>>.Pure(() => Task.Run(f).Result).IOJoin();
    }
}
