using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static GameManager;

public class MainGame : MonoBehaviour
{
    #region Member Variables

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject minusCountPrefab;

    public float TimeLimit = 3f;

    private float cardScale = 1f;            // 카드 스케일
    private int widthNumber;                // 카드 가로 개수
    private int heightNumber;               // 카드 세로 개수
    private const float cardBlank = 1.05f;      // 카드 간격
    Vector2 cardCenterValue = new Vector2(-1.58f, -2f);     // 카드 센터 조정 값

    public GameObject gameoverPanel;   // 게임 오버시 나올 판넬
    public GameObject gamevictoryPanel;
    public GameObject gameEndBG;

    [SerializeField] private TMP_Text timeTxt;
    [SerializeField] private TMP_Text tryTxt;

    [SerializeField] private TMP_Text currentScoreTxt;
    [SerializeField] private TMP_Text bestScoreTxt;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        // 팝업 확인을 누르기 전까지 TimeScale 조절
        Time.timeScale = 0f;
    }

    private void Start()
    {
        //Time.timeScale = 1f;    // 게임 시작

        if (!GameManager.Instance.IsAlive)
            GameManager.Instance.IsAlive = true;

        GameManager.Instance.MinusCount(minusCountPrefab);

        this.Initalized();
    }

    private void Update()
    {
        //시작 애니메니션 로딩이 끝났으면 타이머 시작
        if (GameManager.Instance.startAnim)
        {
            if (GameManager.Instance.IsAlive)
            {
                GameManager.Instance.GameTime -= Time.deltaTime;
                timeTxt.text = GameManager.Instance.GameTime.ToString("N2");
                if (GameManager.Instance.GameTime <= 0)
                {
                    GameManager.Instance.GameTime = 0f;
                    GameManager.Instance.GameOver();
                    timeTxt.text = GameManager.Instance.GameTime.ToString("N2");
                }
            }
        }

        // 첫 카드 고르고 3초 지날 시 다시 덮기
        if (GameManager.Instance.firstCard != null)
        {
            TimeLimit -= Time.deltaTime;
        } else
        {
            TimeLimit = 3f;
        }

        if (GameManager.Instance.firstCard != null && TimeLimit <= 0 && GameManager.Instance.secondCard == null)
        {
            GameManager.Instance.firstCard.GetComponent<MemberCard>().CloseCard();
            GameManager.Instance.firstCard = null;
        }    
    }
    #endregion

    #region Main Methods
    private void Initalized()
    {
        Transform parents = GameObject.Find("Cards").transform;

        switch (GameManager.Instance.Difficulty)
        {
            case DIFFICULTY.EASY:
                widthNumber = heightNumber = 4;
                GameManager.Instance.GameTime = 40f;
                GameManager.Instance.difficultyBasicScore = 60;
                GameManager.Instance.cardScore = 10;
                break;
            case DIFFICULTY.NORMAL:
                widthNumber = 4;
                heightNumber = 5;
                cardCenterValue = new Vector2(-1.58f, -3f);
                GameManager.Instance.GameTime = 50f;
                GameManager.Instance.difficultyBasicScore = 80;
                GameManager.Instance.cardScore = 15;
                break;
            case DIFFICULTY.HARD:
                // cardScale 줄여야함
                widthNumber = 5;
                heightNumber = 6;
                cardCenterValue = new Vector2(-1.78f, -3.2f);
                cardScale = 0.85f;
                GameManager.Instance.GameTime = 90f;
                GameManager.Instance.difficultyBasicScore = 120;
                GameManager.Instance.cardScore = 18;
                break;
        }

        // 스테이지에 존재하는 스코어 텍스트
        this.UITextSetting();

        // 게임매니저가 알아야 될 정보 세팅
        this.InitGameManagerSetting(parents);

        // 카드 생성
        this.CreateCard(parents);
    }

    private void UITextSetting()
    {
        GameObject scoreStandard = GameObject.Find("ScoreStandard");
        Transform currScoreUI = scoreStandard.transform.Find("CurrScore").Find("Standard");
        Transform bestScoreUI = scoreStandard.transform.Find("BestScore").Find("Standard");

        currentScoreTxt = currScoreUI.GetChild(0).GetComponent<TMP_Text>();
        bestScoreTxt = bestScoreUI.GetChild(0).GetComponent<TMP_Text>();

        bestScoreTxt.text = GameManager.Instance.BestScore.ToString();
    }

    private void InitGameManagerSetting(Transform parents)
    {
        // 시작 전 점수 세팅
        GameManager.Instance.CurrentScore = 0;
        GameManager.Instance.loadScore();

        GameManager.Instance.gameEndBG = gameEndBG;

        GameManager.Instance.SettingCards(parents.gameObject);
        GameManager.Instance.SettingGameOverPanel(gameoverPanel);
        GameManager.Instance.SettingGameVictoryPanel(gamevictoryPanel);
        GameManager.Instance.SettingTryText(tryTxt);
    }

    private void CreateCard(Transform parents)
    {
        int[] CardImages = new int[widthNumber * heightNumber];

        for (int i = 0; i < widthNumber * heightNumber; ++i)
            CardImages[i] = i / 2;

        CardImages = CardImages.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < widthNumber * heightNumber; ++i)
        {
            GameObject card = Instantiate(cardPrefab);
            card.transform.parent = parents;

            float x = (i % widthNumber) * cardBlank;
            float y = (i / widthNumber) * cardBlank;

            card.transform.position = new Vector3(x, y, 0);

            string cardName ="CardImage" + CardImages[i].ToString("D2");
            
            card.transform.name = cardName;
            card.transform.Find("Front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
        }

        parents.position = cardCenterValue;
        parents.localScale = new Vector3(cardScale, cardScale, 0);
    } 
    #endregion

    #region Sub Methods
    public void ScoreTextUpdate()
    {
        GameManager.Instance.MainStageScoreTextUpdate(currentScoreTxt, bestScoreTxt);
    }
    #endregion
}
