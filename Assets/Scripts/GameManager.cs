using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player & Respawn")]
    public GameObject playerPrefab;
    public Transform fallbackRespawnPoint;

    [Header("UI Elements")]
    public Canvas uiCanvas;
    public Text lifeText; // ✅ Make sure this is UnityEngine.UI.Text
    public GameObject endScreenUI;

    private int playerLives = 3;
    private bool isRespawning = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (uiCanvas != null) uiCanvas.enabled = true;
        if (endScreenUI != null) endScreenUI.SetActive(false);

        UpdateLifeUI();
    }

    public void PlayerFellInWater(Vector3 waterPosition)
    {
        if (isRespawning) return;

        StartCoroutine(RespawnPlayer(waterPosition));
    }

    IEnumerator RespawnPlayer(Vector3 waterPosition)
    {
        isRespawning = true;

        playerLives--;
        UpdateLifeUI();
        Debug.Log("Player fell. Lives left: " + playerLives);

        yield return new WaitForSeconds(1f);

        if (playerLives <= 0)
        {
            ShowEndScreen();
            yield break;
        }

        Vector3 spawnPosition = FindGroundNear(waterPosition);
        GameObject newPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

        var cam = FindObjectOfType<CameraFollow>();
        if (cam != null)
            cam.SetTarget(newPlayer.transform);

        isRespawning = false;
    }

    Vector3 FindGroundNear(Vector3 position)
    {
        float checkDistance = 2f;
        float raycastHeight = 2f;
        float raycastDown = 3f;
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        Vector2 left = new Vector2(position.x - checkDistance, position.y + raycastHeight);
        RaycastHit2D hitLeft = Physics2D.Raycast(left, Vector2.down, raycastDown, groundLayer);

        if (hitLeft.collider != null)
            return new Vector3(hitLeft.point.x, hitLeft.point.y + 1f, 0);

        Vector2 right = new Vector2(position.x + checkDistance, position.y + raycastHeight);
        RaycastHit2D hitRight = Physics2D.Raycast(right, Vector2.down, raycastDown, groundLayer);

        if (hitRight.collider != null)
            return new Vector3(hitRight.point.x, hitRight.point.y + 1f, 0);

        return fallbackRespawnPoint.position;
    }

    void UpdateLifeUI()
    {
        if (lifeText != null)
            lifeText.text = "x" + playerLives;
    }

    void ShowEndScreen()
    {
        if (endScreenUI != null)
            endScreenUI.SetActive(true);
    }

    public void ReplayGame()
    {
        playerLives = 3;
        UpdateLifeUI();
        if (endScreenUI != null) endScreenUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
