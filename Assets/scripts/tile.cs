using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour {

    public bool hasPlant;

	void Start () {
        hasPlant = false;
	}
	
	void Update () {
		
	}

    public void plant(GameObject p) {
        GameObject plant = Instantiate(
        p,
        new Vector2(transform.position.x, transform.position.y + 0.2f),
        Quaternion.identity,
        transform
        );
        hasPlant = true;
    }
}
