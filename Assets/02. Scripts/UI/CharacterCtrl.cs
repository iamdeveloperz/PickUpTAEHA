using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterCtrl : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Animator tabAnimator;
    [SerializeField] private Animator characterAnimator;

    private bool isTouch = false;
    private const string animParam = "IsTouch";

    private void Start()
    {
        
    }

    private void Update()
    {
        if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsTag("CharacterJump") &&
            characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
        {
            isTouch = false;
            tabAnimator.SetBool(animParam, isTouch);
            characterAnimator.SetBool(animParam, isTouch);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
        tabAnimator.SetBool(animParam, isTouch);
        characterAnimator.SetBool(animParam, isTouch);
    }
}