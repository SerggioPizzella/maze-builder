using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ShowValue : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;

        Slider slider;

        void Start()
        {
            // Get the slider component and cache it
            slider = GetComponent<Slider>();
        }

        // Update text on UI to show the current value of the slider.
        public void UpdateTextValue()
        {
            text.text = slider.value.ToString();
        }
    }
}
