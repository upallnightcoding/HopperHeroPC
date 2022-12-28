using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    public void display(int points)
    {
        scoreText.text = points.ToString();
    }
}
