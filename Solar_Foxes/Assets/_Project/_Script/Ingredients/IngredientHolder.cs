using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientHolder : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    private IngredientCard cardHolding;
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        cardHolding = eventData.pointerDrag.GetComponent<IngredientCard>();
        cardHolding.Init(this);
        cardHolding.draggingTransform.position = GetComponent<RectTransform>().position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!cardHolding) return;
        
        cardHolding.FreeIngredient();
    }
}
