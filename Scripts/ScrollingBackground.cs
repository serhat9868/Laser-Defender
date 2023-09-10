using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
        Vector2 offset;
    Material material;
    
    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;

    }
}
