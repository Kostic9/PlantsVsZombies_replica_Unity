using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

    private int lawn_height = 5;
    private int lawn_width = 9;
    private int c = 0;
    private int sunCount;
    private int plantActiveID = -1;
    private Vector2[] lanePositions;

    private int kills;
    private int killsTotal = 50;

    public int SunCount {
    get { return sunCount; } 
    set { sunCount = value;
    if (sunCount < 0) {
                sunCount = 0;
    }
    }
    }

    public GameObject p_tile;
    public GameObject p_sunflower;
    public GameObject p_pea;
    public GameObject p_freezer;
    public GameObject p_nut;
    public GameObject p_sun;
    public GameObject p_zombie;

    public GameObject canvas;
    public GameObject plantTemp;

    public Color c1;
    public Color c2;    

	void Start () {
        Time.timeScale = 1f;
        sunCount = 0;
        lanePositions = new Vector2[5];
        canvas.GetComponent<canvas>().updateSunCount(sunCount);
        canvas.GetComponent<canvas>().updateK(kills, killsTotal);
        generateLawn();
        InvokeRepeating("spawnSun", 5f, 5f);
        InvokeRepeating("AddButton", 5f, 5f);
        InvokeRepeating("spawnZombie", 7f, 5f);
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            mouseClick();
        }
        mouseHover();
        plantTempMng();
	}

    private void plantTempMng() {
        if (plantActiveID > -1) {
            plantTemp.GetComponent<SpriteRenderer>().enabled = true;
            plantTemp.GetComponent<SpriteRenderer>().sprite = canvas.GetComponent<canvas>().plantSprites[plantActiveID];
        } else {
            plantTemp.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void generateLawn() {
        for (int i = 0; i < lawn_height; i++) {
            for (int j = 0; j < lawn_width; j++) {
                GameObject tile = Instantiate(
                p_tile,
                new Vector2((float)j - lawn_width/2, (float)i - lawn_height/2),
                Quaternion.identity,
                transform);
                tile.GetComponent<SpriteRenderer>().color = c % 2 == 0 ? c1 : c2;
                c++;
                if (j == 0) {
                    lanePositions[i] = new Vector2(tile.transform.position.x + 15f, tile.transform.position.y + 0.2f);
                }
            }           
        }
    }

    private void spawnZombie() {
        GameObject zombie = Instantiate(
        p_zombie,
        lanePositions[Random.Range(0, 5)],
        Quaternion.identity,
        transform
        );
    }

    private void mouseClick() {
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.gameObject.tag == "tile" && plantActiveID > -1 && !hit.transform.gameObject.GetComponent<tile>().hasPlant) {
                hit.transform.gameObject.GetComponent<tile>().plant(getPlant(plantActiveID));
                plantActiveID = -1;
            } else if (hit.transform.gameObject.tag == "sun") {
                hit.transform.gameObject.GetComponent<sun>().death();
                addSun(1);
                canvas.GetComponent<canvas>().updateSunCount(sunCount);
            }
        }
    }

    private void mouseHover() {
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.gameObject.tag == "tile") {
                plantTemp.transform.position = new Vector3(
                hit.transform.position.x,
                hit.transform.position.y + 0.2f,
                0
                );                
            } 
        }
        else {
            plantTemp.transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 0.2f,
            0
            );            
        }
    }

    private void spawnSun() {
        float x = Random.Range(-5f, 5f);
        GameObject sun = Instantiate(
        p_sun,
        new Vector2(x, 5f),
        Quaternion.identity,
        transform
        );
    }

    private void AddButton() {
        canvas.GetComponent<canvas>().addButton();
    }

    private GameObject getPlant(int id) {
        switch (id) {
            case 0:return p_sunflower;
            case 1:return p_pea;
            case 2:return p_freezer;
            case 3:return p_nut;
            default: return p_sunflower;
        }
    }

    public void addSun(int amount) {
        sunCount += amount;
    }

    public void setPlantID(int id) {
        plantActiveID = id;
    }

    public void updateKills() {
        canvas.GetComponent<canvas>().updateK(kills, killsTotal);
        if (kills >= killsTotal) {
            canvas.GetComponent<canvas>().lvlCompleted();
        }
    }

    public void addKill() {
        kills++;
    }
}
