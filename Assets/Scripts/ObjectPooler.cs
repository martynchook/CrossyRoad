using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary <string, Queue<GameObject>> poolDictionary;

    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach ( Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(this.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public bool TryToSpawnFromPool (string tag, Vector3 position, out GameObject obj) 
    { 
        bool isPresent = false;
        obj = null;
        if (poolDictionary.TryGetValue(tag, out var poolQueue ) && poolDictionary.Count > 0 ) 
        {  
            isPresent = true;
            obj = poolQueue.Dequeue(); 
            obj.SetActive(true); 
            obj.transform.position = position; 
            poolQueue.Enqueue(obj); 
        }          
        return isPresent ; 
    }

}
