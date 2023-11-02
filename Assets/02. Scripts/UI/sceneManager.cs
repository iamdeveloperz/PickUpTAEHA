
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    //[SerializeField] private GameObject gameoverPanel;

    public void Retry()
    {
        GameManager.Instance.GameoverPanel.SetActive(false);
        SceneManager.LoadScene(1);
        SoundManager.Instance.ButtonClick();
        SoundManager.Instance.GoMain();
        SoundManager.Instance.SoundCheck = true;
    }

    public void GoTitle()
    {
        GameManager.Instance.GameoverPanel.SetActive(false);
        SceneManager.LoadScene(0);
        SoundManager.Instance.ButtonClick();
        SoundManager.Instance.GoTitle();
    }
}
