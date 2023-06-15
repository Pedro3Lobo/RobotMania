using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GetPublicVariables : MonoBehaviour
{
    public MonoBehaviour scriptToInspect; // The script to inspect, set in the Inspector window
    public List<GameObject> InputObject;
    public List<GameObject> OutputObject;
    public List<Toggle> Input = new List<Toggle>();
    public List<Toggle> Output = new List<Toggle>();
    public static int Number_Input = 0, Number_Output = 0;
    public static List<bool> Input_On = new List<bool>();
    public static List<bool> Output_On = new List<bool>();
    public static List<string> FieldsName = new List<string>();
    public static List<float> FieldsValues = new List<float>();
    public FieldInfo[] fields;

    private void Start()
    {
        if (scriptToInspect == null)
        {
            Debug.LogError("Please set the scriptToInspect field in the Inspector window.");
            return;
        }

        // Get all public instance fields defined in the script
        fields = scriptToInspect.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        // Log the name and value of each public field to the console
        foreach (FieldInfo field in fields)
        {
            try
            {
             
                Debug.Log(field.Name + ": " + field.GetValue(scriptToInspect));
                FieldsName.Add(field.Name);
                FieldsValues.Add((float)field.GetValue(scriptToInspect));
                
            }
            catch (InvalidCastException)
            {
                Debug.LogWarning("Ignoring variable " + field.Name + " as it cannot be converted to float.");
            }
        }
        SetToggleTexts();
        SetToggleTexts2();
    }

    private void Update()
    {
        if (scriptToInspect == null)
        {
            Debug.LogError("Please set the scriptToInspect field in the Inspector window.");
            return;
        }
        
    }

    private void FixedUpdate()
    {
        fields = scriptToInspect.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        int i = 0;
        // Log the name and value of each public field to the console
        foreach (FieldInfo field in fields)
        {
            try
            {
                FieldsValues[i] = (float)field.GetValue(scriptToInspect);

                Debug.Log("Print value saved" + (float)field.GetValue(scriptToInspect));
            }
            catch (InvalidCastException)
            {
                Debug.LogWarning("Ignoring variable " + field.Name + " as it cannot be converted to float.");
            }
            i++;
        }
    }

    private void SetToggleTexts()
    {
        for (int i = 0; i < Input.Count; i++)
        {
            if (i<= fields.Length-1) {
                Debug.Log("i=" + i);
                Input[i].GetComponentInChildren<Text>().text = fields[i].Name;
            }
            else
            {
                InputObject[i].SetActive(false);
            }
        }
        
    }
    private void SetToggleTexts2()
    {
        for (int i = 0; i < Output.Count; i++)
        {
            if (i <= fields.Length - 1)
            {
                Output[i].GetComponentInChildren<Text>().text = fields[i].Name;
            }
            else
            {
                OutputObject[i].SetActive(false);
            }

        }
    }
    
    public void DataSemaphore()
    {
        Input_On.Clear();
        Output_On.Clear();
        Number_Input = 0;
        Number_Output = 0;
        foreach (Toggle tog in Input)
        {
            Input_On.Add(tog.isOn);
            Output_On.Add(!tog.isOn);
            if (tog.isOn)
            {
                Number_Input++;
                Debug.Log("Number Of Inputs: "+Number_Input);
            }
            else
            {
                Number_Output++;
                Debug.Log("Number Of Outputs: " + Number_Output);
            }
        }
    }

    


}
