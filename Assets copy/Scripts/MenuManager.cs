using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Call this method from the Button's OnClick event
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Scenes/GameScene-ALU"); // Replace "GameScene" with your target scene name
    }
}
