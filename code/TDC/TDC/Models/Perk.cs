namespace TDC.Models;
public class Perk(int perkId, string description)
{
    #region getters & setters
    public int GetPerkId() { 
        return perkId;
    }

    public string GetDescription() { 
        return description;
    }
    #endregion
}

