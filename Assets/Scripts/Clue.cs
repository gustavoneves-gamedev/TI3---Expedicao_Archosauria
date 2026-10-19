using TMPro;
//using UnityEditor.UI;
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
    public string correctAnswer;
    public string wrongAnswer;
    private bool hasShow;

    public void ChooseMenu()
    {
        choiceMenu.SetActive(true);
    }

    public void ChooseOption(int option)
    {
                
        if (option == correctOption)
        {
            choiceText.text = correctAnswer;
            hasShow = false;
            gameObject.SetActive(false);
        }
        else
        {
            choiceText.text = wrongAnswer;
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

            if (clueCode == 1)
            {
                if (!hasShow) GameController.gameController.uiController.ShowTutorial(5);

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
