using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject[] posts;
    public int[] correctGears;
    public int[] currentGears;
    public int selectedPost = 0;

    public GameObject gearPrefab;

    private Vector3[] gearSizes = {
        new Vector3(0.05f, 0.05f, 0.05f),
        new Vector3(0.1f, 0.1f, 0.1f),
        new Vector3(0.2f, 0.2f, 0.2f),
        new Vector3(0.4f, 0.4f, 0.4f),
    };

	void Start () {
        SelectPost(0);
	}
	
	void Update () {
        if (correctGears.SequenceEqual(currentGears)) {
            Debug.Log("you win");
        }
    }

    public void SelectPost(int postId) {
		if (posts[postId].GetComponent<SpriteRenderer>().enabled == true) {
			Transform oldOverlay = posts[selectedPost].transform.GetChild (0);
			oldOverlay.gameObject.SetActive (false);

			selectedPost = postId;

			Transform newOverlay = posts [postId].transform.GetChild (0);
			newOverlay.gameObject.SetActive (true);
		}
    }

    public void SetGear(int size) {
        Transform parent = GameObject.FindWithTag("Television").transform;
        Vector3 position = posts[selectedPost].transform.position;
        Vector3 scale = gearSizes[size];

        GameObject newGear = Instantiate(gearPrefab, parent);
        newGear.transform.position = position;
        newGear.transform.localScale = scale;

        currentGears[selectedPost] = size;
    }
}
