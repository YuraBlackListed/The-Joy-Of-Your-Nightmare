using TMPro;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    [SerializeField] private TimeCounter Time;

    [SerializeField] private TMP_Text ClockText;

    private int hours;
    private int minutesReal;
    private int minutes;

    private int minutesFirst;
    private int minutesSecond;

    private void Update()
    {
        minutesReal = (int)Time.GameTime / 60;
        hours = minutesReal / 60;

        minutes = minutesReal - hours * 60;
        minutesFirst = minutes / 10;
        minutesSecond = minutes % 10;

        ChangeClock();
    }
    private void ChangeClock()
    {
        ClockText.text = $"{hours}:{minutesFirst}{minutesSecond}";
    }
}
