using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeDisplayController : MonoBehaviour
{
    [SerializeField]
    Text timeText;
    //[SerializeField]
    //WeatherDisplayController wdc;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        timeText.text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString("00");
        //wdc.TimeMonitor(timeText.text);

    }
}