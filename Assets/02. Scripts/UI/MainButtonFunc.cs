using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonFunc : MonoBehaviour
{
    public void GoToTitleButton()
    {
        SceneManager.LoadScene(0);
    }
}