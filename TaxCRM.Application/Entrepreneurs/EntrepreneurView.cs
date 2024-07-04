using System.ComponentModel.DataAnnotations;

namespace TaxCRM.Application.Entrepreneurs;

public record EntrepreneurView(Guid? Id, [Required]string FirstName, [Required]string LastName);
