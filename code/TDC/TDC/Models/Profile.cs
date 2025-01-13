namespace TDC.Models;
public class Profile
{
    protected string Id;
    protected string Name;
    protected Character Character;
    protected string Description;
    protected int Level;

    #region constructors
    public Profile(string id, string name, Character character, string description, int level)
    {
        this.Id = id;
        this.Name = name;
        this.Character = character;
        this.Description = description;
        this.Level = level;
    }

    #endregion

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

