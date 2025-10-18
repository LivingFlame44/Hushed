using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CameraFade : MonoBehaviour
{
    public UnityEvent[] eventAfterFade;

    public KeyCode key = KeyCode.Space; // Which key should trigger the fade?
    public float speedScale = 1f;
    public Color fadeColor = Color.black;
    // Rather than Lerp or Slerp, we allow adaptability with a configurable curve
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1),
        new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public bool startFadedOut = false;

    public GameObject clickBlockPanel;

    private float alpha = 0f;
    private Texture2D texture;
    private int direction = 0;
    private float time = 0f;

    private void Start()
    {
        if (startFadedOut) alpha = 1f; else alpha = 0f;
        texture = new Texture2D(1, 1);
        texture.wrapMode = TextureWrapMode.Repeat;
        UpdateTextureColor();
    }

    private void Update()
    {
        //if (direction == 0 && Input.GetKeyDown(key))
        //{
        //    if (alpha >= 1f) // Fully faded out
        //    {
        //        alpha = 1f;
        //        time = 0f;
        //        direction = 1;
        //    }
        //    else // Fully faded in
        //    {
        //        alpha = 0f;
        //        time = 1f;
        //        direction = -1;
        //    }

        //}
    }

    private void UpdateTextureColor()
    {
        if (texture != null)
        {
            texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
            texture.Apply();
        }
    }
    public void OnGUI()
    {
        // Always draw the texture if alpha > 0, regardless of direction
        if (alpha > 0f)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        }

        if (direction != 0)
        {
            time += direction * Time.deltaTime * speedScale;
            alpha = Curve.Evaluate(time);
            UpdateTextureColor(); // Update texture with new alpha

            if (alpha <= 0f || alpha >= 1f)
            {
                direction = 0;
                // Ensure final alpha is exactly 0 or 1
                alpha = Mathf.Clamp01(alpha);
                UpdateTextureColor();
            }
        }
    }

    public void Fade()
    {
        if (direction == 0)
        {
            if (alpha >= 1f) // Fully faded out
            {
                alpha = 1f;
                time = 0f;
                direction = 1;  

            }
            else // Fully faded in
            {
                alpha = 0f;
                time = 1f;
                direction = -1;

                StartCoroutine(FadeOutTimer());
            }
        }
    }

    public IEnumerator FadeOutTimer()
    {
        clickBlockPanel.SetActive(true);

        yield return new WaitForSeconds(2);
        if (direction == 0)
        {
            if (alpha >= 1f) // Fully faded out
            {
                alpha = 1f;
                time = 0f;
                direction = 1;

                clickBlockPanel.SetActive(false);
                StopCoroutine(FadeOutTimer());

            }
            else // Fully faded in
            {
                alpha = 0f;
                time = 1f;
                direction = -1;

                StopCoroutine(FadeOutTimer());
            }
        }
    }

    public void EventOnFade(int index)
    {
        if (direction == 0)
        {
            if (alpha >= 1f) // Fully faded out
            {
                alpha = 1f;
                time = 0f;
                direction = 1;

            }
            else // Fully faded in
            {
                alpha = 0f;
                time = 1f;
                direction = -1;

                StartCoroutine(EventOnFadeOutTimer(index));
            }
        }
    }

    public IEnumerator EventOnFadeOutTimer(int index)
    {
        clickBlockPanel.SetActive(true);

        yield return new WaitForSeconds(2);
        if (direction == 0)
        {
            if (alpha >= 1f) // Fully faded out
            {
                alpha = 1f;
                time = 0f;
                direction = 1;

                clickBlockPanel.SetActive(false);
                yield return new WaitForSeconds(2);
                eventAfterFade[index].Invoke();
                StopCoroutine(FadeOutTimer());

            }
            else // Fully faded in
            {
                alpha = 0f;
                time = 1f;
                direction = -1;
                yield return new WaitForSeconds(2);
                eventAfterFade[index].Invoke();
                StopCoroutine(FadeOutTimer());
            }
        }
    }

    public void ExecuteEventAfterFade(int eventIndex)
    {
        if (eventIndex < 0 || eventIndex >= eventAfterFade.Length)
        {
            Debug.LogWarning($"Event index {eventIndex} is out of range!");
            return;
        }

        // Start fading out if not already black
        if (alpha < 1f)
        {
            StartCoroutine(FadeOutAndExecuteEvent(eventIndex));
        }
        else // Already faded to black, execute immediately
        {
            eventAfterFade[eventIndex].Invoke();
        }
    }

    private IEnumerator FadeOutAndExecuteEvent(int eventIndex)
    {
        clickBlockPanel.SetActive(true);

        // Start fade out
        if (direction == 0 && alpha < 1f)
        {
            alpha = 0f;
            time = 1f;
            direction = -1;
        }

        // Wait until fully faded to black (alpha >= 1f)
        while (alpha < 1f)
        {
            yield return null;
        }

        // Ensure we're fully black
        direction = 0;
        alpha = 1f;
        UpdateTextureColor();

        // Execute the event
        eventAfterFade[eventIndex].Invoke();

        clickBlockPanel.SetActive(false);
    }
}
