using UnityEngine;
using System.Collections;

public class BackgroundTransitions : MonoBehaviour {
    private Camera cameraRef;

    void Start() {
        cameraRef = GetComponent<Camera>();
        StartCoroutine("ShiftSkyColors");
    }

    private IEnumerator ShiftSkyColors() {
        yield return new WaitForSeconds(37.3f);
        cameraRef.backgroundColor = new Color32(64, 64, 64, 255); // Ohio
        yield return new WaitForSeconds(26.0f);
        cameraRef.backgroundColor = new Color32(40, 30, 40, 255); // Parents
        yield return new WaitForSeconds(23.0f);
        cameraRef.backgroundColor = new Color32(53, 45, 60, 255); // City
    }
}
