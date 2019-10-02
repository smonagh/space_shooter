using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] AudioClip exitSound;
    public void ExitGame(){
        AudioSource.PlayClipAtPoint(exitSound, Camera.main.transform.position, 0.5f);
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame(){
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
