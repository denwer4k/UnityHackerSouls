using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOfTheEnd : MonoBehaviour
{
    [SerializeField] private string nameOfScene;

    IEnumerator WaitForKeyPressing()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.G));
        SceneManager.LoadScene(nameOfScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TheDoor"))
            StartCoroutine(WaitForKeyPressing());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TheDoor"))
            StopCoroutine(WaitForKeyPressing());
    }
}
