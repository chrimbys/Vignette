using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharactorController : MonoBehaviour
{
    int waitTime;
    NavMeshAgent goalPos;
    NavMeshPath path;
    Animator anime;

    private void Start()
    {
        anime = GetComponent<Animator>();
        goalPos = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        waitTime = Random.Range(10, 20);
        //Debug.Log("Start num=" + waitTime);
        Invoke("MotionCoroutine",waitTime);
    }
    void Update()//動作確認用
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomMove();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            RandomRotate();
        }*/
    }
    private void MotionCoroutine()
    {
        //Debug.Log("CoroutineStart");
        waitTime = Random.Range(0, 3);
        if(waitTime == 0)
        {
            //Debug.Log("Move");
            RandomMove();
        }
        else
        {
            //Debug.Log("Rotate");
            RandomRotate();
        }
    }
    private void RandomMove()
    {
        RaycastHit hit;
        NavMeshHit nhit;
        float j = Random.Range(-44, 65);
        float k = Random.Range(-65, 34);
        Vector3 rayStartPos = new Vector3(j, 2100, k);
        //Debug.Log("座標" + rayStartPos);
        bool hasHit = Physics.Raycast(rayStartPos, Vector3.down, out hit);
        //Debug.Log("hashit" + hasHit);
        if (NavMesh.CalculatePath(this.transform.position, hit.point, NavMesh.AllAreas, path))
        {
            //Debug.Log("一発OK");
            goalPos.destination = hit.point;
        }
        else
        {
            //Debug.Log("再検索");
            bool rePos = NavMesh.SamplePosition(rayStartPos, out nhit, 100.0f, NavMesh.AllAreas);
            //Debug.Log("rePos" + rePos + "nhit=" + nhit);
            if (rePos)
            {
                //Debug.Log("再検索成功");
                goalPos.destination = nhit.position;
            }
        }
        waitTime = Random.Range(60, 180);
        //Debug.Log("Move num=" + waitTime);
        Invoke("MotionCoroutine", waitTime);
    }
    public void RandomRotate()
    {
        waitTime = Random.Range(0, 2);
        //Debug.Log(i);
        if (waitTime == 0)
        {
            anime.SetBool("rotate1bool", true);
        }
        else
        {
            anime.SetBool("rotate2bool", true);
        }

    }
    public void AnimeStop()
    {
        anime.SetBool("rotate1bool", false);
        anime.SetBool("rotate2bool", false);
        waitTime = Random.Range(10,20);
        //Debug.Log("rotateOK&waittime =" + num);
        Invoke("MotionCoroutine", waitTime);
    }
}
