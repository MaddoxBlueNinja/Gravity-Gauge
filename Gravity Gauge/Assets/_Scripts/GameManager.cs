using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (CarManager.health <= 0 || CarManager.thrill <= 0)
        {
            Debug.Log("LOST");
        }
    }

    public static void StartWin()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
