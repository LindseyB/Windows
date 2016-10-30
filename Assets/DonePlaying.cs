using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class DonePlaying : MonoBehaviour {
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("LoadMainMenu");
    }

    private IEnumerator LoadMainMenu() {
        yield return new WaitForSeconds(audioSource.clip.length+1);
        SceneManager.LoadScene("menu");
    }
}
