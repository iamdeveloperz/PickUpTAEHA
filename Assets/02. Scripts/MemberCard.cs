using UnityEngine;
using UnityEngine.UI;

public class MemberCard : MonoBehaviour
{
    public Animator anim;

    string[] imgName = { "희성", "준호", "승준", "태용", "태하" };
    string joker;
    int jokerCheck;
    public Text cardName;

    private GameObject front;
    private GameObject back;
    // Start is called before the first frame update


    private bool isFlip = false;

    #region Unity Methods
    void Start()
    {
        //조커 판별을 위한 이름 받아와 끝 수자리만 int 값으로 만들기
        joker = gameObject.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        jokerCheck = int.Parse(joker.Substring(joker.Length - 2));
        cardName.text = imgName[jokerCheck % 5];
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

        //조커의 규칙인 5로 나누면 4가 나올 때 시간 줄이기
        if (jokerCheck % 5 == 4)
        {
            GameManager.Instance.GameTime -= 3.0f;
        }

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = gameObject;
        }
        else if (GameManager.Instance.firstCard != this.gameObject)
        {
            GameManager.Instance.secondCard = gameObject;
            GameManager.Instance.CardMatched();
        }

        /* 카드가 뒤집힌 적이 없었다면 */
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
        NameCheck();
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.4f);
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
    #endregion
}
