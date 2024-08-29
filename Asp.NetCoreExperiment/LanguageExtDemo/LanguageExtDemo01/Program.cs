using LanguageExt;
using LanguageExt.Common;

Option<int> x = Some(123);
Option<int> y = Option<int>.None;

Option<int> Some(int i) => i + 123;

Console.WriteLine();

Result<T> fff()
{
    return new Result<T>(new Exception("ddd"));
}