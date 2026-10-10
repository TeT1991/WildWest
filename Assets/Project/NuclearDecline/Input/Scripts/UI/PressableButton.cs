using UnityEngine;

public class PressableButton : InteractableControllElementUIView
{
    public override void ShowInteractions()
    {
        base.SetMarker(Vector2.zero);
        base.ShowInteractions();
    }

    public override void HideInteractions() { }
}
