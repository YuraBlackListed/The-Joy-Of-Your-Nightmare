using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject cameraObj;
    private Animator anim;
    private bool stood = false;
    [SerializeField] private KeyCode SpaceButton;
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    void Update()
    {
        if(Input.GetKeyDown(SpaceButton))
        {

            if (stood == false)
            {
                bool lookingForward = cameraObj.transform.localRotation.eulerAngles.y >= 220 &&
                                      cameraObj.transform.localRotation.eulerAngles.y < 288;
                bool lookingDrawer = cameraObj.transform.localRotation.eulerAngles.y > 288 &&
                                     cameraObj.transform.localRotation.eulerAngles.y <= 320;
                bool lookingRightForward = cameraObj.transform.localRotation.eulerAngles.y >= 320 &&
                                           cameraObj.transform.localRotation.eulerAngles.y <= 360;
                bool lookingRightBack = cameraObj.transform.localRotation.eulerAngles.y >= 0 &&
                                        cameraObj.transform.localRotation.eulerAngles.y <= 45;
                bool lookingBack = cameraObj.transform.localRotation.eulerAngles.y > 45 &&
                                   cameraObj.transform.localRotation.eulerAngles.y <= 145;

                stood = true;

                if (lookingForward)
                {
                    anim.SetBool("Forward", stood);
                }
                else if (lookingDrawer)
                {
                    anim.SetBool("Drawer", stood);
                }
                else if (lookingRightForward || lookingRightBack)
                {
                    anim.SetBool("Right", stood);
                }
                else if (lookingBack)
                {
                    anim.SetBool("Back", stood);
                }
            }
            else
            {
                stood = false;

                anim.SetBool("Forward", stood);
                anim.SetBool("Drawer", stood);
                anim.SetBool("Right", stood);
                anim.SetBool("Back", stood);
            }
        }
        
        
        
    }
}
