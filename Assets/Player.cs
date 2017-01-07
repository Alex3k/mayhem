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
        gun = new Weapon(100, 0.1f, 1);
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
        var move = new Vector3(CnControls.CnInputManager.GetAxis("ShootHorizontal"), CnControls.CnInputManager.GetAxis("ShootVertical"), 0);

        if (move != Vector3.zero)
        {
            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;

            gun.Shoot(transform.position, Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles);
        }
    }

    void UpdateMovement()
    {
        var move = new Vector3(CnControls.CnInputManager.GetAxis("Horizontal"), CnControls.CnInputManager.GetAxis("Vertical"), 0);
        transform.position += move * MovementSpeed * Time.deltaTime;

        float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
