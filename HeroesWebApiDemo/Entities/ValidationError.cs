namespace HeroesWebApiDemo.Entities;

public class ValidationError
{
    public string FieldName { get; set; }
        
    public List<string> ErrorMessages { get; set; }
}