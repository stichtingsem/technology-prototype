using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// A group or other grouping of students.
  /// </summary>
  [DataContract]
  public class SisGroup {
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
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets School
    /// </summary>
    [DataMember(Name="school", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "school")]
    public SisSchool School { get; set; }

    /// <summary>
    /// Gets or Sets Students
    /// </summary>
    [DataMember(Name="students", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "students")]
    public List<StudentReference> Students { get; set; }

    /// <summary>
    /// Gets or Sets Teachers
    /// </summary>
    [DataMember(Name="teachers", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "teachers")]
    public List<TeacherReference> Teachers { get; set; }

    /// <summary>
    /// Gets or Sets SubjectChoices
    /// </summary>
    [DataMember(Name="subjectChoices", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "subjectChoices")]
    public List<SubjectChoice> SubjectChoices { get; set; }

    /// <summary>
    /// A reference to the school year for this group.
    /// </summary>
    /// <value>A reference to the school year for this group.</value>
    [DataMember(Name="schoolYear", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "schoolYear")]
    public string SchoolYear { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Group {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  SchemaVersion: ").Append(SchemaVersion).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  School: ").Append(School).Append("\n");
      sb.Append("  Students: ").Append(Students).Append("\n");
      sb.Append("  Teachers: ").Append(Teachers).Append("\n");
      sb.Append("  SubjectChoices: ").Append(SubjectChoices).Append("\n");
      sb.Append("  SchoolYear: ").Append(SchoolYear).Append("\n");
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
