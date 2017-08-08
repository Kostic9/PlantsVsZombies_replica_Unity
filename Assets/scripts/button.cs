using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {

    public int plantID;
    public int index;
    private float speed = 100f;
    private static int[] costs = new int[4] {1, 2, 2, 3};

    canvas can;
    gameManager gm;

	void Awake () {
        plantID = Random.Range(0,4);        
	}

    void Start() {
        can = GameObject.Find("Canvas").GetComponent<canvas>();
        gm = GameObject.Find("gamemanager").GetComponent<gameManager>();    
        index = 0;
    }
	
	void Update () {
        manageMovement();
	}

    public void clicked() {
        if (gm.SunCount >= costs[plantID]) {
            gm.setPlantID(plantID);
            gm.SunCount -= costs[plantID];
            can.updateSunCount(gm.SunCount);
            can.killButton(this);
        }        
    }

    private void manageMovement() {
        if (index > 0 && index < can.btnPositions.Length) {
            transform.position = Vector2.MoveTowards(
            transform.position,
            can.btnPositions[index],
            speed * Time.deltaTime
            );
        }
        if (transform.position.y <= - Screen.height/10f) {
            transform.position = can.btnPositions[0];
            index = 0;
            can.resetButton(this);
            can.shiftButtons();
        }
    }
}
