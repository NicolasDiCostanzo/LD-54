using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    [SerializeField]
    Image endCreditsImage;
    [SerializeField]
    Image eyeClosing;
    [SerializeField]
    Image endText;
    [SerializeField]
    Button quitButton;
    [SerializeField]
    Sprite[] eyeAnimation;

    UiManager uiManager;

    AudioSource audiosource;
    [SerializeField]
    AudioClip audioClip;

    void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        uiManager = FindObjectOfType<UiManager>();

        quitButton.onClick.AddListener(uiManager.QuitGame);
    }

    public void Trigger()
    {
        eyeClosing.gameObject.SetActive(true);
        StartCoroutine(nameof(PlayEndAnimation));
    }

    IEnumerator PlayEndAnimation()
    {
        // fade to black
        float duration = 2f;
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;

            Color color = eyeClosing.color;
            color.a = Mathf.Lerp(0, 1, t / duration);
            eyeClosing.color = color;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        audiosource.PlayOneShot(audioClip);

        yield return new WaitForSeconds(1f);

        endCreditsImage.gameObject.SetActive(true);

        foreach (Sprite eyeFrame in eyeAnimation)
        {
            eyeClosing.sprite = eyeFrame;
            yield return new WaitForSeconds(0.2f);
        }
        eyeClosing.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        endText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        quitButton.gameObject.SetActive(true);
        yield return null;
    }
}