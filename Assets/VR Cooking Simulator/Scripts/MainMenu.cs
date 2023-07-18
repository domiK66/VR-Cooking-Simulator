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

    public GameObject tutorialInteractables;

    public GameObject Interactables;

    public AudioSource menuMusic;

    public ParticleSystem musicParticles;

    public void Start()
    {
        menuMusic.Play();
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

        GameObject[] ingredientObjects = GameObject.FindGameObjectsWithTag("IngredientDelete");

        foreach (GameObject obj in ingredientObjects)
        {
            Destroy(obj);
        }

        gameObject.SetActive(true);
        newGameGameObjects.SetActive(false);
        tutorialGameObjects.SetActive(false);
        menuMusic.Play();
        musicParticles.Play();
    }

    public void BackToMenuFromCredits()
    {
        gameObject.SetActive(true);
        creditsGameObjects.SetActive(false);
    }

    public void NewGame()
    {
        //menuMusic.Stop();
        orderManager.GenerateRandomOrder();
        gameObject.SetActive(false);
        newGameGameObjects.SetActive(true);
        var ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");
        var TextIngredients = GameObject.FindGameObjectWithTag("TextIngredients");
        TextIngredients.GetComponent<TMPro.TextMeshProUGUI>().text =
            "Ingredients: " + ingredientObjects.Length + " / 30";
        Instantiate(
            Interactables,
            new Vector3(-1.0038172f, 0.983418643f, -0.134758055f),
            Quaternion.identity
        );
    }

    public void NewTutorial()
    {
        musicParticles.Stop();
        menuMusic.Stop();
        orderManager.GenerateRandomOrder();
        gameObject.SetActive(false);
        newGameGameObjects.SetActive(true);
        tutorialGameObjects.SetActive(true);
        var ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");
        var TextIngredients = GameObject.FindGameObjectWithTag("TextIngredients");
        TextIngredients.GetComponent<TMPro.TextMeshProUGUI>().text =
            "Ingredients: " + ingredientObjects.Length + " / 30";

        Instantiate(
            Interactables,
            new Vector3(-1.0038172f, 0.983418643f, -0.134758055f),
            Quaternion.identity
        );
        Instantiate(
            tutorialInteractables,
            new Vector3(-0.0449822545f, 0.845037818f, 0.322987556f),
            Quaternion.identity
        );
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
