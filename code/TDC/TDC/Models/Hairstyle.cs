namespace TDC.Models;
public class Hairstyle(string color, int sId)
{
    #region getters & setters
    public void SetColor(string c)
    {
        color = c;
    }

    public string GetColor()
    {
        return color;
    }

    public void SetStyle(int styleId)
    {
        sId = styleId;
    }

    public int GetStyle()
    {
        return sId;
    }
    #endregion
}

