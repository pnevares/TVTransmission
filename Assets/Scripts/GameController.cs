using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject[] posts;
    public int[] correctGears;

    private int selectedPost = 0;

	void Start () {
        SelectPost(0);
	}
	
	void Update () {
		
	}

    public void SelectPost(int postId) {
        selectedPost = postId;

        Animator anim = posts[postId].GetComponent<Animator>();
        anim.enabled = true;
    }
}
