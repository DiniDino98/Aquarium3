using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
public IEnumerator Dizzy(float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.Rotate(0f, 300 * Time.deltaTime, 0f, Space.Self);

            elapsed += Time.deltaTime;

            yield return null;
        }
    }
}
