using UnityEngine;
using System.Collections;

/// <summary>
/// Manages scene and game state changes.
/// </summary>
public class GameStateManager : Singleton<GameStateManager>
{
	private static GameState state;

	/// <summary>
	/// Sets initial state to GameState.INTRO.
	/// </summary>
	void Start ()
	{
		state = GameState.GAME;
	}

	#region FUNCTIONAL METHODS
	/// <summary>
	/// Gets the state of the game.
	/// </summary>
	/// <returns>The game state.</returns>
	public static GameState GetGameState ()
	{
		return state;
	}

	/// <summary>
	/// Changes the game scene asynchronously and alters the game state accordingly.
	/// </summary>
	/// <param name="sceneIndex">Scene index.</param>
	public static void ChangeScene (int sceneIndex)
	{
		if (sceneIndex < 0 || sceneIndex > Application.levelCount)
			return;
		Application.LoadLevelAsync (sceneIndex);
		state = TranslateSceneToGameState (sceneIndex);

	}
	#endregion

	#region UTILITY METHODS
	/// <summary>
	/// Translates the scene index to the game state appropriate for that scene.
	/// </summary>
	/// <returns>The appropriate game state.</returns>
	/// <param name="sceneIndex">Scene index.</param>
	private static GameState TranslateSceneToGameState (int sceneIndex)
	{
		switch (sceneIndex) {
		case 0:	// MENU
			return GameState.MENU;
		default: // GAME
			return GameState.GAME;
		}
	}
	#endregion
}
