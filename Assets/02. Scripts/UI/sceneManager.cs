
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    //[SerializeField] private GameObject gameoverPanel;

    public void Retry()
    {
        GameManager.Instance.GameoverPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void GoTitle()
    {
        GameManager.Instance.GameoverPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
