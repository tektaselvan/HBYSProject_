using HBYS.Core.Domain.Enums;
using HBYS.Core.Domain.ValueObjects;

namespace HBYS.Core.Domain.Entities;

public class Patient
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string TcNo { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public DateTime BirthDate { get; private set; }
    public int Age => DateTime.Today.Year - BirthDate.Year;
    public Gender Gender { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? Address { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public ICollection<Visit> Visits { get; private set; } = new List<Visit>();

    private Patient() { }

    public static Patient Create(string tcNo, string firstName, string lastName,
        DateTime birthDate, Gender gender, string? phone = null, string? email = null)
    {
        if (string.IsNullOrWhiteSpace(tcNo) || tcNo.Length != 11)
            throw new ArgumentException("Geçersiz TC Kimlik Numarası.");

        return new Patient
        {
            TcNo = tcNo,
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            BirthDate = birthDate,
            Gender = gender,
            Phone = phone,
            Email = email
        };
    }

    public void Update(string firstName, string lastName, string? phone, string? email, string? address)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Phone = phone;
        Email = email;
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }
}
