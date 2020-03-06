namespace Functional.Monad.IO
{
    static class IOExtensions
    {
        public static IO<T> IOPure<T>(this T t) => IO<T>.Pure(t);
        public static IO<T> IOJoin<T>(this IO<IO<T>> t) => IO<T>.Join(t);
    }
}
