using System;

[Serializable]
public class CharSave 
{
    public int level;
    public int life;
    public int mana;
    public int meleeSkill;
    public int distanceSkill;
    public int defenseSkill;
    public int magicSkill;

    public CharSkin charSkin;

    public CharSave(int level, int life, int mana, int meleeSkill, int distanceSkill, int defenseSkill, int magicSkill, CharSkin charSkin)
    {
        this.level = level;
        this.life = life;
        this.mana = mana;
        this.meleeSkill = meleeSkill;
        this.distanceSkill = distanceSkill;
        this.defenseSkill = defenseSkill;
        this.magicSkill = magicSkill;
        this.charSkin = charSkin;
    }
}
