using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharSkin : MonoBehaviour
{
    [Header("SkinnedMeshRenderer")]
    [SerializeField] SkinnedMeshRenderer body;
    [SerializeField] SkinnedMeshRenderer brows;
    [SerializeField] SkinnedMeshRenderer eyes;
    [SerializeField] SkinnedMeshRenderer[] hairs;
    [SerializeField] SkinnedMeshRenderer belt;
    [SerializeField] SkinnedMeshRenderer boots;
    [SerializeField] SkinnedMeshRenderer jacket;
    [SerializeField] SkinnedMeshRenderer pants;
    [SerializeField] SkinnedMeshRenderer shirt;
    [SerializeField] SkinnedMeshRenderer shirtNoSleeves;

    [Header("Material")]
    [SerializeField] Material black;
    [SerializeField] Material[] eyesColors;
    [SerializeField] Material[] browsColors;
    [SerializeField] Material[] hairsColors1;
    [SerializeField] Material[] hairsColors2;
    [SerializeField] Material[] ArmorsColors;
    [SerializeField] Material[] SkinsColors;

    private void DisableHairs()
    {
        for (int i = 0; i < hairs.Length; i++)
        {
            hairs[i].enabled = false;
        }
    }
    public void SetHair(int id, int idColor)
    {
        DisableHairs();
        hairs[id].enabled = true;
        ColorHair(id, idColor);
    }
    public void ColorHair(int idhair, int id)
    {
        if (idhair <= 3)
        {
            hairs[idhair].material = hairsColors2[id];
        }
        else
        {
            hairs[idhair].material = hairsColors1[id];
        }

    }
    public void ColorBody(int id)
    {
        body.material = SkinsColors[id];
    }
    public void ColorEyes(int id)
    {
        eyes.material = eyesColors[id];
    }
    public void ColorBrows(int id)
    {
        brows.material = browsColors[id];
    }
    public void ColorBelt(int id)
    {
        belt.material = ArmorsColors[id];
    }
    public void ColorBoots(int id)
    {
        boots.material = ArmorsColors[id];
    }
    public void ColorPants(int id)
    {
        pants.material = ArmorsColors[id];
    }
    public void SetShirt(int id, int idColor)
    {
        if (id == 0)
        {
            ColorShirt(0);
            shirtNoSleeves.enabled = false;
            jacket.enabled = false;
            shirt.enabled = true;

        }
        else if (id == 1)
        {
            ColorShirtNoSleeves(0);
            shirt.enabled = false;
            jacket.enabled = false;
            shirtNoSleeves.enabled = true;
        }
        else
        {
            ColorJacket(0);
            shirt.enabled = false;
            shirtNoSleeves.enabled = false;
            jacket.enabled = true;
        }
        ColorShirt(idColor);
        ColorJacket(idColor);
        ColorShirtNoSleeves(idColor);

    }
    public void ColorShirt(int id)
    {
        shirt.material = ArmorsColors[id];
    }
    public void ColorShirtNoSleeves(int id)
    {
        shirtNoSleeves.material = ArmorsColors[id];
    }
    public void ColorJacket(int id)
    {
        jacket.material = ArmorsColors[id];
    }
    public void BlackColor(SkinnedMeshRenderer skinned)
    {
        skinned.material = black;
    }
    public void AllBlackColor()
    {
        BlackColor(body);
        BlackColor(brows);
        BlackColor(eyes);
        SetHair(0, 0);
        BlackColor(hairs[0]);
        BlackColor(belt);
        BlackColor(boots);
        BlackColor(jacket);
        BlackColor(pants);
        BlackColor(shirt);
        BlackColor(shirtNoSleeves);
    }
    public void SetCharacter(int idColorBody, int idColorEyes, int idColorBrows, int idTypeHair, int idColorHair, int idColorBelt, int idColorBoots, int idTypeShirt, int idColorShirt, int idColorPants)
    {
        ColorBody(idColorBody);
        ColorEyes(idColorEyes);
        ColorBrows(idColorBrows);
        SetHair(idTypeHair, idColorHair);
        ColorBelt(idColorBelt);
        ColorBoots(idColorBoots);
        SetShirt(idTypeShirt, idColorShirt);
        ColorPants(idColorPants);
    }
    public void SetCharacter(CharSkin charSkin)
    {
        ColorBody(charSkin.idColorBody);
        ColorEyes(charSkin.idColorEyes);
        ColorBrows(charSkin.idColorBrows);
        SetHair(charSkin.idTypeHair, charSkin.idColorHair);
        ColorBelt(charSkin.idColorBelt);
        ColorBoots(charSkin.idColorBoots);
        SetShirt(charSkin.idTypeShirt, charSkin.idColorShirt);
        ColorPants(charSkin.idColorPants);
        if (charSkin.name == null)
        {
            AllBlackColor();
        }
    }

}
