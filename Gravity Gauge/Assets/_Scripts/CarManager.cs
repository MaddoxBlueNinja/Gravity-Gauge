using UnityEngine;
using UnityEngine.InputSystem;

public class CarManager : MonoBehaviour
{
    public static float speedZ;
    public float accelZ;
    public float decelZ;
    public static int accelZAim;

    float speedX;
    public float accelX;
    public float decelX;
    int accelXAim;

    public float gravAmount;
    public float gravJumpForce;
    int gravAim = -1;
    public float gravSwapCD;
    public static float gravSwapTimer;

    public static float health;
    public static float minSpeed;

    bool onLeftWall = false;
    bool onRightWall = false;
    void Start()
    {
        gravSwapTimer = 0;
    }

    void Update()
    {
        AccelInput();
        GravInput();
    }

    void FixedUpdate()
    {
        SpeedUpdate();
        GravityUpdate();

        if (gravSwapTimer > 0) gravSwapTimer--;
    }

    void GravInput()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && gravSwapTimer <= 0)
        {
            gravSwapTimer = gravSwapCD;
            gravAim *= -1;

            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
            rb.linearVelocity = new Vector3(0, gravJumpForce * gravAim, 0);
        }
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

        if (Keyboard.current.dKey.isPressed && !onRightWall)
        {
            accelXAim = 1;
        }
        else if (Keyboard.current.aKey.isPressed && !onLeftWall)
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

    void GravityUpdate()
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(0, gravAmount * gravAim, 0);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Road") return;

        if (collision.gameObject.tag == "Wall Right")
        {
            speedX = 0;
            accelXAim = 0;
            onRightWall = true;
            return;
        }
        else if (collision.gameObject.tag == "Wall Left")
        {
            speedX = 0;
            accelXAim = 0;
            onLeftWall = true;
            return;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Road") return;

        if (collision.gameObject.tag == "Wall Right")
        {
            onRightWall = false;
            return;
        }
        else if (collision.gameObject.tag == "Wall Left")
        {
            onLeftWall = false;
            return;
        }
    }
}
