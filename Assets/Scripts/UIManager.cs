using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager UiManager;
    public RectTransform StartUI, GameUI, GameOverUI;
    
    private void Awake()
    {
         UiManager = this;
    }

    void Start()
    {
        StartUI.DOAnchorPos3D (new Vector3 (0,0,0), 0.5f);
    }

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartUI.DOAnchorPos3D (new Vector3 (-1100,0,0),  0.5f);
            GameUI.DOAnchorPos3D (new Vector3 (0,0,0), 0.5f);
        }
    }

    public void UIGameOver ()
    {
        GameOverUI.DOAnchorPos3D (new Vector3 (0,0,0), 0.5f);
    }

}
