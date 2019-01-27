using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCaller : MonoBehaviour {

  float startTime;
  public string sceneName;

  // Start is called before the first frame update
  void Start() {
    startTime = Time.time;
  }

  // Update is called once per frame
  void Update() {
    if (Time.time - startTime > 5) {
       SceneManager.LoadScene(this.sceneName);
    }
  }
}
