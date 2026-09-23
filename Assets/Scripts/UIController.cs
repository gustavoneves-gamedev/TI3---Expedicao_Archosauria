using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject tabletMenu;

    void Start()
    {
        GameController.gameController.uiController = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginGame()
    {
        //mainMenu.SetActive(false);
        GameController.gameController.BeginGame();
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        GameController.gameController.PauseGame();
        tabletMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        GameController.gameController.ResumeGame();
        tabletMenu.SetActive(false);
    }


    public void ShowControls(bool isMoving = true)
    {

        if (isMoving && controlMenu.activeSelf)
        {
            controlMenu.SetActive(false);

        }
        else if (!isMoving && !controlMenu.activeSelf)
        {
            controlMenu.SetActive(true);
        }
    }
    public void ToTheScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

}
