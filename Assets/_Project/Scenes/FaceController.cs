using UnityEngine;
using System.Collections;

public class FaceController : MonoBehaviour {

    SkinnedMeshRenderer face;
    float faceValue;

    int randomNumber;

    void Awake () {
        face = GetComponent<SkinnedMeshRenderer>();
        InvokeRepeating("faceAction", 0, Random.Range(2, 6));
    }

    void Update(){
        faceValue = Mathf.Lerp(faceValue, 0, 0.08f);
        face.SetBlendShapeWeight(0, faceValue);
    }
	

    void faceAction()
    {
        randomNumber = Mathf.RoundToInt(Random.Range(1.0f, 3.0f));
        Debug.Log(randomNumber);


        switch (randomNumber){

            //quick blink
            case 1: StartCoroutine("quickBlink");
                    break;
            //double blink
            case 2:
                StartCoroutine("twoBlink");
                break;
            
        }
    }

    IEnumerator quickBlink()
    {
        faceValue = 120;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator twoBlink()
    {
        faceValue = 120;
        yield return new WaitForSeconds(0.3f);
        faceValue = 120;
        yield return new WaitForSeconds(0.2f);
    }
}
