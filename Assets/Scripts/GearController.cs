using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour {
    public bool reverse;

	// Use this for initialization
	void Start () {
        Animator anim = gameObject.GetComponent<Animator>();
        if(reverse) {
            anim.SetBool("reverse", true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
