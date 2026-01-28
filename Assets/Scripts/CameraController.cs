using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
   [Header("Target Object")]
   [SerializeField] private Transform target;

   [Header("Follow Settings")]
   [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); // keep camera positioned relative to the player
   [SerializeField] private float smoothTime = 0.2f;                    // controls responsiveness. 0.05 is snappy, 0.2 is natural, 0.5 is floaty

   private Vector3 velocity = Vector3.zero;
   
   void LateUpdate()
   {
      // ensure the camera moves after the player moves to prevent jitters.
      //Debug.Log("Camera LateUpdate running");
      
      if (target == null) return;
      
         //Set the Camera Position to the target position;
         Vector3 targetPosition = CalculateTargetPosition();
         targetPosition = ClampToBounds(targetPosition);

         transform.position = SmoothFollow(targetPosition);
   }

   Vector3 CalculateTargetPosition()
      {
         return target.position + offset;
      }

   Vector3 SmoothFollow(Vector3 targetPosition)
      {
         // simulates inertia to create a smooth start and stop
         return Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
         );
      }

   Vector3 ClampToBounds(Vector3 position)
   {
      return position;
   }
   
}
