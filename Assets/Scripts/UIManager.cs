using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
		[SerializeField] TextMeshProUGUI text;

        // Update text on UI to show the current value of the slider.
        public void UpdateTextFromSlider(Slider slider)
        {
            text.text = slider.value.ToString();
        }
    }
}
