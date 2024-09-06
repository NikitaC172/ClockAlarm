using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public class TimeRequester : MonoBehaviour
{
    [SerializeField] private string[] _urlServers;

    private void Awake()
    {
        /*private async Task<WeatherInfo> GetWeather()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", CityId, API_KEY));
            HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
            return info;
        }*/
    }
}
