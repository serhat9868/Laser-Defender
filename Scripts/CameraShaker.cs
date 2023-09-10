using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] float shakeMagnitude = 0.5f;
    [SerializeField] float shakeDuration = 1f;
  
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
      
        initialPosition = transform.position;
    }
    public void Play()
    {
        StartCoroutine(Shake());

    }
    IEnumerator Shake()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime = elapsedTime + Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
       
        
    }
}
