using UnityEngine;

public class Tablet : MonoBehaviour
{
    private PlayerRB player;

    [SerializeField] private GameObject tablet;
    [SerializeField] private GameObject[] mainScreenMenus;
    //[SerializeField] private GameObject enciclopediaMenu;
    [SerializeField] private GameObject[] cluesMenus;
    //[SerializeField]
    public bool foundClue1;
    [SerializeField] private GameObject clueLog1;
    public bool foundClue2;
    [SerializeField] private GameObject clueLog2;
    public bool foundClue3;
    [SerializeField] private GameObject clueLog3;

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

    public void UpdateClues(int x = 0)
    {
        if (x == 0) foundClue1 = true;
        else if (x == 1) foundClue2 = true;
        else if (x == 2) foundClue3 = true;
    }

    public void ShowLog()
    {
        //ResetMainMenus();
        //tablet.SetActive(true);
        mainScreenMenus[0].SetActive(true);
        UpdateLog();
    }

    public void UpdateLog()
    {
        if (foundClue1) clueLog1.SetActive(true);
        if (foundClue2) clueLog2.SetActive(true);
        if (foundClue3) clueLog3.SetActive(true);
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
