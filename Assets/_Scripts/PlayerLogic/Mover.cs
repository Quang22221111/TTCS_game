using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private BoxCollider2D BoxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

        protected float ySpeed = 0.75f;         
    protected float xSpeed = 1.0f;

        private Vector3 originalSize;

    protected virtual void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        originalSize = transform.localScale;
    }

        protected virtual void UpdateMotor(Vector3 input,float SPMultiple)
    {
                moveDelta = new Vector3(input.x * xSpeed * SPMultiple, input.y * ySpeed * SPMultiple, 0);

                if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.y);

                        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

                                                                                hit = Physics2D.BoxCast(transform.position, BoxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        else pushDirection = Vector3.zero;

        hit = Physics2D.BoxCast(transform.position, BoxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        else pushDirection = Vector3.zero;
    }
}
