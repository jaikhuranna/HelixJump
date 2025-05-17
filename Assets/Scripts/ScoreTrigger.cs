using UnityEngine;
using System.Collections;

public class ScoreTrigger : MonoBehaviour
{
    private bool triggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Ball"))
        {
            triggered = true;
            GameManager.Instance.AddScore();
            
            StartCoroutine(DeactivateAfterDelay());
        }
    }
    
    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
