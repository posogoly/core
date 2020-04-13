using System.Collections;
using System.Collections.Generic;

namespace Posology.Core
{
    public class FrenchDrugComponents : IDrugComponents
    {
        public FrenchDrugComponents()
        {
            _components = new List<FrenchDrugComponent>();
        }
        private readonly List<FrenchDrugComponent> _components;

        public int Count => _components.Count;

        public bool IsReadOnly => false;

        public IEnumerator<IDrugComponent> GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        public void Add(IDrugComponent component)
        {
            _components.Add((FrenchDrugComponent)component);
        }

        public void Clear()
        {
            _components.Clear();
        }

        public bool Contains(IDrugComponent item)
        {
            return _components.Contains((FrenchDrugComponent)item);
        }

        public void CopyTo(IDrugComponent[] array, int arrayIndex)
        {
            _components.CopyTo((FrenchDrugComponent[])array, arrayIndex);
        }

        public bool Remove(IDrugComponent item)
        {
            return _components.Remove((FrenchDrugComponent)item);
        }
    }
}