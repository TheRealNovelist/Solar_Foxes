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
    public PlayerMovement player;
    
    private void Awake()
    {
        foreach (IngredientCard card in allCards)
        {
            card.Init(this);
            card.AssignIngredient(pool.GetRandomIngredient());
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void ConsumeIngredient()
    {
        foreach (var holder in allHolders)
        {
            if (holder.cardHolding) holder.cardHolding.AssignIngredient(pool.GetRandomIngredient());
            holder.RemoveIngredientFromHolder();
        }
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

    public void SetAllowHolderClick(bool isAllowed)
    {
        foreach (var holder in allHolders)
        {
            holder.allowClick = isAllowed;
        }
    }
    
    public void ReleaseAllHolders()
    {
        foreach (var holder in allHolders)
        {
            holder.RemoveIngredientFromHolder();
        }
    }

    public void Burn()
    {
        ReleaseAllHolders();
        player.Restart();
        foreach (IngredientCard card in allCards)
        {
            card.StartBurning(pool.GetRandomIngredient());
        }
    }
}
