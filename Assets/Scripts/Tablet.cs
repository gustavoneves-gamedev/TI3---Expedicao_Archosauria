using UnityEngine;

public class Tablet : MonoBehaviour
{
    private Player player;

    [SerializeField] private GameObject tablet;
    [SerializeField] private GameObject[] mainScreenMenus;
    //[SerializeField] private GameObject enciclopediaMenu;
    [SerializeField] private GameObject[] cluesMenus;
    //[SerializeField]

    void Start()
    {
        player = GetComponent<Player>();
    }

    public void ClueHelper()
    {
        ResetMainMenus();
        tablet.SetActive(true);
        mainScreenMenus[1].SetActive(true);
        cluesMenus[player.currentClue].gameObject.SetActive(true);
    }

    //public void ExitClueHelper()
    //{
    //    if (player.isInvestigating)
    //    {

    //    }
    //}

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
