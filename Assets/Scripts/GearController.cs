using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour {
    private bool reverse;
    private Animator anim;

    public bool Reverse {
        get {
            return reverse;
        }

        set {
            reverse = value;
            if(reverse) {
                anim.SetBool("reverse", true);
            }
        }
    }

    void Awake () {
        anim = gameObject.GetComponent<Animator>();
	}
}
