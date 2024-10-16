using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<WorkshopObjectSO> workshopObjectSOList;
    public string recipeName;
    public Sprite recipeImage;
}
