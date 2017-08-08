using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour {

	void Start () {
        StartCoroutine(death());
	}

    IEnumerator death() {
        yield return new WaitForSeconds(0.38f);
        Destroy(gameObject);
    }
}
