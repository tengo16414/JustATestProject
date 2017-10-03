using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuforiaBackgroundSizePrinter : MonoBehaviour
{

    public Vuforia.BackgroundPlaneBehaviour background;
    public Transform GrandMarker;

    public UnityEngine.UI.Text targetText;

    public float Multiplier;
    private Vector3 tempScaleVec;
    private bool activator = false;

    public Camera vuforiaCamera;
    public Camera MyCamera;

    private void Start()
    {
        //InvokeRepeating("ChangeBackgroundScaleUsingFrustumHeight", 2f, 0.5f);
        activator = false;
        targetText.text = "Hello Bitch!!!";
    }
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //ChangeDistance();
        //    //ChangeBackgroundScale();
        //    activator = !activator;
        //}
        //if (activator)
        //    ChangeBackgroundScaleUsingFrustumHeight();

        Debug.Log(vuforiaCamera.targetTexture.name);
        Debug.Log(MyCamera.targetTexture.name);

        //string str="undefined";// = string.Format("{0}",background.transform.parent.GetComponent<Camera>().targetTexture.ToString());
        //targetText.text = str;
        //Debug.Log("Before If statement");
        //if (background.transform.parent.GetComponent<Camera>().targetTexture == null)
        //{
        //    Debug.Log("True");
        //    str = "bitch";
        //}
        //else
        //{

        //    str = background.transform.parent.GetComponent<Camera>().targetTexture.name;
        //    Debug.Log("false\n"+str);
        //}
        //targetText.text = str;


        //string str = string.Format(
        //    "X: {0} Y: {1} Z: {2}\nL:{3},\nG:{4}\nD:{5}",
        //    background.transform.localScale.x.ToString(),
        //    background.transform.localScale.y.ToString(),
        //    background.transform.localScale.z.ToString(),
        //    background.transform.localPosition.ToString(),
        //    background.transform.position.ToString(),
        //    (2.0f * background.transform.position.z * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad)).ToString()
        //);
        //background.NumDivisions.ToString(),
        //Multiplier.ToString(),
        //background.transform.parent.localPosition.ToString(), background.transform.parent.position.ToString(),
        //background.transform.parent.parent.localPosition.ToString(), background.transform.parent.parent.position.ToString(),
        //if (background.transform.parent.parent.parent != null)
        //{
        //    str = background.transform.parent.parent.parent.gameObject.name;
        //}
    }
    public void ChangeBackgroundScale()
    {
        tempScaleVec = background.transform.localScale;
        if (Multiplier == 1f)
        {
            tempScaleVec = new Vector3(Screen.width / 2, 1, Screen.height / 2);
            //background.transform.localPosition = new Vector3(0f,0f,1900f);
        }

        tempScaleVec.x *= Multiplier;
        tempScaleVec.z *= Multiplier;

        background.transform.localScale = tempScaleVec;
        Multiplier += 0.05f;
    }

    //var frustumHeight = 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    public void ChangeBackgroundScaleUsingFrustumHeight()
    {
        float frustumHeight = 2.0f * background.transform.localPosition.z * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        Multiplier = frustumHeight / background.transform.localScale.z;

        tempScaleVec = background.transform.localScale;

        Multiplier /= 2f;

        tempScaleVec.x *= Multiplier;
        tempScaleVec.z *= Multiplier;

        AdjustMarkersSize();

        background.transform.localScale = tempScaleVec;
    }
    void AdjustMarkersSize()
    {
        foreach (Transform item in GrandMarker)
        {
            item.localScale *= Multiplier;
        }
    }
    public void ChangeDistance()
    {
        background.transform.position = Vector3.Lerp(
            background.transform.localPosition,
            Vector3.zero,
            0.05f);
    }
}
