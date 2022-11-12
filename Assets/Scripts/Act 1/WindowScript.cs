using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public bool IsUsed;
    public bool IsClosed = false;

    [SerializeField] private Animator leftCurtain;
    [SerializeField] private Animator rightCurtain;


    private void Update()
    {
        if (IsUsed) Close();
        else Open();
    }

    public void Open()
    {
        IsClosed = false;

        leftCurtain.SetBool("Opened", true);
        rightCurtain.SetBool("Opened", true);
    }

    public void Close()
    {
        IsClosed = true;

        leftCurtain.SetBool("Opened", false);
        rightCurtain.SetBool("Opened", false);
    }
}
