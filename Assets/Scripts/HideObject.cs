using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    [SerializeField] private float xEndPos;

    private void Update()
    {
        if (gameObject.transform.position.x ==  xEndPos)
        {
            gameObject.SetActive(false);
        }
    }
}
