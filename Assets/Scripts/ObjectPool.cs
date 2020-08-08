using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get { return instance; } }

    private static ObjectPool instance;

    private Dictionary<string, Stack<GameObject>> poolDictionary;

    private void Awake()
    {
        instance = this;
        poolDictionary = new Dictionary<string, Stack<GameObject>>();
    }

    public GameObject ReuseGameObject(GameObject prefabInstance)
    {
        string poolName = prefabInstance.name;

        if (poolDictionary.ContainsKey(poolName))
        {
            if(poolDictionary[poolName].Count > 0)
            {
                GameObject reusePrefabInstance = poolDictionary[poolName].Pop();
                reusePrefabInstance.SetActive(true);
                return reusePrefabInstance;
            }
            else
            {
                return CreatePrefabInstance(prefabInstance);
            }
        }
        else
        {
            poolDictionary.Add(poolName, new Stack<GameObject>());
            return CreatePrefabInstance(prefabInstance);
        }
    }

    private GameObject CreatePrefabInstance(GameObject prefab)
    {
        GameObject newPrefabInstance = Instantiate(prefab);
        newPrefabInstance.name = prefab.name;
        newPrefabInstance.transform.SetParent(transform);
        return newPrefabInstance;
    }

    public void SetBackToPool(GameObject prefabInstance)
    {
        string poolName = prefabInstance.name;

        prefabInstance.SetActive(false);

        if (poolDictionary.ContainsKey(poolName))
        {
            poolDictionary[poolName].Push(prefabInstance);
        }
        else
        {
            poolDictionary.Add(poolName, new Stack<GameObject>());
            poolDictionary[poolName].Push(prefabInstance);
        }
    }
}
