using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
	public static CameraFollow cameraFollow;

	[SerializeField] private Transform player;
	[HideInInspector] public bool isGameStart, isLose;

    private Vector3 followPoint;
	private Vector3 targetLastPos;
	private Tweener tween;
	private float zPos = 100f;

	public GameObject KillerObject;

	private void Awake()
    {
        cameraFollow = this;
    }
	

	private void Start()
	{
		followPoint = player.position + new Vector3 (0, 6, -1);
		tween = transform.DOMove(followPoint, 350).SetAutoKill(false);
	}

	private void Update()
	{	
		if (isGameStart)
		{
			CameraMovment(isLose);
		}
	}

	public bool GameOverTrigger ()
	{
		if (player.position.z  + 3f < gameObject.transform.position.z)
		{
			KillerObject = Instantiate(KillerObject, new Vector3 (player.position.x, 20f, player.position.z), Quaternion.identity);
			KillerObject.transform.DOMoveY (0f, 0.1f, false);		
			isLose = true;
			return true;
		}
		return false;
	}

	private void CameraMovment (bool isLose)
	{
		if (!isLose)
		{
			followPoint = player.position + new Vector3 (0, 6, zPos);
			transform.DOMoveX(player.position.x, 1).SetAutoKill(false);
			if (player.position.z  > gameObject.transform.position.z + 2f)
        	{
        		transform.DOMoveZ(player.position.z, 1).SetAutoKill(false);   
			}
			tween.ChangeEndValue(followPoint, true).Restart();
		}
		else
		{
			tween.ChangeEndValue(gameObject.transform.position, true).Restart();
		}	
	}


}
