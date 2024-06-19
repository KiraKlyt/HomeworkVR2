using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class SaveLoad : MonoBehaviour
{
    public TMPro.TMP_InputField text;
    public TMPro.TMP_Text label;

	public Button btnLoad;

	[Header("Field")]
    [SerializeField] private string srlText;

	[Header("SaveConfig")]
	[SerializeField] private string savePth;
	[SerializeField] private string filename = "data.json";


	public void SaveFile()
	{
		srlText = text.text;
		DataStruct dt = new DataStruct
		{
			srlText = srlText,
		};
		string json = JsonUtility.ToJson(dt,true);
		try
		{
			File.WriteAllText(savePth, json);
		}
		catch  (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public void LoadFile()
	{
		if(!File.Exists(savePth)) 
		{
			Debug.LogError(message: "File not exist!");
			return;
		}

		try
		{
			string json = File.ReadAllText(savePth);
			DataStruct dt = JsonUtility.FromJson<DataStruct>(json);
			label.text = dt.srlText;
			btnLoad.enabled = true;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}

	}

	private void Awake()
	{
		savePth = Path.Combine(Application.dataPath, filename);
		if(!File.Exists(savePth))
			btnLoad.enabled = false;
		else btnLoad.enabled = true;
	}

}

struct DataStruct
{
	public string srlText;
}