using System;

[Serializable]
public class CharSkin
{
    public string name;
    public int idColorBody;
    public int idColorEyes;
    public int idColorBrows;
    public int idColorHair;
    public int idTypeHair;
    public int idColorBelt;
    public int idColorBoots;
    public int idTypeShirt;
    public int idColorShirt;
    public int idColorPants;

    public CharSkin(string name, int idColorBody, int idColorEyes, int idColorBrows, int idColorHair, int idTypeHair, int idColorBelt, int idColorBoots, int idTypeShirt, int idColorShirt, int idColorPants)
    {
        this.name = name;
        this.idColorBody = idColorBody;
        this.idColorEyes = idColorEyes;
        this.idColorBrows = idColorBrows;
        this.idColorHair = idColorHair;
        this.idTypeHair = idTypeHair;
        this.idColorBelt = idColorBelt;
        this.idColorBoots = idColorBoots;
        this.idTypeShirt = idTypeShirt;
        this.idColorShirt = idColorShirt;
        this.idColorPants = idColorPants;
    }
}
