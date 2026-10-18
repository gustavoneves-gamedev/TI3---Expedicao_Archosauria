using TMPro;
using UnityEditor.UI;
using UnityEngine;

public class Clue : MonoBehaviour
{
    [SerializeField] private int clueCode = 0;
    [SerializeField] private int correctOption = 0;
    [SerializeField] private GameObject investigateButton;
    [SerializeField] private Transform clueFocus;
    [SerializeField] private GameObject choiceMenu;
    [SerializeField] private GameObject choiceResult;
    [SerializeField] private TextMeshProUGUI choiceText;
    [SerializeField] private GameObject softBlocker;
    private bool hasShow;

    public void ChooseMenu()
    {
        choiceMenu.SetActive(true);
    }

    public void ChooseOption(int option)
    {
                
        if (option == correctOption)
        {
            choiceText.text = "Muito bem! " +
                "Essas características são compatíveis com a Formação Serra da Galga," +
                " do final do período Cretáceo. " +
                "Isso significa que nossa criatura pode ter vivido entre cerca de 72 e 66 milhões de anos atrás. " +
                "Vamos registrar essa pista em seu Diário!";

        }
        else
        {
            choiceText.text = "Essa hipótese não combina com todas as pistas." +
                "Compare novamente as características da rocha com os registros do tablet.";
        }

        choiceResult.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (clueCode != 0 || clueCode == 0 && hasShow) investigateButton.SetActive(true);
            //else if (clueCode == 0 && hasShow)

                GameController.gameController.player.clueFocus = clueFocus;
            GameController.gameController.player.currentClue = clueCode;
            GameController.gameController.player.clue = this;

            if (clueCode == 0)
            {
                if(!hasShow) GameController.gameController.uiController.ShowTutorial(2);
                
                hasShow = true;
                softBlocker.SetActive(false);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            investigateButton.SetActive(false);

            if (clueCode == 0)
            {
                //RockClue();
            }

        }
    }

}
