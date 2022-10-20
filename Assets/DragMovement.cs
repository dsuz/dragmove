// 日本語対応済
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMovement : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] float _rayLength = 10f;
    [SerializeField] float _sensitivity = 0.3f;
    Draggable _target;
    Draggable _grabbed;
    Vector3 _lastMousePosition;

    void Start()
    {

    }

    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit, _rayLength, _layerMask))
        {
            Draggable d = hit.collider.gameObject.GetComponent<Draggable>();

            if (d)
            {
                if (!d.Equals(_grabbed))
                {
                    _grabbed?.Release();
                    _grabbed = null;
                }

                if (!d.Equals(_target))
                {
                    _target?.Release();
                    _target = d;
                    _target.Focus();
                }
            }
            else
            {
                if (_target)
                {
                    _target.Release();
                }

                if (_grabbed)
                {
                    _grabbed.Release();
                }
            }
        }
        else
        {
            _target?.Release();
            _target = null;
            _grabbed = null;
        }

        if (_grabbed)
        {
            Vector3 deltaMousePosition = Input.mousePosition - _lastMousePosition;
            _grabbed.Move(_sensitivity * deltaMousePosition.x, 0, _sensitivity * deltaMousePosition.y);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (_grabbed)
            {
                _target = null;
                _grabbed = null;
            }
            else if (_target)
            {
                _grabbed = _target;
                _grabbed.Grab();
            }
        }

        Debug.DrawRay(Camera.main.transform.position, r.direction.normalized * _rayLength, _target ? Color.red : Color.white);
        _lastMousePosition = Input.mousePosition;
    }
}