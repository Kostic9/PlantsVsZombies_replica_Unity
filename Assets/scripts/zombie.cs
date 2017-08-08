using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour {

    private float speed = 0.5f;
    private int hp;
    private bool frozen;
    private float frozenTimer;
    private float attackTimer;
    private float attackCooldown = 1f;
    private bool move;

    public GameObject smoke;

    private GameObject gm;

	void Start () {
        move = true;
        hp = Random.Range(4, 8);
        gm = GameObject.Find("gamemanager");
	}
	
	void Update () {
        if (move) {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }        
        if (frozen) {
            speed = 0.2f;
            frozenTimer += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1f, 1f);
            if (frozenTimer >= 10f) {
                frozenTimer = 0f;
                frozen = false;
            }
        } else {
            speed = 0.5f;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        if (checkPath()) {
            GetComponent<Animator>().SetBool("eating", true);
            move = false;
        } else {
            GetComponent<Animator>().SetBool("eating", false);
            move = true;
        }
        if (transform.position.x < Camera.main.ScreenToWorldPoint(new Vector2(0,0)).x) {
            GameObject.Find("Canvas").GetComponent<canvas>().gameOver();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet") {
            hp--;
            if (hp <= 0) {
                Instantiate(smoke, transform.position, Quaternion.identity);
                gm.GetComponent<gameManager>().updateKills();
                gm.GetComponent<gameManager>().addKill();
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        } else if (collision.tag == "bullet_ice") {
            frozen = true;
            frozenTimer = 0f;
            Destroy(collision.gameObject);
        }
    }

    private bool checkPath() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 0.2f, 1 << 9);
        if (hit) {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown) {
                hit.transform.gameObject.GetComponent<plant>().updateHp(-1);
                attackTimer = 0f;
            }            
        }        
        return hit;
    }
}
