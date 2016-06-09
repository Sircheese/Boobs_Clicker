using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickDownController : MonoBehaviour, IPointerDownHandler
{
    public delegate void MyDelegate();
    public MyDelegate Method;

    public void OnPointerDown(PointerEventData eventData)
    {
        Method();
    }
}