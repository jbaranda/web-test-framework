namespace Framework.Interfaces
{
    public interface ILink
    {
        string Text { get; }
        string Href { get; }
        T ClickTo<T>();
    }
}
