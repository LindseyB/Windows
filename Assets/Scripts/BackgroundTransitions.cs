using UnityEngine;
using System.Collections;

public class BackgroundTransitions : MonoBehaviour {
    private Camera camera;

    // Use this for initialization
    void Start() {
        camera = GetComponent<Camera>();
        StartCoroutine("ShiftSkyColors");
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator ShiftSkyColors() {
        yield return new WaitForSeconds(37.3f);
        camera.backgroundColor = new Color32(64, 64, 64, 255);
        yield return new WaitForSeconds(26.0f);
        camera.backgroundColor = new Color32(40, 30, 40, 255);
        yield return new WaitForSeconds(23.0f);
        camera.backgroundColor = new Color32(53, 45, 60, 255);
    }
}
