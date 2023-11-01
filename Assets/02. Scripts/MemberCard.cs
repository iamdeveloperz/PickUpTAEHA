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
        //��Ŀ �Ǻ��� ���� �̸� �޾ƿ� �� ���ڸ��� int ������ �����
        Debug.Log(gameObject.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name);
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

        //��Ŀ�� ��Ģ�� 5�� ������ 4�� ���� �� �ð� ���̱�
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
