namespace Entity.Dtos.Filter;

public class UserFilterDto
{
    public int[] OperationClaimId { get; set; }

    public bool? Status { get; set; } = null;

    public bool? EmailConfirmed { get; set; } = null;
}
