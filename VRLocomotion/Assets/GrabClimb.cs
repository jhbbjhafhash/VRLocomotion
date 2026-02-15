using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabClimb : MonoBehaviour
{
    private XRSimpleInteractable interactable;
    private ClimbController climbController;
    private bool isGrabbing;
    private Vector3 handPosition;

    private void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        climbController = GetComponentInParent<ClimbController>();
        isGrabbing = false;
    }
    public void Grab()
    {
        isGrabbing = true;
        handPosition = InteractorPosition();
        climbController.Grab();
    }

    private Vector3 InteractorPosition()
    {
        List<IXRHoverInteractor> interactors = interactable.interactorsHovering;
        if (interactors.Count > 0 && interactors[0] is XRBaseInteractor xrBaseInteractor)
            return xrBaseInteractor.transform.position;
        else
            return handPosition;
    }
    private void Update()
    {
        if (isGrabbing)
        {
            Vector3 delta = handPosition - InteractorPosition();
            climbController.Pull(delta);
            handPosition = InteractorPosition();
        }
    }

    public void Release()
    {
        isGrabbing = false;
        climbController.Release();
    }
}
