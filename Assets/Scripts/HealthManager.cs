using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    int currentHealth;
    bool isShielded = false;
    bool isAirBubbled = false;
    public bool isBlind = false;

    public Slider slider;

    public MouthAttacks mouthAttack;

    [Header("Abilities")]
    public float ShieldSpellCooldown = 3;
    public float ShieldSpellDuration = 1.2f;
    public float AirBubbleSpellCooldown = 5;
    public float AirBubbleSpellDuration = 2.5f;
    public float ClearSpellCooldown = 4;

    Coroutine shieldSpellCoroutine = null;
    Coroutine airBubbleSpellCoroutine = null;
    Coroutine clearSpellCoroutine = null;

    bool shieldSpellReady = true;
    bool airBubbleSpellReady = true;
    bool clearSpellReady = true;

    private void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Spell_Shield();
        if (Input.GetKeyDown(KeyCode.W)) Spell_AirBubble();
        if (Input.GetKeyDown(KeyCode.E)) Spell_Clear();
        
    }

    public void TakeDamage(int damage)
    {
        if (!isShielded)
        {
            currentHealth -= damage;
            UpdateHealthBar();

            Debug.Log("Current Health:" + currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void TakeDPSDamage(int damagePerSecond)
    {
        if (!isAirBubbled)
        {
            currentHealth -= damagePerSecond;
            UpdateHealthBar();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void UpdateHealthBar()
    {
        slider.value = currentHealth;
    }

    void Die()
    {

    }

    void ResetIsAttacking()
    {
        mouthAttack.resetIsAttacking();
    }

    void Spell_Shield()
    {
        if (shieldSpellReady)
        {
            if (shieldSpellCoroutine != null) StopCoroutine(shieldSpellCoroutine);
            shieldSpellCoroutine = StartCoroutine(ShieldCooldownRoutine());
            Debug.Log("shielded");
        }
    }

    void Spell_AirBubble()
    {
        if (airBubbleSpellReady)
        {
            if (airBubbleSpellCoroutine != null) StopCoroutine(airBubbleSpellCoroutine);
            airBubbleSpellCoroutine = StartCoroutine(AirBubbleCooldownRoutine());
        }
    }

    void Spell_Clear()
    {
        if (clearSpellReady)
        {
            if (clearSpellCoroutine != null) StopCoroutine(clearSpellCoroutine);
            clearSpellCoroutine = StartCoroutine(ClearCooldownRoutine());
            mouthAttack.spitGameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Coroutines for spells
    /// </summary>
    /// <returns></returns>
    IEnumerator ShieldCooldownRoutine()
    {
        float timer = 0.0f;

        shieldSpellReady = false;
        isShielded = true;

        while (timer < ShieldSpellCooldown)
        {
            timer += Time.deltaTime;

            if (timer >= ShieldSpellDuration & isShielded)
            {
                isShielded = false;
                Debug.Log("not shielded");

            }
            yield return null;
        }

        shieldSpellReady = true;
    }

    IEnumerator AirBubbleCooldownRoutine()
    {
        float timer = 0.0f;

        airBubbleSpellReady = false;
        isAirBubbled = true;

        while (timer < AirBubbleSpellCooldown)
        {
            timer += Time.deltaTime;

            if (timer >= AirBubbleSpellDuration & isAirBubbled)
            {
                isAirBubbled = false;
            }
            yield return null;
        }

        airBubbleSpellReady = true;
    }

    IEnumerator ClearCooldownRoutine()
    {
        float timer = 0.0f;

        clearSpellReady = false;
        isBlind = false;

        while (timer < ClearSpellCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        clearSpellReady = true;
    }

}
