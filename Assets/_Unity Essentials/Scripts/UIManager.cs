using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject mainEnvironmentPanel;

    // This method will be assigned to your Start button's OnClick event
    public void StartExperience()
    {
        mainMenuPanel.SetActive(false);
        mainEnvironmentPanel.SetActive(true);
    }
}
