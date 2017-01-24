using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mayhem.GUI
{
    public class PlatformControlManager : MonoBehaviour
    {
        public GameObject MobileControls;
        
        void Awake()
        {
            if (Application.isMobilePlatform)
            {
                MobileControls.SetActive(true);
            }
            else
            {
                MobileControls.SetActive(false);
            }
        }
    }
}
