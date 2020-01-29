using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    Text text;
    float Score=0;
   // [SerializeField]int scorePerHit = 25;

    // Start is called before the first frame update
    private void Start()
    {
        text = GetComponent<Text>();
        text.text = Score.ToString();
    }

    public void ScoreHit(int points)
    {
        //float  tempScore = float.Parse(text.text);
        Score += points;
        text.text = Score.ToString();
    }
}
