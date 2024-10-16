using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeImageSO : ScriptableObject
{
    public string recipeName;
    public List<WorkshopObjectToUIImage> ObjectToUIImageList;
}

[System.Serializable]
public class WorkshopObjectToUIImage
{
    public WorkshopObjectSO workshopObjectSO;
    public Sprite uiImage;
}
