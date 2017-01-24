using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.GUI
{
    public class PlatformControlManager : MonoBehaviour
    {
        public GameObject MobileControls;
        public GameObject EquipmentChanger;
        
        void Awake()
        {
            if (Application.isMobilePlatform)
            {
                MobileControls.SetActive(true);
                EquipmentChanger.GetComponent<Button>().enabled = true;
                EquipmentChanger.GetComponent<Image>().enabled = true;
            }
            else
            {
                MobileControls.SetActive(false);
                EquipmentChanger.GetComponent<Button>().enabled = false;
                EquipmentChanger.GetComponent<Image>().enabled = false;
            }
        }
    }
}
