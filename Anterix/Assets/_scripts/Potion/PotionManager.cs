using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public List<IngredientInfo.IngredientType> ingredients;

    public void PlaceIngredients(IngredientInfo.IngredientType ingredient)
    {
        ingredients.Add(ingredient);
    }

    public void UsePotion()
    {
        Debug.Log("Potion used");
        ingredients.Clear();
    }
}
