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

        public delegate void EquipmentAddedRemoveEventHandler(object sender);

        public delegate void EquipmentSelectedEventHandler(object sender, T newlySelectedEquipment);
        public delegate void EquipmentDeselectedEventHandler(object sender, T previouslySelectedEquipment);
        public event EquipmentDeselectedEventHandler EquipmentDeselected;
        public event EquipmentSelectedEventHandler EquipmentSelected;
        public event EquipmentAddedRemoveEventHandler EquipmentAddedRemoved;

        private List<T> m_Equipment;

        private int m_SelectedObjectIndex;

        public EquipmentBag()
        {
            m_Equipment = new List<T>();

            m_SelectedObjectIndex = -1;
        }

        public T GetCurrentSelectedObject()
        {
            return m_Equipment[m_SelectedObjectIndex];
        }

        public bool HasSelectedSomething()
        {
            if (m_SelectedObjectIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Deselect()
        {
            int previousSelection = m_SelectedObjectIndex;
            m_SelectedObjectIndex = -1;

            if (EquipmentDeselected != null)
            {
                EquipmentDeselected(null, m_Equipment[previousSelection]);
            }
        }

        public void RemoveCurrentObject()
        {
            int itemToRemove = m_SelectedObjectIndex;
            Deselect();
            m_Equipment.RemoveAt(itemToRemove);

            if (EquipmentAddedRemoved != null)
            {
                EquipmentAddedRemoved(null);
            }
        }

        public void AddObject(T objectToAdd)
        {
            if (m_Equipment.Count == MAX_OBJECT_COUNT)
            {
                return;
            }

            m_Equipment.Add(objectToAdd);
            if (EquipmentAddedRemoved != null)
            {
                EquipmentAddedRemoved(null);
            }
        }

        public bool ChangeObjectToIndex(int index)
        { 
            if (index < 0 || index > m_Equipment.Count)
            {
                return false;
            }
            m_SelectedObjectIndex = index;

            if (EquipmentSelected != null)
            {
                EquipmentSelected(null, m_Equipment[m_SelectedObjectIndex]);
            }

            return true;
        }
    }
}
