using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuforiaBackgroundSizePrinter : MonoBehaviour
{

    public Vuforia.BackgroundPlaneBehaviour background;
    public UnityEngine.UI.Text targetText;

    public float Multiplier;
    private Vector3 tempScaleVec;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //ChangeDistance();
            ChangeBackgroundScale();
        }
        

        string str = string.Format(
            "{0}\nX: {1} Y: {2} Z: {3}\n{4}\n{5}\n{6}",
            //background.NumDivisions.ToString(),
            Multiplier.ToString(),
            background.transform.localScale.x.ToString(),
            background.transform.localScale.y.ToString(),
            background.transform.localScale.z.ToString(),
            background.transform.localPosition.ToString(),
            background.transform.parent.localPosition.ToString(),
            background.transform.parent.parent.localPosition.ToString()
            );
        targetText.text = str;
    }
    public void ChangeBackgroundScale()
    {
        tempScaleVec = background.transform.localScale;
        if (Multiplier == 1f)
        {
            tempScaleVec = new Vector3(Screen.width / 2, 1, Screen.height  / 2);
            //background.transform.localPosition = new Vector3(0f,0f,1900f);
        }

        tempScaleVec.x *= Multiplier;
        tempScaleVec.z *= Multiplier;

        background.transform.localScale = tempScaleVec;
        Multiplier += 0.05f;
    }
    public void ChangeDistance()
    {
        background.transform.position = Vector3.Lerp(
            background.transform.localPosition,
            Vector3.zero,
            0.05f);
    }
}
