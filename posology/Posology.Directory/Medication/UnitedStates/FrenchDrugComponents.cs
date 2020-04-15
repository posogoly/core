using System.Collections;
using System.Collections.Generic;
using Posology.Core;

namespace Posology.Directory.Medication.UnitedStates
{
    public class USDrugComponents : IDrugComponents
    {
        public USDrugComponents()
        {
            _components = new List<USDrugComponent>();
        }
        private readonly List<USDrugComponent> _components;

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
            _components.Add((USDrugComponent)component);
        }

        public void Clear()
        {
            _components.Clear();
        }

        public bool Contains(IDrugComponent item)
        {
            return _components.Contains((USDrugComponent)item);
        }

        public void CopyTo(IDrugComponent[] array, int arrayIndex)
        {
            _components.CopyTo((USDrugComponent[])array, arrayIndex);
        }

        public bool Remove(IDrugComponent item)
        {
            return _components.Remove((USDrugComponent)item);
        }
    }
}