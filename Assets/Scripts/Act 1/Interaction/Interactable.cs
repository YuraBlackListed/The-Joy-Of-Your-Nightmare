using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private Interactable MyInteractable;

    public void DoInteraction()
    {
        MyInteractable.Interact();
    }

    public abstract void Interact();
}
