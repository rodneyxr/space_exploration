using UnityEngine;
using System.Collections;

public class Accretion : MonoBehaviour
{

	public ParticleSystem system;
	public GameObject heavenlyBody;
	public float dissolveSpeed;
	public float showSpeed, showEmit;
	[SerializeField]
	private bool
		start, finish;

	void Start ()
	{
		start = false;
		finish = false;
	}

	void FixedUpdate ()
	{
		showSpeed = system.startSpeed;
		showEmit = system.emissionRate;
		if (start) {
			system.startSize = Mathf.Lerp (system.startSize, 0, Time.deltaTime / (dissolveSpeed * 5));
			if (system.startSpeed > .2) {
				system.startSpeed = Mathf.Lerp (system.startSpeed, 0, Time.deltaTime / dissolveSpeed);
			} else {
				start = false;
				finish = true;
				heavenlyBody.GetComponent<FadeIn> ().startFadeIn ();
			}
		} else if (finish) {
			system.startSize = Mathf.Lerp (system.startSize, 0, Time.deltaTime / (dissolveSpeed * 2));
			if (system.emissionRate > .2) {
				system.emissionRate = Mathf.Lerp (system.emissionRate, 0, Time.deltaTime / dissolveSpeed);
			} else {
				system.emissionRate = 0;
				this.enabled = false;
			}
		}
	}

	public void startAccrete ()
	{
		start = true;
	}

}
