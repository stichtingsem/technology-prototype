using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// Information about an individual who has an entitlement to use a specific product that they have purchased or selected.
  /// </summary>
  [DataContract]
  public class Individual {
    /// <summary>
    /// A unique identifier for the purchaser. 
    /// </summary>
    /// <value>A unique identifier for the purchaser. </value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>
    /// The name of the person purchasing.
    /// </summary>
    /// <value>The name of the person purchasing.</value>
    [DataMember(Name="display_name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "display_name")]
    public string DisplayName { get; set; }

    /// <summary>
    /// The email address of the person purchasing, used to notify the individual of fulfilment and provide access.
    /// </summary>
    /// <value>The email address of the person purchasing, used to notify the individual of fulfilment and provide access.</value>
    [DataMember(Name="email", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }

    /// <summary>
    /// If needed, the ECK-ID of the individual.
    /// </summary>
    /// <value>If needed, the ECK-ID of the individual.</value>
    [DataMember(Name="eckid", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "eckid")]
    public string Eckid { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Individual {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
      sb.Append("  Email: ").Append(Email).Append("\n");
      sb.Append("  Eckid: ").Append(Eckid).Append("\n");
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
