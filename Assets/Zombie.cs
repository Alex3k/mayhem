using UnityEngine;

public class Zombie : MonoBehaviour
{
    PhotonView m_PhotonView;
    private Vector3 m_Target;
    public float MovementSpeed;
    private Rigidbody2D m_MyRigidBody;

    // Use this for initialization
    void Awake () {
        if (PhotonNetwork.isMasterClient)
        {
            m_PhotonView = GetComponent<PhotonView>();

            getNewTarget();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (PhotonNetwork.isMasterClient)
        {
            UpdateMovement();
        }
	}

    void UpdateMovement()
    {
        Vector3 moveDirection = gameObject.transform.position - m_Target;
        if (moveDirection != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_Target, MovementSpeed * Time.deltaTime);

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            angle += 180;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            getNewTarget();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (collision.transform.tag == "Player")
            {
                m_Target = collision.transform.position;
            }
        }
    }

    void getNewTarget()
    {
        float mapWidth = GameObject.Find("Map").GetComponent<Map>().Size.x;
        float mapHeight = GameObject.Find("Map").GetComponent<Map>().Size.y;

        m_Target = new Vector3(Random.Range(0, mapWidth), Random.Range(0, mapHeight), 0);
    }
}
