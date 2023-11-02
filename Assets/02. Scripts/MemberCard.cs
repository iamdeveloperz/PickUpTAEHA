using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemberCard : MonoBehaviour
{
    public Animator anim;

    string[] imgName = { "��", "��ȣ", "����", "�¿�", "����" };
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
        //���� ������ �� ī�� �غ� �ִϸ��̼� ����
        anim.SetBool("isStart", true);
        //�ִϸ��̼� ������ �����ϴ� �Լ�
        Invoke("StartAnimEnd", 3.0f);

        //��Ŀ �Ǻ��� ���� �̸� �޾ƿ� �� ���ڸ��� int ������ �����
        
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
        SoundManager.Instance.OpenCard();   // ī�� ���� ����
        front.SetActive(true);
        back.SetActive(false);

        //��Ŀ�� ��Ģ�� 5�� ������ 4�� ���� �� �ð� ���̱�
        if (jokerCheck % 5 == 4)
        {
            //�̹� ������ ��Ŀ ���� Ŭ�� ����
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
