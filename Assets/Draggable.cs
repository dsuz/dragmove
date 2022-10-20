// 日本語対応済
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] float _intensity = 3f;
    Renderer _r;
    Material _mat;
    DragStatus _status = DragStatus.none;
    Rigidbody _rb;

    void Start()
    {
        _r = GetComponent<Renderer>();
        _mat = _r.material;
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public void Move(float x, float y, float z)
    {
        Vector3 pos = transform.position + new Vector3(x, y, z);
        _rb.MovePosition(pos);
    }

    public void Release()
    {
        Debug.Log($"{name} released");
        _status = DragStatus.none;
        _mat.SetColor("_EmissionColor", new Color());
        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Focus()
    {
        Debug.Log($"{name} got focus");
        _status = DragStatus.focus;
        _mat.SetColor("_EmissionColor", _mat.color * _intensity);
    }

    public void Grab()
    {
        Debug.Log($"{name} got grabbed");
        _status = DragStatus.grab;
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
}

enum DragStatus
{
    none,
    focus,
    grab,
}