using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class OrderManager : MonoBehaviour
{
    public Dictionary<int, List<string>> orders = new Dictionary<int, List<string>>(); // Dictionary to store orders with order numbers
    public int nextOrderNumber = 1; // Counter for the next order number
    public TextMeshProUGUI orderText; // Reference to the TextMeshProUGUI component for displaying the order
    public TextMeshProUGUI orderIngredientsText; // Reference to the TextMeshProUGUI component for displaying the order

    public void GenerateRandomOrder()
    {
        List<string> ingredients = new List<string>();

        bool isBurgerAndNotHotdog = Random.value < 0.5;
        bool orderWithDrink = Random.value < 0.5;
        bool orderWithDrink2 = Random.value < 0.4;

        /*
        if (nextOrderNumber == 1)
        {
            ingredients.AddRange(new string[] { "HotdogSausage", "HotdogBun" });
        }
        else if (nextOrderNumber == 2)
        {
            ingredients.AddRange(new string[] { "HotdogSausage", "HotdogBun", "Drink" });
        }
        else if (nextOrderNumber == 3)
        {
            ingredients.AddRange(
                new string[] { "BurgerBunTop", "Cheese", "BurgerPatty", "BurgerBunBottom" }
            );
              else
        {
        }*/

        if (isBurgerAndNotHotdog)
        {
            ingredients.AddRange(new string[] { "BurgerBunTop", "BurgerPatty" });
            if (nextOrderNumber < 5)
            {
                ingredients.Add("Cheese");
            }
            else if (nextOrderNumber > 5)
            {
                List<string> burgerIngredients = new List<string>()
                {
                    "Tomato",
                    "Salad",
                    "Cheese",
                    "Bacon",
                    "BurgerPatty"
                };
                // Randomly select two additional ingredients
                List<string> randomIngredients = burgerIngredients
                    .OrderBy(x => Random.value)
                    .Take(2)
                    .ToList();

                ingredients.AddRange(randomIngredients);
            }

            ingredients.Add("BurgerBunBottom");
        }
        else
        {
            ingredients.AddRange(new string[] { "HotdogSausage", "HotdogBun" });
        }

        // Add the drinks
        if (orderWithDrink)
        {
            ingredients.Add("Drink");
        }
        if (orderWithDrink2)
        {
            ingredients.Add("Drink");
        }

        orders.Add(nextOrderNumber, ingredients);

        orderText.text = "Order #" + nextOrderNumber + ": ";
        orderIngredientsText.text = string.Join("\n", ingredients);

        nextOrderNumber++;
    }
}
