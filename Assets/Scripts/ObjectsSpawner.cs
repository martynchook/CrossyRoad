using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectsSpawner : MonoBehaviour
{  
    [SerializeField] private string [] tagObj;
    [SerializeField] private float duration;
    [SerializeField] private float minSpawnTime, maxSpawnTime;
    [SerializeField] private float xPosEnd;
    [SerializeField] private bool spawnMovingObjects = false;
    
    private ObjectPooler objectPooler;
    private Tween tween;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance; 
        if (spawnMovingObjects)
        {
            StartCoroutine(SpawnMovingObjects());
        }
        else
        {
            SpawnStaticObjects();
        }
    }

    private IEnumerator SpawnMovingObjects()
    {
	    while(true)
	    {
		    yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
		    GameObject g = objectPooler.SpawnFromPool(tagObj[0], transform.position);
            tween = g.transform.DOMoveX(xPosEnd, duration, false);
            tween.Restart();
            if (transform.position.x == 0)
            {
                gameObject.SetActive(false);
            }
	    }
    }

    private void SpawnStaticObjects()
    {
        int Coin = Random.Range(0, 7);
        if( Coin == 0 )
        {
            objectPooler.SpawnFromPool(tagObj[2], new Vector3(Random.Range(-8, 8), 0, transform.position.z)); 
        }
        else
        {
            for (int i = 0; i < 3; i++) {
                objectPooler.SpawnFromPool(tagObj[Random.Range(0, 2)], new Vector3(Random.Range(-8, 8), 0, transform.position.z));
            }
        } 
    }
}
