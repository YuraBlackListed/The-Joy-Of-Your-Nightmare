using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void Pointed()
    {
        animator.SetBool("Pointed", true);
    }
    public void Unpointed()
    {
        animator.SetBool("Pointed", false);
    }
}
