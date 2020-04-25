namespace Directory
{
    public class Leaflet:ILeaflet
    {
        
        public Leaflet(string pageContents)
        {
            RawContent = pageContents;
        }
        public string SideEffects { get; set; }
        public string Information { get; set; }
        public string Posology { get; set; }
        public string Description { get; set; }
        public string RawContent { get; set; }

        protected bool Equals(Leaflet other)
        {
            return string.Equals(RawContent, other.RawContent) && 
                   string.Equals(SideEffects, other.SideEffects) && 
                   string.Equals(Information, other.Information) && 
                   string.Equals(Posology, other.Posology) && 
                   string.Equals(Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Leaflet) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (RawContent != null ? RawContent.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SideEffects != null ? SideEffects.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Information != null ? Information.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Posology != null ? Posology.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}