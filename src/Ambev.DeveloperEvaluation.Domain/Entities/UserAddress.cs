using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class UserAddress : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string Zipcode { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}