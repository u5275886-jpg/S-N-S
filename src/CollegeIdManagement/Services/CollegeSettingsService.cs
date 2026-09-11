using System.Linq;
using CollegeIdManagement.Data;
using CollegeIdManagement.Models;

namespace CollegeIdManagement.Services
{
    public class CollegeSettingsService
    {
        public CollegeSettings GetSettings()
        {
            using var context = new AppDbContext();
            var settings = context.Settings.FirstOrDefault();
            if (settings == null)
            {
                settings = new CollegeSettings
                {
                    Id = 1,
                    CollegeName = "SHYAM NANDAN SAHAY COLLEGE",
                    ShortName = "SNSEC",
                    AffiliationText = "Permanent Affiliated Unit of B.R. Ambedkar Bihar University",
                    Address = "MUZAFFARPUR, BIHAR",
                    District = "MUZAFFARPUR",
                    State = "BIHAR",
                    PinCode = "842001",
                    Phone = "+91 9128559609",
                    Email = "info@snscollege.ac.in",
                    Website = "www.snscollege.ac.in"
                };
                context.Settings.Add(settings);
                context.SaveChanges();
            }
            return settings;
        }

        public void SaveSettings(CollegeSettings updated)
        {
            using var context = new AppDbContext();
            var existing = context.Settings.FirstOrDefault();
            if (existing == null)
            {
                context.Settings.Add(updated);
            }
            else
            {
                existing.CollegeName = updated.CollegeName;
                existing.ShortName = updated.ShortName;
                existing.AffiliationText = updated.AffiliationText;
                existing.Address = updated.Address;
                existing.District = updated.District;
                existing.State = updated.State;
                existing.PinCode = updated.PinCode;
                existing.Phone = updated.Phone;
                existing.Email = updated.Email;
                existing.Website = updated.Website;
                existing.PrincipalName = updated.PrincipalName;
                existing.DirectorName = updated.DirectorName;
                existing.CollegeLogoPath = updated.CollegeLogoPath;
                existing.CollegeSealPath = updated.CollegeSealPath;
                existing.AuthorizedSignaturePath = updated.AuthorizedSignaturePath;
                existing.IdCardInstructions = updated.IdCardInstructions;
            }
            context.SaveChanges();
            AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "Settings Updated", "CollegeSettings", "Updated college profile settings.");
        }
    }
}
