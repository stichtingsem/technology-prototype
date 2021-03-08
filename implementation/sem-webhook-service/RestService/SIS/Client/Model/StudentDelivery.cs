using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// Access to sensitive delivery information for a student.
  /// </summary>
  [DataContract]
  public class StudentDelivery {
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>
    /// schema version of this event/object. Start with 0, inclement when we have changes, so we know how to deserialise.
    /// </summary>
    /// <value>schema version of this event/object. Start with 0, inclement when we have changes, so we know how to deserialise.</value>
    [DataMember(Name="schemaVersion", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "schemaVersion")]
    public int? SchemaVersion { get; set; }

    /// <summary>
    /// Gets or Sets Eckid
    /// </summary>
    [DataMember(Name="eckid", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "eckid")]
    public string Eckid { get; set; }

    /// <summary>
    /// Gets or Sets DateOfBirth
    /// </summary>
    [DataMember(Name="dateOfBirth", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "dateOfBirth")]
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Gets or Sets EmailAddress
    /// </summary>
    [DataMember(Name="emailAddress", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "emailAddress")]
    public string EmailAddress { get; set; }

    /// <summary>
    /// Gets or Sets Postcode
    /// </summary>
    [DataMember(Name="postcode", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "postcode")]
    public string Postcode { get; set; }

    /// <summary>
    /// Gets or Sets HouseNumber
    /// </summary>
    [DataMember(Name="houseNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "houseNumber")]
    public string HouseNumber { get; set; }

    /// <summary>
    /// Used for delivery 
    /// </summary>
    /// <value>Used for delivery </value>
    [DataMember(Name="firstname", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "firstname")]
    public string Firstname { get; set; }

    /// <summary>
    /// Used for delivery 
    /// </summary>
    /// <value>Used for delivery </value>
    [DataMember(Name="surname", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "surname")]
    public string Surname { get; set; }

    /// <summary>
    /// Used for delivery
    /// </summary>
    /// <value>Used for delivery</value>
    [DataMember(Name="surnamePrefix", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "surnamePrefix")]
    public string SurnamePrefix { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class StudentDelivery {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  SchemaVersion: ").Append(SchemaVersion).Append("\n");
      sb.Append("  Eckid: ").Append(Eckid).Append("\n");
      sb.Append("  DateOfBirth: ").Append(DateOfBirth).Append("\n");
      sb.Append("  EmailAddress: ").Append(EmailAddress).Append("\n");
      sb.Append("  Postcode: ").Append(Postcode).Append("\n");
      sb.Append("  HouseNumber: ").Append(HouseNumber).Append("\n");
      sb.Append("  Firstname: ").Append(Firstname).Append("\n");
      sb.Append("  Surname: ").Append(Surname).Append("\n");
      sb.Append("  SurnamePrefix: ").Append(SurnamePrefix).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
