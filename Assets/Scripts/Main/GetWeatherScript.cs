using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetWeatherScript : MonoBehaviour
{
    [SerializeField]
    Text weatherText;
    [SerializeField]
    GameObject dLight;
    [SerializeField]
    Material cloud;//曇りだけはGetWeatherScriptで使用する
    [SerializeField]
    GameObject rain;
    [SerializeField]
    GameObject snow;
    [SerializeField]
    SoundManager sm;
    [SerializeField]
    SkyController sc;
    public bool bRain, bCloud, bGetW = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetWeather());
    }

    // Update is called once per frame
    void Update()
    {
        var lightPos = dLight.transform.eulerAngles;
        var n = lightPos.x % 15.0f;
        if (n == 0.0f)
        {
            StartCoroutine(GetWeather());
            Debug.Log("StartCoroutine");
        }

        if(Input.GetKeyDown(KeyCode.Space))//動作確認用
        {
            StartCoroutine(GetWeather());
        }
    }
    //OpenWeatherMapから現在の天気を取得、Json形式から変換
    private IEnumerator GetWeather()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get
            ("https://api.openweathermap.org/data/2.5/weather?lat=36&lon=140&appid=9ae562d5660c5c2ff9cca88f20519051");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("サーバーとの通信に失敗");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("サーバーとの通信には成功したが、接続プロトコルでエラーが出た");
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("受信したデータの処理中にエラーがでた");
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("リクエスト成功");
                Debug.Log(webRequest.downloadHandler.text);
                var response = JsonUtility.FromJson<Response>(webRequest.downloadHandler.text);
                var result = response.weather[0].main;
                if (result == "Clouds" && !sc.bNight)
                {
                    weatherText.text = ("天気:くもり");
                    bRain = false;
                    rain.SetActive(false);
                    snow.SetActive(false);
                    RenderSettings.skybox = cloud;
                    bCloud = true;
                }
                else if (result == "Rain")
                {
                    weatherText.text = ("天気:雨");
                    rain.SetActive(true);
                    snow.SetActive(false);
                    RenderSettings.skybox = cloud;
                    bRain = true;
                    bCloud = true;
                }
                else if (result == "Snow")
                {
                    weatherText.text = ("天気:雪");
                    snow.SetActive(true);
                    rain.SetActive(false);
                    RenderSettings.skybox = cloud;
                    bRain = false;
                    bCloud = true;
                }
                else
                {
                    weatherText.text = ("天気:晴れ");
                    rain.SetActive(false);
                    snow.SetActive(false);
                    bRain = false;
                    bCloud = false;
                }
                Debug.Log(result);
                bGetW = true;
                break;

        }
    }
    [System.Serializable]
    public class Response
    {
        public weatherMain[] weather;
    }
    [System.Serializable]
    public class weatherMain
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }


}
