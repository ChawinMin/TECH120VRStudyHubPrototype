using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemDescription : MonoBehaviour
{
    public GameObject descriptionText; // Assign your text object here

    private void OnEnable()
    {
        // Register the grab events
        var interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.onSelectEntered.AddListener(OnGrab);
            interactable.onSelectExited.AddListener(OnRelease);
        }
    }

    private void OnDisable()
    {
        // Unregister the grab events
        var interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.onSelectEntered.RemoveListener(OnGrab);
            interactable.onSelectExited.RemoveListener(OnRelease);
        }
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        if (descriptionText != null)
        {
            descriptionText.SetActive(true); // Show the text
        }
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        if (descriptionText != null)
        {
            descriptionText.SetActive(false); // Hide the text
        }
    }
}
