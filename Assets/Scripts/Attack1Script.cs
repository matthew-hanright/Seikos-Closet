using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Script : MonoBehaviour
{
    public Rigidbody2D body;
    public float velx = 5f;
    float vely = 0f;

    // Start is called before the first frame update
    void Start()
    {
       body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(velx, vely);
    }
}
