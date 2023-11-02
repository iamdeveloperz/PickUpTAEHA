using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class JokerGenerate : MonoBehaviour
{
    private Image[] jokerImages;
    public Sprite[] jokerSprites;
    private GameManager.DIFFICULTY diff;

    private void Start()
    {
        diff = GameManager.Instance.Difficulty;
        jokerImages = new Image[transform.childCount + 1];

        for(int i = 0; i < transform.childCount; ++i)
            jokerImages[i] = transform.GetChild(i).GetComponent<Image>();
        
        for(int i = 0; i <= (int)diff; ++i)
        {
            jokerImages[i].gameObject.SetActive(true);
            jokerImages[i].sprite = jokerSprites[i];
        }
    }
}