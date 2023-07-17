using UnityEngine.XR.Interaction.Toolkit;

public class OffsetXRGrabInteractable : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("LeftController") || args.interactorObject.transform.CompareTag("RightController"))
        {
            attachTransform = args.interactorObject.transform;
        }
        
        base.OnSelectEntered(args);
    }
}
