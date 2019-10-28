using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Script : MonoBehaviour
{
    public GameObject mainCamera;
    public Rigidbody2D body;
    public float velx = 5f;
    private int damage = 10;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Solid")
            Object.Destroy(this.gameObject);

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ParentEnemy>().health -= damage;
            print("health = " + collision.gameObject.GetComponent<ParentEnemy>().health);
            Object.Destroy(this.gameObject);
        }
    }
}
