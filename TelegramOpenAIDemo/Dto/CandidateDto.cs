namespace TeamMagnetix.Dal.Dto;

public sealed class CandidateDto : SkillEntityDto
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string SalaryExpectation { get; set; } = "";
    public string[] LanguageLevels { get; set; } = Array.Empty<string>();
    public string[] Employments { get; set; } = Array.Empty<string>();
    public string[] Certifications { get; set; } = Array.Empty<string>();
    public string GitHub { get; set; } = "";
    public string TotalYearsOfExperience { get; set; }
    public string Title { get; set; } = "";
    public string Mail { get; set; } = "";
    public string LinkedIn { get; set; } = "";
}