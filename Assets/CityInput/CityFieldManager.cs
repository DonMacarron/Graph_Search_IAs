using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CityFieldManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI cityField;

    [SerializeField]
    public ButtonHandler cityButton;
    public void SetCity() {
        if (cityField.text == "")
        {
            Debug.Log("Introduce la ciudad");
        }
        else {
            Debug.Log(cityField.text);
            PlayerPrefs.SetString("City", cityField.text);
            cityButton.changeEsceneToNext();
        }
    }
}
