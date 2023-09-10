using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingUp;
    [SerializeField] float paddingDown;
 

    Vector2 minBounds;
    Vector2 maxBounds;

    Vector2 rawInput;
    Shooter shooter;
   void Awake()
    {
        shooter = GetComponent<Shooter>();     
    }
    void Start()
    {
        InItBounds();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void InItBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }
     void Move()
    {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingDown, maxBounds.y - paddingUp);
        transform.position += delta;
    }
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }
    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.IsFiring = value.isPressed;
        }
        else
        {
            shooter.IsFiring = false;
        }
    }
}
