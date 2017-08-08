using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class canvas : MonoBehaviour {
    
    public Button[] buttons;
    public Sprite[] plantIcons;
    public Sprite[] plantSprites;
    [HideInInspector]
    public Vector2[] btnPositions;
    [HideInInspector]
    public Text sunCountText;
    public Image gameover;
    public Text textFinal;
    public Text killstext;

    void Start () {
        gameover.gameObject.SetActive(false);
        btnPositions = new Vector2[buttons.Length + 1];
		for (int i = 0; i < buttons.Length; i++) {
            btnPositions[i] = buttons[i].transform.position;
            resetButton(buttons[i].GetComponent<button>());
            if (i > 0) {
                buttons[i].transform.position = btnPositions[0];
            }
        }
        btnPositions[buttons.Length] = new Vector2(btnPositions[0].x, -Screen.height/5f);
	}
		
	void Update () {
		
	}

    public void updateSunCount(int amount) {
        sunCountText.text = amount.ToString();
    } 

    public void addButton() {
        foreach (Button b in buttons) {
            b.GetComponent<button>().index++;
            if (b.GetComponent<button>().index == 1) {
                return;
            }
        }
    }

    public void shiftButtons() {
        Button temp = buttons[0];
        for (int i = 0; i < buttons.Length - 1; i++) {
            buttons[i] = buttons[i + 1];
        }
        buttons[buttons.Length - 1] = temp;
    }

    public void resetButton(button b) {                
        int chance = Random.Range(0, 100);
        if (chance < 35) {
            b.plantID = 0;
        } else if (chance < 70) {
            b.plantID = 1;
        } else if (chance < 85) {
            b.plantID = 2;
        } else {
            b.plantID = 3;
        }
        b.GetComponent<Image>().sprite = plantIcons[b.plantID];
        b.GetComponent<Image>().enabled = true;
        b.GetComponent<Button>().interactable = true;
    }

    public void killButton(button b) {
        b.GetComponent<Image>().enabled = false;
        b.GetComponent<Button>().interactable = true;
    } 

    public void gameOver() {
        Time.timeScale = 0;
        gameover.gameObject.SetActive(true);
        textFinal.text = "GAME OVER";
    }

    public void lvlCompleted() {
        Time.timeScale = 0;
        gameover.gameObject.SetActive(true);
        textFinal.text = "LEVEL\r\nCOMPLETED";
    }

    public void updateK(int k1, int k2) {
        killstext.text = string.Format("Kills: {0}/{1}", k1, k2);           
    }

    public void restart () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
