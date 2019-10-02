using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
  [SerializeField] AudioClip playSound;
  public void OnMouseDown(){
      AudioSource.PlayClipAtPoint(playSound, Camera.main.transform.position, 0.5f);
      StartCoroutine(SoundPlay());
  }

  private IEnumerator SoundPlay(){
    yield return new WaitForSeconds(1);
    SceneManager.LoadScene("Level1", LoadSceneMode.Single);
  }
}
