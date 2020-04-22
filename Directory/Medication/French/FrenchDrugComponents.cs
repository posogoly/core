using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Directory.Medication.French
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

        private bool Equals(FrenchDrugComponents other)
        {
            var isEqual = true;
            _components.ForEach(thisComponent =>
            {
                var otherComponent = other.FirstOrDefault(comp => comp.ComponentId == thisComponent.ComponentId);
                if (!thisComponent.Equals(otherComponent))
                {
                    isEqual = false;
                };
            });
            return isEqual;
            
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((FrenchDrugComponents) obj);
        }

        public override int GetHashCode()
        {
            return (_components != null ? _components.GetHashCode() : 0);
        }
    }
}