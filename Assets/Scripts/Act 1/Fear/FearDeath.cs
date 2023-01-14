using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FearDeath : MonoBehaviour
{
    [SerializeField] LevelChanger levelManager;
    [SerializeField] private Image fade;
    private int blinks = 0;

    public void FallingAsleep()
    {
        InvokeRepeating(nameof(Blink), 0.0f, 4f);
        Invoke(nameof(Die), 2f);
    }
    private void Die()
    {
        CancelInvoke();
        fade.DOFade(1f, 2f).OnComplete(()=>levelManager.LoadLevel(0));
    }
    private void Blink()
    {
        fade.DOFade(1f, 1f).OnComplete(()=>UnBlink());
    }
    private void UnBlink()
    {
        fade.DOFade(0f, 1f);
    }
}
