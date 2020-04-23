namespace Directory
{
    public class Leaflet:ILeaflet
    {
        
        public Leaflet(string pageContents)
        {
            RawContent = pageContents;
        }
        public string RawContent { get; }
        public string SideEffects { get; set; }
    }
}