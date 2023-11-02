using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonFunc : MonoBehaviour
{
    public GameObject popupUI;

    public void GoToTitleButton()
    {
        SceneManager.LoadScene(0);
        SoundManager.Instance.ButtonClick();
        SoundManager.Instance.GoTitle();
    }

    public void MainGameConfirmButton()
    {
        popupUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
}