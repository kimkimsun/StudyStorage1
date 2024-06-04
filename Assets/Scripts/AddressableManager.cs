using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableManager : MonoBehaviour
{
    private bool onlyFirst = true;

    [SerializeField]
    private List<AssetReferenceGameObject> TestObj = new List<AssetReferenceGameObject>();

    List<GameObject> objList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < TestObj.Count; i++)
        {
            TestObj[i].InstantiateAsync().Completed += (obj) =>
            {
                objList.Add(obj.Result);
            };
        }
    }
}