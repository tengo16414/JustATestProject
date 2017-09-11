using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class Pixel : MonoBehaviour
{
	private float m_ScreenWidth = 1920;

	void Awake()
	{
		float _Ratio = (float)Screen.height / (float)Screen.width;
		float _ScreenHeight =  m_ScreenWidth * _Ratio;
		float _Size = _ScreenHeight / 200;
		//Camera.main.orthographicSize = _Size;
        Camera.main.orthographic = true;
	}

	void Update()
	{
		Awake();
	}
}