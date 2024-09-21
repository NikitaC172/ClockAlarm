using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeRequesterJson : TimeRequester
{
    [SerializeField] private string _url;
    [SerializeField] private int _timeMinutesRepeatRequest = 60;
    private bool _isWorking = false;
    private bool _isRepeating = true;

    private const int HourToSeconds = 3600;
    private const int MinuteToSeconds = 60;

    private void Start()
    {
         _ = RepeatReauest();
    }

    private async UniTask RepeatReauest()
    {
        while (_isRepeating)
        {
            StartCoroutine(GetTime());
            await UniTask.Delay(TimeSpan.FromMinutes(_timeMinutesRepeatRequest));
        }
    }

    private IEnumerator GetTime()
    {
        if (_isWorking == false)
        {
            _isWorking = true;
            using (UnityWebRequest req = UnityWebRequest.Get(_url))
            {
                req.SetRequestHeader("Access-Control-Allow-Origin", "0.0.0.0");
                req.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
                req.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
                yield return req.SendWebRequest();

                while (!req.isDone)
                {
                    yield return null;
                }

                byte[] result = req.downloadHandler.data;

                if (result != null)
                {
                    string JSON = System.Text.Encoding.Default.GetString(result);
                    JsonStruct info = JsonUtility.FromJson<JsonStruct>(JSON);
                    long timeJSon = info.time;
                    int timeSeconds =
                        DateTimeOffset.FromUnixTimeMilliseconds(timeJSon).LocalDateTime.Hour * HourToSeconds +
                        DateTimeOffset.FromUnixTimeMilliseconds(timeJSon).LocalDateTime.Minute * MinuteToSeconds +
                        DateTimeOffset.FromUnixTimeMilliseconds(timeJSon).LocalDateTime.Second;
                    CurrentTime?.Invoke(timeSeconds);
                }
                else
                {
                    Debug.LogWarning("Cant receive");
                    yield return new WaitForSecondsRealtime(10f);
                    _isWorking = false;
                    StartCoroutine(GetTime());
                }
            }

            _isWorking = false;
        }
    }
}
