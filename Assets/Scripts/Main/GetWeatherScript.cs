using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;
//using UnityEngine.WSA;

public class GetWeatherScript : MonoBehaviour
{
    [SerializeField]
    Text weatherText;
    //[SerializeField]
    //GameObject dLight;
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
    [SerializeField]
    GameObject loadP;
    public bool bCloud, bGetW, bRain, bRain2 = false;
    string result;
    int nowT;
    int startT;
    // Start is called before the first frame update
    void Start()
    {
        startT = int.Parse(DateTime.Now.Hour.ToString()) + 1;//2行先でコルーチンを実行するので、+1しUpdate内if文でコルーチンを実行しないようにする
        Debug.Log("startT=" + startT);
        StartCoroutine(GetWeather());
    }

    // Update is called once per frame
    void Update()
    {
        /*var lightPos = dLight.transform.eulerAngles;
        var n = lightPos.x % 15.0f;//DirectionalLightが15°(1時間)ごとに天気を取得
        if (n == 0.0f)
        {
            StartCoroutine(GetWeather());
            Debug.Log("StartCoroutine");
        }*/
        nowT = int.Parse(DateTime.Now.Hour.ToString());
        if (startT == nowT)
        {
            StartCoroutine(GetWeather());
            startT++;
            if (startT == 24)
            {
                startT = 0;
            }
        }
        // 動作確認用
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetWeather());
        }
        else if(Input.GetKeyDown(KeyCode.A)) 
        {
            result = "Rain";
            WTest();
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            result = "Snow";
            WTest();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            result = "Clear";
            WTest();
        }*/
    }
    //OpenWeatherMapから現在の天気を取得、Json形式から変換
    private IEnumerator GetWeather()
    {
        loadP.SetActive(true);
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
                result = response.weather[0].main;
                if (result == "Clouds")//曇り→skybox変更
                {
                    weatherText.text = ("天気:くもり");
                    rain.SetActive(false);
                    snow.SetActive(false);
                    RenderSettings.skybox = cloud;
                    bRain = false;//SoundManager内Coroutine スタート用
                    bRain2 = false;//SoundManager内Coroutine while用
                    bCloud = true;//skycontrollerで使用
                }
                else if (result == "Rain")//雨の場合→雨オブジェクトとフラグをtrue、skyboxは曇り
                {
                    weatherText.text = ("天気:雨");
                    rain.SetActive(true);
                    snow.SetActive(false);
                    RenderSettings.skybox = cloud;
                    bRain = true;
                    bRain2 = true;
                    bCloud = true;
                }
                else if (result == "Snow")//雪の場合→雪オブジェクトとRainをtrue、skyboxは曇り
                {
                    weatherText.text = ("天気:雪");
                    snow.SetActive(true);
                    rain.SetActive(false);
                    RenderSettings.skybox = cloud;
                    bRain = true;
                    bRain2 = true;
                    bCloud = true;
                }
                else//上記以外は晴れで対応、skyboxはskycontrollerの通り
                {
                    weatherText.text = ("天気:晴れ");
                    rain.SetActive(false);
                    snow.SetActive(false);
                    bRain = false;
                    bRain2 = false;
                    bCloud = false;
                }
                Debug.Log(result);
                loadP.SetActive(false);
                bGetW = true;//天気取得完了
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
        //public int id;
        public string main;
        //public string description;
        //public string icon;
    }

    //動作確認用
    /*void WTest()
    {
        if (result == "Clouds")//曇り→skybox変更
        {
            weatherText.text = ("天気:くもり");
            rain.SetActive(false);
            snow.SetActive(false);
            RenderSettings.skybox = cloud;
            bRain = false;
            bRain2 = false;
            bCloud = true;//skycontrollerで使用        }
        else if (result == "Rain")//雨の場合→雨オブジェクトとフラグをtrue、skyboxは曇り
            {
                weatherText.text = ("天気:雨");
                rain.SetActive(true);
                snow.SetActive(false);
                RenderSettings.skybox = cloud;
                bCloud = true;
                bRain = true;
                bRain2 = true;
            }
            else if (result == "Snow")//雪の場合→雪オブジェクトとRainをtrue、skyboxは曇り
            {
                weatherText.text = ("天気:雪");
                snow.SetActive(true);
                rain.SetActive(false);
                RenderSettings.skybox = cloud;
                bRain = true;
                bRain2 = true;
                bCloud = true;
            }
            else//上記以外は晴れで対応、skyboxはskycontrollerの通り
            {
                weatherText.text = ("天気:晴れ");
                rain.SetActive(false);
                snow.SetActive(false);
                bRain = false;
                bRain2 = false;
                bCloud = false;
            }
        }
    }*/
}