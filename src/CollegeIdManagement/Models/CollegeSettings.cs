using System;

namespace CollegeIdManagement.Models
{
    public class CollegeSettings
    {
        public int Id { get; set; } = 1;
        public string CollegeName { get; set; } = "SHYAM NANDAN SAHAY COLLEGE";
        public string ShortName { get; set; } = "SNSEC";
        public string AffiliationText { get; set; } = "Permanent Affiliated Unit of B.R. Ambedkar Bihar University";
        public string Address { get; set; } = "MUZAFFARPUR, BIHAR";
        public string District { get; set; } = "MUZAFFARPUR";
        public string State { get; set; } = "BIHAR";
        public string PinCode { get; set; } = "842001";
        public string Phone { get; set; } = "+91 9128559609";
        public string Email { get; set; } = "info@snscollege.ac.in";
        public string Website { get; set; } = "www.snscollege.ac.in";
        public string PrincipalName { get; set; } = "Dr. Principal";
        public string DirectorName { get; set; } = "Director";
        public string CollegeLogoPath { get; set; } = string.Empty;
        public string CollegeSealPath { get; set; } = string.Empty;
        public string AuthorizedSignaturePath { get; set; } = string.Empty;
        public string IdCardInstructions { get; set; } = "1. This ID card is non-transferable.\n2. Cardholder must display card at all times on campus.\n3. If found, please return to College Administrative Office.";
    }
}
