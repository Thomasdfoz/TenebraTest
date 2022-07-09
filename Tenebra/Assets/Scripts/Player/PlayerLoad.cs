using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerLoad : MonoBehaviour
{
    public SetCharSkin setCharSkin;
    CharSave data;
    public int slot;

    private void Update()
    {
        if (!setCharSkin)
        {
            setCharSkin = FindObjectOfType<SetCharSkin>();
        }
    }
    public void LoadChar(string slot)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Char" + slot + ".dat", FileMode.Open);

        data = (CharSave)bf.Deserialize(file);

        file.Close();
        setCharSkin.SetCharacter(data.charSkin);
    }

    public string NameChar()
    {
        return data.charSkin.name;
    }





}
