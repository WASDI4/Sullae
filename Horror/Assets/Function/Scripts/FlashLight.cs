using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashLight : MonoBehaviour
{   
    public Light light;
    public TMP_Text text;
    public TMP_Text batteryText;

    public float batteryTime = 100;
    public float batteries =0;
    public AudioSource flashOn;
    public AudioSource flashOff;

    private bool on;
    private bool off;

    void Start()
    {
        text.text = "Flashlight : " + batteryTime + "%" ;
        light = GetComponent<Light>();

        off = true;
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = batteryTime.ToString("0") + "%";
        batteryText.text = batteries.ToString();

        if(Input.GetKeyDown(KeyCode.F) && off)
        {
            flashOn.Play();
            light.enabled = true;
            on = true;
            off = false;
        } 
        else if (Input.GetKeyDown(KeyCode.F) && on)
        {
            flashOff.Play();
            light.enabled = false;
            on = false;
            off = true;  
        }

        if(on)
        {
            batteryTime -= 1 * Time.deltaTime;
        }
        if(batteryTime <= 0)
        {
            light.enabled = false;
            on = false;
            off = true;
            batteryTime = 0;
        }

        if (batteryTime >= 100)
        {
            batteryTime = 100;
        }

        if (Input.GetKeyDown(KeyCode.R) && batteries >= 1)
        {
            batteries -= 1;
            batteryTime += 50;
        }

        if (Input.GetKeyDown(KeyCode.R) && batteries == 0)
        {
            return;
        }

        if ( batteries <= 0)
        {
            batteries = 0;
        }



    }
}
