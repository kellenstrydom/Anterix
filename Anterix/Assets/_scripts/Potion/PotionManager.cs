using System;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public List<IngredientInfo.IngredientType> ingredients;
    public Transform potion;

    private void Awake()
    {
        potion = transform;
    }


    public void AddIngredients(IngredientInfo.IngredientType ingredient)
    {
        ingredients.Add(ingredient);
    }

    public void UsePotion()
    {
        Debug.Log("Potion used");
        ingredients.Clear();
    }
}
