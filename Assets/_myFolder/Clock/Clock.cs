using System;
using System.Threading.Tasks;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private int timeTest; ////////////////////////////////////////////////////////////////
    private int _currentTime;
    public Action<int> timeChangedSeconds;
    public Action<float,float,float> timeChanged;

    private const int MaxSeconds = 24*3600;

    private void Start()
    {
        _currentTime = timeTest;
        CountTime();
    }

    private async void CountTime()
    {
        while (true)
        {
            _currentTime++;

            if (_currentTime >= MaxSeconds)
            {
                _currentTime = 0;
            }

            OnChangedTime();
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    private void OnChangedTime()
    {
        float hour = _currentTime / 3600f;
        float minute = _currentTime % 3600f / 60f;
        float seconds = _currentTime % 60f;

        timeChangedSeconds?.Invoke(_currentTime);
        timeChanged?.Invoke(hour, minute, seconds);
    }
}
