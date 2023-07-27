using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }
    void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += Instance_OnRecipeSpawned;
        UpdateVisual();
    }

    private void Instance_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    void Update()
    {
        
    }
    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;

            Destroy(child.gameObject);           
        }
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliverManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
