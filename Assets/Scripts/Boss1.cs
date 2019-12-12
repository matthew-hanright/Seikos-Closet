using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Vector2 topPosition;
    public Vector2 bottomPosition;

    public GameObject player;
    private int maxHealth = 3000;
    public int currentHealth = 3000;
    public int damage = 2;

    private bool shouldMove = true;
    public bool isMoving = false;
    private float goalDiscrepancy = 1.5f;
    private Vector2 goalNewPosition = new Vector2(0.0f, 0.0f);
    public int maxSpeed = 3;
    public int minSpeed = 3;
    private float currentSpeed = 0;

    private bool isAttacking = false;

    private bool attack1Active = false;
    private float attack1Start;
    private float attack1Delay = 0.5f;
    private float attack1Speed = 2f;
    private float attack1CoolDown = 5f;
    private bool attack1AnimSet = false;
    private Vector2 attack1Distance;

    private bool attack2Active = false;
    private float attack2Start;
    private float attack2Delay = 0.75f;
    private float attack2Speed = 2f;
    private float attack2CoolDown = 5f;
    public GameObject attack2Obj;

    private bool attack3Active = false;
    private float attack3Start;
    private float attack3Delay = 0.5f;
    private float attack3Speed = 2f;
    private float attack3CoolDown = 5f;
    public Vector3 attack3SpawnLeft;
    public Vector3 attack3SpawnRight;
    public GameObject chatter;
    public GameObject activeVision;

    public GameObject eyeSpike;
    private Vector3 eyeSpikeOffset;
    private Vector3 eyeSpikeOriginalScale;
    private Quaternion eyeSpikeOriginalRot;

    //States: 0 = neutral, 1+ = attack of that number
    private int currentState = 0; 

    // Start is called before the first frame update
    void Start()
    {
        eyeSpikeOffset = transform.position - eyeSpike.transform.position;
        eyeSpikeOriginalScale = eyeSpike.transform.localScale;
        eyeSpikeOriginalRot = eyeSpike.transform.rotation;
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(eyeSpike);
            Destroy(this.gameObject);
        }
        if (isMoving)
        {
            if(Mathf.Abs(transform.position.y - goalNewPosition.y) < goalDiscrepancy)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                isMoving = false;
                goalNewPosition = Vector2.zero;
            }
        }
        else if(!isAttacking)
        {
            int nextAction = (int)Mathf.Round(Random.Range(0, 5));
            switch (nextAction)
            {
                case 0:
                    neutralMove();
                    break;
                case 1:
                    if (Time.time > attack1Start + attack1CoolDown)
                    {
                        attack1();
                    }
                    else
                    {
                        neutralMove();
                    }
                    break;
                case 2:
                    if (Time.time > attack2Start + attack2CoolDown)
                    {
                        attack2();
                    }
                    else
                    {
                        neutralMove();
                    }
                    break;
                case 3:
                    if(Time.time > attack3Start + attack3CoolDown)
                    {
                        attack3();
                    }
                    else
                    {
                        neutralMove();
                    }
                    break;
            }
        }
        else if(isAttacking && attack3Active)
        {
            if(Time.time > attack3Start + attack3Delay)
            {
                attack3End();
            }
        }
    }

    private void FixedUpdate()
    {
        if(isAttacking)
        {
            if(attack1Active)
            {
                if(Time.time > attack1Start + attack1Delay)
                {
                    if(!eyeSpike.GetComponent<Animator>().GetBool("expandStart"))
                    {
                        eyeSpike.GetComponent<Animator>().SetBool("expandStart", true);
                    }
                }
            }
        }
    }

    private void neutralMove()
    {
        if(shouldMove)
        {
            if(goalNewPosition == Vector2.zero)
            {
                goalNewPosition = new Vector2(
                    transform.position.x,
                    (topPosition.y - bottomPosition.y) * Random.Range(0.1f, 1));
                currentSpeed = Random.Range(minSpeed, maxSpeed);
                if(goalNewPosition.y < transform.position.y) {
                    currentSpeed = -currentSpeed;
                }
            }
            isMoving = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, currentSpeed);
        }
    }

    private void attack1()
    {
        isAttacking = true;
        attack1Active = true;
        attack1Start = Time.time;
        attack1Distance = eyeSpike.transform.position - player.transform.position;
        eyeSpike.transform.position = transform.position - eyeSpikeOffset;
        Vector3 vectorToTarget = player.transform.position - eyeSpike.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 180;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        eyeSpike.transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
        eyeSpike.SetActive(true);
        eyeSpike.GetComponent<Animator>().SetBool("endAnimation", false);
    }
    
    private void attack2()
    {
        isAttacking = true;
        attack2Active = true;
        attack2Start = Time.time;
        attack2Obj.GetComponent<attack2controller>().startTime = Time.time;
        attack2Obj.GetComponent<attack2controller>().delay = attack2Delay;
        attack2Obj.SetActive(true);
    }

    private void attack3()
    {
        isAttacking = true;
        attack3Active = true;
        attack3Start = Time.time;
        int i = (int)Mathf.Round(Random.Range(1, 3));
        int j = 0;
        for (j = 0; j < i; j++)
        {
            GameObject newChatter = Instantiate(chatter) as GameObject;
            float randX = (attack3SpawnRight.x - attack3SpawnLeft.x) * Random.Range(0, 1);
            newChatter.transform.position += new Vector3(
                attack3SpawnRight.x - randX,
                attack3SpawnRight.y,
                attack3SpawnRight.z);
            GameObject newVision = Instantiate(activeVision) as GameObject;
            newVision.transform.position += new Vector3(
                attack3SpawnRight.x - randX,
                attack3SpawnRight.y,
                attack3SpawnRight.z);
            newVision.GetComponent<ChatterTracking>().player = player;
            newVision.GetComponent<ChatterTracking>().chatter = newChatter;
            newChatter.GetComponent<ChatterAI>().activeVision = newVision;
        }
    }

    public void endAttack1()
    {
        eyeSpike.transform.localScale = eyeSpikeOriginalScale;
        attack1Active = false;
        isAttacking = false;
        attack1Start = Time.time;
        eyeSpike.SetActive(false);
    }

    public void endAttack2()
    {
        attack2Obj.SetActive(false);
        isAttacking = false;
        attack2Active = false;
        attack2Start = Time.time;
    }

    public void attack3End()
    {
        isAttacking = false;
        attack3Active = false;
        attack3Start = Time.time;
    }
}
