using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[RequireComponent(typeof(CanvasGroup))]
public class IngredientCard : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Ingredient ingredient;

    public TextMeshProUGUI text;
    public Image displayImage;
    public Image draggingImage;

    [HideInInspector] public RectTransform displayTransform;
    [HideInInspector] public RectTransform draggingTransform;
    
    public IngredientHolder currentHolder;
    
    
    public void Init(IngredientHolder holder)
    {
        currentHolder = holder;
    }
    
    private void Awake()
    {
        displayTransform = displayImage.GetComponent<RectTransform>();
        draggingTransform = draggingImage.GetComponent<RectTransform>();
        draggingImage.enabled = false;
    }

    private void OnValidate()
    {
        if (ingredient != null)
        {
            text.text = ingredient.name;
            displayImage.sprite = ingredient.sprite;
            draggingImage.sprite = ingredient.sprite;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentHolder) return;
        
        draggingImage.enabled = true;
        draggingTransform.position = eventData.position;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentHolder) return;
        
        GetComponent<CanvasGroup>().alpha = 0.6f;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (currentHolder) return;
        
        draggingTransform.anchoredPosition += eventData.delta;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentHolder) return;

        EnableImage();
    }

    private void EnableImage()
    {
        GetComponent<CanvasGroup>().alpha = 1f;
        draggingTransform.anchoredPosition = displayTransform.anchoredPosition;
        draggingImage.enabled = false;
    }
    
    public void FreeIngredient()
    {
        currentHolder = null;
        
        EnableImage();
    }
}
