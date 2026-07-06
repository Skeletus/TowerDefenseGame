using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_BuildButtonOnHoverEffect : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private float adjustmnetSpeed = 10f;

    [SerializeField] private float showcaseY;
    [SerializeField] private float defaultY;
    [SerializeField] private float selectedY;

    private float targetY;
    private bool canMove;

    private void Update()
    {
        if (Mathf.Abs(transform.position.y - targetY) > 0.01f && canMove)
        {
            float newPositionY = Mathf.Lerp(
                transform.position.y,
                targetY,
                adjustmnetSpeed * Time.deltaTime
            );

            transform.position = new Vector3(
                transform.position.x,
                newPositionY,
                transform.position.z
            );
        }
    }

    public void ToggleMovement(bool buttonsMenuActive)
    {
        canMove = buttonsMenuActive;
        SetTargetY(defaultY);

        if (buttonsMenuActive == false)
        {
            SetPositionDefault();
        }
    }

    private void SetPositionDefault()
    {
        transform.position = new Vector3(transform.position.x, defaultY, transform.position.z );
    }

    private void SetTargetY(float newY) => targetY = newY;

    public void ShowcaseButton(bool showcase)
    {
        if(showcase)
        {
            SetTargetY(showcaseY);
        }
        else
        {
            SetTargetY(defaultY);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetTargetY(selectedY);
    }

}
