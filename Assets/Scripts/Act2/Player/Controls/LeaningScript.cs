using UnityEngine;

public class LeaningScript : MonoBehaviour
{
    public LeanTrigger CurrentLeanPlace;
    public bool IsLeaning;

    public KeyCode LeanLeftButton;
    public KeyCode LeanRightButton;

    //[SerializeField] private Animator LeanAnimator;

    [SerializeField] private float LeanForce; //Delete this when animations are done and uncomment animator

    private bool canLean;

    private LeanSide side;

    //This must be empty gameobject whith position of wanted pivot
    [SerializeField] private Transform MainCameraPivot;

    private void Update()
    {
        if(canLean)
        {
            TryLean();
        }
    }
    private void TryLean()
    {
        if (Input.GetKey(LeanLeftButton) && side == LeanSide.Left)
        {
            //LeanAnimator.SetTrigger("LeanLeft");

            IsLeaning = true;

            transform.position = CurrentLeanPlace.PlayerInTransform.position;

            transform.rotation = CurrentLeanPlace.PlayerInTransform.rotation;

            MainCameraPivot.localRotation = Quaternion.Euler(0f, 0f, LeanForce);

            return;
        }
        if (Input.GetKey(LeanRightButton) && side == LeanSide.Right)
        {
            //LeanAnimator.SetTrigger("LeanRight");

            IsLeaning = true;

            transform.position = CurrentLeanPlace.PlayerInTransform.position;

            transform.rotation = CurrentLeanPlace.PlayerInTransform.localRotation;

            MainCameraPivot.localRotation = Quaternion.Euler(0f, 0f, -LeanForce);

            return;
        }

        IsLeaning = false;

        MainCameraPivot.localRotation = Quaternion.identity;

        //LeanAnimator.SetTrigger("LeanToNormal");
    }
    public void EnableLeaning(LeanSide side)
    {
        canLean = true;
        this.side = side;
    }
    public void DisableLeaning()
    {
        canLean = false;
        side = LeanSide.None;
        MainCameraPivot.localRotation = Quaternion.identity;
    }
}
