using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OutlineOnHover : MonoBehaviour
{
    OutlineFx.OutlineFx _outline;
    Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        _outline = GetComponent<OutlineFx.OutlineFx>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue(); // screen-space (pixels)
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos); // convert to world-space

        bool isInside = collider.OverlapPoint(worldPos);

        _outline.enabled = isInside;
    }
}
