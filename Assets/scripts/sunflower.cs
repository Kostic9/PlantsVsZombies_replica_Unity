using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunflower : plant {

    private float sunTime = 8f;
    public GameObject p_sun;

	void Start () {
		
	}
	
	void Update () {
        timer += Time.deltaTime;
        if (timer >= sunTime) {
            timer = 0f;
            spawnSun();
        } 
	}

    private void spawnSun() {
        GameObject sun = Instantiate(
        p_sun,
        new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)),
        Quaternion.identity,
        transform
        );
        sun.GetComponent<sun>().sunflowerMade = true;
    }
}
