using System.Linq;
using TMPro;
using UnityEngine;
using static GameManager;

public class MainGame : MonoBehaviour
{
    #region Member Variables

    [SerializeField] private GameObject cardPrefab;

    private float cardScale = 1f;            // ī�� ������
    private int widthNumber;                // ī�� ���� ����
    private int heightNumber;               // ī�� ���� ����
    private const float cardBlank = 1.05f;      // ī�� ����
    Vector2 cardCenterValue = new Vector2(-1.58f, -2f);     // ī�� ���� ���� ��

    public GameObject gameoverPanel;   // ���� ������ ���� �ǳ�

    [SerializeField] private TMP_Text timeTxt;
    [SerializeField] private TMP_Text tryTxt;

    #endregion

    #region Unity Methods
    private void Start()
    {
        Time.timeScale = 1f;    // ���� ����

        this.Initalized();
    }

    private void Update()
    {
        GameManager.Instance.GameTime -= Time.deltaTime;
        timeTxt.text = GameManager.Instance.GameTime.ToString("N2");

        if (GameManager.Instance.GameTime <= 0)
        {
            GameManager.Instance.GameOver();
            timeTxt.text = GameManager.Instance.GameTime.ToString("N2");
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
                GameManager.Instance.GameTime = 60f;
                break;
            case DIFFICULTY.NORMAL:
                widthNumber = 4;
                heightNumber = 5;
                cardCenterValue = new Vector2(-1.58f, -2.5f);
                GameManager.Instance.GameTime = 40f;
                break;
            case DIFFICULTY.HARD:
                // cardScale �ٿ�����
                widthNumber = 5;
                heightNumber = 6;
                cardCenterValue = new Vector2(-1.78f, -3.2f);
                cardScale = 0.85f;
                GameManager.Instance.GameTime = 50f;
                break;
        }

        // ���ӸŴ����� �˾ƾ� �� ���� ����
        GameManager.Instance.SettingCards(parents.gameObject);
        GameManager.Instance.SettingGameOverPanel(gameoverPanel);
        GameManager.Instance.SettingTryText(tryTxt);

        // ī�� ����
        this.CreateCard(parents);
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

            string cardName = "CardImage" + CardImages[i].ToString();
            card.transform.name = cardName;
            card.transform.Find("Front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
        }

        parents.position = cardCenterValue;
        parents.localScale = new Vector3(cardScale, cardScale, 0);
    }
    #endregion
}