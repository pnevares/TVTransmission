using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject[] posts;
    public int[] correctGears;
    public int selectedPost = 0;

	void Start () {
        SelectPost(0);
	}
	
	void Update () {
		
	}

    public void SelectPost(int postId) {
        Transform oldOverlay = posts[selectedPost].transform.GetChild(0);
        oldOverlay.gameObject.SetActive(false);

        selectedPost = postId;

        Transform newOverlay = posts[postId].transform.GetChild(0);
        newOverlay.gameObject.SetActive(true);
    }
}
