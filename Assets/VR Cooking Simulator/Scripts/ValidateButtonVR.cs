using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ValidateButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public Collider targetCollider; // The collider area to check against
    private List<GameObject> objectsToValidate = new List<GameObject>(); // List to store objects that collide with the target collider
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    public TextMeshProUGUI displayText; // Reference to the TextMeshProUGUI component in the Canvas
    public TextMeshProUGUI displayValidText; // Reference to the TextMeshProUGUI component in the Canvas

    public TextMeshProUGUI displayLastOrderText; // Reference to the TextMeshProUGUI component in the Canvas
    public TextMeshProUGUI displayLastOrderIngredients;
    public string[] ingredientsArray; // Array to store ingredient names
    public OrderManager orderManager; // Reference to the OrderManager script
    public MainMenu menu; // Reference to the OrderManager script
    public Color redColor = new Color(1f, 0f, 0f); // Red color (full red, no green or blue)
    public Color greenColor = new Color(0f, 1f, 0f); // Green color (full green, no red or blue)
    public bool isValid = false;
    public int points = 0;
    public TextMeshProUGUI displayPointsText;
    public int maxFailures = 0;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        // Get the reference to the OrderManager script
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void ValidateObjects()
    {
        //int ingredientCount = 0; // Counter for the number of ingredients found
        displayText.text = ""; // Clear the text display

        // Validate the order
        IsCurrentOrderValid();
        displayLastOrderText.text = "Last order: Order #" + (orderManager.nextOrderNumber - 1);
        displayLastOrderIngredients.text = string.Join(
            "\n",
            orderManager.orders[orderManager.nextOrderNumber - 1]
        );
        if (isValid)
        {
            //Debug.Log("Order is valid!");
            displayValidText.text = "Order was valid! (+10 Points)";
            displayValidText.color = greenColor;
            points += 10;
            maxFailures = 0;
        }
        else
        {
            //Debug.Log("Order is not valid!");
            displayValidText.text = "Order was invalid! (-10 Points)";
            displayValidText.color = redColor;
            points -= 10;
            maxFailures++;

            if (maxFailures > 2)
            {
                points = 0;
                displayValidText.text = "";
                displayPointsText.text = "0 Points";
                displayText.text = "";
                displayLastOrderText.text = "Last order:";
                displayLastOrderIngredients.text = "No order delivered yet";
                maxFailures = 0;
                isValid = false;
                menu.BackToMenu();
                return;
            }
        }
        displayPointsText.text =
            points.ToString() + " Points" + "\n" + maxFailures.ToString() + "/3 Failures" + "\n" + "Orders delivered: " + (orderManager.nextOrderNumber - 1).ToString();

        // Destroy the elements for a new order
        List<GameObject> collidingIngredients = new List<GameObject>();
        Collider[] colliders = Physics.OverlapBox(
            targetCollider.bounds.center,
            targetCollider.bounds.extents,
            targetCollider.transform.rotation
        );

        foreach (Collider collider in colliders)
        {
            GameObject obj = collider.gameObject;
            if (obj.CompareTag("Ingredient"))
            {
                GameObject highestParent = GetHighestParentWithTag(obj, "IngredientDelete");
                Destroy(highestParent);
            }
        }

        orderManager.GenerateRandomOrder();
    }

    private void IsCurrentOrderValid()
    {
        // get the last element of the orders array
        // Debug.Log(orderManager.orders.Count);
        List<string> currentOrderIngredients = orderManager.orders[orderManager.orders.Count];

        // Iterate over the colliders and check if they match the ingredients in the current order
        Collider[] colliders = Physics.OverlapBox(
            targetCollider.bounds.center,
            targetCollider.bounds.extents,
            targetCollider.transform.rotation
        );

        List<string> collidingIngredients = new List<string>();

        foreach (Collider collider in colliders)
        {
            GameObject obj = collider.gameObject;
            if (obj.CompareTag("Ingredient"))
            {
                collidingIngredients.Add(obj.name);
                // Display the ingredient name in the TextMeshProUGUI component
                displayText.text += obj.name + "\n";

                if (!currentOrderIngredients.Contains(obj.name))
                {
                    isValid = false;
                }
                else
                {
                    var currentOrderIngredient = obj;
                    //Debug.Log("test" + currentOrderIngredient);
                    // Access the highest parent GameObject
                    GameObject highestParent = GetHighestParentWithTag(
                        currentOrderIngredient,
                        "IngredientDelete"
                    );
                    // Access the component in the highest parent GameObject
                    CookTime cookTime = highestParent.GetComponent<CookTime>();
                    // Check if the cooktime script is present in the highest parent GameObject
                    if (cookTime != null)
                    {
                        // Access the public variable of the cooktime script
                        //Debug.Log("test" + cookTime.cookingState);
                        CookingState cookingState = cookTime.cookingState;
                        if (cookingState == CookingState.raw || cookingState == CookingState.burned)
                        {
                            isValid = false;
                        }
                    }
                }
            }
        }
        // Check if the colliding ingredients match the current order's ingredients
        if (currentOrderIngredients.Count == collidingIngredients.Count)
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }
    }

    public GameObject GetHighestParent(GameObject obj)
    {
        Transform parentTransform = obj.transform;
        while (parentTransform.parent != null)
        {
            parentTransform = parentTransform.parent;
        }
        return parentTransform.gameObject;
    }

    private GameObject GetHighestParentWithTag(GameObject child, string tag)
    {
        Transform parentTransform = child.transform.parent;
        while (parentTransform != null)
        {
            if (parentTransform.CompareTag(tag))
            {
                return parentTransform.gameObject;
            }
            parentTransform = parentTransform.parent;
        }
        return null;
    }
}
