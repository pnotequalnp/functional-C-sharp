namespace Functional.Monad.Maybe
{
    static class MaybeExtensions
    {
        public static Maybe<T> MaybePure<T>(this T t) => Maybe<T>.Pure(t);
        public static Maybe<T> MaybeJoin<T>(this Maybe<Maybe<T>> t) => Maybe<T>.Join(t);
    }
}
