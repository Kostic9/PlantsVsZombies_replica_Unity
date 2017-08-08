using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : plant {

    private float shootSpeed = 1f;
    public GameObject p_bullet;
    public Transform bulletStart;

	void Start () {
		
	}
	
	void Update () {
        timer += Time.deltaTime;
        if (timer >= shootSpeed && checkPath()) {
            timer = 0;
            shoot();        
        }
	}

    private void shoot() {
        GameObject bullet = Instantiate(p_bullet, bulletStart.position, Quaternion.identity, transform);
    }

    private bool checkPath() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 6f, 1 << 8);
        return hit;
    }
}
