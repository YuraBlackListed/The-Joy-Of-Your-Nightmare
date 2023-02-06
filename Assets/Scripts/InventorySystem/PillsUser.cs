using UnityEngine;

public class PillsUser : MonoBehaviour
{
    [SerializeField] private float PillForce;
    [SerializeField] private float PillDecreasement;
    [SerializeField] private float PillMinValue;

    [SerializeField] private FearScript FearScript;

    [SerializeField] private KeyCode PillsUseButton = KeyCode.R;

    private bool canUse = true;

    private void Update()
    {
        if(Input.GetKeyDown(PillsUseButton) && canUse)
        {
            TryUsePill();
        }
    }
    private void TryUsePill()
    {
        if (Inventory.CanUsePill())
        {
            FearScript.DecreaseFear(PillForce);

            DoPillaftermath();

            canUse = false;

            Invoke(nameof(ResetUsing), 5f);
        }
    }
    private void DoPillaftermath()
    {
        PillForce -= PillDecreasement;

        PillForce = Mathf.Clamp(PillForce, PillMinValue, 1f);
    }
    private void ResetUsing()
    {
        canUse = true;
    }
}
