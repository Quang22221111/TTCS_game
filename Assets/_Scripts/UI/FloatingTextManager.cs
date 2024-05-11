using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;            public GameObject textPrefab;           
    private List<FloatingText> floatingTexts = new List<FloatingText>();    

    private void Update()
    {
                foreach (FloatingText txt in floatingTexts)
        {         
            txt.UpdateFloatingText();
        }          
    }

        public void Show(string msg,int fontSize, Color color, Vector3 position,Vector3 motion, float duration)
    {
                FloatingText FloatingText = GetFloatingText();

                FloatingText.text.text = msg;
        FloatingText.text.fontSize = fontSize;
        FloatingText.text.color = color;

                
        FloatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
                FloatingText.motion = motion;
                FloatingText.duration = duration;

                
                FloatingText.Show();
    }

        private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.text = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }
        return txt;
    }
}
