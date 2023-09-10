using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class SkyController : MonoBehaviour
{
    [SerializeField]
    GameObject dLight;
    [SerializeField]
    Material[] skyboxes;//空と天気を変える 0:夜, 1:日の出, 2:昼間, 3:夕方
    [SerializeField]
    SoundManager sm;
    [SerializeField]
    GetWeatherScript gw;
    public bool bNight = false;//鳥のSEの再生、停止に使用

    int time;
    // 5~7:朝,8~14:昼,15~17:夕方,18~4:夜
    void Update()
    {
        time = int.Parse(DateTime.Now.Hour.ToString());
        if(time >= 18 && time < 24 && gw.bGetW)//曇りかどうかは関係なし
        {
            bNight = true;
            RenderSettings.skybox = skyboxes[0];//夜
        }
        else if (time >= 0 && time < 5)
        {
            bNight = true;
            RenderSettings.skybox = skyboxes[0];//夜
        }
        else if (!gw.bCloud && gw.bGetW)//晴れの時
        {
            if (time >= 5 && time < 8)
            {
                bNight = false;
                RenderSettings.skybox = skyboxes[1];//朝
            }
            else if (time >= 8 && time < 15)
            {
                bNight = false;
                RenderSettings.skybox = skyboxes[2];//昼
            }
            else if (time >= 15 && time < 18)
            {
                bNight = false;
                RenderSettings.skybox = skyboxes[3];//夕方
            }
        }
        else if (gw.bCloud && gw.bGetW)//曇りの時
        {
            if (time >= 5 && time < 18)
            {
                bNight = false;//夜以外は何もなしフラグ変更のみ
            }
        }

        /*var lightPos = dLight.transform.eulerAngles;
        if (lightPos.x > 360)
        {
            lightPos.x -= 360;
        }
        //Debug.Log(lightPos);
        if (180 <= lightPos.x && lightPos.x < 345 && gw.bGetW)//天気取得後、夜の場合
        {
            //bNight = true;//鳥のSEを止める
            sm.smStop = true;
            RenderSettings.skybox = skyboxes[0];
            //Debug.Log("night");
        }
        else if (!gw.bCloud && gw.bGetW)//天気取得後、曇りでない場合(曇りの場合は夜以外曇りのskybox使用) 
        {
            //bNight = false;
            sm.smStop = false;
            if (345 <= lightPos.x && lightPos.x < 360)
            {
                RenderSettings.skybox = skyboxes[1];
                //Debug.Log("dawn");
            }
            else if (0 <= lightPos.x && lightPos.x < 30)
            {
                RenderSettings.skybox = skyboxes[1];
                //Debug.Log("dawn");
            }
            else if (30 <= lightPos.x && lightPos.x < 135)
            {
                RenderSettings.skybox = skyboxes[2];
                //Debug.Log("morning");
            }
            else if (135 <= lightPos.x && lightPos.x < 180)
            {
                RenderSettings.skybox = skyboxes[3];
                //Debug.Log("evening");
            }*/
    }
}

