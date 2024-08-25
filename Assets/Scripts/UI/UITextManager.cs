using MPUIKIT;
using TMPro;
using UnityEngine;

namespace JustTanks.UI
{
    public class UITextManager
    {
        public void UpdateText(TMP_Text textElement, string newText)
        {
            textElement.text = newText;
        }

        public void UpdateFillAmount(MPImage fillElement, float fillValue)
        {
            fillElement.fillAmount = fillValue;
        }

        public void SetRender(MPImage renderElement, Sprite newRender)
        {
            renderElement.sprite = newRender;
        }
    }
}
