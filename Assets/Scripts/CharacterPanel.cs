using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public sealed class CharacterPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform tf;
    private Vector3 beginDragPosition;
    private readonly List<RaycastResult> raycastResults = new();

    private void Awake()
    {
        tf = transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginDragPosition = tf.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        tf.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (var result in raycastResults)
        {
            if (!result.gameObject.TryGetComponent(out CharacterPanel panel) || panel == this) continue;

            SwapPanels(panel);
            return;
        }

        RevertDrag();
    }

    private void SwapPanels(CharacterPanel otherPanel)
    {
        (tf.position, otherPanel.tf.position) = (otherPanel.tf.position, beginDragPosition);
    }


    public void RevertDrag()
    {
        tf.position = beginDragPosition;
    }
}