using System.Collections.Generic;

namespace Directory
{
    public interface IDrugComponents : ICollection<IDrugComponent>
    {
        //todo change into composition rather than inheritance
        //todo pass Json and do serialisation / deserialisation here
    }
}