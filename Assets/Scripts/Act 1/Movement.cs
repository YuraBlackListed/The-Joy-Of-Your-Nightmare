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
        anim.SetBool("Forward", Input.GetKey(ForwardButton));
        anim.SetBool("Right", Input.GetKey(RightButton));
        anim.SetBool("Back", Input.GetKey(BackwardButton));
    }
}
