using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableManager : MonoBehaviour
{
    [SerializeField]
    private List<AssetReferenceGameObject> TestObj = new List<AssetReferenceGameObject>();

    List<GameObject> objList = new List<GameObject>();
    void Start()
    {
        StartCoroutine(InitAddressabled());
    }

    IEnumerator InitAddressabled()
    {
        var init = Addressables.InitializeAsync();
        yield return init;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            for(int i = 0; i < TestObj.Count; i++)
            {
                TestObj[i].InstantiateAsync().Completed += (obj) =>
                {
                    objList.Add(obj.Result);
                };
            }
        }
    }
}