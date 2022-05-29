using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerStarCollector : MonoBehaviour
{
    public int numberOfStars;
    public int starsCollected;
    public List<int> requirementAmountOfStars;

    public List<Ingredient> rareIngredients;

    public TextMeshProUGUI acquiredMessage;
    
    private void Awake()
    {
        while (requirementAmountOfStars.Count < rareIngredients.Count)
        {
            int newRequirement = Random.Range(1, numberOfStars);
            if (!requirementAmountOfStars.Contains(newRequirement))
            {
                requirementAmountOfStars.Add(newRequirement);
            }
        }
        
        requirementAmountOfStars.Sort();
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            Debug.Log("Collided " + collision);
            starsCollected++;
            CheckIfRequirementMet();
            collision.gameObject.SetActive(false);
        }
    }

    private void CheckIfRequirementMet()
    {
        if (starsCollected == requirementAmountOfStars[0])
        {
            requirementAmountOfStars.RemoveAt(0);
            StartCoroutine(Message(rareIngredients[0].ingredientName));
            rareIngredients.RemoveAt(0);
        }
    }

    private IEnumerator Message(string ingredientName)
    {
        acquiredMessage.text = "You have acquired " + ingredientName;
        acquiredMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        acquiredMessage.gameObject.SetActive(false);
    }
}
