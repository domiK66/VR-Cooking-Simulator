using Oculus.Interaction;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GrabTransformer : MonoBehaviour, ITransformer
{
    private float smoothTime = 0.1f;

    private IGrabbable grabbable;
    private Pose grabDeltaInLocalSpace;
    private Vector3 actPosition;
    private bool collisionDetected;
    private Vector3 velocity;

    public void Initialize(IGrabbable grabbable)
    {
        this.grabbable = grabbable;
    }

    // Get position when the grabable is grabed
    public void BeginTransform()
    {
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Pose grabPoint = grabbable.GrabPoints[0];
        var targetTransform = grabbable.Transform;
        grabDeltaInLocalSpace = new Pose(
            targetTransform.InverseTransformVector(grabPoint.position - targetTransform.position),
            Quaternion.Inverse(grabPoint.rotation) * targetTransform.rotation
        );
    }

    // Update position and rotation of the grabable, while grabbing
    public void UpdateTransform()
    {
        Pose grabPoint = grabbable.GrabPoints[0];
        var targetTransform = grabbable.Transform;

        // Get target rotation
        var targetRotation = grabPoint.rotation * grabDeltaInLocalSpace.rotation;

        // Get target position
        Vector3 targetPosition;

        targetPosition =
            grabPoint.position - targetTransform.TransformVector(grabDeltaInLocalSpace.position);

        // Set position
        targetTransform.position = Vector3.SmoothDamp(
            targetTransform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        // Set rotation
        targetTransform.rotation = Quaternion.Slerp(
            targetTransform.rotation,
            targetRotation,
            smoothTime
        );
    }

    public void EndTransform()
    {
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
