using System.Collections;
using System.Collections.Generic;
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

    #region Member Variables

    [SerializeField] private GameObject cardPrefab;
    private Sprite[] cardImages = null;

    private DIFFICULTY diff = DIFFICULTY.EASY;

    private float cardScale = 1f;          // ī�� ������
    private int widthNumber;        // ī�� ���� ����
    private int heightNumber;       // ī�� ���� ����

    private const float cardBlank = 1.05f;      // ī�� ����
    Vector2 cardCenterValue = new Vector2(-1.58f, -2f); // ī�� ���� ���� ��
    #endregion

    #region Unity Methods
    private void Awake()
    {
        // ���ҽ� �Ŵ����� ���� ��������Ʈ �ҷ��� ����
        //cardImages = Resources.Load<Sprite>("CardImage0")
        
    }

    private void Start()
    {
        this.Initalized();
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