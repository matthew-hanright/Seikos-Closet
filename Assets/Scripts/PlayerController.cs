using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isGrounded;
    private float maxSpeed = 7;
    private float takeOffSpeed = 7;
    private Vector2 move;
    public GameObject player;
    private float distToGround;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        distToGround = player.GetComponent<BoxCollider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        player.transform.Translate(move);
    }
}
