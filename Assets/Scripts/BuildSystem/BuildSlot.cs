using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private TileAnimator tileAnim;
    private Vector3 defaultPosition;

    private void Awake()
    {
        tileAnim = FindFirstObjectByType<TileAnimator>();
        defaultPosition = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("tile was selected");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MoveTileUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MoveDefaultPosition();
    }

    private void MoveTileUp()
    {
        Vector3 targetPosition = transform.position + new Vector3(0, tileAnim.GetBuildOffset(), 0);

        tileAnim.MoveTile(transform, targetPosition);
    }

    private void MoveDefaultPosition()
    {
        tileAnim.MoveTile(transform, defaultPosition);
    }
}
