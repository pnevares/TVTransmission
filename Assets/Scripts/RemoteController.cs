using UnityEngine;

public class RemoteController : MonoBehaviour {
    private GameController gameController;

    private void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    public void SetGear(int size) {
        gameController.SetGear(size);
    }

    public void MoveDown() {
        if (gameController.selectedPost < 20)
            gameController.SelectPost(gameController.selectedPost + 4);
    }

    public void MoveUp() {
        if (gameController.selectedPost > 3)
            gameController.SelectPost(gameController.selectedPost - 4);
    }

    public void MoveRight() {
        if (gameController.selectedPost % 4 < 3)
            gameController.SelectPost(gameController.selectedPost + 1);
    }
    
    public void MoveLeft() {
        if (gameController.selectedPost % 4 > 0)
            gameController.SelectPost(gameController.selectedPost - 1);
    }

}
