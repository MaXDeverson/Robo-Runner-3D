using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoverLogic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float _multiplyer;
     public float PositionX { get; private set; }
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        PositionX = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        PositionX = eventData.delta.x * _multiplyer;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PositionX = 0;
    }
}
