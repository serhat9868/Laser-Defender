using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boosters : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject healthBooster;
    [SerializeField] GameObject fireRateBooster;
    [SerializeField] GameObject fireDamageBooster;

    [Header("Confirmations")]
    [SerializeField] bool isHealthBooster;
    [SerializeField] bool isFireRateBooster;
    [SerializeField] bool isDamageBooster;

    [Header("Settings")]
    [SerializeField] float spawnWait;
    [SerializeField] float waveWait;
    [SerializeField] float startWait;
    [SerializeField] float boosterSpeeds;
    Transform movingPoints;

    Health health;
    DamageDealer damageDealer;
    Shooter shooter;
    // Start is called before the first frame update
    void Awake()
    {
        health = FindObjectOfType<Health>();
        damageDealer = FindObjectOfType<DamageDealer>();
        shooter = FindObjectOfType<Shooter>();
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        health = collision.GetComponent<Health>();
        damageDealer = collision.GetComponent<DamageDealer>();
        shooter = collision.GetComponent<Shooter>();
        if(health != null)
        {
            FullyHealth(100);
        }
        if(damageDealer != null)
        {
            Damage(40);
        }
        if(shooter != null)
        {
            FireRate(0.05f);
        }
    }

 
    public float FireRate(float value)
    {
        Instantiate(fireRateBooster, movingPoints.position, Quaternion.identity, transform);
        float newFireRate = shooter.GetFireRate();
        newFireRate = 0.1f;
        return newFireRate;
    }
    public int Damage(int value)
    {
        int highDamage = damageDealer.GetDamage();
        return highDamage;
    }
    public int FullyHealth(int value)
    {
        int fullyhealth = health.GetHealth();

        return fullyhealth;
    }



    IEnumerator HealthBoost()
    {
        yield return new WaitForSeconds(startWait);
        while (isHealthBooster)
        {
            for (int i = 0; i < spawnWait; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(4, 9), -9, 0);
                Instantiate(healthBooster, spawnPosition, Quaternion.identity);

                yield return new WaitForSeconds(spawnWait);
            }
        }

            yield return new WaitForSeconds(waveWait);
    }
    IEnumerator FireRateBoost()
    {
        yield return new WaitForSeconds(startWait);
        while (isFireRateBooster)
        {
            for (int i = 0; i < spawnWait; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(4, 9), -9, 0);
                Instantiate(fireRateBooster, spawnPosition, Quaternion.identity);

                yield return new WaitForSeconds(spawnWait);
            }
        }

        yield return new WaitForSeconds(waveWait);
    }
    IEnumerator DamageBoost()
    {
        yield return new WaitForSeconds(startWait);
        while (isDamageBooster)
        {
            for (int i = 0; i < spawnWait; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(4, 9), -9, 0);
                Instantiate(fireDamageBooster, spawnPosition, Quaternion.identity);

                yield return new WaitForSeconds(spawnWait);
            }
        }

        yield return new WaitForSeconds(waveWait);
    }
}
