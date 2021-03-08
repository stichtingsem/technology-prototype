using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// Access to non-sensitive student information for us in an LMS or LA.
  /// </summary>
  [DataContract]
  public class Student {
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
    /// Gets or Sets Firstname
    /// </summary>
    [DataMember(Name="firstname", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "firstname")]
    public string Firstname { get; set; }

    /// <summary>
    /// Gets or Sets Surname
    /// </summary>
    [DataMember(Name="surname", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "surname")]
    public string Surname { get; set; }

    /// <summary>
    /// Gets or Sets SurnamePrefix
    /// </summary>
    [DataMember(Name="surnamePrefix", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "surnamePrefix")]
    public string SurnamePrefix { get; set; }

    /// <summary>
    /// A reference to the school year for this student.
    /// </summary>
    /// <value>A reference to the school year for this student.</value>
    [DataMember(Name="schoolYear", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "schoolYear")]
    public string SchoolYear { get; set; }

    /// <summary>
    /// Gets or Sets School
    /// </summary>
    [DataMember(Name="school", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "school")]
    public SisSchool School { get; set; }

    /// <summary>
    /// Gets or Sets Groups
    /// </summary>
    [DataMember(Name="groups", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "groups")]
    public List<GroupReference> Groups { get; set; }

    /// <summary>
    /// Gets or Sets SubjectChoices
    /// </summary>
    [DataMember(Name="subjectChoices", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "subjectChoices")]
    public List<SubjectChoice> SubjectChoices { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Student {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  SchemaVersion: ").Append(SchemaVersion).Append("\n");
      sb.Append("  Eckid: ").Append(Eckid).Append("\n");
      sb.Append("  Firstname: ").Append(Firstname).Append("\n");
      sb.Append("  Surname: ").Append(Surname).Append("\n");
      sb.Append("  SurnamePrefix: ").Append(SurnamePrefix).Append("\n");
      sb.Append("  SchoolYear: ").Append(SchoolYear).Append("\n");
      sb.Append("  School: ").Append(School).Append("\n");
      sb.Append("  Groups: ").Append(Groups).Append("\n");
      sb.Append("  SubjectChoices: ").Append(SubjectChoices).Append("\n");
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
