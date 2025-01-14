namespace TDC.Models;
public class Hairstyle
{
    private string color;
    private int sId;

    #region constructors
    public Hairstyle(string color, int sId) 
    { 
        this.color = color;
        this.sId = sId;
    }
    #endregion

    #region getters & setters
    public void SetColor(string c)
    {
        this.color = c;
    }

    public string GetColor()
    {
        return color;
    }

    public void SetStyle(int styleId)
    {
        this.sId = styleId;
    }

    public int GetStyle()
    {
        return sId;
    }
    #endregion
}

