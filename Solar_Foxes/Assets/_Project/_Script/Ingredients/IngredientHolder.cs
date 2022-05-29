using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientHolder : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public IngredientCard cardHolding;

    [SerializeField] private int index = 0;
    public bool allowClick = true;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        RemoveIngredientFromHolder();
        
        cardHolding = eventData.pointerDrag.GetComponent<IngredientCard>();
        cardHolding.AssignHolder(this);
        cardHolding.draggingTransform.position = GetComponent<RectTransform>().position;
        IngredientManager.AddIngredient(index, cardHolding.ingredient);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (allowClick)
            RemoveIngredientFromHolder();
    }

    public void RemoveIngredientFromHolder()
    {
        if (!cardHolding) return;

        cardHolding.ReturnIngredient();
        cardHolding = null;
        IngredientManager.RemoveIngredient(index);
    }
    

}
