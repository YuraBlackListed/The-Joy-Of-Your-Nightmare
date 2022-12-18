using UnityEngine;

public class LeaningScript : MonoBehaviour
{
    public KeyCode LeanLeftButton;
    public KeyCode LeanRightButton;

    [SerializeField] private Animator LeanAnimator;

    private bool CanLean;

    private LeanSide Side;

    //This must be empty gameobject whith position of wanted pivot
    [SerializeField] private Transform MainCameraPivot;

    private void Update()
    {
        if(CanLean)
        {
            TryLean();
        }
    }
    private void TryLean()
    {
        if (Input.GetKey(LeanLeftButton) && Side == LeanSide.Left)
        {
            LeanAnimator.SetTrigger("LeanLeft");
            return;
        }
        if (Input.GetKey(LeanRightButton) && Side == LeanSide.Right)
        {
            LeanAnimator.SetTrigger("LeanRight");
            return;
        }

        LeanAnimator.SetTrigger("LeanToNormal");
    }
    public void EnableLeaning(LeanSide side)
    {
        CanLean = true;
        Side = side;
    }
    public void DisableLeaning()
    {
        CanLean = false;
        Side = LeanSide.None;
        MainCameraPivot.rotation = Quaternion.identity;
    }
}
