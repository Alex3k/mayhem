using System;
using System.Collections.Generic;

namespace Mayhem.Equipment
{
    public class EquipmentBag<T>
    {
        public const int MAX_OBJECT_COUNT = 3;

        public T[] Objects
        {
            get
            {
                return m_Equipment.ToArray();
            }
        }

        public delegate void EquipmentChangedEventHandler(object sender, EventArgs e);
        public event EquipmentChangedEventHandler Changed;

        private List<T> m_Equipment;

        private int m_SelectedObjectIndex;

        public EquipmentBag()
        {
            m_Equipment = new List<T>();
        
            m_SelectedObjectIndex = 0;
        }

        public T GetCurrentSelectedObject()
        {
            return m_Equipment[m_SelectedObjectIndex];
        }

        public void AddObject(T objectToAdd)
        {
            if (m_Equipment.Count == MAX_OBJECT_COUNT)
            {
                return;
            }

            m_Equipment.Add(objectToAdd);

            if (Changed != null)
            {
                Changed(null, new EventArgs());
            }
        }

        public bool ChangeObjectToIndex(int index)
        {
            if (index < 0 || index > m_Equipment.Count)
            {
                return false;
            }
            m_SelectedObjectIndex = index;
            return true;
        }
    }
}
