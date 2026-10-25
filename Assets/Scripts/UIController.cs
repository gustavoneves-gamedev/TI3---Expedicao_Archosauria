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
        FreeCursor(true);
    }

    #region Tutorial

    public void ShowTutorial(int x)
    {
        tutorialMenus[x].SetActive(true);

        //if (tutorialMenus[x].activeSelf)
        //{
        //    tutorialMenus[x].SetActive(false);

        //}
        //else
        //{
        //    tutorialMenus[x].SetActive(true);

        //}
    }

    public void CloseTutorial()
    {
        foreach (GameObject tutorial in tutorialMenus)
        {
            tutorial.SetActive(false);
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
        //GameController.gameController.player.canMove = false;
        ShowTutorial(0);
    }

    public void PauseGame()
    {
        GameController.gameController.PauseGame();
        tabletMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        GameController.gameController.ResumeGame();
        //GameController.gameController.player.canMove = true;
        tabletMenu.SetActive(false);
    }


    public void ShowControls()
    {

        controlMenu.SetActive(true);

    }

    public void ShowJumpMinigameControls()
    {
        jumpMinigameTutorial.SetActive(true);
    }

    public void FreeCursor(bool free)
    {
        if (!free)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (free)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
