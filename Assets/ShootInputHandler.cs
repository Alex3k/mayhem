using UnityEngine;
using System.Collections;

public class ShootInputHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CnControls.CnInputManager.RegisterVirtualAxis(new CnControls.VirtualAxis("ShootHorizontal"));
        CnControls.CnInputManager.RegisterVirtualAxis(new CnControls.VirtualAxis("ShootVertical"));

    }

    // Update is called once per frame
    void Update () {
	
	}
}
