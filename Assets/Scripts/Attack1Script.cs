using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Script : MonoBehaviour
{
    //Attack 1 is a projectile

    private int damage = 10;
    public float speed = 40f;
    //The damageMultiplier is used by the player's power which increases damage
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
            collision.gameObject.GetComponent<ParentEnemy>().onHit();
            Object.Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Boss 1")
        {
            collision.gameObject.GetComponent<Boss1>().currentHealth -= damage * damageMultiplier;
            Object.Destroy(this.gameObject);
        }
    }
}
