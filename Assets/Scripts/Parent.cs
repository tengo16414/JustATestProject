using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{

    public static Parent _instance;
    public float LogStep;
    public GameObject testpre;
    public GameObject inst;

    int counter = 0;

    void Awake()
    {
        if (_instance != null)
        {
            //Destroy(_instance.gameObject);
            return;
        }
        _instance = this;
        inst = Instantiate(testpre);
    }
    IEnumerator TestCoroutine()
    {
        Debug.Log("CR started");
        float timeCounter = 0f;
        while (true)
        {
            Debug.Log("I am Alive " + timeCounter);
            yield return new WaitForSeconds(LogStep);
            timeCounter += LogStep;
        }
    }
    public void StartTestCoro()
    {
        Debug.Log("StartTestCoro()");
        StartCoroutine(TestCoroutine());
    }
    public void onClick()
    {
        //Destroy(inst);
        inst.GetComponent<Parent>().triggered();
    }
    public void onClickCoroTest()
    {
        StartCoroutine(CoroTest());
    }

    IEnumerator CoroTest()
    {
        counter++;
        int N = counter;
        Debug.LogError(N + "start");
        yield return new WaitForSeconds(5f);
        Debug.LogError(N + "end");
    }

    public virtual void triggered()
    {
        Debug.Log("parent");
    }
}
