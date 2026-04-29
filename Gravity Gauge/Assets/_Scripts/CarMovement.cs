using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    public float gravityCD;
    public float health;
    public float minSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Road") return;
    }
}
