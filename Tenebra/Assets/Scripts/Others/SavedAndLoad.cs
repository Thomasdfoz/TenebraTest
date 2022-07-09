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

        CharSave data = new CharSave(playerStats.Level.CurrentLevel, playerStats.Life.CurrentValue, playerStats.Mana.CurrentValue,
            playerStats.MeleeSkill.CurrentLevel, playerStats.DistanceSkill.CurrentLevel, playerStats.MagicSkill.CurrentLevel,
            playerStats.DefenseSkill.CurrentLevel, playerStats.CharSkin);

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);

        CharSave data = (CharSave)bf.Deserialize(file);
        file.Close();

        playerStats.Level.CurrentLevel = data.level;
        playerStats.Life.CurrentValue = data.life;
        playerStats.Mana.CurrentValue = data.mana;
        playerStats.MeleeSkill.CurrentLevel = data.meleeSkill;
        playerStats.DistanceSkill.CurrentLevel = data.distanceSkill;
        playerStats.MagicSkill.CurrentLevel = data.magicSkill;
        playerStats.DefenseSkill.CurrentLevel = data.defenseSkill;
        playerStats.CharSkin = data.charSkin;
    }
}
