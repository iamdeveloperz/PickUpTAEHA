using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Animator anim;
    string[] imgName = { "희성", "준호", "성준", "태용", "태하" };
    string joker;
    int jokerCheck;
    public Text cardName;
    // Start is called before the first frame update
    void Start()
    {
        //조커 판별을 위한 이름 받아와 끝 수자리만 int 값으로 만들기
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

        
        
        
        //조커의 규칙인 5로 나누면 4가 나올 때 시간 줄이기
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
