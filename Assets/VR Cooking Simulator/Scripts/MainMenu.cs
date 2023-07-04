using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialGameObjects;
    public GameObject newGameGameObjects;

    public OrderManager orderManager;

    public ValidateButtonVR validateButtonVR;

    void Start()
    {
        orderManager = FindObjectOfType<OrderManager>();
        validateButtonVR = FindObjectOfType<ValidateButtonVR>();
    }

    public void BackToMenu()
    {
        orderManager.orders = new Dictionary<int, List<string>>();
        orderManager.nextOrderNumber = 1;
        orderManager.orderText.text = "";
        orderManager.orderIngredientsText.text = "";

        validateButtonVR.isValid = false;
        validateButtonVR.points = 0;
        validateButtonVR.displayPointsText.text = " 0 Points";
        validateButtonVR.maxFailures = 0;
        validateButtonVR.displayValidText.text = "";
        validateButtonVR.displayText.text = "";

        gameObject.SetActive(true);
        newGameGameObjects.SetActive(false);
        tutorialGameObjects.SetActive(false);
    }

    public void NewGame()
    {
        orderManager.GenerateRandomOrder();
        gameObject.SetActive(false);
        newGameGameObjects.SetActive(true);
    }

    public void NewTutorial()
    {
        orderManager.GenerateRandomOrder();
        gameObject.SetActive(false);
        newGameGameObjects.SetActive(true);
        tutorialGameObjects.SetActive(true);
    }

    public void OpenCredits() { }

    public void ExitGame()
    {
        // exit the unity game
        Application.Quit();
    }
}
