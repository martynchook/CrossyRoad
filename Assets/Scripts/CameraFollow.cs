using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
	public static CameraFollow _CameraFollow;

	[SerializeField] private GameObject killerObject;
	[SerializeField] private Transform player;
	[SerializeField] private Vector3 followPoint;
	
	private Tweener tween;
	private float zPosFollowPoint = 100f; 
	private float durationCameraMovement = 300f;
	private float durationShortTravel = 1f;
	private float durationFallKillerObject = 0.1f;
	private float yPosKillerObjectCreation = 20f;
	private float distanceFromCameraToPlayer = 2f;
	private float distanceLosePlayer = 3f;

	private bool isGameStart, isLose, isKillerObjectCreated;

	private void Awake()
    {
        _CameraFollow = this;
    }
	
	private void Start()
	{
		tween = transform.DOMove(followPoint, durationCameraMovement).SetAutoKill(false);
	}

	private void Update()
	{	
		if (isGameStart)
		{
			CameraMovment(isLose);
			GameOverTrigger(!isKillerObjectCreated);
		}
	}

	private void CameraMovment(bool _isLose)
	{
		if (!_isLose)
		{
			followPoint.z = zPosFollowPoint;
			transform.DOMoveX(player.position.x, durationShortTravel).SetAutoKill(false);
			if (player.position.z > transform.position.z + distanceFromCameraToPlayer)
        		transform.DOMoveZ(player.position.z, durationShortTravel).SetAutoKill(false);   
			tween.ChangeEndValue(followPoint, true).Restart();
		}
		else
		{
			tween.ChangeEndValue(player.transform.position, true).Restart();
		}	
	}

	private void GameOverTrigger(bool _isKillerObjectCreated)
	{
		if (((player.position.z + distanceLosePlayer) < gameObject.transform.position.z) && _isKillerObjectCreated)
		{
			killerObject = Instantiate(killerObject, new Vector3(player.position.x, yPosKillerObjectCreation, player.position.z), Quaternion.identity);
			killerObject.transform.DOMove(player.position, durationFallKillerObject, false);
			isKillerObjectCreated = true;
		}
	}

	public void SetIsLose(bool _isLose)
	{
		isLose = _isLose;
	}

	public void SetIsGameStart(bool _isGameStart)
	{
		isGameStart = _isGameStart;
	}
}
