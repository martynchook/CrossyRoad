using UnityEngine;

public class LanesCount : MonoBehaviour
{
    [SerializeField] private int countLanes;

    public int GetCountLanes()
    {
        return countLanes;
    }
}
