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

        return Result<EntrepreneurView>.FromSuccess(entrepreneur.Adapt<EntrepreneurView>());
    }

    public async Task<Result<EntrepreneurView>> Get(Guid id)
    {
        var entrepreneur = await entrepreneurRepository.Get(id);

        if (entrepreneur is null)
            return Result<EntrepreneurView>.FromFailure(Errors.Entrepreneur.NotFound);

        return Result<EntrepreneurView>.FromSuccess(entrepreneur.Adapt<EntrepreneurView>());
    }

    public async Task<Result<ICollection<EntrepreneurView>>> GetEntrepreneurs()
    {
        var entrepreneurs = await entrepreneurRepository.GetAll();
        return Result<ICollection<EntrepreneurView>>.FromSuccess(entrepreneurs.Adapt<ICollection<EntrepreneurView>>());
    }

    public async Task<Result<EntrepreneurProfileView>> AddProfile(EntrepreneurProfileView view, Guid entrepreneurId)
    {
        var entrepreneur = await entrepreneurRepository.Get(entrepreneurId);
        if (entrepreneur == null)
            return Result<EntrepreneurProfileView>.FromFailure(Errors.Entrepreneur.NotFound);

        var newProfile = EntrepreneurProfile.Create(view.Country, view.TaxPayerNumber, entrepreneurId);
        if (!newProfile.Success)
            return Result<EntrepreneurProfileView>.FromFailure(newProfile.Error);

        //Refactor Result to avoid such checks everywhere?
        //Guard.ArgumentIsNotNull(newProfile.Data, "The success result data shouldn't be null");

        var profileExists = await entrepreneurProfileRepository.AnyByEntrepreneurAndCountry(entrepreneurId, newProfile.Data.Country);
        if (profileExists)
            return Result<EntrepreneurProfileView>.FromFailure(Errors.EntrepreneurProfile.AlreadyExists);

        var profile = await entrepreneurProfileRepository.Add(newProfile.Data);

        await notificationService.SendEntrepreneurProfileCreationNotification(entrepreneur.Email, $"{entrepreneur.FirstName} {entrepreneur.LastName}", profile.Country.ToString());

        return Result<EntrepreneurProfileView>.FromSuccess(profile.Adapt<EntrepreneurProfileView>());
    }

    public async Task<Result<ICollection<EntrepreneurProfileView>>> GetEntrepreneurProfiles(Guid entrepreneurId) 
    {
        var profiles = await entrepreneurProfileRepository.GetByEntrepreneur(entrepreneurId);
        return Result<ICollection<EntrepreneurProfileView>>.FromSuccess(profiles.Adapt<ICollection<EntrepreneurProfileView>>());
    }

    public async Task<Result<EntrepreneurProfileView>> GetEntrepreneurProfile(Guid id)
    {
        var profile = await entrepreneurProfileRepository.Get(id);

        if (profile is null)
            return Result<EntrepreneurProfileView>.FromFailure(Errors.EntrepreneurProfile.NotFound);

        return Result<EntrepreneurProfileView>.FromSuccess(profile.Adapt<EntrepreneurProfileView>());
    }
}
