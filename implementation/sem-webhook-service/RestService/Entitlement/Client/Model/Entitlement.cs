using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// The core object that expresses information about a product that a school or indivdiual has selected for use - typically via a LML, but not always.
  /// </summary>
  [DataContract]
  public class Entitlement {
    /// <summary>
    /// Gets or Sets Start
    /// </summary>
    [DataMember(Name="start", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "start")]
    public DateTime? Start { get; set; }

    /// <summary>
    /// Gets or Sets End
    /// </summary>
    [DataMember(Name="end", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "end")]
    public DateTime? End { get; set; }

    /// <summary>
    /// Gets or Sets Product
    /// </summary>
    [DataMember(Name="product", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "product")]
    public Product Product { get; set; }

    /// <summary>
    /// Gets or Sets Entitlee
    /// </summary>
    [DataMember(Name="entitlee", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "entitlee")]
    public OneOfEntitlementEntitlee Entitlee { get; set; }

    /// <summary>
    /// Gets or Sets Model
    /// </summary>
    [DataMember(Name="model", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "model")]
    public BusinessModel Model { get; set; }

    /// <summary>
    /// Gets or Sets Quantity
    /// </summary>
    [DataMember(Name="quantity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "quantity")]
    public int? Quantity { get; set; }

    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>
    /// The status of the entitlement 
    /// </summary>
    /// <value>The status of the entitlement </value>
    [DataMember(Name="status", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    /// <summary>
    /// The marketplace that provided the entitlement.
    /// </summary>
    /// <value>The marketplace that provided the entitlement.</value>
    [DataMember(Name="marketplace", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "marketplace")]
    public string Marketplace { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Entitlement {\n");
      sb.Append("  Start: ").Append(Start).Append("\n");
      sb.Append("  End: ").Append(End).Append("\n");
      sb.Append("  Product: ").Append(Product).Append("\n");
      sb.Append("  Entitlee: ").Append(Entitlee).Append("\n");
      sb.Append("  Model: ").Append(Model).Append("\n");
      sb.Append("  Quantity: ").Append(Quantity).Append("\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Status: ").Append(Status).Append("\n");
      sb.Append("  Marketplace: ").Append(Marketplace).Append("\n");
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
