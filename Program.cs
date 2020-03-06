using static Functional.Prelude.Prelude;
using Functional.Prelude;
using Functional.Monad.IO;
using Functional.Monad.Maybe;

namespace Functional
{
    class Program
    {
        static void Main(string[] args) => main(
            IO<Unit>.Do(async () => {
                IO<Unit> doNothing = Print("hello");
                string name = await GetName();
                await Greet(name);

                Maybe<int> response = await GetAge();

                return FromMaybe(
                    Print("That's not a valid number :("),
                    response.Map(i => Print($"You are {i} years!"))
                );
            })
        );

        static IO<string> GetName() =>
            IO<string>.Do(async () => {
                await Print("Enter your name:");
                return GetLine();
            });

        static IO<Unit> Greet(string name) =>
            Print($"Hello, {name}!");

        static IO<Maybe<int>> GetAge() =>
            IO<Maybe<int>>.Do(async () => {
                await Print("Enter your age:");
                string str = await GetLine();
                return IO<Maybe<int>>.Pure(ParseInt(str));
            });
    }
}
