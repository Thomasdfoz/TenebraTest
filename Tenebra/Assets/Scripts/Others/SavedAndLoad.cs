using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SavedAndLoad : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.dat");

        SaveData data = new SaveData();

        data.Level = playerStats.Level.CurrentLevel;
        data.life = playerStats.Life.CurrentValue;
        data.mana = playerStats.Mana.CurrentValue;
        data.meleeSkill = playerStats.MeleeSkill.CurrentLevel;
        data.distanceSkill = playerStats.DistanceSkill.CurrentLevel;
        data.magicSkill = playerStats.MagicSkill.CurrentLevel;
        data.defenseSkill = playerStats.DefenseSkill.CurrentLevel;

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);

        SaveData data = (SaveData)bf.Deserialize(file);
        file.Close();

        playerStats.Level.CurrentLevel = data.Level;
        playerStats.Life.CurrentValue = data.life;
        playerStats.Mana.CurrentValue = data.mana;
        playerStats.MeleeSkill.CurrentLevel = data.meleeSkill;
        playerStats.DistanceSkill.CurrentLevel = data.distanceSkill;
        playerStats.MagicSkill.CurrentLevel = data.magicSkill;
        playerStats.DefenseSkill.CurrentLevel = data.defenseSkill;
}
}
