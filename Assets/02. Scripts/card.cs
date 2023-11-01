using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Animator anim;
    string[] imgName = { "��", "��ȣ", "����", "�¿�", "����" };
    string joker;
    int jokerCheck;
    public Text cardName;
    // Start is called before the first frame update
    void Start()
    {
        //��Ŀ �Ǻ��� ���� �̸� �޾ƿ� �� ���ڸ��� int ������ �����
        joker = gameObject.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        jokerCheck = int.Parse(joker.Substring(joker.Length - 1));
        cardName.text = imgName[jokerCheck % 5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openCard()
    {
        anim.SetBool("isOpen", true);
        transform.Find("Front").gameObject.SetActive(true);
        transform.Find("Back").gameObject.SetActive(false);

        
        
        
        //��Ŀ�� ��Ģ�� 5�� ������ 4�� ���� �� �ð� ���̱�
        if(jokerCheck%5== 4)
        {
            GameManager.Instance.time -= 3.0f;
        }

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = gameObject;
        }
        else if(GameManager.Instance.firstCard != this.gameObject)
        {
            GameManager.Instance.secondCard = gameObject;
            GameManager.Instance.cardMatched();
        }
    }
    public void DestroyCard()
    {
        NameCheck();
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
    void NameCheck()
    {
        
        anim.SetBool("isFair", true);
    }
}
