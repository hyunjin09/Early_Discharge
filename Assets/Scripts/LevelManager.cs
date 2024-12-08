using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour{
    public float s1, s2, s3;

    public Button sb1, sb2, sb3, sb4, sb5;

    public Text st1, st2, st3;

    private void Start(){
        s1 = PlayerPrefs.GetFloat("s1", -1f);
        s2 = PlayerPrefs.GetFloat("s2", -1f);
        s3 = PlayerPrefs.GetFloat("s3", -1f);


        if(s1 != -1){
            TimeSpan timeSpan = TimeSpan.FromSeconds(s1); // Convert to TimeSpan
            string s1time = timeSpan.ToString("mm':'ss'.'ff"); // Format as mm:ss.ff
            st1.text = s1time; // Assign formatted string to UI text

            sb2.interactable = true;
        }

        if(s2 == -1) {
        } else{
            sb3.interactable = true;
            TimeSpan timeSpan = TimeSpan.FromSeconds(s2); // Convert to TimeSpan
            string s2time = timeSpan.ToString("mm':'ss'.'ff"); // Format as mm:ss.ff
            st2.text = s2time; // Assign formatted string to UI text

        }

        if(s3 == -1) {
        } else{
            TimeSpan timeSpan = TimeSpan.FromSeconds(s3); // Convert to TimeSpan
            string s3time = timeSpan.ToString("mm':'ss'.'ff"); // Format as mm:ss.ff
            st3.text = s3time; // Assign formatted string to UI text
        }
    }




}