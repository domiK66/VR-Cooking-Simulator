using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialGameObjects;
    public GameObject newGameGameObjects;
    public GameObject creditsGameObjects;
    public OrderManager orderManager;
    public ValidateButtonVR validateButtonVR;

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

        GameObject[] ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");

        foreach (GameObject obj in ingredientObjects)
        {
            GameObject highestParent = validateButtonVR.GetHighestParent(obj);
            Destroy(highestParent); // Destroy the parent object
        }

        gameObject.SetActive(true);
        newGameGameObjects.SetActive(false);
        tutorialGameObjects.SetActive(false);
    }

    public void BackToMenuFromCredits()
    {
        gameObject.SetActive(true);
        creditsGameObjects.SetActive(false);
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

    public void OpenCredits()
    {
        gameObject.SetActive(false);
        creditsGameObjects.SetActive(true);
    }

    public void ExitGame()
    {
        // exit the unity game
        Application.Quit();
    }
}
