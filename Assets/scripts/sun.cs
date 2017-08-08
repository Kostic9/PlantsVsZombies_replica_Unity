using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour {

    private float fallSpeed = 1f;
    private float timer;
    private float lifeSpan = 10f;
    private bool fade;
    private float fadeSpeed = 0.05f;
    public bool sunflowerMade;

	void Start () {
        fade = false;
	}
	
	void Update () {
        timer += Time.deltaTime;
        if (timer >= lifeSpan) {
            death();
        }
        if (!sunflowerMade) {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }        
        if (fade) {
            Color myColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(
                myColor.r,
                myColor.g,
                myColor.b,
                myColor.a - fadeSpeed
            );
            if (myColor.a <= 0) {
                Destroy(gameObject);
            }
        }
	}

    public void death() {
        fade = true;
    }
}
