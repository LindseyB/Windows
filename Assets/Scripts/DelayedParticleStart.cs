using UnityEngine;
using System.Collections;

public class DelayedParticleStart : MonoBehaviour {
    private ParticleSystem ps;

    // Use this for initialization
    void Start() {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine("StartParticleSystem");
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator StartParticleSystem() {
        yield return new WaitForSeconds(86.3f);
        ps.Play();
    }
}
