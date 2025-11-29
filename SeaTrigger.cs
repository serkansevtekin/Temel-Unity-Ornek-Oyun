using UnityEngine;

public class SeaTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.UIinstance.OpenGameOverPanel();
           
        }
    }
}
