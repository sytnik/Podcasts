using System.Text.Json;
using TeamMagnetix.Dal.Dto;

namespace TelegramOpenAIDemo.Logic;

public static class AppSettings
{
    public const string ParseCandidateCvKeyword = "parsecv";
    public const string DarkMode = "isDarkMode";
    public static bool Development;

    public static string BasePath;
    public static string OpenAiKey;
    public static string OpenAiModel;

    internal static readonly string
        CandidateDtoJsonSchema = JsonSerializer.Serialize(new CandidateDto
        {
            LanguageLevels = new[] { "language_1: level_1", "language_2: level_2" },
            Employments = new[] { "company_1: position_1, 5 years", "company_2: position_2, 2 years" },
            Certifications = new[] { "certification_1 - 05.2023", "certification_2 - 05.2024" }
        });
        // VacancyDtoJsonSchema = JsonSerializer.Serialize(new VacancyDto()),
        // CandidateVacancyDtoJsonSchema = JsonSerializer.Serialize(new CandidateVacancyDto());

    public const int MaxMessageSize = 102400000;
}