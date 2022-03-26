using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region ------------------------Stats--------------------------
    private AttributesClass life = new AttributesClass(100);
    private AttributesClass mana = new AttributesClass(10);
    private LevelClass level = new LevelClass();
    private int damage;
    private int defense;
    private int resistence;
    private byte moveSpeed;
    private byte attackSpeed;
    private byte range;
    #endregion
     
    #region ------------------------Skills--------------------------
    private SkillClass meleeSkill = new SkillClass();
    private SkillClass distanceSkill = new SkillClass();
    private SkillClass magicSkill = new SkillClass();
    private SkillClass defenseSkill = new SkillClass();
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
