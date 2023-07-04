using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public Dictionary<int, List<string>> orders = new Dictionary<int, List<string>>(); // Dictionary to store orders with order numbers
    public int nextOrderNumber = 1; // Counter for the next order number
    public TextMeshProUGUI orderText; // Reference to the TextMeshProUGUI component for displaying the order
    public TextMeshProUGUI orderIngredientsText; // Reference to the TextMeshProUGUI component for displaying the order

    public void GenerateRandomOrder()
    {
        List<string> ingredients = new List<string>();

        bool isBurger = Random.value < 0.5f;

        if (isBurger)
        {
            ingredients.AddRange(
                new string[] { "BurgerPatty", "BurgerBunTop", "BurgerBunBottom", "Tomato", "Salad" }
            );
        }
        else
        {
            ingredients.AddRange(new string[] { "HotDog", "Bun01", "Bun02" });
        }

        orders.Add(nextOrderNumber, ingredients);

        //Debug.Log("Generated order #" + nextOrderNumber + ": " + string.Join(", ", ingredients));

        orderText.text = "Order #" + nextOrderNumber + ": ";
        orderIngredientsText.text = string.Join(", ", ingredients);
        nextOrderNumber++;
    }

}
