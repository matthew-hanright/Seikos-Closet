using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossRoomEnter : MonoBehaviour
{
    public Boss1 boss;
    public Camera dungeonCamera;
    public UIDungeonScript dungeonUI;
    public Camera bossCamera;
    public UIDungeonScript bossUI;
    private float scaleMultiplier = 1.1f;
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
            if(collision.transform.position.x < transform.position.x)
            {
                dungeonCamera.gameObject.SetActive(false);
                bossCamera.gameObject.SetActive(true);
                boss.enabled = true;
                collision.GetComponent<PlayerController>().UIDungeon = bossUI;
            }
            else
            {
                dungeonCamera.gameObject.SetActive(true);
                bossCamera.gameObject.SetActive(false);
                boss.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                boss.isMoving = false;
                boss.enabled = false;
                collision.GetComponent<PlayerController>().UIDungeon = dungeonUI;
            }
        }
    }
}
