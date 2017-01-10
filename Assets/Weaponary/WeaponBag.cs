using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mayhem.Weaponary
{
    public class WeaponBag
    {
        public const int MAX_WEAPON_COUNT = 5;

        public BaseWeapon[] Weapons
        {
            get
            {
                return m_Weapons.ToArray();
            }
        }

        public delegate void WeaponsChangedEventHandler(object sender, EventArgs e);
        public event WeaponsChangedEventHandler Changed;

        private List<BaseWeapon> m_Weapons;

        private int m_SelectedWeaponIndex;

        public WeaponBag()
        {
            m_Weapons = new List<BaseWeapon>();
            m_Weapons.Add(new Handgun());
            m_SelectedWeaponIndex = 0;
        }

        public BaseWeapon GetCurrentSelectedWeapon()
        {
            return m_Weapons[m_SelectedWeaponIndex];
        }

        public void AddWeapon(BaseWeapon weapon)
        {
            if (m_Weapons.Count == MAX_WEAPON_COUNT)
            {
                return;
            }

            m_Weapons.Add(weapon);
            Changed(null, new EventArgs());
        }

        public void ChangeWeapon(int index)
        {
            m_SelectedWeaponIndex = index;
        }
    }

}
