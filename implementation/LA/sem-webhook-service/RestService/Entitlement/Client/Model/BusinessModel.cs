using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// The model under which the entitlement has been granted, possible business models per product can be exchanged with the Marketplace by the Learning Application provider via the Catalogue API.
  /// </summary>
  [DataContract]
  public class BusinessModel {
    /// <summary>
    /// Unique identifier of the business model recognised by both the marketplace and the learning application.
    /// </summary>
    /// <value>Unique identifier of the business model recognised by both the marketplace and the learning application.</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>
    /// A description of the business model so it can be displayed in the Marketplace or LMS if needed. 
    /// </summary>
    /// <value>A description of the business model so it can be displayed in the Marketplace or LMS if needed. </value>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// A longer description of the business model.
    /// </summary>
    /// <value>A longer description of the business model.</value>
    [DataMember(Name="description", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class BusinessModel {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  Description: ").Append(Description).Append("\n");
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
