using System.Text.Json.Serialization;

namespace IP.Core.Domain.Customers;

[JsonConverter(typeof(JsonStringEnumConverter<PersonTypeEnum>))]
public enum PersonTypeEnum { 
    [Description("PF")] Individual = 7,
    [Description("PJ")] Company = 8
}
