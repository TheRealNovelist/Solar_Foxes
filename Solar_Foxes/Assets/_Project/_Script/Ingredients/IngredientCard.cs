using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


[RequireComponent(typeof(CanvasGroup))]
public class IngredientCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public IngredientManager manager;
    public Ingredient ingredient;

    public TextMeshProUGUI text;
    public Image displayImage;
    public Image draggingImage;

    [HideInInspector] public RectTransform displayTransform;
    [HideInInspector] public RectTransform draggingTransform;

    private Vector2 originalPosition;
    private Vector2 pulledPosition;
    
    public IngredientHolder currentHolder;

    public void Init(IngredientManager newManager)
    {
        manager = newManager;
    }
    
    public void AssignHolder(IngredientHolder holder)
    {
        currentHolder = holder;
    }

    public void AssignIngredient(Ingredient newIngredient)
    {
        ingredient = newIngredient;
        UpdateCard();
    }

    private void UpdateCard()
    {
        if (ingredient != null)
        {
            text.text = ingredient.name;
            displayImage.sprite = ingredient.sprite;
            draggingImage.sprite = ingredient.sprite;
        }
    }
    
    private void Awake()
    {
        displayTransform = displayImage.GetComponent<RectTransform>();
        draggingTransform = draggingImage.GetComponent<RectTransform>();
        draggingImage.enabled = false;

        var position = transform.position;
        originalPosition = position;
        pulledPosition = position + Vector3.down * 100;
        
        UpdateCard();
    }

    private void OnValidate()
    {
        UpdateCard();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentHolder) return;

        transform.position = pulledPosition;
        draggingImage.enabled = true;
        draggingTransform.position = eventData.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (currentHolder) return;
        
        draggingTransform.anchoredPosition += eventData.delta;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentHolder) return;
        
        ReturnDraggingImage();
    }
    
    private void ReturnDraggingImage()
    {
        transform.position = originalPosition;
        draggingTransform.anchoredPosition = displayTransform.anchoredPosition;
        draggingImage.enabled = false;
    }
    
    public void ReturnIngredient()
    {
        currentHolder = null;
        ReturnDraggingImage();
    }
    
}
