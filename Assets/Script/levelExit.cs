using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(loadNextLevel());
    }
    IEnumerator loadNextLevel()
    {
        yield return new WaitForSeconds(levelLoadDelay);
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
