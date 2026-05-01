using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    public float speedZ;
    public float accelZ;
    public float decelZ;
    int accelZAim;

    float speedX;
    public float accelX;
    public float decelX;
    int accelXAim;

    public float gravityCD;
    public float health;
    public float minSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        AccelInput();
    }

    void FixedUpdate()
    {
        SpeedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Road") return;
        // Put lose conditions here.
    }

    void AccelInput()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            accelZAim = 1;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            accelZAim = -1;
        }
        else
        {
            accelZAim = 0;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            accelXAim = 1;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            accelXAim = -1;
        }
        else
        {
            accelXAim = 0;
        }
    }

    void SpeedUpdate()
    {
        if (accelZAim != 0)
        {
            speedZ += accelZ * accelZAim;
        }
        else
        {
            speedZ *= 1 - decelZ;
        }

        if (accelXAim != 0)
        {
            if (Mathf.Sign(accelXAim) != Mathf.Sign(speedX))
            {
                speedX *= 1 - decelX;
            }

            speedX += accelX * accelXAim;
        }
        else
        {
            speedX *= 1 - decelX;
        }

        Vector3 pos = this.transform.position;
        pos.z += speedZ;
        pos.x += speedX;
        this.transform.position = pos;
    }
}
