using UnityEngine;

[CreateAssetMenu(fileName = "IngredientInfo", menuName = "Scriptable Objects/IngredientInfo")]
public class IngredientInfo : ScriptableObject
{
    public enum IngredientType
    {
        chilli = 0,
        mushroom = 1,
        acorn = 2,
        clover = 3,
    }
    
    public IngredientType ingredientType;
    public string ingredientName;
    public Sprite ingredientSprite;
}
