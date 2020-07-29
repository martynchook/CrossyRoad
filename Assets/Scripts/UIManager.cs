using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager _UIManager;

    [SerializeField] private RectTransform StartUI, GameUI, GameOverUI;
    
    private float durationMoveUI = 0.5f;
    private Vector3 posShowUI = new Vector3(0f, 0f ,0f);
    private Vector3 posHideUI = new Vector3(-1100f, 0f ,0f);
    
    private void Awake()
    {
        _UIManager = this;
    }

    private void Start()
    {
        ShowStartUI();
    }

    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HideStartUI();
            ShowGameUI();
        }
    }

    private void ShowStartUI()
    {
        StartUI.DOAnchorPos3D(posShowUI, durationMoveUI);
    }

    private void HideStartUI()
    {
        StartUI.DOAnchorPos3D (posHideUI,  durationMoveUI);
    }

    private void ShowGameUI()
    {
        GameUI.DOAnchorPos3D (posShowUI, durationMoveUI);
    }

    private void ShowGameOverUI()
    {
        GameOverUI.DOAnchorPos3D (posShowUI, durationMoveUI);
    }

    public void UIGameOver()
    {
        ShowGameOverUI();
    }
}
