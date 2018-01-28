using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject[] posts;
    public int[] correctGears;
    public int[] currentGears;
    public GameObject[] currentGearsObjects;
    public int selectedPost = 0;

    public GameObject gearPrefab;

    private Vector3[] gearSizes = {
        new Vector3(0.05f, 0.05f, 0.05f),
        new Vector3(0.1f, 0.1f, 0.1f),
        new Vector3(0.2f, 0.2f, 0.2f),
        new Vector3(0.4f, 0.4f, 0.4f),
    };
    private bool success = false;

	void Start () {
		int startPost;

		// find the first available post
		startPost = 0;
		while (posts[startPost].GetComponent<SpriteRenderer>().enabled == false)
		{
			startPost++;
			if (startPost >= posts.Length) {
				// no valid posts found
				return;
			}
		}
        SelectPost(startPost);
	}
	
	void Update () {
        if (correctGears.SequenceEqual(currentGears)) {
            success = true;
        }
    }

    public void LoadScene(string sceneName) {
        if(success) {
            SceneManager.LoadScene(sceneName);
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
        if(currentGears[selectedPost] == -1) {
            Transform parent = GameObject.FindWithTag("Television").transform;
            Vector3 position = posts[selectedPost].transform.position;
            Vector3 scale = gearSizes[size];

            GameObject newGear = Instantiate(gearPrefab, parent);
            newGear.transform.position = position;
            newGear.transform.localScale = scale;

            // store size at position to track success
            currentGears[selectedPost] = size;
            // store object at position to track moving gears
            currentGearsObjects[selectedPost] = newGear;

            // starting at position 0 (the first, moving gear)
            // check any neighbor gears recursively to start rotating
            CheckNeighbors();
        }
    }

    private void CheckNeighbors(int position = 0) {
        // check down
        if (position < 20) {
            int neighbor = position + 4;
            MoveNeighbor(neighbor, position);
        }

        // check right
        if (position % 4 < 3) {
            int neighbor = position + 1;
            MoveNeighbor(neighbor, position);
        }
    }

    private void MoveNeighbor(int neighbor, int original) {
        if (currentGearsObjects[neighbor] != null) {
            Animator anim = currentGearsObjects[neighbor].GetComponent<Animator>();
            anim.enabled = true;

            // set the neighbor's rotation to the opposite of the other gear's rotation
            currentGearsObjects[neighbor].GetComponent<GearController>().Reverse = !currentGearsObjects[original].GetComponent<GearController>().Reverse;

            CheckNeighbors(neighbor);
        }
    }
}
