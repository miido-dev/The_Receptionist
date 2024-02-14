using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    public int Id;
    private bool _dragging;
    private Vector2 _offset;
    private Vector2 _originalPosition;
    private Transform _originalTransform;
    private Transform _slotTransform;
    [SerializeField] private Collider2D _slotCollider;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    private void Start()
    {
        _originalTransform = transform.parent;
    }

    private void Update()
    {
        if(!_dragging) return;

        var mousePosition = GetMousePosition();
        transform.position = mousePosition - _offset;

    }
    
    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        _dragging = true;
        _offset = GetMousePosition() - (Vector2)transform.position;
        if(_slotCollider != null) _slotCollider.enabled = true;
    }

    private void OnMouseUp()
    {
        if (_slotTransform && Vector2.Distance(transform.position, _slotTransform.position) < 2f) 
        {
            Debug.Log($"Slot Area");

            transform.parent = _slotTransform;
            transform.localPosition = Vector3.zero;
            _slotCollider.enabled = false;
            EventManager.ItemsPlacedCheckTrigger.Invoke();

        }
        else         
        {
            Debug.Log($"Original Area");

            transform.parent = _originalTransform;
            transform.localPosition = Vector3.zero;
            // transform.position = _originalPosition;
        }
        _dragging = false;

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        _slotTransform = col.transform;
        _slotCollider = col;
    }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     _slotTransform = null;
    // }

    public GameObject CheckCorrectPlacement()
    {
        return Id == int.Parse(_slotCollider.name) ?  transform.Find("Right").gameObject : transform.Find("Wrong").gameObject;
    }
}
