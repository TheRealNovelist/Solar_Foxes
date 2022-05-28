using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Ingredient/Ingredient Pool", fileName = "New Ingredient Pool", order = 1)]
public class IngredientPool : ScriptableObject
{
    [SerializeField] private List<Ingredient> allIngredients;

    public Ingredient GetRandomIngredient()
    {
        return allIngredients[Random.Range(0, allIngredients.Count)];
    }
}
