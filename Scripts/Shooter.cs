using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefabs;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float basedFireRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float fireVariance;
    [SerializeField] float minFireRate = 0.1f;
    AudioPlayer audioPlayer;


   [HideInInspector] public bool IsFiring;
    Coroutine firingCoroutine;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (useAI)
        {
            IsFiring = true;
        }
    }
      public float GetFireRate()
    {
        return basedFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }
    void Fire()
    {
        if (IsFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());

        }
        else if(!IsFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefabs,
                transform.position,
                Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;   
            }

            Destroy(instance, projectileLifetime);

            float TimeToNextProjectile = Random.Range(basedFireRate - fireVariance,
                basedFireRate + fireVariance);
            TimeToNextProjectile = Mathf.Clamp(TimeToNextProjectile, minFireRate, float.MaxValue);
            audioPlayer.PlayShootingClip();
     

            yield return new WaitForSeconds(TimeToNextProjectile);
        }
        }

    }

