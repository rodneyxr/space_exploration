using UnityEngine;

/// <summary>
/// Provides the player with movement permissions and capabilities.
/// </summary>
public class PlayerMovement : Singleton<PlayerMovement>
{
	public HandController handController = null;
	private Rigidbody playerBody;
	[SerializeField]
	private float
		playerSpeed = 25, playerTurnSpeed = 25, playerBrakePower = 4;
	[SerializeField]
	private bool
		bodyRotationEnabled = false;
	private float horizontalInput, verticalInput, brakeInput;

	void Awake ()
	{
		playerBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		TakeInput ();
		switch (GameStateManager.GetGameState ()) {
		case GameState.INTRO:
                // No Movement Permissions
			break;
		case GameState.MENU:
			PlayerTurn ();
			break;
		case GameState.GAME:
			PlayerTurn ();
			PlayerMove ();
			break;
		case GameState.PAUSE:
			PlayerTurn ();
			break;
		case GameState.OVER:
                // No Movement Permissions
			break;
		}
	}

	/// <summary>
	/// Applies force to player rigid body in forward direction.
	/// </summary>
	private void PlayerMove ()
	{
		playerBody.AddForce (transform.forward * verticalInput * playerSpeed * Time.deltaTime);
		playerBody.drag = playerBody.angularDrag = brakeInput * playerBrakePower;
	}

	/// <summary>
	/// Applys torque to the player rigid body on the Y axis
	/// </summary>
	private void PlayerTurn ()
	{
		if (bodyRotationEnabled)
			playerBody.AddTorque (0f, horizontalInput * playerTurnSpeed * Time.deltaTime, 0f);
	}

	private void TakeInput ()
	{
		horizontalInput = Input.GetAxis ("Horizontal");
		verticalInput = Input.GetAxis ("Vertical");
		brakeInput = System.Convert.ToSingle (Input.GetButton ("Brake"));

		// handle leap input
		if (handController == null)
			return;
		// Move forward if both palms are facing outwards! Whoot!
		HandModel[] hands = handController.GetAllGraphicsHands ();
		if (hands.Length > 1) {
			Vector3 direction0 = (hands [0].GetPalmPosition () - handController.transform.position).normalized;
			Vector3 normal0 = hands [0].GetPalmNormal ().normalized;
			
			Vector3 direction1 = (hands [1].GetPalmPosition () - handController.transform.position).normalized;
			Vector3 normal1 = hands [1].GetPalmNormal ().normalized;
			
			if (Vector3.Dot (direction0, normal0) > direction0.sqrMagnitude * 0.5f && Vector3.Dot (direction1, normal1) > direction1.sqrMagnitude * 0.5f) {
				// this means the player should move forward
				verticalInput = 1f;
				Debug.Log("Move forward");
			}
		}
	}
	
}
