using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    public Image bgImg;
    public Image joystickImg;
    private Vector3 inputJoystick;

    private void start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
          
            inputJoystick = new Vector3(pos.x * 2, pos.y * 2, 0f);
            inputJoystick = (inputJoystick.magnitude > 1.0f) ? inputJoystick.normalized : inputJoystick;

            Debug.Log(inputJoystick);
           // move joystickImg
            joystickImg.rectTransform.anchoredPosition = new Vector3(inputJoystick.x * (bgImg.rectTransform.sizeDelta.x / 2),
                inputJoystick.y * (bgImg.rectTransform.sizeDelta.y / 2));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputJoystick = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (inputJoystick.x != 0)
            return inputJoystick.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputJoystick.y != 0)
            return inputJoystick.y;
        else
            return Input.GetAxis("Vertical");
    }
}
