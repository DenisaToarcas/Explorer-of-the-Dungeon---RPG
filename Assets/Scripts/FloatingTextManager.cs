using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;


    private List<FloatingTextObject> floatingTexts = new List<FloatingTextObject>();


    private void Update()
    {
        foreach (FloatingTextObject txt in floatingTexts)
            txt.UpdateFloatingTextObject();
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingTextObject floatingText = GetFloatingTextObject();

        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;

        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); //Tranfer world space to screen space so we can use it in the UI;
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show(); 
    }
    private FloatingTextObject GetFloatingTextObject()
    {
    
    FloatingTextObject txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FloatingTextObject();
            GameObject textPrefab1 = textPrefab;
            txt.go = Instantiate(textPrefab1);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
