using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
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
}
