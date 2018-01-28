using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject[] posts;
    public GameObject[] currentGearsObjects;
    public int selectedPost = 0;
    public int targetGearId;
    public GameObject gearPrefab;
    public AudioController audioController;

    // for success animations
    public GameObject captionBackground;
    public GameObject remote;
    public GameObject mainCamera;
	public GameObject tvStatic;

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
		startPost = 1;
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

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
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
        if(currentGearsObjects[selectedPost] == null) {
            Transform parent = GameObject.FindWithTag("Television").transform;
            Vector3 position = posts[selectedPost].transform.position;
            Vector3 scale = gearSizes[size];

            GameObject newGear = Instantiate(gearPrefab, parent);
            newGear.transform.position = position;
            newGear.transform.localScale = scale;

            // store object at position to track moving gears
            currentGearsObjects[selectedPost] = newGear;

            // check any neighbor gears to start rotating
            CheckIfGearShouldMove(selectedPost);
        }
    }

    private void CheckIfGearShouldMove(int position) {
        // check down
        if (position < 20) {
            if (IsMoving(position + 4)) {
                RotateGear(position, position + 4);
            }
        }

        // check up
        if (position > 3) {
            if (IsMoving(position - 4)) {
                RotateGear(position, position - 4);
            }
        }

        // check right
        if (position % 4 < 3) {
            if (IsMoving(position + 1)) {
                RotateGear(position, position + 1);
            }
        }

        // check left
        if (position % 4 > 0) {
            if (IsMoving(position - 1)) {
                RotateGear(position, position - 1);
            }
        }
    }

    private bool IsMoving(int position) {
        return currentGearsObjects[position] != null && currentGearsObjects[position].GetComponent<Animator>().enabled;
    }

    private void RotateGear(int position, int source) {
        if(currentGearsObjects[position] != null) {
            currentGearsObjects[position].GetComponent<GearController>().Reverse = !currentGearsObjects[source].GetComponent<GearController>().Reverse;
            currentGearsObjects[position].GetComponent<Animator>().enabled = true;

            // check if goal is reached
            if (position == targetGearId) {
                success = true;
                captionBackground.GetComponent<Animator>().enabled = true;
                remote.GetComponent<Animator>().SetBool("success", true);
                if (mainCamera != null) {
                    mainCamera.GetComponent<Animator>().SetBool("success", true);
                }
				tvStatic.GetComponent<Animator> ().SetBool ("success", true);
                audioController.Success();
            }

            if (position < 20) {
                if (!IsMoving(position + 4)) {
                    RotateGear(position + 4, position);
                }
            }

            // check up
            if (position > 3) {
                if (!IsMoving(position - 4)) {
                    RotateGear(position - 4, position);
                }
            }

            // check right
            if (position % 4 < 3) {
                if (!IsMoving(position + 1)) {
                    RotateGear(position + 1, position);
                }
            }

            // check left
            if (position % 4 > 0) {
                if (!IsMoving(position - 1)) {
                    RotateGear(position - 1, position);
                }
            }
        }
    }
}
