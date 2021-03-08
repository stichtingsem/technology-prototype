using System;
using System.Collections.Generic;
using RestSharp;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IEntitlementsApi
    {
        /// <summary>
        /// Get a specific entitlement Retrieve the full specification for an entitlement from a marketplace, needs to confirm that the caller is able to access either based on their provider information (they can only see their own products) in the case of the provider scope.
        /// </summary>
        /// <param name="id">Specific identifier of an entitlement.</param>
        /// <returns>Entitlement</returns>
        Entitlement GetEntitlementsById (string id);
        /// <summary>
        /// Get all entitlements for a specific vendor (Learning Application Provider) Get all entitlements related to products from a specific provider of a learning application (or other product used by a school).  It is assumed that at the point of setup and provision of an API key, that the provider name is also stored against the API key by the marketplace, so that all calls with this key will be filtered to that specific provider in the response.  This API has pagination, using &#x60;start&#x60; and &#x60;limit&#x60;, with an optional &#x60;status&#x60; filter.
        /// </summary>
        /// <param name="status">Filter by status </param>
        /// <param name="start">Start point for pagination of results, defaults to 0,</param>
        /// <param name="limit">Limit of number of results returned by page, defaults to 20 with max 100.</param>
        /// <returns>Entitlements</returns>
        Entitlements GetEntitlementsProviderId (string status, int? start, int? limit);
        /// <summary>
        /// Get all entitlements for a specific school Retrieve all entilements for a specific school, the school will have approved the LMS to have access to its Marketplace during the setup phase.  This API has pagination, using &#x60;start&#x60; and &#x60;limit&#x60;, with an optional &#x60;status&#x60; filter.
        /// </summary>
        /// <param name="id">School identifier.</param>
        /// <param name="status">Filter by status</param>
        /// <param name="start">Start point for pagination of results, defaults to 0,</param>
        /// <param name="limit">Limit of number of results returned by page, defaults to 20 with max 100.</param>
        /// <returns>Entitlements</returns>
        Entitlements GetEntitlementsSchoolId (string id, string status, int? start, int? limit);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class EntitlementsApi : IEntitlementsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitlementsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public EntitlementsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitlementsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public EntitlementsApi(String basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }
    
        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(String basePath)
        {
            this.ApiClient.BasePath = basePath;
        }
    
        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public String GetBasePath(String basePath)
        {
            return this.ApiClient.BasePath;
        }
    
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient {get; set;}
    
        /// <summary>
        /// Get a specific entitlement Retrieve the full specification for an entitlement from a marketplace, needs to confirm that the caller is able to access either based on their provider information (they can only see their own products) in the case of the provider scope.
        /// </summary>
        /// <param name="id">Specific identifier of an entitlement.</param>
        /// <returns>Entitlement</returns>
        public Entitlement GetEntitlementsById (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetEntitlementsById");
    
            var path = "/entitlements/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetEntitlementsById: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetEntitlementsById: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Entitlement) ApiClient.Deserialize(response.Content, typeof(Entitlement), response.Headers);
        }
    
        /// <summary>
        /// Get all entitlements for a specific vendor (Learning Application Provider) Get all entitlements related to products from a specific provider of a learning application (or other product used by a school).  It is assumed that at the point of setup and provision of an API key, that the provider name is also stored against the API key by the marketplace, so that all calls with this key will be filtered to that specific provider in the response.  This API has pagination, using &#x60;start&#x60; and &#x60;limit&#x60;, with an optional &#x60;status&#x60; filter.
        /// </summary>
        /// <param name="status">Filter by status </param>
        /// <param name="start">Start point for pagination of results, defaults to 0,</param>
        /// <param name="limit">Limit of number of results returned by page, defaults to 20 with max 100.</param>
        /// <returns>Entitlements</returns>
        public Entitlements GetEntitlementsProviderId (string status, int? start, int? limit)
        {
    
            var path = "/entitlements/provider";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (status != null) queryParams.Add("status", ApiClient.ParameterToString(status)); // query parameter
 if (start != null) queryParams.Add("start", ApiClient.ParameterToString(start)); // query parameter
 if (limit != null) queryParams.Add("limit", ApiClient.ParameterToString(limit)); // query parameter
                        
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetEntitlementsProviderId: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetEntitlementsProviderId: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Entitlements) ApiClient.Deserialize(response.Content, typeof(Entitlements), response.Headers);
        }
    
        /// <summary>
        /// Get all entitlements for a specific school Retrieve all entilements for a specific school, the school will have approved the LMS to have access to its Marketplace during the setup phase.  This API has pagination, using &#x60;start&#x60; and &#x60;limit&#x60;, with an optional &#x60;status&#x60; filter.
        /// </summary>
        /// <param name="id">School identifier.</param>
        /// <param name="status">Filter by status</param>
        /// <param name="start">Start point for pagination of results, defaults to 0,</param>
        /// <param name="limit">Limit of number of results returned by page, defaults to 20 with max 100.</param>
        /// <returns>Entitlements</returns>
        public Entitlements GetEntitlementsSchoolId (string id, string status, int? start, int? limit)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetEntitlementsSchoolId");
    
            var path = "/entitlements/school/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (status != null) queryParams.Add("status", ApiClient.ParameterToString(status)); // query parameter
 if (start != null) queryParams.Add("start", ApiClient.ParameterToString(start)); // query parameter
 if (limit != null) queryParams.Add("limit", ApiClient.ParameterToString(limit)); // query parameter
                        
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetEntitlementsSchoolId: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetEntitlementsSchoolId: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Entitlements) ApiClient.Deserialize(response.Content, typeof(Entitlements), response.Headers);
        }
    
    }
}
