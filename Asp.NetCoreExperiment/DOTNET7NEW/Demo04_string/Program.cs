


int X = 2;
int Y = 3;

var pointMessage = $"""
                    The point "{X}, {Y}" is Math.Sqrt(X * X + Y * Y)={Math.Sqrt(X * X + Y * Y)} from the origin
                    """;

Console.WriteLine(pointMessage);


Console.WriteLine($"|{"Left",-7}|{"Right",7}|");

const int FieldWidthRightAligned = 20;
Console.WriteLine($"{Math.PI,FieldWidthRightAligned} - default formatting of the pi number");
Console.WriteLine($"{Math.PI,FieldWidthRightAligned:F3} - display only three decimal digits of the pi number");


class ABC
{
    public void F()
    {
        var i = 10;
        if(i>20)
        {
            Console.WriteLine("abcd");
        }

    }
}