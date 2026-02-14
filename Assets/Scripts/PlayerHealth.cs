using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;           // Máximo número de golpes que puede recibir
    [SerializeField] private float invulnerableTime = 1f; // Tiempo de invulnerabilidad tras recibir un golpe

    [Header("UI Reference")]
    [SerializeField] private Image healthFillImage;       // La barra roja que se llena y vacía

    [Header("Debug")]
    public bool isInvulnerable = false;

    private int currentHealth;

    void Start()
    {
        // Inicializamos la vida al máximo
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    /// <summary>
    /// El jugador recibe daño de los enemigos.
    /// </summary>
    /// <param name="damage">Cantidad de daño recibida (normalmente 1)</param>
    public bool TakeHit(int damage)
    {
        if (isInvulnerable || currentHealth <= 0)
            return false;

        // Reducimos la vida
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizamos la barra visual
        UpdateHealthBar();

        // Revisamos si el jugador muere
        if (currentHealth <= 0)
        {
            StartCoroutine(RestartGame());
        }
        else
        {
            StartCoroutine(Invulnerability());
        }
        return true;
    }

    /// <summary>
    /// Pequeño período de invulnerabilidad tras recibir daño.
    /// </summary>
    IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }

    /// <summary>
    /// Actualiza la barra de vida visual según el porcentaje de salud.
    /// </summary>
    void UpdateHealthBar()
    {
        if (healthFillImage != null)
        {
            float percentage = (float)currentHealth / maxHealth;
            healthFillImage.fillAmount = percentage;
        }
    }

    /// <summary>
    /// Reinicia el juego después de 2 segundos de morir.
    /// </summary>
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.RestartGame();
    }

    /// <summary>
    /// Devuelve la vida actual del jugador (útil para el sistema de puntuación).
    /// </summary>
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
