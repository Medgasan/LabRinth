using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(FinishRoutine(other.GetComponent<PlayerController>()));
    }

    IEnumerator FinishRoutine(PlayerController player)
    {
        //ScoreManager.Instance.gameFinished = true;

        int remainingHealth = player.GetComponent<PlayerHealth>().GetCurrentHealth();
        int finalScore = ScoreManager.Instance.CalculateScore(remainingHealth);
        Debug.Log("Final Score: " + finalScore);


        yield return new WaitForSeconds(5f);
        // Reiniciamos la puntuación para la próxima partida
        ScoreManager.Instance.ResetScore();

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
