using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LightController : MonoBehaviour
{
    //[SerializeField]
    float Lspeed;
    public float rotX;
    [SerializeField]//ライトの角度基準　0時
    Vector3 lrotateS = new Vector3(270.0f, -45.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        Lspeed = 360.0f / 86400.0f;//360°を1日の秒数で割る
        //Lspeed = 0.5f;//テスト用のスピード
        Debug.Log("speed =" + Lspeed);
        transform.localRotation = Quaternion.Euler(lrotateS);
        Debug.Log("時間" + DateTime.Now.Hour + "分" + DateTime.Now.Minute);
        var lrotateX = transform.localEulerAngles.x + 15.0f * (DateTime.Now.Hour + DateTime.Now.Minute / 60.0f);
        if(lrotateX > 360.0f)
        {
            lrotateX -= 360.0f;
        }
        Debug.Log("StartLight" + lrotateX);
        transform.localEulerAngles = new Vector3(lrotateX, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        rotX = Lspeed * Time.deltaTime;
        transform.Rotate(rotX, 0, 0);
    }
}
