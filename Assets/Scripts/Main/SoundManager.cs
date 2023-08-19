using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //[SerializeField]
    //AudioSource rain;
    [SerializeField]
    AudioSource bird;
    [SerializeField]
    AudioSource bgm;
    [SerializeField]
    SkyController sc;
    [SerializeField]
    GetWeatherScript gw;
    // Start is called before the first frame update
    void Start()
    {
        // bgm = GetComponent<AudioSource>();
        bgm.Play();
        StartCoroutine(BirdSound());
    }
    void Update()
    {
       
    }
    /*public void RainSound()
    {
        rain.Play();
    }*/
    private IEnumerator BirdSound()
    {
        while (!gw.bRain && !sc.bNight && gw.bGetW)
        {
            float i = Random.Range(4, 61);
            Debug.Log("soundfloat i =" + i);
            yield return new WaitForSeconds(i);
            bird.Play();
        }

    }
}
