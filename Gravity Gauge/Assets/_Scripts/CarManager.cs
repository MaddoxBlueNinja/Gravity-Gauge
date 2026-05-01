using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarManager : MonoBehaviour
{
    public static GameObject carGraphics;

    public float speedZ;
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
    public static float gravSwapTimer = 0;

    public static float health = 5;

    public static int thrill = 1;
    public float minSpeedForThrill;
    public float minThrillIncrease;

    bool onLeftWall = false;
    bool onRightWall = false;

    bool inHitstun = false;
    public float hitStunCD;
    float hitStunTimer = 0;
    public float stunFlashCD;
    float stunFlashTimer = 0;
    public float recoilSpeed;

    void Start()
    {
        carGraphics = GameObject.Find("Rollcage");
    }

    void Update()
    {
        if (GameManager.gameIsEnd) return;

        AccelInputX();

        if (inHitstun) return;

        thrill = Mathf.CeilToInt((speedZ - minSpeedForThrill) * 5);

        AccelInputZ();
        GravInput();
    }

    void FixedUpdate()
    {
        if (GameManager.gameIsEnd) return;
        minSpeedForThrill += minThrillIncrease;
        if (gravSwapTimer > 0) gravSwapTimer--;
        if (hitStunTimer > 0) hitStunTimer--;

        SpeedUpdateX();

        if (inHitstun)
        {
            Recoil();
            return;
        }

        SpeedUpdateZ();
        GravityUpdate();
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

    void AccelInputZ()
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
    }

    void AccelInputX()
    {
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

    void SpeedUpdateZ()
    {
        if (accelZAim != 0)
        {
            speedZ += accelZ * accelZAim;
        }
        else
        {
            speedZ *= 1 - decelZ;
        }

        Vector3 pos = this.transform.position;
        pos.z += speedZ;
        this.transform.position = pos;
    }

    void SpeedUpdateX()
    {
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
        pos.x += speedX;
        this.transform.position = pos;
    }

    void GravityUpdate()
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(0, gravAmount * gravAim, 0);
    }

    void Recoil()
    {
        if (hitStunTimer <= 0)
        {
            inHitstun = false;
            carGraphics.SetActive(true);
            return;
        }
        else
        {
            Vector3 pos = this.transform.position;
            pos.z -= recoilSpeed;
            this.transform.position = pos;
        }

        if (stunFlashTimer <= 0)
        {
            carGraphics.SetActive(!carGraphics.activeInHierarchy);

            stunFlashTimer = stunFlashCD;
        }
        else
        {
            stunFlashTimer--;
        }
    }

    public void RestartCar()
    {
        Vector3 pos = this.transform.position;
        pos = Vector3.zero;
        pos.y += 1;
        this.transform.position = pos;

        onLeftWall = false;
        onRightWall = false;

        inHitstun = false;

        speedZ = 2;

        health = 3;

        thrill = 1;

        gravAim = -1;

        gravSwapTimer = 0;

        hitStunTimer = 0;
        stunFlashTimer = 0;

        minSpeedForThrill = 0.5f;
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

        if (collision.gameObject.tag == "Goal")
        {
            GameManager.StartWin();
        }

        if (collision.gameObject.tag == "Lava")
        {
            health = 0;
        }

        // If Collision is NOT a Wall or Road or Lava.
        if (!inHitstun)
        {
            health--;
            gravSwapTimer = 0;

            if (health > 0)
            {
                inHitstun = true;
                hitStunTimer = hitStunCD;
            }
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
