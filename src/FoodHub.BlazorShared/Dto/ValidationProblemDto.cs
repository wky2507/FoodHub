namespace FoodHub.BlazorShared.Dto;

public class ValidationProblemDto
{

    public Dictionary<string, string[]> Errors { get; set; } = new();

}