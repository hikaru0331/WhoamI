using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public sealed class CharacterPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ControllerSwitch character;
    [SerializeField] private ControllerType controllerType;
    private Transform tf;
    private Vector3 beginDragPosition;
    private readonly List<RaycastResult> raycastResults = new();

    private void Awake()
    {
        tf = transform;
    }

    private void Start()
    {
        character.SwitchController(controllerType);
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
        (character, otherPanel.character) = (otherPanel.character, character);
        character.SwitchController(controllerType);
        otherPanel.character.SwitchController(otherPanel.controllerType);
    }

    private void RevertDrag()
    {
        tf.position = beginDragPosition;
    }
}