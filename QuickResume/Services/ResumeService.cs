using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuickResume.Models;
using System.Globalization;

namespace QuickResume.Services
{
    public class ResumeService
    {
        public byte[] GenerateResume(Profile profile, Summary summary, List<Education> educationList, List<Language> languages, Social social, List<Experience> experiences, List<Skill> skills)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    
                    // Header: Full Name and Position
                    page.Header()
                        .AlignCenter()
                        .PaddingBottom(10)
                        .Column(col =>
                        {
                            col.Item().Text(profile.FullName).FontSize(40).Bold();
                            col.Item().Text(summary.Position).FontSize(14).Italic();
                        });
                    

                    page.Content().PaddingVertical(10).Row(row =>
                    {
                        row.RelativeItem(1).PaddingRight(20).Column(leftColumn =>
                        {
                            // Profile Section
                            leftColumn.Item().PaddingBottom(5).Text("Profile").FontSize(16).Bold();
                            leftColumn.Item().Text($"Full Name: {profile.FullName}").FontSize(12);
                            leftColumn.Item().Text($"Date of Birth: {profile.DateofBirth.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture)}").FontSize(12);
                            leftColumn.Item().Text($"Gender: {profile.Gender}").FontSize(12);
                            leftColumn.Item().Text($"Email: {profile.Email}").FontSize(12);
                            leftColumn.Item().Text($"Phone: {profile.PhoneNumber}").FontSize(12);
                            leftColumn.Item().Text($"Address: {profile.City}, {profile.Province}, {profile.ZipCode}, {profile.Country}").FontSize(12);
                            leftColumn.Item().PaddingVertical(10).LineHorizontal(1);

                            // Education Section
                            leftColumn.Item().PaddingBottom(5).Text("Education").FontSize(16).Bold();
                            foreach (var edu in educationList)
                            {
                                leftColumn.Item().Text($"{edu.Institution} - {edu.Degree}").FontSize(12);
                                leftColumn.Item().Text($"Field of Study: {edu.FieldOfStudy}").FontSize(12);
                                leftColumn.Item().Text($"Period: {edu.StartDate:yyyy} - {edu.EndDate:yyyy}").FontSize(12);
                                leftColumn.Item().PaddingVertical(10);
                            }
                            leftColumn.Item().PaddingVertical(10).LineHorizontal(1);

                            // Language Section
                            leftColumn.Item().PaddingBottom(5).Text("Languages").FontSize(16).Bold();
                            foreach (var lang in languages)
                            {
                                leftColumn.Item().Text($"{lang.Name} - {lang.Proficiency}").FontSize(12);
                            }
                            leftColumn.Item().PaddingVertical(10).LineHorizontal(1);

                            // Social Section
                            leftColumn.Item().PaddingBottom(5).Text("Social Links").FontSize(16).Bold();
                            if (!string.IsNullOrEmpty(social.GitHub)) leftColumn.Item().Text($"GitHub: {social.GitHub}").FontSize(12);
                            if (!string.IsNullOrEmpty(social.LinkedIn)) leftColumn.Item().PaddingVertical(5).Text($"LinkedIn: {social.LinkedIn}").FontSize(12);
                            if (!string.IsNullOrEmpty(social.PersonalWebsite)) leftColumn.Item().Text($"Website: {social.PersonalWebsite}").FontSize(12);
                        });

                        row.RelativeItem(2).Column(rightColumn =>
                        {
                            // Summary Description
                            rightColumn.Item().PaddingBottom(5).Text("Professional Summary").FontSize(16).Bold();
                            rightColumn.Item().Text(summary.Description).FontSize(12);
                            rightColumn.Item().PaddingVertical(10).LineHorizontal(1);

                            // Experience Section
                            rightColumn.Item().PaddingBottom(5).Text("Experience").FontSize(16).Bold();
                            foreach (var exp in experiences)
                            {
                                rightColumn.Item().Text($"{exp.JobTitle} - {exp.CompanyName}").FontSize(12);
                                rightColumn.Item().Text($"{exp.City}, {exp.State}").FontSize(12);
                                rightColumn.Item().Text($"From {exp.StartDate:yyyy/MM} - {(exp.IsCurrentlyWorking ? "Present" : exp.EndDate?.ToString("yyyy/MM"))}").FontSize(12);
                                rightColumn.Item().Text($"Duties: {exp.JobDuty}").FontSize(12);
                                rightColumn.Item().PaddingVertical(10);
                            }
                            rightColumn.Item().PaddingVertical(10).LineHorizontal(1);

                            // Skills Section
                            rightColumn.Item().PaddingBottom(5).Text("Skills").FontSize(16).Bold();
                            foreach (var skill in skills)
                            {
                                rightColumn.Item().PaddingVertical(2).Text(skill.Name).FontSize(12);
                            }
                        });
                    });
                });
            }).GeneratePdf();
        }
    }
}
