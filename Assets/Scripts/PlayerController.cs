using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LevelCreator levelCreator;

    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private GameObject playerDead;

    private Rigidbody rb;
    private Touch touch;
    private bool isGrounded, isTap;
    private int countJumps = 1;
    private Vector3 startPos, endPos;
    private Vector3 moveLeft = new Vector3 (-1, 0, 0),
                    moveRight = new Vector3 (1, 0, 0),
                    moveUp = new Vector3 (0, 0, 1),
                    moveDown = new Vector3 (0, 0, -1);

    public AudioSource BackGroundAudio, JumpAudio, JumpInWateAudio, DeathAudio, CarDeathAudio, JumpOnWoodAudio, CoinAudio;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (PlayerPrefs.GetString("music") == "Yes")
            BackGroundAudio.Play();
    }

    private void Update()
    {
        MovementLogic();
        if (CameraFollow.cameraFollow.GameOverTrigger())
        {
            if (PlayerPrefs.GetString("music") == "Yes")
                DeathAudio.Play();
            GameOver(); 
        }
    }

    private void MovementLogic()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameStart();   
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
            posToMove(moveUp, 0);	
		else if (Input.GetKeyUp(KeyCode.DownArrow))
            posToMove(moveDown, 180);	
		else if (Input.GetKeyUp(KeyCode.RightArrow))
            posToMove(moveRight, 90);	
		else if (Input.GetKeyUp(KeyCode.LeftArrow))
            posToMove(moveLeft, -90);			
        levelCreator.SpawnTerrain(false, transform.position);
    }

    private void GameStart ()
    {
        transform.DOScaleY(0.4f , duration);
        CameraFollow.cameraFollow.isGameStart = true;
        if (PlayerPrefs.GetString("music") == "Yes")
        BackGroundAudio.Stop(); 
    }

    public void posToMove (Vector3 posToMove, int rotate) 
    {
        transform.DOScaleY(0.5f, duration);

        if (isGrounded)
        {
            startPos = GetComponent<Transform>().position;
            endPos = gameObject.transform.position + posToMove;
            AdjustPositionAndRotation(new Vector3(0, rotate, 0));
            transform.DOJump(endPos, jumpPower, countJumps, duration, false);           
        }
    }

    private void OnMouseDown()
    {
        transform.DOScaleY(0.4f , duration);
        CameraFollow.cameraFollow.isGameStart = true;
    }

    private void AdjustPositionAndRotation (Vector3 newRotation)
    {
        rb.velocity = Vector3.zero;
        transform.eulerAngles = newRotation;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
        if (collision.gameObject.CompareTag("DeathTrigger"))
            GameOver(); 
        if (collision.gameObject.CompareTag("Tree"))
            transform.DOMove(startPos, 0.3f , false);
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Platform Back"))
        {
            if (PlayerPrefs.GetString("music") == "Yes")
                JumpOnWoodAudio.Play();
            transform.parent = collision.transform;
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            LevelManager._LevelManager.SetCoins();
            if (PlayerPrefs.GetString("music") == "Yes")
                CoinAudio.Play(); 
        }
         if (collision.gameObject.CompareTag("Ground"))
        {
            if (PlayerPrefs.GetString("music") == "Yes")
                JumpAudio.Play(); 
        }
    }

    private void OnCollisionExit(Collision collision) 
    {
        IsGroundedUpate(collision, false);

        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Platform Back"))
        {
             this.transform.parent = null;
        }
            
    }

    private void IsGroundedUpate(Collision collision, bool value) // Если переданный тег Ground, то присвоим переданное bool значение переменной _isGrounded = value;
    {
        if (collision.gameObject.tag == ("Ground") || collision.gameObject.tag == ("Platform") || collision.gameObject.tag == ("Platform Back"))
        {
            isGrounded = value;
        }
           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            if (PlayerPrefs.GetString("music") == "Yes")
                JumpInWateAudio.Play(); 
            GameOver();
        }
        if (other.CompareTag("DeathTrigger"))
        {
            GameOver();
        }
        else if (other.CompareTag("Car") || other.CompareTag("Car Back"))
        {
            GameOver();
            if (PlayerPrefs.GetString("music") == "Yes")
                CarDeathAudio.Play();
        }
        else if (other.CompareTag("StepTrigger"))
        {
            LevelManager._LevelManager.SetSteps();
            Destroy(other.gameObject);
        }

    }

    private void GameOver ()
    {
        Instantiate(playerDead, new Vector3(transform.position.x, 0.01f, transform.position.z ), transform.rotation);
        gameObject.SetActive(false);
        LevelManager._LevelManager.LMGameOver();
        UIManager.UiManager.UIGameOver();
    }

}
