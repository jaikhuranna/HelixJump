using UnityEngine;
using System.Collections;

public class ScoreTrigger : MonoBehaviour
{
    private bool triggered = false;
    private LevelManager levelManager;
    
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            GameManager.Instance.AddScore();
            StartCoroutine(DeactivateAfterDelay());
        }
    }
    
    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);  
        if (levelManager != null)
            levelManager.ReturnToPool(transform.parent.gameObject);
        transform.parent.gameObject.SetActive(false);
    }
}