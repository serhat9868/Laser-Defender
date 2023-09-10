using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] float shootingVolume = 1f;
    [Header("Damage")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField] float explosionVolume = 1f;
    [Header("Click Sounds")]
    [SerializeField] AudioClip clickClip;
    [SerializeField] float clickVolume = 1f;

   static AudioPlayer instance;
    // Start is called before the first frame update
   
    private void Awake()
    {
        ManageSingleton();
            
    }
    void ManageSingleton()
    {
      //  int instanceCount = FindObjectsOfType(GetType()).Length;
       if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayShootingClip()
    {
        if(shootingClip != null)
        {
            AudioSource.PlayClipAtPoint(shootingClip,
                Camera.main.transform.position,
                shootingVolume);
        }

    }
    public void PlayExplosionClip()
    {
        if(explosionClip != null)
        {
            AudioSource.PlayClipAtPoint(explosionClip,
                Camera.main.transform.position,
                explosionVolume);
        }
    }
    public void PlayClickClip()
    {
        if(clickClip != null)
        {
            AudioSource.PlayClipAtPoint(clickClip, 
                Camera.main.transform.position,
                clickVolume);
        }
    }
}
