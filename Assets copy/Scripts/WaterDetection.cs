using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player fell into water.");
            GameManager.instance.PlayerFellInWater(transform.position);

            // Destroy the current player to prevent duplicates
            Destroy(collision.gameObject);
        }
    }
}
