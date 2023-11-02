using TMPro;
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
        //게임 시작할 때 카드 준비 애니메이션 시작
        anim.SetBool("isStart", true);
        //애니메이션 끝나면 종료하는 함수
        Invoke("StartAnimEnd", 3.0f);

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
        anim.SetBool("isNewOpen", true);
        SoundManager.Instance.OpenCard();   // 카드 오픈 사운드
        front.SetActive(true);
        back.SetActive(false);

        //조커의 규칙인 5로 나누면 4가 나올 때 시간 줄이기
        if (jokerCheck % 5 == 4)
        {
            //이미 뒤집힌 조커 연속 클릭 제한
            if (!anim.GetBool("isJoker"))
            {
            GameManager.Instance.GameTime -= 3.0f;
            GameManager.Instance.Minus();
            }
            anim.SetBool("isJoker", true);
        }
        else
        {
            anim.SetBool("isNewOpen", true);
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
        anim.SetBool("isNewOpen", false);
        anim.SetBool("isJoker", false);
        //transform.Find("Back").gameObject.SetActive(true);
        //transform.Find("Front").gameObject.SetActive(false);

    }
    void NameCheck()
    {
        anim.SetBool("isFair", true);
    }
    public void StartAnimEnd()
    {
        GameManager.Instance.startAnim = true;
        anim.SetBool("isStart", false);
    }
    #endregion
}
