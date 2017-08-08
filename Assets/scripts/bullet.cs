using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    private float speed = 5f;

	void Start () {
		
	}
	
	void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
	}
}
