using TMPro;
using UnityEngine;

public class ClockRenderDigital : MonoBehaviour
{
    [SerializeField] private Clock _clock;
    [SerializeField] private TMP_Text _textTime;
    private const int LeftPadding = 2;

    private void OnEnable()
    {
        _clock.timeChanged += RenderClock;
    }

    private void OnDisable()
    {
        _clock.timeChanged -= RenderClock;
    }

    public void RenderClock(float hour, float minute, float second)
    {
        int hourInt = (int)hour;
        int minuteInt = (int)minute;
        int secondInt = (int)second;

        _textTime.text =
                        $"{hourInt}" +
                        $":{minuteInt.ToString().PadLeft(LeftPadding, '0')}" +
                        $":{secondInt.ToString().PadLeft(LeftPadding, '0')}";
    }
}
