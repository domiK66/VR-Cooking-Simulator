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

    // Return to the main menu and reset game state
    public void BackToMenu()
    {
        // Reset order manager
        orderManager.orders = new Dictionary<int, List<string>>();
        orderManager.nextOrderNumber = 1;
        orderManager.orderText.text = "";
        orderManager.orderIngredientsText.text = "";

        // Reset validate button VR
        validateButtonVR.isValid = false;
        validateButtonVR.points = 0;
        validateButtonVR.displayPointsText.text = " 0 Points";
        validateButtonVR.maxFailures = 0;
        validateButtonVR.displayValidText.text = "";
        validateButtonVR.displayText.text = "";

        // Destroy all ingredient objects
        GameObject[] ingredientObjects = GameObject.FindGameObjectsWithTag("IngredientDelete");
        foreach (GameObject obj in ingredientObjects)
        {
            Destroy(obj);
        }

        // Activate the main menu and deactivate other screens
        gameObject.SetActive(true);
        newGameGameObjects.SetActive(false);
        tutorialGameObjects.SetActive(false);

        // Play menu music and particle system
        menuMusic.Play();
        musicParticles.Play();
    }

    // Return to the main menu from the credits screen
    public void BackToMenuFromCredits()
    {
        gameObject.SetActive(true);
        creditsGameObjects.SetActive(false);
    }

    // Start a new game
    public void NewGame()
    {
        orderManager.GenerateRandomOrder();

        // Deactivate the main menu and activate the new game screen
        gameObject.SetActive(false);
        newGameGameObjects.SetActive(true);

        // Update ingredient count
        var ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");
        var TextIngredients = GameObject.FindGameObjectWithTag("TextIngredients");
        TextIngredients.GetComponent<TMPro.TextMeshProUGUI>().text =
            "Ingredients: " + ingredientObjects.Length + " / 30";

        // Instantiate interactables object
        Instantiate(
            Interactables,
            new Vector3(-1.0038172f, 0.983418643f, -0.134758055f),
            Quaternion.identity
        );
    }

    // Start a new tutorial game
    public void NewTutorial()
    {
        musicParticles.Stop();
        menuMusic.Stop();
        orderManager.GenerateRandomOrder();

        // Deactivate the main menu and activate the new game and tutorial screens
        gameObject.SetActive(false);
        newGameGameObjects.SetActive(true);
        tutorialGameObjects.SetActive(true);

        // Update ingredient count
        var ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");
        var TextIngredients = GameObject.FindGameObjectWithTag("TextIngredients");
        TextIngredients.GetComponent<TMPro.TextMeshProUGUI>().text =
            "Ingredients: " + ingredientObjects.Length + " / 30";

        // Instantiate interactables objects for new game and tutorial
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

    // Open the credits screen
    public void OpenCredits()
    {
        gameObject.SetActive(false);
        creditsGameObjects.SetActive(true);
    }

    // Exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
