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

        bool isBurger = Random.value < 0.5f;

        if (isBurger)
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

            foreach (string ingredient in burgerIngredients)
            {
                if (Random.value < 0.5f) // Randomly include or exclude each ingredient
                {
                    ingredients.Add(ingredient);
                }
            }

            ingredients.Add("BurgerBunBottom");
        }
        else
        {
            ingredients.AddRange(new string[] { "HotDog", "Bun01", "Bun02" });
        }

        orders.Add(nextOrderNumber, ingredients);

        orderText.text = "Order #" + nextOrderNumber + ": ";

        if (ingredients.Contains("Bun01") && ingredients.Contains("Bun02"))
        {
            orderIngredientsText.text = "Hot Dog";
        }
        else
        {
            orderIngredientsText.text = "Burger with ";

            string[] ingredientArray = ingredients
                .Where(x => x != "BurgerBunTop" && x != "BurgerBunBottom" && x != "BurgerPatty")
                .ToArray();

            for (int i = 0; i < ingredientArray.Length; i++)
            {
                if (i > 0)
                {
                    if (i == 1)
                    {
                        orderIngredientsText.text += ingredientArray[i] + ", ";
                    }
                    else
                    {
                        orderIngredientsText.text += ingredientArray[i];
                    }
                }
                else
                {
                    orderIngredientsText.text += ingredientArray[i];
                }
            }
        }
        nextOrderNumber++;
    }
}
