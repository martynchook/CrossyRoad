using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectsSpawner : MonoBehaviour
{
    
    ObjectPooler objectPooler;
    
    public string [] tagObj;
    public float duration;
    public float minSpawnTime, maxSpawnTime;
    public bool spawnMovingObjects = false;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance; 
        if (spawnMovingObjects)
        {
            SpawnMovingObjects();
        }
        else
        {
            SpawnStaticObjects();
        }
    }

    
    void SpawnMovingObjects()
    {
		objectPooler.TryToSpawnFromPool(tagObj[0], transform.position, out var gameObjectFromPool);
        Invoke("SpawnMovingObjects", Random.Range(minSpawnTime, maxSpawnTime));
        ObjectMove(tagObj[0], gameObjectFromPool);
    }

    void SpawnStaticObjects()
    {
        int Coin = Random.Range(0, 7);
        if( Coin == 0 )
        {
            objectPooler.TryToSpawnFromPool(tagObj[2], new Vector3(Random.Range(-8, 8), 0, transform.position.z), out var gameObjectFromPool); 
        }
        else
        {
            for (int i = 0; i < 3; i++) {
                objectPooler.TryToSpawnFromPool(tagObj[Random.Range(0, 2)], new Vector3(Random.Range(-8, 8), 0, transform.position.z), out var gameObjectFromPool);
            }
        } 
    }

    
    private void ObjectMove(string tag, GameObject gameObject)
    {
        if (tagObj[0] == "Car 01 Back" || tagObj[0] == "Car 02 Back" || tagObj[0] == "Wood Back")
        {
            gameObject.transform.DOMove(new Vector3 (-20, transform.position.y, transform.position.z ), duration, false);
        }
        else
        {
            gameObject.transform.DOMove(new Vector3 (20, transform.position.y, transform.position.z ), duration, false);
        } 
    }


}
