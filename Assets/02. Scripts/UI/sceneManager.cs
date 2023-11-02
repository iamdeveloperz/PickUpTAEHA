
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    //[SerializeField] private GameObject gameoverPanel;

    public void Retry()
    {
        SceneManager.LoadScene(1);
        SoundManager.Instance.ButtonClick();
        SoundManager.Instance.GoMain();
        SoundManager.Instance.SoundCheck = true;
        GameManager.Instance.startAnim = false;
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(0);
        SoundManager.Instance.ButtonClick();
        SoundManager.Instance.GoTitle();
    }
}
