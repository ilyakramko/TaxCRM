using System.ComponentModel.DataAnnotations;

namespace TaxCRM.Application.Entrepreneurs;

public record EntrepreneurProfileView(Guid? Id, [Required]string TaxPayerNumber, [Required]string Country);
