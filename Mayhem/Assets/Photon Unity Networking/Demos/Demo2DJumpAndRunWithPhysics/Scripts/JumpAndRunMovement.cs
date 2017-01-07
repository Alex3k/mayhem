using UnityEngine;
using System.Collections;

public class JumpAndRunMovement : MonoBehaviour
{
    public float MovementSpeed = 3f;
    public float RotateSpeed = 5f;
    PhotonView m_PhotonView;
    

    void Awake()
    {
      
        m_PhotonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if( m_PhotonView.isMine == false )
        {
            return;
        }

        UpdateMovement();
    }

    void UpdateMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.right * MovementSpeed * Time.deltaTime;
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
