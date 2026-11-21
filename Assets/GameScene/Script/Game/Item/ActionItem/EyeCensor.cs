using UnityEngine;

public class EyeCensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IHelthSystem>(out var helth))
        {
            helth.ModifyHelth(-20, 1);
        }
    }
}
