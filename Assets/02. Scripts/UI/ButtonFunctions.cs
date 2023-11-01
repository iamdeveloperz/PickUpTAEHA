using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    public GameObject popupUI;

    private void Start()
    {
        GameManager.Instance.SettingTimeScale(true);
        this.SettingDifficultyButtons();
    }

    #region Title Buttons
    public void ExitButton()
    {
        Application.Quit();
    }

    public void PopupOnButton()
    {
        popupUI.SetActive(true);
    }
    #endregion

    #region Popup Buttons
    public void PopupExitButton()
    {
        popupUI.SetActive(false);
    }
    #endregion

    public void SettingDifficultyButtons()
    {
        Transform stageBg = popupUI.transform.Find("SelectStageBG").transform;

        for(int i = 0; i < 3; ++i)
        {
            string btnName = "Btn_Diff" + (i + 1).ToString();
            var btn = stageBg.Find(btnName);
            int index = i;
            btn.GetComponent<Button>().onClick.AddListener(() => OnLoadDifficulty(index));
        }
    }

    public void OnLoadDifficulty(int level)
    {
        switch(level)
        {
            // EASY
            case 0:
                GameManager.Instance.Difficulty = GameManager.DIFFICULTY.EASY;
                break;
                // Normal
            case 1:
                GameManager.Instance.Difficulty = GameManager.DIFFICULTY.NORMAL;
                break;
                // Hard
            case 2:
                GameManager.Instance.Difficulty = GameManager.DIFFICULTY.HARD;
                break;
        }

        SceneManager.LoadScene(1);
    }
}