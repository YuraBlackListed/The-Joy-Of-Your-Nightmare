using UnityEngine;

public enum LeanSide
{
    None = 0,
    Right = -1,
    Left = 1
}
public class LeanTrigger : MonoBehaviour
{
    public Transform CornererSpawner;

    public Transform PlayerInTransform;

    [SerializeField] private LeanSide Side;

    private void OnTriggerStay(Collider col)
    {
        GameObject incomingObject = col.gameObject;

        if(incomingObject.tag == "Player")
        {
            var thisLeaningScript = incomingObject.GetComponent<LeaningScript>();

            thisLeaningScript.EnableLeaning(Side);

            thisLeaningScript.CurrentLeanPlace = this;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        GameObject incomingObject = col.gameObject;

        if (incomingObject.tag == "Player")
        {
            var thisLeaningScript = incomingObject.GetComponent<LeaningScript>();

            thisLeaningScript.DisableLeaning();

            thisLeaningScript.CurrentLeanPlace = null;
        }
    }
}
