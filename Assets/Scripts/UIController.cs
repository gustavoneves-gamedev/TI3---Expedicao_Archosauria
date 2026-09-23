using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject interfaceMenu;
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject tabletMenu;

    void Start()
    {
        GameController.gameController.uiController = this;
        mainMenu.SetActive(true);
        interfaceMenu.SetActive(false);
    }

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

}
