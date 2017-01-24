using System;
using System.Collections.Generic;

namespace Mayhem.Equipment
{
    public class EquipmentBag
    {
        public enum BagType
        {
            Toggleable,
            Single
        }

        public const int MAX_OBJECT_COUNT = 3;

        public EquipmentItem[] Contents
        {
            get
            {
                return m_Equipment.ToArray();
            }
        }

        public delegate void EquipmentAddedRemoveEventHandler(object sender);

        public delegate void EquipmentSelectedEventHandler(object sender, EquipmentItem newlySelectedEquipment);
        public delegate void EquipmentDeselectedEventHandler(object sender, EquipmentItem previouslySelectedEquipment);
        public delegate void EquipmentUsedEventHandler(object sender, EquipmentItem usedEquipment);
        public event EquipmentDeselectedEventHandler EquipmentDeselected;
        public event EquipmentSelectedEventHandler EquipmentSelected;
        public event EquipmentAddedRemoveEventHandler EquipmentAddedRemoved;

        /// <summary>
        /// This is used for one time use items such as the turret placer. This item does not stay selected.
        /// </summary>
        public event EquipmentUsedEventHandler EquipmentUsed;

        private List<EquipmentItem> m_Equipment;

        private int m_SelectedObjectIndex;

        private BagType m_BagType;

        public EquipmentBag(BagType bagType)
        {
            m_Equipment = new List<EquipmentItem>();

            m_SelectedObjectIndex = -1;
            m_BagType = bagType;
        }

        public EquipmentItem GetCurrentSelectedObject()
        {
            if (HasSelectedSomething())
            {
                return m_Equipment[m_SelectedObjectIndex];
            }
            else
            {
                return null;
            }
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
            m_SelectedObjectIndex = -1;
            m_Equipment.RemoveAt(itemToRemove);

            if (EquipmentAddedRemoved != null)
            {
                EquipmentAddedRemoved(null);
            }
        }

        public void RemoveObject(EquipmentItem item)
        {
            int itemIndex = m_Equipment.IndexOf(item);

            if (m_SelectedObjectIndex > itemIndex)
            {
                m_SelectedObjectIndex--;
            }

            m_Equipment.Remove(item);

            if (EquipmentAddedRemoved != null)
            {
                EquipmentAddedRemoved(null);
            }
        }

        public void AddObject(EquipmentItem objectToAdd)
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

            if (m_Equipment[index].GetUsageType() == UsageType.OneTime)
            {
                if (EquipmentUsed != null)
                {
                    EquipmentUsed(null, m_Equipment[index]);
                }
            }
            else
            {
                if (m_BagType == BagType.Toggleable)
                {
                    if (m_SelectedObjectIndex == index)
                    {
                        Deselect();
                    }
                    else if (HasSelectedSomething())
                    {
                        Deselect();

                        m_SelectedObjectIndex = index;

                        if (EquipmentSelected != null)
                        {
                            EquipmentSelected(null, m_Equipment[m_SelectedObjectIndex]);
                        }
                    }
                    else
                    {
                        m_SelectedObjectIndex = index;

                        if (EquipmentSelected != null)
                        {
                            EquipmentSelected(null, m_Equipment[m_SelectedObjectIndex]);
                        }
                    }
                }
                else
                {
                    m_SelectedObjectIndex = index;

                    if (EquipmentSelected != null)
                    {
                        EquipmentSelected(null, m_Equipment[m_SelectedObjectIndex]);
                    }
                }

            }
            return true;
        }
    }
}
