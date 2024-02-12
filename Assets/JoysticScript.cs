using UnityEngine;
using UnityEngine.EventSystems;

public class JoysticScript : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickBackground;
    private RectTransform joystickHandle;

    private Vector2 inputDirection = Vector2.zero;
    private bool isJoystickBeingDragged = false;

    void Start()
    {
        joystickBackground = GetComponent<RectTransform>();
        joystickHandle = transform.GetChild(0).GetComponent<RectTransform>(); // Assumes handle is the first child
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - (Vector2)joystickBackground.position - new Vector2(joystickBackground.rect.width / 2f, joystickBackground.rect.height / 2f);
        inputDirection = direction.normalized;
        joystickHandle.anchoredPosition = inputDirection * Mathf.Min(direction.magnitude, joystickBackground.rect.width * 0.5f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isJoystickBeingDragged = true;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
        isJoystickBeingDragged = false;
    }

    public Vector2 GetInputDirection()
    {
        return inputDirection;
    }
}
