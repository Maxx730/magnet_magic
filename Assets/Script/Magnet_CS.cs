﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet_CS : MonoBehaviour {

	//PUBLIC VARIABLES
	public Vector3 target;
	public float stop_distance = 3f;
	public bool throwing = false;
	public bool is_moving = false;
	public bool dismissed = false;

	public List<GameObject> followers;
		
	//PUBLIC MATERIALS
	public Material dismissed_mat;
	public Material not_dismissed_mat;

	//PRIVATE VARIABLES
	private Transform trans;

	// Use this for initialization
	void Awake(){
		trans = transform;
	}

	void OnTriggerEnter(Collider enter){
		if(enter.gameObject.tag == "MetalBar"){

		}
	}
}
