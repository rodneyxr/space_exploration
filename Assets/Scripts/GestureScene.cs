using UnityEngine;
using System.Collections;

public class GestureScene : MonoBehaviour {
	

	public bool push = false;
	public bool pull = false;
	public bool doubleoutwardswipe = false;
	public bool clap = false;

	public AudioClip Clapping;
	public AudioClip Pushing;
	public AudioClip Pulling;
	public AudioClip Outing;
	public TextMesh textObject;
	
	// Use this for initialization
	void Start () {
		ELGManager.GestureRecognised += onGestureRecognised;

		ELGManager.pushGestureRegistered = push;
		ELGManager.pullGestureRegistered = pull;
		ELGManager.doubleOutwardsSwipeGestureRegistered = doubleoutwardswipe;
		ELGManager.clapGestureRegistered = clap;

	}
	
	void OnDestroy() {
		ELGManager.GestureRecognised -= onGestureRecognised;
	}
	
//	void onGestureRecognised(EasyLeapGesture gesture) {
//		if (ELGManager.clapGestureRegistered == true) {
//			Debug.Log("clap");
//
//		}
//	}

	void onGestureRecognised(EasyLeapGesture gesture) {


		if (gesture.Type.Equals(EasyLeapGestureType.CLAP)) {
//			print("Clap detected");
			AudioSource.PlayClipAtPoint (Clapping, transform.position, 1.0f);

			textObject.text = "[Clap detected]";
		}
		 if (gesture.Type.Equals(EasyLeapGestureType.PULL)) {
//			print("Pull deteceted");
			AudioSource.PlayClipAtPoint (Pulling, transform.position, 1.0f);
			textObject.text = "[Pull detected]";
		}
		 if (gesture.Type.Equals(EasyLeapGestureType.PUSH)) {
//			print("Push deteceted");
			AudioSource.PlayClipAtPoint (Pushing, transform.position, 1.0f);
			textObject.text = "[Push detected]";
		}
		 if (gesture.Type.Equals(EasyLeapGestureType.DOUBLE_SWIPE_OUT)) {
//			print("DoubleSwipeOut deteceted");
			AudioSource.PlayClipAtPoint (Outing, transform.position, 1.0f);
			textObject.text = "[Double-Swipe-Out detected]";
		}


//		AudioSource.PlayClipAtPoint (Clapping, transform.position, 1.0f);

	}
	
	void Update() {

		ELGManager.pushGestureRegistered = push;
		ELGManager.pullGestureRegistered = pull;
		ELGManager.doubleOutwardsSwipeGestureRegistered = doubleoutwardswipe;
		ELGManager.clapGestureRegistered = clap;

	}
}
