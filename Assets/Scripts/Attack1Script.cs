﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Script : MonoBehaviour
{
    private int damage = 10;
    public float speed = 40f;
    public int damageMultiplier = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Solid")
        {
            Object.Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ParentEnemy>().health -= damage * damageMultiplier;
            Object.Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Boss1")
        {
            collision.gameObject.GetComponent<Boss1>().currentHealth -= damage * damageMultiplier;
            Object.Destroy(this.gameObject);
        }

    }
}
