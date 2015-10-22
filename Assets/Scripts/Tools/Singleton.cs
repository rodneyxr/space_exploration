using UnityEngine;
using System.Collections;

/// <summary>
/// This class allows for the associated game object to do two things:
/// 	1. Persist between scenes. Changing to another scene will not destroy this object.
/// 	2. Remain the only object of its kind in the scene.
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    public static T Instance {
        get; private set;
    }

    /// <summary>
    /// Checks to see if an instance of this class already exists.
    /// If so, this gameobject is destroyed, but if not, this game
    /// object becomes the singleton instance.
    /// </summary>
    void Awake() {
        if (Instance != null) {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}
