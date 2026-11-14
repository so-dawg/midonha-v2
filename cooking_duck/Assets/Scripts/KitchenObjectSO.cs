using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    private string objectName;
}
