using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    Transform character;
    [SerializeField] float offset;
    [SerializeField] float smoothTime;
    [SerializeField] float zoomSpeed;

    Vector2 smoothVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void AssignCharacter(Transform _character)
	{
        character = _character;
	}

	private void Update()
	{
        Vector2 scroll = Input.mouseScrollDelta;
        cam.orthographicSize -= scroll.y* zoomSpeed * Time.deltaTime;

        
    }

	// Update is called once per frame
	void LateUpdate()
    {
        if (character)
        {

            //if (Vector2.Distance(transform.position, character.position) > offset)
            {

                Vector2 newPosition = Vector2.SmoothDamp(transform.position, character.position, ref smoothVelocity, smoothTime);
                transform.position = new Vector3((float)Math.Round(newPosition.x, 2), (float)Math.Round(newPosition.y, 2), transform.position.z);

            }
        }

    }
}
