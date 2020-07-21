using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public static LevelManager _LevelManager;

    [SerializeField] private int stepsToCreateMoreLanes = 15;
    private int steps, coins;
    private int currentSteps;

    public Text StepText, StepRecordText, CoinsText;
    
    private void Awake()
    {
        _LevelManager = this;
    }

    private void Start()
    {
        StepRecordText.text = "<size=60>TOP:</size> " + PlayerPrefs.GetInt("score");
        coins = PlayerPrefs.GetInt("coins");
        CoinsText.text = PlayerPrefs.GetInt("coins")+"";
        
    }

    public void SetSteps()
    {
        steps++;
        StepText.text = steps.ToString();
    }

    public void SetCoins()
    {
        coins++;
        PlayerPrefs.SetInt("coins", coins);
        CoinsText.text = PlayerPrefs.GetInt("coins")+"";
    }
    
    public void LMGameOver()
    {
        if (PlayerPrefs.GetInt("score") < steps)
        {
            PlayerPrefs.SetInt("score", steps);
            StepRecordText.text = "<size=60>TOP:</size> " + PlayerPrefs.GetInt("score");
        }
    }

}
