using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    Character character;
    Animator animator;

    void Awake()
    {
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
    }

    public void SetVelocity(Vector2 velocity)
	{
        animator.SetFloat("VelocityX", velocity.x == 0 ? 0 : Mathf.Sign(velocity.x));
        animator.SetFloat("VelocityY", -5 <= velocity.y && velocity.y <= 5 ? 0 : Mathf.Sign(velocity.y));
	}
}
