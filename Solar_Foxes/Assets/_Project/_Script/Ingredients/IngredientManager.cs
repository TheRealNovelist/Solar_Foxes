using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static Ingredient[] ChosenIngredients { get; private set; } = new Ingredient[5];    
    
    public List<IngredientCard> allCards;
    public List<IngredientHolder> allHolders;

    public IngredientPool pool;
    
    private void Awake()
    {
        foreach (IngredientCard card in allCards)
        {
            card.Init(this);
            card.AssignIngredient(pool.GetRandomIngredient());
        }
    }

    public Ingredient RequestIngredient()
    {
        return pool.GetRandomIngredient();
    }

    public static void AddIngredient(int index, Ingredient ingredient)
    {
        ChosenIngredients[index] = ingredient;
        FindObjectOfType<PlayerMovement>().RefreshTotalMovement();
    }

    public static void RemoveIngredient(int index)
    {
        ChosenIngredients[index] = null;
        FindObjectOfType<PlayerMovement>().RefreshTotalMovement();
    }

    public void ReleaseAllHolders()
    {
        foreach (var holder in allHolders)
        {
            holder.RemoveIngredientFromHolder();
        }
    }
}
