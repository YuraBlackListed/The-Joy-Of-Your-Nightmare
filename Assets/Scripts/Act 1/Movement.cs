using UnityEngine;

public class Movement : MonoBehaviour
{
    public KeyCode ForwardButton;
    public KeyCode BackwardButton;
    public KeyCode RightButton;
    public KeyCode LeftButton;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetBool("GoForward", Input.GetKey(ForwardButton));
        anim.SetBool("GoRight", Input.GetKey(RightButton));
        anim.SetBool("GoLeft", Input.GetKey(LeftButton));
        anim.SetBool("GoBackward", Input.GetKey(BackwardButton));
    }
}
