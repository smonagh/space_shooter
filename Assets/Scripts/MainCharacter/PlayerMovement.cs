using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject shootPrefab;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip dieSFX;
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameSession gameLevel;

    private Rigidbody2D rigidBody;
    private Collider2D collider;

    public float health = 100f;
    private float speed = 500f;
    private float bulletSpeed = 10f;
    private float movementHorizontal;
    private float movementVertical;
    private float firingPeriod = 0.2f;
    private bool playerDead = false;
    private Coroutine firingCoroutine;

    private void Start(){
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();

    }

    private void Update(){
        if (!playerDead){
          MovePlayer();
          Fire();
        }
    }

    private void MovePlayer(){
        movementHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        movementVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime * 0.5f;

        rigidBody.velocity = new Vector2(movementHorizontal, movementVertical);
    }

    private void Fire(){
        if (Input.GetButtonDown("Fire1")){
            firingCoroutine = StartCoroutine(SpawnBullet());
        }

        if (Input.GetButtonUp("Fire1")){
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator SpawnBullet(){

      while (true){
          GameObject bullet = Instantiate(shootPrefab,
                                          new Vector2(transform.position.x, transform.position.y + 1),
                                          Quaternion.identity) as GameObject;
          bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
          yield return new WaitForSeconds(firingPeriod);
        }
    }

    private void OnCollisionEnter2D(Collision2D other){

      if (other.gameObject.tag == "Enemy" && !playerDead){
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, 0.5f);

        health -= damageDealer.GetDamage();
        FindObjectOfType<GameSession>().UpdateHealth(health);

        Destroy(other.gameObject);
        if (health <= 0){
            Die();
        }
      }

      }

      public void Die(){
        StartCoroutine(EndGame());
      }

      private IEnumerator EndGame(){
          AudioSource.PlayClipAtPoint(dieSFX, Camera.main.transform.position, 1f);
          GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
          gameObject.GetComponent<SpriteRenderer>().enabled = false;
          DestroyObject(gameObject.GetComponent<Rigidbody2D>());
          playerDead = true;
          yield return new WaitForSeconds(3);

          SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
          FindObjectOfType<GameSession>().ResetGame();
  }
}
