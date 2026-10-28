using UnityEngine;

public class Tablet : MonoBehaviour
{
    private PlayerRB player;

    [SerializeField] private GameObject tablet;
    [SerializeField] private GameObject[] mainScreenMenus;
    //[SerializeField] private GameObject enciclopediaMenu;
    [SerializeField] private GameObject[] cluesMenus;
    //[SerializeField]

    void Start()
    {
        player = GetComponent<PlayerRB>();
    }

    public void ClueHelper()
    {
        GameController.gameController.PauseGame();
        ResetMainMenus();
        tablet.SetActive(true);
      //  player.isInvestigating = true;
        mainScreenMenus[1].SetActive(true);
        cluesMenus[player.currentClue].gameObject.SetActive(true);
    }

    public void ShowControls()
    {
        GameController.gameController.PauseGame();
        ResetMainMenus();
        tablet.SetActive(true);
        mainScreenMenus[2].SetActive(true);
    }    

    private void ResetMainMenus()
    {
        foreach (GameObject menu in mainScreenMenus)
        {
            menu.SetActive(false);
        }
        foreach (GameObject menu in cluesMenus)
        {
            menu.SetActive(false);
        }
    }

}
