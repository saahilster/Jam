using System.Collections;
using TMPro;
using UnityEngine;

public class LookInteractor : MonoBehaviour
{
    [SerializeField] private GameObject dialogueGO;
    [SerializeField] private TextMeshProUGUI dialogueText;
     [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask triggerLayer;

    private void Update()
    {
        CastRay();
    }

    private void CastRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, triggerLayer))
        {
            Debug.Log("found");
            DialogueContainer dialogueContainer = hit.collider.GetComponent<DialogueContainer>();

            if (dialogueContainer != null)
            {
                Debug.Log("Found script");
                StartCoroutine(OnDialogueFound(dialogueContainer));
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
    }

    private IEnumerator OnDialogueFound(DialogueContainer dialogueContainer)
    {   
        dialogueGO.SetActive(true);
        dialogueText.text = dialogueContainer.trigger.dialogue;
        yield return new WaitForSeconds(5);
        dialogueText.text = "";
        dialogueGO.SetActive(false);
    }
}
