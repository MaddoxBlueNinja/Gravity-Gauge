using UnityEngine;

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
}
