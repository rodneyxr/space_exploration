using UnityEngine;

/// <summary>
/// Provides the player with movement permissions and capabilities.
/// </summary>
public class PlayerMovement : Singleton<PlayerMovement> {
    private Rigidbody playerBody;
    [SerializeField]
    private float
        playerSpeed = 25, playerTurnSpeed = 25, playerBrakePower = 4;
    [SerializeField]
    private bool bodyRotationEnabled = false;
    private float horizontalInput, verticalInput, brakeInput;

    void Awake() {
        playerBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        TakeInput();
        switch (GameStateManager.GetGameState()) {
            case GameState.INTRO:
                // No Movement Permissions
                break;
            case GameState.MENU:
                PlayerTurn();
                break;
            case GameState.GAME:
                PlayerTurn();
                PlayerMove();
                break;
            case GameState.PAUSE:
                PlayerTurn();
                break;
            case GameState.OVER:
                // No Movement Permissions
                break;
        }
    }

    /// <summary>
    /// Applies force to player rigid body in forward direction.
    /// </summary>
    private void PlayerMove() {
        playerBody.AddForce(transform.forward * verticalInput * playerSpeed * Time.deltaTime);
        playerBody.drag = playerBody.angularDrag = brakeInput * playerBrakePower;
    }

    /// <summary>
    /// Applys torque to the player rigid body on the Y axis
    /// </summary>
    private void PlayerTurn() {
        if (bodyRotationEnabled)
            playerBody.AddTorque(0f, horizontalInput * playerTurnSpeed * Time.deltaTime, 0f);
    }

    private void TakeInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        brakeInput = System.Convert.ToSingle(Input.GetButton("Brake"));
    }

}
