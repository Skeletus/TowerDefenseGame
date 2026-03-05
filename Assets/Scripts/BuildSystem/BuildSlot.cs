using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private TileAnimator tileAnim;
    private BuildManager buildManager;
    private Vector3 defaultPosition;

    private bool tileCanBeMoved = true;

    private Coroutine currentMovementUpCoroutine;

    private void Awake()
    {
        tileAnim = FindFirstObjectByType<TileAnimator>();
        buildManager = FindFirstObjectByType<BuildManager>();
        defaultPosition = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        buildManager.SelectBuildSlot(this);
        MoveTileUp();

        tileCanBeMoved = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(tileCanBeMoved == false)
        {
            return;
        }
        MoveTileUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(tileCanBeMoved == false)
        {
            return;
        }

        if(currentMovementUpCoroutine != null)
        {
            Invoke(nameof(MoveDefaultPosition), tileAnim.GetTravelDuration());
        }
        else
        {
            MoveDefaultPosition();
        }
    }

    private void MoveTileUp()
    {
        Vector3 targetPosition = defaultPosition + new Vector3(0, tileAnim.GetBuildOffset(), 0);

        currentMovementUpCoroutine = StartCoroutine(tileAnim.MoveTileCo(transform, targetPosition));
    }

    private void MoveDefaultPosition()
    {
        tileAnim.MoveTile(transform, defaultPosition);
    }

    public void UnselectTile()
    {
        MoveDefaultPosition();
        tileCanBeMoved = true;
    }
}
