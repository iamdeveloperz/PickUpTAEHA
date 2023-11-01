using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Animator anim;

    private GameObject front;
    private GameObject back;

    private bool isFlip = false;

    #region Unity Methods
    void Start()
    {
        front = transform.Find("Front").gameObject;
        back = transform.Find("Back").gameObject;
    }
    #endregion

    #region Main Methods
    public void openCard()
    {
        anim.SetBool("isOpen", true);

        front.SetActive(true);
        back.SetActive(false);

        //��Ŀ �Ǻ��� ���� �̸� �޾ƿ� �� ���ڸ��� int ������ �����
        string joker = gameObject.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        int jokerCheck = int.Parse(joker.Substring(joker.Length - 1));
        
        //��Ŀ�� ��Ģ�� 5�� ������ 4�� ���� �� �ð� ���̱�
        if(jokerCheck%5== 4)
        {
            GameManager.Instance.GameTime -= 3.0f;
        }

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = gameObject;
        }
        else
        {
            GameManager.Instance.secondCard = gameObject;
            GameManager.Instance.cardMatched();
        }

        /* ī�尡 ������ ���� �����ٸ� */
        if (!isFlip)
            this.IsFlipCard();
    }

    public void IsFlipCard()
    {
        isFlip = true;

        SpriteRenderer backRenderer = back.transform.GetComponent<SpriteRenderer>();

        backRenderer.sprite = Resources.Load<Sprite>("CardBackClicked");
    }
    #endregion

    #region Sub Methods
    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("Back").gameObject.SetActive(true);
        transform.Find("Front").gameObject.SetActive(false);
    }
    #endregion
}
