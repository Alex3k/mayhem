using UnityEngine;

public class Zombie : MonoBehaviour
{
    PhotonView m_PhotonView;
    private Vector3 m_Target;
    public float MovementSpeed;

    // Use this for initialization
    void Awake () {
        m_PhotonView = GetComponent<PhotonView>();
        getNewTarget();
    }

    // Update is called once per frame
    void FixedUpdate () {
        UpdateMovement();
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

    void getNewTarget()
    {
        int x = 8;
        int y = 5;
        m_Target = new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0);
    }
}
