using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //雨のみオブジェクトセットアクティブ時再生されるため別管理とする
    [SerializeField]
    AudioSource bird;
    [SerializeField]
    AudioSource bgm;
    [SerializeField]
    SkyController sc;
    [SerializeField]
    GetWeatherScript gw;
    //public bool smStart, smStop;//鳥の声再生、停止
    // Start is called before the first frame update
    void Start()
    {
        bgm.Play();
    }
    void Update()
    {
        if(!gw.bRain && !sc.bNight)//夜でない、雨＆雪ではない.bNightはSkyController Update内にあるため読み取り専用
        {
            gw.bRain = true;
            StartCoroutine(BirdSound());
        }
    }
    private IEnumerator BirdSound()
    {
        /*while (!smStop || !sc.bNight)//夜or雨or雪になるまで続く
        {
            float i = Random.Range(4, 10);
            Debug.Log("soundfloat i =" + i);
            yield return new WaitForSeconds(i);
            bird.Play();
        }*/
        while (!gw.bRain2 && !sc.bNight)//夜or雨or雪になるまで続く
        {
            float i = Random.Range(15, 61);
            //Debug.Log("soundfloat i =" + i);
            yield return new WaitForSeconds(i);
            bird.Play();
        }

    }


}
