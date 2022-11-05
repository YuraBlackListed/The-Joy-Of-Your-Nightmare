using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public bool IsClosed { get; private set; }
    public bool IsUsed;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsUsed) Close();
        else Open();
    }

    public void Open()
    {
        IsClosed = false;

        animator.SetBool("CloseWindow", false);
    }

    public void Close()
    {
        IsClosed = true;

        animator.SetBool("CloseWindow", true);
    }
}
