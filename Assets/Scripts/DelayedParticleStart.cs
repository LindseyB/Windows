using UnityEngine;
using System.Collections;

public class DelayedParticleStart : MonoBehaviour {
    private ParticleSystem ps;

    void Start() {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine("StartParticleSystem");
    }

    private IEnumerator StartParticleSystem() {
        yield return new WaitForSeconds(86.3f); // Delay until City
        ps.Play();
    }
}
