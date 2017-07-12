using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControl : MonoBehaviour {
private Animation animation;
	private bool shouldMove = false;
	private bool shouldEat = false;
	private bool shouldJump = false;

	// Use this for initialization
	void Start () {
		animation = GetComponent<Animation>();
	}

  	public Transform cameraToFollow;
	// Update is called once per frame
	void Update () {
		
		Vector3 distance = cameraToFollow.position - transform.position;
		shouldMove = distance.magnitude > 1.5f;
		if (shouldMove) {
			LookAt();
			if (!animation.IsPlaying("Walk")) {
				animation.Play("Walk");
			}
			transform.Translate(Vector3.forward * Time.deltaTime * (transform.localScale.x * .05f));
		}
		else if (!animation.IsPlaying("Idle")) {
				animation.Play("Idle");
		}
	}

	public void Walk (){
		if (!animation.isPlaying){
			animation.Play();
			shouldMove = true;
		} else {
			animation.Stop();
			shouldMove = false;
			animation.Play("Idle");
		}
	}

	public void Eat (){
		if (!animation.isPlaying){
			animation.Play("Eat");
		} else {
			animation.Stop("Eat");
		}
	}

	public void Jump (){
		if (!animation.isPlaying){
			animation.Play("Jump");

		} else {
			animation.Stop("Jump");
		}
	}

	public void LookAt (){
		transform.LookAt(Camera.main.transform.position);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y,0);
	}
	public void Bigger (){
		transform.localScale += new Vector3 (1,1,1);
	}
	public void Smaller (){
		if (transform.localScale.x > 1);
		transform.localScale -= new Vector3 (1,1,1);
	}
}
