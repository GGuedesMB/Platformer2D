using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;

    [SerializeField] int maxHealth;
    [SerializeField] float health;
    [SerializeField] float damageCooldown;
    [SerializeField] float flickTime;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float hitColorDuration;
    [SerializeField] Color hitColor;
    [SerializeField] float scaleTime;
    [SerializeField] Vector3 hitScale;

    [Header("Audio")]
    [SerializeField] GameObject healingSFX;
    [SerializeField] GameObject damageSFX;

    Vector4 originalColor;
    Vector3 originalScale;
    public bool invulnerable;

    private void Start()
    {
        if (spriteRenderer)
        {
            originalColor = spriteRenderer.color;
        }
        originalScale = transform.localScale;

        if (healthBar)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth((int)health);
        }
    }

    public void TakeDamage(float damage)
    {

        if (invulnerable)
        {
            return;
        }

        health -= damage;

        if (healthBar)
        {
            healthBar.SetHealth((int)health);
        }

        if (damageSFX)
        {
            damageSFX.SetActive(false);
            damageSFX.SetActive(true);
        }

        if (hitColorDuration > 0)
        {
            StartCoroutine(HitColor());
        }

        if (scaleTime > 0)
        {
            StartCoroutine(HitScale());
        }

        if (damageCooldown > 0 && health > 0)
        {
            invulnerable = true;
            StartCoroutine(DamageCooldown());
            StartCoroutine(Flicker());
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;

        if (healthBar)
        {
            healthBar.SetHealth((int)health);
        }

        if (healingSFX)
        {
            healingSFX.SetActive(false);
            healingSFX.SetActive(true);
        }
    }

    public float GetHealth()
    {

        return health;
    }

    IEnumerator Flick()
    {
        float invulnerableTimer = 0;
        float flickingTimer = 0;
        float sign = -1;

        Vector4 color = spriteRenderer.color;

        while (invulnerableTimer < damageCooldown)
        {
            if (flickingTimer > flickTime)
            {
                sign *= -1;
                flickingTimer = 0;
                Debug.Log("Change sign: " + sign);
            }
            float alphaAddition = Time.deltaTime * (sign / flickTime);
            spriteRenderer.color = new Vector4(color.x, color.y, color.z, spriteRenderer.color.a + alphaAddition);
            Debug.Log("Alpha addition: " + alphaAddition);
            Debug.Log("Current color: " + spriteRenderer.color);

            invulnerableTimer += Time.deltaTime;
            flickingTimer += Time.deltaTime;   
            
            yield return null;
        }
        invulnerable = false;
        spriteRenderer.color = color;
    }

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        invulnerable = false;
    }

    IEnumerator Flicker()
    {
        int alpha = -1;
        yield return new WaitForSeconds(hitColorDuration);
        while (invulnerable)
        {
            spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + alpha);
            alpha *= -1;
            yield return new WaitForSeconds(flickTime);
        }
        
        spriteRenderer.color = originalColor;
    }

    IEnumerator HitColor()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(hitColorDuration);
        spriteRenderer.color = originalColor;
    }

    IEnumerator HitScale()
    {
        spriteRenderer.transform.localScale = hitScale;
        yield return new WaitForSeconds(scaleTime);
        spriteRenderer.transform.localScale = originalScale;
    }
}
