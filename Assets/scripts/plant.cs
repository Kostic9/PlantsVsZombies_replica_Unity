using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class plant : MonoBehaviour {

    [SerializeField]
    protected int hp;
    protected float timer;
    public GameObject smoke;

	void Start () {
		
	}
	
	void Update () {
        
	}

    public void updateHp(int amount) {
        hp += amount;
        if (hp <= 0) {
            Instantiate(smoke, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
