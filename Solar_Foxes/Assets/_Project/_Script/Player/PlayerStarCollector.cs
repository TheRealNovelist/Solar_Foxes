using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerStarCollector : MonoBehaviour
{
    public int numberOfStars;
    public int starsCollected;
    public List<int> requirementAmountOfStars;

    public List<Ingredient> rareIngredients;
    
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
            Debug.Log("Acquired Material");
        }
    }
    
}
