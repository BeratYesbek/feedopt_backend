namespace Entity.Dtos.Filter;

public class UserFilterDto
{
    public int[] OperationClaimId { get; set; }
    
    public bool Status { get; set; }    
    
    public bool EmailConfirmed { get; set; }
}