using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages and updates the player's health display on the UI.
/// </summary>
public class HealthManager : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Reference to the UI text displaying the player's health.")]
    public TextMeshProUGUI healthText;

    /// <summary>
    /// Updates the health display with the latest health value.
    /// </summary>
    /// <param name="newHealth">The player's current health value.</param>
    public void UpdateHealthDisplay(int newHealth)
    {
        healthText.text = "Health: " + newHealth;
    }
}
