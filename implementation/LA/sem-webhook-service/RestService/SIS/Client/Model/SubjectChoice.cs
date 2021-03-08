using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class SubjectChoice {
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>
    /// Start of this subject for the student. Date cannot be in the future.
    /// </summary>
    /// <value>Start of this subject for the student. Date cannot be in the future.</value>
    [DataMember(Name="startDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "startDate")]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Start of this subject for the student. Date cannot be in the future. null if not ended
    /// </summary>
    /// <value>Start of this subject for the student. Date cannot be in the future. null if not ended</value>
    [DataMember(Name="endDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "endDate")]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Officiële elementcode, bron: Ministerie van OCW, like https://zoek.officielebekendmakingen.nl/stcrt-2019-35044
    /// </summary>
    /// <value>Officiële elementcode, bron: Ministerie van OCW, like https://zoek.officielebekendmakingen.nl/stcrt-2019-35044</value>
    [DataMember(Name="subjectCode", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "subjectCode")]
    public string SubjectCode { get; set; }

    /// <summary>
    /// Optional name of subject if different within specific school.
    /// </summary>
    /// <value>Optional name of subject if different within specific school.</value>
    [DataMember(Name="schoolSubjectName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "schoolSubjectName")]
    public string SchoolSubjectName { get; set; }

    /// <summary>
    /// Official Level, followed by courseyear. example 'havo-1, vwo-2, etc.'
    /// </summary>
    /// <value>Official Level, followed by courseyear. example 'havo-1, vwo-2, etc.'</value>
    [DataMember(Name="level", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "level")]
    public string Level { get; set; }

    /// <summary>
    /// School name of the Level/couse/study. example 'Technasium, etc.' This gives schools a way to name the course how they want it, wile we still have a link with the official level
    /// </summary>
    /// <value>School name of the Level/couse/study. example 'Technasium, etc.' This gives schools a way to name the course how they want it, wile we still have a link with the official level</value>
    [DataMember(Name="course", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "course")]
    public string Course { get; set; }

    /// <summary>
    /// Learning year
    /// </summary>
    /// <value>Learning year</value>
    [DataMember(Name="courseYear", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "courseYear")]
    public int? CourseYear { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class SubjectChoice {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  StartDate: ").Append(StartDate).Append("\n");
      sb.Append("  EndDate: ").Append(EndDate).Append("\n");
      sb.Append("  SubjectCode: ").Append(SubjectCode).Append("\n");
      sb.Append("  SchoolSubjectName: ").Append(SchoolSubjectName).Append("\n");
      sb.Append("  Level: ").Append(Level).Append("\n");
      sb.Append("  Course: ").Append(Course).Append("\n");
      sb.Append("  CourseYear: ").Append(CourseYear).Append("\n");
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
