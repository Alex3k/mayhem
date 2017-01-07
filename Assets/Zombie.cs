using UnityEngine;

public class Zombie : MonoBehaviour
{
    PhotonView m_PhotonView;

    // Use this for initialization
    void Awake () {
        m_PhotonView = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void FixedUpdate () {
        UpdateMovement();
	}

    void UpdateMovement()
    {
        transform.Rotate(new Vector3(0, 0, 1), 5);

    }
}
