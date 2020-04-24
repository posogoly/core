namespace Directory
{
    public interface ILeaflet
    {
        string RawContent { get; set; }
        string SideEffects { get; set; }
        string Information { get; set; }
        string Posology { get; set; }
        string Description { get; set; }
    }
}