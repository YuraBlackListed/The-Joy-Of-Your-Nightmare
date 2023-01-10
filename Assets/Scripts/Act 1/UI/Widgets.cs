using UnityEngine;

public class Widgets : MonoBehaviour
{
    [SerializeField] private Animator widget;
    private float minDistance = 1.6f;
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        if(distance <= minDistance)
        {
            widget.SetBool("Aimed", true);
        }
        else
        {
            widget.SetBool("Aimed", false);
        }
        
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }
}
