using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void OnButtonPressed()
    {
        animator.SetBool("IsPressed", true);
    }

    public void OnButtonRelease()
    {
        animator.SetBool("IsPressed", false);
    }

}
