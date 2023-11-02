
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    //[SerializeField] private GameObject gameoverPanel;

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(0);
    }
}
