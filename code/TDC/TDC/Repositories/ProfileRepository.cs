using TDC.Models;

namespace TDC.Repositories;

public class ProfileRepository
{
    private List<Profile> profiles;

    #region constructors
    public ProfileRepository()
    {
        profiles = new List<Profile>();
    }
    #endregion

    #region getters & setters
    public void AddProfile(Profile profile)
    {
        profiles.Add(profile);
    }

    public void RemoveProfile(Profile profile)
    {
        profiles.Remove(profile);
    }

    public List<Profile> GetProfiles()
    {
        return profiles;
    }
    #endregion

    public List<Profile> GetProfilesByName(string name)
    {
        return [];
    }

    public Profile? GetProfileById(int id)
    {
        return null;
    }
}

