using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float jumpIteration = 10f;

    void Update()
    {
        //TODO: remove TESTING
        if (Input.GetKeyDown(KeyCode.G))
        {
            Shake();
        }
    }

    public void Shake()
    {
        // determines height of shake
        float height = Mathf.PerlinNoise(jumpIteration, 0.0f) * 5.0f;
        height = height * height * 0.2f;

        // determines degrees of shake
        float shakeAmount = height;
        // determines how long the shake will last
        float shakePeriodTime = 0.25f;
        // determines how quickly the shake will drop off
        float dropOffTime = 1.25f;

        // creates the shake tween until drop off time
        LTDescr shakeTween = LeanTween.rotateAroundLocal(gameObject, Vector3.right, shakeAmount, shakePeriodTime)
            .setEase(LeanTweenType.easeShake)
            .setLoopClamp()
            .setRepeat(-1);
        
        LeanTween.value(gameObject, shakeAmount, 0.0f, dropOffTime).setOnUpdate((float val) =>
        {
            shakeTween.setTo(Vector3.right * val);
        }).setEase(LeanTweenType.easeOutQuad);
    }

}