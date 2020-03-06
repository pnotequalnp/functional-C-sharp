using System;
using Functional.Monad.IO;
using Functional.Monad.Maybe;

namespace Functional.Prelude
{
    public static class Prelude
    {
        private static Unit unit { get; } = new Unit();

        public static void main(IO<Unit> x) => x.GetAwaiter().GetResult();

        public static IO<Unit> Print<T>(T x) =>
            IO<Unit>.Pure(() => {
                Console.WriteLine(x);
                return unit;
            });

        public static IO<string> GetLine() =>
            IO<string>.Pure(() => Console.ReadLine());

        public static Maybe<int> ParseInt(string s)
        {
            bool success = int.TryParse(s, out var t);
            if (success) return Maybe<int>.Just(t);
            return Maybe<int>.Nothing();
        }

        public static Func<R,T> Compose<R,S,T>(Func<S,T> f, Func<R,S> g) =>
            x => f(g(x));

        public static T FromMaybe<T>(T x, Maybe<T> y) =>
            y.HasValue ? y.Value : x;
    }
}
