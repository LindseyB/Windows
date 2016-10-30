using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class StartPlaying : MonoBehaviour {
	void Update () {
        // Wait for space to be hit and then load
        if (Input.GetKeyUp("space")) {
            SceneManager.LoadScene("Levels/main");
        }
	}
}
