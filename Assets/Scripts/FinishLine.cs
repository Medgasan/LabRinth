using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(FinishRoutine(other.GetComponent<Player>()));
    }

    IEnumerator FinishRoutine(Player player)
    {
        yield return new WaitForSeconds(5f);

        int remainingHealth = player.GetComponent<PlayerHealth>().GetCurrentHealth();
        int finalScore = ScoreManager.Instance.CalculateScore(remainingHealth);
        Debug.Log("Final Score: " + finalScore);

        // Reiniciamos la puntuación para la próxima partida
        ScoreManager.Instance.ResetScore();

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
