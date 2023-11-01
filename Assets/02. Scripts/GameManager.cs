using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    #region Properties
    public float GameTime { get { return gameTime; } set { gameTime = value; } }
    #endregion

    #region Member Variables

    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private TMP_Text timeTxt;
    [SerializeField] private TMP_Text tryTxt;

    private float gameTime = 30.00f;
    private int gameTryCount = 0;

    private DIFFICULTY diff = DIFFICULTY.EASY;

    private float cardScale = 1f;          // ī�� ������
    private int widthNumber;        // ī�� ���� ����
    private int heightNumber;       // ī�� ���� ����
    private const float cardBlank = 1.05f;      // ī�� ����
    Vector2 cardCenterValue = new Vector2(-1.58f, -2f); // ī�� ���� ���� ��

    public GameObject firstCard;
    public GameObject secondCard;
    #endregion

    #region Unity Methods
    private void Start() 
    { 
        this.Initalized();
    }
    void Update()
    {
        gameTime -= Time.deltaTime;
        timeTxt.text = gameTime.ToString("N2");
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
                // cardScale �ٿ�����
                widthNumber = 5;
                heightNumber = 6;
                break;
        
        }

        // ī�� ����
        this.CreateCard(parents);
    }

    private void CreateCard(Transform parents)
    {
        int[] CardImages = new int[widthNumber * heightNumber];

        for(int i = 0; i < widthNumber * heightNumber; ++i)
            CardImages[i] = i / 2;

        CardImages = CardImages.OrderBy(item => Random.Range(-1.0f,1.0f)).ToArray();

        for (int i = 0; i < widthNumber * heightNumber; ++i)
        {
            GameObject card = Instantiate(cardPrefab);
            card.transform.parent = parents;

            float x = (i % widthNumber) * cardBlank;
            float y = (i / heightNumber) * cardBlank;

            card.transform.position = new Vector3(x, y, 0);
            card.transform.localScale = new Vector3(cardScale, cardScale, 0);
            
            string cardName = "CardImage" + CardImages[i].ToString();
            card.transform.Find("Front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
        }

        parents.position = cardCenterValue;
    }

    public void cardMatched()
    {
        string firstCardImage = firstCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            
            firstCard.GetComponent<Card>().DestroyCard();
            secondCard.GetComponent<Card>().DestroyCard();

            //ī�� �Ǻ��� ���������� ������ ���� 4�� ���� ���� ����
            if(int.Parse(secondCardImage.Substring(secondCardImage.Length -1))%5 == 4 && 
                int.Parse(firstCardImage.Substring(firstCardImage.Length - 1))%5==4)
            {
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            firstCard.GetComponent<Card>().CloseCard();
            secondCard.GetComponent<Card>().CloseCard();
        }

        firstCard = null;
        secondCard = null;

        // ī�� ��ġ �õ� Ƚ��
        this.TryTextUpdate();
    }

    #endregion

    #region Sub Methods
    private void TryTextUpdate()
    {
        ++gameTryCount;
        tryTxt.text = "TRY : " + gameTryCount.ToString();
    }
    #endregion
}