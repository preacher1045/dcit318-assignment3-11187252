namespace CoreModels
{
    public record Transaction(
        int Id, 
        DateTime Date, 
        decimal Amount, 
        string Category 
        );
}
