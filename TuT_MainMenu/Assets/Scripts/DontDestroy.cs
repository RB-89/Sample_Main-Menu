using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

    public static DontDestroy duplicate;

	void Start()
	{
        if (duplicate)
            //Destroy the game object this script is attached to if it already exists
            DestroyImmediate(gameObject);
        else
        {
            //Causes UI object not to be destroyed when loading a new scene.
            DontDestroyOnLoad(gameObject);
            duplicate = this;
        }
	}

	

}
