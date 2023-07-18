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

        //Debug.Log("RANDOM" + Random.value);

        bool isBurgerAndNotHotdog = Random.value < 0.5;
        bool orderWithDrink = Random.value < 0.5;
        bool orderWithDrink2 = Random.value < 0.4;

        if (isBurgerAndNotHotdog)
        {
            ingredients.Add("BurgerBunTop");
            ingredients.Add("BurgerPatty");

            List<string> burgerIngredients = new List<string>()
            {
                "Tomato",
                "Salad",
                "Cheese",
                "Bacon",
                "BurgerPatty"
            };

            float ingredientProbability = 0.1f; // Initial probability of including each ingredient

            foreach (string ingredient in burgerIngredients)
            {
                if (Random.value < ingredientProbability)
                {
                    ingredients.Add(ingredient);
                }

                ingredientProbability += 0.1f; // Increase the probability for each ingredient

                if (ingredientProbability > 0.8f)
                {
                    ingredientProbability -= Random.value; // Limit the probability to 50%
                }
            }

            ingredients.Add("BurgerBunBottom");
        }
        else
        {
            ingredients.AddRange(new string[] { "HotDog", "HotdogBun" });
        }

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
