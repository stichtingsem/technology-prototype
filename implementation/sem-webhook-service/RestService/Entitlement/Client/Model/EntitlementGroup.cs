using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// A sub-group within a school that the entitlement is applicable to - typically a subject and level.
  /// </summary>
  [DataContract]
  public class EntitlementGroup
    {
    /// <summary>
    /// Government standard subject code.
    /// </summary>
    /// <value>Government standard subject code.</value>
    [DataMember(Name="subjectCode", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "subjectCode")]
    public string SubjectCode { get; set; }

    /// <summary>
    /// Government standard level - e.g. havo-1
    /// </summary>
    /// <value>Government standard level - e.g. havo-1</value>
    [DataMember(Name="level", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "level")]
    public string Level { get; set; }

    /// <summary>
    /// Quantity selected or purchased.
    /// </summary>
    /// <value>Quantity selected or purchased.</value>
    [DataMember(Name="quantity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "quantity")]
    public int? Quantity { get; set; }

    /// <summary>
    /// School description of this subject, may differ from standard.
    /// </summary>
    /// <value>School description of this subject, may differ from standard.</value>
    [DataMember(Name="schoolSubjectName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "schoolSubjectName")]
    public string SchoolSubjectName { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Group {\n");
      sb.Append("  SubjectCode: ").Append(SubjectCode).Append("\n");
      sb.Append("  Level: ").Append(Level).Append("\n");
      sb.Append("  Quantity: ").Append(Quantity).Append("\n");
      sb.Append("  SchoolSubjectName: ").Append(SchoolSubjectName).Append("\n");
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
