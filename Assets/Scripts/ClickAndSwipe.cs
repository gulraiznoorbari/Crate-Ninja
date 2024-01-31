using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _mousePosition;
    private TrailRenderer _trailRenderer;
    private BoxCollider _boxCollider;

    private bool swiping = false;

    private void Awake()
    {
        _camera = Camera.main;
        _trailRenderer = GetComponent<TrailRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _trailRenderer.enabled = false;
        _boxCollider.enabled = false;
    }

    private void Update()
    {
        if (GameManager.instance.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }
            if (swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    private void UpdateMousePosition()
    {
        _mousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = _mousePosition;
    }

    private void UpdateComponents()
    {
        _trailRenderer.enabled = swiping;
        _boxCollider.enabled = swiping;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
