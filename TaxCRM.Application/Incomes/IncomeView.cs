using System.ComponentModel.DataAnnotations;

namespace TaxCRM.Application.Incomes;

public record IncomeView(Guid? Id, [Required]decimal Amount, [Required]string Currency, [Required]DateTime Date);
