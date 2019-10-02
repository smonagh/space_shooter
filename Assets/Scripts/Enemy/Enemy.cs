using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour{
    [SerializeField] float health = 100;
    [SerializeField] int score = 10;
    [SerializeField] float minTimeBetween = 0.5f;
    [SerializeField] float maxTimeBetween = 2f;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] float durExplosion = 1f;
    [SerializeField] float deathSFXVol = 0.3f;
    [SerializeField] float shootSFXVol = 0.3f;
    [SerializeField] GameObject shootPrefab;
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;

    private float shotCounter;

    void Start(){
        shotCounter = Random.Range(minTimeBetween, maxTimeBetween);
    }

    void Update(){
        CountDownAndShoot();
    }

    private void CountDownAndShoot(){
        shotCounter -= Time.deltaTime;

        if (shotCounter < 0){
            Fire();
            shotCounter = Random.Range(minTimeBetween, maxTimeBetween);
        }
    }

    private void Fire(){
      GameObject bullet = Instantiate(shootPrefab,
                                      new Vector2(transform.position.x, transform.position.y - 0.2f),
                                      Quaternion.identity) as GameObject;
      bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);
      AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVol);
    }

    private void OnTriggerEnter2D(Collider2D other){
      if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<DamageDealer>()){
          DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
          health -= damageDealer.GetDamage();

          Destroy(other.gameObject);
          if (health <= 0){
              FindObjectOfType<GameSession>().AddToScore(score);
              Die();
        }
      } else if(other.gameObject.tag == "Player" && !other.gameObject.GetComponent<DamageDealer>()){
          PlayerMovement player = FindObjectOfType<PlayerMovement>();
          player.health = 0;
          FindObjectOfType<GameSession>().UpdateHealth(player.health);
          player.Die();
      }
    }

    public void Die(){
      Destroy(gameObject);
      GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
      Destroy(explosion, durExplosion);
      AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVol);
    }
}
