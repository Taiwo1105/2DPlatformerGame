using UnityEngine;
using UnityEngine.UI;

public class StoryCanvas : MonoBehaviour
{
    public Transform[] cameraPositions;         // Target positions in front of each canvas
    public GameObject[] storyCanvases;          // Your StoryCanvas1 - StoryCanvas5
    public Camera mainCamera;                   // Reference to your main camera
    public float cameraMoveSpeed = 2f;          // Speed of the camera movement

    private int currentIndex = 0;
    private bool isMoving = false;

    void Start()
    {
        // Enable only the first canvas at start
        for (int i = 0; i < storyCanvases.Length; i++)
            storyCanvases[i].SetActive(i == currentIndex);

        // Move camera to initial position
        if (cameraPositions.Length > 0 && mainCamera != null)
        {
            mainCamera.transform.position = cameraPositions[0].position;
            mainCamera.transform.rotation = cameraPositions[0].rotation;
        }
    }

    void Update()
    {
        if (isMoving)
            MoveCamera();
    }

    public void OnNextButton()
    {
        if (currentIndex < storyCanvases.Length - 1)
        {
            storyCanvases[currentIndex].SetActive(false);
            currentIndex++;
            storyCanvases[currentIndex].SetActive(true);
            isMoving = true;
        }
    }

    public void OnPreviousButton()
    {
        if (currentIndex > 0)
        {
            storyCanvases[currentIndex].SetActive(false);
            currentIndex--;
            storyCanvases[currentIndex].SetActive(true);
            isMoving = true;
        }
    }

    void MoveCamera()
    {
        Transform target = cameraPositions[currentIndex];

        // Move position
        mainCamera.transform.position = Vector3.MoveTowards(
            mainCamera.transform.position,
            target.position,
            cameraMoveSpeed * Time.deltaTime
        );

        // Rotate to face the canvas
        mainCamera.transform.rotation = Quaternion.Slerp(
            mainCamera.transform.rotation,
            target.rotation,
            cameraMoveSpeed * Time.deltaTime
        );

        // Finish movement when close enough
        if (Vector3.Distance(mainCamera.transform.position, target.position) < 0.01f)
        {
            mainCamera.transform.position = target.position;
            mainCamera.transform.rotation = target.rotation;
            isMoving = false;
        }
    }

    // Quit the game when called
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Shows up in editor only
        Application.Quit();     // Works in built version
    }
}
