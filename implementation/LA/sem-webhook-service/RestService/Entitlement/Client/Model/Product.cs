using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// A product that can be accessed after being purchased or selected.
  /// </summary>
  [DataContract]
  public class Product {
    /// <summary>
    /// Gets or Sets Ean
    /// </summary>
    [DataMember(Name="ean", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ean")]
    public string Ean { get; set; }

    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// This is the default URL to access the product - there may be more detailed structure and access points exchanged later between LA and LMS. 
    /// </summary>
    /// <value>This is the default URL to access the product - there may be more detailed structure and access points exchanged later between LA and LMS. </value>
    [DataMember(Name="defaultAccessUrl", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "defaultAccessUrl")]
    public string DefaultAccessUrl { get; set; }

    /// <summary>
    /// Short name for a specific vendor, established (along with the vendor secret) at the point of setup.
    /// </summary>
    /// <value>Short name for a specific vendor, established (along with the vendor secret) at the point of setup.</value>
    [DataMember(Name="provider", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "provider")]
    public string Provider { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Product {\n");
      sb.Append("  Ean: ").Append(Ean).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  DefaultAccessUrl: ").Append(DefaultAccessUrl).Append("\n");
      sb.Append("  Provider: ").Append(Provider).Append("\n");
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
