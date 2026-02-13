using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;

    private float startTime;       // Tiempo al empezar la partida
    private float finalTime = 0f;  // Tiempo congelado al finalizar
    private int trapsTriggered = 0;
    private int finalScore = 0;
    private bool gameFinished = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        // Si el juego terminó, la UI usa el tiempo final congelado
        if (scoreText != null)
        {
            float displayTime = gameFinished ? finalTime : (Time.time - startTime);
            int timeScore = Mathf.Max(0, 1000 - (int)(displayTime * 10));

            // Si aún no hemos calculado la puntuación final, muestra solo la parte temporal
            scoreText.text = gameFinished ? "Final Score: " + finalScore : "Score: " + timeScore;
        }
    }

    /// <summary>
    /// Llamar desde trampas para penalizar.
    /// </summary>
    public void AddTrap()
    {
        if (!gameFinished)
            trapsTriggered++;
    }

    /// <summary>
    /// Calcular la puntuación final cuando se termina el laberinto.
    /// </summary>
    public int CalculateScore(int remainingHealth)
    {
        if (!gameFinished)
        {
            gameFinished = true;

            // Guardamos el tiempo final para congelar la puntuación
            finalTime = Time.time - startTime;

            int timeScore = Mathf.Max(0, 1000 - (int)(finalTime * 10));
            int healthScore = remainingHealth * 200;
            int trapPenalty = trapsTriggered * 100;

            finalScore = timeScore + healthScore - trapPenalty;

            // Bonus si no se golpeó ni cayó en trampas
            if (remainingHealth == 3 && trapsTriggered == 0)
                finalScore += 500;
        }

        return finalScore;
    }

    /// <summary>
    /// Reinicia el ScoreManager para una nueva partida.
    /// </summary>
    public void ResetScore()
    {
        startTime = Time.time;
        finalTime = 0f;
        trapsTriggered = 0;
        finalScore = 0;
        gameFinished = false;

        if (scoreText != null)
            scoreText.text = "Score: 0";
    }
}
