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
  public class TeacherReference {
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

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
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class TeacherReference {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Eckid: ").Append(Eckid).Append("\n");
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
