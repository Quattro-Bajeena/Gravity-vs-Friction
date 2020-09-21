using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    Transform character;
    [SerializeField] float offset;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void AssignCharacter(Transform _character)
	{
        character = _character;
	}

    // Update is called once per frame
    void LateUpdate()
    {
        if(character)
            transform.position = new Vector3(character.position.x, character.position.y + offset, transform.position.z);
    }
}
