using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LevelCreator levelCreator;
    [SerializeField] private AudioSource BackGroundAudio, JumpAudio, JumpOnWoodAudio, CoinAudio;
    [SerializeField] private float durationMove = 0.15f;
    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private GameObject playerDead;

    private Rigidbody rb;
    private float normalScale = 0.5f;
    private float prepJumpScale = 0.4f;
    private int countJumps = 1;
    private Vector3 startPos, endPos;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (PlayerPrefs.GetString("music") == "Yes")
        BackGroundAudio.Play();
    }
    
    private void Update()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            GameStart();
        if (Input.GetKeyUp(KeyCode.UpArrow))
            posToMove(Vector3.forward, 0);
		else if (Input.GetKeyUp(KeyCode.DownArrow))
            posToMove(Vector3.back, 180);
		else if (Input.GetKeyUp(KeyCode.RightArrow))
            posToMove(Vector3.right, 90);
		else if (Input.GetKeyUp(KeyCode.LeftArrow))
            posToMove(Vector3.left, -90);
        levelCreator.SpawnTerrain(false, transform.position);
    }

    private void GameStart()
    {
        transform.DOScaleY(prepJumpScale, durationMove);
        CameraFollow._CameraFollow.SetIsGameStart(true);
        if (PlayerPrefs.GetString("music") == "Yes")
            BackGroundAudio.Stop();
    }

    private void posToMove (Vector3 posToMove, int rotate)
    {
        transform.DOScaleY(normalScale, durationMove);
        if (isGrounded)
        {
            startPos = GetComponent<Transform>().position;
            endPos = gameObject.transform.position + posToMove;
            AdjustPositionAndRotation(new Vector3(0, rotate, 0));
            transform.DOJump(endPos, jumpPower, countJumps, durationMove, false);
        }
    }

    private void AdjustPositionAndRotation(Vector3 newRotation)
    {
        rb.velocity = Vector3.zero;
        transform.eulerAngles = newRotation;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
        if (collision.gameObject.GetComponent<DeathTrigger>() != null)
        {
            GameOver();
            if (PlayerPrefs.GetString("music") == "Yes")
                collision.gameObject.GetComponent<DeathTrigger>().PlayDeathSound();
        }
        if (collision.gameObject.GetComponent<DecorCollision>() != null)
            transform.DOMove(startPos, durationMove, false);
        if (collision.gameObject.GetComponent<PlatformCollision>() != null)
        {
            if (PlayerPrefs.GetString("music") == "Yes")
                JumpOnWoodAudio.Play();
            transform.parent = collision.transform;
        }
        if (collision.gameObject.GetComponent<CoinCollision>() != null)
        {
            collision.gameObject.SetActive(false);
            LevelManager._LevelManager.SetCoins();
            if (PlayerPrefs.GetString("music") == "Yes")
                CoinAudio.Play(); 
        }
        if (collision.gameObject.GetComponent<GroundCollision>() != null)
        {
            if (PlayerPrefs.GetString("music") == "Yes")
                JumpAudio.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DeathTrigger>() != null)
        {
            GameOver();
            if (PlayerPrefs.GetString("music") == "Yes")
                other.gameObject.GetComponent<DeathTrigger>().PlayDeathSound();
        }
        else if (other.gameObject.GetComponent<StepTrigger>() != null)
        {
            LevelManager._LevelManager.SetSteps();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
        if (collision.gameObject.GetComponent<PlatformCollision>() != null)
        {
             this.transform.parent = null;
        }
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if ((collision.gameObject.GetComponent<GroundCollision>() != null) || (collision.gameObject.GetComponent<PlatformCollision>() != null))
        {
            isGrounded = value;
        }
    }

    private void GameOver()
    {
        Instantiate(playerDead, new Vector3(transform.position.x, 0.01f, transform.position.z ), transform.rotation);
        gameObject.SetActive(false);
        CameraFollow._CameraFollow.SetIsLose(true);
        LevelManager._LevelManager.LMGameOver();
        UIManager._UIManager.UIGameOver();
    }
}
