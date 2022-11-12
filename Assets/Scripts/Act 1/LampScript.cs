using UnityEngine;

public class LampScript : MonoBehaviour
{
    public bool Active;
    [SerializeField] private GameObject lightning;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Active) 
        {
            TurnOff();
        }
        else 
        {
            TurnOn();
        }
    }

    public void TurnOn()
    {
        lightning.SetActive(false);
    }

    public void TurnOff()
    {
        lightning.SetActive(true);
    }
}
