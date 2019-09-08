using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        Vector3 direction = new Vector2(Input.GetAxis("Vertical"),0);

        rb2d.MovePosition(rb2d.transform.position + direction);
    }
}
