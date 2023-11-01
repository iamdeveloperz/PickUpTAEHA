using UnityEngine;

public class TimerAnimation : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("TimeNumber", GameManager.Instance.GameTime);
    }
}
