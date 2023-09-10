using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int health = 100;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool isPlayer;
    [SerializeField] bool applyCameraShake;
    [SerializeField] int score;
    LevelManager levelManager;
    CameraShaker cameraShaker;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    
  void Awake()
    {
        cameraShaker = Camera.main.GetComponent<CameraShaker>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();

    }
    public int GetHealth()
    {
        return health;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
 
                        if (damageDealer != null)
                {
                TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            CameraShake();
            audioPlayer.PlayExplosionClip();

                damageDealer.Hit();
            }
        
        void TakeDamage(int damage)
        {
            health = health - damage;

            if(health <= 0)
            {
                Die();   
                
            }
        }
        void Die()
        {
            if (!isPlayer)
            {
                scoreKeeper.ModifyScore(score);
            }
            else
            {
                levelManager.LoadGameOverScene();
            }
            Destroy(gameObject);
        }
        void PlayHitEffect()
        {
            if(hitEffect != null)
            {
                ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
            }
        }
        void CameraShake()
        {
            if(cameraShaker != null && applyCameraShake)
            {
                cameraShaker.Play();
            }
        }
   
       
    }
}
