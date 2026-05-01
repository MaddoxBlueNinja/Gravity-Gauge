using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject Player;

    public static bool gameIsEnd = false;

    private float gameTimer = 0;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        if (gameTimer < 0.5f) return;

        if (CarManager.health <= 0 || CarManager.thrill <= 0)
        {
            StartLose();
        }
    }

    void FixedUpdate()
    {
        gameTimer += 0.02f;
    }

    public static void StartWin()
    {
        EndGame();

        UIManager.winUI.SetActive(true);
        UIManager.advanceUI.SetActive(true);
        
    }

    public static void StartLose()
    {
        EndGame();
        UIManager.loseUI.SetActive(true);
        UIManager.restartUI.SetActive(true);
    }

    static void EndGame()
    {
        gameIsEnd = true;

        CarManager.carGraphics.SetActive(false);

        UIManager.speedUpUI.SetActive(false);
        UIManager.slowDownUI.SetActive(false);
        UIManager.healthUI.SetActive(false);
        UIManager.thrillUI.SetActive(false);
        UIManager.gravUI.SetActive(false);
    }

    public void AdvanceScene()
    {
        resetValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartScene()
    {
        resetValues();
    }

    void resetValues()
    {
        gameIsEnd = false;

        CarManager.carGraphics.SetActive(true);

        UIManager.speedUpUI.SetActive(true);
        UIManager.slowDownUI.SetActive(true);
        UIManager.healthUI.SetActive(true);
        UIManager.thrillUI.SetActive(true);
        UIManager.gravUI.SetActive(true);

        UIManager.winUI.SetActive(false);
        UIManager.advanceUI.SetActive(false);
        UIManager.loseUI.SetActive(false);
        UIManager.restartUI.SetActive(false);

        CarManager pScript = Player.GetComponent<CarManager>();
        pScript.RestartCar();

        gameTimer = 0;
    }
}
