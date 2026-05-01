using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject speedUpUI;
    public GameObject slowDownUI;
    public GameObject healthUI;
    public GameObject thrillUI;
    public GameObject gravUI;

    Color colorSpUpUI;
    float alphaSpUpUI = 0;

    Color colorSlDoUI;
    float alphaSlDoUI = 0;

    TMPro.TextMeshProUGUI healthText;

    TMPro.TextMeshProUGUI thrillText;
    Color colorThrillUI;
    float greenThrillUI = 0;

    TMPro.TextMeshProUGUI gravText;

    public float alphaChangeSpeed;

    private void Start()
    {
        colorSpUpUI = speedUpUI.GetComponent<Image>().color;
        colorSpUpUI.a = alphaSpUpUI;
        speedUpUI.GetComponent<Image>().color = colorSpUpUI;

        colorSlDoUI = slowDownUI.GetComponent<Image>().color;
        colorSlDoUI.a = alphaSlDoUI;
        slowDownUI.GetComponent<Image>().color = colorSlDoUI;

        healthText = healthUI.GetComponent<TMPro.TextMeshProUGUI>();

        thrillText = thrillUI.GetComponent<TMPro.TextMeshProUGUI>();
        colorThrillUI = thrillUI.GetComponent<TMPro.TextMeshProUGUI>().color;
        colorThrillUI.g = greenThrillUI;
        thrillUI.GetComponent<TMPro.TextMeshProUGUI>().color = colorThrillUI;

        gravText = gravUI.GetComponent<TMPro.TextMeshProUGUI>();
    }
    void FixedUpdate()
    {
        SpeedUpUpdate();
        SlowDownUpdate();
        HealthUpdate();
        ThrillUpdate();
        GravUpdate();
    }

    void SpeedUpUpdate()
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
        speedUpUI.GetComponent<Image>().color = colorSpUpUI;
    }

    void SlowDownUpdate()
    {
        colorSpUpUI.a = alphaSpUpUI;
        speedUpUI.GetComponent<Image>().color = colorSpUpUI;

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
        slowDownUI.GetComponent<Image>().color = colorSlDoUI;
    }

    void HealthUpdate()
    {
        healthText.text = "HP: ";

        for (int i = 0; i < CarManager.health; i++)
        {
            healthText.text += "♥";
        }
    }

    void ThrillUpdate()
    {
        thrillText.text = "Thrill: ";

        for (int i = 0; i < CarManager.thrill; i++)
        {
            thrillText.text += "|";
        }

        greenThrillUI = 1 - ((float)CarManager.thrill / 15);

        colorThrillUI.g = greenThrillUI;
        thrillUI.GetComponent<TMPro.TextMeshProUGUI>().color = colorThrillUI;
    }
    void GravUpdate()
    {
        if (CarManager.gravSwapTimer <= 0)
        {
            gravText.text = "READY";
            gravText.color = Color.cyan;
            //√
        }
        else
        {
            gravText.text = (CarManager.gravSwapTimer / 50).ToString("F2");
            gravText.color = Color.white;
        }

        gravText.text += " :Grav";
    }
}
