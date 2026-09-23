using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    public Player player;
    public UIController uiController;

    public bool isPlaying;
    public bool isPaused;

    private void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void BeginGame()
    {
        isPlaying = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }


}
