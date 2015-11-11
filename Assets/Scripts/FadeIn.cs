using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeIn : MonoBehaviour
{

	public Color color;
	public List<Material> materials;
	public List<Light> lights;
	private bool start;

	void Start ()
	{
		start = false;
		foreach (Light light in lights)
			light.intensity = 0f;
		foreach (Material mat in materials) {
			mat.color = Color.black;
			if (mat.HasProperty ("_ColorTint"))
				mat.SetColor ("_ColorTint", Color.black);
		}
	}

	void FixedUpdate ()
	{
		if (start) {
			foreach (Light light in lights)
				light.intensity = Mathf.Lerp (light.intensity, 1f, Time.deltaTime);
			foreach (Material mat in materials) {
				mat.color = Color.Lerp (mat.color, color, Time.deltaTime);
				if (mat.HasProperty ("_ColorTint"))
					mat.SetColor ("_ColorTint", Color.Lerp (mat.GetColor("_ColorTint"), color, Time.deltaTime));
			}
		}
	}

	public void startFadeIn ()
	{
		start = true;
	}
}
