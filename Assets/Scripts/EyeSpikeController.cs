using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EyeSpikeController : MonoBehaviour
{
    private int damage = 15;
    private Vector2 knockback = new Vector2(5f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            try
            {
                collision.GetComponent<PlayerController>().takeDamage(damage, knockback);
            }
            catch (Exception e)
            {
                print(e);
            }
        }
    }
}
