using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBase<SoundManager>
{
    /*
    [System.Serializable]
    public struct BgmType
    {
        public string name;
        public AudioClip audio;
    }
    public BgmType[] BGMList;
    */

    private AudioSource BGM;
    private AudioSource audioSource;

    //private string NowBGMname = "";

    public AudioClip openCard;
    public AudioClip matchSuccess;
    public AudioClip buttonClick;
    public AudioClip gameWin;
    public AudioClip gameLose;
    public AudioClip titleMusic;
    public AudioClip mainMusic;

    public bool SoundCheck = true;

    public void Initialize()
    {
        openCard = Resources.Load<AudioClip>("Sounds/OpenCard");
        matchSuccess = Resources.Load<AudioClip>("Sounds/MatchSuccess");
        buttonClick = Resources.Load<AudioClip>("Sounds/ButtonClick");
        gameWin = Resources.Load<AudioClip>("Sounds/GameWin");
        gameLose = Resources.Load<AudioClip>("Sounds/GameLose");
        titleMusic = Resources.Load<AudioClip>("Sounds/TitleMusic");
        mainMusic = Resources.Load<AudioClip>("Sounds/MainMusic");

        if ( audioSource == null && BGM == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            BGM = gameObject.AddComponent<AudioSource>();
        }
        BGM.loop = true;
        BGM.volume = 0.2f;
        audioSource.volume = 0.3f;
        audioSource.playOnAwake = false;

        GoTitle();
        //if (BGMList.Length > 0) PlayBGM(BGMList[0].name);
    }
    public void OpenCard()
    {
        audioSource.PlayOneShot(openCard);
    }
    public void MatchSuccess()
    {
        audioSource.PlayOneShot(matchSuccess);
    }
    public void ButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }
    public void GameWin()
    {
        BGM.Stop();
        audioSource.PlayOneShot(gameWin);
    }
    public void GameLose()
    {
        BGM.Stop();
        audioSource.PlayOneShot(gameLose);
    }
    public void GoMain()
    {
        //PlayBGM(BGMList[1].name);
        BGM.clip = mainMusic;
        BGM.Play();
    }
    public void GoTitle()
    {
        //PlayBGM(BGMList[0].name);
        BGM.clip = titleMusic;
        BGM.Play();
    }

    /*
    public void PlayBGM(string name)
    {
        if (NowBGMname.Equals(name)) return;

        for (int i = 0; i < BGMList.Length; ++i)
            if (BGMList[i].name.Equals(name))
            {
                BGM.clip = BGMList[i].audio;
                BGM.Play();
                NowBGMname = name;
            }
    }
    */
}
