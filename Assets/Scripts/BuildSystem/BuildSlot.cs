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
    private bool buildSlotAvailable = true;

    private Coroutine currentMovementUpCoroutine;
    private Coroutine moveToDefaultCoroutine;

    private void Awake()
    {
        tileAnim = FindFirstObjectByType<TileAnimator>();
        buildManager = FindFirstObjectByType<BuildManager>();
        defaultPosition = transform.position;
    }

    private void Start()
    {
        if(buildSlotAvailable == false)
        {
            transform.position += new Vector3(0, .1f);
        }
    }

    public void SetSlotAvaliableTo(bool value) => buildSlotAvailable = value;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (buildSlotAvailable == false)
        {
            return;
        }

        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if(buildManager.GetSelectedSlot() == this)
        {
            return;
        }

        buildManager.EnableBuildMenu();
        buildManager.SelectBuildSlot(this);
        MoveTileUp();

        tileCanBeMoved = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buildSlotAvailable == false)
        {
            return;
        }

        if (tileCanBeMoved == false)
        {
            return;
        }
        MoveTileUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buildSlotAvailable == false)
        {
            return;
        }

        if(tileCanBeMoved == false)
        {
            return;
        }

        if(currentMovementUpCoroutine != null)
        {
            Invoke(nameof(MoveToDefaultPosition), tileAnim.GetTravelDuration());
        }
        else
        {
            MoveToDefaultPosition();
        }
    }

    private void MoveTileUp()
    {
        Vector3 targetPosition = defaultPosition + new Vector3(0, tileAnim.GetBuildOffset(), 0);

        currentMovementUpCoroutine = StartCoroutine(tileAnim.MoveTileCoroutine(transform, targetPosition));
    }

    private void MoveToDefaultPosition()
    {
        moveToDefaultCoroutine = StartCoroutine(tileAnim.MoveTileCoroutine(transform, defaultPosition));
    }

    public void SnapToDefaultPositionInmidiatly()
    {
        if (moveToDefaultCoroutine != null)
        {
            StopCoroutine(moveToDefaultCoroutine);
        }

        transform.position = defaultPosition;
    }

    public void UnselectTile()
    {
        MoveToDefaultPosition();
        tileCanBeMoved = true;
    }

    public Vector3 GetBuildPosition(float yOffset) => defaultPosition + new Vector3(0, yOffset);
}
