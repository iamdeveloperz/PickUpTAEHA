
using TMPro;
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
    public DIFFICULTY Difficulty { get { return diff; } }
    public int GameTryCount { get { return gameTryCount; } set { gameTryCount = value; } }
    public GameObject GameoverPanel { get { return gameoverPanel; } }
    #endregion

    #region Member Variables

    private int gameTryCount = 0;
    private float gameTime;
    private DIFFICULTY diff = DIFFICULTY.EASY;

    public GameObject firstCard;
    public GameObject secondCard;

    private GameObject gameoverPanel;
    private TMP_Text tryTxt;

    #endregion

    #region Unity Methods
    #endregion

    #region Main Methods
    public void GameOver()
    {
        gameoverPanel.SetActive(true);
        gameTime = 0f;
        gameTryCount = 0;
        Time.timeScale = 0f;
    }

    public void CardMatched()
    {
        string firstCardImage = firstCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {

            firstCard.GetComponent<MemberCard>().DestroyCard();
            secondCard.GetComponent<MemberCard>().DestroyCard();

            //카드 판별과 마찬가지로 나머지 값이 4일 때만 게임 종료
            if (int.Parse(secondCardImage.Substring(secondCardImage.Length - 1)) % 5 == 4 &&
                int.Parse(firstCardImage.Substring(firstCardImage.Length - 1)) % 5 == 4)
            {
                GameManager.Instance.GameOver();
            }
        }
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
    #endregion

    #region Sub Methods
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
    #endregion
}