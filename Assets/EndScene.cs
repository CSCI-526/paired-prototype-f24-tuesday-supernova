using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    public GameObject player; // Reference to the player object, to be assigned in the Inspector
    public Text gameOverText; // Reference to the UI Text component
    public float triggerDistance = 1f; // Distance threshold for showing Game Over

    void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false); // Hide the Game Over text initially
        }
    }

    void Update()
    {
       
        // Calculate the distance between the player and the end flag
        if (player != null)
        {
            Vector2 transformPos2D = new Vector2(transform.position.x, transform.position.y);
            Vector2 playerPos2D = new Vector2(player.transform.position.x, player.transform.position.y);

            float distance = Vector2.Distance(playerPos2D, transformPos2D);
            // If the player is within the specified distance
            if (distance <= triggerDistance)
            {
                TriggerGameOver();
            }
        }
    }

    void TriggerGameOver()
    {
        
        // Show the game over message
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "You win !!!!!!!!!!!!"; // Set the text to "Game Over"
        }

        // Restart the game after a short delay
       // StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before restarting

        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
