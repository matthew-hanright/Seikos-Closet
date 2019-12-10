using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMeleeController : MonoBehaviour
{
    public Sprite[] sprite;
    private float frameStartTime;
    private float frameRate = 0.24f;
    private int currentFrame = 0;
    private int damage = 15;
    public int damageMultiplier = 1;

    private List<float> ignoreEnemy;

    // Start is called before the first frame update
    void Start()
    {
        frameStartTime = Time.time;
        ignoreEnemy = new List<float> { 0f };
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > frameStartTime + frameRate)
        {
            frameStartTime = Time.time;
            currentFrame++;
            if (currentFrame == sprite.Length)
            {
                Destroy(this.gameObject);
            }
            else if (currentFrame < sprite.Length)
            {
                GetComponent<SpriteRenderer>().sprite = sprite[currentFrame];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool alreadyHit = false;
        if (collision.tag == "Enemy")
        {
            if (ignoreEnemy.Contains(collision.gameObject.GetInstanceID()))
            {
                alreadyHit = true;
            }
            if (!alreadyHit)
            {
                collision.gameObject.GetComponent<ParentEnemy>().health -= damage * damageMultiplier;
                ignoreEnemy.Add(collision.gameObject.GetInstanceID());
            }
        }
        else if (collision.gameObject.tag == "Boss1")
        {
            if (ignoreEnemy.Contains(collision.gameObject.GetInstanceID()))
            {
                alreadyHit = true;
            }
            if (!alreadyHit)
            {
                collision.gameObject.GetComponent<Boss1>().currentHealth -= damage * damageMultiplier;
                ignoreEnemy.Add(collision.gameObject.GetInstanceID());
            }
        }
    }
}
