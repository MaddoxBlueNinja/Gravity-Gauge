using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject SpeedUpUI;
    UnityEngine.Color colorSpUpUI;
    public float alphaSpUpUI;

    public GameObject SlowDownUI;
    UnityEngine.Color colorSlDoUI;
    public float alphaSlDoUI;

    public float alphaChangeSpeed;

    private void Start()
    {
        colorSpUpUI = SpeedUpUI.GetComponent<Image>().color;
        colorSpUpUI.a = 100 + alphaSpUpUI;
        SpeedUpUI.GetComponent<Image>().color = colorSpUpUI;

        colorSlDoUI = SlowDownUI.GetComponent<Image>().color;
        colorSlDoUI.a = 100 + alphaSlDoUI;
        SlowDownUI.GetComponent<Image>().color = colorSlDoUI;
    }
    void FixedUpdate()
    {
        UpdateSpeedUp();
        UpdateSlowDown();
    }

    void UpdateSpeedUp()
    {
        if (CarManager.accelZAim > 0)
        {
            if (alphaSpUpUI <= 1)
            {
                alphaSpUpUI += alphaChangeSpeed;
            }
        }
        else if (alphaSpUpUI > 0)
        {
            alphaSpUpUI -= alphaChangeSpeed * 2;
        }
        else
        {
            alphaSpUpUI = 0;
        }

        colorSpUpUI.a = alphaSpUpUI;
        SpeedUpUI.GetComponent<Image>().color = colorSpUpUI;
    }

    void UpdateSlowDown()
    {
        colorSpUpUI.a = alphaSpUpUI;
        SpeedUpUI.GetComponent<Image>().color = colorSpUpUI;

        if (CarManager.accelZAim < 0)
        {
            if (alphaSlDoUI <= 1)
            {
                alphaSlDoUI += alphaChangeSpeed;
            }
        }
        else if (alphaSlDoUI > 0)
        {
            alphaSlDoUI -= alphaChangeSpeed * 2;
        }
        else
        {
            alphaSlDoUI = 0;
        }

        colorSlDoUI.a = alphaSlDoUI;
        SlowDownUI.GetComponent<Image>().color = colorSlDoUI;
    }
}
