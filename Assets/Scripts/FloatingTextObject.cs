using UnityEngine;
using UnityEngine.UI;

public class FloatingTextObject
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingTextObject()
    {
        if (!active)
            return;

        // the actual time (= right now) - the time the text was actually shown of > the stated duration of the showing process
        if (Time.time - lastShown > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}
