using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    bool facingRight = true;
    Vector2 movement;
    private Vector2 pointerInput;
    public Vector2 PointerInput => pointerInput;

    [SerializeField]
    private InputActionReference pointerPosition;

    private WeaponParent weaponParent;
    

    private void Awake() 
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
    }

    // Update is called once per frame
    void Update()
    {
        // input

        pointerInput = GetPointerInput();
        weaponParent.PointerPosition = pointerInput;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x < 0 && facingRight)
        {
            flip();
        }
        else if (movement.x > 0 && !facingRight)
        {
            flip();
        }
    }

    void FixedUpdate()
    {
        // movement

        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private Vector2 GetPointerInput() 
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
