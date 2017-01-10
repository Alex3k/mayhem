using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.GUI
{
    public class WeaponContainer : MonoBehaviour
    {
        public GameObject WeaponIconPrefab;
        private Weaponary.BaseWeapon[] m_AvailableWeapons;

        private const int MAX_WEAPON_COUNT = 5;
        private int m_SelectedWeaponIndex = 1;
 

        private float m_SwipeZoneStartY = Screen.height / 5;
        private float m_SwipeZoneStartX = Screen.width / 2;
        private RectTransform m_SwipeZone;

        public string WeaponChildName = "WeaponIcon";

        void Start()
        {
            m_SwipeZone = transform.FindChild("SwipeZone").GetComponent<RectTransform>();
            m_AvailableWeapons = new Weaponary.BaseWeapon[] {
                new Weaponary.Handgun(),
            };

            for(int i = 0; i < MAX_WEAPON_COUNT; i++)
            {
                GameObject weaponTile = Instantiate(WeaponIconPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                weaponTile.transform.SetParent(GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform);
            }
        }

        Vector2 startPos;
        float minSwipeDist = 10f;
        bool isSwiping = false;

        void handleMobileTouches()
        {
            foreach (var touch in UnityEngine.Input.touches)
            {

                if (RectTransformUtility.RectangleContainsScreenPoint(m_SwipeZone, touch.position) == false)
                {
                    continue;
                }

                if (touch.phase == TouchPhase.Began)
                {
                    startPos = touch.position;
                    isSwiping = true;
                }
                else if (touch.phase == TouchPhase.Ended && isSwiping && Mathf.Abs(touch.position.y - startPos.y) > 30)
                {
                    var swipeDirection = Mathf.Sign(touch.position.y - startPos.y);

                    GetComponent<Text>().text = swipeDirection.ToString();

                    if (swipeDirection > 0)
                    {
                        getNextWeapon();
                    }
                    else if (swipeDirection < 0)
                    {
                        getPreviousWeapon();
                    }

                    isSwiping = false;

                }
            }
        }

        void Update()
        {
//#if MOBILE_INPUT
//            handleMobileTouches();
//#endif

//#if !MOBILE_INPUT
//            for (int i = (int)KeyCode.Alpha1; i < (int)KeyCode.Alpha1 + m_AvailableWeapons.Length; i++)
//            {
//                if (UnityEngine.Input.GetKeyDown((KeyCode)i))
//                {
//                    selectWeapon(i - (int)KeyCode.Alpha1 + 1); // + 1 Accounts for indexs starting at index of 1
//                }
//            }
//#endif

        }

        private void selectWeapon(int newlySelectedWeapon)
        {
            if (newlySelectedWeapon <= MAX_WEAPON_COUNT && newlySelectedWeapon > 0)
            {

                transform.FindChild(WeaponChildName + m_SelectedWeaponIndex).GetComponent<Image>().color = Color.white;
                m_SelectedWeaponIndex = newlySelectedWeapon;

                transform.FindChild(WeaponChildName + newlySelectedWeapon).GetComponent<Image>().color = Color.red;
            }
        }

        private void getNextWeapon()
        {
            selectWeapon(m_SelectedWeaponIndex + 1);
        }

        private void getPreviousWeapon()
        {
            selectWeapon(m_SelectedWeaponIndex - 1);
        }
    }
}
