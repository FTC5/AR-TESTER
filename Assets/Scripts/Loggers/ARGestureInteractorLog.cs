using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

[RequireComponent(typeof(ARGestureInteractor))]
public class ARGestureInteractorLog : MonoBehaviour
{
    // Start is called before the first frame update
    private ARGestureInteractor arGestureInteractor;
    void Start()
    {
        arGestureInteractor = GetComponent<ARGestureInteractor>();
        arGestureInteractor.dragGestureRecognizer.onGestureStarted += DragGestureRecognizerStarted;
        arGestureInteractor.pinchGestureRecognizer.onGestureStarted += PinchGestureRecognizerStarted;
        arGestureInteractor.twoFingerDragGestureRecognizer.onGestureStarted += TwoFingerDragGestureRecognizerStar;
        arGestureInteractor.selectEntered.AddListener((eventArgs) =>
        {
            Logger.Instance.LogInfo("on select");
            Logger.Instance.LogInfo(eventArgs.interactableObject.transform.name);
        });
        //arGestureInteractor.firstInteractableSelected.selectEntered.AddListener(
        //    (sEvent) => 
        //    { 
        //        Logger.Instance.LogInfo("select entered");
        //        Logger.Instance.LogInfo(sEvent.interactableObject.transform.name);
        //        Logger.Instance.LogInfo(sEvent.interactorObject.transform.name);
        //    }
        //);
    }

    void OnDisable()
    {
        arGestureInteractor.dragGestureRecognizer.onGestureStarted -= DragGestureRecognizerStarted;
        arGestureInteractor.pinchGestureRecognizer.onGestureStarted -= PinchGestureRecognizerStarted;
        arGestureInteractor.twoFingerDragGestureRecognizer.onGestureStarted -= TwoFingerDragGestureRecognizerStar;
    }

    private void DragGestureRecognizerStarted(DragGesture dragGesture)
    {
        Logger.Instance.LogInfo("DragGestureRecognizerStarted executed");
        dragGesture.onStart += (s) =>
        {
            Logger.Instance.LogInfo("DragGesture.onStart executed");
        };
        dragGesture.onUpdated += (s) =>
        {
            Logger.Instance.LogInfo("DragGesture.onUpdated executed");
        };
        dragGesture.onFinished += (s) =>
        {
            Logger.Instance.LogInfo("DragGesture.onFinished executed");
        };
    }

    private void TwoFingerDragGestureRecognizerStar(TwoFingerDragGesture twoFingerDragGesture)
    {
        Logger.Instance.LogInfo("TwoFingerDragGestureRecognizerStarted executed");
        twoFingerDragGesture.onStart += (s) =>
        {
            Logger.Instance.LogInfo("TwoFingerDragGesture.onStart executed");
        };
        twoFingerDragGesture.onUpdated += (s) =>
        {
            Logger.Instance.LogInfo("TwoFingerDragGesture.on Updated executed");
        };
        twoFingerDragGesture.onFinished += (s) =>
        {
            Logger.Instance.LogInfo("TipFingerDragGesture.onFinished executed");
        };
    }

    private void PinchGestureRecognizerStarted(PinchGesture pinchGesture)
    {
        Logger.Instance.LogInfo("PinchGestureRecognizerStarted executed");

        pinchGesture.onStart += (s) =>
        {
            Logger.Instance.LogInfo("PinchGesture.onStart executed");
        };
        pinchGesture.onUpdated += (s) =>
        {
            Logger.Instance.LogInfo("PinchGesture.on Updated executed");
        };
        pinchGesture.onFinished += (s) =>
        {
            Logger.Instance.LogInfo("PinchGesture.onFinished executed");
        };
    }
}