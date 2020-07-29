using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private int minDistanseFromPlayer;
    [SerializeField] private int maxTerrainCount;
    [SerializeField] private List<GameObject> terrains = new List<GameObject>();
    
    private List<GameObject> currentTerrain = new List<GameObject>();
    private Vector3 CurrentPosition;
    private int zPositionAdd;
 
    private void Start()
    {
        CurrentPosition = new Vector3(0, 0, 2);
        for (int i = 0; i < maxTerrainCount; i++)
            SpawnTerrain(true, new Vector3(0,0,0));
        maxTerrainCount = currentTerrain.Count;
    }

    public void SpawnTerrain(bool _isStart, Vector3 playerPos)
    {
        if (((CurrentPosition.z - playerPos.z) < minDistanseFromPlayer) || _isStart)
        {
            int whichTerrain = Random.Range(0, terrains.Count);
            GameObject terrain = Instantiate(terrains[whichTerrain], CurrentPosition, Quaternion.identity);
            terrain.transform.SetParent(gameObject.transform);
            currentTerrain.Add(terrain);
            zPositionAdd = terrain.GetComponent<LanesCount>().GetCountLanes();
            if (!_isStart)
            {
                if (currentTerrain.Count > maxTerrainCount)
                {
                    Destroy(currentTerrain[0]);
                    currentTerrain.RemoveAt(0);
                }
            }
            CurrentPosition.z += zPositionAdd;
        }
    }
}




























// public class LevelCreator : MonoBehaviour
// {
    
//     public GameObject[] lanes; // массив объектов для создания

//     private GameObject tempLane;

//     private int zPosition = 2;    

//     void Start()
//     {
//         CreateLanes();
//     }

//     // Создаем карту до позиции 20 по оси Z (идем от 2 до 20 и добавляем переменную LanesCount из отдельного скрипта который висит на префабах карты) 
//     public void CreateLanes ()
//     {
//         int i;
//         for ( i = zPosition; i < zPosition + 30; i++)
//         {
//             tempLane = Instantiate(lanes[Random.Range(0, lanes.Length)], new Vector3 (0, 0, i), Quaternion.identity) as GameObject;
//             tempLane.transform.SetParent(gameObject.transform);
        
//             i += tempLane.GetComponent<LanesCount>().countOfLanes - 1;
//         }
//         zPosition = i;
//     }
// }
