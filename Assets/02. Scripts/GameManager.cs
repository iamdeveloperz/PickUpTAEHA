
using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : SingletonMonoBase<GameManager>
{
    #region Enum Value
    public enum DIFFICULTY
    {
        EASY,
        NORMAL,
        HARD
    }
    #endregion

    #region Properties
    public float GameTime { get { return gameTime; } set { gameTime = value; } }
    public DIFFICULTY Difficulty { get { return diff; } set { diff = value; } }
    public GameObject GameoverPanel { get { return gameoverPanel; } }
    public bool IsVictory { get { return isVictory; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    #endregion

    #region Member Variables

    [SerializeField] private GameObject cards;
    private bool isVictory = false;

    private int gameTryCount = 0;
    private float gameTime;
    private DIFFICULTY diff = DIFFICULTY.EASY;

    public GameObject firstCard;
    public GameObject secondCard;

    private GameObject gameoverPanel;
    private TMP_Text tryTxt;

    public const float lerpTimeValue = 0.7f;
    private bool isAlive = true;

    #endregion

    #region Unity Methods
    #endregion

    #region Main Methods
    public void GameOver()
    {
        isAlive = false;
        StartCoroutine(OnGameOverPanel());
        if (SoundManager.Instance.SoundCheck == true) SoundManager.Instance.GameLose();
        else if (SoundManager.Instance.SoundCheck == false) SoundManager.Instance.GameWin();
        gameTryCount = 0;
        //Time.timeScale = 0f;
    }

    public void GameVictory()
    {
        SoundManager.Instance.SoundCheck = false;
        isAlive = false;
        isVictory = false;
        this.GameOver();
    }

    public void CardMatched()
    {
        string firstCardImage = firstCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;

        // 카드 매치 됐을시
        if (firstCardImage == secondCardImage)
        {
            firstCard.GetComponent<MemberCard>().DestroyCard();
            secondCard.GetComponent<MemberCard>().DestroyCard();
            SoundManager.Instance.MatchSuccess();   // 매칭 성공 사운드

            // 카드 판별과 마찬가지로 나머지 값이 4일 때만 게임 종료
            if (int.Parse(secondCardImage.Substring(secondCardImage.Length - 1)) % 5 == 4 &&
                int.Parse(firstCardImage.Substring(firstCardImage.Length - 1)) % 5 == 4)
            {
                GameOver();
            }

            StartCoroutine(IsVictoryGame());
        }
        // 카드가 매치가 안됐을 시
        else
        {
            firstCard.GetComponent<MemberCard>().CloseCard();
            secondCard.GetComponent<MemberCard>().CloseCard();
        }

        firstCard = null;
        secondCard = null;

        // 카드 매치 시도 횟수
        this.TryTextUpdate();
    }

    private IEnumerator IsVictoryGame()
    {
        yield return new WaitForSeconds(lerpTimeValue);

        int childCount = cards.transform.childCount;

        switch(diff)
        {
            case DIFFICULTY.EASY:
                if (childCount == 2)
                    isVictory = true;
                break;
            case DIFFICULTY.NORMAL:
                if (childCount == 4)
                    isVictory = true;
                break;
            case DIFFICULTY.HARD:
                if (childCount == 6)
                    isVictory = true;
                break;
        }

        if(isVictory)
            GameVictory();
    }
    #endregion

    #region Sub Methods
    public void SettingCards(GameObject cards)
    {
        if(cards != null)
            this.cards = cards;
    }
    private void TryTextUpdate()
    {
        ++gameTryCount;
        tryTxt.text = "TRY : " + gameTryCount.ToString();
    }

    public void SettingGameOverPanel(GameObject overPanel)
    {
        if (gameoverPanel == null)
            gameoverPanel = overPanel;
    }

    public void SettingTryText(TMP_Text tryText)
    {
        if (tryTxt == null)
            tryTxt = tryText;
    }

    public void SettingTimeScale(bool isRun = true)
    {
        Time.timeScale = isRun ? 1f : 0f;
    }

    private IEnumerator OnGameOverPanel()
    {
        yield return new WaitForSeconds(lerpTimeValue);

        gameoverPanel.SetActive(true);
    }
    #endregion
}