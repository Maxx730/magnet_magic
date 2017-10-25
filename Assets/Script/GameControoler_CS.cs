using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControoler_CS : MonoBehaviour {

	//PUBLIC VARIABLES
	public GameObject player;
	public GameObject follower;

	//HOW FAST THE PLAYER CAN SPEED UP.
	public float player_accel = 15f;
	public float rot_speed = 5;

	//MAX SPEED THE PLAYER IS ALLOWED TO TRAVEL AT.
	public float for_speed = 10f;
	public float back_speed = 10f;
	public float left_right_speed = 10f;

	// Update is called once per frame
	void Update () {
		//GRAB THE PLAYER ROTATION FIRST BEFORE MAKING THE CALCULATIONS.
		Quaternion rot = player.GetComponent<Transform> ().rotation;
		float y = rot.eulerAngles.y;

		//SET IS MOVING TO TRUE BASED ON ANY KEY DOWN EVENTS.
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
			player.GetComponent<Magnet_CS> ().is_moving = true;
		}

		if(player.GetComponent<Magnet_CS> ().is_moving && player.GetComponent<Rigidbody> ().velocity.x > (-1f*for_speed) && player.GetComponent<Rigidbody> ().velocity.x < for_speed && player.GetComponent<Rigidbody> ().velocity.z > (-1*back_speed) && player.GetComponent<Rigidbody> ().velocity.z < for_speed){
			if(Input.GetKey(KeyCode.W)){
				player.GetComponent<Transform> ().position += player.GetComponent<Transform> ().forward * player_accel * Time.deltaTime;
			}

			if(Input.GetKey(KeyCode.S)){
				player.GetComponent<Transform> ().position += player.GetComponent<Transform> ().forward * -player_accel * Time.deltaTime;
			}

			//ROTATION LOGIC IS APPLIED HERE.
			if(Input.GetKey(KeyCode.D)){
				y += 10 * rot_speed * Time.deltaTime;
			}

			if(Input.GetKey(KeyCode.A)){
				y += 10 * -rot_speed * Time.deltaTime;
			}

			//SET THE NEW VALUES TO THE PLAYERS POSITION AND ROTATION.
			rot = Quaternion.Euler (0,y,0);
			player.GetComponent<Transform> ().rotation = rot;
		}

		//IF THEY RIGHT MOUSE BUTTON IS CLICKED THEN WE WANT TO SPAWN A FOLLOWER PREFAB WHERE IT HAS BEEN CLICKED.
		if(Input.GetMouseButtonDown(1)){
			Debug.Log ("Spawning a follower!");

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);


			if(Physics.Raycast(ray,out hit,1000.0f)){
				if(hit.collider.gameObject.tag == "PhysicalFloor"){
					var posi = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					Instantiate (follower,new Vector3(posi.x,0,posi.z),Quaternion.identity);
				}
			}
		}

		if(Input.GetKeyDown(KeyCode.Q)){
			player.GetComponent<Magnet_CS> ().dismissed = !player.GetComponent<Magnet_CS> ().dismissed;

			if(player.GetComponent<Magnet_CS> ().dismissed){
				player.GetComponent<MeshRenderer> ().material = player.GetComponent<Magnet_CS> ().dismissed_mat;
			}else{
				player.GetComponent<MeshRenderer> ().material = player.GetComponent<Magnet_CS> ().not_dismissed_mat;
			}
		}
	}
}
