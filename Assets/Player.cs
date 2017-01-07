using Assets;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float MovementSpeed = 3f;
    public float RotateSpeed = 5f;

    PhotonView m_PhotonView;
    private Weapon gun;

    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
        gun = new Weapon(5, 0.1f, 1);
    }

    void FixedUpdate()
    {
        if (m_PhotonView.isMine == false)
        {
            return;
        }

        UpdateMovement();
        handleWeaponary();
    }

    void handleWeaponary()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            gun.Shoot(transform.position, transform.eulerAngles);
        }
    }

    void UpdateMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += ((transform.right * MovementSpeed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * RotateSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * -RotateSpeed);
        }
    }
}
