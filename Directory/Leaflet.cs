namespace Directory
{
    public class Leaflet:ILeaflet
    {
        
        public Leaflet(string pageContents)
        {
            //RawContent = pageContents;
        }
        public string RawContent { get; }
        public string SideEffects { get; set; }
        public string Information { get; set; }
        public string Posology { get; set; }
        public string Description { get; set; }
    }
}