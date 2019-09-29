namespace JsonFlier.UserControls.Logs
{
    public interface IFileControl
    {
        string FileName { get; }

        string FilePath { get; }

        bool IsFocused { get; }
    }
}