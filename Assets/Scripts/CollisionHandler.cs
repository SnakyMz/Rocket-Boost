using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("All good!");
                break;

            case "Finish":
                Debug.Log("Finish Line!");
                break;

            case "Fuel":
                Debug.Log("Fueling");
                break;

            default:
                Debug.Log("Boom!");
                break;
        }
    }
}
