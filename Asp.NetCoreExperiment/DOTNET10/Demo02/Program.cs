


public static class Extensions
{
    extension(IEnumerable<int> source)
    {
        public IEnumerable<int> WhereGreaterThan(int threshold)
            => source.Where(x => x > threshold);

    public bool IsEmpty
        => !source.Any();
}
