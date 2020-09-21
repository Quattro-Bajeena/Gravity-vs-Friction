using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public CharacterController Movement { get; private set; }
    public CharacterHealth Health { get; private set; }

    public bool IsDead
	{
		get { return Health.IsDead; }
	}

    void Awake()
    {
        Movement = GetComponent<CharacterController>();
        Health = GetComponent<CharacterHealth>();
    }




}