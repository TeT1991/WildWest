using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableControllElementUIView : MonoBehaviour
{
    [SerializeField] private Image _marker;
    [SerializeField] private Color _interactionColor = Color.green;

    public virtual void ShowInteractions()
    {
        _marker.gameObject.SetActive(true);
    }
    public virtual void HideInteractions()
    {
        _marker.gameObject.SetActive(false);
    }

    public virtual void SetMarker(Vector2 position)
    {
        _marker.gameObject.transform.position = position;
        _marker.color = _interactionColor;
    }
}
