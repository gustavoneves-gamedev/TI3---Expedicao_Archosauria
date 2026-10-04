using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    public Player player;
    public UIController uiController;

    public bool isPlaying;
    public bool isPaused;

    [Header("Game Phase")]
    public bool isInJumpMinigame;
    public bool shouldCameraFollow;

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

    public void BeginJumpMinigame()
    {
        player.jumpHeight *= 1.5f;
        player.isInJumpMinigame = true;
        shouldCameraFollow = true;
    }

    public void EndJumpMinigame()
    {
        player.jumpHeight /= 1.5f;
        player.isInJumpMinigame = false;
        shouldCameraFollow = false;
    }

}
