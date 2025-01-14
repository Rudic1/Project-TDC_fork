namespace TDC.Models;

public class Character
{
    private int xp;
    private int points;
    private readonly List<Perk> perks;
    private readonly List<int> outfits;
    private int currentOutfit;
    private string color;
    private int face;
    private Hairstyle hairstyle;

    #region constructors
    public Character() // default character
    {
        perks = new List<Perk>();
        outfits = new List<int>();
        color = "#000000";
        hairstyle = new Hairstyle(color, 0);
    }

    public Character(int xp, int points, List<Perk> perks, List<int> outfits, int currentOutfit, string color, int face, Hairstyle hairstyle)
    {
        this.xp = xp;
        this.points = points;
        this.perks = perks;
        this.outfits = outfits;
        this.currentOutfit = currentOutfit;
        this.color = color;
        this.face = face;
        this.hairstyle = hairstyle;
    }
    #endregion

    #region getters & setters
    public void AddXp(int xp)
    {
        this.xp += xp;
    }

    public int GetXp()
    {
        return xp;
    }

    public void AddPoints(int pts)
    {
        this.points += pts;
    }

    public int GetPoints()
    {
        return points;
    }

    public void AddPerk(Perk perk)
    {
        perks.Add(perk);
    }

    public List<Perk> GetPerks()
    {
        return perks;
    }

    public void AddOutfit(int outfitId)
    {
        
    }

    public List<int> GetOutfits()
    {
        return outfits;
    }

    public void SetCurrentOutfit(int outfitId)
    {
        currentOutfit = outfitId;
    }

    public int GetCurrentOutfit()
    {
        return currentOutfit;
    }

    public void SetColor(string colorCode)
    {
        color = colorCode;
    }

    public string GetColor()
    {
        return color;
    }

    public void SetFace(int faceId)
    {
        face = faceId;
    }

    public int GetFace()
    {
        return face;
    }

    public void SetHairstyle(Hairstyle hair)
    {
        hairstyle = hair;
    }

    public Hairstyle GetHairstyle()
    {
        return hairstyle;
    }
    #endregion
}
