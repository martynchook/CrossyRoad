using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CanvasButtons : MonoBehaviour
{
    [SerializeField] private Sprite musicOn, musicOff;
    [SerializeField] private Image imgLogo;

    private float yPosMoveLogo = 1f;
    private float durationMoveLogo = 1f;

    private void Start()
    {
        Sequence moveLogo = DOTween.Sequence();
		moveLogo.Append(imgLogo.rectTransform.DOMoveY(yPosMoveLogo, durationMoveLogo).SetRelative().SetEase(Ease.InOutQuad));
		moveLogo.SetLoops(-1, LoopType.Yoyo);
        if (PlayerPrefs.GetString("music") == "No" && gameObject.name == "btnVolume")
            GetComponent<Image>().sprite = musicOff;
        else
            GetComponent<Image>().sprite = musicOn;
    }

    private void RestartGame()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void MusicWork()
    {
        if (PlayerPrefs.GetString("music") == "No")
        {
            GetComponent<AudioSource>().Play();
            PlayerPrefs.SetString("music", "Yes");
            GetComponent<Image>().sprite = musicOn;
        }
        else 
        {
            PlayerPrefs.SetString("music", "No");
            GetComponent<Image>().sprite = musicOff;
        }
    }
}
 