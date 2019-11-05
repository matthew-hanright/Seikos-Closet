using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatterTurning : MonoBehaviour
{
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
        if (collision.gameObject.tag == "Solid" && collision.gameObject.layer == 11)
        {
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                GetComponentInParent<ChatterAI>().movingLeft = false;
                GetComponentInParent<SpriteRenderer>().transform.localScale = new Vector2(
                    -Mathf.Abs(GetComponentInParent<SpriteRenderer>().transform.localScale.x),
                    GetComponentInParent<SpriteRenderer>().transform.localScale.y);
            }
            else
            {
                GetComponentInParent<ChatterAI>().movingLeft = true;
                GetComponentInParent<SpriteRenderer>().transform.localScale = new Vector2(
                    Mathf.Abs(GetComponentInParent<SpriteRenderer>().transform.localScale.x),
                    GetComponentInParent<SpriteRenderer>().transform.localScale.y);
            }
        }
    }
}
