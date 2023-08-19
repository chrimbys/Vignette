using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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
    public bool bNight = false;
    // Start is called before the first frame update
    void Start()
    {
    }
    // 5~7:朝,8~14:昼,15~17:夕方,18~4:夜
    void Update()
    {
        var lightPos = dLight.transform.eulerAngles;
        if(lightPos.x > 360)
        {
            lightPos.x -= 360;
        }
        //Debug.Log(lightPos);
        if (180 <= lightPos.x && lightPos.x < 345 && gw.bGetW)
        {
            bNight = true;
            RenderSettings.skybox = skyboxes[0];
            //Debug.Log("night");
        }
        else if (!gw.bCloud && gw.bGetW)
        {
            if ((345 <= lightPos.x && lightPos.x < 360) || (0 <= lightPos.x && lightPos.x < 30))
            {
                bNight = false;
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
            }
        }
    }
}

