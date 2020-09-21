using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    Character character;

    float maxHealth = 100;
    public bool IsDead
	{
		get { return currentHealth == 0; }
	}
    
    [SerializeField] float _currentHealth;
    float currentHealth
	{
		get { return _currentHealth; }
		set
		{
            if(value < _currentHealth)
                timeSinceDamage = 0;
            _currentHealth = value;
            if (_currentHealth < 0)
                _currentHealth = 0;
		}
	}
    [SerializeField] float regenerationOffset;
    [SerializeField] float regenerationSpeed;

    [SerializeField] float wallHitVelocityThreshold;
    [SerializeField] float floorHitVelocityThreshold;

    [SerializeField] float timeSinceDamage = 0;

    Color normalColor;
    [SerializeField] Color deadColor;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        character = GetComponent<Character>();
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

	private void Start()
	{
        normalColor = spriteRenderer.color;
	}

	// Update is called once per frame
	void Update()
    {
        timeSinceDamage += Time.deltaTime;

        if(timeSinceDamage > regenerationOffset && currentHealth < maxHealth)
		{
            currentHealth += regenerationSpeed * Time.deltaTime;
		}
        if(currentHealth > maxHealth)
		{
            currentHealth = maxHealth;
		}

        spriteRenderer.color = Color.Lerp(deadColor, normalColor, currentHealth / maxHealth);
    }
    public void HitFloorDamage(float velocity)
	{
        
        if (velocity <= floorHitVelocityThreshold)
            return;

        
        currentHealth -= velocity;
    }

    public void HitWallDamage(float velocity)
	{
        
        if (velocity <= wallHitVelocityThreshold)
            return;

        
        currentHealth -= velocity;
	}

    public void TakeDamage(float damage)
	{
        currentHealth -= damage;
	}

    public void RestoreHealth(int percents)
	{
        currentHealth = maxHealth * (percents / 100f);
	}

    public void Die()
	{
        currentHealth = 0;
	}


}
