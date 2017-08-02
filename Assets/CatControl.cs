using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControl : MonoBehaviour
{
    private Animation animation;
    Animator anim;

    public FoodControl food;

    private bool shouldMove = false;

    public float followDistance = 2.0f;
    public float eatDistance = 0.2f;
    public float walkSpeed = 10.0f;
    public Transform cameraToFollow;


    /****************************************
	setting up random timer for cat jumps 
	*****************************************/
    public float maxTime = 15;
    public float minTime = 2;

    //current time
    private float time;

    //The time to spawn the object
    private float jumpTime;

    private float lastMealTime = 0;

    // Use this for initialization
    void Start()
    {
        animation = GetComponent<Animation>();
        anim = GetComponent<Animator>();
        SetRandomTime();
        time = minTime;

    }

    void Update()
    {
        if ((cameraToFollow.position - transform.position).magnitude > followDistance || food.foods.Count > 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        float secondsSpentNotEating = Time.time - lastMealTime;
        if (secondsSpentNotEating < 10)
        {
            //full - eat 0 food objects

        }
        else if (secondsSpentNotEating > 10 && secondsSpentNotEating < 30)
        {
            //getting peckish - eat 2 food objects;

        }
        else if (secondsSpentNotEating < 240)
        {
            //Hungry - eat 3 food objects
        }
        else if (secondsSpentNotEating < 300)
        {
            //Starving - eat 4 food objects
        }

        if (anim.GetBool("IsWalking"))
        {
            if (food.foods.Count > 0)
            {
                GameObject closestFood = food.foods[0];
                if ((closestFood.transform.position - transform.position).magnitude < eatDistance) {
                    anim.SetBool("IsWalking", false);
                    //Eat();
                    anim.SetBool("IsEating", true);
                    food.foods.Remove(closestFood);
                    Destroy(closestFood);
                }
                else {
                    LookAt(closestFood.transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, closestFood.transform.position, walkSpeed * Time.deltaTime);
                }
            }
            else
            {
                LookAt(Camera.main.transform.position);
                transform.Translate(Vector3.forward * Time.deltaTime * (transform.localScale.x * walkSpeed));
            }
        
        }
    }

    public void Walk()
    {
        anim.SetBool("IsWalking", true);

    }

    public void Eat()
    {
        Debug.Log("Entered EAT()");

        if (food.foods.Count > 0)
        {           
            
            anim.SetBool("IsEating", true);
            
            lastMealTime = Time.time;
        }
    }

    public void Jump()
    {
        time = 0;
        anim.SetTrigger("IsJumping");
    }

    public void LookAt(Vector3 position)
    {
        transform.LookAt(position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    public void Bigger()
    {
        transform.localScale += new Vector3(1, 1, 1);
    }
    public void Smaller()
    {
        if (transform.localScale.x > 1)
        {
            transform.localScale -= new Vector3(1, 1, 1);
        }
    }


    void FixedUpdate()
    {

        //Counts up
        time += Time.deltaTime;

        //Check if its time to jump 
        if (time >= jumpTime)
        {
            Jump();
            SetRandomTime();
        }

    }

    //Sets the random time between minTime and maxTime for cat to jump
    void SetRandomTime()
    {
        jumpTime = Random.Range(minTime, maxTime);
    }

}
