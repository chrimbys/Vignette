using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    [SerializeField]
    GameObject sliderbar;
    Slider slider;
    [SerializeField]
    GameObject button;
    AsyncOperation async;
    bool bpush = false;
    // Start is called before the first frame update
    void Start()
    {
        slider = sliderbar.GetComponent<Slider>();
        button.SetActive(false);
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync("Main");
        async.allowSceneActivation = false;
        while(!async.isDone)
        {
            slider.value = async.progress;
            if(async.progress >= 0.9f)
            {
                sliderbar.SetActive(false);
                button.SetActive(true);
                if(bpush) async.allowSceneActivation = true;
            }
            yield return null;
        }

    }
    public void PushButton()
    {
        bpush = true;
    }
}
