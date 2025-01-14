using TDC.Models;

namespace TDC.Repositories;

public class ProfileRepository
{
    private readonly List<Profile> profiles;

    #region constructors
    public ProfileRepository()
    {
        profiles = new List<Profile>();
        var accRepos = new AccountRepository();
        foreach (var acc in accRepos.GetAllAccounts())
        {
            profiles.Add(new Profile(acc.GetId(), acc.GetName(), new Character(), acc.GetDescription(), acc.GetLevel())); //TO-DO: Add database for characters
        }
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

    public Profile? GetProfileById(string id)
    {
        return profiles.FirstOrDefault(profile => id.Equals(profile.GetId()));
    }
}

