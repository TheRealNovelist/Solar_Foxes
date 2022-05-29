using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStarCollector : MonoBehaviour
{
    public int numberOfStars;
    public int starsCollected;
    public List<int> requirementAmountOfStars;

    public AudioSource fx;

    public List<Ingredient> rareIngredients;

    public GameObject acquiredObject;
    private TextMeshProUGUI acquiredMessage;
    private Image acquiredImage;
    
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

        acquiredMessage = acquiredObject.GetComponentInChildren<TextMeshProUGUI>();
        acquiredImage = acquiredObject.GetComponentInChildren<Image>();
    }

    private void Update()
    {
        if (rareIngredients.Count == 0)
        {
            SceneManager.LoadScene("Winning Screen");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            Debug.Log("Collided " + collision);
            starsCollected++;
            fx.Play();
            CheckIfRequirementMet();
            collision.gameObject.SetActive(false);
        }
    }

    private void CheckIfRequirementMet()
    {
        if (starsCollected == requirementAmountOfStars[0])
        {
            requirementAmountOfStars.RemoveAt(0);
            StartCoroutine(Message(rareIngredients[0]));
            rareIngredients.RemoveAt(0);
        }
    }

    private IEnumerator Message(Ingredient ingredient)
    {
        acquiredMessage.text = "You have acquired " + ingredient.ingredientName;
        acquiredImage.sprite = ingredient.sprite;
        acquiredObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        acquiredObject.SetActive(false);
    }
}
