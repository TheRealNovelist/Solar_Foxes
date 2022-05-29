using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class IngredientCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public IngredientManager manager;
    public Ingredient ingredient;

    public TextMeshProUGUI text;
    public Image displayImage;
    public Image draggingImage;
    public GameObject objectToMove;

    private bool allowInput = true;
    
    [HideInInspector] public RectTransform displayTransform;
    [HideInInspector] public RectTransform draggingTransform;

    private float originalPosition;
    private float pulledPosition;
    
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

        var position = objectToMove.transform.localPosition;
        originalPosition = position.y;
        pulledPosition = position.y - 100;
        
        UpdateCard();
    }

    private void OnValidate()
    {
        UpdateCard();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentHolder || !allowInput) return;

        objectToMove.transform.DOLocalMoveY(pulledPosition, 0.5f);
        draggingImage.enabled = true;
        draggingTransform.position = eventData.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (currentHolder || !allowInput) return;
        
        draggingTransform.anchoredPosition += eventData.delta;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentHolder || !allowInput) return;
        
        ReturnDraggingImage();
    }
    
    private void ReturnDraggingImage()
    {
        objectToMove.transform.DOLocalMoveY(originalPosition, 0.5f);
        draggingTransform.parent = gameObject.transform;
        draggingTransform.anchoredPosition = displayTransform.anchoredPosition;
        draggingImage.enabled = false;
    }
    
    public void ReturnIngredient()
    {
        currentHolder = null;
        ReturnDraggingImage();
    }

    public void StartBurning(Ingredient newIngredient)
    {
        StartCoroutine(Burning(newIngredient));
    }

    private IEnumerator Burning(Ingredient newIngredient)
    {
        Sequence sequence = DOTween.Sequence();
        
        allowInput = false;
        
        sequence.Append(objectToMove.transform.DOLocalMoveY(pulledPosition - 200, 0.5f));
        AssignIngredient(newIngredient);
        sequence.Append(objectToMove.transform.DOLocalMoveY(originalPosition, 0.5f));

        yield return new DOTweenCYInstruction.WaitForCompletion(sequence);

        allowInput = true;
    }
    
}
