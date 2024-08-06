using Mapster;
using TaxCRM.Application.Notifications;
using TaxCRM.Domain.Entrepreneurs;
using TaxCRM.Domain.Results;
using TaxCRM.Domain.Results.Errors;
using TaxCRM.Utils.Guards;

namespace TaxCRM.Application.Entrepreneurs;

public class EntrepreneurService(IEntrepreneurRepository entrepreneurRepository, IEntrepreneurProfileRepository entrepreneurProfileRepository, NotificationService notificationService)
{
    public async Task<Result<EntrepreneurView>> Create(EntrepreneurView view)
    {
        var entrepreneur = view.Adapt<Entrepreneur>();

        entrepreneur = await entrepreneurRepository.Add(entrepreneur);

        return entrepreneur.Adapt<EntrepreneurView>();
    }

    public async Task<Result<EntrepreneurView>> Get(Guid id)
    {
        var entrepreneur = await entrepreneurRepository.Get(id);

        if (entrepreneur is null)
            return Errors.Entrepreneur.NotFound;

        return entrepreneur.Adapt<EntrepreneurView>();
    }

    public async Task<Result<List<EntrepreneurView>>> GetEntrepreneurs()
    {
        var entrepreneurs = await entrepreneurRepository.GetAll();
        return entrepreneurs.Adapt<List<EntrepreneurView>>();
    }

    public async Task<Result<EntrepreneurProfileView>> AddProfile(EntrepreneurProfileView view, Guid entrepreneurId)
    {
        var newProfile = EntrepreneurProfile.Create(view.Country, view.TaxPayerNumber, entrepreneurId);
        if (!newProfile.Success)
            return newProfile.Error ?? throw new ArgumentNullException("The error shouldn't be null");

        Guard.ArgumentIsNotNull(newProfile.Data, "The success result data shouldn't be null");

        var profileExists = await entrepreneurProfileRepository.AnyByEntrepreneurAndCountry(
            entrepreneurId, 
            newProfile.Data.Country);

        if (profileExists)
            return Errors.EntrepreneurProfile.AlreadyExists;

        var profile = await entrepreneurProfileRepository.Add(newProfile.Data);

        var entrepreneur = await entrepreneurRepository.Get(entrepreneurId);
        if (entrepreneur == null)
            return Errors.Entrepreneur.NotFound;

        await notificationService.SendEntrepreneurProfileCreationNotification(
            entrepreneur.Email, 
            $"{entrepreneur.FirstName} {entrepreneur.LastName}", 
            profile.Country.ToString());

        return profile.Adapt<EntrepreneurProfileView>();
    }

    public async Task<Result<List<EntrepreneurProfileView>>> GetEntrepreneurProfiles(Guid entrepreneurId) 
    {
        var profiles = await entrepreneurProfileRepository.GetByEntrepreneur(entrepreneurId);
        return profiles.Adapt<List<EntrepreneurProfileView>>();
    }

    public async Task<Result<EntrepreneurProfileView>> GetEntrepreneurProfile(Guid id)
    {
        var profile = await entrepreneurProfileRepository.Get(id);

        if (profile is null)
            return Errors.EntrepreneurProfile.NotFound;

        return profile.Adapt<EntrepreneurProfileView>();
    }

    //TODO: Able to delete the profile only if no info accosiated with it
}
