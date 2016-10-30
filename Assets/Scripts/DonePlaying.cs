using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class DonePlaying : MonoBehaviour {
    private AudioSource audioSource;

	void Start () {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("LoadMainMenu");
    }

    private IEnumerator LoadMainMenu() {
        yield return new WaitForSeconds(audioSource.clip.length+1); // Wait until the audio is over (plus some silence)
        SceneManager.LoadScene("menu");                             // Jump back to the main menu
    }
}
