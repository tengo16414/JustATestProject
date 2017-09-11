using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Parent {


    private void OnDestroy()
    {
        Debug.Log("OnDestroy()");
        triggered();
        //StartTestCoro();
       // Parent._instance.StartTestCoro();
    }
    public override void triggered()
    {
        Debug.Log("child1");
    }
}
