using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatterPlatformEndTurn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Solid")
        {
            print("Solid left");
            if (GetComponentInParent<ChatterAI>().movingLeft &&
                (collision.gameObject.transform.position.x > transform.position.x) &&
                (collision.gameObject.transform.position.y < transform.position.y))
            {
                GetComponentInParent<ChatterAI>().movingLeft = false;
                if (Mathf.Sign(GetComponentInParent<SpriteRenderer>().transform.localScale.x) == 1)
                {
                    GetComponentInParent<Transform>().localScale = new Vector2(
                        -GetComponentInParent<Transform>().localScale.x,
                        GetComponentInParent<Transform>().localScale.y);
                }
            }
            else if ((collision.gameObject.transform.position.x < transform.position.x) &&
                (collision.gameObject.transform.position.y < transform.position.y))
            {
                GetComponentInParent<ChatterAI>().movingLeft = true;
                GetComponentInParent<Transform>().localScale = new Vector2(
                    Mathf.Abs(GetComponentInParent<Transform>().localScale.x),
                    GetComponentInParent<Transform>().localScale.y);
            }
        }
    }
}
