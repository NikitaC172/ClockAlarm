using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TimeRequester : MonoBehaviour
{
    [SerializeField] private string[] _urlServers;

    private List<Task<DateTime>> _tasks;
    private DateTime _globDateTime;
    private float _intervalRequestMinutes = 60f;

    public Action<int> CurrentTime;

    private void Start()
    {
        RequestTimePeriodic();
    }

    private async void RequestTimePeriodic()
    {
        while (true)
        {
            CreateTaskTimeRequest();
            Task<DateTime> getTime = await Task.WhenAny(_tasks);
            _globDateTime = await getTime;
            SetCurrentTime();
            await Task.Delay(TimeSpan.FromMinutes(_intervalRequestMinutes));
        }
    }

    private void SetCurrentTime()
    {
        int timeSeconds = _globDateTime.Hour * 3600 + _globDateTime.Minute * 60 + _globDateTime.Second;
        CurrentTime?.Invoke(timeSeconds);
    }

    private void CreateTaskTimeRequest()
    {
        _tasks = new List<Task<DateTime>>();

        for (int i = 0; i < _urlServers.Length; i++)
        {
            _tasks.Add(CheckGlobalTime(_urlServers[i]));
        }
    }

    private async Task<DateTime> CheckGlobalTime(string URL)
    {
        DateTime globDateTime;
        bool isCorrect = true;
        UnityWebRequest unityWebRequest = new UnityWebRequest(URL);
        unityWebRequest.SendWebRequest();
        string timeStr = null;

        while (isCorrect == true)
        {
            while (!unityWebRequest.isDone && unityWebRequest.error == null)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            try
            {
                timeStr = unityWebRequest.GetResponseHeader("Date");
                isCorrect = false;
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
                await Task.Delay(TimeSpan.FromSeconds(10));
            }

        }

        if (!DateTime.TryParse(timeStr, out globDateTime))
        {
            return DateTime.MinValue;
        }

        return globDateTime;
    }
}
