using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject interfaceMenu;
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject tabletMenu;
    [SerializeField] private GameObject jumpMinigameTutorial;

    [SerializeField] private GameObject[] tutorialMenus;
    [SerializeField] private GameObject clueChoiceMenu;
    private int tutorialCounter;

    void Start()
    {
        GameController.gameController.uiController = this;
        mainMenu.SetActive(true);
        interfaceMenu.SetActive(false);
    }

    #region Tutorial

    public void ShowTutorial(int x)
    {
        
        //tutorialCounter++;

        if (tutorialMenus[x].activeSelf)
        {
            tutorialMenus[x].SetActive(false);
        }
        else
        {
            tutorialMenus[x].SetActive(true);
        }
        
    }

    #endregion

    #region Investigation

    public void CloseTablet()
    {
        if (GameController.gameController.player.isInvestigating)
        {
            clueChoiceMenu.SetActive(true);
        }
    }

    //public void 

    #endregion


    // Update is called once per frame
    void Update()
    {

    }

    public void BeginGame()
    {
        mainMenu.SetActive(false);
        interfaceMenu.SetActive(true);
        GameController.gameController.BeginGame();
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

    public void ShowJumpMinigameControls()
    {
        jumpMinigameTutorial.SetActive(true);
    }
}
