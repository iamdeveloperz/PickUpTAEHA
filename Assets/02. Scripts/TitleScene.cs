using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    private void Awake()
    {
        SoundManager.Instance.Initialize();
    }
}
