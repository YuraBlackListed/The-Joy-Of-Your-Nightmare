using UnityEngine;

public enum LeanSide
{
    None = 0,
    Right = -1,
    Left = 1
}
public class LeanTrigger : MonoBehaviour
{
    [SerializeField] private LeanSide Side;

    private void OnTriggerEnter(Collider col)
    {
        GameObject incomingObject = col.gameObject;

        if(incomingObject.tag == "Player")
        {
            var thisLeaningScript = incomingObject.GetComponent<LeaningScript>();

            thisLeaningScript.EnableLeaning(Side);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        GameObject incomingObject = col.gameObject;

        if (incomingObject.tag == "Player")
        {
            var thisLeaningScript = incomingObject.GetComponent<LeaningScript>();

            thisLeaningScript.DisableLeaning();
        }
    }
}
