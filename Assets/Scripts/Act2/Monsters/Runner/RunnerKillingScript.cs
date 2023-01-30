using UnityEngine;

public class RunnerKillingScript : MonoBehaviour
{
    [SerializeField] private Transform DeathPosition;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject incomingObject = collision.gameObject;

        if (incomingObject.tag == "Player")
        {
            //Replace this with a jumscare
            incomingObject.transform.position = DeathPosition.position;
        }
    }
}
