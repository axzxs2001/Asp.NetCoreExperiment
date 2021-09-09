
//var cc = StringComparer.Create(System.Globalization.CultureInfo.CurrentCulture, true);
//Console.WriteLine(cc.Compare("a", "A"));

var oldList = new SortedList<string, int>();
for (var i = 1; i < 128; i++)
{
    if (!oldList.ContainsKey(((char)i).ToString()))
    {
        oldList.Add(((char)i).ToString(), i);
    }
}
var sn = 1;
foreach (var item in oldList)
{
    Console.WriteLine(item.Value);
    //Console.WriteLine($"序号：{sn++}  {item.Key} ascall值为  {item.Value}");
}

return;

Console.WriteLine("-----------按ASCII排序-----------");
var chars = new char[] { 'A', '[', ']', 'a' };
foreach (var c in chars)
{
    Console.WriteLine($"{c}：{(int)c}");
}
//Console.WriteLine("-----------排序集合的排序-----------");
//var list = new SortedList<string, int>();
//list.Add("a", 97);
//list.Add("A", 65);
//list.Add("[", 91);
//list.Add("]", 93);
//foreach (var item in list)
//{
//    Console.WriteLine($"{item.Key}：{item.Value}");
//}
//return;
Console.WriteLine("-----------新排序集合的排序-----------");
var newList = new SortedList<string, int>(new ASCALLComparer());
newList.Add("a", 97);
newList.Add("A", 65);
newList.Add("[", 91);
newList.Add("]", 93);
foreach (var item in newList)
{
    Console.WriteLine($"{item.Key}：{item.Value}");
}




public class ASCALLComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        if (x == null || y == null)
        {
            throw new Exception("x or y is null");
        }
        if (x?.Length != y?.Length)
        {
            if (x?.Length < y?.Length)
            {
                for (var i = 0; i < x?.Length; i++)
                {
                    if ((int)x[i] > (int)y[i])
                    {
                        return 1;
                    }
                    else if ((int)x[i] < (int)y[i])
                    {
                        return -1;
                    }
                }
                return -1;
            }
            else
            {
                for (var i = 0; i < y?.Length; i++)
                {
                    if ((int)x[i] > (int)y[i])
                    {
                        return 1;
                    }
                    else if ((int)x[i] < (int)y[i])
                    {
                        return -1;
                    }
                }
                return 1;
            }
        }
        else
        {
            for (var i = 0; i < x?.Length; i++)
            {
                if ((int)x[i] > (int)y[i])
                {
                    return 1;
                }
                else if ((int)x[i] < (int)y[i])
                {
                    return -1;
                }
            }
            return 0;
        }
    }
}