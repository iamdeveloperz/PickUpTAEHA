using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public TMP_Text timeTxt;
    float time = 30.00f;

    #region Member Variables

    [SerializeField] private GameObject cardPrefab;
    private Sprite[] cardImages = null;

    private DIFFICULTY diff = DIFFICULTY.EASY;

    private float cardScale = 1f;          // 카드 스케일
    private int widthNumber;        // 카드 가로 개수
    private int heightNumber;       // 카드 세로 개수

    private const float cardBlank = 1.05f;      // 카드 간격
    Vector2 cardCenterValue = new Vector2(-1.58f, -2f); // 카드 센터 조정 값
    #endregion

    #region Unity Methods
    private void Awake()
    {
        // 리소스 매니저를 통해 스프라이트 불러올 예정
        //cardImages = Resources.Load<Sprite>("CardImage0")

    }

    private void Start()
    // Update is called once per frame
    this.Initalized();
    }
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
    }
    #endregion

    #region Main Methods
    private void Initalized()
    {
        Transform parents = GameObject.Find("Cards").transform;

        switch (diff)
        {
            case DIFFICULTY.EASY:
                widthNumber = heightNumber = 4;
                break;
            case DIFFICULTY.NORMAL:
                widthNumber = 4;
                heightNumber = 5;
                cardCenterValue = new Vector2(-1.58f, -2.5f);
                break;
            case DIFFICULTY.HARD:
                // cardScale 줄여야함
                widthNumber = 5;
                heightNumber = 6;
                break;
        
        }

        // 카드 생성
        this.CreateCard(parents);
    }

    private void CreateCard(Transform parents)
    {
        for (int i = 0; i < widthNumber * heightNumber; ++i)
        {
            GameObject card = Instantiate(cardPrefab);
            card.transform.parent = parents;

            float x = (i % widthNumber) * cardBlank;
            float y = (i / heightNumber) * cardBlank;

            card.transform.position = new Vector3(x, y, 0);
            card.transform.localScale = new Vector3(cardScale, cardScale, 0);
        }

        parents.position = cardCenterValue;
    }
    #endregion
}