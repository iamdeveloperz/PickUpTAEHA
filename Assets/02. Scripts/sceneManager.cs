using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    [SerializeField] private GameObject gameoverPanel;

    public void Retry()
    {
        gameoverPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void GoTitle()
    {
        gameoverPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
