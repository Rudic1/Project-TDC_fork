namespace TDC.Models;
public class Profile(string id, string name, Character character, string description, int level)
{
    protected string Id = id;
    protected string Name = name;
    protected Character Character = character;
    protected string Description = description;
    protected int Level = level;

    #region getters & setters
    public string GetId()
    {
        return Id;
    }

    public string GetName()
    {
        return Name;
    }

    public Character GetCharacter()
    {
        return Character;
    }

    public string GetDescription()
    {
        return Description;
    }

    public int GetLevel()
    {
        return Level;
    }
    #endregion
}

