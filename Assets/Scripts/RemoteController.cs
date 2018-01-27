using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteController : MonoBehaviour {
    private GameController gameController;

    private void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    public void MoveDown() {
        gameController.SelectPost(gameController.selectedPost + 1);
    }

    public void MoveUp() {
        gameController.SelectPost(gameController.selectedPost - 1);
    }

}
