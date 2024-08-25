using UnityEngine;

namespace JustTanks.UI
{
    public class UIVisibilityManager
    {
        public void SetVisible(GameObject uiElement, bool isVisible)
        {
            uiElement.SetActive(isVisible);
        }
    }
}
