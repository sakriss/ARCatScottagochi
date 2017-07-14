using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControl : MonoBehaviour {
private Animation animation;
Animator anim;

	private bool shouldMove = false;
	private bool shouldEat = false;
	private bool shouldJump = false;

	// Use this for initialization
	void Start () {
		animation = GetComponent<Animation>();
		anim = GetComponent<Animator>();
		SetRandomTime();
        time = minTime;

	}

	public float followDistance = 2.0f;
	public float walkSpeed = 10.0f;
  	public Transform cameraToFollow;
    // Update is called once per frame
    // void Update () {

    // 	Vector3 distance = cameraToFollow.position - transform.position;
    // 	shouldMove = distance.magnitude > 1.5f;
    // 	if (shouldMove) {
    // 		LookAt();
    // 		if (!animation.IsPlaying("Walk")) {
    // 			animation.Play("Walk");
    // 		}
    // 		transform.Translate(Vector3.forward * Time.deltaTime * (transform.localScale.x * .05f));
    // 	}
    // 	else if (!animation.IsPlaying("Idle")) {
    // 			animation.Play("Idle");
    // 	}
    // }

    void Update()
    {
        Vector3 distance = cameraToFollow.position - transform.position;
        shouldMove = distance.magnitude > followDistance;
        if (shouldMove)
        {
            LookAt();
            anim.SetBool("IsWalking", true);
            transform.Translate(Vector3.forward * Time.deltaTime * (transform.localScale.x * walkSpeed));
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

	public void Walk (){
		anim.SetBool("IsWalking", true);
		
		// if (!animation.isPlaying){
		// 	animation.Play();
		// 	shouldMove = true;
		// } else {
		// 	animation.Stop();
		// 	shouldMove = false;
		// 	animation.Play("Idle");
		// }
	}

	public void Eat (){
		anim.SetTrigger("IsEating");
		Debug.Log("EAT TRIGGERED INSIDE JUMP FUNCTION");	
		// if (!animation.isPlaying){
		// 	animation.Play("Eat");
		// } else {
		// 	animation.Stop("Eat");
		// }
	}

	public void Jump (){
		time = 0;
		anim.SetTrigger("IsJumping");
		Debug.Log("JUMP TRIGGERED INSIDE JUMP FUNCTION");
	}

	public void LookAt (){
		transform.LookAt(Camera.main.transform.position);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y,0);
	}
	public void Bigger (){
		transform.localScale += new Vector3 (1,1,1);
	}
	public void Smaller (){
		if (transform.localScale.x > 1){
		transform.localScale -= new Vector3 (1,1,1);
		}
	}

	/****************************************
	setting up random timer for cat jumps 
	*****************************************/
	public float maxTime = 15;
	public float minTime = 2;
 
     //current time
    private float time;
 
     //The time to spawn the object
     private float jumpTime;
 
     void FixedUpdate(){
 
         //Counts up
         time += Time.deltaTime;
 
         //Check if its the right time to spawn the object
         if(time >= jumpTime){
			 Jump();
             SetRandomTime();
         }
     }

     //Sets the random time between minTime and maxTime
     void SetRandomTime(){
         jumpTime = Random.Range(minTime, maxTime);
     }
}
