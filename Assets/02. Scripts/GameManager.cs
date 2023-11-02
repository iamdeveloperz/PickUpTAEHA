
using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    public bool IsVictory { get { return isVictory; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    public int CurrentScore { get { return currentScore; } set { currentScore = value; } }
    public int BestScore { get { return bestScore; } set { bestScore = value; } }
    public GameObject Cards { get { return cards; } }
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
    private GameObject gamevictoryPanel;
    public GameObject gameEndBG;
    private TMP_Text tryTxt;

    private GameObject minusCount;

    public const float lerpTimeValue = 0.7f;
    private bool isAlive = true;
    private int currentScore;   // ���� ����
    private int bestScore;       // �ְ� ����

    // ���� ���۽� ī�� ��ġ �ִϸ��̼��� �������� �Ǵ��ϴ� ��
    public bool startAnim = false;

    // ���� ���� ����
    public int cardScore;     // ī�� ��ġ �� ���� ����
    public const int timeMultiple = 3;  // ���� �ð� ���� ȯ��� ���� ���
    public int difficultyBasicScore;      // ���̵��� �⺻ ����

    #endregion

    #region Unity Methods
    #endregion

    #region Main Methods
        public void GameOver()
    {
        isAlive = false;
        savedScore();
        gameEndBG.SetActive(true);
        StartCoroutine(OnGameOverPanel());
        SoundManager.Instance.GameLose();
        gameTryCount = 0;
        //Time.timeScale = 0f;
    }

    public void GameVictory()
    {
        isAlive = false;
        isVictory = false;
        this.VictoryScoreCalculate();
        savedScore();
        gameEndBG.SetActive(true);
        SoundManager.Instance.GameWin();
        StartCoroutine(OnGameVictoryPanel());
    }

    public void CardMatched()
    {
        string firstCardImage = firstCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;

        // ī�� ��ġ ������
        if (firstCardImage == secondCardImage)
        {

            firstCard.GetComponent<MemberCard>().DestroyCard();
            secondCard.GetComponent<MemberCard>().DestroyCard();
            SoundManager.Instance.MatchSuccess();   // ��Ī ���� ����

            // ī�� �Ǻ��� ���������� ������ ���� 4�� ���� ���� ����
            if (int.Parse(secondCardImage.Substring(secondCardImage.Length - 1)) % 5 == 4 &&
                int.Parse(firstCardImage.Substring(firstCardImage.Length - 1)) % 5 == 4)
            {
                GameOver();
            }
            else // �ƴ� ��� ���� �߰�
            {
                Instance.CurrentScore += cardScore;
            }

            StartCoroutine(IsVictoryGame());
        }
        // ī�尡 ��ġ�� �ȵ��� ��
        else
        {
            firstCard.GetComponent<MemberCard>().CloseCard();
            secondCard.GetComponent<MemberCard>().CloseCard();
        }

        firstCard = null;
        secondCard = null;

        // ī�� ��ġ �õ� Ƚ��
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

    // �¸� �� �޴� ���� ��
    private void VictoryScoreCalculate()
    {
        int timeScore = (int)Instance.GameTime * timeMultiple;
        int tryScore = difficultyBasicScore - gameTryCount;

        currentScore += timeScore + tryScore;
        gameTryCount = 0;
    }
    public void savedScore()
    {
        if (PlayerPrefs.HasKey("bestScore") == false)
            PlayerPrefs.SetInt("bestScore", currentScore);
        else
        {
            if (PlayerPrefs.GetInt("bestScore") < currentScore)
            {
                PlayerPrefs.SetInt("bestScore", currentScore);
            }
        }
    }

    public void loadScore()
    {
        if (PlayerPrefs.HasKey("bestScore") == false)
            PlayerPrefs.SetInt("bestScore", 0);
        else
            bestScore = PlayerPrefs.GetInt("bestScore");
    }

    //�ð� ���� �ִϸŴϼ� �۵�����
    public void Minus()
    {
        Transform parents = GameObject.Find("TimerFrame").transform;
        GameObject minusTxt = Instantiate(minusCount, parents);
        Destroy(minusTxt, 1.0f);
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

    public void SettingGameVictoryPanel(GameObject victoryPanel)
    {
        if (gamevictoryPanel == null)
            gamevictoryPanel = victoryPanel;
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
        TMP_Text curScore = gameoverPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text bstScore = gameoverPanel.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();

        loadScore();

        curScore.text = currentScore.ToString();
        bstScore.text = bestScore.ToString();

        yield return new WaitForSeconds(lerpTimeValue);

        gameoverPanel.SetActive(true);
    }

    private IEnumerator OnGameVictoryPanel()
    {
        TMP_Text curScore = gamevictoryPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text bstScore = gamevictoryPanel.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();

        loadScore();

        curScore.text = currentScore.ToString();
        bstScore.text = bestScore.ToString();

        yield return new WaitForSeconds(lerpTimeValue);

        gamevictoryPanel.SetActive(true);
    }

    public void MainStageScoreTextUpdate(TMP_Text curSText, TMP_Text bestSText)
    {
        if (this.bestScore < currentScore)
        {
            bestSText.text = currentScore.ToString();
            curSText.text = currentScore.ToString();
        }
        else
        {
            bestSText.text = bestScore.ToString();
            curSText.text = currentScore.ToString();
        }
    }

    public void MinusCount(GameObject minusCountPrefab)
    {
        if(minusCount == null)
        {
            minusCount = minusCountPrefab;
        }
    }
    #endregion
}