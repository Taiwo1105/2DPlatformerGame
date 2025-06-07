using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Call this method to load Scene_01
    public void LoadScene01()
    {
        SceneManager.LoadScene("Scenes/Scene_01");
    }
}
