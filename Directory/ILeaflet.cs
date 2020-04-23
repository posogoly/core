namespace Directory
{
    public interface ILeaflet
    {
        string RawContent { get; }
        string SideEffects { get; set; }
    }
}