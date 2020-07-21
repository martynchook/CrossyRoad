using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CanvasButtons : MonoBehaviour
{
    [SerializeField] private Sprite musicOn, musicOff;
    [SerializeField] private Image imgLogo;

    private void Start()
    {
        Sequence s = DOTween.Sequence();
		s.Append(imgLogo.rectTransform.DOMoveY(1, 1f).SetRelative().SetEase(Ease.InOutQuad));
		s.SetLoops(-1, LoopType.Yoyo);

        if (PlayerPrefs.GetString("music") == "No" && gameObject.name == "btnVolume")
        {
            GetComponent<Image>().sprite = musicOff;
        }
        else
        {
            GetComponent<Image>().sprite = musicOn;
        }
    }

    public void RestartGame ()
    {
        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().Play();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MusicWork ()
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
 