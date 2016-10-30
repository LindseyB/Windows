using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class StartPlaying : MonoBehaviour {
	void Update () {
        if (Input.GetKeyUp("space")) {
            SceneManager.LoadScene("Levels/main");
        }
	}
}
